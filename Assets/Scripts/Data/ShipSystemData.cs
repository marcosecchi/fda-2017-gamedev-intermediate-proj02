using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShipSystemData {

	public string name;

	public float acceleration = 40f;

	public float maxRoll = 30f;

	public GameObject shipModelPrefab;

	public Color[] shipColors;
}
