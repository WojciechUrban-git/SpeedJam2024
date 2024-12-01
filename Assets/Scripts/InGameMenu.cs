using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class InGameMenu : MonoBehaviour
{
    [Header("Menu Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject optionsPanel;

    [Header("Options Settings")]
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMP_Text sensitivityValueText;
    [SerializeField] private TMP_Text volumeValueText;
    private bool optionsActive = false;

    [Header("Default Settings")]
    [SerializeField] private float defaultMouseSensitivity = 3.5f;
    [SerializeField] private float defaultVolume = 1f;

    private float currentMouseSensitivity;
    private float currentVolume;

    private void Update()
    {
        // Check for Escape key press to toggle the menu
        if (Input.GetKeyDown(KeyCode.Escape) && !optionsActive)
        {
            ToggleMenu();
        }
        if (optionsActive && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseOptions();
        }
    }

    private void UpdateSensitivityText()
    {
        if (sensitivityValueText != null)
            sensitivityValueText.text = currentMouseSensitivity.ToString("F1");
    }

    private void UpdateVolumeText()
    {
        if (volumeValueText != null)
            volumeValueText.text = (currentVolume * 100).ToString("F0") + "%";
    }

    public void ToggleMenu()
    {
        bool isActive = mainMenuPanel.activeSelf;
        mainMenuPanel.SetActive(!isActive);

        // Pause the game when the menu is active
        Time.timeScale = isActive ? 1 : 0;

        // Handle cursor visibility and lock state
        if (isActive)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }


    public void OpenOptions()
    {
        optionsActive = true;
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsActive = false;
        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1; // Resume the game
        SceneManager.LoadScene("MainMenu"); // Replace "MainMenu" with the name of your main menu scene
    }

    public void ResumeGame()
    {
        ToggleMenu();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    public void QuitToDesktop()
    {
        Application.Quit();
    }

    private void OnSensitivityChanged(float value)
    {
        currentMouseSensitivity = value;
        UpdateSensitivityText();
        // Apply sensitivity to your player movement script
        var player = FindAnyObjectByType<Movement>();
        if (player != null)
        {
            player.SetMouseSensitivity(currentMouseSensitivity);
        }
    }

    private void OnVolumeChanged(float value)
    {
        currentVolume = value;
        UpdateVolumeText();
        // Apply volume to the audio mixer or AudioListener
        AudioListener.volume = currentVolume;
    }
}