using UnityEngine;
using System.Collections;

public class Army : MonoBehaviour {

	public ArmyType armyType;
	public TechnologyType requiredTechnology;
	public Sprite sprite;
	public string armyDescription;
	public int price;
	public int attack;
	public int defense;
	public int speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool isNazi(){
		return Army.isNazi(armyType);
	}

	public static bool isNazi(ArmyType type){
		return (int)type >= (int)ArmyType.NaziTroop;
	}
}