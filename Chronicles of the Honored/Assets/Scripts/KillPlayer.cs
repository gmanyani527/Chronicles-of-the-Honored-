using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public Vector3 respawnPosition = Vector3.zero; // Store the respawn position

    // OnTriggerEnter is called when the Collider other enters the trigger.
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Reset player position to the respawn position
            other.transform.position = respawnPosition;
        }
    }
}