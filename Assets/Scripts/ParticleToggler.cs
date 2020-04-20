using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.Rendering;

// [RequireComponent(typeof(DamageComponent))]
public class ParticleToggler : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private FloatReference period;
#pragma warning restore 0649
    private ParticleSystem _particleSystem;
    private bool particlesOn = true;
    private Collider _collider;
    
    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        if (_collider == null) {
            _collider = GetComponent<Collider>();
        }
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
            _particleSystem.Stop();
            _collider.enabled = false;
        }
        else
        {
            _particleSystem.Play();
            _collider.enabled = true;
        }

        particlesOn = !particlesOn;
    }
}
