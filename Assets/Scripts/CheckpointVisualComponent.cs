using UnityEngine;

public class CheckpointVisualComponent : MonoBehaviour
{
    private Transform _transform;
    
    public void ActivateCheckpoint()
    {
        var position = _transform.position;
        _transform.position = new Vector3(position.x, 1.12f, position.z);
    }

    public void DeactivateCheckpoint()
    {
        var position = _transform.position;
        _transform.position = new Vector3(position.x, 0, position.z);
    }

    private void Awake()
    {
        _transform = transform;
    }
}
