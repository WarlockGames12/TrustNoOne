using UnityEngine;

public enum RoleType
{
    None,
    Human,
    Robot
}

public enum Gender
{
    Male,
    Female, 
    Child
}

public class NpcRole : MonoBehaviour
{
    [Header("NPC Settings:")]
    [SerializeField] private string id;
    public RoleType roles;
    public Gender gender;

    public string ID => id;
}
