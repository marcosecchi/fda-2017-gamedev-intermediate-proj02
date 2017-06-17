using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShipEngine {

	// I dati del motore recuperati dallo scriptable object
	public EngineData data;

	// I sistemi particellari dei motori
	public List<ParticleSystem> particleSystems = new List<ParticleSystem>();
}
