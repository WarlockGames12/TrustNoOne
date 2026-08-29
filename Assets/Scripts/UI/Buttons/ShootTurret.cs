using UnityEngine;

public class ShootTurret : MonoBehaviour
{
    public void ShootingTurret() => PlayerPrefs.SetInt("Has_Shot", 1);
}
