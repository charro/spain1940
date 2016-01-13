using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RecruitListItem : MonoBehaviour {

	public ArmyType armyType;
	public TechnologyType requiredTechnology;
	public Text itemNameText;
	public Button itemButton;
	public Text priceText;
	public Image militaryPointsImage;

	private Sprite originalSprite;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable(){
		int armyPrice = FindObjectOfType<ArmyValues>().armyPricesDictionary[armyType]; 
		priceText.text = "" + armyPrice;
	}

	public void EnableRecruitment(){
		itemNameText.gameObject.SetActive (true);
		priceText.gameObject.SetActive (true);
		militaryPointsImage.gameObject.SetActive (true);
		itemButton.image.sprite = originalSprite;
		itemButton.interactable = true;
	}

	public void DisableRecruitment(){
		itemNameText.gameObject.SetActive (false);
		priceText.gameObject.SetActive (false);
		militaryPointsImage.gameObject.SetActive (false);
		originalSprite = itemButton.image.sprite;
		itemButton.image.sprite = FindObjectOfType<RecruitmentManager> ().unresearchedArmySprite;
		itemButton.interactable = false;
	}
}
