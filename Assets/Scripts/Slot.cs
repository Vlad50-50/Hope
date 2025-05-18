using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

interface IMovable {
    string name{get; set;}
    void Move();
}

public class Slot : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public ItemData itemData;
    private Transform tempParentForSlot;
    private InventoryManager inventoryManager;
    private PlayerController playerController;
    private string parentName;
    public void OnPointerUp(PointerEventData data){
        inventoryManager.deskrptionPanel.SetActive(false);
    }
    public void OnPointerDown(PointerEventData data){
        inventoryManager.deskrptionPanel.SetActive(true);
        inventoryManager.deskrptionPanel.transform.position = transform.position;
        if(itemData != null){
            inventoryManager.deskrptionPanel.transform.Find("DeskText").GetComponent<Text>().text = itemData.description;
        }
    }
 
    public void OnDrag(PointerEventData data){
        inventoryManager.deskrptionPanel.SetActive(false);
        transform.SetParent(tempParentForSlot);
        transform.position = data.position;
    }

    public void OnEndDrag(PointerEventData data){
        float slotICDist = Vector3.Distance(transform.position, inventoryManager.invContent.transform.position);
        float slotCCDist = Vector3.Distance(transform.position, inventoryManager.chestContent.transform.position);

        if (slotICDist < slotCCDist){
            if(parentName == "InventoryContent"){
                transform.SetParent(inventoryManager.invContent.transform);
            }
            else {
                inventoryManager.currentChestSlots.Remove(gameObject);
                playerController.currentChestItems.Remove(itemData);
                AddToListOnDrag(inventoryManager.inventorySlots, playerController.inventoryItems, inventoryManager.invContent.transform);
            }
        }
        else {
            if(parentName == "ChestContent"){
                transform.SetParent(inventoryManager.chestContent.transform);
            }
            else {
                inventoryManager.inventorySlots.Remove(gameObject);
                playerController.inventoryItems.Remove(itemData);
                AddToListOnDrag(inventoryManager.currentChestSlots, playerController.currentChestItems, inventoryManager.chestContent.transform);
            }
        }
    }

    public void OnPointerEnter(PointerEventData edata){
        if(itemData != null){
            playerController.itemYourCanEquipmqnt = itemData.name;

        }
    }

    public void OnPointerExit(PointerEventData edata){
        playerController.itemYourCanEquipmqnt = PlayerController.EQUIP_NOT_SELECTED_TEXT;
    }

    void AddToListOnDrag(List<GameObject> slots, List<ItemData> items, Transform parent){
        if(itemData == null) return;
        if(itemData.isUniq || slots.Count == 0){
            slots.Add(gameObject);
            items.Add(itemData);
            transform.SetParent(parent);
            parentName = transform.parent.name;

        }
        else if(!itemData.isUniq){
            for(int i = 0; i < slots.Count; ++i){
                if(slots[i].GetComponent<Slot>().itemData.id == itemData.id){
                    items[i].count += itemData.count;
                    slots[i].transform.Find("ItemCountText").GetComponent<Text>().text = slots[i].GetComponent<Slot>().itemData.count.ToString();
                    Destroy(gameObject);
                    break;
                }
                else if(i == slots.Count-1){
                    slots.Add(gameObject);
                    items.Add(itemData);
                    transform.SetParent(parent);
                    parentName = transform.parent.name;
                    break;
                }
            }
        }
    }

    void Start()
    {
        tempParentForSlot = GameObject.Find("Canvas").transform;
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        parentName = transform.parent.name;
    }
}
