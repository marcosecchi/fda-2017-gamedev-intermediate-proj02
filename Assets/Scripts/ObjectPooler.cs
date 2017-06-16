using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : Singleton<ObjectPooler> {

	// Nascondo il costruttore
	protected ObjectPooler () {}

	// Questo dictionary contiene le liste degli oggetti, individuati dal
	// rispettivo prefab
	private Dictionary<GameObject, List<GameObject>> _dict = new Dictionary<GameObject, List<GameObject>>();

	// Ritorna un'istanza del prefab richiesto
	public GameObject GetPooledObject(GameObject prefab) {

		// Se il dizionario non contiene già il prefab richiesto,
		// creo la lista e la aggiungo al dizionario stesso
		if (!_dict.ContainsKey (prefab)) {
			_dict.Add (prefab, new List<GameObject> ());
		}

		// Dichiarazione della lista locale
		List<GameObject> list;
		// Recupera la lista dal dizionario ()
		_dict.TryGetValue (prefab, out list);

		// Cerco un'istanza nella lista che non sia attivo (cioè non visibile in scena)
		foreach (GameObject go in list) {
			if (!go.activeInHierarchy)
				return go;
		}
		// Se non l'ho trovato (cioè, se tutte le istanze disponibili sono in scena),
		// crea una nuova istanza, la disabilito e la aggiungo alla lista
		GameObject newGo = GameObject.Instantiate (prefab);
		newGo.SetActive (false);
		list.Add (newGo);

		// ritorno l'oggetto appena creato
		return newGo;
	}

	// Permette di pre-creare una serie di istanze (ad esempio in fase di inizializzazione)
	public void InitPrefabInstances(GameObject prefab, int numInstances) {
		for (int i = 0; i < numInstances; i++) {
			GetPooledObject (prefab);
		}
	}

}
