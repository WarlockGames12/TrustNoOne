using UnityEngine;
using System;


[CreateAssetMenu(fileName = "New Dialogue Data", menuName = "Dialogue System/Dialogue Box")]
public class DialogueBox : ScriptableObject
{
    [Serializable]
    public struct DialogueEntry
    {
        [Header("Dialogue Settings:")]
        public AudioClip speakerVoice;
        public string speakerName;
        [TextArea(3, 10)] public string speakerDescription;
    }

    [Serializable]
    public struct DialogueOption
    {
        [Header("Dialogue Option Settings:")]
        public string optionText;
        public DialogueBox nextDialogue;
    }

    public DialogueEntry[] dialogueEntries;
    public DialogueOption[] dialogueOptions;

    [Header("If true: Play Other event:")]
    public bool anotherEvent;
}
