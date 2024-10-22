using Sirius.Audio;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 5.0f;
    public float lifeTime = 10f;
    public int damage = 25;
    [SerializeField] private ParticleSystem particles;

    private Rigidbody rb;
    
    //Delegate (nomenclature : On...), dans les <>, signature de l'objet à retourner
    public Action<GameObject> OnDeactivate;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        rb.velocity = transform.forward * projectileSpeed;
        //Destroy(this.gameObject, lifeTime);
        StartCoroutine(DeactivateCoroutine());
    }

    IEnumerator DeactivateCoroutine()
    {
        yield return new WaitForSeconds(lifeTime);
        DeactivateProjectile();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("solid"))
        {
            DeactivateProjectile();
            AudioManager.Singleton.PlayAudio("enemyTakesDamage");        //peut aussi être mis en variable
        }

        Health_Ennemy_ health = collision.GetComponent<Health_Ennemy_>();

        if (health)
        {
            health.TakeDamage(damage);
            DeactivateProjectile();
            AudioManager.Singleton.PlayAudio("enemyTakesDamage");        //peut aussi être mis en variable
        }
    }

    private void DeactivateProjectile()
    {
        
        
        GameObject go = Instantiate(particles.gameObject);
        go.transform.position = this.transform.position;
        Destroy(go, 2);

        //au lieu de détruire on désactive
        this.gameObject.SetActive(false);

        //le ? check si c'est nul
        OnDeactivate?.Invoke(this.gameObject);
    }
}


