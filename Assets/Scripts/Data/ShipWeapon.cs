using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShipWeapon {

	// I dati dell'arma recuperati dallo scriptable object
	public WeaponData data;

	// Temporizzatore per il fuoco multiplo
	public float timeToNextFire = 0;

	// I punti per la generazione dei proiettili
	public List<GameObject> spawnPoints = new List<GameObject>();
}
