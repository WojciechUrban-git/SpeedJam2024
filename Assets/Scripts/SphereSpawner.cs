using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spherePrefab; // Prefab for the sphere
    [SerializeField] private Transform waterSurface; // Reference to the water
    [SerializeField] private float waterDropAmount = 0.02f; // Amount the water level drops when a sphere is popped
    [SerializeField] private float waterMinY = -0.1f; // Minimum Y level for water
    public int bubblesLeft = 24;

    private GameObject currentBubble; // Tracks the currently spawned bubble

    private void Start()
    {
        bubblesLeft = 24;
    }
    public void SpawnSphere()
    {
        if (bubblesLeft < 1) return;
        bubblesLeft--;
        Debug.Log("bubbles left = " + bubblesLeft);
        if (waterSurface.position.y <= waterMinY)
        {
            Debug.Log("Water level too low. No more bubbles will spawn.");
            return; // Stop spawning bubbles when the water is too low
        }

        // Random position on the water surface
        Vector3 spawnPosition = new Vector3(
            Random.Range(waterSurface.position.x - waterSurface.localScale.x / 2, waterSurface.position.x + waterSurface.localScale.x / 2),
            waterSurface.position.y + waterSurface.localScale.y / 2, // Place on top of the water
            Random.Range(waterSurface.position.z - waterSurface.localScale.z / 2, waterSurface.position.z + waterSurface.localScale.z / 2)
        );

        // Instantiate the sphere and attach it to this GameObject (bathtub)
        currentBubble = Instantiate(spherePrefab, spawnPosition, Quaternion.identity, transform);

        // Initialize the sphere
        Sphere sphereScript = currentBubble.GetComponent<Sphere>();
        if (sphereScript != null)
        {
            sphereScript.Initialize(this);
        }
    }

    public void LowerWaterLevel()
    {
        if (waterSurface.position.y > waterMinY)
        {
            // Lower the water level
            waterSurface.position -= new Vector3(0, waterDropAmount, 0);
        }

        // Spawn a new bubble after lowering the water level
        SpawnSphere();
    }
}
