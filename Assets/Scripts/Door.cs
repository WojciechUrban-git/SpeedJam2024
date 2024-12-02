using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isOpen = false; // Tracks the state of the door
    private bool isAnimating = false; // Prevents interruption during animation
    private float animationSpeed = 2f; // Controls the speed of the animation
    private float initialX; // Initial X rotation
    private float initialY; // Initial Y rotation
    private float initialZ; // Initial Z rotation
    private float targetZ; // Stores the target Z rotation

    private Quaternion initialRotation; // Initial rotation during animation
    private Quaternion targetRotation;  // Target rotation during animation
    private float animationProgress = 0f; // Tracks progress of the animation

    public AudioSource doorSound;

    private void Start()
    {
        // Store the initial rotation values
        Vector3 startRotation = transform.rotation.eulerAngles;
        initialX = startRotation.x;
        initialY = startRotation.y;
        initialZ = startRotation.z;

        // Trigger initial door state setup
        ToggleDoor();
    }

    public void ToggleDoor()
    {
        if (isAnimating) return; // Prevent multiple animations

        // Calculate the new target Z rotation based on the current state
        targetZ = isOpen ? initialZ - 120f : initialZ;

        // Set the initial and target rotations
        initialRotation = transform.rotation;
        targetRotation = Quaternion.Euler(initialX, initialY, targetZ);

        // Reset animation progress and start animating
        animationProgress = 0f;
        isAnimating = true;
        isOpen = !isOpen; // Toggle the state
        doorSound.Play();
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
            }
        }
    }
}
