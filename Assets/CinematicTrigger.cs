using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicTrigger : MonoBehaviour
{
    [SerializeField] private GameStateVariable currentGameState;
    [SerializeField] private GameObjectReference cinematicGameObject;

    private void OnEnable()
    {
        cinematicGameObject.Value = gameObject;
        currentGameState.AddListener(CurrentGameStateChanged);
        
    }

    private void OnDisable()
    {
        currentGameState.RemoveListener(CurrentGameStateChanged);
        cinematicGameObject.Value = null;
    }

    private void CurrentGameStateChanged()
    {
        if (currentGameState.Value == GameState.cinematic)
        {
            GetComponent<PlayableDirector>().Play();
            Invoke("CinematicFinished", (float)GetComponent<PlayableDirector>().duration);    
        }
    }

    void CinematicFinished()
    {
        currentGameState.Value = GameState.playing;
        DestroyImmediate(gameObject);
    }
}
