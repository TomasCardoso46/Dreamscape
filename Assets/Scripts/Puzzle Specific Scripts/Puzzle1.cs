using System.Collections;
using UnityEngine;

public class Puzzle1 : MonoBehaviour, IPuzzle
{
    public GameObject[] cubes;
    public Animation animationClip2;
    
    private int currentCubeIndex = 0;
    private bool isPuzzleActive = false;
    private string[] letters = { "A", "R", "M", "D" };
    private string[] specialLetters = { "A", "E", "M", "D" };
    private int[] currentCubeRotations;
    private float rotationSpeed = 180f; // Degrees per second
    private bool isRotating = false; // To check if currently rotating
    private Quaternion targetRotation; // Target rotation quaternion
    [SerializeField]
    private InteractionTrigger interactionTrigger;

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
        Debug.Log(isPuzzleActive ? "Puzzle1 activated" : "Puzzle1 deactivated");
    }

    private void HandleRotation()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isRotating)
        {
            targetRotation = cubes[currentCubeIndex].transform.rotation * Quaternion.Euler(Vector3.right * 90);
            StartCoroutine(RotateCube(targetRotation));
            UpdateCubeRotation(-1);
        }
        else if (Input.GetKeyDown(KeyCode.S) && !isRotating)
        {
            targetRotation = cubes[currentCubeIndex].transform.rotation * Quaternion.Euler(Vector3.right * -90);
            StartCoroutine(RotateCube(targetRotation));
            UpdateCubeRotation(1);
        }
    }

    private IEnumerator RotateCube(Quaternion targetRotation)
    {
        isRotating = true; // Set the rotating flag
        Quaternion startRotation = cubes[currentCubeIndex].transform.rotation;
        float journeyLength = Quaternion.Angle(startRotation, targetRotation);
        float journey = 0f;

        while (journey < journeyLength)
        {
            journey += rotationSpeed * Time.deltaTime; // Increment journey based on speed
            float fractionOfJourney = journey / journeyLength; // Calculate fraction of journey
            cubes[currentCubeIndex].transform.rotation = Quaternion.Slerp(startRotation, targetRotation, fractionOfJourney); // Smoothly rotate
            yield return null; // Wait for the next frame
        }

        cubes[currentCubeIndex].transform.rotation = targetRotation; // Ensure it ends at the target rotation
        isRotating = false; // Reset the rotating flag
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
            interactionTrigger.ToggleInteraction();
            animationClip2.Play("Chest_Open2");
            AudioManager.Instance.PlaySFX(0);
            AudioManager.Instance.PlaySFX(1);
        }
        else
        {
            Debug.Log($"Current sequence: {sequence}");
            AudioManager.Instance.PlaySFX(2);
        }
    }
}
