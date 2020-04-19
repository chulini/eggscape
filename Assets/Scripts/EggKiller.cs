using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class EggKiller : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private FloatGameEvent cameraShakeEvent;
    [SerializeField] private FloatReference playerHealth;
    [SerializeField] private GameObject brokenEggPrefab;
    [SerializeField] private GameEvent playerDied;
#pragma warning restore 0649
    private void OnEnable()
    {
        playerHealth.AddListener(PlayerHealthChanged);
    }
    private void OnDisable()
    {
        playerHealth.RemoveListener(PlayerHealthChanged);
    }

    private void PlayerHealthChanged()
    {
        if (playerHealth.Value <= 0)
        {
            
            Instantiate(brokenEggPrefab, transform.position, transform.rotation);       
            DestroyImmediate(gameObject);
            playerDied.Raise();
            cameraShakeEvent.Raise(3f);
            
        }
    }
}
