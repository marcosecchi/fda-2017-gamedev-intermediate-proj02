using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	// La velocità del proiettile
	public float speed = 30f;

	public GameObject explosionPrefab;

	public KeyCode activationKey;

	void Update () {
		// Ad ogni update sposto il proiettile verso l'alto (asse z)
		// basandomi sul deltaTime
		transform.Translate(0, 0, speed * Time.deltaTime);

		if (Input.GetKeyDown (activationKey)) {
			GameObject explosion =  ObjectPooler.Instance.GetPooledObject (explosionPrefab);
			explosion.transform.position = transform.position;
			explosion.SetActive (true);
		}
	}

	// Quando il renderer dell'oggetto esce dalla vista della
	// Camera, l'oggetto viene reso invisibile
	void OnBecameInvisible() {
		gameObject.SetActive (false);
	}

	void OnCollisionEnter(Collision collision) {
		Debug.Log ("Hit: " + collision.gameObject);
	}
}
