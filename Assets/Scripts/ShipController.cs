using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

	// I dati della navetta
	public ShipScriptableObject data;

	// Il contenitore dove inserire il modello della navetta
	public GameObject modelContainer;

	// Riferimento ai dati della navetta
	private ShipSystemData _shipData;

	// Riferimento al weapons controller
	private WeaponsController _weaponsController;

	void Start () {
		// Inizializzo i dati
		Init ();
	}

	// Durante il rendering di ogni frame...
	void Update () {
		// ... controllo il movimento,
		CheckMovement ();
	}

	// Inizializza la navetta
	public void Init() {

		_shipData = data.shipData;

		// Rimuovo tutti gli elementi all'interno del model container
		// Nel caso avessi già instanziato una navetta precedentemente
		foreach (Transform t in modelContainer.transform)
			Destroy (t.gameObject);

		// Instanzion il modello della navetta
		GameObject ship = GameObject.Instantiate (_shipData.shipPrefab, modelContainer.transform);
		ship.name = _shipData.modelName;

		// Se la navetta possiede un renderer, procedo alla sostituzione dei colori
		if (ship.GetComponentInChildren<Renderer> () != null) {
			// Recupero l'elenco dei materiali della navetta
			Material[] shipMaterials = ship.GetComponentInChildren<Renderer> ().materials;

			// Ciclo sui colori all'interno del mio ScriptableObject
			for (int i = 0; i < _shipData.shipColors.Length; i++) {
				// Se l'indice del colore che sto considerando è presente nella lista
				// dei materiali...
				if (i >= shipMaterials.Length)
					break;
				// ...assegno il colore
				shipMaterials [i].color = _shipData.shipColors [i];
			}
		}

		// Recupero il weapons controller, nel caso esista e lo inizializzo
		WeaponsController _weaponsController = gameObject.GetComponent<WeaponsController> ();
		if (_weaponsController != null)
			_weaponsController.Init (data.weaponsData);

	}

	// Funzione che controlla il movimento
	private void CheckMovement () {

		/* *** MOVIMENTO NAVETTA *** */

		// Recupero il movimento orizzontale del joystick o delle frecce
		// e le moltiplico per l'accelerazione della navetta
		float vMove = Input.GetAxis ("Vertical") * _shipData.acceleration;
		float hMove = Input.GetAxis ("Horizontal") * _shipData.acceleration;

		// Calcolo lo spostamento effettivo (orizzontale e verticale)
		// basato sul deltaTime, cioè il tempo intercorso dall'ultimo
		// frame renderizzato
		vMove *= Time.deltaTime;
		hMove *= Time.deltaTime;

		// Sposto la la navetta dei valori calcolati
		transform.Translate (hMove, 0, vMove);


		/* *** INCLINAZIONE NAVETTA *** */

		// Calcolo l'inclinazione della navetta durante il movimento orizzontale
		float roll = - Input.GetAxis ("Horizontal") * _shipData.maxRoll;

		// Ruoto il container del modello della navetta
		Quaternion rot = Quaternion.Euler(0, 0, roll);
		modelContainer.transform.rotation = rot;
	}
}