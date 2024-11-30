using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private Material highlightMaterial;

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

        if (Physics.Raycast(ray, out hit))
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
                    var pipe = selection.GetComponent<Pipe>();
                    if (pipe != null)
                    {
                        pipe.RotatePipe(); // Rotate the pipe when "E" is pressed
                    }
                }
            }
        }
    }
}
