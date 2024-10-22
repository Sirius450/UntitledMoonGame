using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyReference : MonoBehaviour
{
    public NavMeshAgent agent;

    public float pathUpdateDelay = 0.2f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    //Finate State Machine à regarder
}
