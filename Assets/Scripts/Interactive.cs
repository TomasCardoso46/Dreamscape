using UnityEngine;

public class Interactive : MonoBehaviour
{
    public ItemData itemData;

    public void Interact()
    {
        Debug.Log($"Interacted with {itemData.itemName}");
        InventoryManager.Instance.AddItem(itemData);
        AudioManager.Instance.PlaySFX(3);

        switch (itemData.itemType)
        {
            case ItemData.ItemType.Key:
                gameObject.SetActive(false);
                break;

            case ItemData.ItemType.Object:
                gameObject.SetActive(false);
                break;
            default:
                Debug.Log("Unknown item type.");
                break;
        }
    }
}
