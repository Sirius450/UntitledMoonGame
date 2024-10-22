using Sirius.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] public int maxHealth = 100;
    [SerializeField] private GameObject Drop;
    [SerializeField] private Transform DropPoint;
    [SerializeField] private ParticleSystem Blood;
    bool withChildren = true;

    [SerializeField] UnityEvent OnTakeDamege;
    [SerializeField] UnityEvent OnGainHealth;
    [SerializeField] UnityEvent OnDead;

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
        Debug.Log(this.gameObject.name + " took " + damage + " damage  pv : " + currentHealth);

        //si plus de point de vie
        if (currentHealth <= 0)
        {
            //detriur l'objet et appeler le script de camera shake
            //CameraController.Singleton.ChangeCible(this.transform);
            //Destroy(this.gameObject); //peut - être bénéfique pour la performance
            //TimeManager.Singleton.TimeSlow(0.1f, 2);
            //gameObject.SetActive(false);
            //si c'est un ennemie

            //le temps stop lorsque le joueur meurt
            Time.timeScale = 0f;

            //invoke event de la mort
            OnDead?.Invoke();
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

 