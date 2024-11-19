using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    public string targetTag = "Player";
    public Transform cameraTargetPosition;
    public Camera mainCamera;
    public Puzzle1 puzzleScript;

    private bool isPlayerInside = false;
    private bool isInteractionActive = false;
    private Vector3 originalCameraPosition;
    private Quaternion originalCameraRotation;
    private MonoBehaviour playerMovementScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            isPlayerInside = true;
            playerMovementScript = other.GetComponent<MonoBehaviour>();
            Debug.Log("Player entered trigger zone");
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
        }
    }

    private void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            ToggleInteraction();
        }
    }

    private void ToggleInteraction()
    {
        if (isInteractionActive)
        {
            UnlockPlayerMovement();
            ResetCameraPosition();
            puzzleScript.SetPuzzleActive(false);
            Debug.Log("Puzzle deactivated, camera reset");
        }
        else
        {
            LockPlayerMovement();
            ChangeCameraPosition();
            puzzleScript.SetPuzzleActive(true);
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
