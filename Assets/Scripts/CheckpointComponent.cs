using ScriptableObjectArchitecture;
using UnityEngine;

public class CheckpointComponent : MonoBehaviour
{
    [SerializeField] private GameObjectReference _player;
    [SerializeField] private CheckpointComponentVariable _activeCheckpoint;
    [SerializeField] private FloatGameEvent _onPlayerHealed;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != _player.Value)
        {
            return;
        }

        HealHandler();
    }

    private void HealHandler()
    {
        _onPlayerHealed.Raise(100f);
    }
}
