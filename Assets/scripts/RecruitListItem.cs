using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RecruitListItem : MonoBehaviour {

	public Army army;

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
		int armyPrice = army.price; 
		priceText.text = "" + armyPrice;
	}

	public void EnableRecruitment(){
		itemNameText.gameObject.SetActive (true);
		priceText.gameObject.SetActive (true);
		militaryPointsImage.gameObject.SetActive (true);
		itemButton.image.sprite = army.sprite;
		itemButton.interactable = true;
	}

	public void DisableRecruitment(){
		itemNameText.gameObject.SetActive (false);
		priceText.gameObject.SetActive (false);
		militaryPointsImage.gameObject.SetActive (false);
		//originalSprite = itemButton.image.sprite;
		itemButton.image.sprite = FindObjectOfType<RecruitmentManager> ().unresearchedArmySprite;
		itemButton.interactable = false;
	}

	public void ItemClicked(){
		FindObjectOfType<RecruitmentManager>().AddToUnit(army.armyType);
	}
}
