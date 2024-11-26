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
            numberObjects[i].AddComponent<Clickable>().Setup(this, number.ToString());
        }
    }

    public void SetPuzzleActive(bool active)
    {
        isPuzzleActive = active;
        Cursor.visible = isPuzzleActive;  // Show cursor when puzzle is active
        Cursor.lockState = isPuzzleActive ? CursorLockMode.None : CursorLockMode.Locked;  // Unlock cursor when active
        Debug.Log(isPuzzleActive ? "PinChecker puzzle activated" : "PinChecker puzzle deactivated");
    }

    public void OnNumberClicked(string number)
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

public class Clickable : MonoBehaviour
{
    private PinChecker pinChecker;
    private string number;

    public void Setup(PinChecker checker, string num)
    {
        pinChecker = checker;
        number = num;
    }

    private void OnMouseDown()
    {
        pinChecker.OnNumberClicked(number);
    }
}
