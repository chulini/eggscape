using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] FloatReference conveyorBeltSpeed;
    [SerializeField] float speedToShader;
    [SerializeField] Renderer _renderer;
#pragma warning restore 0649
    private Rigidbody _rigidbody;
    private Transform _transform;
    private MaterialPropertyBlock _materialPropertyBlock;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _materialPropertyBlock = new MaterialPropertyBlock();
        _transform = transform;
    }

    private void FixedUpdate()
    {
        Vector3 pos = _rigidbody.position;
        _rigidbody.position += -transform.right * conveyorBeltSpeed.Value * Time.fixedDeltaTime;
        _rigidbody.MovePosition(pos);
        
        _renderer.GetPropertyBlock(_materialPropertyBlock);
        _materialPropertyBlock.SetFloat("Vector1_B5B16460", conveyorBeltSpeed.Value*speedToShader);
        _renderer.SetPropertyBlock(_materialPropertyBlock);
    }
}
