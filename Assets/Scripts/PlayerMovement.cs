using System;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// Listen to the input SO and moves/jump the player
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
#pragma warning disable 0649
        [Header("Scriptable Objects (Reading)")]
        [SerializeField] private GameObjectReference _cameraReference;
        [SerializeField] private FloatReference _xAxis;
        [SerializeField] private FloatReference _yAxis;
        [SerializeField] private FloatReference _rotationSpeed;
        [SerializeField] private FloatReference _walkingSpeed;
        [SerializeField] private FloatReference _runningSpeed;
        [SerializeField] private BoolReference _isRunning;
        [SerializeField] private BoolReference _isGrounded;
        [SerializeField] private BoolReference _jumpButtonPressed;
        [SerializeField] private FloatVariable _jumpForce;
        [SerializeField] private FloatVariable _jumpForceForwardBoost;
        
#pragma warning restore 0649
        private Transform _cameraTransform;
        private Rigidbody _rigidbody;
        
        private Vector3 _lastValidLookingDirection;
        private bool _forceNotGrounded;
        

        private void OnEnable()
        {
            _jumpButtonPressed.AddListener(JumpButtonPressedChanged);
        }
        private void OnDisable()
        {
            _jumpButtonPressed.RemoveListener(JumpButtonPressedChanged);
        }
        private void JumpButtonPressedChanged()
        {
            if (_jumpButtonPressed.Value && _isGrounded.Value)
            {
                Vector3 currentVel = _rigidbody.velocity.WithY(0);
                _rigidbody.AddForce(
                    currentVel.normalized * (_jumpForce.Value * _jumpForceForwardBoost.Value) + 
                    Vector3.up * (_jumpForce.Value)
                    , ForceMode.Impulse);
                
                // Asume is not gounded for a small time so vectical force
                // can act and then start checking if its really grounded
                _forceNotGrounded = true;
                Invoke("StopForcingNotGrounded",.2f);
            }
        }


        private void Start()
        {
            _cameraTransform = _cameraReference.Value.GetComponent<Transform>();
            _rigidbody = GetComponent<Rigidbody>();
            _lastValidLookingDirection = new Vector3(_cameraTransform.forward.x, 0, _cameraTransform.forward.z).normalized;
        }

        
        void StopForcingNotGrounded()
        {
            _forceNotGrounded = false;
        }
        void FixedUpdate()
        {

            if (_forceNotGrounded || !_isGrounded.Value)
            {
                // Debug.Log($"is not grounded");
                // If its jumping don't respond to rotation
                 _rigidbody.rotation = Quaternion.Slerp(_rigidbody.rotation, Quaternion.LookRotation(_lastValidLookingDirection.normalized), Time.fixedDeltaTime * _rotationSpeed.Value);
                return;
            }

            Vector3 forwardDirection =
                new Vector3(_cameraTransform.forward.x, 0, _cameraTransform.forward.z).normalized;
            Vector3 rightDirection = (Quaternion.Euler(0f, 90f, 0f) * forwardDirection).normalized;

            Vector3 currentLookingDirection = Vector3.zero;
            currentLookingDirection += forwardDirection * _yAxis.Value;
            currentLookingDirection += rightDirection * _xAxis.Value;
            currentLookingDirection.y = 0.0f;

            float currentSpeed = _isRunning.Value ? _runningSpeed.Value : _walkingSpeed.Value;
            if (currentLookingDirection.magnitude > 0.01f)
                _lastValidLookingDirection = (currentLookingDirection.normalized * currentSpeed).WithY(0);

            Quaternion targetRotation = Quaternion.LookRotation(_lastValidLookingDirection.normalized);

            _rigidbody.rotation = Quaternion.Slerp(_rigidbody.rotation, targetRotation,
                Time.fixedDeltaTime * _rotationSpeed.Value);
            _rigidbody.velocity =
                (currentLookingDirection.magnitude > 0.01f
                    ? _lastValidLookingDirection * Time.fixedDeltaTime
                    : Vector3.zero).WithY(_rigidbody.velocity.y);

        }
    }
}
