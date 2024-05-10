using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float lookRadius = 5f; // Detection radius
    public float movementSpeed = 2f; // Movement speed
    public float rotateSpeed = 2f; // Rotation speed

    private Transform target;
    private Rigidbody rb; // Rigidbody component (if applicable)
    private float originalY; // Store the original y-position

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();

        // If using a rigidbody, lock constraints to prevent y-axis movement
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        // Store the initial y-position
        originalY = transform.position.y;
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        // Calculate the direction from the enemy to the player
        Vector3 direction = (target.position - transform.position).normalized;

        // Ensure y-axis is zero to avoid tilting
        direction.y = 0;

        // Smoothly rotate towards the player
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        if (distance <= lookRadius)
        {
            // Move towards the player
            Vector3 move = transform.forward * movementSpeed * Time.deltaTime;
            move.y = 0; // Avoid vertical movement
            transform.position += move;

            // Maintain a constant y-position
            transform.position = new Vector3(transform.position.x, originalY, transform.position.z);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius); // Draw detection radius
    }
}