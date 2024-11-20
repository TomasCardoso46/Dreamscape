using UnityEngine;

public class Puzzle1 : MonoBehaviour
{
    public GameObject[] cubes;
    public Animation animationClip;
    
    private int currentCubeIndex = 0;
    private bool isPuzzleActive = false;
    private string[] letters = { "A", "R", "M", "D" };
    private string[] specialLetters = { "A", "E", "M", "D" };
    private int[] currentCubeRotations;

    void Start()
    {
        if (cubes.Length != 5)
        {
            Debug.LogError("Puzzle1 requires exactly 5 cubes");
        }
        else
        {
            Debug.Log("Puzzle1 initialized with 5 cubes");
            currentCubeRotations = new int[cubes.Length];
            for (int i = 0; i < cubes.Length; i++)
            {
                currentCubeRotations[i] = 0;
            }
        }
    }

    void Update()
    {
        if (isPuzzleActive && cubes.Length == 5)
        {
            HandleRotation();
            HandleCubeSwitch();
            
            if (Input.GetKeyDown(KeyCode.Return))
            {
                CheckForKeySequence();
            }
        }
    }

    public void SetPuzzleActive(bool active)
    {
        isPuzzleActive = active;
        Debug.Log(isPuzzleActive ? "Puzzle activated" : "Puzzle deactivated");
    }

    private void HandleRotation()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            RotateCube(Vector3.right, 90);
            UpdateCubeRotation(1);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            RotateCube(Vector3.right, -90);
            UpdateCubeRotation(-1);
        }
    }

    private void RotateCube(Vector3 axis, float angle)
    {
        cubes[currentCubeIndex].transform.Rotate(axis, angle, Space.World);
    }

    private void UpdateCubeRotation(int direction)
    {
        currentCubeRotations[currentCubeIndex] = (currentCubeRotations[currentCubeIndex] + direction + 4) % 4;
        Debug.Log($"Cube {currentCubeIndex} displayed letter: {GetCurrentLetter(currentCubeIndex)}");
    }

    private string GetCurrentLetter(int cubeIndex)
    {
        return cubeIndex == 2 ? specialLetters[currentCubeRotations[cubeIndex]] : letters[currentCubeRotations[cubeIndex]];
    }

    private void HandleCubeSwitch()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentCubeIndex = (currentCubeIndex + 1) % cubes.Length;
            Debug.Log($"Switched to cube at index {currentCubeIndex} by pressing D");
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            currentCubeIndex = (currentCubeIndex - 1 + cubes.Length) % cubes.Length;
            Debug.Log($"Switched to cube at index {currentCubeIndex} by pressing A");
        }
    }

    private void CheckForKeySequence()
    {
        string sequence = "";
        for (int i = 0; i < cubes.Length; i++)
        {
            sequence += GetCurrentLetter(i);
        }

        if (sequence == "DREAM")
        {
            Debug.Log("Key found");
            animationClip.Play("Chest_Open");
        }
        else
        {
            Debug.Log($"Current sequence: {sequence}");
        }
    }
}
