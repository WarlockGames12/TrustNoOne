using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Shotgun Shoot Event")]
public class ShootEvent : GameEvent
{
    [Header("Get Shoot UI:")]
    [SerializeField] private string targetName;

    public override IEnumerator Execute(EventPlayer eventPlayer)
    {
        var target = eventPlayer.GetTarget(targetName);
        var shoot = target.GetComponent<ShotgunUI>();
        shoot.Shoot();
        yield return null;
    }
}
