using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f; // Speed at which the food moves toward the camera
    [SerializeField] private Transform playerCamera; // Reference to the player's camera

    private bool isConsumed = false; // Tracks if the food is currently being consumed

    private void Update()
    {
        if (isConsumed)
        {
            // Move the food toward the camera
            transform.position = Vector3.MoveTowards(transform.position, playerCamera.position, moveSpeed * Time.deltaTime);

            // Check if the food has reached the camera
            if (Vector3.Distance(transform.position, playerCamera.position) < 0.1f)
            {
                Consume(); // Complete the consumption process
            }
        }
    }

    public void ConsumeFood(Transform cameraTransform)
    {
        if (!isConsumed)
        {
            isConsumed = true; // Mark the food as consumed
            playerCamera = cameraTransform; // Set the camera as the target
        }
    }

    private void Consume()
    {
        // Activate boost effect on the player
        var player = FindObjectOfType<Movement>(); // Assuming the player script is "Movement"
        if (player != null)
        {
            player.ActivateBoost(); // Trigger the boost effect
        }

        // Destroy the food object
        Destroy(gameObject);
    }
}
