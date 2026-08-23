using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{

    [Header("Camera Settings:")]
    [SerializeField] private Transform target;
    [SerializeField, Range(0, 100)] private float speed;
    [SerializeField] private Vector3 camOffset;

    // Update is called once per frame
    private void LateUpdate() => transform.position = Vector3.Lerp(transform.position, target.position + camOffset, speed * Time.deltaTime);
}
