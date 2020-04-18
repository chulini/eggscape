using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private FloatVariable conveyorBeltSpeed;
#pragma warning restore 0649
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 pos = _rigidbody.position;
        _rigidbody.position += Vector3.back * conveyorBeltSpeed.Value * Time.fixedDeltaTime;
        _rigidbody.MovePosition(pos);
    }
}
