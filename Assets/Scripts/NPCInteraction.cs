using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class NPCInteraction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private DialogueRunner dialogueRunner;

    [Header("Interaction")]
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private string nodeName = "QuestGiver";

    private void Update()
    {
        if (Keyboard.current == null)
            return;

        if (!Keyboard.current.eKey.wasPressedThisFrame)
            return;

        if (dialogueRunner == null ||
            dialogueRunner.IsDialogueRunning)
            return;

        if (playerCamera == null)
        {
            Debug.LogWarning("Assign the player camera.", this);
            return;
        }

        Ray ray = playerCamera.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0f)
        );

        if (Physics.Raycast(
            ray,
            out RaycastHit hit,
            interactionDistance,
            Physics.DefaultRaycastLayers,
            QueryTriggerInteraction.Ignore))
        {
            NPCInteraction npc =
                hit.collider.GetComponentInParent<NPCInteraction>();

            if (npc != null)
            {
                npc.StartNPCDialogue();
            }
        }
    }

    public void StartNPCDialogue()
    {
        if (dialogueRunner == null)
        {
            Debug.LogError(
                "Dialogue Runner is not assigned.", this);
            return;
        }

        if (!dialogueRunner.IsDialogueRunning)
        {
            dialogueRunner.StartDialogue(nodeName);
        }
    }
}
