using UnityEngine;

public class SpiedRegionInfo: ScriptableObject
{
	public ArmyType[] spiedArmyTypes;
	public int[] spiedArmyAmounts;

	public void Initialize(Region spiedRegion,  float chanceOfFindingArmyType, float chanceOfFindingArmyAmount){
		int maxArmySlots = spiedRegion.GetMaxSlots ();
		spiedArmyTypes = new ArmyType[maxArmySlots];
		spiedArmyAmounts = new int[maxArmySlots];

		// Find the spied values
		for(int i=0; i<maxArmySlots; i++){
			spiedArmyTypes[i] = FindArmyType(spiedRegion, i, chanceOfFindingArmyType);
			spiedArmyAmounts[i] = FindArmyAmount(spiedRegion, i, chanceOfFindingArmyAmount);
		}
	}

	private ArmyType FindArmyType(Region region, int slotNumber, float chanceOfFindingArmyType){
		int randomNumber = Random.Range (0, 100);
		if(randomNumber <= chanceOfFindingArmyType*100){
			return region.GetArmySlots()[slotNumber].armyType;
		}
		else{
			return ArmyType.Unknown;
		}
	}

	private int FindArmyAmount(Region region, int slotNumber, float chanceOfFindingArmyAmount){
		int randomNumber = Random.Range (0, 100);
		if(randomNumber <= chanceOfFindingArmyAmount*100){
			return region.GetArmySlots()[slotNumber].armyAmount;
		}
		else{
			return 0;
		}
	}

}

