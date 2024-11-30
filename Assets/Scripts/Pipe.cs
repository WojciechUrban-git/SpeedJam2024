using UnityEngine;

public class Pipe : MonoBehaviour
{
    protected float correctZRotation; // The correct rotation for this pipe
    protected float currentZRotation; // Tracks the pipe's current Z-axis rotation
    private const float rotationStep = 90f; // Rotation step size

    private bool isRotating = false; // Prevent interaction while rotating

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
        if (isRotating) return; // Ignore input if already rotating

        // Calculate the new target rotation
        float targetZRotation = (currentZRotation + rotationStep) % 360;

        // Start the rotation coroutine
        StartCoroutine(SmoothRotate(targetZRotation));
    }

    private System.Collections.IEnumerator SmoothRotate(float targetZRotation)
    {
        isRotating = true;

        float duration = 0.3f; // Time in seconds for the rotation
        float elapsedTime = 0f;

        Quaternion initialRotation = transform.rotation;
        Quaternion finalRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, targetZRotation);

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, finalRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        // Ensure the final rotation is precise
        transform.rotation = finalRotation;
        currentZRotation = targetZRotation;

        isRotating = false; // Allow interaction again
    }

    public virtual bool IsCorrectlyAligned()
    {
        // Check if the pipe's current Z-axis rotation matches its correct Z-axis rotation
        return Mathf.Approximately(currentZRotation, correctZRotation);
    }
}
