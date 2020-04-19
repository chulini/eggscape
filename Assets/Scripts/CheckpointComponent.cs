using ScriptableObjectArchitecture;
using UnityEngine;

public class CheckpointComponent : MonoBehaviour
{
    [SerializeField] private GameObjectReference _player;
    [SerializeField] private CheckpointComponentVariable _activeCheckpoint;
    [SerializeField] private FloatGameEvent _onPlayerHealed;
    [SerializeField] private GameObject _checkpointLight;

    private Renderer _renderer;
    private Color _activeColor = new Color(36, 191, 0) * 1.5f;
    private Color _disabledColor = new Color(191, 4, 0) * 1.5f;
    private static readonly int _emissionColor = Shader.PropertyToID("_EmissionColor");

    private void Awake()
    {
        _renderer = _checkpointLight.GetComponent<Renderer>();
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
        _renderer.material.SetColor(_emissionColor, _disabledColor);
        _renderer.material.EnableKeyword("_Emission");
        _renderer.material.EnableKeyword("_EmissionColor");
        
        //Set the main Color of the Material to green
        _renderer.material.shader = Shader.Find("_EmissionColor");
        _renderer.material.SetColor("_EmissionColor", _disabledColor);
    }

    private void HealHandler()
    {
        _onPlayerHealed.Raise(100f);
    }
}
