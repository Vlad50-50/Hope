using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public List<ItemData> chestItems = new List<ItemData>();
    void Awake()
    {
        InventoryManager inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        int itemCountinChest = Random.Range(3,8);
        for (int i = 0; i <itemCountinChest; i++)
        {
            inventoryManager.CreateItem(Random.Range(0, inventoryManager.items.Length), chestItems);
        }
    }
}
