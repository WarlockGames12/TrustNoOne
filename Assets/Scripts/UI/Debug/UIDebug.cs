using UnityEngine;
using UnityEngine.EventSystems;

public class UIDebug : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("POINTER ENTER: " + gameObject.name);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("POINTER DOWN: " + gameObject.name);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("POINTER CLICK: " + gameObject.name);
    }
}
