using ScriptableObjectArchitecture;
using UnityEngine;

public class CheckpointComponent : MonoBehaviour
{
    [SerializeField] private GameObjectReference _player;
    [SerializeField] private CheckpointComponentVariable _activeCheckpoint;
    [SerializeField] private CheckpointVisualComponent _checkpointVisual;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != _player.Value)
        {
            return;
        }

        if (null != _activeCheckpoint.Value && null != _activeCheckpoint.Value._checkpointVisual)
        {
            _activeCheckpoint.Value._checkpointVisual.DeactivateCheckpoint();
        }

        if (null != _checkpointVisual)
        {
            _checkpointVisual.ActivateCheckpoint();
        }
        
        _activeCheckpoint.Value = this;
    }
}
