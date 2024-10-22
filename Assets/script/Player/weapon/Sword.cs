using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int damage = 25;

    private void OnTriggerEnter(Collider collision)
    {
        Health_Ennemy_ health = collision.GetComponent<Health_Ennemy_>();

        if (health)
        {
            health.TakeDamage(damage);
            
        }
    }
}
