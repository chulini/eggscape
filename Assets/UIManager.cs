﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Rewired;
using ScriptableObjectArchitecture;
using Cursor = UnityEngine.Cursor;

public class UIManager : MonoBehaviour
{
    [SerializeField] private BoolGameEvent _onPauseEvent;
    [SerializeField] private GameObject pauseMenuContainer;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Time.timeScale = 0;
    }

    private void OnEnable()
    {
        _onPauseEvent.AddListener(PauseEventHandler);
        gameObject.transform.SetAsLastSibling();
    }

    private void OnDisable()
    {
        _onPauseEvent.RemoveListener(PauseEventHandler);
    }

    private void PauseEventHandler(bool paused)
    {
        Cursor.visible = paused;
        Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;
        pauseMenuContainer.SetActive(paused);
    }

    public void PlayClicked()
    {
        Time.timeScale = 1;
    }

    public void QuitClicked()
    {
        Application.Quit();
    }

    public void ResumeButtonClick()
    {
        _onPauseEvent.Raise(false);
    }
}
