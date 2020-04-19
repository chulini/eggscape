using System;
using Rewired;
using ScriptableObjectArchitecture;
using UnityEngine;


/// <summary>
/// Update axises scriptable objects from the player input 
/// </summary>
public class PlayerInput : MonoBehaviour
{
#pragma warning disable 0649
    private Rewired.Player player;
    private int playerId = 0;
    private bool _internalPauseState;

    [Header("Scriptable Objects (Writing)")] [SerializeField]
    private FloatVariable _xAxisMove;

    [SerializeField] private FloatVariable _yAxisMove;
    [SerializeField] private FloatVariable _xAxisView;
    [SerializeField] private FloatVariable _yAxisView;
    [SerializeField] private BoolGameEvent _onPauseEvent;
    [SerializeField] private GameEvent _onPlayerDiedEvent;
    [SerializeField] private GameEvent _onPlayerSpawnedEvent;
#pragma warning restore 0649

    private void Start()
    {
        player = ReInput.players.GetPlayer(playerId);
        _internalPauseState = true;
        _onPauseEvent.Raise(true);
    }

    private void Update()
    {
        HandlePausePressed();

        if (_internalPauseState)
        {
            _yAxisMove.Value = 0;
            _xAxisMove.Value = 0;
            _xAxisView.Value = 0;
            _yAxisView.Value = 0;
            
            return;
        }

        HandleMovementPressed();
    }

    private void OnEnable()
    {
        _onPlayerDiedEvent.AddListener(OnPlayerDied);
        _onPlayerSpawnedEvent.AddListener(OnPlayerSpawned);
    }

    private void OnDisable()
    {
        _onPlayerDiedEvent.RemoveListener(OnPlayerDied);
        _onPlayerSpawnedEvent.RemoveListener(OnPlayerSpawned);
    }

    private void HandlePausePressed()
    {
        if (player.GetButtonDown("PauseMenu"))
        {
            TogglePaused();
            _onPauseEvent.Raise(_internalPauseState);
        }
    }

    private void HandleMovementPressed()
    {
        if (Math.Abs(player.GetAxis("XAxisMove") - _xAxisMove.Value) > float.Epsilon)
            _xAxisMove.Value = player.GetAxis("XAxisMove");
        if (Math.Abs(player.GetAxis("YAxisMove") - _yAxisMove.Value) > float.Epsilon)
            _yAxisMove.Value = player.GetAxis("YAxisMove");
        if (Math.Abs(player.GetAxis("XAxisView") - _xAxisView.Value) > float.Epsilon)
            _xAxisView.Value = player.GetAxis("XAxisView");
        if (Math.Abs(player.GetAxis("YAxisView") - _yAxisView.Value) > float.Epsilon)
            _yAxisView.Value = player.GetAxis("YAxisView");
    }

    private void TogglePaused()
    {
        _internalPauseState = !_internalPauseState;
    }

    private void OnPlayerDied()
    {
        _internalPauseState = true;
    }

    private void OnPlayerSpawned()
    {
        _internalPauseState = false;
    }
}