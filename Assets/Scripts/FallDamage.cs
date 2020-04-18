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

    private void FixedUpdate()
    {
        Vector3 currVelocity = _rb.velocity;
        float deltaVelocity = (currVelocity - _lastVelocity).magnitude;
        _lastVelocity = currVelocity;

        if (deltaVelocity > velocityDamageThreshold) {
            float damage = deltaVelocity * velocityDamageMultiplier;
            eggHealth.Value -= damage;
            // Debug.Log("Velocity: " + deltaVelocity.ToString());
            // Debug.Log("Damage: " + (damage).ToString());
        }
    }
}
