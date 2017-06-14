using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

	// I dati della navetta
	public ShipDataScriptableObject data;

	public GameObject modelContainer;

	private ShipSystemData _shipData;

	private WeaponsController _weaponsController;


	void Start () {
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
				if (i <= shipMaterials.Length) {
					// ...assegno il colore
					shipMaterials [i].color = _shipData.shipColors [i];
				}
			}
		}

		WeaponsController _weaponsController = gameObject.GetComponent<WeaponsController> ();
		if (_weaponsController != null)
			_weaponsController.Init (data.weaponsData);
		
	}

	// Funzione che controlla il movimento
	private void CheckMovement () {

		// Recupero il movimento orizzontale del joystick o delle frecce
		// e le moltiplico per l'accelerazione della navetta
		float vMove = Input.GetAxis ("Vertical") * _shipData.acceleration;
		float hMove = Input.GetAxis ("Horizontal") * _shipData.acceleration;

		// Calcolo lo spostamento effettivo (orizzontale e verticale)
		// basato sul deltaTime, cioè il tempo intercorso dall'ultimo
		// frame renderizzato
		vMove *= Time.deltaTime;
		hMove *= Time.deltaTime;

		Vector3 nextPosition = new Vector3 (transform.position.x + hMove, 0, transform.position.z + vMove);
		Vector3 viewPos = Camera.main.WorldToViewportPoint (nextPosition);

		if (viewPos.x > 0.1f && viewPos.x < 0.9f && viewPos.y > 0.1f && viewPos.y < 0.9f) {	
			// Sposto la la navetta dei valori calcolati
			transform.Translate (hMove, 0, vMove);
		}

		// Calcolo l'inclinazione della navetta durante il movimento orizzontale
		float roll = - Input.GetAxis ("Horizontal") * _shipData.maxRoll;

		Quaternion rot = Quaternion.Euler(0, 0, roll);
		modelContainer.transform.rotation = rot;
	}
}