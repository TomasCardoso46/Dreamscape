using UnityEngine;

public class MusicBox : MonoBehaviour
{
    private bool isMusicBoxActive = false;

    public bool IsMusicBoxActive {get => isMusicBoxActive;}

    public void SetMusicBoxToTrue()
    {
        isMusicBoxActive = true;
    }

    public void SetMusicBoxToFalse()
    {
        isMusicBoxActive = false;
    }
}
