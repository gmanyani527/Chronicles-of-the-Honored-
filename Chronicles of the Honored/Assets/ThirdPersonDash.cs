using UnityEngine;
using System.Collections;

public class DashController : MonoBehaviour
{
    // Serialized fields for dash settings
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeField] private KeyCode dashKey = KeyCode.E;

    // Reference to the Player script
    private Player player;

    private void Start()
    {
        // Get the Player script component attached to the same GameObject
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(dashKey))
        {
            if (player != null)
            {
                // Start the Dash coroutine on the Player with the configured parameters
                player.StartCoroutine(player.Dash(dashSpeed, dashDuration));
            }
            else
            {
                Debug.LogWarning("Player reference is missing.");
            }
        }
    }
}