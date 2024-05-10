using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;



public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;

    // Start is called before the first frame update
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    public MenuController menuController;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();

    }

    private void FixedUpdate()
    {

        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);

    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();

        }
    }


    void OnMove(InputValue movementValue)
    {




        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    void SetCountText()
    {
        // Update the count text with the current count.
        countText.text = "Count: " + count.ToString();

        // Check if the count has reached or exceeded the win condition.
        if (count >= 12)
        {
            // Display the win text.
            //winTextObject.SetActive(true);

            menuController.WinGame();
        }
    }

    void EndGame()
    {
        menuController.LoserGame();
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EndGame();
        }
    }
}
