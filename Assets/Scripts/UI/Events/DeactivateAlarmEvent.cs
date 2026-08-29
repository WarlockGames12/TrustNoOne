using System.Collections;
using UnityEngine;

public class DeactivateAlarmEvent : GameEvent
{

    [Header("Deactivate Alarm Event:")]
    [SerializeField] private string alarmString;

    public override IEnumerator Execute(EventPlayer eventPlayer)
    {
        var target = eventPlayer.GetTarget(alarmString);
        var get_alarm_bool = target.GetComponent<AlarmEvent>();
        get_alarm_bool.alarm_enabled = false;
        yield return null;
    }
}
