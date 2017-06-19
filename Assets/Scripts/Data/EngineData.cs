using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EngineData {

	public string name;

	public string tag;

	public float maxLifetime = 0.5f;

	public float minLifetime = 0.5f;

	public EngineDirectionType type;
}

public enum EngineDirectionType {
	MoveUp,
	MoveDown,
	MoveLeft,
	MoveRight
}