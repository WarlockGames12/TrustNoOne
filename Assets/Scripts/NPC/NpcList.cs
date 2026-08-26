using System.Collections.Generic;
using UnityEngine;


public class NpcList : MonoBehaviour
{
    [Header("NPC Settings:")]
    [SerializeField] private List<NpcRole> npc;
    [SerializeField] private GameObject afterListEvent;
    [SerializeField] private GameObject pointEvent;

    // private variables
    private readonly Dictionary<string, NpcRole> npc_dic = new();
    private readonly List<NpcRole> deceased_humans = new();
    private readonly List<NpcRole> deceased_robots = new();
    private readonly List<NpcRole> alive_humans = new();
    private readonly List<NpcRole> alive_robots = new();

    private void Awake()
    {
        foreach(var character in npc)
            RegisterCharacter(character);
    }

    public NpcRole GetCharacterToSpawn()
    {
        if (npc.Count <= 0)
            return null;
        
        var index = Random.Range(0, npc.Count);
        var character = npc[index];
        npc.RemoveAt(index);

        return character;
    }

    public void RegisterCharacter(NpcRole character)
    {
        if (character == null || string.IsNullOrEmpty(character.ID))
            return;
        if (!npc_dic.ContainsKey(character.ID))
            npc_dic.Add(character.ID, character);
    }

    public void RemoveCharacterFromList(string id, bool killed)
    {
        if (!npc_dic.TryGetValue(id, out var character))
            return;
        npc_dic.Remove(id);

        if (killed)
        {
            switch (character.roles)
            {
                case RoleType.Human:
                    deceased_humans.Add(character);
                    break;
                case RoleType.Robot:
                    deceased_robots.Add(character);
                    break;
            }
        }
        else
        {
            switch (character.roles)
            {
                case RoleType.Human:
                    alive_humans.Add(character);
                    break;
                case RoleType.Robot:
                    alive_robots.Add(character);
                    break;
            }
        }

        if (npc_dic.Count <= 0)
        {
            afterListEvent.SetActive(true);
            pointEvent.SetActive(false);
        }
    }
}
