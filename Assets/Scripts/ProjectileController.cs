using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

	// LA velocità del proiettile
	public float speed = 30f;

	void Update () {
		// Ad ogni update sposto il proiettile verso l'alto (asse z)
		// basandomi sul deltaTime
		transform.Translate(0, 0, speed * Time.deltaTime);	
	}

	// Quando il renderer dell'oggetto esce dalla vista della
	// Camera, l'oggetto viene distrutto
	void OnBecameInvisible() {
		Destroy (gameObject);
	}
}
