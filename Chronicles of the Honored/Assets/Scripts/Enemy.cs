using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private GameObject Player;
    private NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player exists and is not destroyed
        if (Player != null)
        {
            // Set destination to player's position
            nav.SetDestination(Player.transform.position);
        }
    }
}