using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private GameObjectVariable playerGameObject;
#pragma warning restore 0649
    private void OnEnable()
    {
        playerGameObject.Value = gameObject;
    }

    private void OnDisable()
    {
        playerGameObject.Value = null;
    }
}
