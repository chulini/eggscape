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
    [SerializeField] private FloatVariable _xAxis;
    [SerializeField] private FloatVariable _yAxis;
#pragma warning restore 0649

    private void Start() {
        player = ReInput.players.GetPlayer(playerId);
    }

    void Update()
    {
        if (player.GetAxis("MoveHorizontal") != _xAxis.Value) _xAxis.Value = player.GetAxis("MoveHorizontal");
        if (player.GetAxis("MoveVertical") != _yAxis.Value) _yAxis.Value = player.GetAxis("MoveVertical");
    }
}
