using UnityEngine;

public class StraightPipe : Pipe
{
    public override bool IsCorrectlyAligned()
    {
        // Straight pipes are correct if they are aligned at either 0° or 180° relative to the correct rotation
        return Mathf.Approximately(currentZRotation, correctZRotation) ||
               Mathf.Approximately((currentZRotation + 180) % 360, correctZRotation);
    }
}
