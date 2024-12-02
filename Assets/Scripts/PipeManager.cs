using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    public List<Pipe> pipes; // Assign all the pipes in the scene to this list
    public bool complete;

    public AudioSource success;
    public AudioSource working;


    private void Start()
    {
        complete = false;
    }

    void Update()
    {
        // Continuously log the fraction of pipes that are correctly aligned
        int correctlyAlignedCount = GetCorrectlyAlignedPipeCount();
        Debug.Log($"Pipes correctly aligned: {correctlyAlignedCount}/{pipes.Count}");

        // Check if all pipes are correctly aligned and not already complete
        if (!complete && correctlyAlignedCount == pipes.Count)
        {
            Debug.Log("All pipes are correctly aligned! Puzzle solved.");
            complete = true;
            success.Play();
            working.Play();
            // Loop through all pipes and set their tag to "Untagged"
            foreach (Pipe pipe in pipes)
            {
                pipe.gameObject.tag = "Untagged";
            }
        }
    }

    private int GetCorrectlyAlignedPipeCount()
    {
        int count = 0;

        foreach (Pipe pipe in pipes)
        {
            if (pipe.IsCorrectlyAligned())
            {
                count++;
            }
        }

        return count;
    }

    // New function to set the "Selectable" tag on every pipe
    public void SetPipesToSelectable()
    {
        foreach (Pipe pipe in pipes)
        {
            pipe.gameObject.tag = "Selectable";
        }
    }
}
