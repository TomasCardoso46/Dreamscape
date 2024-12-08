using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private UI_FadeScreen fadeScreen;

    [SerializeField] private float loadSceneTime;

    public void StartGame()
    {
        StartCoroutine(LoadSceneWithFadeEffect(loadSceneTime));
    }

    public void ExitGame()
    {
        Application.Quit();
        
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif
    }

    private IEnumerator LoadSceneWithFadeEffect(float _delay)
    {
        fadeScreen.FadeOut();

        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene(sceneName);
    }
}
