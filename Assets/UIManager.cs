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
    [SerializeField] private BoolGameEvent _onPauseEvent;
    [SerializeField] private GameObject startMenuContainer;
    [SerializeField] private GameObject pauseMenuContainer;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _onPauseEvent.Raise(true);
        startMenuContainer.SetActive(true);
        pauseMenuContainer.SetActive(false);
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
        startMenuContainer.SetActive(false);
        pauseMenuContainer.SetActive(paused);
    }

    public void PlayClicked()
    {
        _onPauseEvent.Raise(false);
    }
    public void ResumeClicked()
    {
        _onPauseEvent.Raise(false);
    }


    public void QuitClicked()
    {
        Application.Quit();
    }

}
