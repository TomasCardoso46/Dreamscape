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
    private string apple = "Apple";
    [SerializeField]
    private ParticleSystem smoke;
    [SerializeField]
    private InteractionTrigger interactionTrigger;
    [SerializeField]
    private MoveObjects moveObjects;
    [SerializeField]
    private int foodGiven = 0;
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
                if (InventoryManager.Instance.HasItem(banana))
                {
                    AudioManager.Instance.PlaySFX(7);
                    InventoryManager.Instance.UseItemWithString(banana);
                    foodGiven++;
                }
                if (InventoryManager.Instance.HasItem(cookies))
                {
                    AudioManager.Instance.PlaySFX(7);
                    InventoryManager.Instance.UseItemWithString(cookies);
                    foodGiven++;
                }
                if (InventoryManager.Instance.HasItem(apple))
                {
                    AudioManager.Instance.PlaySFX(7);
                    InventoryManager.Instance.UseItemWithString(apple);
                    foodGiven++;
                }

                switch (foodGiven)
                {
                    case 0:
                    {
                        ActivateObject(noFood);
                        break;
                    }
                    case 1:
                    {
                        DeActivateObject(noFood);
                        ActivateObject(oneFood);
                        break;
                    }
                    case 2:
                    {
                        DeActivateObject(noFood);
                        ActivateObject(oneFood);
                        break;
                    }
                    case 3:
                    {
                        DeActivateObject(noFood);
                        DeActivateObject(oneFood);
                        Destroy(hit.collider.gameObject);
                        smoke.loop = false;
                        moveObjects.StartMovement();
                        Debug.Log("Monster fed.");
                        interactionTrigger.ToggleInteraction();
                        SetPuzzleActive(false); // Deactivate puzzle after destroying monster
                        break;
                    }
                    
                    
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
