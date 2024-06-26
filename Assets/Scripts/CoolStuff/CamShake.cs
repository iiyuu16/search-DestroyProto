using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamShake : MonoBehaviour
{
    public static CamShake instance;

    private CinemachineVirtualCamera CinemachineVirtualCamera;
    public float shakeInstensity = 1f;
    public float shakeDuration = .2f;

    private float timer;
    private CinemachineBasicMultiChannelPerlin perlin;

    private void Awake()
    {
        CinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    public void ShakeCamera()
    {
        CinemachineBasicMultiChannelPerlin perlin = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = shakeInstensity;
        timer = shakeDuration;
    }

    public void StopShake()
    {
        CinemachineBasicMultiChannelPerlin perlin = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = 0f;
        timer = 0f;
    }

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                StopShake();
            }
        }
    }

}
