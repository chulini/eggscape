using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class Launchpad : MonoBehaviour
{
    [SerializeField] private float invulnerabilityTime;
    [SerializeField] Rigidbody _rb;
    [SerializeField] IntReference eggInvulnerability;
    [SerializeField] private float impulseUp;
    [SerializeField] private float impulseForward;

    private Transform GetParentMostTransform(Transform t) {
        while(t.parent != null) {
            t = t.parent;
        }
        return t;
    }

    private void Start() {
        eggInvulnerability.Value = 0;
    }

    private void OnTriggerEnter(Collider other) {
        // Debug.Log("something entered");
        Transform collTransform = GetParentMostTransform(other.transform);
        if (collTransform.gameObject.tag == "Player") {
            Rigidbody collRb = collTransform.GetComponent<Rigidbody>();
            Vector3 totalForce = Vector3.up * impulseUp + transform.forward * impulseForward;
            SetPlayerInvulnerable(invulnerabilityTime);
            collRb.AddForce(totalForce, ForceMode.Impulse);
        }
    }

    void SetPlayerInvulnerable(float invulnerableTime) {
        eggInvulnerability.Value += 1;
        Invoke("DeactivateInvulnerability", invulnerableTime);
    }

    void DeactivateInvulnerability() {
        eggInvulnerability.Value -= 1;
    }
}
