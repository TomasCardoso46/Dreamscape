using System.Collections.Generic;
using UnityEngine;

public class BoardPuzzle : MonoBehaviour , IPuzzle
{
    [SerializeField] private GameObject         controllerPadImage;
    // Adjusts movement speed
    [SerializeField] private float              moveSpeed = 50f;
    private GameObject                          selectedPiece = null;
    private GameObject                          piece1, piece2;
    private Vector3                             piece1StartPos, piece2StartPos;
    private float                               swapStartTime;
    // Time of switch positions
    private float                               swapDuration = 0.5f;
    [SerializeField] private List<GameObject>   puzzleSequence; 
    [SerializeField] private List<GameObject>   myInitialSequence; 
    private List<GameObject>                    currentSequence;
    private bool                                isPuzzleOver = false;
    private bool                                isSwapping = false;
    private bool                                isPuzzleActive = false;

    private void Start()
    {
        currentSequence = new List<GameObject>(myInitialSequence);
        controllerPadImage.SetActive(false);
    }

    private void Update()
    {
        if (isSwapping)
        {
            // Calculates the time spent and the interpolation(t)
            float elapsedTime = Time.time - swapStartTime;
            float t = elapsedTime / swapDuration;

            if (t < 1f)
            {
                // Moves smoothly to piece 1 to piece 2
                piece1.transform.position = Vector3.Lerp(piece1StartPos, piece2StartPos, t);
                // Moves smoothly to piece 2 to piece 1
                piece2.transform.position = Vector3.Lerp(piece2StartPos, piece1StartPos, t);
            }
            else
            {
                // When the timesUp, the pieces are on exactly on the place
                piece1.transform.position = piece2StartPos;
                piece2.transform.position = piece1StartPos;
                isSwapping = false;  // End of swap
            }
        }
    }

    public void OnPieceClicked(GameObject piece)
    {
        if (isPuzzleOver)
            return;
        if (!isPuzzleActive)
            return;
        if (selectedPiece == null)
        {
            // First piece selected
            selectedPiece = piece;
            
            HighlightPiece(piece);
        }
        else
        {
            if (selectedPiece == piece)
                return;
            
            UnHighlightPiece(selectedPiece);
            // Second piece selected, start swapping
            StartSwap(selectedPiece, piece);
            selectedPiece = null; // Reset selection
        }
    }

    private void HighlightPiece(GameObject piece)
    {
        // Piece goes up a bit
        piece.transform.position = Vector3.Lerp(piece.transform.position,
        piece.transform.position + new Vector3(-0.3f, 0, 0), moveSpeed * Time.deltaTime);
    }

    private void UnHighlightPiece(GameObject piece)
    {
        // Returns to the original position
        piece.transform.position = Vector3.Lerp(piece.transform.position, 
        piece.transform.position + new Vector3(+0.3f, 0, 0), moveSpeed * Time.deltaTime);
    }

    private void StartSwap(GameObject piece1, GameObject piece2)
    {
        if (isPuzzleOver)
            return;
        this.piece1 = piece1;
        this.piece2 = piece2;
        piece1StartPos = piece1.transform.position;
        piece2StartPos = piece2.transform.position;
        isSwapping = true;
        swapStartTime = Time.time;  

        // Swap the positions in the current sequence
        SwapInCurrentSequence(piece1, piece2);

        // Check if the sequence is correct after the swap
        CheckSequence();
    }

    private void SwapInCurrentSequence(GameObject piece1, GameObject piece2)
    {
        int index1 = currentSequence.IndexOf(piece1);
        int index2 = currentSequence.IndexOf(piece2);

        if (index1 != -1 && index2 != -1)
        {
            // Swap their positions in the current sequence
            GameObject temp = currentSequence[index1];
            currentSequence[index1] = currentSequence[index2];
            currentSequence[index2] = temp;
        }
    }

    private void CheckSequence()
    {
        bool isCorrect = true;

        // Check if the current sequence matches the correct sequence
        for (int i = 0; i < puzzleSequence.Count; i++)
        {
            if (currentSequence[i] != puzzleSequence[i])
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect)
        {
            Debug.Log("Sequence is correct!");
            isPuzzleOver = true;
            controllerPadImage.SetActive(true);
        }
        else
        {
            Debug.Log("Sequence is incorrect.");
        }
    }

    public void SetPuzzleActive(bool active)
    {
        isPuzzleActive = active;
        if (active == true)
            ShowCursor();
        else
            HideCursor();
    }
    private void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }
}
