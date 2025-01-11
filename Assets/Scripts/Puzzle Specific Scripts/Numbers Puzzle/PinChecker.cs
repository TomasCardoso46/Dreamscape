using System.Collections;
using UnityEngine;

public class PinChecker : MonoBehaviour, IPuzzle
{
    [SerializeField] private GameObject[] numberObjects = new GameObject[9];
    private string inputString = "";
    private const string correctPin = "34197";
    private bool isPuzzleActive = false;
    [SerializeField]
    private InteractionTrigger interactionTrigger;
    [SerializeField]
    private float rotationDistance = 90f;
    [SerializeField]
    private float rotationSpeed = 45f;
    [SerializeField]
    private Transform objectToRotate;
    private bool rotationCompleted = true;

    [SerializeField] private float moveSpeed = 2f; // Speed for moving objects
    private bool isResetting = false;  // Flag to track if a reset is in progress

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
        if (rotationCompleted == false)
        {
            RotateY(-84);
        }
        if (!isPuzzleActive) return;

        if (Input.GetMouseButtonDown(0)) // Detect left mouse click
        {
            Debug.Log("Clicked");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("Raycast working");
                GameObject clickedObject = hit.collider.gameObject;
                for (int i = 0; i < numberObjects.Length; i++)
                {
                    if (clickedObject == numberObjects[i])
                    {
                        Debug.Log($"{clickedObject}");
                        OnNumberClicked((i + 1).ToString()); // Pass the number associated with the object
                         MoveNumberOnClick(numberObjects[i]);
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

        // Prevent clicking if reset is in progress
        if (isResetting) return;

        inputString += number;
        Debug.Log("Current input: " + inputString);

        // Disable the clicked number to prevent re-clicking
        DisableNumber(number);

        if (inputString.Length == 5)
        {
            if (inputString == correctPin)
            {
                Debug.Log("Victory");
                rotationCompleted = false;
                interactionTrigger.ToggleInteraction();
            }
            else
            {
                Debug.Log("Wrong pin");
                StartCoroutine(ResetAfterDelay()); // Start the reset after a delay
            }
            inputString = "";  // Reset input for the next attempt
        }
    }

    private IEnumerator ResetAfterDelay()
    {
        isResetting = true; // Set flag to true, blocking further interactions

        // Wait for 1 second before resetting
        yield return new WaitForSeconds(1f);
        
        // Now reset all numbers
        ResetAllNumbers();

        // Re-enable all number objects after the reset
        EnableAllNumbers();

        isResetting = false; // Reset flag after the reset is complete
    }

    // Disables the clicked number object
    private void DisableNumber(string number)
    {
        int index = int.Parse(number) - 1;
        numberObjects[index].GetComponent<Collider>().enabled = false; // Disable collider to prevent clicking again
    }

    // Re-enables all number objects
    private void EnableAllNumbers()
    {
        foreach (GameObject numberObject in numberObjects)
        {
            numberObject.GetComponent<Collider>().enabled = true; // Re-enable collider (or interactions)
        }
    }

    // Reset all numbers' Y position to 0
    public void ResetAllNumbers()
    {
        foreach (GameObject numberObject in numberObjects)
        {
            Vector3 destination = new Vector3(numberObject.transform.localPosition.x, 0f, numberObject.transform.localPosition.z); // Y = 0 for reset
            StartCoroutine(MoveObjectToPosition(numberObject, destination));
        }
    }

    // Move a specific number on the Y-axis
    private void MoveNumberOnClick(GameObject numberObject)
    {
        Vector3 destination = new Vector3(numberObject.transform.localPosition.x, -0.00115f, numberObject.transform.localPosition.z); // Set destination Y as 1 (or any other value)
        StartCoroutine(MoveObjectToPosition(numberObject, destination));
    }

    // Coroutine to move an object towards a destination
    private IEnumerator MoveObjectToPosition(GameObject numberObject, Vector3 destination)
    {
        float stopThreshold = 0.0001f; // Adjust this threshold for precision

        while (Mathf.Abs(numberObject.transform.localPosition.y - destination.y) > stopThreshold)
        {
            // Move the object towards the destination
            numberObject.transform.localPosition = Vector3.MoveTowards(numberObject.transform.localPosition, destination, moveSpeed * Time.deltaTime);
            yield return null; // Wait until the next frame
        }

        // Final adjustment to ensure exact position
        numberObject.transform.localPosition = destination;
    }

    private void RotateY(float rotationDistance)
    {
        if (rotationCompleted == false)
        {
            Debug.Log("Starting Rotation");
            Quaternion targetRotation = Quaternion.Euler(0f, rotationDistance, 0f);

            objectToRotate.localRotation = Quaternion.RotateTowards(objectToRotate.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
