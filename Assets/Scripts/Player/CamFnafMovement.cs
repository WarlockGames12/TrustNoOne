using UnityEngine;

public class CamFnafMovement : MonoBehaviour
{

    [Header("Camera Movement Settings:")]
    [SerializeField, Range(0, 100)] private float cameraRotationSpeed;
    [SerializeField, Range(0, 180)] private float horLookLimit, verLookLimit;

    // private variables
    private float start_x;
    private float start_y;

    private void Start()
    {
        var rot = transform.localEulerAngles;

        start_x = rot.x;
        start_y = rot.y;
    }
    
    private void LateUpdate()
    {
        var mouse_x = Input.mousePosition.x / Screen.width;
        var mouse_y = Input.mousePosition.y / Screen.height;

        var hor = (mouse_x - 0.5f) * 2;
        var ver = (mouse_y - 0.5f) * 2;

        var tar_y = start_y + hor * horLookLimit;
        var tar_x = start_x - ver * verLookLimit;

        tar_y = Mathf.Clamp(tar_y, start_y + -horLookLimit, start_y + horLookLimit);
        tar_x = Mathf.Clamp(tar_x, start_x + -verLookLimit, start_x + verLookLimit);

        var target_rot = Quaternion.Euler(tar_x, tar_y, 0);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, target_rot, cameraRotationSpeed * Time.deltaTime); 
    }
}
