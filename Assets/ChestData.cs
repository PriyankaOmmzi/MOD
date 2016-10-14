using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using System.Linq;

public class ChestData : MonoBehaviour {
	public ChestDataTable chestData;
	public static ChestData _instance;
	string[] chestRewards = {"Wooden Chest","Golden Chest","Royal Keys","Peasant Keys"};
	string[] chestDataTableParameters = {"royal_key" , "peasant_key" , "event_points" , "chest_count" , "blessing_starttime","overall_points"};
	// Use this for initialization
	void Start () {
		_instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public IEnumerator UpdateChestData(System.Action<bool,string> callBack)
	{
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "doAddUpdateChest");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("royal_key", chestData.royalKeys);
		wwwForm.AddField ("peasant_key", chestData.peasantKeys);
		wwwForm.AddField ("event_points", chestData.eventPoints);
		wwwForm.AddField ("overall_points", chestData.overallEventPoints);
		wwwForm.AddField ("chest_count", chestData.chestCount);
		wwwForm.AddField ("blessing_starttime", chestData.blessingStartTime.ToString());

		WWW wwwFetchChest = new WWW (loadingScene.Instance.baseUrl, wwwForm);
		yield return wwwFetchChest;
		if (wwwFetchChest.error == null) {
			Debug.Log (wwwFetchChest.text);
			IDictionary resultDict = (IDictionary)Json.Deserialize (wwwFetchChest.text);
			if (wwwFetchChest.text.Contains ("error_msg")) {
				callBack (false, resultDict ["error_msg"].ToString ());
			} else {
				callBack (true, "");
			}
		} else {
			callBack (false, "Network Error!");
		}

	}

	public IEnumerator UpdatePlayerAndChestdata(Dictionary<string, string> apiParameters , System.Action<bool,string> callBack)
	{
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "doAddUpdateChestPlayerData");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);

		for (int i = 0; i < apiParameters.Count; i++) {
			wwwForm.AddField(apiParameters.Keys.ElementAt(i) , apiParameters.Values.ElementAt(i));
		}
		WWW wwwFetchChest = new WWW (loadingScene.Instance.baseUrl, wwwForm);
		yield return wwwFetchChest;
		if (wwwFetchChest.error == null) {
			Debug.Log (wwwFetchChest.text);
			IDictionary resultDict = (IDictionary)Json.Deserialize (wwwFetchChest.text);
			if (wwwFetchChest.text.Contains ("error_msg")) {
				callBack (false, resultDict ["error_msg"].ToString ());
			} else {
				callBack (true, "");
			}
		} else {
			callBack (false, "Network Error!");
		}

	}

	string ConvertTimeToString(System.DateTime dateTime)
	{
		string timeToReturn = "";
		if(dateTime.Day < 10)
			timeToReturn = dateTime.Year + "/" + dateTime.Month + "/0" + dateTime.Day + " " +  dateTime.Hour + ":" + dateTime.Minute + ":" + dateTime.Second;
		else
			timeToReturn = dateTime.Year + "/" + dateTime.Month + "/" + dateTime.Day + " " +  dateTime.Hour + ":" + dateTime.Minute + ":" + dateTime.Second;

		Debug.Log ("timeToReturn = "+timeToReturn);
		return timeToReturn.ToString();
	}

	public Dictionary<string , string> ArtefactHotspotReward(ref string rewardName , ref int countOfArtefact)
	{
		int valueAdded = Random.Range(0,chestRewards.Length);
//		int valueAdded = 0;

		rewardName = chestRewards[valueAdded];
		Dictionary<string , string> chestDict = new Dictionary<string, string> ();

		if (valueAdded > 1) {
			chestDict.Add ("chestDataTableParametersNo",(valueAdded - 2).ToString());
			countOfArtefact = Random.Range (1, 4);
			if (valueAdded == 2)
				chestDict.Add (chestDataTableParameters [valueAdded - 2], (chestData.royalKeys + countOfArtefact).ToString());
			else
				chestDict.Add (chestDataTableParameters [valueAdded - 2], (chestData.peasantKeys + countOfArtefact).ToString());

		} else {
			int isGoldenChest = 0;
			float chestDuration = 60f;
			chestDict.Add ("chestDataTableParametersNo","3");
			int finalChestCount = 0;
			if (valueAdded == 0) {//Wooden
				finalChestCount = chestData.chestCount + 1;
				countOfArtefact = 1;
			} else {
				isGoldenChest = 1;
				finalChestCount = chestData.chestCount + 5;
				countOfArtefact = 5;
				chestDuration = 30f;
			}
			if (finalChestCount > 250) {
				finalChestCount = 0;
				countOfArtefact = -(chestData.chestCount);
				//Blessingime
				chestDict.Add (chestDataTableParameters [4], ConvertTimeToString(TimeManager._instance.GetCurrentServerTime()));
			}
			chestDict.Add (chestDataTableParameters [3], finalChestCount.ToString());
			CallChestFound (isGoldenChest , chestDuration);
		}
		return chestDict;

	}

	void CallChestFound(int isGoldenChest , float chestDuration)
	{
		Debug.Log ("Found a chestt-----"+isGoldenChest);
//		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
//			if (isConnected) {
		StartCoroutine (ChestFound (isGoldenChest, chestDuration, (isSucess, msg) => {
			if(isSucess)
			{
				bool shouldAdd = false;
				if((isGoldenChest == 0 && chestScript._instance.fetchedWoodenChests) || (isGoldenChest == 1 && chestScript._instance.fetchedGoldenChests))
				{
					shouldAdd = true;
				}
				if(shouldAdd)
				{
					GameObject chestObject = null;
					if (isGoldenChest == 0) {
						chestObject = (GameObject)Instantiate(Resources.Load("WoodenChest"));
						chestObject.transform.name = "WoodenChest";
						chestObject.transform.SetParent (chestScript._instance.woodenChestParent);
						chestScript._instance.myWoodenChests.Add (chestObject.GetComponent<ChestsScrollElement> ());
					}
					else {
						chestObject = (GameObject)Instantiate(Resources.Load("GoldenChest"));
						chestObject.transform.name = "GoldenChest";
						chestObject.transform.SetParent (chestScript._instance.goldenChestParent);
						chestScript._instance.myGoldenChests.Add (chestObject.GetComponent<ChestsScrollElement> ());
					}
					ChestsScrollElement chestObjectScroll = chestObject.GetComponent<ChestsScrollElement> ();
					int.TryParse(msg , out chestObjectScroll.myInfo.chestID);
					chestObject.transform.localScale = Vector3.one;
					chestObjectScroll.myInfo.is_golden_chest = isGoldenChest;
					chestObjectScroll.myInfo.chest_duration = chestDuration;
					chestObjectScroll.myInfo.is_ally_sent = 0;
					chestObjectScroll.myInfo.start_time = TimeManager._instance.GetCurrentServerTime ();
					chestObjectScroll.myInfo.end_time = TimeManager._instance.GetCurrentServerTime ().AddMinutes (chestDuration);
				}
			}
		}));
//			}
//		});
	}

	public void UpdateChestDataTableIntVariables(int  chestDataTableParametersNo, int countToAdd)
	{
		switch(chestDataTableParametersNo)
		{
		case 0 :// "royal_key":
			chestData.royalKeys += countToAdd;
			break;
		case 1: //"peasant_key":
			chestData.peasantKeys += countToAdd;
			break;
		case 2://"event_points":
			chestData.eventPoints += countToAdd;
//			chestData.overallEventPoints += countToAdd;
			break;
		case 3: //"chest_count":
			chestData.chestCount += countToAdd;
			break;
		}
	}

	public void RewardFromChest(float factorOfRewardDivide,float multiplicationFactor , ref int wheatIncrement , ref int goldIncrement , ref int attackPotion , ref int staminaPotion , ref int eventPointsGained ,ref int noOfRewards ,ref List<string> rewardTexts,int isGoldenChest )
	{
		
		wheatIncrement = 0;
		goldIncrement = 0;
		attackPotion = 0;
		staminaPotion = 0;
		noOfRewards = 0;
		int isWheat = Random.Range (0, 10);
		if (isWheat < 5) {
			wheatIncrement = EmpireManager._instance.barn.finalValue1 [EmpireManager._instance.barn.currentLevel];
			wheatIncrement = Mathf.CeilToInt(wheatIncrement* multiplicationFactor/factorOfRewardDivide);
			noOfRewards++;
			rewardTexts.Add ("Food\n +"+wheatIncrement);
		}

		int isGold = Random.Range (0, 10);
		if (isGold < 5)
		{
			goldIncrement = EmpireManager._instance.goldMine.finalValue1 [EmpireManager._instance.goldMine.currentLevel];
			noOfRewards++;
			goldIncrement = Mathf.CeilToInt(goldIncrement* multiplicationFactor/factorOfRewardDivide);
			rewardTexts.Add ("Gold\n +"+goldIncrement);
		}
		int isAttackPotion = Random.Range (0, 10);
		if (isAttackPotion < 5)
		{
			attackPotion = Random.Range(1,6);
			noOfRewards++;
			attackPotion = Mathf.CeilToInt(attackPotion/factorOfRewardDivide);
			rewardTexts.Add ("Attack Potion\n +"+attackPotion);
		}

		int isStaminaPotion = Random.Range (0, 10);
		if (isStaminaPotion < 5)
		{
			staminaPotion = Random.Range(1,6);
			noOfRewards++;
			staminaPotion = Mathf.CeilToInt(staminaPotion/factorOfRewardDivide);
			rewardTexts.Add ("Stamina Potion\n +"+staminaPotion);
		}

		int isArtefact = Random.Range (0, 10);
		if (isArtefact < 5 || isGoldenChest == 1) {
			int minRange = 1;
			int maxRange = 4;
			if (isGoldenChest == 1) {
				minRange = 3;
				maxRange = 11;
			}
			int countOfArtefact = Random.Range (minRange,maxRange);
			noOfRewards++;
			for (int i = 0; i < countOfArtefact; i++) {
				int idOfArtefact = Random.Range (0,Artefacts._instance.chestEventArtefact.Length);
				eventPointsGained += Artefacts._instance.chestEventArtefact [idOfArtefact].eventPointsFetcehd ;
			}
			eventPointsGained = Mathf.CeilToInt(eventPointsGained* multiplicationFactor/factorOfRewardDivide);
			rewardTexts.Add ("Event Points\n +"+eventPointsGained);
		}

		if (noOfRewards == 0) {
			goldIncrement = EmpireManager._instance.goldMine.finalValue1 [EmpireManager._instance.goldMine.currentLevel];
			noOfRewards++;
			rewardTexts.Add ("Gold\n +"+Mathf.CeilToInt(goldIncrement/factorOfRewardDivide));
		}
		Debug.Log ("no of reward Text"+rewardTexts.Count);
	}



	IEnumerator ChestFound(int isGoldenChest ,float chestduration, System.Action<bool,string> callBack)
	{
		//device_id=123&is_golden_chest=1&start_time=2016-09-29 02:04:15&end_time=2016-09-29 02:04:15&is_ally_sent=3&chest_duration=1&user_id=123
		Debug.Log("Chest Found-----"+isGoldenChest);
		Debug.Log("Chest Found-----"+chestduration);
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "doInsertChest");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("is_golden_chest", isGoldenChest.ToString ());
		wwwForm.AddField ("start_time", ConvertTimeToString(TimeManager._instance.GetCurrentServerTime ()));
		wwwForm.AddField ("end_time", ConvertTimeToString(TimeManager._instance.GetCurrentServerTime ().AddMinutes (chestduration)));
		wwwForm.AddField ("is_ally_sent", "0");
		wwwForm.AddField ("chest_duration", chestduration.ToString ());

		Debug.Log ("start time = "+TimeManager._instance.GetCurrentServerTime ());
		Debug.Log ("end time = "+TimeManager._instance.GetCurrentServerTime ().AddMinutes (chestduration));

		WWW wwwInsertChest = new WWW (loadingScene.Instance.baseUrl, wwwForm);
		yield return wwwInsertChest;
		if (wwwInsertChest.error == null) {
			Debug.Log (wwwInsertChest.text);
			IDictionary resultDict = (IDictionary)Json.Deserialize (wwwInsertChest.text);
			if (wwwInsertChest.text.Contains ("error_msg")) {
				callBack (false, resultDict ["error_msg"].ToString ());
			} else {
			//{"success":1,"data":"Chest data saved successfully","last_insert_id":6}

			callBack (true, resultDict["last_insert_id"].ToString());
			}
		} else {
			callBack (false, "Network Error!");
		}
			
	}
}

[System.Serializable]
public class ChestDataTable
{
	public int royalKeys;
	public int peasantKeys;
	public int chestCount;
	public int eventPoints;
	public int overallEventPoints;
	public System.DateTime blessingStartTime;
	public bool isBlessingActive;
}

