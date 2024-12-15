using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 3f; 
    [SerializeField] private LayerMask interactableLayer; // item layer

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) //mouse left
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance, interactableLayer))
            {
                Interactive interactive = hit.collider.GetComponent<Interactive>();
                if (interactive != null)
                {
                    interactive.Interact();
                }
            }
        }
    }
}
