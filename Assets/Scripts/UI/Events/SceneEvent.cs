using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Events/Scene Event")]
public class SceneEvent : GameEvent
{

    [Header("Get to Scene Settings:")]
    [SerializeField] private string sceneName;

    public override IEnumerator Execute(EventPlayer eventPlayer)
    {
        SceneManager.LoadScene(sceneName);
        yield return null;
    }
}
