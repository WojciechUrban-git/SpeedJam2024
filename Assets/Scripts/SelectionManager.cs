using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private float maxDistance = 3f;
    [SerializeField] private TMP_Text interactionText;
    [SerializeField] private Image textBackground;
    [SerializeField] private ObjectiveManager objectiveManager;
    [SerializeField] private NPCDialogueManager npcDialogueManager;

    private Transform _selection;
    private Material _originalMaterial;

    void Start()
    {
        interactionText.text = string.Empty;
        textBackground.gameObject.SetActive(false);
    }

    void Update()
    {
        if (npcDialogueManager.IsDialogueActive())
        {
            // Optionally, clear interaction text and hide background
            interactionText.text = string.Empty;
            textBackground.gameObject.SetActive(false);
            return; // Exit early if dialogue is active
        }
        // Reset the interaction text and background
        interactionText.text = string.Empty;
        textBackground.gameObject.SetActive(false);

        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            if (selectionRenderer != null)
            {
                selectionRenderer.material = _originalMaterial;
            }
            _selection = null;
        }

        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            var selection = hit.transform;

            if (selection.CompareTag(selectableTag))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    _originalMaterial = selectionRenderer.material;
                    selectionRenderer.material = highlightMaterial;
                }

                _selection = selection;

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
                    interactionText.text = "[Press E to Talk]";
                }

                if (!string.IsNullOrEmpty(interactionText.text))
                {
                    textBackground.gameObject.SetActive(true);
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    var food = selection.GetComponent<Food>();
                    if (food != null)
                    {
                        food.ConsumeFood(Camera.main.transform);
                    }
                    var pipe = selection.GetComponent<Pipe>();
                    if (pipe != null)
                    {
                        pipe.RotatePipe();
                    }
                    var door = selection.GetComponent<Door>();
                    if (door != null)
                    {
                        door.ToggleDoor();
                        Debug.Log("Door open/close");
                    }
                    var gardenDoor = selection.GetComponent<GardenDoor>();
                    if (gardenDoor != null)
                    {
                        gardenDoor.ToggleDoor();
                    }
                    var sphere = selection.GetComponent<Sphere>();
                    if (sphere != null)
                    {
                        sphere.Pop();
                    }
                    var toilet = selection.GetComponentInChildren<ToiletFlush>();
                    if (toilet != null)
                    {
                        toilet.Flush();
                    }
                    var npc = selection.GetComponentInChildren<NPCBehavior>();
                    if (npc != null)
                    {
                        Debug.Log("NPC PRESSED");
                        npc.TriggerDialogue();
                    }

                    if (!string.IsNullOrEmpty(interactionText.text))
                    {
                        textBackground.gameObject.SetActive(true);
                    }

                    if (selection.name == "Car4")
                    {
                        Debug.Log("The End");
                        objectiveManager.theEnd();
                    }
                }
            }
        }
    }
}
