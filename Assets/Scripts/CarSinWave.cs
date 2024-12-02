using UnityEngine;

public class CarSinWave : MonoBehaviour
{
    // Amplitude of the sine wave (half the distance between -0.01 and 0.03)
    private float amplitude = 0.01f;

    // Midpoint of the movement (average of -0.01 and 0.03)
    private float midpoint = 0.01f;

    // Speed of the sine wave movement
    public float frequency = 1.0f;

    // Initial position of the object
    private Vector3 initialPosition;

    void Start()
    {
        // Store the initial position of the object
        initialPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new Y position based on a sine wave
        float newY = midpoint + amplitude * Mathf.Sin(Time.time * frequency * Mathf.PI * 2);

        // Update the object's position
        transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);
    }
}
