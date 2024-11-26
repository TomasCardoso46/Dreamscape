using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _interactionPanel;
    [SerializeField] private GameObject _inventorySlotsContainer;
    [SerializeField] private GameObject _PsPuzzle;
    [SerializeField] private GameObject _inventoryIconsContainer;
    [SerializeField] private Color      _unselectedSlotColor;
    [SerializeField] private Color      _selectedSlotColor;
    

    [Header("Prefab References")]
    [SerializeField] private GameObject trianglePrefab;
    [SerializeField] private GameObject circlePrefab;
    [SerializeField] private GameObject crossPrefab;
    [SerializeField] private GameObject squarePrefab;
    [Header("Scroll View Content")]
    public Transform scrollViewContent; // The content object under the Scroll View
    [Header("PS Game Logic")]
    public List<string> correctCombination = new List<string>() { "Triangle", "Circle", "Cross", "Square" }; // The correct order
    private List<string> currentCombination = new List<string>(); // Stores the player's
    
    private TextMeshProUGUI _interactionMessage;
    private Image[]         _inventorySlots;
    private Image[]         _inventoryIcons;
    private int             _selectedSlotIndex;

    private void Start()
    {
        //_interactionMessage = GetComponentInChildren<TextMeshProUGUI>();
        //_inventorySlots     = _inventorySlotsContainer.GetComponentsInChildren<Image>();
        //_inventoryIcons     = _inventoryIconsContainer.GetComponentsInChildren<Image>();
        //_selectedSlotIndex  = -1;

        //HideInteractionPanel();
        //HideInventoryIcons();
        //ResetInventorySlots();
        HidePSPuzzleUI();
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

    #region "PS Puzzle"
    public void HidePSPuzzleUI()
    {
        _PsPuzzle.SetActive(false);
    }

    public void ShowPSPuzzleUI()
    {
        _PsPuzzle.SetActive(true);
    }

    public void SpawnTriangle()
    {
        AddPrefabToCombination("Triangle", trianglePrefab);
    }

    public void SpawnCircle()
    {
        AddPrefabToCombination("Circle", circlePrefab);
    }

    public void SpawnCross()
    {
        AddPrefabToCombination("Cross", crossPrefab);
    }

    public void SpawnSquare()
    {
        AddPrefabToCombination("Square", squarePrefab);
    }

    private void AddPrefabToCombination(string prefabName, GameObject prefab)
    {
        if (currentCombination.Count < 4)
        {
            // Add the prefab name to the current combination
            currentCombination.Add(prefabName);

            // Instantiate the prefab in the Scroll View
            Instantiate(prefab, scrollViewContent);
        }

        // Check if we reached 4 inputs
        if (currentCombination.Count == 4)
        {
            CheckCombination();
        }
    }

    private void CheckCombination()
    {
        // Compare the current combination with the correct one
        if (IsCombinationCorrect())
        {
            Debug.Log("I won!");
            //console animation
        }
        else
        {
            Debug.Log("You failed!");
            ResetCombination();
        }

        // Reset after checking
        //ResetCombination();
    }

    private bool IsCombinationCorrect()
    {
        // Check if the current combination matches the correct one
        if (currentCombination.Count != correctCombination.Count) return false;

        for (int i = 0; i < correctCombination.Count; i++)
        {
            if (currentCombination[i] != correctCombination[i])
                return false;
        }

        return true;
    }

    private void ResetCombination()
    {
        // Clear the current combination
        currentCombination.Clear();

        // Remove all instantiated prefabs from the Scroll View content
        foreach (Transform child in scrollViewContent)
        {
            Destroy(child.gameObject);
        }
    }
    #endregion

    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }
}
