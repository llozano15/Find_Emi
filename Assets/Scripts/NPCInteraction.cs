using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class NPCInteraction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private DialogueRunner dialogueRunner;

    [Header("Interaction")]
    [SerializeField] private float interactionDistance = 5f;
    [SerializeField] private string nodeName = "QuestGiver";
    [SerializeField] private LayerMask interactionLayers = ~0;

    private void Update()
    {
        if (Keyboard.current == null)
            return;

        if (!Keyboard.current.eKey.wasPressedThisFrame)
            return;

        Debug.Log("E key detected!");

        if (playerCamera == null)
        {
            Debug.LogError("Player Camera is NOT assigned!", this);
            return;
        }

        if (dialogueRunner == null)
        {
            Debug.LogError("Dialogue Runner is NOT assigned!", this);
            return;
        }

        if (dialogueRunner.IsDialogueRunning)
        {
            Debug.Log("Dialogue is already running.");
            return;
        }

        Ray ray = playerCamera.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0f)
        );

        if (Physics.Raycast(ray,out RaycastHit hit,interactionDistance,~0,
            QueryTriggerInteraction.Collide))
        {
            Debug.Log(
                $"Raycast hit: {hit.collider.name}, " +
                $"Layer: {LayerMask.LayerToName(hit.collider.gameObject.layer)}, " +
                $"Distance: {hit.distance:F2}"
            );

            NPCInteraction npc =
                hit.collider.GetComponentInParent<NPCInteraction>();

            if (npc != null)
            {
                Debug.Log("Found NPC: " + npc.nodeName);
                npc.StartNPCDialogue();
            }
            else
            {
                Debug.Log("This object is not an NPC.");
            }
        }
        else
        {
            Debug.LogWarning("Nothing detected within " +
                             interactionDistance + " units.");
        }
    }

    public void StartNPCDialogue()
    {
        if (dialogueRunner == null)
            return;

        if (!dialogueRunner.IsDialogueRunning)
        {
            dialogueRunner.StartDialogue(nodeName);
        }
    }
}
