using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RaycastDebugger : MonoBehaviour
{
    void Update()
    {
        if (EventSystem.current == null)
            return;

        PointerEventData data = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(data, results);

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("=== UI RAYCAST RESULTS ===");

            foreach (var result in results)
            {
                Debug.Log(
                    $"{result.gameObject.name} | " +
                    $"depth={result.depth} | " +
                    $"module={result.module.GetType().Name}"
                );
            }
        }
    }
}
