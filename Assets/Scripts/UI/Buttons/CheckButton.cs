using UnityEngine;

public class CheckButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        if (PlayerPrefs.HasKey("Has_Shot"))
            gameObject.SetActive(false);
    }
}
