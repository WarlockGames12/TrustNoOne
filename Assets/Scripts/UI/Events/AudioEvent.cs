using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(menuName = "Events/Audio Event")]
public class AudioEvent : GameEvent
{

    public enum AudioStatus
    {
        One, 
        List
    }

    [Header("Audio Standalone Settings:")]
    [SerializeField] private AudioClip audioOnceClip;

    [Header("Audio List Settings:")]
    [SerializeField] private string listID;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioStatus audioStatus;

    [Header("Wait Settings:")]
    [SerializeField] private bool waitForFinish = true;

    public override IEnumerator Execute(EventPlayer eventPlayer)
    {
        var source = eventPlayer.GetSource();

        switch (audioStatus)
        {
            case AudioStatus.One:
                source.clip = audioOnceClip;
                source.Play();
                break;
            case AudioStatus.List:
                var target = eventPlayer.GetTarget(listID);
                if (target == null)
                    yield break;

                if (!target.TryGetComponent<NpcList>(out var npc_list))
                    yield break;

                if (npc_list.Current == null)
                    yield break;

                source = eventPlayer.GetSource();

                switch (npc_list.Current.gender)
                {
                    case Gender.Male:
                        source.clip = audioClips[0];
                        break;
                    case Gender.Female:
                        source.clip = audioClips[1];
                        break;
                    case Gender.Child:
                        source.clip = audioClips[2];
                        break;
                }
                source.Play();
                break;
        }

        if (waitForFinish && source != null)
            yield return new WaitWhile(() => source.isPlaying);
    }
}
