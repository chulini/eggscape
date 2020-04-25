using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class FloorDisabler : MonoBehaviour
{
    [SerializeField] private DamageComponent _floorDamageComponent;
    [SerializeField] private GameObjectReference _eggPlayer;
    [SerializeField] private GameEvent _afterRespawnEvent;

    private void OnEnable() {
        _afterRespawnEvent.AddListener(EnableDamageFloor);
    }

    private void OnDisable() {
        _afterRespawnEvent.RemoveListener(EnableDamageFloor);
    }

    private void EnableDamageFloor() {
        _floorDamageComponent.enabled = true;
    }

    private void OnTriggerEnter(Collider other) {
        print("Disabling!");
        if (other.gameObject == _eggPlayer.Value) {
            print("yes!");
            _floorDamageComponent.enabled = false;
        }
    }
}
