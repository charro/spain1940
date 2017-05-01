using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGameManager : MonoBehaviour {

	private static SaveGameManager instance;
	private SaveGameData gameData = new SaveGameData();

	void Start(){
		instance = this;
	}

	public static void Save(){
		instance.SaveGame ();
	}

	public static void Load(){
		instance.LoadGame ();
	}

	public static SaveGameData GetGameData(){
		return instance.gameData;
	}


	public void SaveGame(){
		BinaryFormatter formatter = new BinaryFormatter ();
		FileStream saveGameFile = File.OpenWrite (Application.persistentDataPath + "/savegame.sav");

		gameData.regions.Clear ();
		Dictionary<RegionType, Region> allRegions = FindObjectOfType<GameManager> ().GetAllRegions ();

		foreach (Region region in allRegions.Values) {
			gameData.regions.Add (new SavedRegion (region));
		}

		formatter.Serialize (saveGameFile, gameData);
		saveGameFile.Close ();
	}

	public void LoadGame(){
		String filePath = Application.persistentDataPath + "/savegame.sav";
		if (File.Exists (filePath)) {
			BinaryFormatter formatter = new BinaryFormatter ();
			FileStream saveGameFile = File.OpenRead (filePath);

			gameData = (SaveGameData)formatter.Deserialize (saveGameFile);
			saveGameFile.Close ();

			if(SceneManager.GetActiveScene ().name == "ejpanya") {
				//Restore region data
				Dictionary<RegionType, Region> allRegions = FindObjectOfType<GameManager> ().GetAllRegions ();

				foreach (SavedRegion savedRegion in gameData.regions) {
					Region region = allRegions[savedRegion.regionType];

					region.selected = savedRegion.selected;
					region.isNazi = savedRegion.isNazi;
					region.enabledRegion = savedRegion.enabledRegion;
					region.actionGenerationLevel = savedRegion.actionGenerationLevel;
					region.militaryLevel = savedRegion.militaryLevel;
					region.regionType = savedRegion.regionType;
				}
			}

		} else {
			gameData = new SaveGameData ();
		}


	}

	[Serializable]
	public class SaveGameData {
		public bool tutoDone = false;

		public List<SavedRegion> regions = new List<SavedRegion>();
	}

	[Serializable]
	public class SavedRegion {
		public bool selected;
		public bool isNazi;
		public bool enabledRegion;
		public int actionGenerationLevel;
		public int militaryLevel;
		public RegionType regionType;

		public SavedRegion(Region region){
			selected = region.selected;
			isNazi = region.isNazi;
			enabledRegion = region.enabledRegion;
			actionGenerationLevel = region.actionGenerationLevel;
			militaryLevel = region.militaryLevel;
			regionType = region.regionType;
		}
	}
		
}