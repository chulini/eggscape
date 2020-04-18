using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class Disk : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private FloatReference diskSpeed;
#pragma warning restore 0649
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rigidbody.MoveRotation(Quaternion.Euler(0,_rigidbody.rotation.eulerAngles.y + diskSpeed.Value*Time.fixedDeltaTime,0));
        // _rigidbody.rotation = ;
        // _transform.localRotation = Quaternion.Euler(0,_transform.localRotation.eulerAngles.y + diskSpeed.Value*Time.fixedDeltaTime,0);
    }
}
