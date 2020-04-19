using ScriptableObjectArchitecture;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private GameEvent playerDied;
    [SerializeField] private FloatGameEvent _onPlayerHealed;
    [SerializeField] private AudioSource effectsAudioSource;
    [SerializeField] private AudioSource bgMusicAudioSource;
    [SerializeField] private AudioClip splatClip;
    [SerializeField] private AudioClip _healClip;
#pragma warning restore 0649
    
    private void OnEnable()
    {
        playerDied.AddListener(PlayerDiedHandler);
        _onPlayerHealed.AddListener(OnPlayerHealed);
    }

    private void OnDisable()
    {
        playerDied.RemoveListener(PlayerDiedHandler);
        _onPlayerHealed.RemoveListener(OnPlayerHealed);
    }

    private void Update()
    {
        bgMusicAudioSource.pitch = Time.timeScale;
    }

    private void PlayerDiedHandler()
    {
        effectsAudioSource.PlayOneShot(splatClip);   
    }

    private void OnPlayerHealed()
    {
        effectsAudioSource.PlayOneShot(_healClip);
    }
}
