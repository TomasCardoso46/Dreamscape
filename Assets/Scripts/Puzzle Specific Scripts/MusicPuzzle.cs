using UnityEngine;

public class MusicPuzzle : MonoBehaviour, IPuzzle
{
    public Transform top;
    public Transform nutcracker;
    public Transform lever;
    public float topRotationSpeed = 5f;
    public float nutcrackerMoveSpeed = 0.01f;
    public float leverRotationSpeed = 30f;

    private bool topCompleted = false;
    private bool nutcrackerCompleted = false;
    private bool leverCompleted = false;
    private bool isActive = false;
    private float leverRotation = 0f;
    [SerializeField]
    private Transform targetTransform;
    [SerializeField]
    private GameObject objectToMove;
    [SerializeField]
    private Transform originalPosition;
    private Quaternion originalRotation;
    private bool isDown = true;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float rotationSpeed2;
    private bool rotationCompleted;
    [SerializeField]
    private Transform objectToRotate;
    [SerializeField]
    private float rotationDistance;
    [SerializeField]
    private GameObject RedGear;
    [SerializeField]
    private GameObject YellowGear;
    [SerializeField]
    private GameObject GreenGear;
    private string GearGreenItemName = "EngrenagemVerde";
    private string GearRedItemName = "EngrenagemVermelha";
    [SerializeField]
    private bool puzzleStarted = false;
    private bool greenGearPlaced = false;
    private bool redGearPlaced = false;

    void Start()
    {
        originalRotation = objectToMove.transform.rotation;
    }
    void Update()
    {


        if (Input.GetMouseButton(1) && puzzleStarted == true)
        {
            RotateObjectWithMouse();
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("MusicDoor"))
            {
                RotateY();
            }
            else if (Physics.Raycast(ray,out hit) && hit.collider.CompareTag("GearGreen"))
            {
                Debug.Log("Green gear place hit");
                if(InventoryManager.Instance.HasItem(GearGreenItemName) && greenGearPlaced == false)
                {
                    Debug.Log("Green gear placed");
                    ActivateObject(GreenGear);
                    greenGearPlaced = true;
                }
            }
            else if (Physics.Raycast(ray,out hit) && hit.collider.CompareTag("GearRed"))
            {
                Debug.Log("Red gear place hit");
                if(InventoryManager.Instance.HasItem(GearRedItemName) && redGearPlaced == false)
                {
                    Debug.Log("Red gear placed");
                    ActivateObject(RedGear);
                    redGearPlaced = true;
                }
            }
        }

        if (puzzleStarted == true && Input.GetKey(KeyCode.D) && greenGearPlaced == true && redGearPlaced == true)
        {

            if (!topCompleted)
            {
                top.localRotation = Quaternion.RotateTowards(top.localRotation, Quaternion.Euler(-85f, 0f, 0f), topRotationSpeed * Time.deltaTime);
                if (Mathf.Approximately(top.localRotation.eulerAngles.x, 275f)) topCompleted = true;
            }
            if (!nutcrackerCompleted)
            {
                nutcracker.localPosition = Vector3.MoveTowards(nutcracker.localPosition, new Vector3(nutcracker.localPosition.x, 0.005f, nutcracker.localPosition.z), nutcrackerMoveSpeed * Time.deltaTime);
                if (Mathf.Approximately(nutcracker.localPosition.y, 0.005f)) nutcrackerCompleted = true;
            }
            if (!leverCompleted)
            {
                leverRotation += leverRotationSpeed * 0.02f;
                lever.localRotation = Quaternion.Euler(leverRotation, 0f, 0f);

                if (leverRotation <= -1080f) leverCompleted = true;
            }
        }
    }

    public void SetPuzzleActive(bool isActive)
    {
        Cursor.visible = isActive;  // Show cursor when puzzle is active
        Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;  // Unlock cursor when active
        topCompleted = false;
        nutcrackerCompleted = false;
        leverCompleted = false;
        Debug.Log($"isActive = {isActive}");
        if (isActive == true)
        {
            ChangePosition();
            puzzleStarted = true;
        }
        else
        {
            ResetPositionAndRotation();
            puzzleStarted = false;
        }
    }

    public void ChangePosition()
    {
            objectToMove.transform.position = targetTransform.position;
    }

    public void ResetPositionAndRotation()
    {
        if (objectToMove != null)
        {
            // Reset position and rotation to the original values
            objectToMove.transform.position = originalPosition.position;
            objectToMove.transform.rotation = originalRotation;
        }
        else
        {
            Debug.LogWarning("No object assigned to reset!");
        }
    }

    private void RotateObjectWithMouse()
    {
        // Get the mouse movement
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate the object based on mouse movement
        objectToMove.transform.Rotate(Vector3.up, mouseX * -rotationSpeed, Space.World);
        objectToMove.transform.Rotate(Vector3.left, mouseY * -rotationSpeed, Space.World);
    }

    private void RotateY()
    {
        if (rotationCompleted == false)
        {
            Debug.Log("Starting Rotation");
            Quaternion targetRotation = Quaternion.Euler(0f, rotationDistance, 0f);

            objectToRotate.localRotation = Quaternion.RotateTowards(objectToRotate.localRotation, targetRotation, rotationSpeed2 * Time.deltaTime);
        }
    }

    public void ActivateObject(GameObject objectToActivate)
    {
        objectToActivate.SetActive(true);
        Debug.Log($"{objectToActivate.name} has been activated.");
    }
}
