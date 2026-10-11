using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;

public class DialogPortraits : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image portraitImage;

    [Header("Character Portraits")]
    [SerializeField] private Sprite playerPortrait;
    [SerializeField] private Sprite questGiverPortrait;
    [SerializeField] private Sprite friend1Portrait;
    [SerializeField] private Sprite friend2Portrait;

    [YarnCommand("portrait")]
    public void SetPortrait(string character)
    {
        if (portraitImage == null)
        {
            Debug.LogWarning("Portrait Image is not assigned.");
            return;
        }

        switch (character.ToLowerInvariant())
        {
            case "player":
                portraitImage.sprite = playerPortrait;
                break;

            case "questgiver":
                portraitImage.sprite = questGiverPortrait;
                break;

            case "friend1":
                portraitImage.sprite = friend1Portrait;
                break;

            case "friend2":
                portraitImage.sprite = friend2Portrait;
                break;

            default:
                Debug.LogWarning(
                    "Unknown portrait character: " + character);
                break;
        }

        portraitImage.enabled = portraitImage.sprite != null;
    }
}
