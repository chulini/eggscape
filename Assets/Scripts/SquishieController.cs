using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class SquishieController : MonoBehaviour
{
    [SerializeField] private float windUpAmount;
    [SerializeField] private float windUpTime;
    [SerializeField] private float windUpRestTime;
    [SerializeField] private float squishedDownRestTime;
    [SerializeField] private float squishedUpTime;
    [SerializeField] private float squishedUpRestTime;
    [SerializeField] private float squishDownSpeed;
    [SerializeField] private float chillSpeed;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private AnimationCurve animCurve;

    [SerializeField] private GameObjectReference playerObject;
    [SerializeField] private FloatReference playerHealth;

    private bool squishingDown;
    private bool movingToInitial;
    private bool windingUp;
    private Vector3 currVelocity;
    [SerializeField] private BoxCollider actualBoxCollider;
    [SerializeField] private float delayTime;

    private Vector3 _spawnPosition;
    // [SerializeField] private BoxCollider triggerBoxCollider;

    private void Start()
    {
        _spawnPosition = transform.position;
        Invoke("WindUp", delayTime);
    }

    private void FixedUpdate() {
        if (movingToInitial) {
            _rb.MovePosition(_rb.transform.position + currVelocity * Time.fixedDeltaTime);
            Vector3 goalPosition = _spawnPosition;
            Vector3 offset = goalPosition - _rb.position;
            float angle = Mathf.Abs(Vector3.Angle(offset, transform.up));
            if (angle > 1f) {
                movingToInitial = false;
                SquishedUpRest();
            }
        } else if (windingUp) {
            _rb.MovePosition(_rb.transform.position + currVelocity * Time.fixedDeltaTime);
            Vector3 goalPosition = _spawnPosition + transform.up * windUpAmount;
            Vector3 offset = goalPosition - _rb.position;
            float angle = Mathf.Abs(Vector3.Angle(offset, transform.up));
            if (angle > 1f) {
                windingUp = false;
                WindUpRest();
            }
        } else if (squishingDown) {
            // print("Moving: " + _rb.transform.position + currVelocity * Time.fixedDeltaTime);
            Vector3 newPosition = _rb.transform.position + currVelocity * Time.fixedDeltaTime;
            Collider[] results = Physics.OverlapBox(transform.position + actualBoxCollider.center, actualBoxCollider.bounds.size / 2, transform.rotation);
            if (CheckCollisions(results)) {
                // print("Collided!");
                squishingDown = false;
                SquishDownRest();
            } else {
                _rb.MovePosition(newPosition);
            }
        }
    }

    private bool CheckCollisions(Collider[] collisions) {
        if (collisions == null || collisions.Length <= 0) {
            return false;
        }

        int realCollisions = 0;
        foreach (Collider c in collisions) {
            if (GetParentMostGameObject(c.gameObject) != gameObject) {
                realCollisions += 1;
                if (c.gameObject == playerObject.Value) {
                    playerHealth.Value -= 9999f;
                }
            }
        }
        return realCollisions > 0;
    }

    private GameObject GetParentMostGameObject(GameObject g) {
        Transform t = g.transform;
        while (t.parent != null) {
            t = t.parent;
        }
        return t.gameObject;
    }


    private void SquishDown() {
        // print("SquishDown");
        squishingDown = true;
        currVelocity = -transform.up * squishDownSpeed;
    }

    private void SquishDownRest() {
        Invoke("SquishedUp", squishedDownRestTime);
    }

    private void SquishedUp() {
        // print("SquishedUp");
        movingToInitial = true;
        currVelocity = transform.up * (Vector3.Distance(_rb.transform.position , _spawnPosition) / squishedUpTime);
    }

    private void SquishedUpRest() {
        // print("SquishedUpRest");
        Invoke("WindUp", squishedUpRestTime);
    }

    private void WindUp() {
        // print("Wind Up");
        windingUp = true;
        currVelocity = transform.up * (Vector3.Distance(_rb.transform.position, _spawnPosition + transform.up * windUpAmount) / windUpTime);
    }

    private void WindUpRest() {
        // print("Wind Up Rest");
        Invoke("SquishDown", windUpRestTime);
    }
}
