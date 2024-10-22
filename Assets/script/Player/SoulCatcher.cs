using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoulCatcher : MonoBehaviour
{
    [SerializeField] UnityEvent OnGainSouls;
    [SerializeField] UnityEvent OnLoseSouls;

    public int maxSouls = 999999;
    private int currentSouls = 0;
    public int CurrentSouls => currentSouls;

    public void GainSouls(int Souls)
    {
        currentSouls += Souls;
        currentSouls = Mathf.Clamp(currentSouls, 0, maxSouls);

        OnGainSouls?.Invoke();
    }



}
