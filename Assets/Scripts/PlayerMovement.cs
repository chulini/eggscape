using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private Vector2Reference _torqueMaxPower;
    [Header("Scriptable Objects (Reading)")]
    [SerializeField] private FloatVariable _xAxisMove;
    [SerializeField] private FloatVariable _yAxisMove;
    [SerializeField] private FloatVariable _axisThreshold;
    [SerializeField] private GameObjectVariable _cameraGameObject;
    [SerializeField] private FloatVariable _airControlAmount;
    [SerializeField] private FloatReference _maxAngularVelocity;
#pragma warning restore 0649
    private Rigidbody _rigidbody;
    private Transform _transform;
    private Transform _cameraTransform;
    private bool _grounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.maxAngularVelocity = _maxAngularVelocity.Value;
        _transform = transform;
    }

    private void Start()
    {
        _cameraTransform = _cameraGameObject.Value.transform;
        
    }

    private void FixedUpdate()
    {
        Vector3 forwardDirection = _cameraTransform.forward.WithY(0).normalized;
        Vector3 rightDirection = _cameraTransform.right.WithY(0).normalized;
        Vector3 torquePower = Vector3.zero;
        // Forward/Backwards torque
        if (Mathf.Abs(_yAxisMove.Value) > _axisThreshold.Value)
        {
            torquePower += rightDirection * _yAxisMove.Value * _torqueMaxPower.Value.y;
        }
        if (Mathf.Abs(_xAxisMove.Value) > _axisThreshold.Value)
        {
            torquePower -= forwardDirection * _xAxisMove.Value * _torqueMaxPower.Value.x;
        }
        _rigidbody.AddTorque(torquePower, ForceMode.Force);

        // Movement in air
        Vector3 movementDirection = (forwardDirection * _yAxisMove.Value + rightDirection * _xAxisMove.Value).WithY(0f).normalized;
        // bool isGrounded = Physics.Raycast(transform.position + new Vector3(0f, 0.1f, 0f), Vector3.down, 0.2f);
        if (!_grounded) {
            // Debug.Log("Controlling the air!");
            _rigidbody.AddForce(movementDirection * _airControlAmount, ForceMode.Acceleration);
        }
    }

    // For detecting if in air
    private void OnTriggerEnter(Collider other) {
        _grounded = true;
    }

    private void OnTriggerStay(Collider other) {
        _grounded = true;
    }

    private void OnTriggerExit(Collider other) {
        _grounded = false;
    }
}
