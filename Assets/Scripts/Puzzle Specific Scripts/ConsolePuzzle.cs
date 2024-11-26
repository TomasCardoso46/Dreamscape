using UnityEngine;

public class ConsolePuzzle : MonoBehaviour, IPuzzle
{
    [SerializeField] private UIManager  uiManager;
    private bool                        isPuzzleActive = false;
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
}
