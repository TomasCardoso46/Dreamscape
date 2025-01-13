using UnityEngine;

public class DoorScript : MonoBehaviour, IPuzzle
{
    private bool isPuzzleActive = false;
    private string hitTag;
    private string red = "Key_Red";
    private string green = "Key_Green";
    private string blue = "Key_Blue";
    private string yellow = "Key_Yellow";
    private string purple = "Key_Purple";
    [SerializeField]
    private int locksOpened = 0;
    private bool rotationCompleted = true;
    [SerializeField]
    private float rotationDistance;
    [SerializeField]
    private Transform objectToRotate;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private InteractionTrigger interactionTrigger;

    // Update is called once per frame
    void Update()
    {
        if (rotationCompleted == false)
        {
            RotateY();
        }
         if (Input.GetMouseButtonDown(0) && isPuzzleActive)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.Log("Clicked");

            if (Physics.Raycast(ray, out hit))
            {
                hitTag = hit.collider.tag;
                switch(hitTag)
                {
                    case "RedLock":
                        Debug.Log("Red lock hit");
                        if (InventoryManager.Instance.HasItem(red))
                        {
                            EnableFall(hit.collider.gameObject);
                            locksOpened++;
                        }
                        break;
                    case "GreenLock":
                        Debug.Log("Green lock hit");
                        if (InventoryManager.Instance.HasItem(green))
                        {
                            EnableFall(hit.collider.gameObject);
                            locksOpened++;
                        }
                        break;
                    case "BlueLock":
                        Debug.Log("Blue lock hit");
                        if (InventoryManager.Instance.HasItem(blue))
                        {
                            EnableFall(hit.collider.gameObject);
                            locksOpened++;
                        }
                        break;
                    case "YellowLock":
                        Debug.Log("Yellow lock hit");
                        if (InventoryManager.Instance.HasItem(yellow))
                        {
                            EnableFall(hit.collider.gameObject);
                            locksOpened++;
                        }
                        break;
                    case "PurpleLock":
                        Debug.Log("Purple lock hit");
                        if (InventoryManager.Instance.HasItem(purple))
                        {
                            EnableFall(hit.collider.gameObject);
                            locksOpened++;
                        }
                        break;
                }
                if (locksOpened >=5)
                {
                    rotationCompleted = false;
                    interactionTrigger.ToggleInteraction();
                    SetPuzzleActive(false);
                }
            }
        }
    }

    public void SetPuzzleActive(bool active)
    {
        isPuzzleActive = active;
        Cursor.visible = isPuzzleActive;
        Cursor.lockState = isPuzzleActive ? CursorLockMode.None : CursorLockMode.Locked;
    }
    public void EnableFall(GameObject objectToFall)
    {
        // Check if the GameObject has a Rigidbody component
        Rigidbody rb = objectToFall.GetComponent<Rigidbody>();
        
        if (rb != null)
        {
            // Unfreeze the position and rotation on the Rigidbody
            rb.constraints = RigidbodyConstraints.None;
            Debug.Log($"{objectToFall.name} can now fall and rotate freely!");
        }
        else
        {
            Debug.LogWarning($"{objectToFall.name} does not have a Rigidbody component.");
        }
    }
    private void RotateY()
    {
        if (rotationCompleted == false)
        {
            Debug.Log("Starting Rotation");
            Quaternion targetRotation = Quaternion.Euler(0f, rotationDistance, 0f);

            objectToRotate.localRotation = Quaternion.RotateTowards(objectToRotate.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    
}
