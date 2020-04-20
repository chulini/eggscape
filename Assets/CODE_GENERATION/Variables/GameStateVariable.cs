using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[CreateAssetMenu(
	    fileName = "GameStateVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "GameState",
	    order = 120)]
	public class GameStateVariable : BaseVariable<GameState>
	{
	}
}