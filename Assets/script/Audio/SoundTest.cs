using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirius.Audio;

public class SoundTest : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Play fireball
            AudioManager.Singleton.PlayAudio("fireball");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //Play iceball
            AudioManager.Singleton.PlayAudio("iceball");
        }
    }
}
