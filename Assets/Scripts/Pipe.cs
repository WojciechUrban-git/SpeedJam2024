using UnityEngine;

public class Pipe : MonoBehaviour
{
    protected float correctZRotation; // The correct rotation for this pipe
    protected float currentZRotation; // Tracks the pipe's current Z-axis rotation
    private const float rotationStep = 90f; // Rotation step size

    protected virtual void Start()
    {
        // Save the initial correct rotation for the pipe
        correctZRotation = transform.eulerAngles.z;

        // Randomize the Z rotation
        float randomRotation = Random.Range(0, 4) * rotationStep;
        currentZRotation = (correctZRotation + randomRotation) % 360;
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, currentZRotation);
    }

    public void RotatePipe()
    {
        // Rotate the pipe 90 degrees clockwise
        currentZRotation = (currentZRotation + rotationStep) % 360;
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, currentZRotation);

        Debug.Log($"Pipe rotated. Current rotation: {currentZRotation}");
    }

    public virtual bool IsCorrectlyAligned()
    {
        // Check if the pipe's current Z-axis rotation matches its correct Z-axis rotation
        return Mathf.Approximately(currentZRotation, correctZRotation);
    }
}
