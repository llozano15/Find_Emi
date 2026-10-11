using UnityEngine;
using Yarn.Unity;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private string nodeName = "QuestGiver";

    private bool playerNearby;

    private void Update()
    {
        if (playerNearby &&
            Input.GetKeyDown(KeyCode.E) &&
            dialogueRunner != null &&
            !dialogueRunner.IsDialogueRunning)
        {
            dialogueRunner.StartDialogue(nodeName);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}
