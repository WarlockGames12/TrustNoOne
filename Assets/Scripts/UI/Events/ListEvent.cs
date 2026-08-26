using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/List Event")]
public class ListEvent : GameEvent
{

    [Header("List Event Settings:")]
    [SerializeField] private string listTargetId;
    [SerializeField] private string npcID;
    [SerializeField] private bool killed;

    public override IEnumerator Execute(EventPlayer eventPlayer)
    {
        var target = eventPlayer.GetTarget(listTargetId);
        if (target == null)
            yield break;
            
        if (!target.TryGetComponent<NpcList>(out var npc_list))
            yield break;

        npc_list.RemoveCharacterFromList(npcID, killed);
    }
}
