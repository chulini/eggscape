using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _spawnObject;
    [SerializeField] private Vector3 _spawnRadius;

    [SerializeField] private float _spawnFrequency;
    [SerializeField] private float _playerSpawnDistance;
    [SerializeField] private GameObjectReference _eggPlayer;
    [Header("DEBUG")]
    [SerializeField] private bool _verboseOutput = false;

    private bool _spawning;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnNewObject), 0f, _spawnFrequency);
    }

    private void Update()
    {
        if (null == _eggPlayer.Value)
        {
            return;
        }

        _spawning = Vector3.Distance(transform.position, _eggPlayer.Value.transform.position) < _playerSpawnDistance;
    }

    private void SpawnNewObject()
    {
        if(_verboseOutput) print("Spawn");
        if (_spawning)
        {
            Vector3 spawnPosition = transform.position;
            spawnPosition.x += SPos(_spawnRadius.x);
            spawnPosition.y += SPos(_spawnRadius.y);
            spawnPosition.z += SPos(_spawnRadius.z);
            if(_verboseOutput) print("Spawn! " + spawnPosition);
            Instantiate(_spawnObject, spawnPosition, Quaternion.identity);
        }
    }

    private float SPos(float v)
    {
        return Random.Range(-v / 2, v / 2);
    }
}