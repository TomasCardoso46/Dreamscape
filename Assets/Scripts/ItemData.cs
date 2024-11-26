using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Scriptable Objects/InventoryItem")]
public class ItemData : ScriptableObject
{
    public enum ItemType {Key, Object}
    public string itemName;
    public Sprite itemIcon;
    public ItemType itemType;
}
