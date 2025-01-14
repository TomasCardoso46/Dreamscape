using UnityEngine;

public class ConsolePuzzle : MonoBehaviour, IPuzzle
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Transform consoleTop;
    private bool isPuzzleActive = false;
    [SerializeField] private BoxCollider keyCollider;

    // Smooth rotation variables
    private Quaternion targetRotation;
    private float rotationSpeed = 0.75f;
    private bool isRotating = false;

    private void Start()
    {
        consoleTop.transform.rotation = Quaternion.Euler(-90f,
            consoleTop.transform.eulerAngles.y,
            consoleTop.transform.eulerAngles.z);

        keyCollider.enabled = false;
    }

    private void Update()
    {
        // Smoothly rotates the rotation of the top if true
        if (isRotating)
        {
            consoleTop.transform.rotation = Quaternion.Lerp(
                consoleTop.transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(consoleTop.transform.rotation, targetRotation) < 0.01f)
            {
                consoleTop.transform.rotation = targetRotation;
                isRotating = false;
            }
        }
    }

    public void SetPuzzleActive(bool active)
    {
        isPuzzleActive = active;
        if (active)
        {
            uiManager.ShowCursor();
            uiManager.ShowPSPuzzleUI();
        }
        else
        {
            uiManager.HideCursor();
            uiManager.HidePSPuzzleUI();
        }
    }

    public void OpenConsoleTop()
    {
        targetRotation = Quaternion.Euler(0f,
            consoleTop.transform.eulerAngles.y,
            consoleTop.transform.eulerAngles.z);

        isRotating = true;
        keyCollider.enabled = true;

        AudioManager.Instance.PlaySFX(1);
        AudioManager.Instance.StopSFXWithTime(1);
    }
}
