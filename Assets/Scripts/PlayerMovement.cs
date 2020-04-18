using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private Vector2Reference torqueMaxPower;
    [Header("Scriptable Objects (Reading)")]
    [SerializeField] private FloatVariable _xAxisMove;
    [SerializeField] private FloatVariable _yAxisMove;
    [SerializeField] private FloatVariable axisThreshold;
    [SerializeField] private GameObjectVariable cameraGameObject;
#pragma warning restore 0649
    private Rigidbody _rigidbody;
    private Transform _transform;
    private Transform _cameraTransform;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = transform;
    }

    private void Start()
    {
        _cameraTransform = cameraGameObject.Value.transform;
    }

    private void FixedUpdate()
    {
        Vector3 forwardDirection = _cameraTransform.forward.WithY(0).normalized;
        Vector3 rightDirection = _cameraTransform.right.WithY(0).normalized;
        Vector3 torquePower = Vector3.zero;
        // Forward/Backwards torque
        if (Mathf.Abs(_yAxisMove.Value) > axisThreshold.Value)
        {
            torquePower += rightDirection * _yAxisMove.Value * torqueMaxPower.Value.y;
        }
        if (Mathf.Abs(_xAxisMove.Value) > axisThreshold.Value)
        {
            torquePower -= forwardDirection * _xAxisMove.Value * torqueMaxPower.Value.x;
        }
        _rigidbody.AddTorque(torquePower, ForceMode.Force);
    }
}
