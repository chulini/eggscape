using ScriptableObjectArchitecture;
using UnityEngine;
using Random = UnityEngine.Random;

public class DamageComponent : MonoBehaviour
{
    [SerializeField] private Vector2 _damageRange = new Vector2(1, 10);
    [SerializeField] private float _damageInterval = 1f;
    
    [SerializeField] private FloatReference _eggHealth;
    [SerializeField] private GameObjectReference _player;
    [SerializeField] private GameEvent _onPlayerDied;

    private float _timeElapsed;
    private bool _takingDamage;

    private void OnEnable()
    {
        _onPlayerDied.AddListener(OnPlayerDied);
    }

    private void OnDisable()
    {
        _onPlayerDied.AddListener(OnPlayerDied);
    }

    private void Awake()
    {
        _timeElapsed = _damageInterval;
    }

    private void Update()
    {
        if (!_takingDamage) {
            _timeElapsed = _damageInterval;
        } else {
            TakeDamage();
        }
    }

    private void TakeDamage() {
        _timeElapsed += Time.deltaTime;
        
        if (_timeElapsed > _damageInterval) {
            _timeElapsed = 0;
            _eggHealth.Value -= CalculateDamage();
        }
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
        StopTakingDamage();
    }

    private void StopTakingDamage()
    {
        _timeElapsed = _damageInterval;
        _takingDamage = false;
    }

    private float CalculateDamage()
    {
        var damage = Random.Range(_damageRange.x, _damageRange.y);
        print($"Taking Damage: {damage}");
        
        return damage;
    }

    private void OnPlayerDied()
    {
        StopTakingDamage();
    }
}
