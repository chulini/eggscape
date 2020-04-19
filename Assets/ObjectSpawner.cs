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

    private bool _spawning;

    void Start()
    {
        InvokeRepeating("SpawnNewObject", 0f, _spawnFrequency);
    }

    void Update()
    {
        _spawning = Vector3.Distance(transform.position, _eggPlayer.Value.transform.position) < _playerSpawnDistance;
    }

    private void SpawnNewObject() {
        print("Spawn");
        if (_spawning) {
            Vector3 spawnPosition = transform.position;
            spawnPosition.x += SPos(_spawnRadius.x);
            spawnPosition.y += SPos(_spawnRadius.y);
            spawnPosition.z += SPos(_spawnRadius.z);
            print("Spawn! " + spawnPosition);
            Instantiate(_spawnObject, spawnPosition, Quaternion.identity);
        }
    }

    private float SPos(float v) {
        return Random.Range(-v / 2, v / 2);
    }
}
