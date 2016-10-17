using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/**
 * Represents a Unit that is being recruited in the Recruiting screen.
 **/
public class RecruitedUnitGroup : MonoBehaviour {

	public Text unitAmountText;
	public Image unitImage;

	private int unitAmount;
	public int UnitAmount
	{
		get { return unitAmount; }
		set { unitAmount = value; }
	}

	private ArmyType unitType;
	public ArmyType UnitType
	{
		get { return unitType; }
		set { unitType = value; }
	}

	// Use this for initialization
	void Start () {
		// unitAmountText = GetComponent<Text> () as Text; 
		// unitImage = GetComponent<Image> () as Image;
	}
	
	// Update is called once per frame
	void Update () {
		unitAmountText.text = "" + unitAmount;
	}

	public void AddUnits(int units){
		unitAmount += units;
	}

	public void AddUnit(){
		unitAmount++;
	}

	public void RemoveUnits(int units){
		unitAmount -= units;
		(FindObjectOfType<RecruitmentManager>() as RecruitmentManager).OnRemoveUnits (this, units);
	}

	public void RemoveUnit(){
		/**
		unitAmount--;
		(FindObjectOfType<RecruitmentManager>() as RecruitmentManager).OnRemoveUnit (this);
		*/
		int unitsToRemove = (FindObjectOfType<RecruitmentManager> () as RecruitmentManager).GetUnitsPerClick ();
		if(unitsToRemove > unitAmount){
			unitsToRemove = unitAmount;
		}
		RemoveUnits (unitsToRemove);
	}

	/* public void Touched(){
		RemoveUnit();

		if(unitAmount == 0){
			Destroy(this);
		}
	}*/
}
