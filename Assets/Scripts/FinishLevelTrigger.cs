using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevelTrigger : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    // [SerializeField] private GameObjectVariable playerGameObject;
    [SerializeField] private GameEvent playerDied;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerDied.Raise();
            // playerGameObject.Value = null;
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
