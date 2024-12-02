using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Menu Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;

    [Header("Loading Screen")]
    [SerializeField] private GameObject loadingScreenPanel; // The loading screen canvas
    [SerializeField] private Slider progressBar;            // The progress bar slider


    private bool creditsOpen = false;
    private bool settingsOpen = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && creditsOpen)
        {
            BackFromCredits();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && settingsOpen)
        {
            BackFromSettings();
        }
    }

    // Show Settings Panel
    public void ShowSettings()
    {
        settingsOpen = true;
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    // Show Credits Panel
    public void ShowCredits()
    {
        creditsOpen = true;
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void BackFromCredits()
    {
        creditsOpen = false;
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void BackFromSettings()
    {
        settingsOpen = false;
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    // Start the game and show the loading screen
    public void StartGame()
    {
        mainMenuPanel.SetActive(false);          // Disable the main menu
        loadingScreenPanel.SetActive(true);     // Enable the loading screen
        StartCoroutine(LoadSceneAsync("TestLevel")); // Start loading the scene
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false; // Prevent immediate activation

        float delayTime = 2f; // Set a longer delay time for a more visible loading screen
        float timer = 0f;
        float progress = 0f;

        // Simulate loading progress for the delay time
        while (!operation.isDone)
        {
            // Update the progress bar with a gradual fill
            progress += Time.deltaTime / delayTime;
            progressBar.value = Mathf.Clamp01(progress);

            timer += Time.deltaTime;

            // Check if the delay time has passed and the scene is nearly loaded
            if (timer >= delayTime && operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true; // Activate the scene
            }

            yield return null; // Wait for the next frame
        }
    }

    // Quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
