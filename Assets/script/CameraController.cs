using Sirius.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NON LIÉ AU JEU, EFFECTUER L'EFFET DANS CINEMACHINE

public class CameraController : MonoBehaviour
{
    public static CameraController Singleton;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    [SerializeField] private Transform cible;

    private void Update()
    {
        Vector3 targetPosition = this.transform.position;

        //transition de notre position à la position de l'ennemi en 1 seconde
        targetPosition = Vector3.Lerp(targetPosition, cible.transform.position, 2f * Time.deltaTime);

        targetPosition.z = -10;

        this.transform.position = targetPosition;
    }

    public void ChangeCible(Transform newCible)
    {
        cible = newCible;
    }
}
