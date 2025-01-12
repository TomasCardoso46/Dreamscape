using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;
    private bool canPlaySFX;
    private int bgmIndex = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else 
            Destroy(gameObject);
        
        // Starts the background sound after 4 secs
        Invoke("AllowSFX", 4f);
    }

    private void Update()
    {
        if (canPlaySFX == false)
            return;

        if (!bgm[bgmIndex].isPlaying)
            PlayBGM(bgmIndex); 

        if (Input.GetKeyDown(KeyCode.O))
        {
            PlaySFX(1);
        }
    }

    public void PlaySFX(int _sfxIndex)
    {
        if (canPlaySFX == false)
            return;

        if (_sfxIndex < sfx.Length)
        {

            sfx[_sfxIndex].pitch = Random.Range(0.85f, 1.1f); // to sound a little bit different
            sfx[_sfxIndex].Play();
        }
    }

    public void StopSFX(int index) => sfx[index].Stop();
    public void StopBGM(int index) => sfx[index].Stop();

    /*public void StopSFXWithTime(int _index) => StartCoroutine(DecreaseVolume(sfx[_index]));

    private IEnumerator DecreaseVolume(AudioSource _audio)
    {
        float defaultVolume = _audio.volume;

        while(_audio.volume > 0.1f)
        {
            _audio.volume -= _audio.volume * 0.2f;
            yield return new WaitForSeconds(0.6f);

            if (_audio.volume <= 0.1f)
            {
                _audio.Stop();
                _audio.volume = defaultVolume;
                break;
            }
        }
    }*/

    public void PlayDayBGM()
    {
        if (bgmIndex == 1)
        {
            bgm[bgmIndex].Stop();
        }
        else
        {
            bgmIndex = 0;
            bgm[bgmIndex].Play();
        }
    }

    public void PlayNightBGM()
    {
        if (bgmIndex == 0)
        {
            bgm[bgmIndex].Stop();
        }
        else
        {
            bgmIndex = 1;
            bgm[bgmIndex].Play();
        }
    }

    public void PlayBGM(int _bgmIndex)
    {
        bgmIndex = _bgmIndex;

        StopAllBGM();
        bgm[bgmIndex].Play();
    }

    private void StopAllBGM()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }

    private void AllowSFX()
    {
        canPlaySFX = true;
    }
}
