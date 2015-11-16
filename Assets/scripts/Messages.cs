using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Messages : MonoBehaviour {

	public GameObject whereToMoveText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void HideAll(){
		whereToMoveText.SetActive(false);
	}

	public void showWhereToMoveText(){
		UIManager.ShowMessagesPanel();
		whereToMoveText.SetActive(true);
	}

	public void showWhereToSpyText(){
		UIManager.ShowMessagesPanel();
		whereToMoveText.SetActive(true);
	}
}
