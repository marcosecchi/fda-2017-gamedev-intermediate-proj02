using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="ShipData", menuName="FDA/ShipData", order=1)]
public class ShipDataScriptableObject : ScriptableObject {

	[Header("Ship Data")]
	public ShipSystemData shipSystemData;

	[Header("Weapons Data")]
	public WeaponsSystemData weaponsSystemData;

}
