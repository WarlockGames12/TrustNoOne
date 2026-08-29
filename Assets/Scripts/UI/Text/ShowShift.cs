using UnityEngine;
using UnityEngine.UI;

public class ShowShift : MonoBehaviour
{
    [Header("Shift Text Settings:")]
    [SerializeField] private Text text;
    private int shift_day = 1;

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("Shift_Day"))
            shift_day += PlayerPrefs.GetInt("Shift_Day");
    }

    // Update is called once per frame
    private void Update()
    {
        text.text = "Shift:" + shift_day;
    }
}
