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
        // rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (diskSpeedX != null && diskSpeedX.Value != 0) {
            _rigidbody.MoveRotation(Quaternion.Euler(_rigidbody.rotation.eulerAngles.x + diskSpeedX.Value * Time.fixedDeltaTime, 0, 0));
        }

        if (diskSpeedY != null && diskSpeedY.Value != 0) {
            _rigidbody.MoveRotation(Quaternion.Euler(0, _rigidbody.rotation.eulerAngles.y + diskSpeedY.Value * Time.fixedDeltaTime, 0));
        }

        if (diskSpeedZ != null && diskSpeedZ.Value != 0) {
            _rigidbody.MoveRotation(Quaternion.Euler(0, 0, _rigidbody.rotation.eulerAngles.z + diskSpeedZ.Value * Time.fixedDeltaTime));
        }
    }
}
