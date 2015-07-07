using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * Represents a Unit that is being recruited in the Recruiting screen
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

	public void AddUnit(){
		unitAmount++;
	}
	
	public void RemoveUnit(){
		unitAmount--;
		(FindObjectOfType<RecruitmentManager>() as RecruitmentManager).OnRemoveUnit (this);
	}

	/* public void Touched(){
		RemoveUnit();

		if(unitAmount == 0){
			Destroy(this);
		}
	}*/
}
