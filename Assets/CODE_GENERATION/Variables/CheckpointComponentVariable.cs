using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[CreateAssetMenu(
	    fileName = "CheckpointComponentVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "CheckpointComponent",
	    order = 120)]
	public class CheckpointComponentVariable : BaseVariable<CheckpointComponent>
	{
	}
}