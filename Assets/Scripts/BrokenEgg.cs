using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class BrokenEgg : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private FloatReference explosionForce;
    [SerializeField] private Rigidbody[] brokenParts;
#pragma warning restore 0649
    private void Start()
    {
        foreach (Rigidbody brokenPart in brokenParts)
        {
            brokenPart.AddExplosionForce(explosionForce.Value, transform.position,0.5f, 0, ForceMode.Impulse );
        }
    }
}
