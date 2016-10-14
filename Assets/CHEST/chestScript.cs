using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using MiniJSON;


public class chestScript : MonoBehaviour {
	int cycleNo;
	int previousCycleNo = -1;
	bool isMenuActive=false;
	public GameObject[] contents;
	public Button[] upperButtons;
	public GameObject[] contentsRewards;
	public Button[] upperButtonsRewards;

	public List<GameObject> chestPages; //0- chest Page , 1-mainPage, 2 - rules , 3- rewards
	public List<GameObject> chestScenes; //0- chest Page , 1-mainPage, 2 - rules , 3- rewards

	public Button[] bottomsButtons;

	public System.DateTime eventStartTime;
	public System.DateTime eventEndTime;
	public int eventDuration;
	public System.DateTime cycleStartTime;
	public System.DateTime cycleEndTime;
	public int cycleDuration;

	public ChestEventPage chestEventPage;

	public bool eventEnded;
	public bool eventStarted;

	public string []cycleName;

	public static chestScript _instance;

	public int playerRank;
	public int guildRank;

	public string eventStartTimeCheck;
	public string eventEndTimeCheck;
	public string cycleEndTimeCheck;

	public Transform woodenChestParent, goldenChestParent, allyChestParent;
	public List<ChestsScrollElement> myWoodenChests;
	public List<ChestsScrollElement> myGoldenChests;
	public List<ChestsScrollElement> allyChests;

	public GameObject chestRewardsPanel;

	public List<Text> chestRewards;

	public bool fetchedWoodenChests , fetchedGoldenChests;

	public enum CycleTYpes
	{
		NORMAL_CYCLE,
		GOLDEN_CYCLE
	}

	public CycleTYpes currentRunningCycle;

	bool dataFetched;

	public void ResetValues()
	{
		fetchedWoodenChests = false;
		fetchedGoldenChests = false;
		for (int i = myWoodenChests.Count-1; i >= 0; i--) {
			Destroy (myWoodenChests [i].gameObject);
		}

		for (int i = myGoldenChests.Count-1; i >= 0; i--) {
			Destroy (myGoldenChests [i].gameObject);
		}

		for (int i = allyChests.Count-1; i >= 0; i--) {
			Destroy (allyChests [i].gameObject);
		}
		allyChests.Clear ();
		ChestData._instance.chestData.royalKeys = 0;
		ChestData._instance.chestData.peasantKeys = 0;
		ChestData._instance.chestData.chestCount = 0;
		ChestData._instance.chestData.eventPoints = 0;
		ChestData._instance.chestData.isBlessingActive = false;
		dataFetched = false;
		myWoodenChests.Clear ();
		myGoldenChests.Clear ();
	}
	// Use this for initialization
	void Start () {
		_instance = this;
		deactivateContents (0);
		deactivateContentsRewards (0);
		eventStartTime = System.Convert.ToDateTime (eventStartTimeCheck);
		eventEndTime = System.Convert.ToDateTime (eventEndTimeCheck);
	}

	IEnumerator DoGetChestData(System.Action<bool , string> callBack)
	{
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "doGetChestData");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);

		WWW wwwInsertChest = new WWW (loadingScene.Instance.baseUrl, wwwForm);
		yield return wwwInsertChest;
		if (wwwInsertChest.error == null) {
			Debug.Log (wwwInsertChest.text);
			IDictionary resultDict = (IDictionary)Json.Deserialize (wwwInsertChest.text);
			if (wwwInsertChest.text.Contains ("error_msg")) {
				callBack (true, resultDict ["error_msg"].ToString ());
			} else {
		//	{"success":1,"data":[{"id":"1","user_id":"16","blessing_starttime":"2001-01-01 00:00:00","royal_key":"0","peasant_key":"7","event_points":"0","chest_count":"0","arr_chestData":""}]}
				IList dataListBig = (IList)resultDict["data"];
				IDictionary dataList = (IDictionary)dataListBig[0];
				int.TryParse (dataList ["royal_key"].ToString (), out ChestData._instance.chestData.royalKeys);
				int.TryParse (dataList ["peasant_key"].ToString (), out ChestData._instance.chestData.peasantKeys);
				int.TryParse (dataList ["event_points"].ToString (), out ChestData._instance.chestData.eventPoints);
				int.TryParse (dataList ["chest_count"].ToString (), out ChestData._instance.chestData.chestCount);
				int.TryParse (dataList ["overall_points"].ToString (), out ChestData._instance.chestData.overallEventPoints);
				if (dataList ["blessing_starttime"] != null && !string.IsNullOrEmpty (dataList ["blessing_starttime"].ToString ()))
				{
					if (!dataList ["blessing_starttime"].ToString ().Contains ("0000-00-00 00:00:00")) {
						ChestData._instance.chestData.blessingStartTime = System.Convert.ToDateTime (dataList ["blessing_starttime"].ToString ());
					
						System.TimeSpan diffblessing = TimeManager._instance.GetCurrentServerTime () - ChestData._instance.chestData.blessingStartTime;
						if (diffblessing.TotalMinutes < 10 && diffblessing.TotalMinutes > 10)
							ChestData._instance.chestData.isBlessingActive = true;
					}
				}
				dataFetched = true;
				callBack (true, "Done");
			}
		} else {
			callBack (false, "Network Error!");
		}
	}


	public void deactivateItems(int index)
	{
		for(int i=0;i<chestPages.Count;i++)
		{
			if(i==index)
			{
				chestPages[i].SetActive(true);
				if(chestScenes.Contains(chestPages[i].gameObject))
				{
					chestScenes.RemoveAt(chestPages.Count-1);
				}
				else
				{
					chestScenes.Add(chestPages[i].gameObject);
				}
			}
			else
			{
				if(chestPages[i] != null)
					chestPages[i].SetActive(false);
			}

		}

	}

	public void BackFromChestEvent()
	{
		loadingScene.Instance.scenes[loadingScene.Instance.scenes.Count-2].SetActive(true);
		loadingScene.Instance.scenes[loadingScene.Instance.scenes.Count-1].SetActive(false);
		loadingScene.Instance.scenes.RemoveAt(loadingScene.Instance.scenes.Count-1);
	}

	public void BackFromOtherChestPanels()
	{
		chestScript._instance.chestScenes[chestScript._instance.chestScenes.Count-2].SetActive(true);
		chestScript._instance.chestScenes[chestScript._instance.chestScenes.Count-1].SetActive(false);
		chestScript._instance.chestScenes.RemoveAt(chestScript._instance.chestScenes.Count-1);
	}


	public void menuPopUp()
	{
//		loadingScene.Instance.objectToOpenMenu = this.gameObject;
		if(!isMenuActive)
		{
			for(int i=0;i<bottomsButtons.Length;i++)
			{
				bottomsButtons[i].GetComponent<Button>().interactable=false;

				bottomsButtons[i].GetComponent<Image>().color=new Color32(131,106,106,255);
				bottomsButtons[i].GetComponentInChildren<Text>().color=new Color32(131,106,106,255);
			}
			newMenuScene.instance.menuScreen.SetActive(true);
			isMenuActive = true;
		}
		else
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			newMenuScene.instance.menuScreen.SetActive(false);
			isMenuActive = false;
		}

	}

	void OnEnable()
	{
		chestScenes.Clear ();
		deactivateItems (0);
		if (!dataFetched) {
			loadingScene.Instance.loader.SetActive (true);
			NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
				if (isConnected) {
					StartCoroutine (DoGetChestData ((isSucess, msg) => {
						if(isSucess)
						{
							loadingScene.Instance.loader.SetActive (false);
							if (eventEnded) {
								chestEventPage.eventTimer.text = "EVENT ENDED!";
								chestEventPage.cycleTimer.text = "0h 0m";
							}

							for(int j=0;j<bottomsButtons.Length;j++)
							{
								bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
								bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
								bottomsButtons[j].GetComponent<Button>().interactable=true;
							}
							isMenuActive = false;
							newMenuScene.instance.menuScreen.SetActive(false);
						}
						else
						{
							loadingScene.Instance.popupFromServer.ShowPopup("Come Back Again Later!");
							BackFromChestEvent();
						}
					}));
				}
				else
				{
					loadingScene.Instance.popupFromServer.ShowPopup("Come Back Again Later!");
					BackFromChestEvent();
				}
			});
		}
	}

	// Update is called once per frame
	void Update () {

		System.TimeSpan differenceOfTimeFromStart = ( TimeManager._instance.GetCurrentServerTime () - chestScript._instance.eventStartTime );
		if (!eventStarted) {
			
			if (differenceOfTimeFromStart.TotalSeconds > 0)
				eventStarted = true;
			else {
				chestEventPage.eventTimer.text = "Coming Soon!";
				chestEventPage.cycleTimer.text = "Coming Soon!";
			}
		}


		if (!eventEnded && eventStarted) {

			cycleNo = Mathf.FloorToInt(differenceOfTimeFromStart.Hours / 4f);

			if (cycleNo != previousCycleNo) {
				chestEventPage.cycleHeading.text = cycleName [cycleNo];
				previousCycleNo = cycleNo;
				cycleEndTime = chestScript._instance.eventStartTime.AddHours (differenceOfTimeFromStart.Days*24 + ((cycleNo+1) * 4));
				cycleStartTime = chestScript._instance.eventStartTime.AddHours (differenceOfTimeFromStart.Days*24 + ((cycleNo) * 4));
				cycleEndTimeCheck = cycleEndTime.ToString ();
				if (cycleName [cycleNo] == "Golden Cycle")
					currentRunningCycle = CycleTYpes.GOLDEN_CYCLE;
				else
					currentRunningCycle = CycleTYpes.NORMAL_CYCLE;
			}
			System.TimeSpan cycleTimeDiff = cycleEndTime - TimeManager._instance.GetCurrentServerTime ();
			chestEventPage.cycleTimer.text = cycleTimeDiff.Hours  + "h " + cycleTimeDiff.Minutes + "m";
			int totalMinutes = cycleTimeDiff.Hours * 60 + cycleTimeDiff.Minutes;
			float radialDisplay = totalMinutes / 240f;
			if (radialDisplay > 1f)
				radialDisplay = 1f;
			chestEventPage.cycleRadial.fillAmount = radialDisplay;
			chestEventPage.cycleNeedle.transform.localEulerAngles = new Vector3 (0,0, - radialDisplay*360);

			System.TimeSpan differenceOfTime = (chestScript._instance.eventEndTime - TimeManager._instance.GetCurrentServerTime ());
			if (differenceOfTime.Days > 0)
				chestEventPage.eventTimer.text = differenceOfTime.Days + "d " + differenceOfTime.Hours + "h " + differenceOfTime.Minutes + "m";
			else if (differenceOfTime.TotalSeconds > 0) {
				chestEventPage.eventTimer.text = differenceOfTime.Hours + "h " + differenceOfTime.Minutes + "m " + differenceOfTime.Seconds + "s";
			} else {
				eventEnded = true;
				chestEventPage.eventTimer.text = "EVENT ENDED!";
				chestEventPage.cycleTimer.text = "0h 0m";
			}
		}
	}

	public void clickButton(Button button)
	{
		if (!eventEnded && eventStarted) {
			if (button.name == "Wooden") {
				if (!fetchedWoodenChests) {
					loadingScene.Instance.loader.SetActive (true);
					NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
						if (isConnected) {
							StartCoroutine (FetchChests (0, (isSuccess, msg) => {
								if (isSuccess || msg != "Network Error!") {
									fetchedWoodenChests = true;
									loadingScene.Instance.loader.SetActive (false);
									if(!isSuccess)
										loadingScene.Instance.popupFromServer.ShowPopup (msg);
								} else
									loadingScene.Instance.popupFromServer.ShowPopup (msg);
							}));
						} else {
							loadingScene.Instance.popupFromServer.ShowPopup ("Network Error!");
						}
					});

				}
				deactivateContents (0);
			} else if (button.name == "Golden") {
				if (!fetchedGoldenChests) {
					loadingScene.Instance.loader.SetActive (true);
					NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
						if (isConnected) {
							StartCoroutine (FetchChests (1, (isSuccess, msg) => {
								if (isSuccess || msg != "Network Error!") {
									fetchedGoldenChests = true;
									loadingScene.Instance.loader.SetActive (false);
									if(!isSuccess)
										loadingScene.Instance.popupFromServer.ShowPopup (msg);
								} else
									loadingScene.Instance.popupFromServer.ShowPopup (msg);
							}));
						} else {
							loadingScene.Instance.popupFromServer.ShowPopup ("Network Error!");
						}
					});

				}
				deactivateContents (1);

			} else if (button.name == "Ally") {
				loadingScene.Instance.loader.SetActive (true);
				NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
					if (isConnected) {
						StartCoroutine (FetchAllyChests ( (isSuccess, msg) => {
							if (isSuccess) {
								loadingScene.Instance.loader.SetActive (false);
							} else
								loadingScene.Instance.popupFromServer.ShowPopup (msg);
						}));
					} else {
						loadingScene.Instance.popupFromServer.ShowPopup ("Network Error!");
					}
				});
				deactivateContents (2);
			}
			if (button.name == "Milestone") {
				deactivateContentsRewards (0);

			} else if (button.name == "Personal") {
				deactivateContentsRewards (1);

			} else if (button.name == "Guild") {
				deactivateContentsRewards (2);

			}
			else if (button.name == "clock") {
				deactivateItems (4);
			}
		} else {
			if(eventEnded)
				loadingScene.Instance.popupFromServer.ShowPopup ("Event has Ended!");
			else
				loadingScene.Instance.popupFromServer.ShowPopup ("Event has not started!");
		}
	}




	public IEnumerator FetchAllyChests( System.Action<bool,string> callBack)
	{
		Debug.Log (allyChests.Count);
		for (int i = allyChests.Count-1; i >= 0; i--) {
			if(allyChests [i].gameObject != null)
				Destroy (allyChests [i].gameObject);
		}

		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "doGetChestGuildFriend");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		if(!string.IsNullOrEmpty(PlayerParameters._instance.myPlayerParameter.guildID))
			wwwForm.AddField ("guild_id", PlayerParameters._instance.myPlayerParameter.guildID);

		WWW wwwFetchChest = new WWW (loadingScene.Instance.baseUrl, wwwForm);
		yield return wwwFetchChest;
		allyChests.Clear ();
		if (wwwFetchChest.error == null) {
			Debug.Log (wwwFetchChest.text);
			IDictionary resultDict = (IDictionary)Json.Deserialize (wwwFetchChest.text);
			if (wwwFetchChest.text.Contains ("error_msg")) {
				callBack (false, resultDict ["error_msg"].ToString ());
			} else {
				//{"success":1,"data":[{"id":"3","user_id":"16","is_golden_chest":"1","start_time":"2016-10-03 02:13:15",
				//"end_time":"2016-10-03 02:43:15","is_ally_sent":"0","chest_duration":"60"}]}

				//{"success":1,"data":[{"id":"1","user_id":"16","is_golden_chest":"0","start_time":"2016-10-03 02:12:15",
				//"end_time":"2016-10-03 02:42:15","is_ally_sent":"0","chest_duration":"30"},{"id":"2","user_id":"16",
				//"is_golden_chest":"0","start_time":"2016-10-03 02:13:15","end_time":"2016-10-03 02:43:15","is_ally_sent":"0","chest_duration":"30"}]}
				IList listOfChests = (IList)resultDict["data"];
				for (int i = 0; i < listOfChests.Count; i++) {
					IDictionary chestData = (IDictionary)listOfChests[i];
					GameObject chestObject =(GameObject)Instantiate(Resources.Load("AllyChest"));
					chestObject.transform.name = "AllyChest";
					chestObject.transform.SetParent (allyChestParent);
					allyChests.Add (chestObject.GetComponent<ChestsScrollElement> ());
					chestObject.transform.localScale = Vector3.one;
					ChestsScrollElement chestObjectScroll = chestObject.GetComponent<ChestsScrollElement> ();
					int.TryParse (chestData ["id"].ToString (), out chestObjectScroll.myInfo.chestID);
					int.TryParse (chestData ["is_golden_chest"].ToString (), out chestObjectScroll.myInfo.is_golden_chest);
					float.TryParse (chestData ["chest_duration"].ToString (), out chestObjectScroll.myInfo.chest_duration);
					int.TryParse (chestData ["is_ally_sent"].ToString (), out chestObjectScroll.myInfo.is_ally_sent);
					chestObjectScroll.myInfo.start_time = System.Convert.ToDateTime(chestData ["start_time"].ToString ());
					chestObjectScroll.myInfo.end_time = System.Convert.ToDateTime(chestData ["end_time"].ToString ());
				}
				callBack (true, "");
			}
		} else {
			Debug.Log (wwwFetchChest.error);
			callBack (false, "Network Error!");
		}

	}

	public IEnumerator FetchChests(int isGoldenChest, System.Action<bool,string> callBack)
	{
		//device_id=123&is_golden_chest=1&user_id=123

		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "doGetChestList");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("is_golden_chest", isGoldenChest.ToString ());

		WWW wwwFetchChest = new WWW (loadingScene.Instance.baseUrl, wwwForm);
		yield return wwwFetchChest;
		if (wwwFetchChest.error == null) {
			Debug.Log (wwwFetchChest.text);
			IDictionary resultDict = (IDictionary)Json.Deserialize (wwwFetchChest.text);
			if (wwwFetchChest.text.Contains ("error_msg")) {
				callBack (false, resultDict ["error_msg"].ToString ());
			} else {
				//{"success":1,"data":[{"id":"3","user_id":"16","is_golden_chest":"1","start_time":"2016-10-03 02:13:15",
				//"end_time":"2016-10-03 02:43:15","is_ally_sent":"0","chest_duration":"60"}]}

				//{"success":1,"data":[{"id":"1","user_id":"16","is_golden_chest":"0","start_time":"2016-10-03 02:12:15",
				//"end_time":"2016-10-03 02:42:15","is_ally_sent":"0","chest_duration":"30"},{"id":"2","user_id":"16",
				//"is_golden_chest":"0","start_time":"2016-10-03 02:13:15","end_time":"2016-10-03 02:43:15","is_ally_sent":"0","chest_duration":"30"}]}
				IList listOfChests = (IList)resultDict["data"];
				for (int i = 0; i < listOfChests.Count; i++) {
					IDictionary chestData = (IDictionary)listOfChests[i];
					GameObject chestObject = null;
					if (isGoldenChest == 0) {
						chestObject = (GameObject)Instantiate(Resources.Load("WoodenChest"));
						chestObject.transform.name = "WoodenChest";
						chestObject.transform.SetParent (woodenChestParent);
						myWoodenChests.Add (chestObject.GetComponent<ChestsScrollElement> ());

					}
					else {
						chestObject = (GameObject)Instantiate(Resources.Load("GoldenChest"));
						chestObject.transform.name = "GoldenChest";
						chestObject.transform.SetParent (goldenChestParent);
						myGoldenChests.Add (chestObject.GetComponent<ChestsScrollElement> ());
					}
					chestObject.transform.localScale = Vector3.one;
					ChestsScrollElement chestObjectScroll = chestObject.GetComponent<ChestsScrollElement> ();
					int.TryParse (chestData ["id"].ToString (), out chestObjectScroll.myInfo.chestID);
					int.TryParse (chestData ["is_golden_chest"].ToString (), out chestObjectScroll.myInfo.is_golden_chest);
					float.TryParse (chestData ["chest_duration"].ToString (), out chestObjectScroll.myInfo.chest_duration);
					int.TryParse (chestData ["is_ally_sent"].ToString (), out chestObjectScroll.myInfo.is_ally_sent);
					Debug.Log ("server : = "+chestData ["start_time"].ToString ());

					chestObjectScroll.myInfo.start_time = System.Convert.ToDateTime(chestData ["start_time"].ToString ());
					Debug.Log ("chestObjectScroll.myInfo.start_time = "+chestObjectScroll.myInfo.start_time);
					chestObjectScroll.myInfo.end_time = System.Convert.ToDateTime(chestData ["end_time"].ToString ());
				}
				callBack (true, "");
			}
		} else {
			callBack (false, "Network Error!");
		}

	}
	void deactivateContents(int index)
	{
		for(int i=0;i<contents.Length;i++)
		{
			if(i==index)
			{
				upperButtons[i].interactable=false;
				contents[i].SetActive(true);

			}
			else
			{
				upperButtons[i].interactable=true;
				contents[i].SetActive(false);

			}
		}
	}
	void deactivateContentsRewards(int index)
	{
		for(int i=0;i<contentsRewards.Length;i++)
		{
			if(i==index)
			{
				upperButtonsRewards[i].interactable=false;
				contentsRewards[i].SetActive(true);

			}
			else
			{
				upperButtonsRewards[i].interactable=true;
				contentsRewards[i].SetActive(false);

			}
		}
	}
}
