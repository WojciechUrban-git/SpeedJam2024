using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OnAwakeOpen : MonoBehaviour
{
    public Image fadeImage; // The image that will fade out
    public float fadeSpeed = 1.0f; // Speed of the fade-out

    private void Start()
    {
        // Start with the image fully black
        Color color = fadeImage.color;
        color.a = 1; // Full opacity
        fadeImage.color = color;

        // Start the fade-out coroutine
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float startTime = Time.time;
        float duration = 1 / fadeSpeed; // Duration of the fade-out

        while (Time.time - startTime < duration)
        {
            float alpha = Mathf.Lerp(1, 0, (Time.time - startTime) / duration);
            Color color = fadeImage.color;
            color.a = alpha;
            fadeImage.color = color;
            yield return null;
        }

        // Ensure the image is completely transparent at the end
        Color finalColor = fadeImage.color;
        finalColor.a = 0;
        fadeImage.color = finalColor;

        // Optionally, you can disable the image once it's fully transparent to save resources
        fadeImage.gameObject.SetActive(false);
    }
}
