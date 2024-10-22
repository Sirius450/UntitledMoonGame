using Sirius.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSouls : MonoBehaviour
{
    public int NombreDeSoulsDropper = 10;

    private void OnTriggerEnter(Collider collision)
    {
        SoulCatcher Souls = collision.GetComponent<SoulCatcher>();

        if (Souls)
        {
            Souls.GainSouls(NombreDeSoulsDropper);
            AudioManager.Singleton.PlayAudio("soulPickUp");
            Destroy(this.gameObject);
        }
    }

}
