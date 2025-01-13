using UnityEngine;

public class MonsterInteraction : MonoBehaviour, IPuzzle
{
    [SerializeField]
    private GameObject noFood;
    [SerializeField]
    private GameObject oneFood;
    private bool isPuzzleActive = false;
    private string banana = "Banana";
    private string cookies = "Cookies";
    [SerializeField]
    private ParticleSystem smoke;
    [SerializeField]
    private InteractionTrigger interactionTrigger;
    [SerializeField]
    private MoveObjects moveObjects;
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && isPuzzleActive)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.Log("Clicked");

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Monster"))
            {
                Debug.Log("Monster Hit");
                if (InventoryManager.Instance.HasItem(banana) && InventoryManager.Instance.HasItem(cookies))
                {
                    DeActivateObject(noFood);
                    DeActivateObject(oneFood);
                    Destroy(hit.collider.gameObject);
                    smoke.loop = false;
                    moveObjects.StartMovement();
                    Debug.Log("Monster fed.");
                    interactionTrigger.ToggleInteraction();
                    SetPuzzleActive(false); // Deactivate puzzle after destroying monster
                }
                else if (InventoryManager.Instance.HasItem(banana) || InventoryManager.Instance.HasItem(cookies))
                {
                    DeActivateObject(noFood);
                    ActivateObject(oneFood);
                    Debug.Log("Needs more food");
                }
                else
                {
                    ActivateObject(noFood);
                    Debug.Log("No food");
                }

            }
        }
    }

    public void SetPuzzleActive(bool active)
    {
        isPuzzleActive = active;
        Cursor.visible = isPuzzleActive;
        Cursor.lockState = isPuzzleActive ? CursorLockMode.None : CursorLockMode.Locked;
        Debug.Log(isPuzzleActive ? "Monster interaction puzzle activated" : "Monster interaction puzzle deactivated");
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
