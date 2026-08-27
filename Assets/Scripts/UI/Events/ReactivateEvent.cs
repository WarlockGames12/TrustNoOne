using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Events/Reactivate Event Player Event")]
public class ReactivateEvent : GameEvent
{
    [Header("Reactivate Settings:")]
    [SerializeField] private string eventPlayerID;

    public override IEnumerator Execute(EventPlayer eventPlayer)
    {
        var target = eventPlayer.GetTarget(eventPlayerID);
        if (target == null)
            yield break;
        
        if (!target.TryGetComponent<EventPlayer>(out var target_event_player))
            yield break;
            
        target_event_player.PlayEvent();
        yield return null;
    }
}
