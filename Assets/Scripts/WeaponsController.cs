using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour {

	private List<ShipWeapon> _weapons;

	// Inizializzo le armi
	public void Init (WeaponsSystemData data) {

		_weapons = new List<ShipWeapon> ();

		foreach (WeaponData wd in data.weapons) {
			ShipWeapon sw = new ShipWeapon ();
			sw.data = wd;
			Transform[] allChildren = gameObject.GetComponentsInChildren<Transform>();
			foreach (Transform t in allChildren) {
				if (t.gameObject.tag == sw.data.tag)
					sw.spawnPoints.Add (t.gameObject);
			}
			_weapons.Add (sw);
		}
	}

	// Durante il rendering di ogni frame...
	void Update () {

		foreach (ShipWeapon sw in _weapons) {
			// ... diminuisco il tempo che rimane per riutilizzare la seconda arma,
			sw.timeToNextFire -= Time.deltaTime;

			// ... controllo il fuoco delle armi
			CheckWeaponFire (sw);
		}
	}

	// Funzione che controlla il fuoco delle armi
	private void CheckWeaponFire(ShipWeapon weapon) {
		switch (weapon.data.fireRate) {
		case FireRateType.Auto:
			CheckAutoFire (weapon);
			break;
		case FireRateType.Multiple:
			CheckMultipleFire (weapon);
			break;
		case FireRateType.Single:
			CheckSingleFire (weapon);
			break;
		default:
			break;
		}
	}

	private void GenerateBullets(ShipWeapon weapon) {
		// Crea una istanza del proiettile per ogni
		// spawn point (se è abilitato)
		foreach(GameObject spawn in weapon.spawnPoints) {
			if (!spawn.activeInHierarchy)
				continue;
			GameObject go = GameObject.Instantiate (weapon.data.weaponPrefab, spawn.transform.position, spawn.transform.rotation);
			go.name = weapon.data.name;
		}
	}

	private void CheckSingleFire(ShipWeapon weapon) {
		// Spara una singola volta per ogni volta che viene premuto
		// il tasto 'Space'
		if (Input.GetKeyDown (weapon.data.fireKeycode)) {
			GenerateBullets (weapon);
		}
	}

	private void CheckMultipleFire(ShipWeapon weapon) {
		// Spara se è intercorso il
		// tempo per sparare il successivo proiettile
		if (Input.GetKey (weapon.data.fireKeycode) && weapon.timeToNextFire <= 0) {
			GenerateBullets (weapon);

			// Inizializzo il contatore per il fuoco multiplo
			weapon.timeToNextFire = weapon.data.fireInterval;
		}

		// Se il tasto viene rilasciato, resetto il contatore del fuoco multiplo
		if (Input.GetKeyUp (weapon.data.fireKeycode)) {
			weapon.timeToNextFire = 0;
		}
	}

	private void CheckAutoFire(ShipWeapon weapon) {
		// Spara se è intercorso il
		// tempo per sparare il successivo proiettile
		if (weapon.timeToNextFire <= 0) {
			GenerateBullets (weapon);

			// Inizializzo il contatore per il fuoco multiplo
			weapon.timeToNextFire = weapon.data.fireInterval;
		}
	}
}