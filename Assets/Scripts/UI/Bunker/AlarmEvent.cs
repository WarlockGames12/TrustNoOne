using UnityEngine;
using UnityEngine.UI;

public class AlarmEvent : MonoBehaviour
{

    [Header("Alarm Settings:")]
    [SerializeField] private AudioSource alarmSource;
    [SerializeField, Range(0, 100)] private int timer;
    [SerializeField] private Text timer_text;

    [Header("Game Over Settings:")]
    [SerializeField] private AudioSource gameOverSound;
    [SerializeField] private GameObject gameOver;

    [Header("Win Condition Shift:")]
    [SerializeField] private GameObject winCondition;
    [SerializeField] private GameObject endGame;

    public bool alarm_enabled;
    private float time_left;
    private int shift;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        if (PlayerPrefs.HasKey("Shift_Day"))
            shift = PlayerPrefs.GetInt("Shift_Day");
        if (PlayerPrefs.HasKey("Has_Save") && PlayerPrefs.HasKey("Alive_Robots"))
        {
            alarm_enabled = true;
            alarmSource.Play();

            var alive_robots = PlayerPrefs.GetInt("Alive_Robots");
            var calculate = timer / alive_robots;

            time_left = calculate;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (alarm_enabled)
        {
            time_left -= Time.deltaTime;

            if (timer_text != null)
                timer_text.text = Mathf.CeilToInt(time_left).ToString();

            if (time_left <= 0f)
            {
                time_left = 0f;
                alarmSource.Stop();
                gameOver.SetActive(true);
            }
        }
    }

    public void DisableAlarm()
    {
        if (shift >= 3)
        {
            alarm_enabled = false;
            PlayerPrefs.DeleteKey("Alive_Robots");
            endGame.SetActive(true);
        }
        else if (shift < 3)
        {
            alarm_enabled = false;
            winCondition.SetActive(true);
        }
    }
}
