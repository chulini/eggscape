using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class FallDamage : MonoBehaviour
{
    public float velocityDamageThreshold = 5f;
    public float velocityDamageMultiplier = 5f;

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private FloatReference _eggHealth;
    [SerializeField] private IntReference _eggInvulnerability;
    private Vector3 _lastVelocity;

    private void Start() {
        _eggHealth.Value = 100f;
    }

    private void FixedUpdate()
    {
        float deltaVelocity = (_rb.velocity - _lastVelocity).magnitude;

        if (deltaVelocity > velocityDamageThreshold && _eggInvulnerability.Value <= 0) {
            float damage = deltaVelocity * velocityDamageMultiplier;
            _eggHealth.Value -= damage;
            // Debug.Log("Velocity: " + deltaVelocity.ToString());
            // Debug.Log("Damage: " + (damage).ToString());
        }
        _lastVelocity = _rb.velocity;
    }
}
