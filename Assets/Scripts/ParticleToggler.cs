using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.Rendering;

public class ParticleToggler : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private FloatReference period;
#pragma warning restore 0649
    private ParticleSystem _particleSystem;
    private bool particlesOn = true;
    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        period.AddListener(PeriodChanged);
    }

    private void OnDisable()
    {
        period.RemoveListener(PeriodChanged);
    }

    private void PeriodChanged()
    {
        CancelInvoke("ToggleParticles");
        InvokeRepeating("ToggleParticles", period.Value, period.Value);
    }

    private void Start()
    {
        InvokeRepeating("ToggleParticles", period.Value, period.Value);
    }

    private void ToggleParticles()
    {
        if (particlesOn)
        {
            Debug.Log($"STOP");
            _particleSystem.Stop();
        }
        else
        {
            Debug.Log($"PLAY");
            _particleSystem.Play();
        }

        particlesOn = !particlesOn;
    }
}
