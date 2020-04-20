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
    [SerializeField] FloatReference period;
    [SerializeField] private float _initialWaitTime;
    [Header("OPTIONAL")]
    [SerializeField] private AudioClip activateSoundClip;
    [SerializeField] private DamageComponent damageComponent;
#pragma warning restore 0649
    private ParticleSystem _particleSystem;
    private bool particlesOn = true;
    private Collider _collider;
    private AudioSource _audioSource;
    
    
    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        if (_collider == null) {
            _collider = GetComponent<Collider>();
        }

        _audioSource = GetComponent<AudioSource>();
        
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
        Invoke("StartSpraying", _initialWaitTime);
    }

    private void StartSpraying() {
        InvokeRepeating("ToggleParticles", period.Value, period.Value);

    }

    private void ToggleParticles()
    {
        if (particlesOn)
        {
            _particleSystem.Stop();
            _collider.enabled = false;
            if (_audioSource != null && activateSoundClip != null)
            {
                _audioSource.Stop();
            }
            if(damageComponent != null)
                damageComponent.ResetDamageComponent();
        }
        else
        {
            _particleSystem.Play();
            _collider.enabled = true;
            if (_audioSource != null && activateSoundClip != null)
            {
                _audioSource.clip = activateSoundClip;
                _audioSource.Play();
            }
        }

        particlesOn = !particlesOn;
    }
}