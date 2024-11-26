using UnityEngine;
using UnityEngine.UI;  // Make sure to import the UI namespace
using TMPro;

public class InteractionTrigger : MonoBehaviour
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

    private void ToggleInteraction()
    {
        if (isInteractionActive)
        {
            UnlockPlayerMovement();
            ResetCameraPosition();
            puzzle?.SetPuzzleActive(false);
            Debug.Log("Puzzle deactivated, camera reset");
        }
        else
        {
            LockPlayerMovement();
            ChangeCameraPosition();
            puzzle?.SetPuzzleActive(true);
            Cursor.visible = true;
            
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
}
