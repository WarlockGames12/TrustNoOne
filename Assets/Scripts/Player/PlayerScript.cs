using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerScript : MonoBehaviour
{

    [Header("Player Settings:")]
    [SerializeField, Range(0, 100)] private float playerSpeed; 
    [SerializeField, Range(0, 100)] private float sprintMultiplier;
    [SerializeField] private Animator playerAnim;
    [SerializeField, Range(0, 100)] private float rotationMovement;
    [SerializeField] private Rigidbody rb;

    [Header("Player Jump Settings:")]
    [SerializeField] private AudioSource jumpSource;
    [SerializeField] private AudioClip[] jumpOrLand;
    [SerializeField, Range(0, 100)] private float jumpForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask layer;

    [Header("Player Footstep Sound Settings:")]
    [SerializeField] private AudioSource playerFootsteps;
    [SerializeField] private AudioClip[] footsteps;
    [SerializeField, Range(0, 1)] private float footstepSpeedWalk, footstepSpeedClimb;

    [Header("Player Ladder Settings:")]
    [SerializeField] private LayerMask ladderMask;

    // private variables
    private bool is_sprinting;
    private float footstep_timer;

    private bool can_interact;
    private bool was_grounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        var on_ladder = OnLadder();
        var grounded = IsGrounded();
        var hor = Input.GetAxisRaw("Horizontal");

        UpdateFacingDirection(hor);

        is_sprinting = Input.GetKey(KeyCode.LeftShift);
        var speed = playerSpeed * (is_sprinting ? sprintMultiplier : 1);

        float animSpeed = 0f;

        if (Mathf.Abs(hor) > 0.01f)
            animSpeed = is_sprinting ? 2f : 1f;

        playerAnim.SetFloat("Speed", animSpeed);

        var movement = hor * speed * Time.fixedDeltaTime * Vector3.right;
        if (on_ladder)
        {
            var ver = Input.GetAxisRaw("Vertical");
            movement += ver * playerSpeed * Time.fixedDeltaTime * Vector3.up;

            playerAnim.SetBool("IsFalling", true);
            playerAnim.speed = Mathf.Abs(ver);
        }
        else 
        {
            playerAnim.SetBool("IsClimbing", false);
            playerAnim.speed = 1f;
        }
        

        rb.MovePosition(rb.position + movement);
        rb.useGravity = !on_ladder;

        Footsteps(on_ladder);

        // Check for falling
        if (!grounded)
        {
            playerAnim.SetBool("IsFalling", true);
        }
            
        // check if landed
        if (grounded && !was_grounded)
        {
            playerAnim.SetTrigger("Landed");
            playerAnim.SetBool("IsFalling", false);
        }
        was_grounded = grounded;
    }

    private void UpdateFacingDirection(float hor_input)
    {
        var on_ladder = OnLadder();
        if (hor_input > 0f && !on_ladder)
            playerAnim.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
        else if (hor_input < 0f && !on_ladder)
            playerAnim.transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
        else if (on_ladder)
        {
            playerAnim.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    private void Footsteps(bool on_ladder)
    {
        if (playerFootsteps == null || footsteps.Length == 0)
            return;
        
        bool move;

        if (on_ladder)
            move = Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0;
        else  
            move = Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0;

        if (!move || (!on_ladder && !IsGrounded()))
        {
            footstep_timer = 0;
            return;
        }

        footstep_timer -= Time.fixedDeltaTime;
        if (footstep_timer <= 0)
        {
            playerFootsteps.pitch = Random.Range(0.75f, 1.25f);
            if (!on_ladder)
                playerFootsteps.PlayOneShot(footsteps[Random.Range(0, 1)]);
            else
                playerFootsteps.PlayOneShot(footsteps[2]);
            footstep_timer = on_ladder ? footstepSpeedClimb : footstepSpeedWalk;
        }
    }

    private void Update() => Jump();

    private void Jump()
    {  
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            jumpSource.pitch = Random.Range(0.75f, 1.25f);
            jumpSource.clip = jumpOrLand[0];
            jumpSource.Play();
            playerAnim.SetTrigger("Jump");
            
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
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
