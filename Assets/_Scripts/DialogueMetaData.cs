using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DialogueData", order = 1)]
public class DialogueMetaData : ScriptableObject
{
    public string Dialogue;
    public string DialogueHappy;
    public string DialogueSad;

    public Sprite profileImage;
    public Sprite CharacterImage;
    public AudioClip introAudio;
}
