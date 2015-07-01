using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RecruitedUnitGroup : MonoBehaviour {

	public Text unitAmountText;
	public Image unitImage;

	private int unitAmount;
	public int UnitAmount
	{
		get { return unitAmount; }
		set { unitAmount = value; }
	}

	private int unitType;
	public int UnitType
	{
		get { return unitType; }
		set { unitType = value; }
	}

	public RecruitedUnitGroup(int unitType){
		this.unitType = unitType;
	}

	public void AddUnit(){
		unitAmount++;
	}

	public void RemoveUnit(){
		unitAmount--;

		if(unitAmount < 0){
			unitAmount = 0;
		}
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

}
