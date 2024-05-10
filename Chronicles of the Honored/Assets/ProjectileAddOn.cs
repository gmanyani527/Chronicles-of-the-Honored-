using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAddOn : MonoBehaviour
{


    private Rigidbody rb;

    private bool targetHit;
    [SerializeField]
    private LayerMask stickableLayers; // Layers to which the projectile should stick

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Ensure it only sticks to the first target
        if (targetHit)
            return;

        // Check if the collision is with a stickable layer
        if ((stickableLayers.value & (1 << collision.gameObject.layer)) > 0)
        {
            targetHit = true;

            // Make sure the projectile sticks to the surface
            rb.isKinematic = true;

            // Make sure the projectile moves with the target
            transform.SetParent(collision.transform);
        }
    }
}
