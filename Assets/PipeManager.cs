using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    public List<Pipe> pipes; // Assign all the pipes in the scene to this list

    void Update()
    {
        // Continuously log the fraction of pipes that are correctly aligned
        int correctlyAlignedCount = GetCorrectlyAlignedPipeCount();
        Debug.Log($"Pipes correctly aligned: {correctlyAlignedCount}/{pipes.Count}");

        // Check if all pipes are correctly aligned
        if (correctlyAlignedCount == pipes.Count)
        {
            Debug.Log("All pipes are correctly aligned! Puzzle solved.");
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
}
