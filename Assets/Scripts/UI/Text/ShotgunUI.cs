using UnityEngine;
using UnityEngine.UI;

public class ShotgunUI : MonoBehaviour
{

    [Header("Shotgun Settings:")]
    [SerializeField] private Text shotgunText;
    [SerializeField, Range(0, 5)] private int shotgunShellCount;

    [Header("Money Settings:")]
    [SerializeField] private bool needMoney;
    [SerializeField] private Text moneyText;
    [SerializeField] private bool isMenu;

    public int current_shell_count;
    private int money;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        if (needMoney)
            money = PlayerPrefs.GetInt("Result_Money");

        if (PlayerPrefs.HasKey("Current_Shell_Count"))
            current_shell_count = PlayerPrefs.GetInt("Current_Shell_Count");
        else
            current_shell_count = shotgunShellCount;

        if (shotgunText != null)
            shotgunText.text = "Shells: " + current_shell_count + "/" + shotgunShellCount;
    }

    private void Update()
    {
        if (needMoney && PlayerPrefs.HasKey("Result_Money"))
        {
            money = PlayerPrefs.GetInt("Result_Money");
            moneyText.text = "$" + money;
        }

        if (isMenu)
            shotgunText.text = "Shells: " + current_shell_count + "/" + shotgunShellCount;
    }

    public void Shoot()
    {
        if (current_shell_count <= 0)
            return;
        else
            current_shell_count--;
        
        if (shotgunText != null)
            shotgunText.text = "Shells: " + current_shell_count + "/" + shotgunShellCount;

        PlayerPrefs.SetInt("Current_Shell_Count", current_shell_count);
    }

    public void PurchaseShell(int cost_shell)
    {
        if (current_shell_count > 3)
            return;
        else
        {
            money = PlayerPrefs.GetInt("Result_Money");
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
