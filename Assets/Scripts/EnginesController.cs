using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnginesController : MonoBehaviour {

	private List<ShipEngine> _engines;

	public void Init(EnginesSystemData data) {

		_engines = new List<ShipEngine> ();

		foreach (EngineData ed in data.engines) {
			ShipEngine se = new ShipEngine ();
			se.data = ed;

			Transform[] allChildren = gameObject.GetComponentsInChildren<Transform>();
			foreach (Transform t in allChildren) {
				if (t.gameObject.tag == se.data.tag) {
					ParticleSystem ps = t.GetComponentInChildren<ParticleSystem> ();
					if (ps != null)
						se.particleSystems.Add (ps);
				}
			}
			_engines.Add (se);
		}

	}

	public void UpdateEngines(float vMove, float hMove) {
		foreach (ShipEngine se in _engines) {
			if (se.data.type == EngineDirectionType.MoveUp && vMove >= 0) {
				UpdateParticleSystem (se, vMove);
			}
		}
	}

	private void UpdateParticleSystem(ShipEngine shipEngine, float move) {
		foreach (ParticleSystem ps in shipEngine.particleSystems) {
			ParticleSystem.MainModule mainModule = ps.main;

			float lifetime = shipEngine.data.maxLifetime * move;
			lifetime = lifetime > shipEngine.data.minLifetime ? lifetime : shipEngine.data.minLifetime;
		
			mainModule.startLifetime = lifetime;
		}
	}

}
