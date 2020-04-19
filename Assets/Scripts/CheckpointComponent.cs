using ScriptableObjectArchitecture;
using UnityEngine;

public class CheckpointComponent : MonoBehaviour
{
    [SerializeField] private GameObjectReference _player;
    [SerializeField] private CheckpointComponentVariable _activeCheckpoint;
    [SerializeField] private CheckpointVisualComponent _checkpointVisual;
    [SerializeField] private FloatGameEvent _onPlayerHealed;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != _player.Value)
        {
            return;
        }

        ActivationHandler();
        HealHandler();
    }

    private void ActivationHandler()
    {
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

    private void HealHandler()
    {
        _onPlayerHealed.Raise(100f);
    }
}
