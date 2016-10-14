using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MiniJSON;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ChatMechanics : MonoBehaviour {
//	public Text	GetText,PostText;
//	public Scrollbar ChatScroll;
	//-----  friend list -----
	string listPlyerName;
	string listPlayerGuildName;
	int friendId,friend_Id;
	string message,FriendName;
	Sprite friendAvatar,MyAvatar;
	string status;
	string listPlayerLogin;
	int listPlayerLevel;
	int listAvatarNo;
	public List<GameObject> listPlayer;
	public List<GameObject> InstObj;
	public List<string> chatId;
	string searchPlyerName;
	string searchPlayerGuildName;
	string searchPlayerLogin;
	int searchPlayerLevel;
	public Sprite[] searchPlayerProfile;
	int searchAvatarNo;
	public List<GameObject> searchedPlayer;
	//	public List <int> searchedPlayerIdList;
	public int searchedPlayerId;
	//	public List <int> searchedPlayerIdList;
	public int listPlayerId;
	public List<string> FriendId=new List<string>();
	public GameObject listFriendContainer,FriendPanel,FriendChatStart,
						MyMsgPanel,FriendMsgPanel,MsgChatPanelContainer,FriendList;
	public Text SendMsgText;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OnClick_1to1FriendChat()
	{
		
		FriendChatStart.SetActive (true);
		friendAvatar=EventSystem.current.currentSelectedGameObject.transform.parent.transform.GetChild(0).GetComponent<Image>().sprite;
		int.TryParse (EventSystem.current.currentSelectedGameObject.transform.parent.name.ToString(), out friend_Id);
		FriendName=EventSystem.current.currentSelectedGameObject.transform.parent.transform.GetChild(1).GetComponent<Text>().text;
		FriendList.SetActive (false);
		if(PlayerParameters._instance.myPlayerParameter.avatar_no ==1)
		{
			MyAvatar=searchPlayerProfile[0];
		}
		else if(PlayerParameters._instance.myPlayerParameter.avatar_no ==2)
		{
			MyAvatar=searchPlayerProfile[1];
		}
		else if(PlayerParameters._instance.myPlayerParameter.avatar_no ==3)
		{
			MyAvatar=searchPlayerProfile[2];
		}
		//FetchChat ();
		InvokeRepeating ("FetchChat", 0, 1f);
	}
	void FetchChat()
	{
		if (InstObj.Count > 20) {
			Destroy ((GameObject)InstObj [0]);
			InstObj.RemoveAt (0);
		}
	NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
		if (isConnected) {
			WWWForm form_time = new WWWForm ();
				string URltime = API.Instance.commonURL;
			form_time.AddField ("tag", "fetchUserChatsByFriend");
			form_time.AddField ("user_id",   PlayerDataParse._instance.playersParam.userId.ToString()); 
			form_time.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
			form_time.AddField ("friend_id", PlayerDataParse._instance.ID(friend_Id.ToString()));//PlayerDataParse._instance.ID(friend_Id.ToString())
			form_time.AddField ("max_count", "20");
			WWW www = new WWW (URltime, form_time.data);
				Debug.Log("www "+www);
			StartCoroutine (userTIMEfetching1 (www, isSuccess => 
				{

					IDictionary ChatDict = Json.Deserialize(www.text) as IDictionary;
					Debug.Log("text FriendChat = "+(www.text));
					if(www.text.Contains("error_msg"))
					{
						//{"success":0,"error_msg":"No cards available!"}
						//newMenuScene.instance.popupFromServer.ShowPopup ("You don't have any friend!");
						//deactivateContents(0);
					}
					else
					{
						IList ChatList = (IList)ChatDict ["data"];
							for (int i = (ChatList.Count-1); i >=0; i--) 
						{
							IDictionary ChatListDic = (IDictionary)ChatList[i];
							print("ChatList"+ChatList.Count);
							//								
							if(ChatListDic["friend_id"] != null){
									int.TryParse(ChatListDic ["friend_id"].ToString(), out friendId);

							}


							//status = (friendListDic ["description"].ToString());


							//listPlyerName = (friendListDic ["userName"].ToString());
							//								if(friendListDic["login_datetime"] != null)
							//									int.TryParse (friendListDic["login_datetime"].ToString (), out listPlayerLogin);
							//								listPlayerLogin = (friendListDic ["login_datetime"].ToString());
							if(ChatListDic["message"] != null)
								message= ChatListDic["message"].ToString ();
							if(ChatListDic["id"] != null)
								{
									if(!chatId.Contains(ChatListDic["id"].ToString ()))
									{
										chatId.Add( ChatListDic["id"].ToString ());

								if(friendId!=friend_Id)
								{
							GameObject ChatShowPanel = (GameObject)Instantiate (FriendMsgPanel);
											InstObj.Add(ChatShowPanel);
									ChatShowPanel.SetActive(true);
									ChatShowPanel.transform.SetParent (MsgChatPanelContainer.transform);
									ChatShowPanel.transform.localScale = Vector3.one;
									ChatShowPanel.transform.GetChild(0).transform.GetChild(0). GetComponent<Text>().text=FriendName+":\n"+ message;
									//ChatShowPanel.transform.GetChild(0).GetComponents<RectTransform>().
									ChatShowPanel.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().sprite=friendAvatar;



									}
								else{
									GameObject ChatShowPanel = (GameObject)Instantiate (MyMsgPanel);
											InstObj.Add(ChatShowPanel);
									ChatShowPanel.SetActive(true);
									ChatShowPanel.transform.SetParent (MsgChatPanelContainer.transform);
									ChatShowPanel.transform.localScale = Vector3.one;
									ChatShowPanel.transform.GetChild(0).transform.GetChild(0). GetComponent<Text>().text=PlayerDataParse._instance.playersParam.userName +":\n"+ message;
									ChatShowPanel.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().sprite=MyAvatar;



										}
									}
								}


							//newCard1.name=listPlayerId.ToString();
							//listPlayer.Add (newCard1.gameObject);
							//								searchedPlayerIdList.Add (searchedPlayerId);
							//deactivateContents(0);
							//totalFriends.text=PlayerParameters._instance.myPlayerParameter.FriendList+"/"+GetMaxFriends();

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
//	public void OnClick_FriendChatOpen()
//	{
//		//string playerID;
//		Debug.LogError(PlayerDataParse._instance.ID(EventSystem.current.currentSelectedGameObject.transform.parent.name));
//		//playerID = PlayerDataParse._instance.ID( FriendId [0].ToString());
//		//Debug.Log ("FriendID After" + playerID);
//		//newMenuScene.instance.loader.SetActive (true);
//		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
//			if (isConnected) {
//				WWWForm form_time = new WWWForm ();
//				string URltime = loadingScene.Instance.baseUrl;
//				form_time.AddField ("tag", "userFriendChatsCreate");
//				form_time.AddField ("user_id",   PlayerDataParse._instance.playersParam.userId.ToString()); 
//				form_time.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
//				form_time.AddField ("friend_id", PlayerDataParse._instance.ID(EventSystem.current.currentSelectedGameObject.transform.parent.name));
//				form_time.AddField ("max_count", "20");
//				WWW www = new WWW (URltime, form_time.data);
//
//				StartCoroutine (userTIMEfetching1 (www, isSuccess => 
//					{
//						IDictionary cardDict = Json.Deserialize(www.text) as IDictionary;
//						Debug.Log("text FriendChat = "+(www.text));
//						if(www.text.Contains("error_msg"))
//						{
//							//{"success":0,"error_msg":"No cards available!"}
//							newMenuScene.instance.popupFromServer.ShowPopup ("You don't have any friend!");
//							//deactivateContents(0);
//						}
//					/*	else
//						{
//							IList friendList = (IList)cardDict ["data"];
//							for (int i = 0; i < friendList.Count; i++) 
//							{
//								IDictionary friendListDic = (IDictionary)friendList[i];
//								print("cardList"+friendList.Count);
//								//								
//								if(friendListDic["guild_name"] == null)
//									listPlayerGuildName="empty";
//								else
//								{
//									listPlayerGuildName = (friendListDic ["guild_name"].ToString());
//
//								}
//
//
//								//status = (friendListDic ["description"].ToString());
//
//
//								//listPlyerName = (friendListDic ["userName"].ToString());
//								//								if(friendListDic["login_datetime"] != null)
//								//									int.TryParse (friendListDic["login_datetime"].ToString (), out listPlayerLogin);
//								//								listPlayerLogin = (friendListDic ["login_datetime"].ToString());
//								if(friendListDic["avatar_level"] != null)
//									int.TryParse (friendListDic["avatar_level"].ToString (), out listPlayerLevel);
//
//								if(friendListDic["avatar_no"] != null)
//									int.TryParse (friendListDic["avatar_no"].ToString (), out listAvatarNo);
//								if(friendListDic["user_id"] != null)
//									int.TryParse (friendListDic["friend_id"].ToString (), out listPlayerId);
//								listPlyerName = (friendListDic ["userName"].ToString());
//								listPlayerLogin = (friendListDic ["login_datetime"].ToString());
//
//
//								GameObject newCard1 = (GameObject)Instantiate (Resources.Load ("ChatlistFriend"));
//								newCard1.transform.SetParent (listFriendContainer.transform);
//								newCard1.transform.localScale = Vector3.one;
//								newCard1.transform.GetChild(1).GetComponent<Text>().text=listPlyerName;
//								newCard1.transform.GetChild(5).GetComponent<Text>().text=""+status;
//								newCard1.transform.GetChild(3).GetComponent<Text>().text=listPlayerGuildName.ToString();
//								newCard1.transform.GetChild(2).GetComponent<Text>().text="Last login:"+listPlayerLogin;
//								//print("lastlogin" +(friendListDic ["login_datetime"].ToString()));
//								if(listAvatarNo==1)
//								{
//									newCard1.transform.GetChild(0).GetComponent<Image>().sprite=searchPlayerProfile[0];
//								}
//								else if(listAvatarNo==2)
//								{
//									newCard1.transform.GetChild(0).GetComponent<Image>().sprite=searchPlayerProfile[1];
//								}
//								else if(listAvatarNo==3)
//								{
//									newCard1.transform.GetChild(0).GetComponent<Image>().sprite=searchPlayerProfile[2];
//								}
//
//								newCard1.name=listPlayerId.ToString();
//								listPlayer.Add (newCard1.gameObject);
//								//								searchedPlayerIdList.Add (searchedPlayerId);
//								//deactivateContents(0);
//								//totalFriends.text=PlayerParameters._instance.myPlayerParameter.FriendList+"/"+GetMaxFriends();
//
//							}
//						}*/
//
//					}
//				));
//			} 
//			else 
//			{
//				empireScene.instance.popupFromServer.ShowPopup ("Network Error!");
//			}
//
//		});
//	}
	public void ChatBack()
	{
		CancelInvoke ("FetchChat");
		chat1.instance.backButton ();


	}
	public void SendChatMessage()
	{
		SendChat ();
		SendMsgText.text = "";
	}
	void SendChat(){
			NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
				if (isConnected) {
					WWWForm form_time = new WWWForm ();
					string URltime = API.Instance.commonURL;
					form_time.AddField ("tag", "userFriendChatsCreate");
					form_time.AddField ("user_id",   PlayerDataParse._instance.playersParam.userId.ToString()); 
					form_time.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
				form_time.AddField ("friend_id", PlayerDataParse._instance.ID(friend_Id.ToString()));//PlayerDataParse._instance.ID(friend_Id.ToString())
					form_time.AddField ("message", SendMsgText.text.ToString());
					form_time.AddField ("flag", "1");
					WWW www = new WWW (URltime, form_time.data);
					Debug.Log("www "+www);
					StartCoroutine (userTIMEfetching1 (www, isSuccess => 
						{
							IDictionary ChatDict = Json.Deserialize(www.text) as IDictionary;
							Debug.Log("text FriendChat = "+(www.text));
							if(www.text.Contains("error_msg"))
							{
								//{"success":0,"error_msg":"No cards available!"}
								newMenuScene.instance.popupFromServer.ShowPopup ("You don't have any friend!");
								//deactivateContents(0);
							}
							else
							{
								Debug.Log("Success");
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
		for(int i = listPlayer.Count - 1; i > -1; i--)
		{
			//if (itemList[i] == null)
			Destroy (listPlayer[i]);
			listPlayer.RemoveAt(i);
		}
		FriendList.SetActive (true);
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
							//deactivateContents(0);
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


								//status = (friendListDic ["description"].ToString());

							
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

								FriendId.Add(PlayerDataParse._instance.ID( listPlayerId.ToString()));
//								if(!FriendId.Contains(listPlayerId.ToString())
//									{
								GameObject newCard1 = (GameObject)Instantiate (FriendPanel);
								newCard1.SetActive(true);
								newCard1.transform.SetParent (listFriendContainer.transform);
								newCard1.transform.localScale = Vector3.one;
								newCard1.transform.GetChild(1).GetComponent<Text>().text=listPlyerName;
								newCard1.transform.GetChild(5).GetComponent<Text>().text=""+status;
								newCard1.transform.GetChild(3).GetComponent<Text>().text=listPlayerGuildName.ToString();
								newCard1.transform.GetChild(2).GetComponent<Text>().text="Last login:"+listPlayerLogin;
								//print("lastlogin" +(friendListDic ["login_datetime"].ToString()));
								if(listAvatarNo==1)
								{
									newCard1.transform.GetChild(0).GetComponent<Image>().sprite=searchPlayerProfile[0];
								}
								else if(listAvatarNo==2)
								{
									newCard1.transform.GetChild(0).GetComponent<Image>().sprite=searchPlayerProfile[1];
								}
								else if(listAvatarNo==3)
								{
									newCard1.transform.GetChild(0).GetComponent<Image>().sprite=searchPlayerProfile[2];
								}

								newCard1.name=listPlayerId.ToString();
								listPlayer.Add (newCard1.gameObject);
									
								//								searchedPlayerIdList.Add (searchedPlayerId);
								//deactivateContents(0);
								//totalFriends.text=PlayerParameters._instance.myPlayerParameter.FriendList+"/"+GetMaxFriends();

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
}
