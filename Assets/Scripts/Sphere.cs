using UnityEngine;

public class Sphere : MonoBehaviour
{
    private SphereSpawner spawner;
    public AudioSource popSound;

    [SerializeField] private GameObject bubblePopEffect; // Assign the particle prefab here

    public void Initialize(SphereSpawner spawnerReference)
    {
        popSound.Play();
        spawner = spawnerReference;
        transform.position = new Vector3(
            transform.position.x + Random.Range(-0.7f, 0.7f),
            transform.position.y,
            transform.position.z + Random.Range(-0.4f, 0.4f)
        );
    }

    public void Pop()
    {
        // Spawn the particle effect at the bubble's position
        if (bubblePopEffect != null)
        {
            Instantiate(bubblePopEffect, transform.position, Quaternion.identity);
        }

        // Inform the spawner to lower the water level  
        if (spawner != null)
        {
            spawner.LowerWaterLevel();
        }

        // Destroy the sphere
        Destroy(gameObject);
    }
}
