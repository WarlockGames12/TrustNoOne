using System.Collections.Generic;
using UnityEngine;


public class NpcList : MonoBehaviour
{
    [Header("NPC Settings:")]
    [SerializeField] private List<NpcRole> npc;
    [SerializeField] private GameObject afterListEvent;
    [SerializeField] private GameObject pointEvent;
    [SerializeField, Range(0, 1000)] private int guaranteedMoney;

    // private variables
    private readonly Dictionary<string, NpcRole> npc_dic = new();
    private readonly List<NpcRole> deceased_humans = new();
    private readonly List<NpcRole> deceased_robots = new();
    private readonly List<NpcRole> alive_humans = new();
    private readonly List<NpcRole> alive_robots = new();

    public string CurrentID { get; private set;}
    public NpcRole Current { get; private set;}
    private int shift_day;

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

    public void SetCurrentSpawned(NpcRole spawned_instance)
    {
        if (spawned_instance == null || string.IsNullOrEmpty(spawned_instance.ID))
            return;
        
        CurrentID = spawned_instance.ID;
        Current = spawned_instance;

        npc_dic[spawned_instance.ID] = spawned_instance;
    }

    public void RegisterCharacter(NpcRole character)
    {
        if (character == null || string.IsNullOrEmpty(character.ID))
            return;
        npc_dic[character.ID] = character;
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
            if (alive_humans.Count > 0 || deceased_robots.Count > 0)
            {
                var plus = alive_humans.Count + deceased_robots.Count;
                var calculate = plus - deceased_humans.Count;
                var result_money = guaranteedMoney * calculate;
                if (PlayerPrefs.HasKey("Result_Money"))
                {
                    var get_money = PlayerPrefs.GetInt("Result_Money");
                    result_money += get_money;
                    PlayerPrefs.SetInt("Result_Money", result_money);
                }
                else
                    PlayerPrefs.SetInt("Result_Money", result_money);
            }

            if (alive_robots.Count >= 0)
                PlayerPrefs.SetInt("Alive_Robots", alive_robots.Count);
            else if (alive_robots.Count == 0)
                PlayerPrefs.SetInt("No_Robots_Got_In", 0);

            if (PlayerPrefs.HasKey("Shift_Day"))
                shift_day = PlayerPrefs.GetInt("Shift_Day");
            shift_day += 1;
            
            PlayerPrefs.SetInt("Has_Save", 1);
            PlayerPrefs.SetInt("Shift_Day", shift_day); 
            afterListEvent.SetActive(true);
        }
    }
}
