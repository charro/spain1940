﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGameManager : MonoBehaviour {

	private static SaveGameManager instance;
	private SaveGameData gameData = new SaveGameData();

	private static string saveFileName = "/savegame.sav";

	void Awake(){
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

	public static void Delete(){
		instance.DeleteSave ();
	}


	public void SaveGame(){
		BinaryFormatter formatter = new BinaryFormatter ();
		FileStream saveGameFile = File.OpenWrite (Application.persistentDataPath + saveFileName);

		GameManager gameManager = FindObjectOfType<GameManager> ();
		// Add general game data
		gameData.turnNumber = gameManager.GetCurrentTurnNumber();

		// Add all Regions data
		gameData.regions.Clear ();
		Dictionary<RegionType, Region> allRegions = gameManager.GetAllRegions ();

		foreach (Region region in allRegions.Values) {
			gameData.regions.Add (new SavedRegion (region));
		}

		// Add economy data
		FindObjectOfType<EconomyManager>().FillSaveGameData (gameData);

		// Add technologies data
		FindObjectOfType<ResearchManager>().FillSaveGameData (gameData);

		formatter.Serialize (saveGameFile, gameData);
		saveGameFile.Close ();
	}

	public void LoadGame(){
		String filePath = Application.persistentDataPath + saveFileName;
		if (File.Exists (filePath)) {
			BinaryFormatter formatter = new BinaryFormatter ();
			FileStream saveGameFile = File.OpenRead (filePath);

			gameData = (SaveGameData)formatter.Deserialize (saveGameFile);
			saveGameFile.Close ();

			// If we're in gameplay stage, let's update all gameplay data
			if(SceneManager.GetActiveScene ().name == "ejpanya") {
				GameManager gameManager = FindObjectOfType<GameManager> ();
				// Restore Game data
				gameManager.RestoreDataFromSaveGame (gameData);

				// Restore Economy data
				FindObjectOfType<EconomyManager> ().RestoreDataFromSaveGame (gameData);

				// Restore Technologies data
				FindObjectOfType<ResearchManager> ().RestoreDataFromSaveGame (gameData);

				// Refresh and go to the main map
				gameManager.ShowMapAndHUD();
			}

		} else {
			gameData = new SaveGameData ();
		}

	}

	public void DeleteSave (){
		String filePath = Application.persistentDataPath + saveFileName;

		if (File.Exists (filePath)) {
			File.Delete (filePath);
		}
	}

	public enum eventFlagStatus {
		NOT_DONE = 0,
		DONE = 1,
		DONE_AND_NOTIFIED=2
	}

	[Serializable]
	public class SaveGameData {
		public bool tutoDone = false;
		public eventFlagStatus firstRegionRecovered = eventFlagStatus.NOT_DONE;
		public eventFlagStatus firstRegionLost = eventFlagStatus.NOT_DONE;

		// Game data
		public int turnNumber = 0;
		// Region data
		public List<SavedRegion> regions = new List<SavedRegion>();

		// Economy data
		public int militaryPoints;
		public int availableActionPointsForThisTurn;
		public int maximumActionsPerTurn;
		public int totalActionGenerationPoints;
		public int totalMilitaryGenerationPoints;

		// Technology data
		public TechnologyType currentResearchedTechnology = TechnologyType.None;
		public int turnsToEndResearchingCurrentTech = 0;
		public List<SavedTechnology> savedTechnologies = new List<SavedTechnology>();
	}

	[Serializable]
	public class SavedRegion {
		public bool selected;
		public bool isNazi;
		public bool enabledRegion;
		public int actionGenerationLevel;
		public int militaryLevel;
		public RegionType regionType;
		public RegionArmySlot[] armySlots;
		public SavedSpiedRegionInfo spiedRegionInfo;

		public SavedRegion(Region region){
			selected = region.selected;
			isNazi = region.isNazi;
			enabledRegion = region.enabledRegion;
			actionGenerationLevel = region.actionGenerationLevel;
			militaryLevel = region.militaryLevel;
			regionType = region.regionType;
			armySlots = region.GetArmySlots();
			if(region.GetLastSpiedRegionInfo() != null){
				spiedRegionInfo = new SavedSpiedRegionInfo(region.GetLastSpiedRegionInfo());
			}
		}
	}

	[Serializable]
	public class SavedSpiedRegionInfo {
		public ArmyType[] spiedArmyTypes;
		public int[] spiedArmyAmounts;
		public int spiedTurnNumber;

		public SavedSpiedRegionInfo(SpiedRegionInfo spiedInfo){
			spiedArmyTypes = spiedInfo.spiedArmyTypes;
			spiedArmyAmounts = spiedInfo.spiedArmyAmounts;
			spiedTurnNumber = spiedInfo.spiedTurnNumber;
		}
	}

	[Serializable]
	public class SavedTechnology {
		public TechnologyType type;
		public bool alreadyResearched = false;

		public SavedTechnology(Technology technology){
			type = technology.technologyType;
			alreadyResearched = technology.alreadyResearched;
		}
	}

}