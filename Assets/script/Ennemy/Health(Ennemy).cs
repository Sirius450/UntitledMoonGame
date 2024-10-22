using Sirius.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health_Ennemy_ : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private GameObject Drop;
    [SerializeField] private Transform DropPoint;
    [SerializeField] private ParticleSystem Blood;
    bool withChildren = true;

    [SerializeField] UnityEvent OnTakeDamege;
    [SerializeField] UnityEvent OnGainHealth;

    private int currentHealth;
    public int CurrentHelath => currentHealth;
    Vector3 dir = Vector3.zero;

    void Awake()
    {
        //set le max de point de vie
        currentHealth = maxHealth;

        Blood.Stop();

    }

    public void TakeDamage(int damage)
    {
        //retir le nombre de point de vie
        currentHealth -= damage;
        AudioManager.Singleton.PlayAudio("enemyTakesDamage");

        //joue un effet de particul
        Blood.Play();

        //Clamp le nombre de point de vie pour pas aller endesous de 0
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        //log pour voir si tout fonctione
        //Debug.Log(this.gameObject.name + " took " + damage + " damage  pv : " + currentHealth);

        //si plus de point de vie
        if (currentHealth <= 0)
        {
            //detriur l'objet et appeler le script de camera shake
            //CameraController.Singleton.ChangeCible(this.transform);
            //Destroy(this.gameObject); //peut - être bénéfique pour la performance
            //TimeManager.Singleton.TimeSlow(0.1f, 2);
            //gameObject.SetActive(false);
            //si c'est un ennemie

            if (this.gameObject.CompareTag("Enemy"))
            {
                AudioManager.Singleton.PlayAudio("enemyTakesDamage");
                Destroy(this.gameObject);
                //Debug.Log("DIE");
            }
            Debug.Log("mate your dead");

            //si l'objet peut droper un truc
            if (gameObject.tag == "Enemy")
            {
                Instantiate(Drop, DropPoint.position, Quaternion.Euler(dir));
            }

        }


        OnTakeDamege?.Invoke();
    }

    public void GainHealth(int heal)
    {
        currentHealth += heal;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        OnGainHealth?.Invoke();
    }
}
