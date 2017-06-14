using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour {

	private ShipWeapon _weapon1;
	private ShipWeapon _weapon2;

	// Inizializzo le armi
	public void Init (WeaponsSystemData data) {
		_weapon1 = new ShipWeapon ();
		_weapon2 = new ShipWeapon ();

		_weapon1.data = data.weapon1;
		_weapon2.data = data.weapon2;

	}

	// Durante il rendering di ogni frame...
	void Update () {

		// ... diminuisco il tempo che rimane per riutilizzare la seconda arma,
		_weapon1.timeToNextFire -= Time.deltaTime;
		_weapon2.timeToNextFire -= Time.deltaTime;

		// ... controllo il fuoco delle armi
		CheckWeaponFire (_weapon1);
		CheckWeaponFire (_weapon2);
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

	private void CheckSingleFire(ShipWeapon weapon) {
		// Spara una singola volta per ogni volta che viene premuto
		// il tasto 'Space'
		if (Input.GetKeyDown (weapon.data.fireKeycode)) {
			// Crea una istanza del proiettile e la rinomina
			GameObject go = GameObject.Instantiate (weapon.data.weaponPrefab, transform.position, Quaternion.identity);
			go.name = weapon.data.name;
		}
	}

	private void CheckMultipleFire(ShipWeapon weapon) {
		// Spara se è intercorso il
		// tempo per sparare il successivo proiettile
		if (Input.GetKey (weapon.data.fireKeycode) && weapon.timeToNextFire <= 0) {
			// Crea una istanza del secondo proiettile e la rinomina
			GameObject go = GameObject.Instantiate (weapon.data.weaponPrefab, transform.position, Quaternion.identity);
			go.name = weapon.data.name;

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
			// Crea una istanza del secondo proiettile e la rinomina
			GameObject go = GameObject.Instantiate (weapon.data.weaponPrefab, transform.position, Quaternion.identity);
			go.name = weapon.data.name;

			// Inizializzo il contatore per il fuoco multiplo
			weapon.timeToNextFire = weapon.data.fireInterval;
		}
	}
}