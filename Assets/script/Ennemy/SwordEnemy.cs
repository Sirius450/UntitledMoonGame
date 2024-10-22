using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemy : MonoBehaviour
{
    public int damage = 25;

    private void OnTriggerEnter(Collider collision)
    {
        Health health = collision.GetComponent<Health>();

        if (health)
        {
            health.TakeDamage(damage);
            
        }
    }
}
