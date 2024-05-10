using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint; // Spawn point in the scene
    [SerializeField]
    private GameObject player; // Reference to the player GameObject

    void Start()
    {
        if (spawnPoint == null)
        {
            Debug.LogError("Spawn point is not set! Please assign a spawn point in the Inspector.");
        }

        if (player == null)
        {
            Debug.LogError("Player GameObject is not set! Please assign a player GameObject.");
        }
        else
        {
            // Ensure the player GameObject has children
            Debug.Log($"Player has {player.transform.childCount} children.");

            // Move the existing player to the spawn point
            player.transform.position = spawnPoint.position;
            player.transform.rotation = spawnPoint.rotation;
        }
    }
}