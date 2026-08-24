using UnityEngine;

public class EventTarget : MonoBehaviour
{
    [Header("Event Target Settings:")]
    [SerializeField] private string id;

    public string ID => id;
}
