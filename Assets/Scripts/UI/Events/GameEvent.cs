using System.Collections;
using UnityEngine;

public abstract class GameEvent : ScriptableObject
{
    public abstract IEnumerator Execute(EventPlayer eventPlayer);
}

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

[CreateAssetMenu(menuName = "Events/Wait Event")]
public class WaitEvent : GameEvent
{
    [Header("Wait Settings:")]
    [SerializeField, Range(0, 100)] private float duration;

    // Event is used to wait during event
    public override IEnumerator Execute(EventPlayer eventPlayer)
    {
        yield return new WaitForSeconds(duration);
    }
}

[CreateAssetMenu(menuName = "Events/Animation Event")]
public class AnimationEvent : GameEvent
{
    public enum AnimationAction
    {
        Play,
        SetBool
    }

    [Header("Action Settings:")]
    [SerializeField] private AnimationAction action;

    [Header("Play Settings:")]
    [SerializeField] private string animName;

    [Header("Set Bool Settings:")]
    [SerializeField] private string boolName;
    [SerializeField] private bool boolVal;

    public override IEnumerator Execute(EventPlayer eventPlayer)
    {
        var anim = eventPlayer.GetAnimator();
        switch (action)
        {
            case AnimationAction.Play:
                anim.Play(animName);
                break;
            case AnimationAction.SetBool:
                anim.SetBool(boolName, boolVal);
                break;
        }

        yield return null;
    }
}

[CreateAssetMenu(menuName = "Events/GameObject Event")]
public class GameObjectEvent : GameEvent
{
    [Header("GameObject Event Settings:")]
    [SerializeField] private string eventId;
    [SerializeField] private bool active;

    public override IEnumerator Execute(EventPlayer eventPlayer)
    {
        var target = eventPlayer.GetTarget(eventId);
        if (target != null)
            target.gameObject.SetActive(active);
        yield return null;
    }
}
