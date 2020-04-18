﻿using System;
using JetBrains.Annotations;
using ScriptableObjectArchitecture;
using UnityEditor;
using UnityEngine;

namespace Camera
{
    /// <summary>
    /// Moves the camera in a third person logic
    /// </summary>
    public class CameraThirdPerson : MonoBehaviour
    {
#pragma warning disable 0649
        [Header("Scriptable Objects (Read)")]
        [SerializeField] private GameObjectReference _playerGameObjectSO;
        [SerializeField] private FloatReference _distanceFromPlayerBackwards;
        [SerializeField] private FloatReference _distanceFromPlayerHeight;
        [SerializeField] private Vector2Reference _rotationSpeed;
        
#pragma warning restore 0649
        private Transform _transform;
        private Transform _playerTransformCache;
        private Transform _cameraParentFollowingPlayer;
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
        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }
        
        private void OnEnable()
        {
            // Create parent game object following player
            GameObject parentGameObject = new GameObject("Camera Parent");
            _cameraParentFollowingPlayer = parentGameObject.transform;
            _transform.SetParent(_cameraParentFollowingPlayer);
            Cursor.visible = false;
        }

        private void OnDisable()
        {
            // Unparent camera and destroy parent game object following player
            _transform.SetParent(null);
            Destroy(_cameraParentFollowingPlayer.gameObject);
            Cursor.visible = true;
        }

    

        private void FixedUpdate()
        {
            _cameraParentFollowingPlayer.position = _playerTransform.position;
            _transform.localPosition = new Vector3(0,_distanceFromPlayerHeight.Value,-_distanceFromPlayerBackwards.Value);
            _transform.LookAt(_playerTransform.position + Vector3.up);
        }

        void Update()
        {
            //TODO move Input.GetAxis to input manager and listen here scriptable objects
            _yaw += _rotationSpeed.Value.x * Input.GetAxis("Mouse X");
            _pitch -= _rotationSpeed.Value.y * Input.GetAxis("Mouse Y");
            _cameraParentFollowingPlayer.eulerAngles = new Vector3(_pitch, _yaw, 0);
        }

    }
}