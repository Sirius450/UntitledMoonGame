using Sirius.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portail : MonoBehaviour
{
    [SerializeField] ParticleSystem Rune;
    [SerializeField] ParticleSystem Particul;
    [SerializeField] SceneSwitcher SceneSwitcher;
    [SerializeField] private int SceneIndex;
    [SerializeField] private int timeToTP = 5;

    private void Start()
    {
        Rune.Play();
        Particul.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Singleton.PlayAudio("portail");
            Particul.Play();
            StartCoroutine(Teleportation());
        }
    }

    private void OnTriggerExit(Collider other)
    {

        {
            if (other.CompareTag("Player"))
            {
                Particul.Stop();
                StopCoroutine(Teleportation());
            }
        }
    }


    private IEnumerator Teleportation()
    {
        yield return new WaitForSeconds(timeToTP);
        SceneSwitcher.SwitchScene(SceneIndex);

    }



}
