using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerScript : MonoBehaviour
{

    [Header("Player Settings:")]
    [SerializeField, Range(0, 100)] private float playerSpeed; 
    [SerializeField] private Rigidbody rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        var hor = Input.GetAxisRaw("Horizontal");
        var movement = hor * playerSpeed * Time.fixedDeltaTime * Vector3.right;

        rb.MovePosition(rb.position + movement);
    }
}
