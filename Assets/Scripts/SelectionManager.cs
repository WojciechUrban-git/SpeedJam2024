using TMPro;
using UnityEngine;
using UnityEngine.UI; // For UI Text and Image. Use TMPro if using TextMeshPro.

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private float maxDistance = 3f; // Max raycast distance
    [SerializeField] private TMP_Text interactionText; // Reference to the UI Text
    [SerializeField] private Image textBackground; // Reference to the background image

    private Transform _selection;
    private Material _originalMaterial; // Store the original material of the selected object

    void Start()
    {
        // Ensure text and background are hidden at the start
        interactionText.text = string.Empty;
        textBackground.gameObject.SetActive(false);
    }

    void Update()
    {
        // Hide the interaction text and background initially
        interactionText.text = string.Empty;
        textBackground.gameObject.SetActive(false);

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

                // Determine and display interaction text
                if (selection.GetComponent<Food>() != null)
                {
                    interactionText.text = "[Press E to Eat]";
                }
                else if (selection.GetComponent<Pipe>() != null)
                {
                    interactionText.text = "[Press E to Interact]";
                }
                else if (selection.GetComponent<Door>() != null || selection.GetComponent<GardenDoor>() != null)
                {
                    interactionText.text = "[Press E to Open]";
                }
                else if (selection.GetComponent<NPCBehavior>() != null)
                {
                    interactionText.text = "Press E to Listen";
                }

                // Show the text background when text is displayed
                if (!string.IsNullOrEmpty(interactionText.text))
                {
                    textBackground.gameObject.SetActive(true);
                }

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

                    // Check for sphere interaction
                    var sphere = selection.GetComponent<Sphere>();
                    if (sphere != null)
                    {
                        sphere.Pop(); // Pop the sphere
                    }

                    var toilet = selection.GetComponentInChildren<ToiletFlush>();
                    if (toilet != null)
                    {
                        toilet.Flush();
                    }

                    var npc = selection.GetComponentInChildren<NPCBehavior>();
                    if (npc != null)
                    {
                        Debug.Log("Game start");
                    }
                }
            }
        }
    }
}
