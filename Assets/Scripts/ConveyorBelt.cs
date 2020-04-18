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
    private Renderer _renderer;
    private MaterialPropertyBlock _materialPropertyBlock;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
        
    }

    private void FixedUpdate()
    {
        Vector3 pos = _rigidbody.position;
        _rigidbody.position += Vector3.back * conveyorBeltSpeed.Value * Time.fixedDeltaTime;
        _rigidbody.MovePosition(pos);
        
        _renderer.GetPropertyBlock(_materialPropertyBlock);
        _materialPropertyBlock.SetFloat("Vector1_B5B16460", conveyorBeltSpeed.Value);
        _renderer.SetPropertyBlock(_materialPropertyBlock);
    }
}
