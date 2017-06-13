using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShipWeapon {

	public WeaponData data;

	public float timeToNextFire = 0;

	public List<GameObject> spawnPoints = new List<GameObject>();
}
