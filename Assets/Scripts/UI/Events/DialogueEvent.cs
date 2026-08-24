using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Dialogue Event")]
public class DialogueEvent : GameEvent
{
    [Header("Dialogue Event Settings:")]
    [SerializeField] private DialogueBox dialogue;
    
    // Event is used to play the dialogue until its finished
    public override IEnumerator Execute(EventPlayer eventPlayer)
    {
        var dialogueSystem = eventPlayer.GetDialogueSystem();
        dialogueSystem.StartDialogue(dialogue);
        yield return new WaitUntil(() => !dialogueSystem.IsDialogueActive());
    }   
}
