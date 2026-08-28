using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverScript : MonoBehaviour, IPointerEnterHandler
{

    [Header("Hover Button Settings:")]
    [SerializeField] private AudioSource hoverSound;

    public void OnPointerEnter(PointerEventData eventData) => hoverSound.Play();
}
