using UnityEngine;

public class ToiletFlush : MonoBehaviour
{
    [SerializeField] private GameObject waterObject; // Assign the water object in the Inspector
    [SerializeField] private float flushDuration = 2f; // Time it takes for the water to scale down
    private Vector3 initialScale; // The initial scale of the water
    private bool isFlushing = false; // Whether the flushing is in progress
    private bool hasFlushed = false; // Ensures the toilet can only be flushed once
    private float flushTimer = 0f;

    void Start()
    {
        if (waterObject != null)
        {
            // Store the initial scale of the water
            initialScale = waterObject.transform.localScale;
        }
        else
        {
            Debug.LogError("Water object is not assigned in the Inspector.");
        }
    }

    public void Flush()
    {
        // Check if the toilet has already been flushed
        if (!hasFlushed && !isFlushing && waterObject != null)
        {
            isFlushing = true;
            hasFlushed = true; // Mark the toilet as flushed
            flushTimer = 0f;
        }
    }

    void Update()
    {
        if (isFlushing && waterObject != null)
        {
            // Increment the timer
            flushTimer += Time.deltaTime;

            // Calculate the progress of the flushing
            float progress = flushTimer / flushDuration;

            // Gradually scale down the water
            waterObject.transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, progress);

            // Stop flushing when the scale is zero
            if (progress >= 1f)
            {
                isFlushing = false;
                waterObject.transform.localScale = Vector3.zero; // Ensure the scale is exactly zero
            }
        }
    }
}
