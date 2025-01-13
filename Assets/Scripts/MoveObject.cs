using UnityEngine;

public class MoveObjects : MonoBehaviour
{
    // Reference to the target position in the Inspector
    public Transform targetObject;

    // Speed of the movement
    public float speed = 1.0f;

    // Original position of the GameObject
    private Vector3 originalPosition;
    private Vector3 targetPosition;

    // State to track if moving to target or returning
    private bool movingToTarget = false; // Start with not moving

    void Start()
    {
        // Store the original position of the object
        originalPosition = transform.position;

        // Set the target position to the target object's position
        targetPosition = targetObject.position;
    }

    void Update()
    {
        // If not moving, exit the Update
        if (!movingToTarget) return;

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, movingToTarget ? targetPosition : originalPosition, speed * Time.deltaTime);
                                                  
                                                  

        // Check if the object has reached the target position or original position
        if (transform.position == targetPosition)
        {
            movingToTarget = false; // Start returning to original position
        }
        else if (transform.position == originalPosition)
        {
            movingToTarget = true; // Move back to target
        }
    }

    // Public method to start the movement
    public void StartMovement()
    {
        movingToTarget = true; // Set the state to start moving
    }
}
