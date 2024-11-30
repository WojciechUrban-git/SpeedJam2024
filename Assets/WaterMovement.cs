using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    [SerializeField] private float initialRiseSpeed = 0.2f; // Slower speed for the initial rise
    [SerializeField] private float floatSpeed = 0.05f;       // Slower speed for the floating motion
    [SerializeField] private float floatAmplitude = 0.05f;  // Smaller fluctuations
    [SerializeField] private float floatFrequency = 0.3f;   // Slower floating wave
    [SerializeField] private float finalTargetHeight = 0.7f; // Stop a bit lower than 0.8

    private bool initialRiseComplete = false;
    private Vector3 startPosition;
    private Vector3 targetPosition;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = new Vector3(startPosition.x, finalTargetHeight, startPosition.z); // Target height for the initial rise
    }

    void Update()
    {
        if (!initialRiseComplete)
        {
            // Smooth rise to the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, initialRiseSpeed * Time.deltaTime);

            // Check if the water has reached the target position
            if (transform.position.y >= targetPosition.y)
            {
                initialRiseComplete = true;
                startPosition = transform.position; // Reset for floating motion
            }
        }
        else
        {
            // Smooth floating motion with smaller fluctuations
            float newY = finalTargetHeight + floatAmplitude * Mathf.Sin(Time.time * floatFrequency * Mathf.PI * 2);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}
