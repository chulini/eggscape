using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private GameObjectVariable playerGameObject;
    [SerializeField] private Vector3Reference centerOfMass;
#pragma warning restore 0649
    Rigidbody _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        playerGameObject.Value = gameObject;
    }

    private void OnDisable()
    {
        playerGameObject.Value = null;
    }

    private void Update()
    {
        _rigidbody.centerOfMass = centerOfMass.Value;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + transform.rotation*centerOfMass.Value, .05f);
    }
}
