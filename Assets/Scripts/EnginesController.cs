using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnginesController : MonoBehaviour {

	// Lista delle armi
	private List<ShipEngine> _engines;

	// Inizializzo le armi
	public void Init (EnginesSysyemData data) {

		// Inizializzo la lista
		_engines = new List<ShipEngine> ();

		// Cicla sulle armi recuperate dallo scriptable object
		// e successivamente recupera gli effetti particellari tramite i tag assegnati
		foreach (EngineData ed in data.engines) {
			ShipEngine se = new ShipEngine ();
			se.data = ed;
			Transform[] allChildren = gameObject.GetComponentsInChildren<Transform>();
			foreach (Transform t in allChildren) {
				if (t.gameObject.tag == se.data.tag) {
					ParticleSystem ps = t.GetComponentInChildren<ParticleSystem> ();
					if (ps != null) {
						se.particleSystems.Add (ps);
					}
				}
			}
			_engines.Add (se);
		}
	}

	// Aggiorna i sistemi particellari dei motori
	public void UpdateEngines(float vMove, float hMove) {

		// Per ogni blocco di motori, controllo la casistica di movimento
		// ed applico l'opportuna modifica alla quantità di particelle generate
		foreach (ShipEngine se in _engines) {
			if (se.data.type == EngineDirectionType.MoveUp && vMove >= 0) {
				UpdateParticleSystems (se, vMove);
			} else if (se.data.type == EngineDirectionType.MoveDown && vMove <= 0) {
				UpdateParticleSystems (se, Mathf.Abs (vMove));
			} else if (se.data.type == EngineDirectionType.MoveRight && hMove >= 0) {
				UpdateParticleSystems (se, hMove);
			} else if (se.data.type == EngineDirectionType.MoveLeft && hMove <= 0) {
				UpdateParticleSystems (se, Mathf.Abs (hMove));
			}
		}
	}

	// Aggiorna tutti i sistemi particellari di un determinato gruppo
	private void UpdateParticleSystems(ShipEngine shipEngine, float move) {
		// Per ogni effetto particellare nel sistema di motori...
		foreach (ParticleSystem ps in shipEngine.particleSystems) {
			// ...recupero il modulo principale delle particelle...
			ParticleSystem.MainModule mainModule = ps.main;
			// ...calcolo il numero di particelle prodotte...
			float lifetime = shipEngine.data.maxLifetime * move;
			// ... applicando il valore minimo, se necessario
			lifetime = lifetime > shipEngine.data.minLifetime ? lifetime : shipEngine.data.minLifetime;

			// Infine, impongo la nuova lifetime alle particelle
			mainModule.startLifetime = lifetime;
		}
	
	}
}
