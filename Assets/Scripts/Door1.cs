using UnityEngine;

public class Door1 : MonoBehaviour
{
    private bool isOpen = false; // Tracks the state of the door
    private bool isAnimating = false; // Prevents interruption during animation
    private float animationSpeed = 2f; // Controls the speed of the animation

    private float targetZ; // Stores the target Z rotation
    private Quaternion initialRotation; // Initial rotation during animation
    private Quaternion targetRotation; // Target rotation during animation
    private float animationProgress = 0f; // Tracks progress of the animation

    private void Start()
    {
        // Initialize the door in the closed state (Z = 180)
        transform.rotation = Quaternion.Euler(90f, 180f, 180f);
    }

    public void ToggleDoor()
    {
        if (isAnimating) return; // Prevent multiple animations

        // Define the new target Z rotation based on the current state
        if (!isOpen)
        {
            // Closed -> Opening
            targetZ = 60f;
        }
        else
        {
            // Open -> Closing
            targetZ = 180f;
        }

        // Set the initial and target rotations
        initialRotation = transform.rotation;
        targetRotation = Quaternion.Euler(90f, 180f, targetZ);

        // Reset animation progress and start animating
        animationProgress = 0f;
        isAnimating = true;
        isOpen = !isOpen; // Toggle the state
    }

    private void Update()
    {
        if (isAnimating)
        {
            // Increment animation progress
            animationProgress += Time.deltaTime * animationSpeed;

            // Interpolate rotation between initial and target
            transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, animationProgress);

            // End animation when progress is complete
            if (animationProgress >= 1f)
            {
                isAnimating = false;

                // Ensure exact target rotation at the end
                transform.rotation = targetRotation;
            }
        }
    }
}
