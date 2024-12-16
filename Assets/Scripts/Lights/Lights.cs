using System.Collections;
using UnityEngine;
using TMPro;

public class Lights : MonoBehaviour
{
    [SerializeField] private GameObject topWindows;
    [SerializeField] private Transform lightSwitch;
    [SerializeField] private Light directionalLight;
    private Material defaultSkybox;

    [SerializeField] private TextMeshProUGUI interactionText;
    private bool isTextSeen = true;
    public string targetTag = "Player";
    private bool isPlayerInside = false;
    private bool isDark = false;


    private void Awake()
    {
        defaultSkybox = RenderSettings.skybox;
    }
    private void Start()
    {

        // Initially hide the interaction text
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }

        lightSwitch.transform.rotation = Quaternion.Euler(-35f, 
            lightSwitch.transform.eulerAngles.y, lightSwitch.transform.eulerAngles.z);

        topWindows.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            isTextSeen = false;
            if (isDark)
            {
                StartCoroutine(WaitForSkybox());
                
            }
            else
            {
                ToggleInteraction();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            isPlayerInside = true;

            // Show the interaction text when the player enters the trigger
            if (interactionText != null && isTextSeen)
            {
                interactionText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            isPlayerInside = false;
            isTextSeen = true;

            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(false);
            }
        }
    }

    private void ToggleInteraction()
    {
        lightSwitch.transform.rotation = Quaternion.Euler(35f, 
            lightSwitch.transform.eulerAngles.y, lightSwitch.transform.eulerAngles.z);
        directionalLight.intensity = (directionalLight.intensity == 0) ? 1 : 0;
        SetAmbientIntensityAndSkybox(0f, null);
        topWindows.SetActive(true);

        isDark = true;

        interactionText.gameObject.SetActive(false);
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
        yield return new WaitForSeconds(0.15f);
        lightSwitch.transform.rotation = Quaternion.Euler(-35f, 
            lightSwitch.transform.eulerAngles.y, lightSwitch.transform.eulerAngles.z);
        directionalLight.intensity = (directionalLight.intensity == 0) ? 1 : 0;
        RenderSettings.skybox = defaultSkybox;
        RestoreDefaults();
        topWindows.SetActive(false);
        isDark = false;

        interactionText.gameObject.SetActive(false);
    }
}
