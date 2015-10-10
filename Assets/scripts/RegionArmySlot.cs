using UnityEngine;
/**
 * Represents a Slot belonging to a Region, that can host an army
 * */
public class RegionArmySlot
{
	public ArmyType armyType;
	public int armyAmount;

	public RegionArmySlot ()
	{
		armyType = ArmyType.Empty;
		armyAmount = 0;
	}

	public void addUnit(){
		addUnits (1);
	}

	public void addUnits(int unitsNumber){
		armyAmount += unitsNumber;
	}

	public void removeUnit(){
		removeUnits(1);
	}

	public void removeUnits(int unitsNumber){
		if (armyAmount < unitsNumber) {
			Debug.Log ("ERROR: TRYING TO REMOVE " + unitsNumber + " FROM REGION ARMY SLOT OF TYPE " + armyType +
								" WHERE THERE ARE ONLY " + armyAmount + " UNITS");
		} else {
			armyAmount -= unitsNumber;
		}

		if(armyAmount == 0){
			armyType = ArmyType.Empty;
		}
	}

	public void CleanSlot(){
		armyType = ArmyType.Empty;
		armyAmount = 0;
	}

	public virtual string ToString(){
		return "["+ armyType +"]:[units="+armyAmount+"]";
	}
}