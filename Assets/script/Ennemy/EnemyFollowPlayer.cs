using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowPlayer : MonoBehaviour
{
    public Transform target;

    private EnemyReference enemyReferences;

    private float attackDistance;

    private float pathUpdateDeadLine;

    private GameObject cible = null;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            cible = other.gameObject;
            Debug.Log("Le player est dans la zone");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            cible = null;
            Debug.Log("Le player n'est plus dans la zone");
        }
    }

    private void Awake()
    {
        enemyReferences = GetComponent<EnemyReference>();
    }

    // Start is called before the first frame update
    void Start()
    {
        attackDistance = enemyReferences.agent.stoppingDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && cible != null)
        {
            bool inRange = Vector3.Distance(transform.position, target.position) <= attackDistance;

            if (inRange)
            {
                LookAtTarget();
            }
            else
            {
                UpdatePath();
            }
        }
    }

    private void LookAtTarget()
    {
        Vector3 lookPosition = target.position - transform.position;

        lookPosition.y = 0;

        Quaternion rotation = Quaternion.LookRotation(lookPosition);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
    }

    private void UpdatePath()
    {
        if (Time.time >= pathUpdateDeadLine)
        {
            Debug.Log("Updating Path");

            pathUpdateDeadLine = Time.time + enemyReferences.pathUpdateDelay;

            enemyReferences.agent.SetDestination(target.position);
        }
    }
}
