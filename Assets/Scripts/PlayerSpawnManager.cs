using System.Collections;
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

    private Rigidbody _playerRigidBody;
    private Coroutine _dieCoroutine;
    private Coroutine _spawnCoroutine;

    private void Start()
    {
        _playerRigidBody = _playerReference.Value.GetComponent<Rigidbody>();
    }

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

    private void OnPlayerDied()
    {
        Time.timeScale = 0.25f;
        _onScreenFade.Raise(1f);

        if (null != _dieCoroutine)
        {
            StopCoroutine(_dieCoroutine);
        }

        _dieCoroutine = StartCoroutine(DieCoroutine());
    }

    private void RespawnPlayer()
    {
        _eggHealth.Value = 100;
        _playerRigidBody.velocity = Vector3.zero;
        _playerRigidBody.angularVelocity = Vector3.zero;
        _playerReference.Value.transform.position = _activeCheckpoint.Value.transform.position;
        _playerReference.Value.transform.rotation = Quaternion.identity;
        
        if (null != _spawnCoroutine)
        {
            StopCoroutine(_spawnCoroutine);
        }

        _spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator DieCoroutine()
    {
        yield return new WaitForSecondsRealtime(_respawnTime);
        
        _onRespawnPlayer.Raise();
    }

    private IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSecondsRealtime(_respawnTime);

        Time.timeScale = 1f;
        _onScreenFade.Raise(0f);
        _playerSpawnedEvent.Raise();
    }
}