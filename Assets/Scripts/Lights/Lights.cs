using System.Collections;
using UnityEngine;

public class Lights : MonoBehaviour
{
    [SerializeField] private GameObject topWindows;
    private Material defaultSkybox;

    private void Awake()
    {
        defaultSkybox = RenderSettings.skybox;
    }
    private void Start()
    {
        SetAmbientIntensityAndSkybox(0f, null);
        topWindows.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SetAmbientIntensityAndSkybox(0f, null);
            topWindows.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(WaitForSkybox());
        }
    }

    public void SetAmbientIntensityAndSkybox(float intensity, Material skyboxMaterial)
    {
        // Set the ambient intensity
        RenderSettings.ambientIntensity = intensity;

        // Set the skybox
        RenderSettings.skybox = skyboxMaterial;
    }

    // Example to restore the lighting and skybox
    public void RestoreDefaults()
    {
        SetAmbientIntensityAndSkybox(1f, defaultSkybox);
    }

    private IEnumerator WaitForSkybox()
    {
        yield return new WaitForSeconds(0.2f);
        RenderSettings.skybox = defaultSkybox;
        RestoreDefaults();
        topWindows.SetActive(false);
    }
}
