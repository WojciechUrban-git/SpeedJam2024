using UnityEngine;
using TMPro;

public class NPCDialogueManager : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject dialogueWindow;

    private string[] dialogueSentences;
    private int currentSentenceIndex = 0;
    private bool isDialogueActive = false;

    public void StartDialogue(string[] sentences)
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
                // Trigger a new objective when dialogue ends
                FindObjectOfType<ObjectiveManager>().newObjective();
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
