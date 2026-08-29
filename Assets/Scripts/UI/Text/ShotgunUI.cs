using UnityEngine;
using UnityEngine.UI;

public class ShotgunUI : MonoBehaviour
{

    [Header("Shotgun Settings:")]
    [SerializeField] private Text shotgunText;
    [SerializeField, Range(0, 5)] private int shotgunShellCount;

    public int current_shell_count;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        if (PlayerPrefs.HasKey("Current_Shell_Count"))
            current_shell_count = PlayerPrefs.GetInt("Current_Shell_Count");
        else
            current_shell_count = shotgunShellCount;

        if (shotgunText != null)
            shotgunText.text = "Shells: " + current_shell_count + "/" + shotgunShellCount;
    }

    public void Shoot()
    {
        if (current_shell_count <= 0)
            return;
        else
            current_shell_count--;
        
        if (shotgunText != null)
            shotgunText.text = "Shells: " + current_shell_count;

        PlayerPrefs.SetInt("Current_Shell_Count", current_shell_count);
    }

    public void PurchaseShell(int cost_shell)
    {
        if (current_shell_count > 3)
            return;
        else
        {
            var money = PlayerPrefs.GetInt("Result_Money");
            var shell_count = PlayerPrefs.GetInt("Current_Shell_Count");

            if (money < cost_shell)
                return;
            else if (shell_count > shotgunShellCount)
                return;
            else if (shell_count <= shotgunShellCount && money > cost_shell)
            {
                money -= cost_shell;
                current_shell_count += 1;
                PlayerPrefs.SetInt("Result_Money", money);
                PlayerPrefs.SetInt("Current_Shell_Count", current_shell_count);
            }
        }
    }
}
