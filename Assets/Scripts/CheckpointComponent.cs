using ScriptableObjectArchitecture;
using UnityEngine;
using DoorState = SpawnerDoorAnimationState;

public class CheckpointComponent : MonoBehaviour
{
    [SerializeField] private GameObjectReference _player;
    [SerializeField] private CheckpointComponentVariable _activeCheckpoint;
    [SerializeField] private FloatGameEvent _onPlayerHealed;
    [SerializeField] private GameObject _checkpointLight;
    [SerializeField] private GameEvent _onSpawnPlayer;
    [SerializeField] private GameStateVariable _gameState;

    private Animator _animator;
    private Renderer _renderer;
    private MaterialPropertyBlock _activeBlock;
    private MaterialPropertyBlock _inactiveBlock;
    private static readonly int _state = Animator.StringToHash("state");
    private AudioSource _audioSource;
    private const float _soundDelay = 0.33f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponentInChildren<Animator>();
        _renderer = _checkpointLight.GetComponent<MeshRenderer>();
        _activeBlock = new MaterialPropertyBlock();
        _inactiveBlock = new MaterialPropertyBlock();
        var emissionColor = Shader.PropertyToID("_EmissionColor");
        _activeBlock.SetColor(emissionColor, new Color(36 / 255f * 1.5f, 191 / 255f * 1.5f, 0));
        _inactiveBlock.SetColor(emissionColor, new Color(191 / 255f * 1.5f, 4 / 255f * 1.5f, 0));
    }

    private void OnEnable()
    {
        _onSpawnPlayer.AddListener(OnSpawnPlayer);
        _gameState.AddListener(InitialiseCheckpointAnimation);
    }

    private void OnDisable()
    {
        _onSpawnPlayer.RemoveListener(OnSpawnPlayer);
        _gameState.RemoveListener(InitialiseCheckpointAnimation);
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
        if (_activeCheckpoint.Value == this)
        {
            InitialiseCheckpointAnimation();

            return;
        }

        if (null != _activeCheckpoint.Value)
        {
            var previous = _activeCheckpoint.Value;
            AnimateCheckpoint(previous._renderer, _inactiveBlock, previous._animator, DoorState.Close);
        }

        _audioSource.PlayDelayed(_soundDelay);
        AnimateCheckpoint(_renderer, _activeBlock, _animator, DoorState.Open);
        _activeCheckpoint.Value = this;
    }

    private void HealHandler()
    {
        _onPlayerHealed.Raise(100f);
    }

    private static void AnimateCheckpoint(Renderer lightRenderer, MaterialPropertyBlock block, Animator animator,
        DoorState animationState)
    {
        lightRenderer.SetPropertyBlock(block);
        animator.SetInteger(_state, (int) animationState);
    }

    private void InitialiseCheckpointAnimation()
    {
        if (_animator.GetInteger(_state) == 1 || _gameState.Value != GameState.playing ||
            _activeCheckpoint.Value != this)
        {
            return;
        }

        _audioSource.PlayDelayed(_soundDelay);
        AnimateCheckpoint(_renderer, _activeBlock, _animator, DoorState.Open);
    }

    private void OnSpawnPlayer()
    {
        var previous = _activeCheckpoint.Value;
        AnimateCheckpoint(previous._renderer, _inactiveBlock, previous._animator, DoorState.ForceClose);
    }
}