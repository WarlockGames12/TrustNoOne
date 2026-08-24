using System.Collections;
using UnityEngine;

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
