using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public void NewGame(string scene)
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(scene);
    }
    public void ExitGame() => Application.Quit();
}
