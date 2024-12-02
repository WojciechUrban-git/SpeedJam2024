using UnityEngine;
using TMPro;

public class NPCDialogueManager : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject dialogueWindow;
    [SerializeField] private ObjectiveManager objectiveManager;


    private string[] dialogueSentences;
    private int currentSentenceIndex = 0;
    private bool isDialogueActive = false;

    private NPCBehavior currentNPC; // Reference to the NPC that started the dialogue

    public void StartDialogue(string[] sentences, NPCBehavior npc)
    {
        // Reset the dialogue if it's already active
        if (isDialogueActive)
        {
            EndDialogue(); // Close the current dialogue if it's already active
        }

        dialogueSentences = sentences;
        currentSentenceIndex = 0;
        isDialogueActive = true;
        dialogueWindow.SetActive(true);
        ShowCurrentSentence();

        currentNPC = npc; // Keep reference to NPC
    }

    public bool IsDialogueActive()
    {
        return isDialogueActive;
    }

    void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.E))
        {
            currentSentenceIndex++;
            if (currentSentenceIndex < dialogueSentences.Length)
            {
                ShowCurrentSentence();
            }
            else
            {
                EndDialogue();
                // Notify NPC that dialogue has ended
                if (currentNPC != null)
                {
                    currentNPC.OnDialogueEnd();
                }
                currentNPC = null; // Clear the reference
            }
        }
    }

    private void ShowCurrentSentence()
    {
        if (currentSentenceIndex < dialogueSentences.Length)
        {
            dialogueText.text = dialogueSentences[currentSentenceIndex];
        }
    }

    private void EndDialogue()
    {
        dialogueWindow.SetActive(false);
        isDialogueActive = false;
    }
}
