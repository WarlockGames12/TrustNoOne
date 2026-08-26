using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Spawn Event")]
public class SpawnEvent : GameEvent
{
    [Header("Spawn Settings:")]
    [SerializeField] private string listTargetId;

    public override IEnumerator Execute(EventPlayer eventPlayer)
    {
        var tar = eventPlayer.GetTarget(listTargetId);
        if (tar == null)
            yield break;
        
        if (!tar.TryGetComponent<NpcList>(out var npc_list))
            yield break;
        
        var pref = npc_list.GetCharacterToSpawn();
        if (pref == null)
            yield break;
        
        var point = eventPlayer.GetTransform();
        Instantiate(pref, point.transform.position, point.transform.rotation);
        yield return null;
    }
}
