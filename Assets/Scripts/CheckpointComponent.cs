using ScriptableObjectArchitecture;
using UnityEditor;
using UnityEngine;

public class CheckpointComponent : MonoBehaviour
{
    [SerializeField] private GameObjectReference _player;
    [SerializeField] private CheckpointComponentVariable _activeCheckpoint;
    [SerializeField] private FloatGameEvent _onPlayerHealed;
    [SerializeField] private GameObject _checkpointLight;

    private Renderer _renderer;

    private MaterialPropertyBlock _activeBlock;
    private MaterialPropertyBlock _inactiveBlock;

    private void Awake()
    {
        _renderer = _checkpointLight.GetComponent<MeshRenderer>();
        _activeBlock = new MaterialPropertyBlock();
        _inactiveBlock = new MaterialPropertyBlock();
        var emissionColor = Shader.PropertyToID("_EmissionColor");
        _activeBlock.SetColor(emissionColor, new Color(36 / 255f * 1.5f, 191 / 255f * 1.5f, 0));
        _inactiveBlock.SetColor(emissionColor, new Color(191 / 255f * 1.5f, 4 / 255f * 1.5f, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != _player.Value)
        {
            return;
        }

        ActivateCheckpoint();
        HealHandler();
    }

    private void ActivateCheckpoint()
    {
        if (null != _activeCheckpoint.Value)
        {
            UpdateCheckpointLight(_activeCheckpoint.Value._renderer, _inactiveBlock);
        }
        
        UpdateCheckpointLight(_renderer, _activeBlock);
        _activeCheckpoint.Value = this;
    }

    private void HealHandler()
    {
        _onPlayerHealed.Raise(100f);
    }

    private static void UpdateCheckpointLight(Renderer lightRenderer, MaterialPropertyBlock block)
    {
        lightRenderer.SetPropertyBlock(block);
    }
}
