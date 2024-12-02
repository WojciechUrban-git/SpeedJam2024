using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Menu Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;

    private bool CreditsOpen = false;
    private bool SettingsOpen = false;


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && CreditsOpen)
        {
            BackFromCredits();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && SettingsOpen)
        {
            BackFromSettings();
        }
    }
    
    // Show Settings Panel
    public void ShowSettings()
    {
        SettingsOpen = true;
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    // Show Credits Panel
    public void ShowCredits()
    {
        CreditsOpen = true;
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void BackFromCredits()
    {
        CreditsOpen = false;
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    public void BackFromSettings()
    {
        SettingsOpen = false;
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    // Start the game by loading the specified scene
    public void StartGame()
    {
        SceneManager.LoadScene("TestLevel"); // Replace "TestLevel" with your actual scene name
    }

    // Quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
