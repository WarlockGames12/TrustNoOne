using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{

    [Header("Camera Settings:")]
    [SerializeField] private Transform target;
    [SerializeField, Range(0.01f, 2f)] private float speed;
    [SerializeField] private Vector3 camOffset;

    // private variables
    private Vector3 cam_velocity;

    // Update is called once per frame
    private void LateUpdate()
    {
        // Smoothly move to player
        var cam_pos = target.position + camOffset;
        transform.position = Vector3.SmoothDamp(transform.position, cam_pos, ref cam_velocity, speed);
    }
}
