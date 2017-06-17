using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Questo attributo ci assicura che la classe
// sia serializzabile dall'Editor di Unity
[System.Serializable]
public class EngineData {

	// il nome del motore
	public string name;

	// L'etichetta del motore
	public string tag;

	// Il tipo di motore
	public EngineDirectionType type;

	// La durata delle particelle del motore
	public float maxLifetime = 0.5f;

	// La durata minima delle particelle del motore
	public float minLifetime = 0.05f;

}

// Enumera i tipi di motore
public enum EngineDirectionType {
	MoveUp,
	MoveDown,
	MoveLeft,
	MoveRight
}