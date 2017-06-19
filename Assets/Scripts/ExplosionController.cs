using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour {

	private ParticleSystem _particleSystem;

	// Use this for initialization
	void Start () {
		_particleSystem = gameObject.GetComponentInChildren<ParticleSystem> ();

	}

	void Awake() {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (_particleSystem == null)
			return;

		if (!_particleSystem.IsAlive())
			gameObject.SetActive (false);
	}
}
