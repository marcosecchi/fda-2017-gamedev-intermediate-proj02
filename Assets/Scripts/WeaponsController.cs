using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour {

	private WeaponData _weapon1Data;

	private WeaponData _weapon2Data;

	// Inizializzo le armi
	public void Init (WeaponsSystemData data) {
		_weapon1Data = data.weapon1;

		_weapon2Data = data.weapon2;
	}

	// Durante il rendering di ogni frame...
	void Update () {

		// ... diminuisco il tempo che rimane per riutilizzare la seconda arma,
		_weapon1Data.timeToNextFire -= Time.deltaTime;
		_weapon2Data.timeToNextFire -= Time.deltaTime;

		// ... controllo il fuoco delle armi
		CheckWeaponFire (_weapon1Data);
		CheckWeaponFire (_weapon2Data);
	}

	// Funzione che controlla il fuoco delle armi
	private void CheckWeaponFire(WeaponData data) {
		switch (data.fireRate) {
		case FireRateType.Auto:
			CheckAutoFire (data);
			break;
		case FireRateType.Multiple:
			CheckMultipleFire (data);
			break;
		case FireRateType.Single:
			CheckSingleFire (data);
			break;
		default:
			break;
		}
	}

	private void CheckSingleFire(WeaponData data) {
		// Spara una singola volta per ogni volta che viene premuto
		// il tasto 'Space'
		if (Input.GetKeyDown (data.fireKeycode)) {
			// Crea una istanza del proiettile e la rinomina
			GameObject go = GameObject.Instantiate (data.weaponPrefab, transform.position, Quaternion.identity);
			go.name = data.name;
		}
	}

	private void CheckMultipleFire(WeaponData data) {
		// Spara se è intercorso il
		// tempo per sparare il successivo proiettile
		if (Input.GetKey (data.fireKeycode) && data.timeToNextFire <= 0) {
			// Crea una istanza del secondo proiettile e la rinomina
			GameObject go = GameObject.Instantiate (data.weaponPrefab, transform.position, Quaternion.identity);
			go.name = data.name;

			// Inizializzo il contatore per il fuoco multiplo
			data.timeToNextFire = data.fireInterval;
		}

		// Se il tasto viene rilasciato, resetto il contatore del fuoco multiplo
		if (Input.GetKeyUp (data.fireKeycode)) {
			data.timeToNextFire = 0;
		}
	}

	private void CheckAutoFire(WeaponData data) {
		// Spara se è intercorso il
		// tempo per sparare il successivo proiettile
		if (data.timeToNextFire <= 0) {
			// Crea una istanza del secondo proiettile e la rinomina
			GameObject go = GameObject.Instantiate (data.weaponPrefab, transform.position, Quaternion.identity);
			go.name = data.name;

			// Inizializzo il contatore per il fuoco multiplo
			data.timeToNextFire = data.fireInterval;
		}
	}
}