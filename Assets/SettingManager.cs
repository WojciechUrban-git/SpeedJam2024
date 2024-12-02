using UnityEngine;

public static class SettingsManager
{
    // Static variables to store volume, sensitivity, and best time
    public static float volume = 1.0f;
    public static float sensitivity = 1.0f;
    public static float bestTime = 0f;

    static SettingsManager()
    {
        // Static constructor to load saved settings if they exist
        if (PlayerPrefs.HasKey("Volume"))
        {
            volume = PlayerPrefs.GetFloat("Volume");
            AudioListener.volume = volume;
        }
        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            sensitivity = PlayerPrefs.GetFloat("Sensitivity");
        }
        if (PlayerPrefs.HasKey("BestTime"))
        {
            bestTime = PlayerPrefs.GetFloat("BestTime");
        }
    }

    // Static methods to control the variables
    public static void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume);
        AudioListener.volume = volume; // Adjust the global audio volume

        // Save the volume setting
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }

    public static void SetSensitivity(float newSensitivity)
    {
        sensitivity = newSensitivity;

        // Save the sensitivity setting
        PlayerPrefs.SetFloat("Sensitivity", sensitivity);
        PlayerPrefs.Save();
    }

    public static void UpdateBestTime(float time)
    {
        if (bestTime == 0f || time < bestTime)
        {
            bestTime = time;

            // Save the best time
            PlayerPrefs.SetFloat("BestTime", bestTime);
            PlayerPrefs.Save();
        }
    }
}
