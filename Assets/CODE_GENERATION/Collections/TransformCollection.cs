using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[CreateAssetMenu(
	    fileName = "TransformCollection.asset",
	    menuName = SOArchitecture_Utility.COLLECTION_SUBMENU + "Transform",
	    order = 120)]
	public class TransformCollection : Collection<Transform>
	{
	}
}