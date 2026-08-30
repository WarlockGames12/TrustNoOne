using UnityEngine;
using UnityEngine.UI;

public class ShowShift : MonoBehaviour
{
    [Header("Shift Text Settings:")]
    [SerializeField] private Text text;
    private int shift_day = 1;

    // Update is called once per frame
    private void Update()
    {
        if (!PlayerPrefs.HasKey("Shift_Day"))
            text.text = "Shift:" + shift_day;
        else
        {
            shift_day = PlayerPrefs.GetInt("Shift_Day");
            text.text = "Shift:" + shift_day;
        }
    }
}
