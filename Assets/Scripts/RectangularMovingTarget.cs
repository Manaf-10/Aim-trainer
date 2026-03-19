using UnityEngine;
using System.Collections;

public class RectangularMovingTarget : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 3f;
    public float changeDirectionDelay = 2f;

    [Header("Room Boundaries")]
    public float xRange = 1f; 
    public float zRange = 1f;
    private Vector3 startPosition;
    private Vector3 targetDirection;

    void Start()
    {
        startPosition = transform.position;
        StartCoroutine(PickNewDirection());
    }

    void Update()
{
    Vector3 step = targetDirection * speed * Time.deltaTime;
    Vector3 nextLocalPos = transform.localPosition + step;

    float clampedX = Mathf.Clamp(nextLocalPos.x, -xRange, xRange);
    float clampedZ = Mathf.Clamp(nextLocalPos.z, -zRange, zRange);

    transform.localPosition = new Vector3(clampedX, transform.localPosition.y, clampedZ);

    if (nextLocalPos.x != clampedX || nextLocalPos.z != clampedZ)
    {
        targetDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }
}

    IEnumerator PickNewDirection()
    {
        while (true)
        {
            float randomX = Random.Range(-1f, 1f);
            float randomZ = Random.Range(-1f, 1f);
            targetDirection = new Vector3(randomX, 0, randomZ).normalized;

            yield return new WaitForSeconds(changeDirectionDelay);
        }
    }
}