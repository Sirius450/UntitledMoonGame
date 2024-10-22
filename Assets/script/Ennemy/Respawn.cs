using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    //Variable Serialized
    [SerializeField] private GameObject Dummy;
    [SerializeField] private Transform RespawnPoint;
    [SerializeField] EnemySpawnPoint spawn;
    //varible

    private void OnDestroy()
    {
        //spawn.OnEnemyDestroy();
    }

    private void OnTriggerEnter(Collider collision)
    {
        //EnemySpawnPoint = collision.GetComponent<EnemySpawnPoint>();
        //EnemySpawnPoint.OnEnemyDestroy();
    }

}
