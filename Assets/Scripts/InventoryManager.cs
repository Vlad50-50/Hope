using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject slotPref;
    public GameObject inventoryPanel, chestPanel, deskrptionPanel;
    public GameObject invContent, chestContent;
    public ItemData[] items;
    public List<GameObject> inventorySlots = new List<GameObject>();
    public List<GameObject> currentChestSlots = new List<GameObject>();
    void Awake() {
        inventoryPanel = GameObject.Find("InventoryPanel");
        chestPanel = GameObject.Find("ChestPanel");
        deskrptionPanel = GameObject.Find("DeskPanel");
        invContent = GameObject.Find("InventoryContent");
        chestContent = GameObject.Find("ChestContent");
    }
    void Start()
    {
        deskrptionPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        chestPanel.SetActive(false);
    }
    public void CreateItem(int itemId, List<ItemData> itemList)
    {
        ItemData item = new ItemData(items[itemId].name, 
                                    items[itemId].id,
                                    items[itemId].count,
                                    items[itemId].isUniq,
                                    items[itemId].description);
        if(item.isUniq && item.count>0)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (item.id == itemList[i].id)
                {
                    itemList[i].count++;
                    break;
                }
                else if (i == itemList.Count-1)
                {
                    itemList.Add(item);
                    break;
                }
            }
        }
        else if (item.isUniq || (!item.isUniq && itemList.Count == 0))
        {
            itemList.Add(item);
        }
    }

    public void InstantiatingItem(ItemData item, Transform parent, List<GameObject> itemList)
    {
        GameObject go = Instantiate(slotPref);
        go.transform.SetParent(parent.transform);
        go.AddComponent<Slot>();
        go.GetComponent<Slot>().itemData = item;
        go.transform.Find("ItemNameText").GetComponent<Text>().text = item.name;
        go.transform.Find("ItemImage").GetComponent<Image>().sprite =
                                        Resources.Load<Sprite>(item.name);
        go.transform.Find("ItemCountText").GetComponent<Text>().text = item.count.ToString();
        go.transform.Find("ItemCountText").GetComponent<Text>().color = 
                                        item.isUniq ? Color.clear : Color.white;
        itemList.Add(go);
    }
    void Update()
    {
        
    }
}
