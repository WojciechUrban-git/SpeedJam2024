using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepController : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the audio source
    public List<AudioClip> grassFootsteps; // List of grass footsteps sounds
    public List<AudioClip> woodFootsteps; // List of wood footsteps sounds
    public List<AudioClip> concreteFootsteps; // List of concrete footsteps sounds

    private string currentSurface = "default"; // To keep track of the current surface
    public float raycastDistance = 1.5f; // The distance the ray should check below the player
    public float footstepInterval = 0.5f; // Interval between footstep sounds (in seconds)
    private float lastFootstepTime = 0f; // Time of the last footstep sound played
    private Vector3 lastPosition; // To track the last position of the player

    void Start()
    {
        lastPosition = transform.position; // Initialize the last position
    }

    void Update()
    {
        // Cast a ray downward to check for the surface type
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance))
        {
            // Set the current surface based on the tag of the collider hit
            if (hit.collider.CompareTag("Grass"))
            {
                currentSurface = "grass";
            }
            else if (hit.collider.CompareTag("Wood"))
            {
                currentSurface = "wood";
            }
            else if (hit.collider.CompareTag("Concrete"))
            {
                currentSurface = "concrete";
            }
            else
            {
                currentSurface = "default";
            }
        }
        else
        {
            currentSurface = "default";
        }

        // Check if the player is moving by comparing positions
        if (IsPlayerMoving())
        {
            // If the time since the last footstep is greater than the footstep interval, play the next footstep
            if (Time.time - lastFootstepTime > footstepInterval)
            {
                PlayFootstep();
                lastFootstepTime = Time.time; // Update last footstep time
            }
        }
        else
        {
            // Stop playing the footstep sound when the player stops
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        lastPosition = transform.position; // Update last position after each update
    }

    bool IsPlayerMoving()
    {
        // Check if the current position is different from the last position to detect movement
        return Vector3.Distance(transform.position, lastPosition) > 0.01f; // Small threshold to avoid detecting noise
    }

    void PlayFootstep()
    {
        AudioClip clipToPlay = null;

        // Choose the appropriate footstep sound based on the surface
        switch (currentSurface)
        {
            case "grass":
                clipToPlay = grassFootsteps[Random.Range(0, grassFootsteps.Count)];
                break;
            case "wood":
                clipToPlay = woodFootsteps[Random.Range(0, woodFootsteps.Count)];
                break;
            case "concrete":
                clipToPlay = concreteFootsteps[Random.Range(0, concreteFootsteps.Count)];
                break;
            default:
                return; // No sound if the surface is not recognized
        }

        // Play the chosen footstep sound
        if (clipToPlay != null)
        {
            audioSource.PlayOneShot(clipToPlay);
        }
        else
        {
            Debug.Log("No clip to play");
        }
    }
}
