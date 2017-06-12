using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

	public GameObject modelContainer;

	public ShipDataScriptableObject data;

	void Start () {
		
		Init ();
		
	}
	
	void Update () {
		CheckMovement ();	
	}

	public void Init() {

		foreach (Transform t in modelContainer.transform) {
			Destroy (t.gameObject);
		}

		GameObject ship = Instantiate (data.shipSystemData.shipModelPrefab, modelContainer.transform);
	}

	void CheckMovement() {
		float vMove = Input.GetAxis ("Vertical") * data.shipSystemData.acceleration;
		float hMove = Input.GetAxis ("Horizontal") * data.shipSystemData.acceleration;

		vMove *= Time.deltaTime;
		hMove *= Time.deltaTime;

		transform.Translate (hMove, 0, vMove);

		float roll = Input.GetAxis ("Horizontal") * data.shipSystemData.maxRoll;

		Quaternion rot = Quaternion.Euler (0, 0, - roll);
		modelContainer.transform.rotation = rot;
	}
}
