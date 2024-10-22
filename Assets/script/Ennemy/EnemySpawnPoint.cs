using Sirius.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject EnemyPrefad;
    [SerializeField] private Transform SpawnPoint;


    private float x;
    private float z;

    public int maxEnemy = 5;
    public int currentEnemyCount = 0;
    public float intervalSpawn = 3f;
    public int range = 5;
    public bool PlayerInZone = false;
    public Vector3 newSpawnPoint;

    private void Update()
    {

        currentEnemyCount = Enemy.enemyList.Count;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {

            if (currentEnemyCount < maxEnemy)
            {
                StartCoroutine(SpawnEnemy());
            }
            PlayerInZone = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerInZone = false;
        }
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(intervalSpawn);

        if (Enemy.enemyList.Count < maxEnemy)
        {
            newSpawnPoint = new Vector3(Random.Range(SpawnPoint.position.x - range, SpawnPoint.position.x + range),
                                       SpawnPoint.position.y,
                                       Random.Range(SpawnPoint.position.z - range, SpawnPoint.position.z + range));
          
            Instantiate(EnemyPrefad, newSpawnPoint, Quaternion.identity);

            StartCoroutine(SpawnEnemy());
        }

           
    }


}
