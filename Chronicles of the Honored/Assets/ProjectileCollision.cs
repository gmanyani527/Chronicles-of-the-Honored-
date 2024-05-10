using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    [SerializeField]
    private int damage = 1; // Damage inflicted by the projectile

    private void OnCollisionEnter(Collision collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

        if (enemyHealth != null) // If the projectile hits an enemy
        {
            enemyHealth.TakeDamage(damage); // Apply damage
            Destroy(gameObject); // Destroy the projectile after collision
        }
    }
}