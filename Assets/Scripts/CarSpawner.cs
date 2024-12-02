using System.Collections;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs; // Array of car prefabs to spawn
    public Transform startPoint;   // Start position for cars
    public Transform endPoint;     // End position for cars
    public Transform startPointAlt; // Alternate start position for cars
    public Transform endPointAlt;   // Alternate end position for cars
    public float minSpawnInterval = 20f; // Minimum time interval between spawns
    public float maxSpawnInterval = 40f; // Maximum time interval between spawns
    public float carSpeed;         // Speed of the cars

    void Start()
    {
        StartCoroutine(SpawnCars());
    }

    IEnumerator SpawnCars()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);

            SpawnCar();
        }
    }

    void SpawnCar()
    {
        if (carPrefabs.Length == 0) return;

        // Choose a random car prefab
        GameObject randomCar = carPrefabs[Random.Range(0, carPrefabs.Length)];

        // Randomly decide whether to use the main or alternate start and end points
        bool useAlternateRoute = Random.value > 0.5f;

        Transform chosenStartPoint = useAlternateRoute ? startPointAlt : startPoint;
        Transform chosenEndPoint = useAlternateRoute ? endPointAlt : endPoint;
        Quaternion chosenRotation = useAlternateRoute ? Quaternion.identity : Quaternion.Euler(0, 180, 0);

        // Instantiate the car at the chosen start position with the chosen rotation
        GameObject spawnedCar = Instantiate(randomCar, chosenStartPoint.position, chosenRotation);

        // Move the car towards the chosen end point
        StartCoroutine(MoveCar(spawnedCar, chosenEndPoint));
    }

    IEnumerator MoveCar(GameObject car, Transform targetPoint)
    {
        carSpeed = Random.Range(10f, 20f);
        while (car != null && Vector3.Distance(car.transform.position, targetPoint.position) > 0.1f)
        {
            car.transform.position = Vector3.MoveTowards(car.transform.position, targetPoint.position, carSpeed * Time.deltaTime);
            yield return null;
        }

        if (car != null)
        {
            Destroy(car); // Destroy the car after reaching the end point
        }
    }
}
