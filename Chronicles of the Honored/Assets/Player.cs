using UnityEngine;
using TMPro;
using System.Collections;

public class Player : MonoBehaviour
{
    // Essential components
    public Animator playerAnim;
    public Rigidbody playerRigid;
    public Transform playerTrans;
    public TextMeshProUGUI countText;
    public GameObject exitDoor;

    // Movement parameters
    public float walkSpeed = 6f;  // Walking speed
    public float backwardSpeed = 4f; // Backward speed
    public float runIncrement = 4f; // Running speed increment
    public float rotationSpeed = 90f; // Rotation speed
    public float dashSpeed = 20f; // Speed during dash
    public float dashDuration = 0.5f; // Duration of the dash

    // Miscellaneous state variables
    public int count = 0;
    public bool walking = false;

    // Reference to MenuController to manage game states
    public MenuController menuController;

    private void Awake()
    {
        playerAnim = GetComponentInChildren<Animator>();

        if (playerAnim == null)
        {
            Debug.LogError("Animator component not found on Player or its children. Please add an Animator.");
        }
        playerRigid = GetComponent<Rigidbody>();

        playerTrans = transform;
    }

    private void Start()
    {
        SetCountText(); // Initializes the count text
        exitDoor.SetActive(true); // Ensure the exit door is active
        // Set default position
        /* transform.position = new Vector3(2, 1, (float)-24.294);*/

        // Ensure position doesn't overlap with other structures
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f);
        if (colliders.Length > 1) // If overlapping with other colliders, adjust position
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
        }
    }

    private void FixedUpdate()
    {
        float currentSpeed = walkSpeed; // Default walking speed

        if (Input.GetKey(KeyCode.W))
        {
            playerRigid.velocity = transform.forward * currentSpeed * Time.deltaTime; // Forward movement
        }

        if (Input.GetKey(KeyCode.S))
        {
            playerRigid.velocity = -transform.forward * backwardSpeed * Time.deltaTime; // Backward movement
        }
    }

    private void Update()
    {
        if (playerAnim == null) return;

        // Handle walking forward
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerAnim.SetTrigger("walk");
            walking = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            playerAnim.ResetTrigger("walk");
            playerAnim.SetTrigger("idle");
            walking = false;
        }

        // Handle walking backward
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerAnim.SetTrigger("walkback");
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            playerAnim.ResetTrigger("walkback");
            playerAnim.SetTrigger("idle");
        }

        // Handle running
        if (walking && Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerAnim.SetTrigger("run");
            walkSpeed += runIncrement; // Increase speed for running
        }
        if (walking && Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerAnim.ResetTrigger("run");
            walkSpeed = 6f; // Reset speed to original walking speed
        }

        // Handle dash with "E" key
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Dash(dashSpeed, dashDuration)); // Start dash coroutine
        }

        // Handle rotation
        if (Input.GetKey(KeyCode.A))
        {
            playerTrans.Rotate(0, -rotationSpeed * Time.deltaTime, 0); // Rotate left
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerTrans.Rotate(0, rotationSpeed * Time.deltaTime, 0); // Rotate right
        }
    }

    // Coroutine to handle dash functionality
    public IEnumerator Dash(float dashSpeed, float dashDuration)
    {
        float startTime = Time.time;
        float endTime = startTime + dashDuration;

        while (Time.time < endTime)
        {
            float dashDistance = dashSpeed * Time.deltaTime;
            playerRigid.MovePosition(transform.position + transform.forward * dashDistance);
            yield return null;
        }
    }

   /* private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            menuController.LoserGame();
            gameObject.SetActive(false); // Deactivate player GameObject
        }
    } */

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();

            if (count >= 4)
            {
                exitDoor.SetActive(false); // Deactivate exit door if count is 4 or higher
            }
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 7)
        {
            menuController.WinGame(); // Handle winning scenario
        }
    }
}