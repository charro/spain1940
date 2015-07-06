using UnityEngine;
using System.Collections;

public class Region : MonoBehaviour {

	public bool selected;
	public bool isNazi;
	public Sprite spanishRegionSprite;
	public Sprite naziRegionSprite;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void toggleSelected(){
		selected = !selected;
		Animator animator = GetComponent<Animator>();
		animator.SetBool ("selected", selected);

		if(selected){
			UIManager.regionSelected();
			if(isNazi){
				UIManager.showMainEnemyActions();
			}
			else{
				UIManager.showMainActions();
			}
		}
		else{
			UIManager.regionUnselected();

			UIManager.hideMainActionPanels();
		}
	}

	public bool isSelected(){
		return selected;
	}

	public void SetNaziConquered(bool naziConquered){
		isNazi = naziConquered;
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		
		if(isNazi){
			spriteRenderer.sprite = naziRegionSprite;
		}
		else{
			spriteRenderer.sprite = spanishRegionSprite;
		}
	}
}
