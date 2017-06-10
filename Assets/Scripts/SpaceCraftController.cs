using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCraftController : MonoBehaviour {

	// Lo ScriptableObject contenente i dati di configurazione della navetta
	public SpaceCraftScriptableObject data;

	// Contatore per il fuoco multiplo della seconda arma
	private float _timeToNextFire = 0;

	public GameObject modelContainer;

	// Durante il rendering di ogni frame...
	void Update() {

		// ... controllo il movimento,
		CheckMovement ();

		// ... diminuisco il tempo che rimane per riutilizzare la seconda arma,
		_timeToNextFire -= Time.deltaTime;

		// ... ed infine controllo il fuoco delle armi
		CheckWeaponsFire ();
	}
		
	// Funzione che controlla il fuoco delle armi
	private void CheckWeaponsFire() {

		// Spara una singola volta per ogni volta che viene premuto
		// il tasto 'Space'
		if (Input.GetKeyDown (KeyCode.Space)) {
			// Crea una istanza del primo proiettile e la rinomino
			GameObject go = GameObject.Instantiate (data.weapon1Projectile, transform.position, Quaternion.identity);
			go.name = "Weapon 1 Projectile";
		}

		// Spara ad ogni frame se 'X' è premuto e se è intercorso il
		// tempo per sparare unsafe successivo proiettile
		if (Input.GetKey (KeyCode.X) && _timeToNextFire <= 0) {
			// Crea una istanza del secondo proiettile e la rinomino
			GameObject go = GameObject.Instantiate (data.weapon2Projectile, transform.position, Quaternion.identity);
			go.name = "Weapon 2 Projectile";

			// Inizializzo il contatore per il fuoco multiplo
			_timeToNextFire = data.weapon2FireDelta;
		}

		// Se il tasto 'X' viene rilasciato, resetto il contatore del fuoco multiplo
		if (Input.GetKeyUp (KeyCode.X)) {
			_timeToNextFire = 0;
		}

	}

	// Funzione che controlla il movimento
	private void CheckMovement () {

		// Recupero il movimento orizzontale del joystick o delle frecce
		// e le moltiplico per l'accelerazione della navetta
		float vMove = Input.GetAxis ("Vertical") * data.acceleration;
		float hMove = Input.GetAxis ("Horizontal") * data.acceleration;

		// Calcolo lo spostamento effettivo (orizzontale e verticale)
		// basato sul deltaTime, cioè il tempo intercorso dall'ultimo
		// frame renderizzato
		vMove *= Time.deltaTime;
		hMove *= Time.deltaTime;

		// Sposto la la navetta dei valori calcolati
		transform.Translate (hMove, 0, vMove);


		// Calcolo l'inclinazione della navetta durante il movimento orizzontale
		float roll = - Input.GetAxis ("Horizontal") * data.maxRoll;

		Quaternion rot = Quaternion.Euler(0, 0, roll);
		modelContainer.transform.rotation = rot;
	}
}
