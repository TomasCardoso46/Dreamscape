using UnityEngine;

public class MonsterInteraction : MonoBehaviour, IPuzzle
{
    [SerializeField] private string keyItemName = "Key";
    private bool isPuzzleActive = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isPuzzleActive)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Monster"))
            {
                if (InventoryManager.Instance.HasItem(keyItemName))
                {
                    Destroy(hit.collider.gameObject);
                    Debug.Log("Monster destroyed using key.");
                    SetPuzzleActive(false); // Deactivate puzzle after destroying monster
                }
                else
                {
                    Debug.Log("You need a key to destroy the monster.");
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
}
