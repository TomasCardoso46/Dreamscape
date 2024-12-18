using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField] private UIManager uiManager;
    [SerializeField] private MusicBox musicBox;

    private List<ItemData> inventory = new List<ItemData>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else 
            Destroy(gameObject);
    }

    public void AddItem(ItemData item)
    {
        inventory.Add(item);
        Debug.Log($"Added {item.itemName} to inventory");
        DebugInventory();

        uiManager.UpdateInventoryUI(inventory);
    }

    public void RemoveItem(ItemData item)
    {
        if (inventory.Contains(item))
        {
            inventory.Remove(item);
            Debug.Log($"Removed {item.itemName} from inventory");
            DebugInventory();

            uiManager.UpdateInventoryUI(inventory);
        }
    }

    private void DebugInventory()
    {
        Debug.Log("Current Inventory:");
        foreach (var item in inventory)
        {
            Debug.Log(item.itemName);
        }
    }

    public bool HasItem(string itemName)
    {
        foreach (var item in inventory)
        {
            if (item.itemName == itemName)
                return true;
        }
        return false;
    }

    public void UseItem(int number)
    {
        int count = 1;
        foreach (var item in inventory)
        {
            if (count == number)
            {
                if (item.itemType == ItemData.ItemType.Object && musicBox.IsMusicBoxActive)
                {
                    ItemData currentItem = item;
                    RemoveItem(item);
                    break;
                }
            }
            count++;
            if (count > number)
                continue;
        }
    }

}
