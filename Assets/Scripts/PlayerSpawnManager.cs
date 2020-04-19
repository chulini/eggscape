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

    private void OnEnable()
    {
        _playerDiedEvent.AddListener(OnPlayerDied);
        _onRespawnPlayer.AddListener(RespawnPlayer);
    }

    private void OnDisable()
    {
        _playerDiedEvent.RemoveListener(OnPlayerDied);
        _onRespawnPlayer.RemoveListener(RespawnPlayer);
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
        Time.timeScale = 0.5f;
        Invoke(nameof(RunDeathTransitions), 1f);
    }

    private void SpawnPlayer()
    {
        _playerReference.Value = Instantiate(_playerPrefab, _activeCheckpoint.Value.transform.position, Quaternion.identity);
    }

    private void RespawnPlayer()
    {
        SpawnPlayer();
        _eggHealth.Value = 100;
        Time.timeScale = 1f;
        Invoke(nameof(RunSpawnTransitions), _respawnTime);
    }

    private void RunSpawnTransitions()
    {
        _onScreenFade.Raise(0f);
        _playerSpawnedEvent.Raise();
    }
}