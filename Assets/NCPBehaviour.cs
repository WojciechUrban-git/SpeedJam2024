using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    [SerializeField] private Transform player; // Assign the player in the Inspector

    void Update()
    {
        if (player != null)
        {
            // Calculate the direction to the player
            Vector3 direction = new Vector3(player.position.x - transform.position.x, 0, player.position.z - transform.position.z);

            // Rotate the parent GameObject to face the player on the Y-axis
            transform.parent.rotation = Quaternion.LookRotation(direction);
        }
    }
}
