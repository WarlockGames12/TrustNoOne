using UnityEngine;
using System;


[CreateAssetMenu(fileName = "New Dialogue Data", menuName = "Dialogue System/Dialogue Box")]
public class DialogueBox : ScriptableObject
{
    // Dialogue entries to create dialogue
    [Serializable]
    public struct DialogueEntry
    {
        [Header("Dialogue Settings:")]
        public AudioClip speakerVoice;
        [Range(0.75f, 1.5f)] public float voicePitch; 
        public string speakerName;
        [TextArea(3, 10)] public string speakerDescription;
    }

    // Add Dialogue Options to have different dialogues
    [Serializable]
    public struct DialogueOption
    {
        [Header("Dialogue Option Settings:")]
        public string optionText;
        public DialogueBox nextDialogue;
    }

    // list for dialogue entries
    public DialogueEntry[] dialogueEntries;
    // list for dialogue options
    public DialogueOption[] dialogueOptions;

    [Header("If true: Play Other event:")]
    // if there's an event without any dialogue options, use this
    public bool anotherEvent;
}
