using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SpyingValues : MonoBehaviour {

	public int spyMaxLevel;
	public int[] turnsNeededPerLevel;
	public float[] findArmyTypeChancePerLevel;
	public float[] findArmyNumberChancePerLevel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// This is called from Unity Editor
	void OnValidate(){
		// Be sure that the arrays has the same length
		if(spyMaxLevel != turnsNeededPerLevel.Length){
			int[] newArray = new int[spyMaxLevel];
			for(int i=0; i<newArray.Length && i<turnsNeededPerLevel.Length; i++){
				newArray[i] = turnsNeededPerLevel[i];
			}
			turnsNeededPerLevel = newArray;
		}

		if(spyMaxLevel != findArmyTypeChancePerLevel.Length){
			float[] newArray = new float[spyMaxLevel];
			for(int i=0; i<newArray.Length && i<findArmyTypeChancePerLevel.Length; i++){
				newArray[i] = findArmyTypeChancePerLevel[i];
			}
			findArmyTypeChancePerLevel = newArray;
		}

		if(spyMaxLevel != findArmyNumberChancePerLevel.Length){
			float[] newArray = new float[spyMaxLevel];
			for(int i=0; i<newArray.Length && i<findArmyNumberChancePerLevel.Length; i++){
				newArray[i] = findArmyNumberChancePerLevel[i];
			}
			findArmyNumberChancePerLevel = newArray;
		}
	}

}
