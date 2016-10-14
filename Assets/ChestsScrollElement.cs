using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using MiniJSON;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class ChestInfo
{
	public int chestID;
	public int is_golden_chest;
	public int is_ally_sent;
	public float chest_duration;
	public System.DateTime start_time;
	public System.DateTime end_time;
}

public class ChestsScrollElement : MonoBehaviour {

	int wheatIncrement;
	int goldIncrement;
	int attackPotion;
	int staminaPotion;
	int noOfRewards;
	int[] chestArtefact = new int[6];
	float multiplicationFactor = 1f;

	public Text timerText;
	public int userIdOfAlly;
	public ChestInfo myInfo;


	// Use this for initialization
	void Start () {
//		myInfo.start_time = TimeManager._instance.GetCurrentServerTime ();
//		myInfo.end_time = myInfo.start_time.AddMinutes (myInfo.chest_duration);
	}
	
	// Update is called once per frame
	void Update () {
		System.TimeSpan differenceTime = myInfo.end_time - TimeManager._instance.GetCurrentServerTime ();
		timerText.text = differenceTime.Minutes +"m "+differenceTime.Seconds+"s";
		if (differenceTime.TotalSeconds < 0 && !loadingScene.Instance.loader.activeInHierarchy) {
			RemoveFromList ();
			Destroy (gameObject);
		}
	}

	void RemoveFromList()
	{
		if (userIdOfAlly > 0 || myInfo.is_ally_sent == 1) {
			chestScript._instance.allyChests.Remove (this);
		} else if (myInfo.is_golden_chest == 1) {
			chestScript._instance.myGoldenChests.Remove (this);
		}
		else
			chestScript._instance.myWoodenChests.Remove (this);
	}

	public void SendToAlly(int noOfKeys)
	{
		if (ChestData._instance.chestData.peasantKeys >= noOfKeys) {
			loadingScene.Instance.loader.SetActive (true);
			NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
				if (isConnected) {
					ChestData._instance.chestData.peasantKeys -= noOfKeys;
					StartCoroutine (SendToAllyCoroutine ((isSuccess, msg) => {
						if (isSuccess) {
							StartCoroutine (ChestData._instance.UpdateChestData ((isSuccessAlly, msgString) => {
								if (isSuccessAlly) {
									loadingScene.Instance.loader.SetActive (false);
									RemoveFromList ();
									Destroy (gameObject);
								} else {
									loadingScene.Instance.popupFromServer.ShowPopup (msgString);
									ChestData._instance.chestData.peasantKeys += noOfKeys;
								}
							}));
						} else {
							loadingScene.Instance.popupFromServer.ShowPopup (msg);
						}
					}));
				} else
					loadingScene.Instance.popupFromServer.ShowPopup ("Network Error");
			});
		} else {
			loadingScene.Instance.popupFromServer.ShowPopup ("Not Enough Keys!");
		}
	}


	IEnumerator SendToAllyCoroutine(System.Action<bool,string> callBack)
	{
		//device_id=123&is_golden_chest=1&user_id=123

		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "doUpdateChest");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("chest_id", myInfo.chestID);
		wwwForm.AddField ("is_ally_sent", 1);

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

	IEnumerator SendOpenChestData(Dictionary<string, string> stashParameters , System.Action<bool,string> callBack )
	{
		//device_id=123&friend_id=VhvGK3gtrB4=&arr_stashData=chestdata&item_name=test2&count=1&type=1&user_id=123

		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "doAlterChest");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("chest_id", myInfo.chestID);
		wwwForm.AddField ("friend_id",PlayerDataParse._instance.ID(userIdOfAlly.ToString()));
		if (stashParameters != null) {
			wwwForm.AddField ("arr_stashData", "chestdata");
			string itemNamesToSend = "";
			string countToSend = "";
			for (int i = 0; i < stashParameters.Count; i++) {
				itemNamesToSend += stashParameters.Keys.ElementAt (i)+",";
				countToSend += stashParameters.Values.ElementAt (i)+",";
			}
			itemNamesToSend = itemNamesToSend.Remove (itemNamesToSend.Length-1);
			countToSend = countToSend.Remove (countToSend.Length-1);
			Debug.Log ("itemNamesToSend = "+itemNamesToSend);
			Debug.Log ("countToSend = "+countToSend);
			wwwForm.AddField ("item_name", itemNamesToSend);
			wwwForm.AddField ("count", countToSend);
			wwwForm.AddField ("type", "1");		
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

	public void OpenChest(int noOfKeys)
	{
		Dictionary<string, string> stashParameters = null;
		float factorOfRewardDivide = 1f;
		if (myInfo.is_ally_sent == 1 || userIdOfAlly > 0) {
			factorOfRewardDivide = 2f;
			stashParameters = new Dictionary<string, string> ();
		}
		multiplicationFactor = 1;
		bool canOpenChest = false;
		string buttonName = EventSystem.current.currentSelectedGameObject.name;
		if (buttonName == "P") {
			if(ChestData._instance.chestData.peasantKeys >= noOfKeys)
			{
				canOpenChest = true;
			}
		} else {
			if(ChestData._instance.chestData.royalKeys >= noOfKeys)
			{
				canOpenChest = true;
			}
		}
		if (chestScript._instance.currentRunningCycle == chestScript.CycleTYpes.GOLDEN_CYCLE) {
			multiplicationFactor *= 2;
		}
		if (myInfo.is_golden_chest == 1) {
			multiplicationFactor *= 1.5f;
		} else {
			
		}

		if (canOpenChest) {
			Dictionary<string, string> avatarParameters = new Dictionary<string, string> ();
			int eventPointsGained = 0;
			List<string> rewardTexts = new List<string> ();
			ChestData._instance.RewardFromChest ( factorOfRewardDivide ,multiplicationFactor , ref wheatIncrement,ref goldIncrement,ref attackPotion,ref staminaPotion,ref eventPointsGained, ref noOfRewards, ref rewardTexts, myInfo.is_golden_chest);

			avatarParameters.Add ("array_players" , "players");
			if (wheatIncrement > 0) {
				avatarParameters.Add("wheat",(PlayerParameters._instance.myPlayerParameter.wheat+wheatIncrement).ToString());
				avatarParameters.Add("wheat_time", TimeManager._instance.GetCurrentServerTime().ToString());
				if (myInfo.is_ally_sent == 1 || userIdOfAlly > 0) {
					stashParameters.Add ("wheat",wheatIncrement.ToString());
				}
			}
			if (goldIncrement > 0) {
				avatarParameters.Add("gold",(PlayerParameters._instance.myPlayerParameter.gold+goldIncrement).ToString());
				avatarParameters.Add("gold_time", TimeManager._instance.GetCurrentServerTime().ToString());
				if (myInfo.is_ally_sent == 1 || userIdOfAlly > 0) {
					stashParameters.Add ("gold",goldIncrement.ToString());
				}
			}
			if (attackPotion > 0) {
				avatarParameters.Add("attack_potion",(PlayerParameters._instance.myPlayerParameter.attack_potion+attackPotion).ToString());
				if (myInfo.is_ally_sent == 1 || userIdOfAlly > 0) {
					stashParameters.Add ("attack_potion",attackPotion.ToString());
				}
			}
			if (staminaPotion > 0) {
				avatarParameters.Add("stamina_potion",(PlayerParameters._instance.myPlayerParameter.stamina_potion+staminaPotion).ToString());
				if (myInfo.is_ally_sent == 1 || userIdOfAlly > 0) {
					stashParameters.Add ("stamina_potion",staminaPotion.ToString());
				}
			}
			avatarParameters.Add ("array_chData" , "chestData");
			if (avatarParameters.Count == 1)
				avatarParameters.Remove ("array_players");
			if (eventPointsGained > 0) {
				avatarParameters.Add("event_points",(ChestData._instance.chestData.eventPoints+eventPointsGained).ToString());
				avatarParameters.Add("overall_points",(ChestData._instance.chestData.overallEventPoints+eventPointsGained).ToString());
				if (myInfo.is_ally_sent == 1 || userIdOfAlly > 0) {
					stashParameters.Add ("event_points",eventPointsGained.ToString());
					stashParameters.Add ("overall_points",eventPointsGained.ToString());
				}
			}
			if (buttonName == "P") {
				ChestData._instance.chestData.peasantKeys -= noOfKeys;
				avatarParameters.Add("peasant_key",(ChestData._instance.chestData.peasantKeys).ToString());
			} else {
				ChestData._instance.chestData.royalKeys -= noOfKeys;
				avatarParameters.Add("royal_key",(ChestData._instance.chestData.royalKeys).ToString());

			}
				
			loadingScene.Instance.loader.SetActive (true);
			NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
				if (isConnected) {
					StartCoroutine (SendOpenChestData (stashParameters ,(isSuccess, msg) => {
						if (isSuccess) {
							
							StartCoroutine (ChestData._instance.UpdatePlayerAndChestdata (avatarParameters,(isSuccessUpdate, msgString) => {
								if (isSuccessUpdate) {
									chestScript._instance.chestRewardsPanel.SetActive(true);
									for(int i = 0; i < chestScript._instance.chestRewards.Count ; i++)
									{
										if(i >= noOfRewards)
										{
											chestScript._instance.chestRewards[i].transform.parent.gameObject.SetActive(false);
											continue;
										}
										chestScript._instance.chestRewards[i].transform.parent.gameObject.SetActive(true);
										chestScript._instance.chestRewards[i].text = rewardTexts[i];
									}

									if (wheatIncrement > 0) {
										PlayerParameters._instance.myPlayerParameter.wheat+=wheatIncrement;
										PlayerParameters._instance.myPlayerParameter.wheat_time = TimeManager._instance.GetCurrentServerTime();
									}
									if (goldIncrement > 0) {
										PlayerParameters._instance.myPlayerParameter.gold+=goldIncrement;
										PlayerParameters._instance.myPlayerParameter.gold_time = TimeManager._instance.GetCurrentServerTime();
									}
									if (attackPotion > 0) {
										PlayerParameters._instance.myPlayerParameter.attack_potion+=attackPotion;
									}
									if (staminaPotion > 0) {
										PlayerParameters._instance.myPlayerParameter.stamina_potion+=staminaPotion;
									}
									if (eventPointsGained > 0) {
										ChestData._instance.chestData.eventPoints+=eventPointsGained;
									}
									loadingScene.Instance.loader.SetActive (false);
									RemoveFromList ();
									ChestMain._instance.DisplayFinalText();
									Destroy(gameObject);
								} else {
									if (buttonName == "P") {
										ChestData._instance.chestData.peasantKeys += noOfKeys;
									} else {
										ChestData._instance.chestData.royalKeys += noOfKeys;
									}
									loadingScene.Instance.popupFromServer.ShowPopup ("Netwok error!");
									RemoveFromList ();
									Destroy(gameObject);
								}
							}));
						} else {
							if (buttonName == "P") {
								ChestData._instance.chestData.peasantKeys += noOfKeys;
							} else {
								ChestData._instance.chestData.royalKeys += noOfKeys;
							}
							loadingScene.Instance.popupFromServer.ShowPopup (msg);
						}
					}));
				} else
				{
					if (buttonName == "P") {
						ChestData._instance.chestData.peasantKeys += noOfKeys;
					} else {
						ChestData._instance.chestData.royalKeys += noOfKeys;
					}
					loadingScene.Instance.popupFromServer.ShowPopup ("Network Error");
				}
			});

		}
		else
			loadingScene.Instance.popupFromServer.ShowPopup ("Not Enough Keys!");

	}

	public void AllyOpenChest(int noOfKeys)
	{

	}

}
