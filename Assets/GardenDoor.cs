using UnityEngine;

public class GardenDoor : MonoBehaviour
{
    [SerializeField] private float closedPosition = -6.2f; // Position when the door is closed
    [SerializeField] private float openPosition = -7.2f; // Position when the door is open
    [SerializeField] private float slideSpeed = 2f; // Speed of the sliding animation

    private bool isOpen = false; // Tracks the state of the door
    private bool isAnimating = false; // Prevents interruption during animation
    private float animationProgress = 0f; // Tracks progress of the animation
    private float targetPosition; // The target X position of the door

    private void Start()
    {
        // Set initial target position
        targetPosition = closedPosition;
    }

    public void ToggleDoor()
    {
        if (isAnimating) return; // Prevent multiple animations

        // Set the new target position based on the current state
        targetPosition = isOpen ? closedPosition : openPosition;

        // Reset animation progress and toggle the state
        animationProgress = 0f;
        isAnimating = true;
        isOpen = !isOpen;
    }

    private void Update()
    {
        if (isAnimating)
        {
            // Smoothly move the door toward the target position
            Vector3 currentPosition = transform.position;
            currentPosition.x = Mathf.Lerp(currentPosition.x, targetPosition, animationProgress);

            transform.position = currentPosition;

            // Increment animation progress
            animationProgress += Time.deltaTime * slideSpeed;

            // End animation when progress is complete
            if (Mathf.Abs(transform.position.x - targetPosition) < 0.01f)
            {
                transform.position = new Vector3(targetPosition, currentPosition.y, currentPosition.z); // Snap to target position
                isAnimating = false;
            }
        }
    }
}
