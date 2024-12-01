using UnityEngine;

public class Sphere : MonoBehaviour
{
    private SphereSpawner spawner;

    public void Initialize(SphereSpawner spawnerReference)
    {
        spawner = spawnerReference;
        transform.position = new Vector3(transform.position.x +Random.Range(-0.7f, 0.7f), transform.position.y, transform.position.z + Random.Range(-0.4f, 0.4f));
    }

    public void Pop()
    {
        // Inform the spawner to lower the water level  
        if (spawner != null)
        {
            spawner.LowerWaterLevel();
        }

        // Destroy the sphere
        Destroy(gameObject);
    }
}
