using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Throwing : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;

    [Header("Settings")]
    public int totalThrows = 5; // Number of throws available
    public float throwCoolDown = 1f; // Cooldown between throws
    public float throwForce = 20f; // Force applied to throw
    public float throwUpwardForce = 5f; // Upward force applied to throw
    public float timeToLive = 10f; // Time before the object is destroyed

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Q;

    private bool readyToThrow;

    void Start()
    {
        readyToThrow = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0)
        {
            Throw();
        }
    }

    private void Throw()
    {
        readyToThrow = false;

        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;
        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        // Destroy the projectile after a set time (TTL)
        Destroy(projectile, timeToLive);

        totalThrows--;

        Invoke(nameof(ResetThrow), throwCoolDown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
}
