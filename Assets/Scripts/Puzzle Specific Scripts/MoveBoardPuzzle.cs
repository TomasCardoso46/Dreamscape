using UnityEngine;

public class MoveBoardPuzzle : MonoBehaviour
{
    [SerializeField]private BoardPuzzle boardPuzzle;

    private void Awake()
    {
        boardPuzzle.GetComponent<BoardPuzzle>();
    }
    
    public void OnMouseDown()
    {
        //telling the boardPuzzle script that we clicked this board.
        boardPuzzle.OnPieceClicked(this.gameObject);
    }
}
