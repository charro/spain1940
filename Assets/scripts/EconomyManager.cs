using UnityEngine;
using System.Collections;

public class EconomyManager : MonoBehaviour {
	
	public int INITIAL_RECRUITMENT_POINTS = 200;

	private static EconomyManager singleton;

	private int recruitmentPoints;

	// Use this for initialization
	void Start () {
		singleton = this;
		recruitmentPoints = INITIAL_RECRUITMENT_POINTS;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static int getRecruitmentPoints(){
		return singleton.recruitmentPoints;
	}

	public static void addRecruitmentPoints(int amount){
		singleton.recruitmentPoints += amount;
	}

	public static void decreaseRecruitmentPoints(int amount){
		singleton.recruitmentPoints -= amount;
	}
}
