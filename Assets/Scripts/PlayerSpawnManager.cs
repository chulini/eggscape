using ScriptableObjectArchitecture;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] private FloatReference _eggHealth;
    [SerializeField] private CheckpointComponentVariable _activeCheckpoint;
    [SerializeField] private GameObjectReference _playerReference;
    
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

        RespawnPlayer();
    }

    private void RespawnPlayer()
    {
        _eggHealth.Value = 100;
        _playerReference.Value.transform.position = _activeCheckpoint.Value.transform.position;
    }
}
