using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevelTrigger : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private GameEvent levelPassedEvent;
    [SerializeField] private string nextSceneName;
    [SerializeField] private GameObjectVariable playerGameObject;
    [SerializeField] private GameEvent playerDied;
    [SerializeField] private GameEvent _playerStarted;
#pragma warning restore 0649
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // playerDied.Raise();
            Destroy(playerGameObject.Value);
            playerGameObject.Value = null;
            SceneManager.LoadScene(nextSceneName);
            // _playerStarted.Raise();
            // levelPassedEvent.Raise();
        }
    }
}
