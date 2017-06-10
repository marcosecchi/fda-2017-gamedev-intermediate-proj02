using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Questo attributo permette di aggiungere un elemento al menu dell'Editor di Unity
[CreateAssetMenu(fileName="SpaceCraftData", menuName="FDA/SpaceCraftData", order=1)]
public class SpaceCraftScriptableObjects : ScriptableObject {

	// Attributo decoratore per creare un titolo nella finestra dell'Inspector
	[Header("SpaceCraft Data")]

	// L'accelerazione della navetta
	public float acceleration = 50f;

	// La massima inclinazione della navetta quando è in movimento orizzontale
	public float maxRoll = 30f;

	// Attributo decoratore per creare un titolo nella finestra dell'Inspector
	[Header("Weapon Data")]

	// Il prefab da instanziare quando si spara un proiettile con la prima arma
	public GameObject weapon1Projectile;

	// Il prefab da instanziare quando si spara un proiettile con la seconda arma
	public GameObject weapon2Projectile;

	// Il prefab da instanziare quando si spara un proiettile con la seconda arma
	public float weapon2FireDelta;
}
