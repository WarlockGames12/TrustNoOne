using UnityEngine;

public class PurchaseTurretBullets : MonoBehaviour
{
    public void PurchaseTurret(int cost)
    {
        var money = PlayerPrefs.GetInt("Result_Money");
        if (!PlayerPrefs.HasKey("Has_Shot"))
            return;
        if (money < cost)
            return;
        money -= cost;
        PlayerPrefs.DeleteKey("Has_Shot");
        PlayerPrefs.SetInt("Result_Money", money);
    }
}
