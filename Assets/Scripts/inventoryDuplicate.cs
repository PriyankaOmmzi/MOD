using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using MiniJSON;
public class inventoryDuplicate : MonoBehaviour 
{
	public static inventoryDuplicate instance;
	public Sprite[] searchPlayerProfile;
	public GameObject setting;
	public GameObject menuScreen;
	public GameObject chatBtn;
	bool isMneuActive=false;
	public Sprite[] itemsResource;
	public Button[] bottomsButtons;

	public GameObject itemContainer;
	public GameObject giftContainer;

	public List<GameObject> itemList;
	public List<GameObject> giftList;
	GameObject peaceTreatyObject;
	GameObject attackPotionObject;
	GameObject staminaPotionObject;
	GameObject signalFireObject;
	public Button[] itemGifts;
	public GameObject[] itemGiftsContents;
	string requestPlyerName;
	string requestPlayerGuildName;
	int requestPlayerLogin;
	int requestPlayerLevel;
	int requestAvatarNo;
	public List<GameObject> requestPlayer;
	//	public List <int> searchedPlayerIdList;
	public int requestPlayerId;
	public int giftId;

	// Use this for initialization
	public void Awake()
	{
		instance = this;
	}
	void Start ()
	{
		loadingScene.Instance.playerProfilePanel.SetActive(false);
		setting.SetActive(false);
		itemGifts [0].interactable = false;
		itemGifts [1].interactable = true;

	}

	public void clickGiftItems(Button button )

	{
		if (button.name == "itemButton") {
			
			Items ();
			
		} else if (button.name == "giftButton") {
			

			Gifts ();
		}

		
	}



	public void usePeaceTreaties(Button button)
	{
		if (PlayerParameters._instance.myPlayerParameter.no_of_peace_treaties >=1) 
		{
			if (PlayerParameters._instance.myPlayerParameter.peace_treaty >= 1) 
			{
				newMenuScene.instance.popupFromServer.ShowPopup ("Peace Treaty is already activated!");
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
							PlayerParameters._instance.myPlayerParameter.peace_treaty_start_time = TimeManager._instance.GetCurrentServerTime();
							newMenuScene.instance.popupFromServer.ShowPopup ("Peace Treaty activated for an hour!");
							peaceTreatyObject.transform.GetChild(2).GetComponent<Text>().text= "Total x"+PlayerParameters._instance.myPlayerParameter.no_of_peace_treaties.ToString();
							PlayerParameters._instance.ShowPeaceTreatyBird(3600f);
						}
						else
						{
							
							PlayerParameters._instance.myPlayerParameter.peace_treaty = 0;
							PlayerParameters._instance.myPlayerParameter.peace_treaty_active_time = 0;
							PlayerParameters._instance.myPlayerParameter.no_of_peace_treaties++;
							newMenuScene.instance.popupFromServer.ShowPopup ("You have not enough Peace treaties!");
						}
					}));
			}


		}
		else
		{
			newMenuScene.instance.popupFromServer.ShowPopup ("You do not have enough Peace treaties!");
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
						newMenuScene.instance.popupFromServer.ShowPopup ("Orbs Replenished!");

						attackPotionObject.transform.GetChild(2).GetComponent<Text>().text= "Total x"+PlayerParameters._instance.myPlayerParameter.attack_potion.ToString();

					}
					else
					{
						newMenuScene.instance.popupFromServer.ShowPopup ("Could not use attack potion at this time!");
					}
				}));
		}
		else
		{
			newMenuScene.instance.popupFromServer.ShowPopup ("You do not have any attack potion!");
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

					if(isSuccess)
					{
						Debug.Log ("StaminaPotionClick3" );

						PlayerParameters._instance.myPlayerParameter.orb = PlayerParameters._instance.myPlayerParameter.maxOrb;
						PlayerParameters._instance.myPlayerParameter.orb_time = TimeManager._instance.GetCurrentServerTime();
						if(PlayerParameters._instance.myPlayerParameter.stamina_potion > 0)
							PlayerParameters._instance.myPlayerParameter.stamina_potion--;
						//else
						newMenuScene.instance.popupFromServer.ShowPopup ("Stamina Replenished!");
						//						PlayerParameters._instance.myPlayerParameter.unlocked_attack_potion--;
						staminaPotionObject.transform.GetChild(2).GetComponent<Text>().text= "Total x"+PlayerParameters._instance.myPlayerParameter.stamina_potion.ToString();

					}
					else
					{
						newMenuScene.instance.popupFromServer.ShowPopup ("Could not use stamina potion at this time!");
					}
				}));
		}
		else
		{
			newMenuScene.instance.popupFromServer.ShowPopup ("You do not have any stamina potion!");
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
							newMenuScene.instance.popupFromServer.ShowPopup ("Army Replenished Successfully!");
						}
					}));
				} else {
					newMenuScene.instance.popupFromServer.ShowPopup ("Network Error!");
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
		Items ();
		itemGifts [0].interactable = false;
		itemGifts [1].interactable = true;
		ResetMenu ();
	}
	void OnDisable()
	{
		for(int i = giftList.Count - 1; i > -1; i--)
		{
			//if (itemList[i] == null)
			Destroy (giftList[i]);
			giftList.RemoveAt(i);
		}
		for(int j = itemList.Count - 1; j > -1; j--)
		{
			//if (itemList[i] == null)
			Destroy (itemList[j]);
			itemList.RemoveAt(j);
		}

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

		ResetMenu ();
		clearedSearched ();
		loadingScene.Instance.objectToOpenMenu = this.gameObject;
		loadingScene.Instance.main ();
		//Application.LoadLevel("menuNew");
	}
	public void community()
	{

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
	public void Items()
	{

		for(int i = giftList.Count - 1; i > -1; i--)
		{
			//if (itemList[i] == null)
			Destroy (giftList[i]);
			giftList.RemoveAt(i);
		}


		GameObject item = (GameObject)Instantiate (Resources.Load ("stashItem"));
		item.transform.SetParent (itemContainer.transform);
		item.transform.localScale = Vector3.one;
		item.transform.GetChild(0).GetComponent<Image>().sprite= itemsResource[0];
		item.transform.GetChild(1).GetComponent<Text>().text= "Peace Treaty";
		item.transform.GetChild(2).GetComponent<Text>().text= "Total x"+PlayerParameters._instance.myPlayerParameter.no_of_peace_treaties.ToString();
		item.transform.GetChild(3).transform.GetChild(0).GetComponent<Text>().text="Use";
		item.transform.GetChild(3).GetComponent<Button>().onClick.AddListener (() => {usePeaceTreaties(item.GetComponent<Button>());});
		peaceTreatyObject = item.gameObject;

		GameObject item2 = (GameObject)Instantiate (Resources.Load ("stashItem"));
		item2.transform.SetParent (itemContainer.transform);
		item2.transform.localScale = Vector3.one;
		item2.transform.GetChild(0).GetComponent<Image>().sprite= itemsResource[1];
		item2.transform.GetChild(1).GetComponent<Text>().text= "Attack Potion";
		item2.transform.GetChild(2).GetComponent<Text>().text= "Total x"+PlayerParameters._instance.myPlayerParameter.attack_potion.ToString();
		item2.transform.GetChild(3).transform.GetChild(0).GetComponent<Text>().text="Use";

		item2.transform.GetChild(3).GetComponent<Button>().onClick.AddListener (() => {useAttackPotion(item2.GetComponent<Button>());});
		attackPotionObject = item2.gameObject;

		GameObject item3 = (GameObject)Instantiate (Resources.Load ("stashItem"));
		item3.transform.SetParent (itemContainer.transform);
		item3.transform.localScale = Vector3.one;
		item3.transform.GetChild(0).GetComponent<Image>().sprite= itemsResource[2];

		item3.transform.GetChild(1).GetComponent<Text>().text= "Stamina Potion";
		item3.transform.GetChild(2).GetComponent<Text>().text= "Total x"+PlayerParameters._instance.myPlayerParameter.stamina_potion.ToString();
		item3.transform.GetChild(3).transform.GetChild(0).GetComponent<Text>().text="Use";

		item3.transform.GetChild(3).GetComponent<Button>().onClick.AddListener (() => {useStaminaPotion(item3.GetComponent<Button>());});
		staminaPotionObject = item3.gameObject;

		GameObject item4 = (GameObject)Instantiate (Resources.Load ("stashItem"));
		item4.transform.SetParent (itemContainer.transform);
		item4.transform.localScale = Vector3.one;
		item4.transform.GetChild(0).GetComponent<Image>().sprite= itemsResource[3];

		item4.transform.GetChild(1).GetComponent<Text>().text= "Signal Fire";
		item4.transform.GetChild(2).GetComponent<Text>().text= "Total x"+PlayerParameters._instance.myPlayerParameter.signal_fire.ToString();
		item4.transform.GetChild(3).transform.GetChild(0).GetComponent<Text>().text="Use";
		item4.transform.GetChild(3).GetComponent<Button>().onClick.AddListener (() => {ReplenishArmy(item4.GetComponent<Button>());});
		signalFireObject = item4.gameObject;

		itemList.Add (item.gameObject);
		itemList.Add (item2.gameObject);
		itemList.Add (item3.gameObject);
		itemList.Add (item4.gameObject);
		itemGiftsContents [0].SetActive (true);
		itemGiftsContents [1].SetActive (false);
		itemGifts [0].interactable = false;
		itemGifts [1].interactable = true;
		
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

	public void Gifts()
	{
		for(int i = itemList.Count - 1; i > -1; i--)
		{
			//if (itemList[i] == null)
				Destroy (itemList[i]);
				itemList.RemoveAt(i);
		}



		newMenuScene.instance.loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected) {
				WWWForm form_time = new WWWForm ();
				string URltime = loadingScene.Instance.baseUrl;
				form_time.AddField ("tag", "doGetUserGift");
				form_time.AddField ("user_id",   PlayerDataParse._instance.playersParam.userId.ToString().Split (',') [0]); 
				//form_time.AddField ("friend_type", "friend");  
				//form_time.AddField ("status", "0");
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
								newMenuScene.instance.popupFromServer.ShowPopup ("You don't have any gift request!");
								itemGiftsContents [0].SetActive (false);
								itemGiftsContents [1].SetActive (true);
								itemGifts [0].interactable = true;
								itemGifts [1].interactable = false;
								//deactivateContents (2);
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
								if(friendListDic["Gift_id"] != null)
									int.TryParse (friendListDic["Gift_id"].ToString (), out giftId);
								requestPlyerName = (friendListDic ["userName"].ToString());
								//								if(friendListDic["userName"] != null)
								//									int.TryParse (friendListDic["userName"].ToString (), out requestPlyerName);
								GameObject newCard1 = (GameObject)Instantiate (Resources.Load ("acceptGift"));
								newCard1.transform.SetParent (giftContainer.transform);
								newCard1.transform.localScale = Vector3.one;
								newCard1.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text=requestPlyerName.ToString();
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

								newCard1.name=giftId.ToString();
								giftList.Add (newCard1.gameObject);
								//								searchedPlayerIdList.Add (searchedPlayerId);
								//deactivateContents (2);								
								itemGiftsContents [0].SetActive (false);
								itemGiftsContents [1].SetActive (true);
								itemGifts [0].interactable = true;
								itemGifts [1].interactable = false;
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


	void clearedSearched()
	{
		for (int i = itemList.Count-1; i >= 0; i--) {
			if (itemList [i] != null)
			{
				Destroy (itemList [i]);
				itemList.RemoveAt (i);

			}
		}
	}


	public void inventory()
	{

		PlayerPrefs.SetString("newMain","yes");
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


	public void backButton()
	{
		clearedSearched ();
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
				print("======SCENE NAME======"+loadingScene.Instance.scenes[i]);


			}

		}
	}


}
