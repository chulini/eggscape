using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicEnd : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private AudioSource fxSoundAudioSource;
    [SerializeField] private float fxDelay;
    [SerializeField] private AudioSource bgSoundAudioSource;
    [SerializeField] private float bgMusicDelay;
#pragma warning restore 0649  
    IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(fxDelay);
        fxSoundAudioSource.Play();
        yield return new WaitForSecondsRealtime(bgMusicDelay);
        bgSoundAudioSource.Play();
    }
    

    
}
