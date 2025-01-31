using UnityEngine;
using UnityEngine.UI;  // Make sure to import the UI namespace
using TMPro;

public class InteractionNote : MonoBehaviour
{
    public string targetTag = "Player";
    public Transform cameraTargetPosition;
    public Camera mainCamera;
    public MonoBehaviour puzzleScript;  // Use MonoBehaviour for inspector
    private IPuzzle puzzle;  // Actual interface reference
    public TextMeshProUGUI interactionText;  // Reference to the UI Text component

    private bool isPlayerInside = false;
    private bool isInteractionActive = false;
    private Vector3 originalCameraPosition;
    private Quaternion originalCameraRotation;
    private MonoBehaviour playerMovementScript;
    [SerializeField]
    private Collider targetCollider1;
    [SerializeField]
    private Collider targetCollider2;
    [SerializeField]
    private GameObject targetObject;

    private void Start()
    {
        // Check if the assigned puzzleScript implements IPuzzle
        if (puzzleScript is IPuzzle)
        {
            puzzle = puzzleScript as IPuzzle;
        }
        else
        {
            Debug.LogWarning("Assigned puzzleScript does not implement IPuzzle interface");
        }

        // Initially hide the interaction text
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            isPlayerInside = true;
            playerMovementScript = other.GetComponent<MonoBehaviour>();
            Debug.Log("Player entered trigger zone");

            // Show the interaction text when the player enters the trigger
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            isPlayerInside = false;
            if (isInteractionActive)
            {
                ToggleInteraction();
            }
            Debug.Log("Player exited trigger zone");

            MusicBox musicBox = GetComponent<MusicBox>();
            if (musicBox != null)
                musicBox.SetMusicBoxToFalse();

            // Hide the interaction text when the player leaves the trigger
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            ToggleInteraction();
            
            // Hide the interaction text once the player interacts
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(false);
            }
        }
    }

    public void ToggleInteraction()
    {
        if (isInteractionActive)
        {
            AudioManager.Instance.PlaySFX(12);
            UnlockPlayerMovement();
            ResetCameraPosition();
            ActivateCollider(targetCollider1);
            ActivateCollider(targetCollider2);
            puzzle?.SetPuzzleActive(false);
            DeActivateObject(targetObject);
            Debug.Log("Puzzle deactivated, camera reset");
        }
        else
        {
            AudioManager.Instance.PlaySFX(12);
            LockPlayerMovement();
            ChangeCameraPosition();
            DeactivateCollider(targetCollider1);
            DeactivateCollider(targetCollider2);
            puzzle?.SetPuzzleActive(true);
            Cursor.visible = true;
            ActivateObject(targetObject);
            Debug.Log("Puzzle activated, camera changed position");
        }
        
        isInteractionActive = !isInteractionActive;
    }

    private void LockPlayerMovement()
    {
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = false;
            Debug.Log("Player movement locked");
        }
    }

    private void UnlockPlayerMovement()
    {
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = true;
            Debug.Log("Player movement unlocked");
        }
    }

    private void ChangeCameraPosition()
    {
        if (cameraTargetPosition != null)
        {
            originalCameraPosition = mainCamera.transform.position;
            originalCameraRotation = mainCamera.transform.rotation;
            mainCamera.transform.position = cameraTargetPosition.position;
            mainCamera.transform.rotation = cameraTargetPosition.rotation;
            Debug.Log("Camera moved to target position");
        }
    }

    private void ResetCameraPosition()
    {
        mainCamera.transform.position = originalCameraPosition;
        mainCamera.transform.rotation = originalCameraRotation;
        Debug.Log("Camera reset to original position");
    }
    public void DeactivateCollider(Collider colliderToDeactivate)
    {
        if (colliderToDeactivate != null)
        {
            colliderToDeactivate.enabled = false;
            Debug.Log("Collider deactivated.");
        }
        else
        {
            Debug.LogWarning("No collider assigned!");
        }
    }
    public void ActivateCollider(Collider colliderToActivate)
    {
        if (colliderToActivate != null)
        {
            colliderToActivate.enabled = true;
            Debug.Log("Collider activated.");
        }
        else
        {
            Debug.LogWarning("No collider assigned!");
        }
    }

    public void ActivateObject(GameObject objectToActivate)
    {
        objectToActivate.SetActive(true);
        Debug.Log($"{objectToActivate.name} has been activated.");
    }
    public void DeActivateObject(GameObject objectToDeActivate)
    {
        objectToDeActivate.SetActive(false);
        Debug.Log($"{objectToDeActivate.name} has been activated.");
    }
}
