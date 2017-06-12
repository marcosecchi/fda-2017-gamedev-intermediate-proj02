using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Questo attributo ci assicura che la classe
// sia serializzabile dall'Editor di Unity
[System.Serializable]
public class ShipSystemData {

	public string modelName;

	// L'accelerazione della navetta
	public float acceleration = 50f;

	// La massima inclinazione della navetta quando è in movimento orizzontale
	public float maxRoll = 30f;

	public GameObject shipPrefab;

	public Color[] shipColors;
}