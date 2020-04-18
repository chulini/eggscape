using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class FallDamage : MonoBehaviour
{
    public float velocityDamageThreshold = 5f;
    public float velocityDamageMultiplier = 5f;

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private FloatReference eggHealth;
    private Vector3 _lastVelocity;

    private void Start() {
        eggHealth.Value = 100f;
    }

    private void FixedUpdate()
    {
        float deltaVelocity = (_rb.velocity - _lastVelocity).magnitude;

        if (deltaVelocity > velocityDamageThreshold) {
            float damage = deltaVelocity * velocityDamageMultiplier;
            eggHealth.Value -= damage;
            Debug.Log("Last velocity: " + _lastVelocity.ToString());
            Debug.Log("This velocity: " + _rb.velocity.ToString());

            Debug.Log("Velocity: " + deltaVelocity.ToString());
            Debug.Log("Damage: " + (damage).ToString());
        }
        _lastVelocity = _rb.velocity;
    }
}
