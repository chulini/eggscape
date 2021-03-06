﻿using System;
using JetBrains.Annotations;
using ScriptableObjectArchitecture;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Camera
{
    /// <summary>
    /// Moves the camera in a third person logic
    /// </summary>
    public class CameraThirdPerson : MonoBehaviour
    {
#pragma warning disable 0649
        [Header("Scriptable Objects (Write)")] [SerializeField]
        private GameObjectReference cameraGameObject;
        [Header("Scriptable Objects (Read)")]
        [SerializeField] private FloatVariable _xAxisView;
        [SerializeField] private FloatVariable _yAxisView;
        
        [SerializeField] private GameObjectReference _playerGameObjectSO;
        [SerializeField] private FloatReference _distanceFromPlayerBackwards;
        [SerializeField] private Vector2Reference _rotationSpeed;
        [SerializeField] private float _maxPitch;
        [SerializeField] private float _collisionMargin;
        [SerializeField] private float smoothness;
#pragma warning restore 0649
        private Transform _transform;
        private Transform _playerTransformCache;
        private Transform _cameraParentFollowingPlayer;
        private float _currentBackwardsDistance;
        private float _targetBackwardsDistance;
        Transform _playerTransform
        {
            get
            {
                if (_playerTransformCache != null)
                    return _playerTransformCache;

                if (_playerGameObjectSO.Value != null)
                {
                    _playerTransformCache = _playerGameObjectSO.Value.GetComponent<Transform>();
                    return _playerTransformCache;
                }
                return null;
            }
        }

        private float _yaw = 0;
        private float _pitch = 0;
        private bool lockedRotation = false;
        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }
        
        private void OnEnable()
        {
            _playerGameObjectSO.AddListener(PlayerGameObjectChanged);
            cameraGameObject.Value = gameObject;
            // Create parent game object following player
            GameObject parentGameObject = new GameObject("Camera Parent");
            _cameraParentFollowingPlayer = parentGameObject.transform;
            _transform.SetParent(_cameraParentFollowingPlayer);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Destroy(GetComponent<AudioListener>());
            _cameraParentFollowingPlayer.gameObject.AddComponent<AudioListener>();
        }

        private void OnDisable()
        {
            _playerGameObjectSO.RemoveListener(PlayerGameObjectChanged);
            cameraGameObject.Value = null;
            // Unparent camera and destroy parent game object following player
            _transform.SetParent(null);
            Destroy(_cameraParentFollowingPlayer.gameObject);
            Cursor.visible = true;
            
        }

        private void PlayerGameObjectChanged()
        {
            if (_playerGameObjectSO.Value != null)
            {
                lockedRotation = true;
                Vector3 spawnRotation = new Vector3(30,_playerTransform.rotation.eulerAngles.y, 0 );
                Debug.Log($"setting camera rotation {spawnRotation}");
                _cameraParentFollowingPlayer.rotation = Quaternion.Euler(spawnRotation);
                Invoke("UnlockRotation", .1f);
                _xAxisView.Value = 0;
                _yAxisView.Value = 0;
                _pitch = 30;
                _yaw = _playerTransform.rotation.eulerAngles.y;
            }
        }

        private void UnlockRotation()
        {
            lockedRotation = false;
        }

      

        private void FixedUpdate()
        {
            if (_playerTransform == null)
                return;
            _cameraParentFollowingPlayer.position = _playerTransform.position;
            
            _transform.localPosition = new Vector3(0,0,-_currentBackwardsDistance);
            _transform.LookAt(_playerTransform.position);
        }

        private Vector3 desiredPosition;
        void Update()
        {
            if (_playerTransform == null)
                return;
            _cameraParentFollowingPlayer.position = _playerTransform.position;
            _transform.localPosition = new Vector3(0,0,-_currentBackwardsDistance);
            _transform.LookAt(_playerTransform.position);
            
            
            
            _currentBackwardsDistance = Mathf.Lerp(_currentBackwardsDistance, _targetBackwardsDistance, Time.deltaTime*smoothness);
            if (!lockedRotation)
            {
                _yaw += _rotationSpeed.Value.x * _xAxisView.Value;
                _pitch -= _rotationSpeed.Value.y * _yAxisView.Value;
                _pitch = _pitch.Clamp(-_maxPitch, _maxPitch);
                _cameraParentFollowingPlayer.eulerAngles = new Vector3(_pitch, _yaw, 0);
            }

            desiredPosition = _cameraParentFollowingPlayer.position -
                              _cameraParentFollowingPlayer.forward * _distanceFromPlayerBackwards.Value;
            if (Physics.Linecast(_cameraParentFollowingPlayer.position, desiredPosition, out RaycastHit hit))
            {
                _targetBackwardsDistance = Mathf.Clamp((hit.distance * _collisionMargin), 0.8f, _distanceFromPlayerBackwards.Value);
            }
            else
            {
                _targetBackwardsDistance =_distanceFromPlayerBackwards.Value;
            }
        }

        // private void OnDrawGizmos()
        // {
        //     Gizmos.color = Color.green;
        //     Gizmos.DrawSphere(desiredPosition, 0.5f);
        //     if (_cameraParentFollowingPlayer == null)
        //         return;
        //     
        //     Vector3 camPos = _cameraParentFollowingPlayer.position -
        //                       _cameraParentFollowingPlayer.forward * _currentBackwardsDistance;
        //     Gizmos.color = Color.yellow;
        //     Gizmos.DrawSphere(camPos, 0.4f);
        // }
    }
}