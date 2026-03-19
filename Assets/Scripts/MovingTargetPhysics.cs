using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody rb;
    private Vector3 currentDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        rb.useGravity = false;
        
        PickNewDirection();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = currentDirection * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            PickNewDirection();
        }
    }

    void PickNewDirection()
    {
        float rx = Random.Range(-1f, 1f);
        float rz = Random.Range(-1f, 1f);
        currentDirection = new Vector3(rx, 0, rz).normalized;
    }
}