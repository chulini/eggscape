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
    [SerializeField] private FloatVariable _xAxis;
    [SerializeField] private FloatVariable _yAxis;
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
        Debug.Log($"{_xAxis} {_yAxis}");
        // Vector2 torquePower = Vector2.zero;
        // // Forward/Backwards torque
        // if (Mathf.Abs(_yAxis.Value) > axisThreshold.Value)
        // {
        //     torquePower = torquePower.WithX(torqueMaxPower.Value.y*_yAxis.Value);
        // }
        
        // _rigidbody.AddTorque();
    }
}
