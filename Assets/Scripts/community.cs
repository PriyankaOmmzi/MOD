using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MiniJSON;
using System.Collections.Generic;
public class community : MonoBehaviour
{
	public static community instance;
	public InputField searchField;
	public GameObject setting;
	public GameObject [] contentsLists;
	public  Button [] buttonsName;
	public GameObject menuScreen;
	public GameObject chatBtn;
	bool isMneuActive=false;
	public Button[] bottomsButtons;
	public GameObject acceptRequest;
	public GameObject removeFriend;
	public GameObject sendRequest;
	public GameObject sendGuildReuest;
	public GameObject systemNotification;
	GameObject removeParent;
	public Text defaultDescription;
	Button sendRequesBuutont;
	public int searchFriendCount;
	GameObject acceptGameobject;
	public GameObject searchFriendContainer;
	public GameObject listFriendContainer;
	public GameObject requestFriendContainer;
	public GameObject blackListContainer;

	public GameObject othersButtonContent;
	string searchPlyerName;
	string searchPlayerGuildName;
	string searchPlayerLogin;
	int searchPlayerLevel;
	public Sprite[] searchPlayerProfile;
	int searchAvatarNo;
	public List<GameObject> searchedPlayer;
//	public List <int> searchedPlayerIdList;
	public int searchedPlayerId;



	//-----  friend list -----
	string listPlyerName;
	string listPlayerGuildName;
	string listPlayerLogin;
	int listPlayerLevel;
	int listAvatarNo;
	public List<GameObject> listPlayer;
	//	public List <int> searchedPlayerIdList;
	public int listPlayerId;

	//------  Accept Request ----

	string requestPlyerName;
	string requestPlayerGuildName;
	int requestPlayerLogin;
	int requestPlayerLevel;
	int requestAvatarNo;
	public List<GameObject> requestPlayer;
	//	public List <int> searchedPlayerIdList;
	public int requestPlayerId;

	string blackListPlyerName;
	string blackListGuildName;
	string blackListPlayerLogin;
	int blackListPlayerLevel;
	int blackListAvatarNo;
	public List<GameObject> blackListPlayer;
	public int blackListPlayerId;

	public Text totalFriends;
	//public GameObject friendRequestDialog;
	// Use this for initialization
	void Awake()
	{
		instance = this;
	}
	void Start () 
	{
		//PlayerPrefs.DeleteAll();

		menuScreen.SetActive (false);
		setting.SetActive(false);
		//friendRequestDialog.SetActive (false);
		acceptRequest.SetActive(false);
		removeFriend.SetActive(false);
		sendRequest.SetActive(false);
		sendGuildReuest.SetActive(false);
		systemNotification.SetActive(false);


	}
//	public void opnedRequestDialog()
//	{
//		friendRequestDialog.SetActive (true);
//
//	}
	public void ExitRequestDialog()
	{
		//friendRequestDialog.SetActive (false);

	}
	public void sliderChange(Slider sliderValue)
	{
		loadingScene.Instance.sliderValue = sliderValue.value;
		for(int i=0;i<loadingScene.Instance.allSounds.Length;i++)
		{
			if(loadingScene.Instance.allSounds[i] != null)
			{
				loadingScene.Instance.allSounds[i].volume = sliderValue.value;
			}
			//loadingScene.Instance.allSounds[i].volume=loadingScene.Instance.bgmSliders[i].value;
		}
	}
	public void logOut()
	{
		Start();
		onClickSettingExit();
		PlayerPrefs.SetString("logout","yes");
		loadingScene.Instance.main();
		//LoginScene.SetActive(true);
	}
	public void notificationOnOff()
	{
		loadingScene.Instance.notificationOnOff();
	}
	public void soundOnOff()
	{
		
		loadingScene.Instance.soundOnOff ();
	}


	public void acceptClick(GameObject click)
	{
		acceptGameobject=click;

		defaultDescription.text="Friend request Accepted.";
		openAcceptRequest();
	}
	void openAcceptRequest()
	{
		acceptRequest.SetActive(true);
	}
	public void exitAcceptRequest()
	{
		acceptRequest.SetActive(false);

		
	}
	
	public void YesAcceptRequest()
	{
		acceptRequest.SetActive(false);
		systemNotification.SetActive(true);
		
	}
	public void CancelAcceptRequest()
	{
		acceptRequest.SetActive(false);

		
	}
	

	public void sendClick(Button click)
	{
		sendRequesBuutont=click;
		defaultDescription.text="Friend request sent.";
		opensendRequest();
	}
	void opensendRequest()
	{
		sendRequest.SetActive(true);
	}
	public void exitsendRequest()
	{
		sendRequest.SetActive(false);


	}

	public void YessendRequest()
	{
		sendRequest.SetActive(false);
		systemNotification.SetActive(true);
		
	}
	public void CancelsendRequest()
	{
		sendRequest.SetActive(false);
		
		
	}


	public void removeClick(GameObject clickRemove)
	{
		defaultDescription.text="[Name of user] has been removed from the blacklist.";
		removeParent=clickRemove;
		removeFriend.SetActive(true);
	}

	public void clickYesRemove()
	{
		removeFriend.SetActive(false);

		systemNotification.SetActive(true);
	}
	public void clickNoRemove()
	{
		removeFriend.SetActive(false);

	}

	public void conFirmButton()
	{
		Destroy(removeParent);
		systemNotification.SetActive(false);
			
	}
	public void exitNotification()
	{
		systemNotification.SetActive(false);
		
	}


	public void onClickSetting()
	{
		for(int i=0;i<loadingScene.Instance.bgmSliders.Length;i++)
		{
			if(loadingScene.Instance.bgmSliders[i] != null)
				loadingScene.Instance.bgmSliders[i].value=loadingScene.Instance.sliderValue;
		}
		for(int j=0;j<bottomsButtons.Length;j++)
		{
			bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
			bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
			bottomsButtons[j].GetComponent<Button>().interactable=true;
		}
		print("yes this work");
		menuScreen.SetActive(false);
		isMneuActive=false;
		setting.SetActive(true);
	}
	public void onClickSettingExit()
	{
		setting.SetActive(false);
	}

//	public void cncelDialogFriend()
//	{
//		
//	}


	public void onDetailPlayerExit()
	{
		//friendDetail.SetActive(false);

	}

	public void  onClickRemove()
	{
		onDetailPlayerExit();
		//removeFriendsDialog.SetActive(true);

	}
	public void  onClickRemoveExit()
	{
		//removeFriendsDialog.SetActive(false);

	}



	public void onClickProfileExit()
	{
		loadingScene.Instance.playerProfilePanel.SetActive(false);
	}


	public void onClickProfile()
	{
		loadingScene.Instance.playerProfilePanel.SetActive(true);
	}


	// Update is called once per frame
	void Update () 
	{
	
	}


	public void exitMenu()
	{
		menuScreen.SetActive (false);
		
	}
	public void menuButton()
	{
		loadingScene.Instance.objectToOpenMenu = this.gameObject;
		if(isMneuActive==false)
		{
			for(int i=0;i<bottomsButtons.Length;i++)
			{
				bottomsButtons[i].GetComponent<Button>().interactable=false;

				bottomsButtons[i].GetComponent<Image>().color=new Color32(131,106,106,255);
				bottomsButtons[i].GetComponentInChildren<Text>().color=new Color32(131,106,106,255);
			}
			
			
			menuScreen.SetActive(true);
			
			
			isMneuActive=true;
		}
		else
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			print("yes this work");
			menuScreen.SetActive(false);
			isMneuActive=false;
			
		}
		
		
	}
	public void chatButton()
	{
		Start();

		if(PlayerPrefs.GetString("chat")=="off")
		{
			chatBtn.GetComponent<DragHandeler>().enabled=true;
			chatBtn.GetComponent<CanvasGroup>().blocksRaycasts=true;
			
			chatBtn.GetComponent<Button>().interactable=true;
			chatBtn.GetComponentInChildren<Text>().enabled=true;
			PlayerPrefs.SetString("chat","on");
			
			
		}
		
	}
	void clearedSearched()
	{
		for (int i = searchedPlayer.Count-1; i >= 0; i--) {
			if (searchedPlayer [i] != null)
			{
				Destroy (searchedPlayer [i]);
				searchedPlayer.RemoveAt (i);
//				Destroy (searchedPlayerIdList [i]);

			}
		}
		for (int j = listPlayer.Count-1; j >= 0; j--) {
			if (listPlayer [j] != null)
			{
				Destroy (listPlayer [j]);
				listPlayer.RemoveAt (j);
				//				Destroy (searchedPlayerIdList [i]);

			}
		}
		for (int k = blackListPlayer.Count-1; k >= 0; k--) {
			if (blackListPlayer [k] != null)
			{
				Destroy (blackListPlayer [k]);
				blackListPlayer.RemoveAt (k);
				//blackListPlayer.Clear ();
				//				Destroy (searchedPlayerIdList [i]);

			}
		}
		for (int l= requestPlayer.Count-1; l >= 0; l--) {
			if (requestPlayer [l] != null)
			{
				Destroy (requestPlayer [l]);
				requestPlayer.RemoveAt (l);
				//				Destroy (searchedPlayerIdList [i]);

			}
		}


	

	}
	public void buttonClick(Button click)
	{
		if(click.name=="friendList")
		{
			searchField.text = "";
			clearedSearched ();
			//clearedSearched ();
			listFriend ();
			//deactivateContents (0);
			othersButtonContent.SetActive (true);



		}
		else if(click.name=="search")
		{
			if (PlayerParameters._instance.myPlayerParameter.FriendList < GetMaxFriends ()) {
				clearedSearched ();
				searchFriend ();	
				othersButtonContent.SetActive (true);
			} else {
				loadingScene.Instance.popupFromServer.ShowPopup ("Friends Limit Reached!");
			}

		}
		else if(click.name=="friendRequest")
		{
			clearedSearched ();
			searchField.text = "";
			AcceptRequestFriend ();
			othersButtonContent.SetActive (true);

		}
		else if(click.name=="guildInvites")
		{
			clearedSearched ();
			searchField.text = "";

			deactivateContents (3);
			othersButtonContent.SetActive (true);
		}
		else if(click.name=="friends")
		{
			clearedSearched ();
			searchField.text = "";
			listFriend ();
			othersButtonContent.SetActive (true);
		}
		else if(click.name=="blackList")
		{
			clearedSearched ();
			searchField.text = "";
			blackFriend ();
			othersButtonContent.SetActive (false);


		}
	}
	void deactivateContents(int index)
	{
		for(int i=0;i<contentsLists.Length;i++)
		{
			if(i==index)
			{
				//buttonsName[i].GetComponent<Outline>().effectColor=new Color32(199,255,0,255);
				buttonsName[i].interactable=false;
				contentsLists[i].SetActive(true);

			}
			else
			{
				//buttonsName[i].GetComponent<Outline>().effectColor=new Color32(0,0,0,255);
				buttonsName[i].interactable=true;
				contentsLists[i].SetActive(false);

			}
		}
	}
	public void AcceptRequestFriend()
	{
		newMenuScene.instance.loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected) {
				WWWForm form_time = new WWWForm ();
				string URltime = loadingScene.Instance.baseUrl;
				form_time.AddField ("tag", "doGetFriendsWithStatus");
				form_time.AddField ("user_id",   PlayerDataParse._instance.playersParam.userId.ToString().Split (',') [0]); 
				form_time.AddField ("friend_type", "friend");  
				form_time.AddField ("status", "0");
				//PlayerDataParse._instance.playersParam.userId);
				form_time.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
				WWW www = new WWW (URltime, form_time.data);

				StartCoroutine (userTIMEfetching1 (www, isSuccess => 
					{
						IDictionary cardDict = Json.Deserialize(www.text) as IDictionary;
						Debug.Log("text = "+(www.text));
						if(www.text.Contains("Data does not exist!"))
						{
							//{"success":0,"error_msg":"No cards available!"}
							if(www.text.Contains("No cards available!"))
							{
							}
							else
							{
								newMenuScene.instance.popupFromServer.ShowPopup ("You don't have any friend request!");
								deactivateContents (2);
							}
						}
						else
						{
							IList friendList = (IList)cardDict ["data"];
							for (int i = 0; i < friendList.Count; i++) 
							{
								IDictionary friendListDic = (IDictionary)friendList[i];
								print("cardList"+friendList.Count);
								//								
								if(friendListDic["guild_name"] == null)
									requestPlayerGuildName="empty";
								else
								{
									requestPlayerGuildName = (friendListDic ["guild_name"].ToString());

								}
								//listPlyerName = (friendListDic ["userName"].ToString());
								if(friendListDic["login_datetime"] != null)
									int.TryParse (friendListDic["login_datetime"].ToString (), out requestPlayerLogin);
								//								listPlayerLogin = (friendListDic ["login_datetime"].ToString());
								if(friendListDic["avatar_level"] != null)
									int.TryParse (friendListDic["avatar_level"].ToString (), out requestPlayerLevel);

								if(friendListDic["avatar_no"] != null)
									int.TryParse (friendListDic["avatar_no"].ToString (), out requestAvatarNo);
								if(friendListDic["user_id"] != null)
									int.TryParse (friendListDic["user_id"].ToString (), out requestPlayerId);
								requestPlyerName = (friendListDic ["userName"].ToString());
//								if(friendListDic["userName"] != null)
//									int.TryParse (friendListDic["userName"].ToString (), out requestPlyerName);
								GameObject newCard1 = (GameObject)Instantiate (Resources.Load ("acceptFriend"));
								newCard1.transform.SetParent (requestFriendContainer.transform);
								newCard1.transform.localScale = Vector3.one;
								newCard1.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text=requestPlyerName;
								newCard1.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text="Lvl."+(requestPlayerLevel+1);
								newCard1.transform.GetChild(5).transform.GetChild(0).GetComponent<Text>().text=requestPlayerGuildName.ToString();
								newCard1.transform.GetChild(4).transform.GetChild(0).GetComponent<Text>().text="Last login:"+requestPlayerLogin;
								if(requestAvatarNo==1)
								{
									newCard1.transform.GetChild(2).GetComponent<Image>().sprite=searchPlayerProfile[0];
								}
								else if(requestAvatarNo==2)
								{
									newCard1.transform.GetChild(2).GetComponent<Image>().sprite=searchPlayerProfile[1];
								}
								else if(requestAvatarNo==3)
								{
									newCard1.transform.GetChild(2).GetComponent<Image>().sprite=searchPlayerProfile[2];
								}

								newCard1.name=requestPlayerId.ToString();
								requestPlayer.Add (newCard1.gameObject);
								//								searchedPlayerIdList.Add (searchedPlayerId);
								deactivateContents (2);								

							}
						}

					}
				));
			} 
			else 
			{
				empireScene.instance.popupFromServer.ShowPopup ("Network Error!");
			}

		});



	}



	public void listFriend()
	{
		newMenuScene.instance.loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected) {
				WWWForm form_time = new WWWForm ();
				string URltime = loadingScene.Instance.baseUrl;
				form_time.AddField ("tag", "doGetFriendsList");
				form_time.AddField ("user_id",   PlayerDataParse._instance.playersParam.userId.ToString()); 
				form_time.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
				form_time.AddField ("blacklist", "0");
				WWW www = new WWW (URltime, form_time.data);

				StartCoroutine (userTIMEfetching1 (www, isSuccess => 
					{
						IDictionary cardDict = Json.Deserialize(www.text) as IDictionary;
						Debug.Log("text = "+(www.text));
						if(www.text.Contains("error_msg"))
						{
							//{"success":0,"error_msg":"No cards available!"}
								newMenuScene.instance.popupFromServer.ShowPopup ("You don't have any friend!");
								deactivateContents(0);
						}
						else
						{
							IList friendList = (IList)cardDict ["data"];
							for (int i = 0; i < friendList.Count; i++) 
							{
								IDictionary friendListDic = (IDictionary)friendList[i];
								print("cardList"+friendList.Count);
//								
								if(friendListDic["guild_name"] == null)
									listPlayerGuildName="empty";
								else
								{
									listPlayerGuildName = (friendListDic ["guild_name"].ToString());

								}
								//listPlyerName = (friendListDic ["userName"].ToString());
//								if(friendListDic["login_datetime"] != null)
//									int.TryParse (friendListDic["login_datetime"].ToString (), out listPlayerLogin);
//								listPlayerLogin = (friendListDic ["login_datetime"].ToString());
								if(friendListDic["avatar_level"] != null)
									int.TryParse (friendListDic["avatar_level"].ToString (), out listPlayerLevel);

								if(friendListDic["avatar_no"] != null)
									int.TryParse (friendListDic["avatar_no"].ToString (), out listAvatarNo);
								if(friendListDic["user_id"] != null)
									int.TryParse (friendListDic["friend_id"].ToString (), out listPlayerId);
								listPlyerName = (friendListDic ["userName"].ToString());
								listPlayerLogin = (friendListDic ["login_datetime"].ToString());

								if(friendListDic["friend_list"] != null)
									int.TryParse (friendListDic["friend_list"].ToString (), out PlayerParameters._instance.myPlayerParameter.FriendList );
								GameObject newCard1 = (GameObject)Instantiate (Resources.Load ("listFriend"));
								newCard1.transform.SetParent (listFriendContainer.transform);
								newCard1.transform.localScale = Vector3.one;
								newCard1.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text=listPlyerName;
								newCard1.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text="Lvl."+(listPlayerLevel+1);
								newCard1.transform.GetChild(5).transform.GetChild(0).GetComponent<Text>().text=listPlayerGuildName.ToString();
								newCard1.transform.GetChild(4).transform.GetChild(0).GetComponent<Text>().text="Last login:"+listPlayerLogin;
								//print("lastlogin" +(friendListDic ["login_datetime"].ToString()));
								if(listAvatarNo==1)
								{
									newCard1.transform.GetChild(2).GetComponent<Image>().sprite=searchPlayerProfile[0];
								}
								else if(listAvatarNo==2)
								{
									newCard1.transform.GetChild(2).GetComponent<Image>().sprite=searchPlayerProfile[1];
								}
								else if(listAvatarNo==3)
								{
									newCard1.transform.GetChild(2).GetComponent<Image>().sprite=searchPlayerProfile[2];
								}

								newCard1.name=listPlayerId.ToString();
								listPlayer.Add (newCard1.gameObject);
								//								searchedPlayerIdList.Add (searchedPlayerId);
								deactivateContents(0);
								totalFriends.text=PlayerParameters._instance.myPlayerParameter.FriendList+"/"+GetMaxFriends();

							}
						}

					}
				));
			} 
			else 
			{
				empireScene.instance.popupFromServer.ShowPopup ("Network Error!");
			}

		});



	}

	public int GetMaxFriends()
	{
		int maxFriends = 9 + PlayerParameters._instance.myPlayerParameter.avatar_no;
		if (maxFriends > 50)
			maxFriends = 50;
		return maxFriends;
	}


	public void blackFriend()
	{
		newMenuScene.instance.loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected) {
				WWWForm form_time = new WWWForm ();
				string URltime = loadingScene.Instance.baseUrl;
				form_time.AddField ("tag", "doGetFriendsList");
				form_time.AddField ("user_id",   PlayerDataParse._instance.playersParam.userId.ToString()); 
				form_time.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
				form_time.AddField ("blacklist", "1");
				WWW www = new WWW (URltime, form_time.data);

				StartCoroutine (userTIMEfetching1 (www, isSuccess => 
					{
						IDictionary cardDict = Json.Deserialize(www.text) as IDictionary;
						Debug.Log("text = "+(www.text));
						if(www.text.Contains("error_msg"))
						{
							//{"success":0,"error_msg":"No cards available!"}

							newMenuScene.instance.popupFromServer.ShowPopup ("You have not blacklisted any friend!");
							deactivateContents (5);

						}
						else
						{
							Debug.Log("text = "+(www.text));

							IList friendList = (IList)cardDict ["data"];
							for (int i = 0; i < friendList.Count; i++) 
							{
								IDictionary friendListDic = (IDictionary)friendList[i];
								print("cardList"+friendList.Count);
								//								
								if(friendListDic["guild_name"] == null)
									blackListGuildName="empty";
								else
								{
									blackListGuildName = (friendListDic ["guild_name"].ToString());

								}
								blackListPlyerName = (friendListDic ["userName"].ToString());

								//listPlyerName = (friendListDic ["userName"].ToString());
								//if(friendListDic["login_datetime"] != null)
									blackListPlayerLogin = (friendListDic ["login_datetime"].ToString());
									//int.TryParse (friendListDic["login_datetime"].ToString (), out blackListPlayerLogin);
								//								listPlayerLogin = (friendListDic ["login_datetime"].ToString());
								if(friendListDic["avatar_level"] != null)
									int.TryParse (friendListDic["avatar_level"].ToString (), out blackListPlayerLevel);

								if(friendListDic["avatar_no"] != null)
									int.TryParse (friendListDic["avatar_no"].ToString (), out blackListAvatarNo);
								if(friendListDic["user_id"] != null)
									int.TryParse (friendListDic["friend_id"].ToString (), out blackListPlayerId);

								if(friendListDic["friend_list"] != null)
									int.TryParse (friendListDic["friend_list"].ToString (), out PlayerParameters._instance.myPlayerParameter.FriendList );
								
								GameObject newCard1 = (GameObject)Instantiate (Resources.Load ("blackList"));
								newCard1.transform.SetParent (blackListContainer.transform);
								newCard1.transform.localScale = Vector3.one;
								newCard1.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text=blackListPlyerName;
								newCard1.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text="Lvl."+(blackListPlayerLevel+1);
								newCard1.transform.GetChild(5).transform.GetChild(0).GetComponent<Text>().text=blackListGuildName.ToString();
								newCard1.transform.GetChild(4).transform.GetChild(0).GetComponent<Text>().text="Last login:"+blackListPlayerLogin;
								if(blackListAvatarNo==1)
								{
									newCard1.transform.GetChild(2).GetComponent<Image>().sprite=searchPlayerProfile[0];
								}
								else if(blackListAvatarNo==2)
								{
									newCard1.transform.GetChild(2).GetComponent<Image>().sprite=searchPlayerProfile[1];
								}
								else if(blackListAvatarNo==3)
								{
									newCard1.transform.GetChild(2).GetComponent<Image>().sprite=searchPlayerProfile[2];
								}

								newCard1.name=blackListPlayerId.ToString();
								blackListPlayer.Add (newCard1.gameObject);
								//								searchedPlayerIdList.Add (searchedPlayerId);
								deactivateContents (5);

							}
						}

					}
				));
			} 
			else 
			{
				empireScene.instance.popupFromServer.ShowPopup ("Network Error!");
			}

		});



	}


	public void searchFriend()
	{
		newMenuScene.instance.loader.SetActive (true);
			NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
				if (isConnected) {
					WWWForm form_time = new WWWForm ();
					string URltime = loadingScene.Instance.baseUrl;
					form_time.AddField ("tag", "doGetRandomFriends");
					form_time.AddField ("user_id",   PlayerDataParse._instance.playersParam.userId.ToString().Split (',') [0]);  
				//PlayerDataParse._instance.playersParam.userId);
					form_time.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
					WWW www = new WWW (URltime, form_time.data);

					searchFriendCount=0;
					StartCoroutine (userTIMEfetching1 (www, isSuccess => 
					{
						IDictionary cardDict = Json.Deserialize(www.text) as IDictionary;
						if(www.text.Contains("error_msg"))
						{
							//{"success":0,"error_msg":"No cards available!"}
							if(www.text.Contains("Data does not exist!"))
							{
								
								newMenuScene.instance.popupFromServer.ShowPopup ("Player not available!");
								deactivateContents(1);

							}
							else
							{
								newMenuScene.instance.popupFromServer.ShowPopup ("Network Error!");
							}
						}
						else
						{
							IList friendList = (IList)cardDict ["data"];
							for (int i = 0; i < friendList.Count; i++) 
							{
								IDictionary friendListDic = (IDictionary)friendList[i];
								print("cardList"+friendList.Count);
								Debug.Log(friendListDic["user_id"]);
								Debug.Log(friendListDic["userName"]);
								Debug.Log(friendListDic["login_datetime"]);
								Debug.Log(friendListDic["guild_name"]);
								Debug.Log(friendListDic["avatar_level"]);
								Debug.Log(friendListDic["avatar_no"]);
								if(friendListDic["guild_name"] == null)
									searchPlayerGuildName="empty";
								else
								{
									searchPlayerGuildName = (friendListDic ["guild_name"].ToString());

								}
								searchPlyerName = (friendListDic ["userName"].ToString());
								searchPlayerLogin = (friendListDic ["login_datetime"].ToString());
								if(friendListDic["avatar_level"] != null)
									int.TryParse (friendListDic["avatar_level"].ToString (), out searchPlayerLevel);
								
								if(friendListDic["avatar_no"] != null)
									int.TryParse (friendListDic["avatar_no"].ToString (), out searchAvatarNo);
								if(friendListDic["user_id"] != null)
									int.TryParse (friendListDic["user_id"].ToString (), out searchedPlayerId);
								GameObject newCard1 = (GameObject)Instantiate (Resources.Load ("searchFriend"));
								newCard1.transform.SetParent (searchFriendContainer.transform);
								newCard1.transform.localScale = Vector3.one;
								newCard1.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text=searchPlyerName;
								newCard1.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text="Lvl."+(searchPlayerLevel+1);
								newCard1.transform.GetChild(5).transform.GetChild(0).GetComponent<Text>().text=searchPlayerGuildName.ToString();
								newCard1.transform.GetChild(4).transform.GetChild(0).GetComponent<Text>().text="Last login:"+searchPlayerLogin;
								if(searchAvatarNo==1)
								{
									newCard1.transform.GetChild(2).GetComponent<Image>().sprite=searchPlayerProfile[0];
								}
								else if(searchAvatarNo==2)
								{
									newCard1.transform.GetChild(2).GetComponent<Image>().sprite=searchPlayerProfile[1];
								}
								else if(searchAvatarNo==3)
								{
									newCard1.transform.GetChild(2).GetComponent<Image>().sprite=searchPlayerProfile[2];
								}
								Debug.Log("searchAvatarNo"+ searchAvatarNo);

								newCard1.name=searchedPlayerId.ToString();
								searchedPlayer.Add (newCard1.gameObject);
//								searchedPlayerIdList.Add (searchedPlayerId);
								deactivateContents(1);

							}
						}
						
					}));
				} 
			else 
			{
					empireScene.instance.popupFromServer.ShowPopup ("Network Error!");
				}

			});


		
	}
	public IEnumerator userTIMEfetching1(WWW www , System.Action <bool> callback)
	{
		yield return www;

		if (www.error == null)
		{
			if(www.text.Contains ("error_msg"))
			{
				callback(false);
			}
			else
			{
				callback(true);
			}
			newMenuScene.instance.loader.SetActive (false);
			Debug.Log (www.text);

		}
		else
		{
			callback(false);
			newMenuScene.instance.loader.SetActive (false);
			Debug.Log ("No Found"+www.text);

		}
	}

	public IEnumerator userTIMEfetching2(WWW www , System.Action <bool> callback)
	{
		yield return www;

		if (www.error == null)
		{
			if(www.text.Contains ("error_msg"))
			{
				callback(false);
			}
			else
			{
				callback(true);
			}
			newMenuScene.instance.loader.SetActive (false);
			Debug.Log (www.text);

		}
		else
		{
			callback(false);
			newMenuScene.instance.loader.SetActive (false);
			Debug.Log ("Search Not Match"+www.text);

		}
	}

	public void searchFriendwithId( )
	{
		clearedSearched ();
		newMenuScene.instance.loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected) {
				WWWForm form_time = new WWWForm ();
				string URltime = loadingScene.Instance.baseUrl;
				form_time.AddField ("tag", "doGetRandomFriends");
				form_time.AddField ("user_id",   PlayerDataParse._instance.playersParam.userId.ToString().Split (',') [0]);  
				//PlayerDataParse._instance.playersParam.userId);
				form_time.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
				form_time.AddField ("username", searchField.text);
				WWW www = new WWW (URltime, form_time.data);

				StartCoroutine (userTIMEfetching2 (www, isSuccess => 
					{
						IDictionary cardDict = Json.Deserialize(www.text) as IDictionary;
						Debug.Log("text = "+(www.text));
						if(www.text.Contains("error_msg"))
						{
							//{"success":0,"error_msg":"No cards available!"}

								newMenuScene.instance.popupFromServer.ShowPopup ("No data matches your search!");
						}
						else
						{
							IList friendList = (IList)cardDict ["data"];
							for (int i = 0; i < friendList.Count; i++) 
							{
								//{"success":1,"data":[{"user_id":"1","userName":null,"login_datetime":null,
								//"guild_name":null,"avatar_level":"4","avatar_no":"3","Friend_data":[{"id":"31","user_id":"1","friend_id":"16","status":"0"}]}]}


								//{"success":1,"data":[{"user_id":"28","userName":"Jorgie","login_datetime":"0000-00-00 00:00:00",
								//"guild_name":null,"avatar_level":"0","avatar_no":"1","Friend_data":null}]}
								IDictionary friendListDic = (IDictionary)friendList[i];
								print("cardList"+friendList.Count);
								Debug.Log(friendListDic["user_id"]);
								Debug.Log(friendListDic["userName"]);
								Debug.Log(friendListDic["login_datetime"]);
								Debug.Log(friendListDic["guild_name"]);
								Debug.Log(friendListDic["avatar_level"]);
								Debug.Log(friendListDic["avatar_no"]);
								if(friendListDic["guild_name"] == null)
									searchPlayerGuildName="empty";
								else
								{
									searchPlayerGuildName = (friendListDic ["guild_name"].ToString());

								}
								searchPlyerName = (friendListDic ["userName"].ToString());
								searchPlayerLogin = (friendListDic ["login_datetime"].ToString());
								if(friendListDic["avatar_level"] != null)
									int.TryParse (friendListDic["avatar_level"].ToString (), out searchPlayerLevel);

								if(friendListDic["avatar_no"] != null)
									int.TryParse (friendListDic["avatar_no"].ToString (), out searchAvatarNo);
								if(friendListDic["user_id"] != null)
									int.TryParse (friendListDic["user_id"].ToString (), out searchedPlayerId);
								GameObject newCard1 = (GameObject)Instantiate (Resources.Load ("searchFriend"));
								newCard1.transform.SetParent (searchFriendContainer.transform);
								newCard1.transform.localScale = Vector3.one;
								newCard1.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text=searchPlyerName;
								newCard1.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text="Lvl."+(searchPlayerLevel+1);
								newCard1.transform.GetChild(5).transform.GetChild(0).GetComponent<Text>().text=searchPlayerGuildName.ToString();
								newCard1.transform.GetChild(4).transform.GetChild(0).GetComponent<Text>().text="Last login:"+searchPlayerLogin;
								if(friendListDic["Friend_data"] == null || string.IsNullOrEmpty(friendListDic["Friend_data"].ToString()))
								{
									
								}
								else
								{
									newCard1.transform.FindChild("friendRequst/frame/Text").GetComponent<Text>().text = "Invited!";
									newCard1.transform.FindChild("friendRequst").GetComponent<Button>().interactable  = false;
								}
								Debug.Log(friendListDic["avatar_level"]);
								if(searchAvatarNo==1)
								{
									newCard1.transform.GetChild(2).GetComponent<Image>().sprite=searchPlayerProfile[0];
								}
								else if(searchAvatarNo==2)
								{
									newCard1.transform.GetChild(2).GetComponent<Image>().sprite=searchPlayerProfile[1];
								}
								else if(searchAvatarNo==3)
								{
									newCard1.transform.GetChild(2).GetComponent<Image>().sprite=searchPlayerProfile[2];
								}
								Debug.Log("searchAvatarNo"+ searchAvatarNo);

								newCard1.name=searchedPlayerId.ToString();
								searchedPlayer.Add (newCard1.gameObject);
								//								searchedPlayerIdList.Add (searchedPlayerId);
								deactivateContents(1);

							}
						}

					}));
			} 
			else 
			{
				empireScene.instance.popupFromServer.ShowPopup ("Network Error!");
			}

		});



	}



	public void backButton()
	{

		Start();

		for(int i=0;i<loadingScene.Instance.scenes.Count;i++)
		{
			if(i==loadingScene.Instance.scenes.Count-1)
			{
				loadingScene.Instance.scenes[i].SetActive(false);
				loadingScene.Instance.scenes.RemoveAt(loadingScene.Instance.scenes.Count-1);
			}
			else
			{
				loadingScene.Instance.scenes[loadingScene.Instance.scenes.Count-2].SetActive(true);

				//loadingScene.Instance.scenes[i].SetActive(true);
				
				
			}
			
		}

	}

	public void QuestButton()
	{
		Start();

		PlayerPrefs.SetString("community","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			print("yes this work");
			menuScreen.SetActive(false);
			isMneuActive=false;
			
		}
		else
		{
			
		}
		loadingScene.Instance.quest ();
		//Application.LoadLevel("quest");
	}

	public void trade()
	{
		Start();


		PlayerPrefs.SetString("community","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			print("yes this work");
			menuScreen.SetActive(false);
			isMneuActive=false;
			
		}
		else
		{
			
		}
		loadingScene.Instance.trade();

		//Application.LoadLevel("trade");
	}

	public void ResetValues()
	{
		isMneuActive = false;
		for(int j=0;j<bottomsButtons.Length;j++)
		{
			bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
			bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
			bottomsButtons[j].GetComponent<Button>().interactable=true;
		}
	}

	void OnEnable()
	{
		totalFriends.text=PlayerParameters._instance.myPlayerParameter.FriendList+"/"+GetMaxFriends();
		clearedSearched ();
		listFriend();
		ResetMenu ();
	}
	
	void ResetMenu()
	{
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			print("yes this work");
			menuScreen.SetActive(false);
			isMneuActive=false;
		}
	}

	public void RootMenuButton()
	{
		loadingScene.Instance.objectToOpenMenu = this.gameObject;
		ResetMenu ();

		loadingScene.Instance.main ();
		//Application.LoadLevel("menuNew");
	}
	public void chatClick()
	{
		Start();

		PlayerPrefs.SetString("community","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			print("yes this work");
			menuScreen.SetActive(false);
			isMneuActive=false;
			
		}
		else
		{
			
		}
		loadingScene.Instance.chat();

		//Application.LoadLevel("chatScene");
	}
	public void empire()
	{
		Start();

		PlayerPrefs.SetString("community","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			print("yes this work");
			menuScreen.SetActive(false);
			isMneuActive=false;
			
		}
		else
		{
			
		}
		loadingScene.Instance.empire ();

		//Application.LoadLevel("empireScene");
	}
	public void battle()
	{
		Start();

		PlayerPrefs.SetString("community","yes");
		loadingScene.Instance.battleScene();

	//	Application.LoadLevel("Battle_Layout");
		
	}
	
	public void inventory()
	{
		Start();

		PlayerPrefs.SetString("community","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			print("yes this work");
			menuScreen.SetActive(false);
			isMneuActive=false;
			
		}
		else
		{
			
		}
		loadingScene.Instance.inventory();

		//Application.LoadLevel("inventoryScene");
	}
	public void cardCollections()
	{	
		Start();

		PlayerPrefs.SetString("community","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			print("yes this work");
			menuScreen.SetActive(false);
			isMneuActive=false;
			
		}
		else
		{
			
		}
		
	//	Application.LoadLevel("Battle_Layout4");
		loadingScene.Instance.BattleFormation();

	}
	public void shopScene()
	{
		Start();

		PlayerPrefs.SetString("community","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			print("yes this work");
			menuScreen.SetActive(false);
			isMneuActive=false;
			
		}
		else
		{
			
		}
		loadingScene.Instance.shop ();
		//Application.LoadLevel("shopScene");
	}
}
