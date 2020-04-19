using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class Disk : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private FloatReference diskSpeedX;
    [SerializeField] private FloatReference diskSpeedY;
    [SerializeField] private FloatReference diskSpeedZ;

#pragma warning restore 0649
    [SerializeField] private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Quaternion rotationBefore = transform.localRotation;
        transform.Rotate(Vector3.right, Time.fixedDeltaTime*diskSpeedX.Value, Space.Self);
        transform.Rotate(Vector3.up, Time.fixedDeltaTime*diskSpeedY.Value, Space.Self);
        transform.Rotate(Vector3.forward, Time.fixedDeltaTime*diskSpeedZ.Value, Space.Self);
        Quaternion rotationAfter = transform.localRotation;
        // transform.localRotation = rotationBefore;
        _rigidbody.MoveRotation(rotationAfter);
    }
}
