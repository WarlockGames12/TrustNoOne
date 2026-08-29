using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public void NewGame(string scene)
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(scene);
    }

    public void ChangeScenes(string scene) => SceneManager.LoadScene(scene);
    public void RemoveHasSave() => PlayerPrefs.DeleteKey("Has_Save");
    public void ExitGame() => Application.Quit();
}
