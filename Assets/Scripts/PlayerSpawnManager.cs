using System;
using ScriptableObjectArchitecture;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] private CheckpointComponentVariable _activeCheckpoint;
    [SerializeField] private GameObjectReference _playerReference;
    [SerializeField] private GameEvent _playerDiedEvent;
    [SerializeField] private FloatVariable _eggHealth;
    [SerializeField] private GameEvent _playerSpawnedEvent;
    [SerializeField] private GameEvent _onRespawnPlayer;
    [SerializeField] private FloatGameEvent _onScreenFade;
    [SerializeField] private float _respawnTime = 1f;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameEvent _levelPassedEvent;
    private void OnEnable()
    {
        _playerDiedEvent.AddListener(OnPlayerDied);
        _onRespawnPlayer.AddListener(RespawnPlayer);
        _levelPassedEvent.AddListener(LevelPassedEvent);
    }

    private void OnDisable()
    {
        _playerDiedEvent.RemoveListener(OnPlayerDied);
        _onRespawnPlayer.RemoveListener(RespawnPlayer);
        _levelPassedEvent.RemoveListener(LevelPassedEvent);
    }

    private void LevelPassedEvent()
    {
        // Invoke("RespawnPlayer", .5f);
        Debug.Log($"Level passed");
        RespawnPlayer();
        Time.timeScale = 1;
    }

    private void Start()
    {
        RespawnPlayer();
    }



    private void DelayedSpawnCall()
    {
        _onRespawnPlayer.Raise();
    }

    private void RunDeathTransitions()
    {
        _onScreenFade.Raise(1f);
        Invoke(nameof(DelayedSpawnCall), 1f);
    }

    private void OnPlayerDied()
    {
        // Time.timeScale = 0.5f;
        Invoke(nameof(RunDeathTransitions), 1f);
    }

    private void SpawnPlayer()
    {
        Debug.Log($"SpawnPlayer()");
        _playerReference.Value = Instantiate(_playerPrefab, _activeCheckpoint.Value.transform.position, Quaternion.identity);
    }

    private void RespawnPlayer()
    {
        SpawnPlayer();
        _eggHealth.Value = 100;
        Debug.Log($"timescale set RespawnPlayer");
        Time.timeScale = 1f;
        Invoke(nameof(RunSpawnTransitions), _respawnTime);
    }

    private void RunSpawnTransitions()
    {
        _onScreenFade.Raise(0f);
        _playerSpawnedEvent.Raise();
    }
}