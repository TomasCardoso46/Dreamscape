using UnityEngine;

public class DoorScript : MonoBehaviour, IPuzzle
{
    [SerializeField] private UIManager uIManager;
    private bool isPuzzleActive = false;
    private string hitTag;
    private string red = "Red Key";
    private string green = "Green Key";
    private string blue = "Blue Key";
    private string yellow = "Yellow Key";
    private string purple = "Magenta Key";
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
    private void Update()
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
                            AudioManager.Instance.PlaySFX(6);
                            AudioManager.Instance.PlaySFX(14);
                            InventoryManager.Instance.UseItemWithString(red);
                            EnableFall(hit.collider.gameObject);
                            locksOpened++;
                        }
                        break;
                    case "GreenLock":
                        Debug.Log("Green lock hit");
                        if (InventoryManager.Instance.HasItem(green))
                        {
                            AudioManager.Instance.PlaySFX(6);
                            AudioManager.Instance.PlaySFX(14);
                            InventoryManager.Instance.UseItemWithString(green);
                            EnableFall(hit.collider.gameObject);
                            locksOpened++;
                        }
                        break;
                    case "BlueLock":
                        Debug.Log("Blue lock hit");
                        if (InventoryManager.Instance.HasItem(blue))
                        {
                            AudioManager.Instance.PlaySFX(6);
                            AudioManager.Instance.PlaySFX(14);
                            InventoryManager.Instance.UseItemWithString(blue);
                            EnableFall(hit.collider.gameObject);
                            locksOpened++;
                        }
                        break;
                    case "YellowLock":
                        Debug.Log("Yellow lock hit");
                        if (InventoryManager.Instance.HasItem(yellow))
                        {
                            AudioManager.Instance.PlaySFX(6);
                            AudioManager.Instance.PlaySFX(14);
                            InventoryManager.Instance.UseItemWithString(yellow);
                            EnableFall(hit.collider.gameObject);
                            locksOpened++;
                        }
                        break;
                    case "PurpleLock":
                        Debug.Log("Purple lock hit");
                        if (InventoryManager.Instance.HasItem(purple))
                        {
                            AudioManager.Instance.PlaySFX(6);
                            AudioManager.Instance.PlaySFX(14);
                            InventoryManager.Instance.UseItemWithString(purple);
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
            AudioManager.Instance.PlaySFX(4);
            Quaternion targetRotation = Quaternion.Euler(0f, rotationDistance, 0f);

            objectToRotate.localRotation = Quaternion.RotateTowards(objectToRotate.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    
}
