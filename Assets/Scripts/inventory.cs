using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using MiniJSON;

public class inventory : MonoBehaviour 
{
	public static inventory instance;
	public GameObject setting;
	public GameObject menuScreen;
	public GameObject chatBtn;
	bool isMneuActive=false;
	public Button[] bottomsButtons;
	public GameObject [] EventContent;
	public Button [] outlineClick;
	public Sprite[] itemsResource;

	public GameObject cardContainer;
	public GameObject itemContainer;
	public GameObject dragonContainer;
	public GameObject allContainer;
	public List<GameObject> cardList;
	public List<GameObject> itemList;
	public List<GameObject> dragonList;
	public List<GameObject> allList;
	GameObject peaceTreatyObject;
	GameObject attackPotionObject;
	GameObject staminaPotionObject;
	GameObject signalFireObject;

	public void Awake()
	{
		instance = this;
	}
	// Use this for initialization
	void Start ()
	{
		loadingScene.Instance.playerProfilePanel.SetActive(false);
		setting.SetActive(false);
		deactivateContent(0);

	}
	void OnDisable()
	{
		for(int i = itemList.Count - 1; i > -1; i--)
		{
			Destroy (itemList[i]);
			itemList.RemoveAt(i);
		}
		for(int j = dragonList.Count - 1; j > -1; j--)
		{
			Destroy (dragonList[j]);
			dragonList.RemoveAt(j);
		}
		for(int k = allList.Count - 1; k > -1; k--)
		{
			Destroy (allList[k]);
			allList.RemoveAt(k);
		}

	}
	public IEnumerator userTIMEfetching(WWW www , System.Action <bool> callback)
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
	public void itemsContent()
	{
		for(int j = dragonList.Count - 1; j > -1; j--)
		{
			Destroy (dragonList[j]);
			dragonList.RemoveAt(j);
		}
		for(int k = allList.Count - 1; k > -1; k--)
		{
			Destroy (allList[k]);
			allList.RemoveAt(k);
		}
		newMenuScene.instance.loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected) {
				WWWForm form_time = new WWWForm ();
				string URltime = loadingScene.Instance.baseUrl;
				form_time.AddField ("tag", "doGetUserStash");
				form_time.AddField ("user_id",   PlayerDataParse._instance.playersParam.userId.ToString()); 
				form_time.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
				form_time.AddField ("type", "1");
				WWW www = new WWW (URltime, form_time.data);

				StartCoroutine (userTIMEfetching (www, isSuccess => 
					{
						IDictionary cardDict = Json.Deserialize(www.text) as IDictionary;

						if(www.text.Contains("error_msg"))
						{
							//{"success":0,"error_msg":"No cards available!"}
							//deactivateContent(1);
							for(int j = dragonList.Count - 1; j > -1; j--)
							{
								Destroy (dragonList[j]);
								dragonList.RemoveAt(j);
							}
							newMenuScene.instance.popupFromServer.ShowPopup ("You don't have any item request!");
							Debug.Log("text = "+(www.text));

						}
						else
						{
							
							Debug.Log("text = "+(www.text));
							IList friendList = (IList)cardDict ["data"];
							for (int i = 0; i < friendList.Count; i++) 
							{
								IDictionary friendListDic = (IDictionary)friendList[i];
								print("itemsList"+friendList.Count);
								//deactivateContent(1);
								for(int j = dragonList.Count - 1; j > -1; j--)
								{
									Destroy (dragonList[j]);
									dragonList.RemoveAt(j);
								}
								GameObject item = (GameObject)Instantiate (Resources.Load ("stashItemCollect"));

								item.transform.SetParent (itemContainer.transform);
								item.transform.localScale = Vector3.one;
								Debug.Log("item name"+friendListDic["item_name"]);



								if(friendListDic["item_name"].ToString() == "attack_potion")
								{
									string count;
									int counter=0;
									item.transform.GetChild(1).GetComponent<Text>().text= "Attack Potion";
									item.transform.GetChild(0).GetComponent<Image>().sprite= itemsResource[1];
									count=friendListDic["count"].ToString();
									item.transform.GetChild(2).GetComponent<Text>().text= "Total x"+count;
									if(friendListDic["count"] != null)
										int.TryParse (friendListDic["count"].ToString (), out counter);
									item.GetComponent<collectStash>()._myCount=counter;

								}
								else if(friendListDic["item_name"].ToString() == "stamina_potion")
								{
									string count;
									int counter=0;
									item.transform.GetChild(1).GetComponent<Text>().text= "Stamina Potion";
									item.transform.GetChild(0).GetComponent<Image>().sprite= itemsResource[2];
									count=friendListDic["count"].ToString();
									item.transform.GetChild(2).GetComponent<Text>().text= "Total x"+count;
									if(friendListDic["count"] != null)
										int.TryParse (friendListDic["count"].ToString (), out counter);
									item.GetComponent<collectStash>()._myCount=counter;
								}


								else if(friendListDic["item_name"].ToString() == "peace_treaty")
								{
									string count;
									int counter=0;
									item.transform.GetChild(1).GetComponent<Text>().text= "Peace Treaties";
									item.transform.GetChild(0).GetComponent<Image>().sprite= itemsResource[0];
									count=friendListDic["count"].ToString();
									item.transform.GetChild(2).GetComponent<Text>().text= "Total x"+count;
									if(friendListDic["count"] != null)
										int.TryParse (friendListDic["count"].ToString (), out counter);
									item.GetComponent<collectStash>()._myCount=counter;
								}
								else if(friendListDic["item_name"].ToString() == "signal_fire")
								{
									string count;
									int counter=0;
									item.transform.GetChild(1).GetComponent<Text>().text= "Signal Fire";
									item.transform.GetChild(0).GetComponent<Image>().sprite= itemsResource[0];
									count=friendListDic["count"].ToString();
									item.transform.GetChild(2).GetComponent<Text>().text= "Total x"+count;
									if(friendListDic["count"] != null)
										int.TryParse (friendListDic["count"].ToString (), out counter);
									item.GetComponent<collectStash>()._myCount=counter;
								}
								item.name=friendListDic["id"].ToString();
								peaceTreatyObject = item.gameObject;
								itemList.Add (item.gameObject);
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


	public void dragonCoin()
	{
		for(int j = itemList.Count - 1; j > -1; j--)
		{
			Destroy (itemList[j]);
			itemList.RemoveAt(j);
		}
		for(int k = allList.Count - 1; k > -1; k--)
		{
			Destroy (allList[k]);
			allList.RemoveAt(k);
		}
		newMenuScene.instance.loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected) {
				WWWForm form_time = new WWWForm ();
				string URltime = loadingScene.Instance.baseUrl;
				form_time.AddField ("tag", "doGetUserStash");
				form_time.AddField ("user_id",   PlayerDataParse._instance.playersParam.userId.ToString()); 
				form_time.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
				form_time.AddField ("type", "1");
				WWW www = new WWW (URltime, form_time.data);

				StartCoroutine (userTIMEfetching (www, isSuccess => 
					{
						IDictionary cardDict = Json.Deserialize(www.text) as IDictionary;

						if(www.text.Contains("error_msg"))
						{
							//{"success":0,"error_msg":"No cards available!"}
							//deactivateContent(2);
							for(int j = itemList.Count - 1; j > -1; j--)
							{
								Destroy (itemList[j]);
								itemList.RemoveAt(j);
							}
							newMenuScene.instance.popupFromServer.ShowPopup ("You don't have any dragon coin request!");
							Debug.Log("text = "+(www.text));

						}
						else
						{

							Debug.Log("text = "+(www.text));
							IList friendList = (IList)cardDict ["data"];
							for (int i = 0; i < friendList.Count; i++) 
							{
								IDictionary friendListDic = (IDictionary)friendList[i];
								print("itemsList"+friendList.Count);
								//deactivateContent(2);

								GameObject item = (GameObject)Instantiate (Resources.Load ("stashItemCollect"));

								item.transform.SetParent (itemContainer.transform);
								item.transform.localScale = Vector3.one;



								if(friendListDic["item_name"].ToString() == "dragon_coins")
								{
									string count;
									int counter=0;
									item.transform.GetChild(1).GetComponent<Text>().text= "Dragon Coins";
									item.transform.GetChild(0).GetComponent<Image>().sprite= itemsResource[4];
									count=friendListDic["count"].ToString();
									item.transform.GetChild(2).GetComponent<Text>().text= "Total x"+count;
									if(friendListDic["count"] != null)
										int.TryParse (friendListDic["count"].ToString (), out counter);
									item.GetComponent<collectStash>()._myCount=counter;

								}

								item.name=friendListDic["id"].ToString();
								peaceTreatyObject = item.gameObject;
								dragonList.Add (item.gameObject);
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
	public void usePeaceTreaties(Button button)
	{
		if (PlayerParameters._instance.myPlayerParameter.no_of_peace_treaties >=1) 
		{
			if (PlayerParameters._instance.myPlayerParameter.peace_treaty >= 1) 
			{
				newMenuScene.instance.popupFromServer.ShowPopup ("You have already used !");
			}
			else
			{


				Dictionary<string, string> avatarParameters = new Dictionary<string, string> ();
			
				avatarParameters.Add ("peace_treaty", (PlayerParameters._instance.myPlayerParameter.peace_treaty = 1).ToString ());
				avatarParameters.Add ("no_of_peace_treaties", (PlayerParameters._instance.myPlayerParameter.no_of_peace_treaties--).ToString ());
				avatarParameters.Add ("peace_treaty_start_time", (TimeManager._instance.GetCurrentServerTime()).ToString ());
				avatarParameters.Add ("peace_treaty_active_time", (PlayerParameters._instance.myPlayerParameter.peace_treaty_active_time = 1).ToString ());
					StartCoroutine (PlayerParameters._instance.SendPlayerParameters (avatarParameters, isSuccess => 
						{
							if(isSuccess)
							{
								newMenuScene.instance.popupFromServer.ShowPopup ("Peace Treaty used !");
								peaceTreatyObject.transform.GetChild(2).GetComponent<Text>().text= "Total x"+PlayerParameters._instance.myPlayerParameter.no_of_peace_treaties.ToString();
									//attackPotionObject.transform.GetChild(2).GetComponent<Text>().text= "Total x"+PlayerParameters._instance.myPlayerParameter.attack_potion.ToString();

							}
							else
							{
							newMenuScene.instance.popupFromServer.ShowPopup ("You have not enough Peace treaty !");
							}
						}));



				}
		
		
			}
		}


	public void useAttackPotion(Button button)
	{
		loadingScene.Instance.loader.SetActive (true);
		Debug.Log ("AttackPotionClick" + button);

		if (PlayerParameters._instance.myPlayerParameter.attack_potion > 0)
//			|| PlayerParameters._instance.myPlayerParameter.unlocked_attack_potion > 0) 
		{
				Dictionary<string, string> avatarParameters = new Dictionary<string, string> ();
				avatarParameters.Add ("orb",PlayerParameters._instance.myPlayerParameter.maxOrb.ToString ());
				avatarParameters.Add ("orb_time", TimeManager._instance.GetCurrentServerTime().ToString ());
			if(PlayerParameters._instance.myPlayerParameter.attack_potion > 0)
				avatarParameters.Add ("attack_potion",(PlayerParameters._instance.myPlayerParameter.attack_potion-1).ToString ());
				//else
				StartCoroutine (PlayerParameters._instance.SendPlayerParameters (avatarParameters, isSuccess => 
				{
					if(isSuccess)
					{
						PlayerParameters._instance.myPlayerParameter.orb = PlayerParameters._instance.myPlayerParameter.maxOrb;
						PlayerParameters._instance.myPlayerParameter.orb_time = TimeManager._instance.GetCurrentServerTime();
						if(PlayerParameters._instance.myPlayerParameter.attack_potion > 0)
							PlayerParameters._instance.myPlayerParameter.attack_potion--;
					//	else
//						PlayerParameters._instance.myPlayerParameter.unlocked_attack_potion--;
							newMenuScene.instance.popupFromServer.ShowPopup ("Attack Potion is used!");

								attackPotionObject.transform.GetChild(2).GetComponent<Text>().text= "Total x"+PlayerParameters._instance.myPlayerParameter.attack_potion.ToString();

					}
					else
					{
						newMenuScene.instance.popupFromServer.ShowPopup ("Could not used attack potion at this time!");
					}
				}));
			}
			else
			{
			newMenuScene.instance.popupFromServer.ShowPopup ("You do not have any attack potion !");
			}

	}

	public void useStaminaPotion(Button button)
	{
		loadingScene.Instance.loader.SetActive (true);
		Debug.Log ("StaminaPotionClick" + button);
		if (PlayerParameters._instance.myPlayerParameter.stamina_potion > 0)
			//			|| PlayerParameters._instance.myPlayerParameter.unlocked_attack_potion > 0) 
		{
			Debug.Log ("StaminaPotionClick1" );
			Dictionary<string, string> avatarParameters = new Dictionary<string, string> ();
			avatarParameters.Add ("orb",PlayerParameters._instance.myPlayerParameter.maxOrb.ToString ());
			avatarParameters.Add ("orb_time", TimeManager._instance.GetCurrentServerTime().ToString ());
			if(PlayerParameters._instance.myPlayerParameter.stamina_potion > 0)
				avatarParameters.Add ("stamina_potion",(PlayerParameters._instance.myPlayerParameter.stamina_potion-1).ToString ());
			//else
				StartCoroutine (PlayerParameters._instance.SendPlayerParameters (avatarParameters, isSuccess => 
					{
						Debug.Log ("StaminaPotionClick2" );

						if(isSuccess)
						{
							Debug.Log ("StaminaPotionClick3" );

							PlayerParameters._instance.myPlayerParameter.orb = PlayerParameters._instance.myPlayerParameter.maxOrb;
							PlayerParameters._instance.myPlayerParameter.orb_time = TimeManager._instance.GetCurrentServerTime();
							if(PlayerParameters._instance.myPlayerParameter.stamina_potion > 0)
								PlayerParameters._instance.myPlayerParameter.stamina_potion--;
							//else
								newMenuScene.instance.popupFromServer.ShowPopup ("Stamina Potion is used!");
								//						PlayerParameters._instance.myPlayerParameter.unlocked_attack_potion--;
								staminaPotionObject.transform.GetChild(2).GetComponent<Text>().text= "Total x"+PlayerParameters._instance.myPlayerParameter.stamina_potion.ToString();

						}
						else
						{
							newMenuScene.instance.popupFromServer.ShowPopup ("Could not used stamina potion at this time!");
						}
					}));
		}
		else
		{
			newMenuScene.instance.popupFromServer.ShowPopup ("You do not have any stamina potion !");
		}

	}

	public void ReplenishArmy(Button button)
	{

		if (loadingScene.Instance.myBattleFormation.cardDecks [0].noOfCardsSelected == 0 ||
		   loadingScene.Instance.myBattleFormation.cardDecks [1].noOfCardsSelected == 0 ||
		   loadingScene.Instance.myBattleFormation.cardDecks [2].noOfCardsSelected == 0) {
			newMenuScene.instance.popupFromServer.ShowPopup ("You have not selected any battle formation!");
			return;
		}
		loadingScene.Instance.loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected) {
				string cardIds = "";
				string cardSoldiers = "";
				string initialCardSoldiers = "";
				string initialAllSoldiers = CardsManager._instance.InitialCardSoldiers();
				if (PlayerParameters._instance.myPlayerParameter.signal_fire > 0) {
					PlayerParameters._instance.myPlayerParameter.signal_fire--;
					signalFireObject.transform.GetChild(2).GetComponent<Text>().text= "Total x"+PlayerParameters._instance.myPlayerParameter.signal_fire.ToString();

					for (int k = 0; k < loadingScene.Instance.myBattleFormation.cardDecks.Count; k++) {
						for (int i = 0; i < loadingScene.Instance.myBattleFormation.cardDecks [k].cardRows.Count; i++) {
							for (int j = 0; j < loadingScene.Instance.myBattleFormation.cardDecks [k].cardRows[i].cardIdsForRow.Count; j++) {
								int cardNoInMyCards = CardsManager._instance.PositionOfCardInList (loadingScene.Instance.myBattleFormation.cardDecks [k].cardRows [i].cardIdsForRow [j]);
								if(!cardIds.Contains (loadingScene.Instance.myBattleFormation.cardDecks [k].cardRows [i].cardIdsForRow [j]+","))
								{
									cardIds+=loadingScene.Instance.myBattleFormation.cardDecks [k].cardRows [i].cardIdsForRow [j]+",";
									cardSoldiers+=CardsManager._instance.mycards [cardNoInMyCards].leadership+",";
									initialCardSoldiers+=CardsManager._instance.mycards [cardNoInMyCards].card_soldiers+",";
								}
								CardsManager.CardParameters a = CardsManager._instance.mycards [cardNoInMyCards];
								a.card_soldiers = CardsManager._instance.mycards [cardNoInMyCards].leadership;
								CardsManager._instance.mycards [cardNoInMyCards] = a;

							}
						}
					}

					int initialDeployedSoldiers = PlayerParameters._instance.myPlayerParameter.currentlyDeployedSoldiers;
					int initialAvailableSoldiers = PlayerParameters._instance.myPlayerParameter.currentlyAvailableSoldiers;
					string initialCardIds = cardIds;
					PlayerParameters._instance.SetSoldiersCount (ref cardIds , ref initialCardSoldiers , ref cardSoldiers);
					if (initialCardIds != cardIds)
						initialCardSoldiers = initialAllSoldiers;
					if(cardIds.Length > 0)
					{
						cardIds = cardIds.Remove (cardIds.Length-1);
						cardSoldiers = cardSoldiers.Remove (cardSoldiers.Length-1);
						initialCardSoldiers = initialCardSoldiers.Remove (initialCardSoldiers.Length-1);
					}
					Dictionary<string, string> avatarParameters = new Dictionary<string, string> ();
					avatarParameters.Add ("currently_deployed_soldiers",PlayerParameters._instance.myPlayerParameter.currentlyDeployedSoldiers.ToString ());
					avatarParameters.Add ("currently_available_soldiers",PlayerParameters._instance.myPlayerParameter.currentlyAvailableSoldiers.ToString ());
					avatarParameters.Add ("signal_fire" , PlayerParameters._instance.myPlayerParameter.signal_fire.ToString ());
					StartCoroutine (CardsManager._instance.SendCardSoldiers (cardIds,cardSoldiers,avatarParameters , isSuccess =>{
						if(!isSuccess)
						{
							newMenuScene.instance.popupFromServer.ShowPopup ("Could not Replenish Army!");
							PlayerParameters._instance.myPlayerParameter.signal_fire--;
							signalFireObject.transform.GetChild(2).GetComponent<Text>().text= "Total x"+PlayerParameters._instance.myPlayerParameter.signal_fire.ToString();

							PlayerParameters._instance.myPlayerParameter.currentlyDeployedSoldiers = initialDeployedSoldiers;
							PlayerParameters._instance.myPlayerParameter.currentlyAvailableSoldiers = initialAvailableSoldiers;
							string []cardIdArray = cardIds.Split (',');
							string []cardSoldiersArrayInitially = initialCardSoldiers.Split (',');
							for(int i = 0 ; i < cardIdArray.Length ; i++)
							{
								int cardNoInMyCards = CardsManager._instance.PositionOfCardInList (int.Parse(cardIdArray[i]));
								CardsManager.CardParameters a = CardsManager._instance.mycards [cardNoInMyCards];
								a.card_soldiers = int.Parse(cardSoldiersArrayInitially[i]);
								CardsManager._instance.mycards [cardNoInMyCards] = a;
							}
						}
						else
						{
							newMenuScene.instance.popupFromServer.ShowPopup ("Army Replenished Successfully !");
							//questNew.instance.DisplayFinalText();
						}
					}));
				} else {
					newMenuScene.instance.popupFromServer.ShowPopup ("Network Error !");
				}
			}
		});

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
	public void notificationOnOff()
	{
		loadingScene.Instance.notificationOnOff();
	}
	
	
	public void soundOnOff()
	{
		
		loadingScene.Instance.soundOnOff ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	
	}
	public void logOut()
	{
		deactivateContent(0);
		onClickSettingExit();
		PlayerPrefs.SetString("logout","yes");
		loadingScene.Instance.main();
		//LoginScene.SetActive(true);
	}

	public void chatButton()
	{
		if(PlayerPrefs.GetString("chat")=="off")
		{
			chatBtn.GetComponent<DragHandeler>().enabled=true;
			chatBtn.GetComponent<CanvasGroup>().blocksRaycasts=true;
			
			chatBtn.GetComponent<Button>().interactable=true;
			chatBtn.GetComponentInChildren<Text>().enabled=true;
			PlayerPrefs.SetString("chat","on");
			
			
		}
		
	}


	//-----------------------

	public void chatClick()
	{
		deactivateContent(0);

		PlayerPrefs.SetString("inventoryScene","yes");
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
		loadingScene.Instance.chat ();

		//Application.LoadLevel("chatScene");
	}
	public void cardCollections()
	{
		deactivateContent(0);

		PlayerPrefs.SetString("inventoryScene","yes");
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
		loadingScene.Instance.BattleFormation ();

		//Application.LoadLevel("Battle_Layout4");
	}
	public void shopScene()
	{
		deactivateContent(0);

		PlayerPrefs.SetString("inventoryScene","yes");
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

	
	
	
	public void battle()
	{
		deactivateContent(0);

		PlayerPrefs.SetString("inventoryScene","yes");
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
		loadingScene.Instance.battleScene ();

		//Application.LoadLevel("Battle_Layout");
		
	}
	public void quest()
	{
		deactivateContent(0);

		PlayerPrefs.SetString("inventoryScene","yes");
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

	//	Application.LoadLevel("quest");
		
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

	public void rootMenu()
	{
		deactivateContent(0);

		ResetMenu ();
		loadingScene.Instance.objectToOpenMenu = this.gameObject;
		loadingScene.Instance.main ();
		//Application.LoadLevel("menuNew");
	}
	public void community()
	{
		deactivateContent(0);

		PlayerPrefs.SetString("inventoryScene","yes");
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
		loadingScene.Instance.community ();

	//	Application.LoadLevel("community");
		
	}
	public void trade()
	{
		deactivateContent(0);

		PlayerPrefs.SetString("inventoryScene","yes");
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
		loadingScene.Instance.trade ();

		//Application.LoadLevel("trade");
	}

	public void empire()
	{
		deactivateContent(0);

		PlayerPrefs.SetString("inventoryScene","yes");
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
	//-----------------------

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

	public void onClickProfile()
	{
		loadingScene.Instance.playerProfilePanel.SetActive(true);

	}
	public void onClickProfileExit()
	{
		loadingScene.Instance.playerProfilePanel.SetActive(false);

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
	public void clickOnContent(Button clickEvent)
	{

	
		if(clickEvent.name=="cards")
		{
			deactivateContent (0);
		}

		else if(clickEvent.name=="items")
		{
			deactivateContent (1);
			itemsContent ();

		}
		else if(clickEvent.name=="dragonCoins")
		{
			deactivateContent (2);
			dragonCoin ();

		}
		else if(clickEvent.name=="all")
		{
			deactivateContent (3);
			allcontents ();
			
		}
	}

	void clearedSearched()
	{
		for (int i = itemList.Count-1; i >= 0; i--) {
			if (itemList [i] != null)
			{
				Destroy (itemList [i]);
				itemList.RemoveAt (i);
				//				Destroy (searchedPlayerIdList [i]);

			}
		}
	}
	void clearedDragon()
	{
		for (int i = dragonList.Count-1; i >= 0; i--) {
			if (dragonList [i] != null)
			{
				Destroy (dragonList [i]);
				dragonList.RemoveAt (i);
				//				Destroy (searchedPlayerIdList [i]);

			}
		}
	}


	void tempDragon()
	{
		for(int i = itemList.Count - 1; i > -1; i--)
		{
			Destroy (itemList[i]);
			itemList.RemoveAt(i);
		}

		for(int k = allList.Count - 1; k > -1; k--)
		{
			Destroy (allList[k]);
			allList.RemoveAt(k);
		}
		newMenuScene.instance.loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected) {
				WWWForm form_time = new WWWForm ();
				string URltime = loadingScene.Instance.baseUrl;
				form_time.AddField ("tag", "doGetUserStash");
				form_time.AddField ("user_id",   PlayerDataParse._instance.playersParam.userId.ToString()); 
				form_time.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
				form_time.AddField ("type", "1");
				WWW www = new WWW (URltime, form_time.data);

				StartCoroutine (userTIMEfetching (www, isSuccess => 
					{
						IDictionary cardDict = Json.Deserialize(www.text) as IDictionary;

						if(www.text.Contains("error_msg"))
						{
							//{"success":0,"error_msg":"No cards available!"}

							//newMenuScene.instance.popupFromServer.ShowPopup ("You don't have any dragon coin request!");
							Debug.Log("text = "+(www.text));

						}
						else
						{

							Debug.Log("text = "+(www.text));
							IList friendList = (IList)cardDict ["data"];
							for (int i = 0; i < friendList.Count; i++) 
							{
								IDictionary friendListDic = (IDictionary)friendList[i];
								print("itemsList"+friendList.Count);


								GameObject item = (GameObject)Instantiate (Resources.Load ("stashItemCollect"));

								item.transform.SetParent (allContainer.transform);
								item.transform.localScale = Vector3.one;



								if(friendListDic["item_name"].ToString() == "dragon_coins")
								{
									string count;
									int counter=0;
									item.transform.GetChild(1).GetComponent<Text>().text= "Dragon Coins";
									item.transform.GetChild(0).GetComponent<Image>().sprite= itemsResource[4];
									count=friendListDic["count"].ToString();
									item.transform.GetChild(2).GetComponent<Text>().text= "Total x"+count;
									if(friendListDic["count"] != null)
										int.TryParse (friendListDic["count"].ToString (), out counter);
									item.GetComponent<collectStash>()._myCount=counter;

								}

								item.name=friendListDic["id"].ToString();
								peaceTreatyObject = item.gameObject;
								allList.Add (item.gameObject);
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

	void tempItem()
	{
		
		for(int j = dragonList.Count - 1; j > -1; j--)
		{
			Destroy (dragonList[j]);
			dragonList.RemoveAt(j);
		}
		for(int k = allList.Count - 1; k > -1; k--)
		{
			Destroy (allList[k]);
			allList.RemoveAt(k);
		}
		newMenuScene.instance.loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected) {
				WWWForm form_time = new WWWForm ();
				string URltime = loadingScene.Instance.baseUrl;
				form_time.AddField ("tag", "doGetUserStash");
				form_time.AddField ("user_id",   PlayerDataParse._instance.playersParam.userId.ToString()); 
				form_time.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
				form_time.AddField ("type", "1");
				WWW www = new WWW (URltime, form_time.data);

				StartCoroutine (userTIMEfetching (www, isSuccess => 
					{
						IDictionary cardDict = Json.Deserialize(www.text) as IDictionary;

						if(www.text.Contains("error_msg"))
						{
							//{"success":0,"error_msg":"No cards available!"}

							//newMenuScene.instance.popupFromServer.ShowPopup ("You don't have any item request!");
							Debug.Log("text = "+(www.text));

						}
						else
						{

							Debug.Log("text = "+(www.text));
							IList friendList = (IList)cardDict ["data"];
							for (int i = 0; i < friendList.Count; i++) 
							{
								IDictionary friendListDic = (IDictionary)friendList[i];
								print("itemsList"+friendList.Count);

								GameObject item = (GameObject)Instantiate (Resources.Load ("stashItemCollect"));

								item.transform.SetParent (allContainer.transform);
								item.transform.localScale = Vector3.one;
								Debug.Log("item name"+friendListDic["item_name"]);



								if(friendListDic["item_name"].ToString() == "attack_potion")
								{
									string count;
									int counter=0;
									item.transform.GetChild(1).GetComponent<Text>().text= "Attack Potion";
									item.transform.GetChild(0).GetComponent<Image>().sprite= itemsResource[1];
									count=friendListDic["count"].ToString();
									item.transform.GetChild(2).GetComponent<Text>().text= "Total x"+count;
									if(friendListDic["count"] != null)
										int.TryParse (friendListDic["count"].ToString (), out counter);
									item.GetComponent<collectStash>()._myCount=counter;

								}
								else if(friendListDic["item_name"].ToString() == "stamina_potion")
								{
									string count;
									int counter=0;
									item.transform.GetChild(1).GetComponent<Text>().text= "Stamina Potion";
									item.transform.GetChild(0).GetComponent<Image>().sprite= itemsResource[2];
									count=friendListDic["count"].ToString();
									item.transform.GetChild(2).GetComponent<Text>().text= "Total x"+count;
									if(friendListDic["count"] != null)
										int.TryParse (friendListDic["count"].ToString (), out counter);
									item.GetComponent<collectStash>()._myCount=counter;
								}


								else if(friendListDic["item_name"].ToString() == "peace_treaty")
								{
									string count;
									int counter=0;
									item.transform.GetChild(1).GetComponent<Text>().text= "Peace Treaties";
									item.transform.GetChild(0).GetComponent<Image>().sprite= itemsResource[0];
									count=friendListDic["count"].ToString();
									item.transform.GetChild(2).GetComponent<Text>().text= "Total x"+count;
									if(friendListDic["count"] != null)
										int.TryParse (friendListDic["count"].ToString (), out counter);
									item.GetComponent<collectStash>()._myCount=counter;
								}
								else if(friendListDic["item_name"].ToString() == "signal_fire")
								{
									string count;
									int counter=0;
									item.transform.GetChild(1).GetComponent<Text>().text= "Signal Fire";
									item.transform.GetChild(0).GetComponent<Image>().sprite= itemsResource[0];
									count=friendListDic["count"].ToString();
									item.transform.GetChild(2).GetComponent<Text>().text= "Total x"+count;
									if(friendListDic["count"] != null)
										int.TryParse (friendListDic["count"].ToString (), out counter);
									item.GetComponent<collectStash>()._myCount=counter;
								}
								item.name=friendListDic["id"].ToString();
								peaceTreatyObject = item.gameObject;
								allList.Add (item.gameObject);
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
	void allcontents()
	{
		
		//deactivateContent (3);
		tempItem ();
		tempDragon ();

	}

	void deactivateContent(int index)
	{
		for(int i=0;i<EventContent.Length;i++)
		{
			if(i==index)
			{
				outlineClick[i].GetComponent<Button>().interactable=false;

				//outlineClick[i].GetComponent<Outline>().effectColor=new Color32(232,255,13,255);

				EventContent[i].SetActive(true);
			}
			else
			{
				outlineClick[i].GetComponent<Button>().interactable=true;

				//outlineClick[i].GetComponent<Outline>().effectColor=new Color32(0,0,0,255);

				EventContent[i].SetActive(false);

			}
		}
	}


	public void backButton()
	{
//		if(PlayerPrefs.GetString("chatScene")=="yes")
//		{
//			loadingScene.Instance.chat ();
//
//			//Application.LoadLevel("chatScene");
//			PlayerPrefs.SetString("chatScene","no");
//			
//			
//		}
//		if(PlayerPrefs.GetString("layout3")=="yes")
//		{
//			loadingScene.Instance.battle3 ();
//
//			//Application.LoadLevel("battle_Layout3");
//			PlayerPrefs.SetString("layout3","no");
//			
//		}
//		
//		if(PlayerPrefs.GetString("battleLyout")=="yes")
//		{
//			loadingScene.Instance.battle1 ();
//
//		//	Application.LoadLevel("Battle_Layout");
//			PlayerPrefs.SetString("battleLyout","no");
//			
//		}
//		if(PlayerPrefs.GetString("Battle_Layout4")=="yes")
//		{
//			loadingScene.Instance.battle4 ();
//
//			//Application.LoadLevel("Battle_Layout4");
//			PlayerPrefs.SetString("Battle_Layout4","no");
//			
//		}
//		if(PlayerPrefs.GetString("newMain")=="yes")
//		{
//			loadingScene.Instance.main ();
//
//			//Application.LoadLevel("menuNew");
//			PlayerPrefs.SetString("newMain","no");
//			
//		}
//		if(PlayerPrefs.GetString("cardCollection")=="yes")
//		{
//			loadingScene.Instance.cardCollecton ();
//
//			//Application.LoadLevel("cardCollections");
//			PlayerPrefs.SetString("cardCollection","no");
//			
//		}
//		if(PlayerPrefs.GetString("lost")=="yes") 
//		{
//			loadingScene.Instance.lost ();
//
//			//Application.LoadLevel("lost");
//			PlayerPrefs.SetString("lost","no");
//			
//		}
//		if(PlayerPrefs.GetString("win")=="yes") 
//		{
//			loadingScene.Instance.win ();
//
//			//Application.LoadLevel("win");
//			PlayerPrefs.SetString("win","no");
//			
//		}
//		if(PlayerPrefs.GetString("detail")=="yes") 
//		{
//			loadingScene.Instance.detail ();
		//cardS
//			//Application.LoadLevel("detail");
//			PlayerPrefs.SetString("detail","no");
//			
//		}
//		if(PlayerPrefs.GetString("shopScene")=="yes") 
//		{
//			loadingScene.Instance.shop ();
//
//			//Application.LoadLevel("shopScene");
//			PlayerPrefs.SetString("shopScene","no");
//			
//		}
//		if(PlayerPrefs.GetString("quest")=="yes") 
//		{
//			loadingScene.Instance.quest ();
//
//			//Application.LoadLevel("quest");
//			PlayerPrefs.SetString("quest","no");
//			
//		}
//		if(PlayerPrefs.GetString("empireScene")=="yes") 
//		{
//			loadingScene.Instance.empire ();
//
//			//Application.LoadLevel("empireScene");
//			PlayerPrefs.SetString("empireScene","no");
//			
//		}
//		if( PlayerPrefs.GetString("trade")=="yes")
//		{
//			loadingScene.Instance.trade ();
//
//			//Application.LoadLevel("trade");
//			PlayerPrefs.SetString("trade","no");
//			
//		}
//		if(PlayerPrefs.GetString("community")=="yes")
//		{
//			loadingScene.Instance.community ();
//
//			//Application.LoadLevel("community");
//			PlayerPrefs.SetString("community","no");
//			
//			
//		}
		deactivateContent(0);

		for(int i=0;i<loadingScene.Instance.scenes.Count;i++)
		{
			if(i==loadingScene.Instance.scenes.Count-1)
			{
				loadingScene.Instance.scenes[i].SetActive(false);
				//loadingScene.Instance.scenes.Clear();

				loadingScene.Instance.scenes.RemoveAt(loadingScene.Instance.scenes.Count-1);
			}
			else
			{
				loadingScene.Instance.scenes[loadingScene.Instance.scenes.Count-2].SetActive(true);
				//loadingScene.Instance.scenes[i].SetActive(true);
				print("======SCENE NAME======"+loadingScene.Instance.scenes[i]);
				
				
			}
			
		}
	}


}
