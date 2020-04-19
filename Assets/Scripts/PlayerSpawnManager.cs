using ScriptableObjectArchitecture;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] private FloatReference _eggHealth;
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
        if (_eggHealth.Value > float.Epsilon)
        {
            return;
        }

        Invoke("RespawnPlayer", 3f);
    }

    private void RespawnPlayer()
    {
        Instantiate(_playerPrefab, _activeCheckpoint.Value.transform.position, Quaternion.identity);
        _eggHealth.Value = 100;
            
        // _playerReference.Value.transform.position = _activeCheckpoint.Value.transform.position;
    }
}
