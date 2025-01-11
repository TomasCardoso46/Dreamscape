using UnityEngine;

public class ConsolePuzzle : MonoBehaviour, IPuzzle
{
    [SerializeField] private UIManager  uiManager;
    [SerializeField] private Transform consoleTop;
    private bool                        isPuzzleActive = false;

    private void Start()
    {
        consoleTop.transform.rotation = Quaternion.Euler(-90f, 
            consoleTop.transform.eulerAngles.y, 
            consoleTop.transform.eulerAngles.z);
    }
    public void SetPuzzleActive(bool active)
    {
        isPuzzleActive = active;
        if (active == true)
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
        consoleTop.transform.rotation = Quaternion.Euler(0f, 
            consoleTop.transform.eulerAngles.y, 
            consoleTop.transform.eulerAngles.z);
    }
}
