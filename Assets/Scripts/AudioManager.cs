using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private GameEvent playerDied;
    [SerializeField] private AudioSource effectsAudioSource;
    [SerializeField] private AudioSource bgMusicAudioSource;
    [SerializeField] private AudioClip[] crackSounds;
#pragma warning restore 0649
    private void OnEnable()
    {
        playerDied.AddListener(PlayerDiedHandler);
    }

    private void OnDisable()
    {
        playerDied.RemoveListener(PlayerDiedHandler);
    }

    private void Update()
    {
        bgMusicAudioSource.pitch = Time.timeScale;
    }

    private void PlayerDiedHandler()
    {
        effectsAudioSource.PlayOneShot(crackSounds[Random.Range(0,crackSounds.Length)]);   
    }
}
