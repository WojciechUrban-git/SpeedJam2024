using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    [SerializeField] private Transform player; // Assign the player in the Inspector
    [SerializeField] private NPCDialogueManager dialogueManager; // Reference to the dialogue manager

    private string[] dialogueSentences = {
        "Hello there, I need help fixing my pipes",
        "From what i've seen you need to align the pipes correctly",
    };

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = new Vector3(player.position.x - transform.position.x, 0, player.position.z - transform.position.z);
            transform.parent.rotation = Quaternion.LookRotation(direction);
        }
    }

    public void TriggerDialogue()
    {
        if (dialogueManager != null && !dialogueManager.IsDialogueActive()) // Check if the dialogue is not active
        {
            dialogueManager.StartDialogue(dialogueSentences);
        }
    }
}
