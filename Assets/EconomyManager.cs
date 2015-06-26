using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EconomyManager : MonoBehaviour {

	// TODO: Sustituir por un Script para cada Layer de UI
	// Cuando se actualice un valor, actualizamos los textos de cada layer
	public Text tankUnitText;

	private int tankUnits = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddTankUnits(){
		tankUnits++;

		tankUnitText.text = "" + tankUnits;
	}

}
