using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Questo attributo ci assicura che la classe
// sia serializzabile dall'Editor di Unity
[System.Serializable]
public class WeaponData {

	// il nome dell'arma
	public string name;

	// Il prefab del proiettile sparato
	public GameObject weaponPrefab;

	// La tipologia di fuoco
	public FireRateType fireRate;

	// Il pulsante della tastiera che attiva l'arma
	public KeyCode fireKeycode;

	// L'intervallo tra un proiettile ed il successivo
	// (Solitamente utilizzato nei casi di Multiple ed Auto)
	public float fireInterval = 0;

	// Questo valore non deve essere mostrato nell'editor
	[HideInInspector]
	public float timeToNextFire = 0;
}

// Enumera i tipi di fuoco
public enum FireRateType {
	Auto,
	Single,
	Multiple
}