using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _interactionPanel;
    [SerializeField] private GameObject _inventorySlotsContainer;
    [SerializeField] private GameObject _inventoryIconsContainer;
    [SerializeField] private Color      _unselectedSlotColor;
    [SerializeField] private Color      _selectedSlotColor;
    
    private TextMeshProUGUI _interactionMessage;
    private Image[]         _inventorySlots;
    private Image[]         _inventoryIcons;
    private int             _selectedSlotIndex;

    void Start()
    {
        _interactionMessage = GetComponentInChildren<TextMeshProUGUI>();
        _inventorySlots     = _inventorySlotsContainer.GetComponentsInChildren<Image>();
        _inventoryIcons     = _inventoryIconsContainer.GetComponentsInChildren<Image>();
        _selectedSlotIndex  = -1;

        HideInteractionPanel();
        HideInventoryIcons();
        ResetInventorySlots();
    }

    public void HideInteractionPanel()
    {
        _interactionPanel.SetActive(false);
    }

    public void ShowInteractionPanel(string message)
    {
        _interactionMessage.text = message;
        _interactionPanel.SetActive(true);
    }

    public int GetInventorySlotCount()
    {
        return _inventorySlots.Length;
    }

    public void HideInventoryIcons()
    {
        foreach (Image image in _inventoryIcons)
            image.enabled = false;
    }

    private void ResetInventorySlots()
    {
        foreach (Image image in _inventorySlots)
            image.color = _unselectedSlotColor;
    }

    public void ShowInventoryIcon(int index, Sprite icon)
    {
        _inventoryIcons[index].sprite   = icon;
        _inventoryIcons[index].enabled  = true;
    }

    public void SelectInventorySlot(int index)
    {
        if (_selectedSlotIndex != -1)
            _inventorySlots[_selectedSlotIndex].color = _unselectedSlotColor;

        if (index != -1)
        {
            _inventorySlots[index].color = _selectedSlotColor;
            _selectedSlotIndex = index;
        }
    }
}
