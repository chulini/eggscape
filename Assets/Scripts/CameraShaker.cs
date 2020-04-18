using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private FloatGameEvent cameraShakeEvent;
    [SerializeField] private float shakeDecreasingSpeed;
    [SerializeField] private float perlinNoiseTimeSpeed;
#pragma warning restore 0649
    private Transform cameraShakeParent;
    private float shakeIntensity;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void OnEnable()
    {
        cameraShakeParent = new GameObject("Camera Shaker Parent").transform;
        cameraShakeParent.SetParent(transform.parent);
        transform.parent = cameraShakeParent;
        cameraShakeEvent.AddListener(CameraShakeEventHandler);
    }

    private void OnDisable()
    {
        transform.SetParent(null);
        Destroy(cameraShakeParent.gameObject);
        cameraShakeEvent.RemoveListener(CameraShakeEventHandler);
    }

    private void CameraShakeEventHandler(float inShakeIntensity)
    {
        shakeIntensity = inShakeIntensity;
    }

    void Update()
    {
        shakeIntensity = Mathf.Lerp(shakeIntensity, 0, Time.deltaTime*shakeDecreasingSpeed);
        cameraShakeParent.localPosition = new Vector3(
            Mathf.PerlinNoise(234.23f, Time.time*perlinNoiseTimeSpeed), 
            Mathf.PerlinNoise(45.12f, Time.time*perlinNoiseTimeSpeed), 
            Mathf.PerlinNoise(288.48f, Time.time*perlinNoiseTimeSpeed)) * shakeIntensity;
    }
}
