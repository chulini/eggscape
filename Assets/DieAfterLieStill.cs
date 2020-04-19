using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAfterLieStill : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    private float lastMoving;
    [SerializeField] private float destroyAfterStillForXSeconds;

    private void Start() {
        lastMoving = Time.time;
    }

    void Update()
    {
        if (_rb.velocity.magnitude > 0.1) {
            lastMoving = Time.time;
        }
        // print(lastMoving);
        if (lastMoving + destroyAfterStillForXSeconds < Time.time) {
            // print("DED!");
            Destroy(gameObject);
        }
    }
}
