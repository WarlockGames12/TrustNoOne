using System.Collections;
using UnityEngine;

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
