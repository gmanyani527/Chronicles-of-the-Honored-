using UnityEngine;

public class ProjectileStick : MonoBehaviour
{
    private Rigidbody rb; // Rigidbody component
    private bool targetHit = false; // Ensure it sticks only once
    [SerializeField]
    private LayerMask stickableLayers; // Layers to which the projectile should stick

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Initialize the Rigidbody
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous; // For accurate collisions
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the projectile has already hit something, exit early
        if (targetHit)
            return;

        // Check if the collision is with a stickable layer
        if ((stickableLayers.value & (1 << collision.gameObject.layer)) > 0)
        {
            targetHit = true; // Set the hit flag to true

            // Stop physics interactions by setting isKinematic
            rb.isKinematic = true;

            // Stick to the collided object
            transform.SetParent(collision.transform); // Attach to the target

            // Optionally adjust position and rotation to ensure a clean attachment
            AdjustPosition(collision);

            // Apply damage to the enemy if it has the EnemyHealth component
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(1); // Apply damage
            }
        }
    }

    // Function to adjust the projectile's position and orientation
    private void AdjustPosition(Collision collision)
    {
        ContactPoint contact = collision.contacts[0]; // Get the contact point
        transform.position = contact.point; // Set the position to the contact point
        transform.rotation = Quaternion.LookRotation(contact.normal); // Align with the normal
    }
}