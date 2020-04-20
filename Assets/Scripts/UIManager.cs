using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Rewired;
using ScriptableObjectArchitecture;
using Cursor = UnityEngine.Cursor;

public class UIManager : MonoBehaviour
{
    // public static UIManager instance; 
    // [SerializeField] private BoolGameEvent _onPauseEvent;
    [SerializeField] private GameObject startMenuContainer;
    [SerializeField] private GameObject pauseMenuContainer;
    [SerializeField] private GameStateVariable currentGameState;
    // [SerializeField] private GameEvent _playerStarted;
    [SerializeField] private BoolReference playerStartedTheGame;
    [SerializeField] private GameObjectReference cinematicGameObject;
    private void Awake()
    {
        // if (instance == null)
        // {
        //     instance = this;
        //     DontDestroyOnLoad(gameObject);   
        // }
        // else
        // {
        //     DestroyImmediate(gameObject);
        // }
        
    }

    private void Start()
    {
        // _onPauseEvent.Raise(true);
        if(!playerStartedTheGame.Value)
            currentGameState.Value = GameState.startMenu;
        else
            currentGameState.Value = GameState.playing;
    }

    private void OnEnable()
    {
        // _onPauseEvent.AddListener(PauseEventHandler);
        currentGameState.AddListener(GameStateChanged);
    }

    private void OnDisable()
    {
        // _onPauseEvent.RemoveListener(PauseEventHandler);
        currentGameState.RemoveListener(GameStateChanged);
    }

    private void GameStateChanged()
    {
        if (currentGameState.Value == GameState.startMenu)
        {
            startMenuContainer.SetActive(true);
            pauseMenuContainer.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (currentGameState.Value == GameState.cinematic)
        {
            startMenuContainer.SetActive(false);
            pauseMenuContainer.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (currentGameState.Value == GameState.playing)
        {
            startMenuContainer.SetActive(false);
            pauseMenuContainer.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (currentGameState.Value == GameState.paused)
        {
            startMenuContainer.SetActive(false);
            pauseMenuContainer.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }



    public void PlayClicked()
    {
        // _playerStarted.Raise();
        currentGameState.Value = cinematicGameObject.Value != null ? GameState.cinematic : GameState.playing;
        playerStartedTheGame.Value = true;
    }

  
    public void ResumeClicked()
    {
        currentGameState.Value = GameState.playing;
    }

    private void OnApplicationQuit()
    {
        playerStartedTheGame.Value = false;
    }

    public void QuitClicked()
    {
        Application.Quit();
    }

}