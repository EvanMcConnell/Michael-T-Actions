using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterMetaData : MonoBehaviour
{
    [Header("Canvas Items")]
    [SerializeField] GameObject DialogueBox;
    [SerializeField] TextMeshProUGUI dialogueText;
   // [SerializeField] TextMeshProUGUI NameText;
    [SerializeField] Image portrait;

    [Header("3D Items")]
    [SerializeField] SpriteRenderer CharacterSpriteRenderer;

    [Header("Weezer ‍웃웃웃웃")]
    [SerializeField] DialogueMetaData DialogueMetaData;

    internal Camera camera;

    private void Start()
    {

        DialogueBox.GetComponent<Canvas>().worldCamera = PlatfromerCameraController.Instance.GetComponentInChildren<Camera>();
        dialogueText.SetText(DialogueMetaData.Dialogue);

        portrait.sprite = DialogueMetaData.profileImage;
        if (CharacterSpriteRenderer != null)
        {
            CharacterSpriteRenderer.sprite = DialogueMetaData.CharacterImage;
        }
        // Name (coming soon (never))

        DialogueBox.SetActive(false);
    }
    internal void ActivateDialogueBox()
    {
        DialogueBox.SetActive(true);
    }

    internal void DeactivateDialogueBox()
    {
        DialogueBox.SetActive(false);
    }

    /// <summary>
    /// IF POOR THEY ARE SAD 
    /// </summary>
    /// <param name="poor">LOSER</param>
    internal void SetDialogue(bool poor)
    {
        if (!poor)
        {
            dialogueText.SetText(DialogueMetaData.DialogueHappy);
        }
        else
        {
            dialogueText.SetText(DialogueMetaData.DialogueSad);
        }
    }
}
