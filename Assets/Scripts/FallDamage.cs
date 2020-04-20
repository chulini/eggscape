using UnityEngine;
using ScriptableObjectArchitecture;

public class FallDamage : MonoBehaviour
{
    public float velocityDamageThreshold = 5f;
    public float velocityDamageMultiplier = 5f;

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private FloatReference _eggHealth;
    [SerializeField] private IntReference _eggInvulnerability;
    [SerializeField] private IntGameEvent _onDiedFromDamageType;
    private Vector3 _lastVelocity;

    private void Start()
    {
        _eggHealth.Value = 100f;
    }

    private void FixedUpdate()
    {
        if (_eggInvulnerability.Value > 0)
        {
            if (_rb != null) {
                _lastVelocity = _rb.velocity;
            }
            return;
        }

        var deltaVelocity = (_rb.velocity - _lastVelocity).magnitude;

        if (deltaVelocity > velocityDamageThreshold)
        {
            var damage = deltaVelocity * velocityDamageMultiplier;
            _eggHealth.Value -= damage;

            if (_eggHealth.Value <= float.Epsilon)
            {
                _onDiedFromDamageType.Raise((int) DamageType.Smashed);
            }
        }

        _lastVelocity = _rb.velocity;
    }
}