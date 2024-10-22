using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform spawnPoint;

    public float spawnInterval = 1f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // If the timer exceeds the spawn interval, spawn a projectile
        if (timer >= spawnInterval)
        {
            //APPELER FONCTION DE CRÉATION PROJECTILE!
            timer = 0f; // Reset the timer
        }
    }
}


