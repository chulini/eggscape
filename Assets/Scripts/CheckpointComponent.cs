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
        
        _activeCheckpoint.Value._checkpointVisual.DeactivateCheckpoint();
        _checkpointVisual.ActivateCheckpoint();
        _activeCheckpoint.Value = this;
    }
}
