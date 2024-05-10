using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    public Transform[] waypoints = new Transform[3]; // Waypoints for patrolling
    public float patrolSpeed = 3f; // Speed for patrolling
    public float waitTime = 2f; // Time to wait at each waypoint
    public Transform player; // Player to detect
    public float detectionRadius = 10f; // Radius to start chasing
    public float returnRadius = 12f; // Radius to return to patrol

    private int currentWaypointIndex = 0; // Index of the current waypoint
    private Rigidbody rb; // Rigidbody for physics-based movement
    private bool isChasing = false; // Whether the enemy is currently chasing
    private float waitTimer = 0f; // Timer for waiting at waypoints

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Initialize Rigidbody
        GoToWaypoint(currentWaypointIndex); // Start at the first waypoint
    }

    void Update()
    {
        if (isChasing)
        {
            ChasePlayer(); // Chase if in chase mode
            CheckReturnToPatrol(); // Check if the player has left the detection radius
        }
        else
        {
            Patrol(); // Patrol if not chasing
            CheckForPlayer(); // Check if the player is detected to start chasing
        }
    }

    void GoToWaypoint(int index)
    {
        if (waypoints.Length == 0) 
        { return; }
        else
        { // Ensure there are waypoints

            currentWaypointIndex = index % waypoints.Length; // Ensure the index stays within bounds
            waitTimer = waitTime; // Reset the wait timer 
        }
    }

    void Patrol()
    {
        Transform targetWaypoint = waypoints[currentWaypointIndex]; // Current waypoint
        Vector3 direction = (targetWaypoint.position - transform.position).normalized; // Direction towards the waypoint
        rb.velocity = direction * patrolSpeed; // Apply patrol speed

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f) // Close to the waypoint
        {
            waitTimer -= Time.deltaTime; // Start the wait timer

            if (waitTimer <= 0) // If the wait timer has elapsed
            {
                GoToWaypoint(currentWaypointIndex + 1); // Move to the next waypoint
            }
            else
            {
                rb.velocity = Vector3.zero; // Stop while waiting
            }
        }
    }

    void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized; // Direction to the player
        rb.velocity = direction * patrolSpeed * 2; // Chase speed
        transform.LookAt(player); // Face the player
    }

    void CheckForPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius) // If the player is within detection radius
        {
            isChasing = true; // Start chasing
        }
    }

    void CheckReturnToPatrol()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > returnRadius) // If the player has left the return radius
        {
            isChasing = false; // Return to patrol mode
        }
    }
}