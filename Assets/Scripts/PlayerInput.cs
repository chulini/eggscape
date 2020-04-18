using Rewired;
using ScriptableObjectArchitecture;
using UnityEngine;


/// <summary>
/// Update axises and trigger button events from the player input 
/// </summary>
public class PlayerInput : MonoBehaviour
{
#pragma warning disable 0649
    private Rewired.Player player;
    private int playerId = 0;

    [Header("Scriptable Objects (Writing)")]
    [SerializeField] private FloatVariable _xAxis;
    [SerializeField] private FloatVariable _yAxis;
    [SerializeField] private BoolVariable _actionButtonPressed;
    [SerializeField] private BoolVariable _runningButtonPressed;
    [SerializeField] private BoolVariable _changeCameraButtonPressed;
    [SerializeField] private BoolVariable _jumpButtonPressed;
#pragma warning restore 0649

    private void Start() {
        player = ReInput.players.GetPlayer(playerId);
    }

    void Update()
    {
        if (player.GetAxis("MoveHorizontal") != _xAxis.Value) _xAxis.Value = player.GetAxis("MoveHorizontal");
        if (player.GetAxis("MoveVertical") != _yAxis.Value) _yAxis.Value = player.GetAxis("MoveVertical");
        
        if(player.GetButtonDown("Interact1")) _actionButtonPressed.Value = true;
        else if(player.GetButtonUp("Interact1")) _actionButtonPressed.Value = false;

        if(player.GetButtonDown("Sprint")) _runningButtonPressed.Value = true;
        else if(player.GetButtonUp("Sprint")) _runningButtonPressed.Value = false;
        
        if(player.GetButtonDown("ChangeCamera")) _changeCameraButtonPressed.Value = true;
        else if(player.GetButtonUp("ChangeCamera")) _changeCameraButtonPressed.Value = false;
        
        if(player.GetButtonDown("Jump")) _jumpButtonPressed.Value = true;
        else if(player.GetButtonUp("Jump")) _jumpButtonPressed.Value = false;
    }
}
