using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Questo attributo permette di aggiungere un elemento al menu dell'Editor di Unity
[CreateAssetMenu(fileName="ShipData", menuName="FDA/ShipData", order=1)]
public class ShipDataScriptableObject : ScriptableObject {

	// Attributo decoratore per creare un titolo nella finestra dell'Inspector
	[Header("SpaceCraft Data")]

	public ShipSystemData shipData;

	// Attributo decoratore per creare un titolo nella finestra dell'Inspector
	[Header("Weapon Data")]

	public WeaponsSystemData weaponsData;

	// Attributo decoratore per creare un titolo nella finestra dell'Inspector
	[Header("Engine Data")]

	public EnginesSysyemData enginesData;

}