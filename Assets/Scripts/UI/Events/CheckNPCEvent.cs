using System.Collections;
using UnityEngine;


[CreateAssetMenu(menuName = "Events/Check NPC Event")]
public class CheckNPCEvent : GameEvent
{

    [Header("Killed Or Not Settings:")]
    [SerializeField] private string listTargetID;
    [SerializeField] private bool killed;

    public override IEnumerator Execute(EventPlayer eventPlayer)
    {
        var target = eventPlayer.GetTarget(listTargetID);
        if (target == null)
            yield break;
        
        if (!target.TryGetComponent<NpcList>(out var npc_list))
            yield break;

        npc_list.RemoveCharacterFromList(npc_list.CurrentID, killed);

        yield return null;
    }
}
