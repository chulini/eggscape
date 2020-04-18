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

    [Header("Scriptable Objects (Writing)")]
    [SerializeField] private FloatVariable _xAxisMove;
    [SerializeField] private FloatVariable _yAxisMove;
    [SerializeField] private FloatVariable _xAxisView;
    [SerializeField] private FloatVariable _yAxisView;
#pragma warning restore 0649

    private void Start() {
        player = ReInput.players.GetPlayer(playerId);
    }

    void Update()
    {
        if (player.GetAxis("XAxisMove") != _xAxisMove.Value) _xAxisMove.Value = player.GetAxis("XAxisMove");
        if (player.GetAxis("YAxisMove") != _yAxisMove.Value) _yAxisMove.Value = player.GetAxis("YAxisMove");
        if (player.GetAxis("XAxisView") != _xAxisView.Value) _xAxisView.Value = player.GetAxis("XAxisView");
        if (player.GetAxis("YAxisView") != _yAxisView.Value) _yAxisView.Value = player.GetAxis("YAxisView");
        
        // Debug.Log($"{_xAxisMove.Value} {_yAxisMove.Value} {_xAxisView.Value} {_yAxisView.Value}");
        
    }
}
