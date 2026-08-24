using System.Collections;
using UnityEngine;

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
