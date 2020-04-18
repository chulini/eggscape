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
        _playerReference.Value = Instantiate(_playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        _eggHealth.Value = 0;
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
