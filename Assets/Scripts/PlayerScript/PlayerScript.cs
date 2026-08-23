using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerScript : MonoBehaviour
{

    [Header("Player Settings:")]
    [SerializeField, Range(0, 100)] private float playerSpeed; 
    [SerializeField] private Rigidbody rb;

    [Header("Player Jump Settings:")]
    [SerializeField, Range(0, 100)] private float jumpForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask layer;

    [Header("Player Ladder Settings:")]
    [SerializeField] private LayerMask ladderMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        var on_ladder = OnLadder();

        var hor = Input.GetAxisRaw("Horizontal");
        var movement = hor * playerSpeed * Time.fixedDeltaTime * Vector3.right;

        if (on_ladder)
        {
            var ver = Input.GetAxisRaw("Vertical");
            movement += ver * playerSpeed * Time.fixedDeltaTime * Vector3.up;
        }

        rb.MovePosition(rb.position + movement);
        rb.useGravity = !on_ladder;
    }

    private void Update() => Jump();

    private void Jump()
    {  
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.2f, layer);
    }

    private bool OnLadder()
    {
        return Physics.CheckSphere(groundCheck.position, 0.2f, ladderMask, QueryTriggerInteraction.Collide);
    }
}
