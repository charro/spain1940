using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RecruitmentManager : MonoBehaviour {

	public int[] UNIT_TYPE_PRICES = {25		// Tank
									 ,10	// Soldier
									};

	public Text recruitmentPointsText;
	public RectTransform recruitmentPanel;

	public GameObject unitGroupPrefab;
	
	private ArrayList recruitedUnitGroups = new ArrayList();
	
	// Use this for initialization
	void Start () {
		ResetUnits ();
	}
	
	// Update is called once per frame
	void Update () {
		recruitmentPointsText.text = "" + EconomyManager.getRecruitmentPoints();
	}

	public void AddToUnit(int unitType){

		GameObject selectedUnitGroupGameObject = null;

		foreach(GameObject unitGroupGameObject in recruitedUnitGroups){
			RecruitedUnitGroup recruitedUnitGroup = unitGroupGameObject.GetComponent<RecruitedUnitGroup>() as RecruitedUnitGroup;

			if(recruitedUnitGroup.UnitType == unitType){
				selectedUnitGroupGameObject = unitGroupGameObject;
				break;
			}
		}

		if(selectedUnitGroupGameObject == null){
			GameObject instance = Instantiate (unitGroupPrefab) as GameObject;
			instance.transform.SetParent (recruitmentPanel.transform, false);

			RecruitedUnitGroup recruitedUnitGroup = instance.GetComponent<RecruitedUnitGroup>();

			recruitedUnitGroup = new RecruitedUnitGroup(unitType);
			recruitedUnitGroups.Add(instance);
		}

		RecruitedUnitGroup selectedUnitGroup = selectedUnitGroupGameObject.GetComponent<RecruitedUnitGroup>() as RecruitedUnitGroup;
		selectedUnitGroup.AddUnit ();
	}

	public void ResetUnits(){
		foreach (GameObject unitGroupGameObject in recruitedUnitGroups) {
			Destroy(unitGroupGameObject);
		}

		// recruitedUnitGroups = new ArrayList ();
	}
	
}
