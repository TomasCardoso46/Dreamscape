using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Transform _itemReceivedContainer;
    [SerializeField] private GameObject _itemReceivedBg;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private Transform _inventorySlotsContainer;
    [SerializeField] private GameObject _inventory;
    [SerializeField] private GameObject _inventorySlotsUI;
    [SerializeField] private GameObject _PsPuzzle;
    [SerializeField] private GameObject _PsHint;
    
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
    
    //private TextMeshProUGUI _interactionMessage;

    //SerializeField of slotImages, if we got images for background.
    private List<Image> slotImages = new List<Image>();

    private bool isInventoryOpen;

    private void Awake()
    {
        if (_inventorySlotsContainer != null && slotImages.Count == 0)
        {
            foreach (Transform child in _inventorySlotsContainer)
            {
                var slotImage = child.GetComponent<Image>();
                if (slotImage != null)
                    slotImages.Add(slotImage);
            }
        }
    }

    private void Start()
    {
        //_interactionMessage = GetComponentInChildren<TextMeshProUGUI>();

        HidePSPuzzleUI();
        HidePSHintPuzzleUI();
        HideInventorySlotsUI();
        HidePauseMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (_inventory.activeSelf)
            {
                isInventoryOpen = true;
                HideInventory();
            }
            else
            {
                isInventoryOpen = false;
                ShowInventory();
            }
        }

        if (isInventoryOpen)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("Key 1 was pressed while inventory is open.");
                InventoryManager.Instance.UseItem(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("Key 2 was pressed while inventory is open.");
                InventoryManager.Instance.UseItem(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("Key 3 was pressed while inventory is open.");
                InventoryManager.Instance.UseItem(3);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Debug.Log("Key 4 was pressed while inventory is open.");
                InventoryManager.Instance.UseItem(4);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                Debug.Log("Key 5 was pressed while inventory is open.");
                InventoryManager.Instance.UseItem(5);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                Debug.Log("Key 6 was pressed while inventory is open.");
                InventoryManager.Instance.UseItem(6);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                Debug.Log("Key 7 was pressed while inventory is open.");
                InventoryManager.Instance.UseItem(7);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                Debug.Log("Key 8 was pressed while inventory is open.");
                InventoryManager.Instance.UseItem(8);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                Debug.Log("Key 9 was pressed while inventory is open.");
                InventoryManager.Instance.UseItem(9);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_pauseMenu.activeSelf == false)
                PauseGame(true);
            else
                PauseGame(false);
        }
    }

#region "UI Inventory"
    public void ShowInventory()
    {
        _inventory.SetActive(true);
        HideInventorySlotsUI();
    }

    public void HideInventory()
    {
        _inventory.SetActive(false);
        ShowInventorySlotsUI();
    }

    public void ShowInventorySlotsUI()
    {
        _inventorySlotsUI.SetActive(true);
    }

    public void HideInventorySlotsUI()
    {
        _inventorySlotsUI.SetActive(false);
    }

    public void UpdateInventoryUI(List<ItemData> inventory)
    {
        // Limpa os slots antes de atualizar
        for (int i = 0; i < slotImages.Count; i++)
        {
            if (i < inventory.Count)
            {
                slotImages[i].sprite = inventory[i].itemIcon;
                slotImages[i].color = Color.white; // Torna visível o slot com o item
            }
            else
            {
                slotImages[i].sprite = null;
                slotImages[i].color = Color.clear; // Deixa o slot vazio invisível
            }
        }
    }

#endregion

    /*public void ShowInteractionPanel(string message)
    {
        _interactionMessage.text = message;
        _interactionPanel.SetActive(true);
    }*/

    private void HidePauseMenu()
    {
        _pauseMenu.SetActive(false);
    }

    public void ShowPickedItem(string item)
    {
        StartCoroutine(ShowPickedItemCO(item));
    }

    private IEnumerator ShowPickedItemCO(string item)
    {
        GameObject itemReceived = Instantiate(_itemReceivedBg, _itemReceivedContainer);
        TextMeshProUGUI itemText = itemReceived.GetComponentInChildren<TextMeshProUGUI>();
        itemText.text = $"x1 {item}";
        yield return new WaitForSeconds(2f);
        HideItemReceivedBg(itemReceived);
    }
    private void HideItemReceivedBg(GameObject itemBg)
    {
        itemBg.SetActive(false);
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

    public void ShowPSHintPuzzleUI()
    {
        _PsHint.SetActive(true);
    }

    public void HidePSHintPuzzleUI()
    {
        _PsHint.SetActive(false);
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


    public void LeavePauseMenu()
    {
        if (Time.timeScale == 0)
        {
            _pauseMenu.SetActive(false);

            Time.timeScale = 1;
            HideCursor();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Left game");
    }

    private void PauseGame(bool _pause)
    {
        if (_pause)
        {
            _pauseMenu.SetActive(_pause);
            Time.timeScale = 0;
            ShowCursor();
        }
        else
        {
            HidePauseMenu();
            Time.timeScale = 1;
            HideCursor();
        }
    }
}
