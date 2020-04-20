using ScriptableObjectArchitecture;
using UnityEngine;
using Random = UnityEngine.Random;

public class DamageComponent : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private Vector2 _damageRange = new Vector2(1, 10);
    [SerializeField] private float _damageInterval = 1f;
    [SerializeField] private FloatReference _eggHealth;
    [SerializeField] private GameEvent _onPlayerDied;
    [SerializeField] private GameObjectReference _player;
    [SerializeField] private Collider _damageCollider;
    [SerializeField] private DamageType _damageType = DamageType.None;
    [SerializeField] private IntGameEvent _onDiedFromDamageType;
#pragma warning restore 0649
    private float _timeElapsed;
    private bool _takingDamage;

    private void OnEnable()
    {
        _onPlayerDied.AddListener(OnPlayerDied);
    }

    private void OnDisable()
    {
        _onPlayerDied.RemoveListener(OnPlayerDied);
    }

    private void Awake()
    {
        _timeElapsed = _damageInterval;
    }

    private void Update()
    {
        if (_takingDamage)
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        if (_damageCollider != null && !_damageCollider.enabled)
        {
            return;
        }
        
        _timeElapsed += Time.deltaTime;

        if (!(_timeElapsed > _damageInterval))
        {
            return;
        }
        
        _timeElapsed = 0;
        _eggHealth.Value -= CalculateDamage();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != _player.Value && !other.gameObject.CompareTag("Player"))
        {
            return;
        }

        _takingDamage = true;
    }

    private void OnTriggerExit(Collider other)
    {
        ResetDamageComponent();
    }

    public void ResetDamageComponent()
    {
        _timeElapsed = _damageInterval;
        _takingDamage = false;
    }

    private float CalculateDamage()
    {
        return Random.Range(_damageRange.x, _damageRange.y);
    }

    private void OnPlayerDied()
    {
        if (_takingDamage)
        {
            _onDiedFromDamageType.Raise((int) _damageType);
        }
        
        ResetDamageComponent();
    }
}