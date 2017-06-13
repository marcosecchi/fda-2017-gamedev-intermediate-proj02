using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour {

	private WeaponData _weapon1Data;
	private WeaponData _weapon2Data;

	private List<GameObject> _weapon1SpawnPoints;
	private List<GameObject> _weapon2SpawnPoints;

	private float _timeToNextFire1 = 0;
	private float _timeToNextFire2 = 0;

	// Inizializzo le armi
	public void Init (WeaponsSystemData data) {
		_weapon1Data = data.weapon1;
		_weapon2Data = data.weapon2;

		_weapon1SpawnPoints = new List<GameObject> ();
		_weapon2SpawnPoints = new List<GameObject> ();

		Transform[] allChildren = gameObject.GetComponentsInChildren<Transform>();
		foreach (Transform t in allChildren) {
			if (t.gameObject.tag == _weapon1Data.tag)
				_weapon1SpawnPoints.Add (t.gameObject);
			else if (t.gameObject.tag == _weapon2Data.tag)
				_weapon2SpawnPoints.Add (t.gameObject);
		}
	}

	// Durante il rendering di ogni frame...
	void Update () {

		// ... diminuisco il tempo che rimane per riutilizzare la seconda arma,
		_timeToNextFire1 -= Time.deltaTime;
		_timeToNextFire2 -= Time.deltaTime;

		// ... controllo il fuoco delle armi
		CheckWeaponFire (_weapon1Data, _weapon1SpawnPoints);
		CheckWeaponFire (_weapon2Data, _weapon2SpawnPoints);
	}

	// Funzione che controlla il fuoco delle armi
	private void CheckWeaponFire(WeaponData data, List<GameObject> spawnPoints) {
		switch (data.fireRate) {
		case FireRateType.Auto:
			CheckAutoFire (data, spawnPoints);
			break;
		case FireRateType.Multiple:
			CheckMultipleFire (data, spawnPoints);
			break;
		case FireRateType.Single:
			CheckSingleFire (data, spawnPoints);
			break;
		default:
			break;
		}
	}

	private void CheckSingleFire(WeaponData data, List<GameObject> spawnPoints) {
		// Spara una singola volta per ogni volta che viene premuto
		// il tasto 'Space'
		if (Input.GetKeyDown (data.fireKeycode)) {
			// Crea una istanza del proiettile e la rinomina
			foreach(GameObject spawn in spawnPoints) {
				GameObject go = GameObject.Instantiate (data.weaponPrefab, spawn.transform.position, spawn.transform.rotation);
				go.name = data.name;
			}
		}
	}

	private void CheckMultipleFire(WeaponData data, List<GameObject> spawnPoints) {
		// Spara se è intercorso il
		// tempo per sparare il successivo proiettile
		if (Input.GetKey (data.fireKeycode) && data.timeToNextFire <= 0) {
			// Crea una istanza del secondo proiettile e la rinomina
			foreach(GameObject spawn in spawnPoints) {
				GameObject go = GameObject.Instantiate (data.weaponPrefab, spawn.transform.position, spawn.transform.rotation);
				go.name = data.name;
			}

			// Inizializzo il contatore per il fuoco multiplo
			data.timeToNextFire = data.fireInterval;
		}

		// Se il tasto viene rilasciato, resetto il contatore del fuoco multiplo
		if (Input.GetKeyUp (data.fireKeycode)) {
			data.timeToNextFire = 0;
		}
	}

	private void CheckAutoFire(WeaponData data, List<GameObject> spawnPoints) {
		// Spara se è intercorso il
		// tempo per sparare il successivo proiettile
		if (data.timeToNextFire <= 0) {
			// Crea una istanza del secondo proiettile e la rinomina
			foreach(GameObject spawn in spawnPoints) {
				GameObject go = GameObject.Instantiate (data.weaponPrefab, spawn.transform.position, spawn.transform.rotation);
				go.name = data.name;
			}

			// Inizializzo il contatore per il fuoco multiplo
			data.timeToNextFire = data.fireInterval;
		}
	}
}