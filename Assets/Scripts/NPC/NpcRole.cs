using UnityEngine;

 public enum RoleType
{
    None,
    Human,
    Robot
}

public class NpcRole : MonoBehaviour
{
    [Header("NPC Settings:")]
    [SerializeField] private string id;
    public RoleType roles;

    public string ID => id;
}
