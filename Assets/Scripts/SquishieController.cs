﻿using UnityEngine;
using ScriptableObjectArchitecture;

public class SquishieController : MonoBehaviour
{
    [SerializeField] private AudioClip preparingClip;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private FloatGameEvent cameraShakeEvent;
    [SerializeField] private GameObjectReference playerGameObject;
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
    [SerializeField] private IntGameEvent _onDiedFromDamageType;

    private bool squishingDown;
    private bool movingToInitial;
    private bool windingUp;
    private Vector3 currVelocity;
    [SerializeField] private BoxCollider actualBoxCollider;
    [SerializeField] private float delayTime;

    private Vector3 _spawnPosition;
    private AudioSource _audioSource;

    [SerializeField] private float _audioActivationDistance;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _spawnPosition = transform.position;
        Invoke(nameof(WindUp), delayTime);
    }

    private void FixedUpdate()
    {
        if (movingToInitial)
        {
            _rb.MovePosition(_rb.transform.position + currVelocity * Time.fixedDeltaTime);
            var goalPosition = _spawnPosition;
            var offset = goalPosition - _rb.position;
            var angle = Mathf.Abs(Vector3.Angle(offset, transform.up));
            
            if (angle > 1f)
            {
                movingToInitial = false;
                SquishedUpRest();
            }
        }
        else if (windingUp)
        {
            _rb.MovePosition(_rb.transform.position + currVelocity * Time.fixedDeltaTime);
            var goalPosition = _spawnPosition + transform.up * windUpAmount;
            var offset = goalPosition - _rb.position;
            var angle = Mathf.Abs(Vector3.Angle(offset, transform.up));
            
            if (angle > 1f)
            {
                windingUp = false;
                WindUpRest();
            }
        }
        else if (squishingDown)
        {
            // print("Moving: " + _rb.transform.position + currVelocity * Time.fixedDeltaTime);
            var newPosition = _rb.transform.position + currVelocity * Time.fixedDeltaTime;
            var results = Physics.OverlapBox(transform.position + actualBoxCollider.center,
                actualBoxCollider.bounds.size / 2, transform.rotation);
            
            if (CheckCollisions(results))
            {
                // print("Collided!");
                squishingDown = false;
                SquishDownRest();
            }
            else
            {
                _rb.MovePosition(newPosition);
            }
        }
    }

    private bool CheckCollisions(Collider[] collisions)
    {
        if (collisions == null || collisions.Length <= 0)
        {
            return false;
        }

        var realCollisions = 0;
        foreach (var c in collisions)
        {
            if (c != null && GetParentMostGameObject(c.gameObject) != gameObject)
            {
                realCollisions += 1;
                
                if (c.gameObject == playerObject.Value)
                {
                    playerHealth.Value -= 9999f;
                    _onDiedFromDamageType.Raise((int) DamageType.Crushed);
                }
            }
        }

        return realCollisions > 0;
    }

    private GameObject GetParentMostGameObject(GameObject g)
    {
        var t = g.transform;
        while (t.parent != null)
        {
            t = t.parent;
        }

        return t.gameObject;
    }


    private void SquishDown()
    {
        // print("SquishDown");
        // _audioSource.clip = preparingClip; 
        // _audioSource.Play();
        squishingDown = true;
        currVelocity = -transform.up * squishDownSpeed;
    }

    private void SquishDownRest()
    {
        if (playerGameObject.Value != null)
        {
            var distance = (playerGameObject.Value.transform.position - transform.position).magnitude;
            if (distance < _audioActivationDistance)
            {
                cameraShakeEvent.Raise(Mathf.Lerp(.3f, .0f, ((distance - 1f) / 4f)));
                _audioSource.Stop();
                _audioSource.clip = hitClip;
                _audioSource.Play();
            }
        }

        Invoke("SquishedUp", squishedDownRestTime);
    }

    private void SquishedUp()
    {
        // print("SquishedUp");
        movingToInitial = true;
        currVelocity = transform.up * (Vector3.Distance(_rb.transform.position, _spawnPosition) / squishedUpTime);
    }

    private void SquishedUpRest()
    {
        // print("SquishedUpRest");
        Invoke("WindUp", squishedUpRestTime);
    }

    private void WindUp()
    {
        // print("Wind Up");
        windingUp = true;
        currVelocity = transform.up *
                       (Vector3.Distance(_rb.transform.position, _spawnPosition + transform.up * windUpAmount) /
                        windUpTime);
    }

    private void WindUpRest()
    {
        // print("Wind Up Rest");
        Invoke("SquishDown", windUpRestTime);
    }
}