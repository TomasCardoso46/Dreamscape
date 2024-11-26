using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

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
    }

    public void RemoveItem(ItemData item)
    {
        if (inventory.Contains(item))
        {
            inventory.Remove(item);
            Debug.Log($"Removed {item.itemName} from inventory");
        }
    }

    
}
