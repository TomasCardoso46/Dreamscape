public interface IPuzzle
{
    /// <summary>
    /// Activates or deactivates the puzzle.
    /// </summary>
    /// <param name="isActive">If true, activates the puzzle; if false, deactivates it.</param>
    void SetPuzzleActive(bool isActive);
}
