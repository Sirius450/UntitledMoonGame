using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Singleton;

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

    private bool isTimeSlowed = false;
    private float timeSlowDuration;
    private float currentTime = 0;

    private void Update()
    {
        if (isTimeSlowed)
        {
            currentTime += Time.deltaTime;
                             //d'un chiffre à un chiffre (plutôt qu'un vector3)
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1, timeSlowDuration);

            if (currentTime > timeSlowDuration)
            {
                isTimeSlowed = false;
                currentTime = 0;
                Time.timeScale = 1;
            }
        }
    }

    public void PauseGame()
    {   //Seulement les éléments de Update arrêtent d'être exécutés
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void TimeSlow(float intensity, float duration)
    {
        Time.timeScale = intensity;
        timeSlowDuration = duration;
        isTimeSlowed = true;
    }
}
