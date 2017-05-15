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

	public static bool isGroundTroop(ArmyType type){
		return type == ArmyType.Milicia ||
				type == ArmyType.TankBisonte ||
				type == ArmyType.TankLince ||
				type == ArmyType.TankToro ||
				type == ArmyType.NaziTroop ||
				type == ArmyType.NaziTiger ||
				type == ArmyType.NaziPanzer1;
	}

	public static bool isAirTroop(ArmyType type){
		return type == ArmyType.NaziBf109 ||
				type == ArmyType.NaziMe262 ||
				type == ArmyType.BomberQuebrantahuesos ||
				type == ArmyType.FighterAzor ||
				type == ArmyType.FighterBomberHalcon;
	}

	public int GetTotalDefense(Region thisArmyRegion, ArmyType attackerArmy){
		return defense + FindObjectOfType<ResearchManager> ().GetAdditionalDefenseForArmyRegionAndAttacker (armyType, thisArmyRegion, attackerArmy);
	}

	public int GetTotalAttack(ArmyType defenderArmy){
		return attack + FindObjectOfType<ResearchManager> ().GetAdditionalAttackForArmyAndDefender (armyType, defenderArmy);
	}

	// GAME COMBAT DEFENSE BALANCE BY ARMY VS ARMY TYPES
	public static int GetAdditionalDefenseForArmyType(ArmyType defender, ArmyType attacker){
		int additionalDefense = 0;

		switch (defender) {
		case ArmyType.Antiaereo:
			if (isAirTroop (attacker)) {
				additionalDefense += 3;
			}
			break;

		case ArmyType.BomberQuebrantahuesos:
			if (isGroundTroop (attacker)) {
				additionalDefense += 2;
			}
			break;

		case ArmyType.FighterAzor:
			if (isGroundTroop (attacker)) {
				additionalDefense += 1;
			}
			break;

		case ArmyType.FighterBomberHalcon:
			if (isGroundTroop (attacker)) {
				additionalDefense += 3;
			}
			break;

		case ArmyType.Milicia:
			// No advantages
			break;

		case ArmyType.TankBisonte:
			if (attacker == ArmyType.NaziTroop) {
				additionalDefense += 3;
			}
			if (attacker == ArmyType.NaziTiger) {
				additionalDefense += 1;
			}
			break;

		case ArmyType.TankLince:
			if (attacker == ArmyType.NaziTroop) {
				additionalDefense += 1;
			}
			break;

		case ArmyType.TankToro:
			if (attacker == ArmyType.NaziTroop) {
				additionalDefense += 2;
			}
			break;

		case ArmyType.NaziTroop:
			// No advantages
			break;

		case ArmyType.NaziBf109:
			if (isGroundTroop (attacker)) {
				additionalDefense += 1;
			}
			break;

		case ArmyType.NaziMe262:
			if (isGroundTroop (attacker)) {
				additionalDefense += 3;
			}
			break;

		case ArmyType.NaziTiger:
			if (attacker == ArmyType.Milicia) {
				additionalDefense += 1;
			}
			break;

		case ArmyType.NaziPanzer1:
			if (attacker == ArmyType.Milicia) {
				additionalDefense += 3;
			}
			if (attacker == ArmyType.TankLince) {
				additionalDefense += 1;
			}
			break;

		}

		return additionalDefense;
	}


	// GAME COMBAT ATTACK BALANCE BY ARMY VS ARMY TYPES
	public static int GetAdditionalAttackForArmyType(ArmyType attacker, ArmyType defender){
		int additionalAttack = 0;

		switch (attacker) {
		case ArmyType.Antiaereo:
			if (isAirTroop (defender)) {
				additionalAttack += 5;
			}
			break;

		case ArmyType.BomberQuebrantahuesos:
			if (isGroundTroop (defender)) {
				additionalAttack += 4;
			}
			break;

		case ArmyType.FighterAzor:
			if (isGroundTroop (defender)) {
				additionalAttack += 1;
			}
			if (isAirTroop (defender)) {
				additionalAttack += 4;
			}
			break;

		case ArmyType.FighterBomberHalcon:
			if (isGroundTroop (defender)) {
				additionalAttack += 3;
			}
			if (isAirTroop (defender)) {
				additionalAttack += 2;
			}
			break;

		case ArmyType.Milicia:
			// No advantages
			break;

		case ArmyType.TankBisonte:
			if (defender == ArmyType.NaziTroop) {
				additionalAttack += 4;
			}
			if (defender == ArmyType.NaziTiger) {
				additionalAttack += 1;
			}
			break;

		case ArmyType.TankLince:
			if (defender == ArmyType.NaziTroop) {
				additionalAttack += 2;
			}
			break;

		case ArmyType.TankToro:
			if (defender == ArmyType.NaziTroop) {
				additionalAttack += 3;
			}
			break;

		case ArmyType.NaziTroop:
			// No advantages
			break;

		case ArmyType.NaziBf109:
			if (isGroundTroop (defender)) {
				additionalAttack += 1;
			}
			break;

		case ArmyType.NaziMe262:
			if (isGroundTroop (defender)) {
				additionalAttack += 2;
			}
			if (isAirTroop (defender)) {
				additionalAttack += 3;
			}
			break;

		case ArmyType.NaziTiger:
			if (defender == ArmyType.Milicia) {
				additionalAttack += 1;
			}
			break;

		case ArmyType.NaziPanzer1:
			if (defender == ArmyType.Milicia) {
				additionalAttack += 3;
			}
			if (defender == ArmyType.TankLince) {
				additionalAttack += 1;
			}
			break;

		}

		return additionalAttack;
	}
}