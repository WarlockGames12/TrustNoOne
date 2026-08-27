using UnityEngine;

public class CamFnafMovement : MonoBehaviour
{

    public enum PointedLayer 
    {   None, 
        Shotgun, 
        Valve   
    }

    [Header("References:")]
    [SerializeField] private DialogueSystem dialogue;

    [Header("Camera Movement Settings:")]
    [SerializeField, Range(0, 100)] private float cameraRotationSpeed;
    [SerializeField, Range(0, 180)] private float horLookLimit, verLookLimit;

    [Header("Shotgun Or Valve")]
    [SerializeField] private LayerMask shotgunMask;
    [SerializeField] private LayerMask valveMask;

    [Header("Raycast Settings:")]
    [SerializeField] private Camera raycastCam;
    [SerializeField, Range(0, 100)] private float rayDistance;

    [Header("Start Event Life Or Death:")]
    [SerializeField] private EventPlayer[] eventPlayer;

    // private variables
    private float start_x;
    private float start_y;

    private PointedLayer current_pointed_layer = PointedLayer.None;
    private RaycastHit cur_hit;
    public PointedLayer CurrentPointedLayer => current_pointed_layer;

    private void Start()
    {
        var rot = transform.localEulerAngles;

        start_x = rot.x;
        start_y = rot.y;

        if (raycastCam == null)
            raycastCam = Camera.main;
    }

    private void Update()
    {
        if (dialogue == null || dialogue.gameObject.activeSelf)
            return;

        var is_pointing_at_something = UpdatePointLayer();
        if (is_pointing_at_something && Input.GetMouseButtonDown(0))
            HandleClick();
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

    private bool UpdatePointLayer()
    {
        current_pointed_layer = PointedLayer.None;
        var ray = raycastCam.ScreenPointToRay(Input.mousePosition);
        var combined_mask = shotgunMask | valveMask;

        if (Physics.Raycast(ray, out cur_hit, rayDistance, combined_mask))
        {
            var hit_layer_bit = 1 << cur_hit.collider.gameObject.layer;

            if ((shotgunMask.value & hit_layer_bit) != 0)
                current_pointed_layer = PointedLayer.Shotgun;
            else if ((valveMask.value & hit_layer_bit) != 0)
                current_pointed_layer = PointedLayer.Valve;

            return true;
        }

        return false;
    }

    private void HandleClick()
    {
        switch (current_pointed_layer)
        {
            case PointedLayer.Shotgun:
                Debug.Log("Play Shotgun Event");
                eventPlayer[0].PlayEvent();
                break;
            case PointedLayer.Valve:
                eventPlayer[1].PlayEvent();
                break;
        }
    }
}
