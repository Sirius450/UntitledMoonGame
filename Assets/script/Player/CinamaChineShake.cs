using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.Mathematics;

public class CinamaChineShake : MonoBehaviour
{
    public static CinamaChineShake Instance { get; private set; }
    private CinemachineVirtualCamera camera; 
    private float shakeTimer;

    private void Awake()
    {
        Instance = this;
        camera = GetComponent<CinemachineVirtualCamera>();
    }

    public void OnShakeCamera(float intensity, float time)
    { 
        CinemachineBasicMultiChannelPerlin camPerlin =
        camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        camPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
        

    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer < 0f)
            {
                CinemachineBasicMultiChannelPerlin camPerlin =
                camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                camPerlin.m_AmplitudeGain = 0f;
            }
        }
    }
}
