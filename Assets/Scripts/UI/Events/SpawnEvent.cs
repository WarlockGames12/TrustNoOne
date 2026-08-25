using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Spawn Event")]
public class SpawnEvent : GameEvent
{
    [Header("Spawn Settings:")]
    [SerializeField] private GameObject[] characters;

    public override IEnumerator Execute(EventPlayer eventPlayer)
    {
        var point = eventPlayer.GetTransform();
        Instantiate(characters[Random.Range(0, characters.Length)], point.transform.position, point.transform.rotation, point);
        yield return null;
    }
}
