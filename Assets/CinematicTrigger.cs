using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicTrigger : MonoBehaviour
{
    [SerializeField] private GameStateVariable currentGameState;

    private void OnEnable()
    {
        currentGameState.AddListener(CurrentGameStateChanged);
    }

    private void OnDisable()
    {
        currentGameState.RemoveListener(CurrentGameStateChanged);
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
