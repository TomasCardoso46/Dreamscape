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

    void Update()
    {
        if (isActive && Input.GetKey(KeyCode.D))
        {

            if (!topCompleted)
            {
                top.localRotation = Quaternion.RotateTowards(top.localRotation, Quaternion.Euler(-85f, 0f, 0f), topRotationSpeed * Time.deltaTime);
                if (Mathf.Approximately(top.localRotation.eulerAngles.x, 275f)) topCompleted = true;
            }
            if (!nutcrackerCompleted)
            {
                nutcracker.localPosition = Vector3.MoveTowards(nutcracker.localPosition, new Vector3(nutcracker.localPosition.x, 0.0275f, nutcracker.localPosition.z), nutcrackerMoveSpeed * Time.deltaTime);
                if (Mathf.Approximately(nutcracker.localPosition.y, 0.0275f)) nutcrackerCompleted = true;
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
        this.isActive = isActive;
        if (!isActive)
        {
            topCompleted = false;
            nutcrackerCompleted = false;
            leverCompleted = false;
        }
    }
}
