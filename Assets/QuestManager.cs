
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour {
	public List <ChapterOfQuest> chapters;

	void Start () {
//		List<int> areaLines = new List<int> ();
//		areaLines.Add (1);
//		areaLines.Add (6);
//		areaLines.Add (12);
//		areaLines.Add (18);
//		areaLines.Add (22);
//		areaLines.Add (27);
//		areaLines.Add (32);
//		areaLines.Add (38);
//		areaLines.Add (42);
//		areaLines.Add (49);
//		questData.Load ("Assets/Resources/Chapter1.txt" , 0 , areaLines);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

[System.Serializable]
public class ChapterOfQuest
{
	public List <DifferentAreaValuePerTimeOfClearance> area;
	public bool isCleared;
}

[System.Serializable]
public class DifferentAreaValuePerTimeOfClearance
{
	public int areaNo;
	public List <AreaOfQuest> areas;
	public List <int> hotSpotNoUsed;
	public List <bool> isHotSpotCleared;
	public int noOfTimesAreaWasCleared;
	public int itemNoSetOfsetGained;
	public bool isCleared;
	public int amountCleared;
	public int bossStats;
	public bool isAllHotSpotsCleared;
}


[System.Serializable]
public class AreaOfQuest
{
	public int areaNo;
	public int timeOfAreaClearance;
	public int noOfHotspots;
	public int wheatReward;
	public int goldReward;
	public int expReward;
	public int attackPotionReward;
	public int staminaPotionReward;
	public int unlockedStaminaPotionReward;
	public int unlockedAttackPotionReward;
	public int callToArmsReward; //signal fire
//	public int dragonCoinsReward;
	public int dragonEggsReward;
	public int peaceTreatyReward;
	public int clearancePointsNeeded;

}

[System.Serializable]
public class Hotspot
{
	public int typeOfHotspot;
	public string nameOfHotspot;
	public int staminaReqd;
	public int clearancePointsAwarded;
	public int avatarExpFetched;
	public string descriptionOfHotspot;
}
