using UnityEngine;

public class SpawnPointEvents : MonoBehaviour
{

    [Header("Spawn Point Settings:")]
    [SerializeField] private Transform[] points;
    [SerializeField] private GameObject target;
    private bool has_player_prefs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        has_player_prefs = PlayerPrefs.HasKey("Has_Save");
        var spawn_point = points[has_player_prefs ? 1 : 0];
        target.transform.SetPositionAndRotation(spawn_point.position, spawn_point.rotation);
    }

    public void RemoveHasSave() => PlayerPrefs.DeleteKey("Has_Save");
}
