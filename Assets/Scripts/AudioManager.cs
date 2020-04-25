using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private GameEvent playerDied;
    [SerializeField] private AudioSource effectsAudioSource;
    [SerializeField] private AudioSource bgMusicAudioSource;
    [SerializeField] private AudioClip splatClip;
    [SerializeField] AudioMixer mixer;
    [SerializeField] FloatReference fxVolumeVariable;
    [SerializeField] FloatReference musicVolumeVariable;
#pragma warning restore 0649
    private void OnEnable()
    {
        playerDied.AddListener(PlayerDiedHandler);
        fxVolumeVariable.AddListener(FXVolumeChanged);
        musicVolumeVariable.AddListener(MusicVolumeChanged);
        Invoke("RefreshVolumes", 1f/30f);
    }

    void RefreshVolumes()
    {
        FXVolumeChanged();
        MusicVolumeChanged();
    }
    private void OnDisable()
    {
        playerDied.RemoveListener(PlayerDiedHandler);
        fxVolumeVariable.RemoveListener(FXVolumeChanged);
        musicVolumeVariable.RemoveListener(MusicVolumeChanged);
    }
    void FXVolumeChanged()
    {
        Debug.Log($"FXVol changed {fxVolumeVariable.Value}");
        mixer.SetFloat("Effects", Mathf.Lerp(-80f, 0, Mathf.Pow(fxVolumeVariable.Value, .3f)));
    }
    void MusicVolumeChanged()
    {
        Debug.Log($"Music changed {musicVolumeVariable.Value}");
        mixer.SetFloat("Music", Mathf.Lerp(-80f, 0, Mathf.Pow(musicVolumeVariable.Value, .3f)));
    }
    private void Update()
    {
        bgMusicAudioSource.pitch = Time.timeScale;
    }
    private void PlayerDiedHandler()
    {
        effectsAudioSource.PlayOneShot(splatClip);   
    }
}
