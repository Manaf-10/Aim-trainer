using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveSide = Input.GetAxisRaw("Horizontal");
        float moveForward = Input.GetAxisRaw("Vertical");

        CheckGrounded();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        float moveSide = Input.GetAxisRaw("Horizontal");
        float moveForward = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = transform.forward * moveForward + transform.right * moveSide;
        
        rb.linearVelocity = new Vector3(moveDir.x * moveSpeed, rb.linearVelocity.y, moveDir.z * moveSpeed);
    }

    void CheckGrounded()
    {
    isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 0.3f);
    }
}
