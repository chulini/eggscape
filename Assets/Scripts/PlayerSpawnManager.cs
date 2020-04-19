using ScriptableObjectArchitecture;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] private FloatVariable _eggHealth;
    [SerializeField] private CheckpointComponentVariable _activeCheckpoint;
    [SerializeField] private GameObjectReference _playerReference;
    [SerializeField] private GameObject _playerPrefab;
    private void OnEnable()
    {
        _eggHealth.AddListener(CheckHealth);
    }

    private void OnDisable() {
        _eggHealth.RemoveListener(CheckHealth);
    }

    private void CheckHealth()
    {
        Debug.Log($"_eggHealth.Value {_eggHealth.Value}");
        if (_eggHealth.Value > float.Epsilon)
        {
            Debug.Log($"Player didn't die, dont' respawn");
            return;
        }
        Debug.Log($"Respawning!");
        Invoke("RespawnPlayer", 3f);
    }

    private void RespawnPlayer()
    {
        Debug.Log($"RespawnPlayer()");
        _eggHealth.Value = 100;
        Instantiate(_playerPrefab, _activeCheckpoint.Value.transform.position, Quaternion.identity);
        
        // _playerReference.Value.transform.position = _activeCheckpoint.Value.transform.position;
    }
}
