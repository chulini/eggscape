using ScriptableObjectArchitecture;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CheckpointComponentVariable _activeCheckpoint;
    [SerializeField] private GameObjectReference _playerReference;
    [SerializeField] private FloatReference _eggHealth;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private CheckpointComponent _levelStartPoint;

    private void Awake()
    {
        SetSpawnPoint();
        _playerReference.Value = Instantiate(_playerPrefab, _levelStartPoint.transform.position, Quaternion.identity);
        _eggHealth.Value = 100;
    }

    private void OnDestroy()
    {
        _activeCheckpoint.Value = _levelStartPoint;
    }

    private void SetSpawnPoint()
    {
        if (null == _activeCheckpoint.Value)
        {
            _activeCheckpoint.Value = _levelStartPoint;
        }
    }
}
