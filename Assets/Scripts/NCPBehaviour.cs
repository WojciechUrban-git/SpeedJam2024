using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    [SerializeField] private Transform player; // Assign the player in the Inspector
    [SerializeField] private NPCDialogueManager dialogueManager; // Reference to the dialogue manager
    [SerializeField] private ObjectiveManager objectiveManager;

    private string[] dialoguePhase1 = {
        "Hello there, I need help fixing my pipes",
        "From what I've seen, you need to align the pipes correctly"
    };

    private string[] dialoguePhase2 = {
        "Thank you for fixing my pipes!",
        "My toilets are all clogged up. Could you flush them?",
        "Oh, and remove the water from my bathtub"
    };

    private int dialoguePhase = 1; // Keep track of the dialogue phase

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
        if (dialogueManager != null && !dialogueManager.IsDialogueActive())
        {
            if (dialoguePhase == 1)
            {
                dialogueManager.StartDialogue(dialoguePhase1, this);
            }
            else if (dialoguePhase == 2)
            {
                dialogueManager.StartDialogue(dialoguePhase2, this);
            }
        }
    }

    // This method is called when the dialogue ends
    public void OnDialogueEnd()
    {
        // Perform actions based on the dialogue phase
        if (dialoguePhase == 1)
        {
            // Start the first objective
            if (objectiveManager != null)
            {
                objectiveManager.newObjective();
            }
        }
        else if (dialoguePhase == 2)
        {
            // Start the second objective
            if (objectiveManager != null)
            {
                objectiveManager.newObjective();
            }
        }

        // Increment dialogue phase
        dialoguePhase++;

        // After dialogue ends, prevent NPC interaction
        gameObject.tag = "Untagged";
    }

}
