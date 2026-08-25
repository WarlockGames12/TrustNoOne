using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Animation Event")]
public class AnimationEvent : GameEvent
{
    public enum AnimationAction
    {
        Play,
        SetBool,
        SetActive
    }

    [Header("Action Settings:")]
    [SerializeField] private AnimationAction action;

    [Header("Play Settings:")]
    [SerializeField] private string animName;

    [Header("Set Bool Settings:")]
    [SerializeField] private string boolName;
    [SerializeField] private bool boolVal;

    [Header("Wait Settings:")]
    [SerializeField] private bool waitForFinish = true;

    [Header("Set Active Settings:")]
    [SerializeField] private bool SetActive;

    public override IEnumerator Execute(EventPlayer eventPlayer)
    {
        var anim = eventPlayer.GetAnimator();
        switch (action)
        {
            case AnimationAction.Play:
                anim.Play(animName);

                if (waitForFinish)
                {
                    yield return null;
                    while (!anim.GetCurrentAnimatorStateInfo(0).IsName(animName))
                        yield return null;
                    while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
                        yield return null;
                }

                break;
            case AnimationAction.SetBool:
                anim.SetBool(boolName, boolVal);

                if (waitForFinish)
                {
                    yield return null;

                    while (anim.IsInTransition(0))
                        yield return null;

        
                    while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
                        yield return null;
                }
                break;
            case AnimationAction.SetActive:
                anim.enabled = SetActive;
                break;
        }

        yield return null;
    }
}
