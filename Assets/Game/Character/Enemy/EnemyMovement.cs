using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    NavMeshAgent navMeshAgent;
    GameObject player;
    public float detectionDistance = 30f;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    [HideInInspector]
    public bool playerDetected = false;
    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < detectionDistance){
            playerDetected = true;
        }
        if (playerDetected)
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
    }
}
