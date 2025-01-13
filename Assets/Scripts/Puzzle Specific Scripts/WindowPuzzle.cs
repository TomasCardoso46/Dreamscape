using System.Collections;
using UnityEngine;

public class WindowPuzzle : MonoBehaviour, IPuzzle
{
    private bool isPuzzleActive = false;
    [SerializeField] private LayerMask interactableLayer;
    private string ClipItemName = "PaperClip";
    public bool isWindowLocked = true;
    [SerializeField]
    private float speed = 5f; // Speed at which the object moves
    [SerializeField]
    private float targetZ = 10f; // Target x-coordinate to stop moving
    [SerializeField]
    private GameObject objectToMove;
    [SerializeField]
    private InteractionTriggerWindow interWindow;
    [SerializeField]
    private GameObject childToActivate;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("WindowLock"))
            {
                if (InventoryManager.Instance.HasItem(ClipItemName))
                {
                    Debug.Log("Window Unlocked");
                    Destroy(hit.collider.gameObject);
                    isWindowLocked = false;
                    MoveObjectToRight();
                    interWindow.canReachOut = true;
                    interWindow.ToggleInteraction();
                }
                else
                {
                    Debug.Log("Still locked");
                }
            }
            
            if (Physics.Raycast(ray, out hit, 100, interactableLayer))
            {
                Debug.Log("Clip clicked");
                Interactive interactive = hit.collider.GetComponent<Interactive>();
                interactive.Interact();
            }
            
        }
    }

    public void SetPuzzleActive(bool active)
    {
        isPuzzleActive = active;
        Cursor.visible = isPuzzleActive;
        Cursor.lockState = isPuzzleActive ? CursorLockMode.None : CursorLockMode.Locked;
        Debug.Log(isPuzzleActive ? "Window puzzle activated" : "Window puzzle deactivated");
    }

    // Method to start the movement
    public void MoveObjectToRight()
    {
        Vector3 destination = new Vector3(objectToMove.transform.localPosition.x, objectToMove.transform.localPosition.y, targetZ);
        StartCoroutine(MoveObjectToPosition(objectToMove, destination));
    }

    // Coroutine to move the object
    private IEnumerator MoveObjectToPosition(GameObject movingObject, Vector3 destination)
    {
        float stopThreshold = 0.0001f; // Adjust this threshold for precision

        while (Mathf.Abs(movingObject.transform.localPosition.y - destination.y) > stopThreshold)
        {
            // Move the object towards the destination
            movingObject.transform.localPosition = Vector3.MoveTowards(movingObject.transform.localPosition, destination, speed * Time.deltaTime);
            yield return null; // Wait until the next frame
        }

        // Final adjustment to ensure exact position
        movingObject.transform.localPosition = destination;
    }

    // Method to activate the specified child
    public void ActivateSpecifiedChild()
    {
        childToActivate.SetActive(true);
        Debug.Log($"{childToActivate.name} has been activated.");
    }
    public void DeactivateSpecifiedChild()
    {
        childToActivate.SetActive(false);
        Debug.Log($"{childToActivate.name} has been activated");
    }
}
