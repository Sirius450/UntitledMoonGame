using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    [SerializeField] Enemy EnemyScript;
    [SerializeField]float dist = 0;

    [Header("Attack Parameters")]
    [SerializeField] private float attackDistance;
    [SerializeField] private float attackSpeed;

    [Header("Chase Parameters")]
    [SerializeField] private float chaseDistance;
    [SerializeField] private float chaseSpeed;

    [Header("Patrol Parameters")]
    [SerializeField] private float patrolDistance;
    [SerializeField] private float patrolSpeed;
    private Vector3 startingPoint;
    

    [SerializeField]private EnemyState currentState /*= EnemyState.IDLE*/;

    [SerializeField] Transform player;      //peut-être utiliser un FindObjectOfType dans Awake pour assigner les ennemis du spawner au joueur?

    Animator animator;

    private void Start()
    {
        startingPoint = this.transform.position;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        dist = Vector2.Distance(this.transform.position, player.position);
        transform.LookAt(player.position);


        if(dist >= 0 && dist < attackDistance)
        { 
            Attack();
            currentState = EnemyState.ATTACK;
            //Debug.Log("atk");
        }
        else if(dist > attackDistance &&  dist < chaseDistance)
        { 
            Chase(); 
            currentState = EnemyState.CHASE;
            //Debug.Log("Chase");
        }
        else if(dist >  chaseDistance && dist < patrolDistance)
        { 
            Patrol();
            currentState = EnemyState.PATROL;
            //Debug.Log("Patrol");
        }
        else
        { 
            Idle(); 
            currentState = EnemyState.IDLE;
            //Debug.Log("idle");
        }

        GetState();
    }

    public EnemyState GetState()
    {
        return currentState;
    }

    //pour la gestion plus efficace des animations
    private void TransitionTo(EnemyState nextState)
    {
        switch (nextState)
        {
            case EnemyState.IDLE:
                //change vers l'animation Idle
                animator.Play("IDLE");
                currentState = EnemyState.IDLE;
                break;

            case EnemyState.PATROL:
                //change vers l'animation Patrol
                animator.Play("IDLE");
                currentState = EnemyState.PATROL;
                break;

            case EnemyState.CHASE:
                //change vers l'animation Chase
                animator.Play("CHASE");
                currentState = EnemyState.CHASE;
                break;

            case EnemyState.ATTACK:
                //change vers l'animation Attack
                animator.Play("ATTACK");
                currentState = EnemyState.ATTACK;
                break;
        }
    }

    private void Idle()
    {

    }

    private void Patrol()
    {
        this.transform.position = Vector3.MoveTowards(
            this.transform.position, 
            startingPoint, 
            patrolSpeed * Time.deltaTime);
    }

    private void Chase()
    {
        this.transform.position = Vector3.MoveTowards(
            this.transform.position,
            player.position,
            chaseSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        EnemyScript.OnAttack();
    }
    

    private void OnDrawGizmos()
    {
        //Dessiner le rayon du wire
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, chaseDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, attackDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, patrolDistance);
    }
}

public enum EnemyState
{
    IDLE,
    PATROL,
    CHASE,
    ATTACK
}
