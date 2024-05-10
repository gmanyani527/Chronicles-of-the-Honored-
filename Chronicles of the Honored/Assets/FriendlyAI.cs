using UnityEngine;
using UnityEngine.Events;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // Reference to the player
    public Vector3 offset = new Vector3(0, 3, -6); // Offset from the player
    public float followSpeed = 5f; // Speed of following
    public float interactionRadius = 2f;

    public UnityEvent OnPlayerHealed; // Event to trigger healing
    private Rigidbody rb;

    public UnityEngine.Events.UnityEvent OnDialogueTrigger;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // Make the Rigidbody kinematic
        }
        // Ensure the event is not null
        if (OnPlayerHealed == null)
        {
            OnPlayerHealed = new UnityEvent();
        }
    }

    void Update()
    {
        
        
            // Follow the player with a fixed offset
            Vector3 targetPosition = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

            // Make sure the ally looks at the player
            transform.LookAt(player);
        
    }


    // Method to trigger healing
    public void HealPlayer(int healAmount)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.HealPlayer(healAmount); // Heal the player with the specified amount
        }
    }
}