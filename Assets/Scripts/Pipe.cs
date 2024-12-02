using System.Collections;
using System.Collections.Generic;
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
        correctZRotation = Mathf.Round(transform.eulerAngles.z);

        // Randomize the Z rotation
        float randomRotation = Random.Range(0, 4) * rotationStep;
        currentZRotation = NormalizeAngle(correctZRotation + randomRotation);
            
        // Apply the random rotation
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, currentZRotation);
    }

    public void RotatePipe()
    {
        if (isRotating)
        {
            return; // Ignore input if already rotating
        }

        // Determine rotation angle based on X rotation
        float xRotation = NormalizeAngle(transform.eulerAngles.x);

        float rotationAngle = rotationStep;

        if (!(Mathf.Approximately(xRotation, 90) || Mathf.Approximately(xRotation, -90)))
        {
            // Decrement rotation angle if X rotation is not ±90 degrees
            rotationAngle = -rotationStep;
        }

        // Start the rotation coroutine with the rotation angle
        StartCoroutine(SmoothRotate(rotationAngle));
    }


    private IEnumerator SmoothRotate(float rotationAngle)
    {
        isRotating = true;

        float duration = 0.3f; // Duration of the rotation
        float elapsedTime = 0f;

        // Get the initial rotation
        Quaternion initialRotation = transform.rotation;

        // Create the rotation increment as a quaternion
        Quaternion rotationIncrement = Quaternion.Euler(0f, 0f, rotationAngle);

        // Compute the final rotation by applying the increment
        Quaternion finalRotation = initialRotation * rotationIncrement;

        while (elapsedTime < duration)
        {
            // Interpolate between the initial and final rotation
            transform.rotation = Quaternion.Slerp(initialRotation, finalRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        // Ensure the final rotation is precise
        transform.rotation = finalRotation;
        currentZRotation = NormalizeAngle(currentZRotation + rotationAngle);

        isRotating = false; // Allow interaction again
    }


    public virtual bool IsCorrectlyAligned()
    {
        float normalizedCurrentZRotation = NormalizeAngle(currentZRotation);
        float normalizedCorrectZRotation = NormalizeAngle(correctZRotation);

        return Mathf.Approximately(normalizedCurrentZRotation, normalizedCorrectZRotation);
    }

    private float NormalizeAngle(float angle)
    {
        // Normalize the angle to the range [0°, 360°)
        angle = angle % 360f;
        if (angle < 0f)
        angle += 360f;
        return angle;
    }

}
