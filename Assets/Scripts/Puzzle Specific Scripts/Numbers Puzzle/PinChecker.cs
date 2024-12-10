using UnityEngine;

public class PinChecker : MonoBehaviour, IPuzzle
{
    [SerializeField] private GameObject[] numberObjects = new GameObject[9];
    private string inputString = "";
    private const string correctPin = "34197";
    private bool isPuzzleActive = false;
    public Animation cabinOpen;

    private void Start()
    {
        for (int i = 0; i < numberObjects.Length; i++)
        {
            int number = i + 1;  // Assigns numbers from 1 to 9
            numberObjects[i].name = number.ToString(); // Optional: Set object name for clarity
        }
    }

    private void Update()
    {
        if (!isPuzzleActive) return;

        if (Input.GetMouseButtonDown(0)) // Detect left mouse click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
                for (int i = 0; i < numberObjects.Length; i++)
                {
                    if (clickedObject == numberObjects[i])
                    {
                        OnNumberClicked((i + 1).ToString()); // Pass the number associated with the object
                        break;
                    }
                }
            }
        }
    }

    public void SetPuzzleActive(bool active)
    {
        isPuzzleActive = active;
        Cursor.visible = isPuzzleActive;  // Show cursor when puzzle is active
        Cursor.lockState = isPuzzleActive ? CursorLockMode.None : CursorLockMode.Locked;  // Unlock cursor when active
        Debug.Log(isPuzzleActive ? "PinChecker puzzle activated" : "PinChecker puzzle deactivated");
    }

    private void OnNumberClicked(string number)
    {
        if (!isPuzzleActive) return;

        inputString += number;
        Debug.Log("Current input: " + inputString);

        if (inputString.Length == 5)
        {
            if (inputString == correctPin)
            {
                Debug.Log("Victory");
                cabinOpen.Play("Cabinet_Door_Open");
            }
            else
            {
                Debug.Log("Wrong pin");
            }
            inputString = "";  // Reset input for the next attempt
        }
    }
}
