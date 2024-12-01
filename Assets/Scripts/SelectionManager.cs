using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private float maxDistance = 3f; // Max raycast distance

    private Transform _selection;
    private Material _originalMaterial; // Store the original material of the selected object

    void Update()
    {
        // Deselect the previously selected object
        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            if (selectionRenderer != null)
            {
                selectionRenderer.material = _originalMaterial; // Restore the original material
            }
            _selection = null;
        }

        // Cast a ray from the center of the screen
        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // Add max distance to the raycast
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            var selection = hit.transform;

            if (selection.CompareTag(selectableTag))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    _originalMaterial = selectionRenderer.material; // Save the original material
                    selectionRenderer.material = highlightMaterial; // Apply the highlight material
                }

                _selection = selection;

                // Check for interaction input
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Check for food interaction
                    var food = selection.GetComponent<Food>();
                    if (food != null)
                    {
                        food.ConsumeFood(Camera.main.transform); // Start consuming the food
                    }
                    var pipe = selection.GetComponent<Pipe>();
                    if (pipe != null)
                    {
                        pipe.RotatePipe(); // Rotate the pipe when "E" is pressed
                    }
                    // Check for Door
                    var door = selection.GetComponent<Door>();
                    if (door != null)
                    {
                        door.ToggleDoor(); // Open or close the door
                        Debug.Log("Door open/close");
                    }
                    // Check for garden door interaction
                    var gardenDoor = selection.GetComponent<GardenDoor>();
                    if (gardenDoor != null)
                    {
                        gardenDoor.ToggleDoor(); // Open or close the garden door
                    }
                }
            

            }
        }
    }
}
