using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider sensitivitySlider;

    void Start()
    {
        // Initialize sliders with current values from SettingsManager
        volumeSlider.value = SettingsManager.volume;
        sensitivitySlider.value = SettingsManager.sensitivity;
    }

    public void OnVolumeChanged(float newVolume)
    {
        Debug.Log("OnVolumeChanged called with newVolume: " + newVolume);

        // Update the volume in SettingsManager
        SettingsManager.SetVolume(newVolume);
    }

    public void OnSensitivityChanged(float newSensitivity)
    {
        Debug.Log("OnSensitivityChanged called with newSensitivity: " + newSensitivity);

        // Update the sensitivity in SettingsManager
        SettingsManager.SetSensitivity(newSensitivity);
    }
}
