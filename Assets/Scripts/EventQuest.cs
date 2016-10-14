using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using MiniJSON;

using Random=UnityEngine.Random;
using System.Linq;


public class EventQuest : MonoBehaviour 
{
	bool getReadyForBossBattle;
	int hotSpotCounter;
	Button hotSpotButton;
	bool isMneuActive=false;
	int randowmFunction=0;
	int hotspotPage;
	int maxHotspotPage;

	public static EventQuest instance;
	public GameObject setting;

	public Sprite []randomHotSpot;
	public Sprite defaultHotSpotSprite;
	public Text nameOfAreaInHotspotPage;
	public Slider areaPercentageSlider;
	public Image defaultHotSpot;

	public Sprite ambushImage;
	public Sprite clearImages;
	public Button greenYes;
	public Text areaPercentageText;
	public GameObject areaClearImage;
	public GameObject [] hotspots;
	public GameObject nextButtonForHotSpotScroll;
	public GameObject prevButtonForHotSpotScroll;
	public GameObject warningHotSpot;

	public Button[] bottomsButtons;
	public GameObject hotSpotClear;
	public Text reward1;
	public Text reward2;
	public GameObject threeRewardsObject;
	public Text threeRewardsObjectReward1;
	public Text threeRewardsObjectReward2;
	public Text threeRewardsObjectReward3;
	public Image rewardImage;
	public Text expRewardOnly;
	public Text hotspotNameClear;
	public Text hotspotNameFailed;
	public GameObject hotSpotFailed;
	public GameObject warningClear;
	public GameObject greenWarning;
	public GameObject warningAmbush;
	public GameObject warningText;
	public GameObject hotSpotDescription;
	public Text hotspotNameInDescription;
	public Text hotspotDescriptionStaminaReqd;
	public Text hotspotDescriptionText;
	public GameObject numberText;  
	public GameObject warning;
	public Text warningAreaExplored;
	public GameObject hotspot;
	public GameObject timerMap;
	public GameObject areaSelection;
	public GameObject chaptersobj;
	public GameObject areaPanel;
	public GameObject menu;
	public GameObject chatBtn;
	public int clickcounter=0;


	public Text gemsText;
	public Text wheatText;
	public Text goldText;
	public Text staminaText;
	public Text avatarLevelText;
	public Text avatarPercentageText;
	public Slider avatarPercentage;
	public Text staminaPotion;
	public Text attackPotion;
	public List<Image> attackingOrbs = new List<Image>();
	public Sprite activatedOrb , deactivedOrb;


	public int noOfHotspotsInArea;
	public int hotSpotClicked;

	[Serializable]
	public struct AreaParameters
	{
		public Image itemSetIcon;
		public Image reward_1;
		public Image reward_2;
		public Image reward_3;
		public Text hotspotNo;
		public Text areaClearedPercentage;
		public Slider areaClearedValue;
	}
	public List<AreaParameters> areaList = new List<AreaParameters>();

	[Serializable]
	public struct HotspotButtons
	{
		public Text staminaReqd;
		public Image hotspotImage;
	}
	public List<HotspotButtons> hotspotButtons = new List<HotspotButtons>();
	
	public int areaNo;
	public int chapterNo;
	public Sprite wheatReward;
	public Sprite goldReward;
	public Sprite attackPotionReward;
	public Sprite staminaPotionReward;
	public Popup popupFromServer;
	public GameObject areaAlreadyClearedPopup;
	public GameObject areaClearedWithoutRewards;
	public GameObject areaClearedWithRewards;
	public GameObject baseRewards , specialRewards;
	public List<Text> baseRewardsText = new List<Text>(); 
	public List<Text> specialRewardsText = new List<Text>(); 
	public Slider soldiersInDecksSlider;
	public Text soldiersNoInDeckText;

	public int noOfOrbsDeduct;

	public GameObject popupToStartBossBattle;
	public battleFormation myQuestFormation;

	public QuestManager myQuestManager;

	Dictionary<string, string> chestData = new Dictionary<string, string>();
	// Use this for initialization
	void Start ()
	{
		Debug.Log (SystemInfo.deviceUniqueIdentifier);
		hotSpotCounter=0;
		clickcounter=0;

		PlayerPrefs.SetString("redirect","no");
		setting.SetActive(false);

		loadingScene.Instance.playerProfilePanel.SetActive(false);
		//(hotspot);
		warningHotSpot.SetActive(false);
		hotSpotClear.SetActive(false);
		hotSpotFailed.SetActive(false);
		warningAmbush.SetActive(false);
		warningClear.SetActive(false);
		greenWarning.SetActive(false);
		warningText.SetActive(false);
		hotSpotDescription.SetActive(false);
		warning.SetActive(false);
		hotspot.SetActive(false);
		areaPanel.SetActive(false);
		areaSelection.SetActive(false);
		timerMap.SetActive(true);
		chaptersobj.SetActive(true);

	}


	public void DisplayFinalText()
	{
		gemsText.text = PlayerParameters._instance.myPlayerParameter.gems.ToString ();
		wheatText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
		goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
		staminaText.text = PlayerParameters._instance.myPlayerParameter.stamina.ToString ()+"/"+ PlayerParameters._instance.myPlayerParameter.max_stamina;
		staminaPotion.text = PlayerParameters._instance.myPlayerParameter.stamina_potion.ToString ();;
		attackPotion.text = PlayerParameters._instance.myPlayerParameter.attack_potion.ToString ();
		avatarLevelText.text = "Lvl" + (PlayerParameters._instance.myPlayerParameter.avatar_level+1);

		Int64 reqdAvtarExpForLevelUp = 0;
		if (PlayerParameters._instance.myPlayerParameter.avatar_level < PlayerParameters._instance.avatarReqdExpForLevelUp.Length) {
			reqdAvtarExpForLevelUp = PlayerParameters._instance.avatarReqdExpForLevelUp [PlayerParameters._instance.myPlayerParameter.avatar_level];
		} else {
			reqdAvtarExpForLevelUp = PlayerParameters._instance.avatarReqdExpForLevelUp [PlayerParameters._instance.avatarReqdExpForLevelUp.Length-1];
			
		}
		double percentValForAvatar = PlayerParameters._instance.myPlayerParameter.avatar_exp / (double)reqdAvtarExpForLevelUp;
//		Debug.Log ("percentValForAvatar = "+percentValForAvatar);
		avatarPercentage.value = (float)percentValForAvatar;
		avatarPercentageText.text = Mathf.FloorToInt(avatarPercentage.value*100)+"%";

		for (int i = 0; i < attackingOrbs.Count; i++) {
			if( i < PlayerParameters._instance.myPlayerParameter.orb)
			{
				attackingOrbs[i].sprite = activatedOrb;
			}
			else
			{
				attackingOrbs[i].sprite = deactivedOrb;
			}
		}
		myQuestFormation.TotalSoldiersInDeck();
		int cardDeck = (PlayerParameters._instance.myPlayerParameter.questFormationDeck != 0) ? (PlayerParameters._instance.myPlayerParameter.questFormationDeck-1) : (PlayerParameters._instance.myPlayerParameter.battleFormationDeck-1);
		if(cardDeck < 0)
			cardDeck = myQuestFormation.FindCardDeck();
		soldiersNoInDeckText.text = myQuestFormation.cardDecks[cardDeck].deckSoldiers +"/"+myQuestFormation.cardDecks[cardDeck].deckLeadership; ;
		float soldiersPercentage = myQuestFormation.cardDecks[cardDeck].deckSoldiers/(float) myQuestFormation.cardDecks[cardDeck].deckLeadership;
		soldiersInDecksSlider.value = soldiersPercentage;
	}

	void Update()
	{
		staminaText.text = PlayerParameters._instance.myPlayerParameter.stamina.ToString ();
		for (int i = 0; i < attackingOrbs.Count; i++) {
			if( i < PlayerParameters._instance.myPlayerParameter.orb)
			{
				attackingOrbs[i].sprite = activatedOrb;
			}
			else
			{
				attackingOrbs[i].sprite = deactivedOrb;
			}
		}
	}
	void OnEnable()
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
			menu.SetActive(false);
			isMneuActive=false;
			
		}
		popupToStartBossBattle.SetActive (false);
		areaClearedWithRewards.SetActive (false);
		areaClearedWithoutRewards.SetActive (false);
		DisplayFinalText ();
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




	void sameStart()
	{
		hotSpotCounter=0;
		clickcounter=0;
		setting.SetActive(false);
		loadingScene.Instance.playerProfilePanel.SetActive(false);
		warningHotSpot.SetActive(false);
		hotSpotClear.SetActive(false);
		hotSpotFailed.SetActive(false);
		warningAmbush.SetActive(false);
		warningClear.SetActive(false);
		greenWarning.SetActive(false);
		warningText.SetActive(false);
		hotSpotDescription.SetActive(false);
		warning.SetActive(false);
		hotspot.SetActive(false);
		areaPanel.SetActive(false);
		areaSelection.SetActive(false);
		timerMap.SetActive(true);
		chaptersobj.SetActive(true);
	}

	public void Awake()
	{
		instance=this;
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
		}
	}
	public void logOut()
	{
		sameStart();
		onClickSettingExit();
		PlayerPrefs.SetString("logout","yes");
		loadingScene.Instance.main();
	}
	public void notificationOnOff()
	{
		loadingScene.Instance.notificationOnOff();
	}
	public void soundOnOff()
	{
		
		loadingScene.Instance.soundOnOff ();
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
		menu.SetActive(false);
		isMneuActive=false;
		setting.SetActive(true);
	}
	public void onClickSettingExit()
	{
		setting.SetActive(false);
	}
	public void onClickProfile()
	{
		loadingScene.Instance.playerProfilePanel.SetActive(true);
	}
	public void onClickProfileExit()
	{
		loadingScene.Instance.playerProfilePanel.SetActive(false);
	}

	public void ReplenishArmy()
	{
		if (myQuestFormation.cardDecks [0].noOfCardsSelected == 0 || myQuestFormation.cardDecks [1].noOfCardsSelected == 0 || myQuestFormation.cardDecks [2].noOfCardsSelected == 0) {
//		if(PlayerParameters._instance.myPlayerParameter.questFormationDeck == 0){
			popupFromServer.ShowPopup ("Battle Formation of quest has not been set up!");
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
					for (int k = 0; k < myQuestFormation.cardDecks.Count; k++) {
						for (int i = 0; i < myQuestFormation.cardDecks [k].cardRows.Count; i++) {
							for (int j = 0; j < myQuestFormation.cardDecks [k].cardRows[i].cardIdsForRow.Count; j++) {
								int cardNoInMyCards = CardsManager._instance.PositionOfCardInList (myQuestFormation.cardDecks [k].cardRows [i].cardIdsForRow [j]);
								if(!cardIds.Contains (myQuestFormation.cardDecks [k].cardRows [i].cardIdsForRow [j]+","))
								{
									cardIds+=myQuestFormation.cardDecks [k].cardRows [i].cardIdsForRow [j]+",";
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
							popupFromServer.ShowPopup ("Could not Replenish Army !");
							PlayerParameters._instance.myPlayerParameter.signal_fire--;
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
							popupFromServer.ShowPopup ("Army Replenished Successfully !");
							DisplayFinalText();
						}
					}));
				} else {
					popupFromServer.ShowPopup ("Network Error !");
				}
			}
		});
		
	}

	bool forgingClicked;
	public void forgeGreenButton()
	{
		List <int> hotspotsCleared = new List <int>();
		int reqdStamina = 0;
		bool artefactHotSpotCameOnce = false;
		for (int i = 0; i< hotspotButtons.Count; i++) {
			if((hotspotPage*5 + i) < noOfHotspotsInArea)
			{
				if(!myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].isHotSpotCleared[hotspotPage*5 + i])
				{
					int hotspotId = myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].hotSpotNoUsed[hotspotPage*5 + i];
					if(hotspotId != 3)
					{
						bool allowAddition = true;
						if (hotspotId == 10 && artefactHotSpotCameOnce) {
							allowAddition = false;
						}
						if (allowAddition) {
							reqdStamina += QuestDataFetch._instance.hotspotTypes [hotspotId].staminaReqd;
							hotspotsCleared.Add ((hotspotPage * 5 + i));
						}
						if (hotspotId == 10 && !artefactHotSpotCameOnce) {
							artefactHotSpotCameOnce = true;
						}
					}
				}

			}
		}
		if (PlayerParameters._instance.myPlayerParameter.stamina >= reqdStamina && hotspotsCleared.Count > 0) {
			forgingClicked = true;
			loadingScene.Instance.loader.SetActive (true);
			warningHotSpot.SetActive (false);
			foodForReward = 0;
			goldForReward = 0;
			avatarExpForReward = 0;

			staminaToSend = PlayerParameters._instance.myPlayerParameter.stamina - reqdStamina;
			int amtCleared = myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].amountCleared;

			for(int i = hotspotsCleared.Count-1; i >= 0 ; i--)
			{
				randowmFunction=Random.Range(0,4);
				if(randowmFunction==0)     //not cleared
				{
					hotspotsCleared.RemoveAt (i);
				}

			}
			for(int i = 0; i < hotspotsCleared.Count ; i++)
			{
				int hotSpotId = myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].hotSpotNoUsed [hotspotsCleared[i]];
				amtCleared+= QuestDataFetch._instance.hotspotTypes [hotSpotId].clearancePointsAwarded;
			}
			int noOfTimesAreaWasCleared = myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].noOfTimesAreaWasCleared;
		
			Dictionary<string, string> playerParamsDict = new Dictionary<string, string> (); 
			int noOfCaptives = 0;
			for(int k = 0; k < hotspotsCleared.Count ; k++)
			{
				Debug.Log("hotspot ="  +hotspotsCleared[k]);
				int hotSpotId = myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].hotSpotNoUsed [hotspotsCleared[k]];
				Debug.Log("hotSpotId ="  +hotSpotId);
				if (hotSpotId == 4) {
					noOfCaptives++;
				} else {
					Dictionary<string, string> playerParamsDictNew = RewardsToSend (hotSpotId);
					for (int i = 0; i < playerParamsDictNew.Count; i++) {
						if(!playerParamsDict.ContainsKey (playerParamsDictNew.Keys.ElementAt (i)))
							playerParamsDict.Add (playerParamsDictNew.Keys.ElementAt (i), playerParamsDictNew.Values.ElementAt (i));
					}

				}
			}

			if(noOfCaptives > 0 && CardsManager._instance.myCaptives.Count < EmpireManager._instance.prison.finalValue1[EmpireManager._instance.prison.currentLevel])
			{
				int maxPossibleCaptives  = EmpireManager._instance.prison.finalValue1[EmpireManager._instance.prison.currentLevel];
				int totalCaptives = noOfCaptives + CardsManager._instance.myCaptives.Count;
				if( totalCaptives > maxPossibleCaptives)
				{
					noOfCaptives = maxPossibleCaptives - CardsManager._instance.myCaptives.Count;
				}
				NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
					if (isConnected) {
						StartCoroutine (GetCaptive (noOfCaptives , (isSuccess , msgString) => {
							if (isSuccess) {
								RewardsData (1, playerParamsDict, amtCleared, noOfTimesAreaWasCleared, hotspotsCleared);
								Debug.Log("it was successful...");
							} else {
								popupFromServer.ShowPopup ("Network Error! Cannot fetch data!");

							}
						}));
					} else {
						popupFromServer.ShowPopup ("Network Error! Cannot fetch data!");
					}
				});
			}
			else
			{
				RewardsData (1, playerParamsDict, amtCleared, noOfTimesAreaWasCleared, hotspotsCleared);
			}


		} else{
			if(hotspotsCleared.Count <= 0)
				popupFromServer.ShowPopup ("Cannot Forge Ahead!");
			else
				popupFromServer.ShowPopup ("Not Enough Stamina!");
		}

	}


	public void forgeClick()
	{
		noOfOrbsDeduct = 0;
		BattleLogic._instance.orbsTosubtract = noOfOrbsDeduct;
		int reqdStamina = 0;
		List <int> hotspotsCleared = new List <int>();
		for (int i = 0; i< hotspotButtons.Count; i++) {
			if((hotspotPage*5 + i) < noOfHotspotsInArea)
			{
				if(!myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].isHotSpotCleared[hotspotPage*5 + i])
				{
					int hotspotId = myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].hotSpotNoUsed[hotspotPage*5 + i];
					if(hotspotId != 3)
					{
						reqdStamina+=QuestDataFetch._instance.hotspotTypes[hotspotId].staminaReqd;
						hotspotsCleared.Add ((hotspotPage*5 + i));
					}
				}
				
			}
		}
		if (PlayerParameters._instance.myPlayerParameter.stamina >= reqdStamina && hotspotsCleared.Count > 0)
			warningHotSpot.SetActive(true);
		else{
			if(hotspotsCleared.Count <= 0)
				popupFromServer.ShowPopup ("Cannot Forge Ahead!");
			else
				popupFromServer.ShowPopup ("Not Enough Stamina!");
		}

	}

	public void exitWarningHotSpot()
	{
		warningHotSpot.SetActive(false);
		areaAlreadyClearedPopup.SetActive (false);
	}

	void SelectEnemyCards()
	{
		BattleLogic._instance.EnemyResourcesLoot (PlayerParameters._instance.myPlayerParameter.avatar_level , PlayerParameters._instance.myPlayerParameter.wheat , PlayerParameters._instance.myPlayerParameter.gold);
		loadingScene.Instance.battleInstance.fromQuest = true;
		List<string> allEnemyCards = new List<string>();
		allEnemyCards = CardsData._instance.LoadCardsData ("CardRarity1", EnemyAI._instance.encounterAmbushAI [chapterNo - 1].areaAI [areaNo - 1].cardOfRarity[0] ,ref allEnemyCards);
		allEnemyCards = CardsData._instance.LoadCardsData ("CardRarity2", EnemyAI._instance.encounterAmbushAI [chapterNo - 1].areaAI [areaNo - 1].cardOfRarity[1],ref allEnemyCards);
		allEnemyCards = CardsData._instance.LoadCardsData ("CardRarity3", EnemyAI._instance.encounterAmbushAI [chapterNo - 1].areaAI [areaNo - 1].cardOfRarity[2],ref allEnemyCards);
		allEnemyCards = CardsData._instance.LoadCardsData ("CardRarity4", EnemyAI._instance.encounterAmbushAI [chapterNo - 1].areaAI [areaNo - 1].cardOfRarity[3],ref allEnemyCards);
		allEnemyCards = CardsData._instance.LoadCardsData ("CardRarity5", EnemyAI._instance.encounterAmbushAI [chapterNo - 1].areaAI [areaNo - 1].cardOfRarity[4],ref allEnemyCards);
		int enemyCardNo = 0;
		for (int i = 0; i < allEnemyCards.Count; i++) {
//			Debug.Log ("Enemy Card Name = "+allEnemyCards[i]);
			if(enemyCardNo<3)
				loadingScene.Instance.battleInstance.enemyCards[0].cardEntity[enemyCardNo].GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("images/"+allEnemyCards[i]);
			else if(enemyCardNo<6)
				loadingScene.Instance.battleInstance.enemyCards[1].cardEntity[enemyCardNo-3].GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("images/"+allEnemyCards[i]);
			else
				loadingScene.Instance.battleInstance.enemyCards[2].cardEntity[enemyCardNo-6].GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("images/"+allEnemyCards[i]);
			enemyCardNo++;
		}

		for (int i = 3; i < 5; i++) {
			Destroy (loadingScene.Instance.battleInstance.enemyCards[0].cardEntity[i]);
			Destroy(loadingScene.Instance.battleInstance.enemyHealth[0].cardEntity[i].transform.parent.gameObject);
			Destroy (loadingScene.Instance.battleInstance.enemyCards[1].cardEntity[i]);
			Destroy(loadingScene.Instance.battleInstance.enemyHealth[1].cardEntity[i].transform.parent.gameObject);
			Destroy (loadingScene.Instance.battleInstance.enemyCards[2].cardEntity[i]);
			Destroy(loadingScene.Instance.battleInstance.enemyHealth[2].cardEntity[i].transform.parent.gameObject);
		}
	}



	void SetBossStats()
	{
		BattleLogic._instance.battleType = BattleLogic.BattleType.CHEST_BOSS;
		int enemyCardNo = 0;
		int checkingForRarityNo = 0;
		int noOfTimesAreaWasCleared = myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].noOfTimesAreaWasCleared;
		loadingScene.Instance.battleInstance.enemyDeck.gateLevel = EmpireManager._instance.gate.currentLevel;
		loadingScene.Instance.battleInstance.enemyDeck.avatarDefense = PlayerParameters._instance.myPlayerParameter.avatar_attack;
		loadingScene.Instance.battleInstance.enemyDeck.avatarStamina = PlayerParameters._instance.myPlayerParameter.stamina;
		loadingScene.Instance.battleInstance.enemyDeck.avatarNo = PlayerParameters._instance.myPlayerParameter.avatar_no;
		if (loadingScene.Instance.battleInstance.enemyDeck.avatarNo > 2)
			loadingScene.Instance.battleInstance.enemyDeck.avatarNo = 0;
		for (int i = 0; i < 3; i++) {

			for(int j = 0 ; j < 3 ; j++)
			{
				loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsAttack.Add (Mathf.CeilToInt(EnemyAI._instance.bossAI[chapterNo-1].chapterAI[areaNo-1].areaAI[noOfTimesAreaWasCleared].bossStats/9));
				loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsDefense.Add (Mathf.CeilToInt(EnemyAI._instance.bossAI[chapterNo-1].chapterAI[areaNo-1].areaAI[noOfTimesAreaWasCleared].bossStats/9));
				loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsLeadership.Add (Mathf.CeilToInt(EnemyAI._instance.bossAI[chapterNo-1].chapterAI[areaNo-1].areaAI[noOfTimesAreaWasCleared].bossHealth/9));
				loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsSoldiers.Add (Mathf.CeilToInt(EnemyAI._instance.bossAI[chapterNo-1].chapterAI[areaNo-1].areaAI[noOfTimesAreaWasCleared].bossHealth/9));
			}
			
		}
	}


	void SetEncounterEnemyStats()
	{
		BattleLogic._instance.battleType = BattleLogic.BattleType.CHEST_EVENT;
		int enemyCardNo = 0;
		int checkingForRarityNo = 0;
		int totalEnemySoldiers = 0;
		loadingScene.Instance.battleInstance.enemyDeck.gateLevel = EmpireManager._instance.gate.currentLevel-1;
		loadingScene.Instance.battleInstance.enemyDeck.avatarDefense = PlayerParameters._instance.myPlayerParameter.avatar_attack;
		loadingScene.Instance.battleInstance.enemyDeck.avatarStamina = PlayerParameters._instance.myPlayerParameter.stamina;
		loadingScene.Instance.battleInstance.enemyDeck.avatarNo = PlayerParameters._instance.myPlayerParameter.avatar_no;
		if (loadingScene.Instance.battleInstance.enemyDeck.avatarNo > 2)
			loadingScene.Instance.battleInstance.enemyDeck.avatarNo = 0;
		for (int i = 0; i < 3; i++) {
			for(int j = 0 ; j < 3 ; j++)
			{
				enemyCardNo++;
				if(enemyCardNo > EnemyAI._instance.encounterAmbushAI [chapterNo - 1].areaAI [areaNo - 1].cardOfRarity[checkingForRarityNo])
				{
					checkingForRarityNo++;
					enemyCardNo = 0;
				}
				loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsAttack.Add (EnemyAI._instance.baseAttack[checkingForRarityNo]);
				loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsDefense.Add (EnemyAI._instance.baseDefense[checkingForRarityNo]);
				loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsLeadership.Add (EnemyAI._instance.baseLeadership[checkingForRarityNo]);
				loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsSoldiers.Add (EnemyAI._instance.baseLeadership[checkingForRarityNo]);
				totalEnemySoldiers+=EnemyAI._instance.baseLeadership[checkingForRarityNo];
			}

		}
	}

	public void pressedEnter()
	{
		forgingClicked = false;
		staminaToSend = PlayerParameters._instance.myPlayerParameter.stamina - staminaToDeduct;
		staminaText.text = PlayerParameters._instance.myPlayerParameter.stamina.ToString ();
		GameObject.Find("enterButton").GetComponent<Button>().interactable=false;
		int hotSpotId = myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].hotSpotNoUsed[(hotspotPage*5) + (hotSpotClicked-1)];
		Debug.Log ("hotSpotId = " + hotSpotId);
		BattleLogic._instance.battleType = BattleLogic.BattleType.CHEST_EVENT;
		if (hotSpotId == 3) {
			int cardDeck = PlayerParameters._instance.myPlayerParameter.questFormationDeck-1;
			if (myQuestFormation.cardDecks [cardDeck].deckSoldiers > 0) {
				noOfOrbsDeduct = BattleLogic._instance.AttackingOrbsUsed (PlayerParameters._instance.myPlayerParameter.avatar_level);
				BattleLogic._instance.orbsTosubtract = noOfOrbsDeduct;
				if (PlayerParameters._instance.myPlayerParameter.orb >= noOfOrbsDeduct) {
					SelectEnemyCards ();
					SetEncounterEnemyStats ();
					BattleLogic._instance.SetPlayerCardForBattle (myQuestFormation);
					loadingScene.Instance.StartBattleQuest ();
				} else {
					popupFromServer.ShowPopup ("Not Enough Orbs");
				}
			}
			else {
				popupFromServer.ShowPopup ("Your formation does not hold any soldiers. Replenish your Army!");
			}
				

		} else {
			randowmFunction = Random.Range (0, 3);
			Debug.Log("randowmFunction = "+randowmFunction);
			if (randowmFunction == 0) {
				Invoke ("enableWrningText", 0.5f);
			} else {
				Invoke ("enableGreenWarning", 0.5f);
			}
		}

	}
	public void enableWrningText()
	{
		Debug.Log("enable waringng");
		warningText.SetActive(true);
		Invoke("enableAmbush",0.5f);
	}

	void enableAmbush()
	{
		Debug.Log("enable ambush");
		int hotSpotId = myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].hotSpotNoUsed[(hotspotPage*5) + (hotSpotClicked-1)];
		loadingScene.Instance.loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected)
			{
				Dictionary<string, string> avatarParameters = new Dictionary<string, string> ();
				if(noOfOrbsDeduct > 0)
				{
					avatarParameters.Add ("orb", (PlayerParameters._instance.myPlayerParameter.orb-noOfOrbsDeduct).ToString ());
					avatarParameters.Add ("orb_time" , PlayerParameters._instance.myPlayerParameter.orb_time.ToString ());
				}
				avatarParameters.Add ("stamina", staminaToSend.ToString ());
				avatarParameters.Add ("stamina_time" , PlayerParameters._instance.myPlayerParameter.stamina_time.ToString ());
				StartCoroutine (PlayerParameters._instance.SendPlayerParameters (avatarParameters , isSuccess => {
					if(isSuccess)
					{
						loadingScene.Instance.loader.SetActive (false);
						PlayerPrefs.GetString("hotSpot","yes"+hotSpotButton.name);
						warningText.SetActive(false);
						warningAmbush.SetActive(true);
						PlayerParameters._instance.myPlayerParameter.stamina = staminaToSend;
						PlayerParameters._instance.myPlayerParameter.orb-=noOfOrbsDeduct;
						DisplayFinalText ();
						staminaText.text = PlayerParameters._instance.myPlayerParameter.stamina.ToString ();
						hotspotNameFailed.text = QuestDataFetch._instance.hotspotTypes[hotSpotId].nameOfHotspot;
						Invoke("enableHotSpotFailed",0.5f);
					}
					else
					{
						GameObject.Find("enterButton").GetComponent<Button>().interactable=true;
						popupFromServer.ShowPopup ("Network Error!");
					}
				}));
			}
			else
			{
				if(GameObject.Find("enterButton"))
					GameObject.Find("enterButton").GetComponent<Button>().interactable=true;
				PlayerParameters._instance.myPlayerParameter.stamina+=QuestDataFetch._instance.hotspotTypes[hotSpotId].staminaReqd;
				popupFromServer.ShowPopup ("Network Error!");

			}
		});


	}

	int foodForReward , goldForReward , signalFireReward , bazarTicketForReward;
	Int64 avatarExpForReward;
	int staminaToSend;
//{"success":1,"msg":"Quest data success","Player_Card_detail":[{"quest_id":"7","chapter_name":"1","area_number":"1",
//			"percentage_cleared":"15","hotspots_cleared":"0,0,0,0,0,0,0,0,0","hotspot_percentage":"","no_of_hotspots":"0"}]}
	public void enableGreenWarning()
	{
		foodForReward = 0;
		goldForReward = 0;
		avatarExpForReward = 0;
		if (BattleLogic._instance.battleType != BattleLogic.BattleType.CHEST_BOSS) {
//			if (PlayerPrefs.GetString ("hotSpot") == "yes" + hotSpotButton.name) {
//			staminaToSend = PlayerParameters._instance.myPlayerParameter.stamina;
				int hotSpotId = myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].hotSpotNoUsed [(hotspotPage * 5) + (hotSpotClicked - 1)];
			int amtCleared = myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].amountCleared + QuestDataFetch._instance.hotspotTypes [hotSpotId].clearancePointsAwarded;
				int noOfTimesAreaWasCleared = myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].noOfTimesAreaWasCleared;

				Dictionary<string, string> playerParamsDict = new Dictionary<string, string> (); 
				if (hotSpotId == 4) {
					if (CardsManager._instance.myCaptives.Count < EmpireManager._instance.prison.finalValue1 [EmpireManager._instance.prison.currentLevel]) {
						NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
							if (isConnected) {
								StartCoroutine (GetCaptive (1, (isSuccess , msgString) => {
									if (isSuccess) {
										List <int> hotspotsCleared = new List <int> {(hotspotPage*5) + (hotSpotClicked-1)};
										RewardsData (hotSpotId, playerParamsDict, amtCleared, noOfTimesAreaWasCleared, hotspotsCleared);
									} else {
									PlayerParameters._instance.myPlayerParameter.stamina += QuestDataFetch._instance.hotspotTypes [hotSpotId].staminaReqd;
										GameObject.Find ("enterButton").GetComponent<Button> ().interactable = true;
										popupFromServer.ShowPopup ("Network Error! Cannot fetch data!");
									}
								}));
							} else {
							PlayerParameters._instance.myPlayerParameter.stamina += QuestDataFetch._instance.hotspotTypes [hotSpotId].staminaReqd;
								GameObject.Find ("enterButton").GetComponent<Button> ().interactable = true;
								popupFromServer.ShowPopup ("Network Error! Cannot fetch data!");
							}
						});
					} else {
						List <int> hotspotsCleared = new List <int> {(hotspotPage*5) + (hotSpotClicked-1)};
						RewardsData (hotSpotId, playerParamsDict, amtCleared, noOfTimesAreaWasCleared, hotspotsCleared);
					}
				} else {
					Dictionary<string, string> playerParamsDictNew = RewardsToSend (hotSpotId);
					for (int i = 0; i < playerParamsDictNew.Count; i++) {
						playerParamsDict.Add (playerParamsDictNew.Keys.ElementAt (i), playerParamsDictNew.Values.ElementAt (i));
					}
					List <int> hotspotsCleared = new List <int> {(hotspotPage*5) + (hotSpotClicked-1)};
					RewardsData (hotSpotId, playerParamsDict, amtCleared, noOfTimesAreaWasCleared, hotspotsCleared);
				}
//			}
		}
		else
		{
			int amtCleared = myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].amountCleared;
			int noOfTimesAreaWasCleared = myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].noOfTimesAreaWasCleared;
			Dictionary<string, string> playerParamsDict = new Dictionary<string, string> (); 
			AddAreaRewards (ref amtCleared, noOfTimesAreaWasCleared, ref playerParamsDict);
		}

	}

	void AddAreaRewards(ref int amtCleared , int noOfTimesAreaWasCleared, ref Dictionary<string, string> playerParamsDict)
	{
		if(amtCleared >= myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].clearancePointsNeeded && BattleLogic._instance.battleType == BattleLogic.BattleType.CHEST_BOSS)
		{
			staminaToSend = PlayerParameters._instance.myPlayerParameter.max_stamina;
			avatarExpForReward = myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].expReward;
			foodForReward = myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].wheatReward;
			goldForReward = myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].goldReward;
			amtCleared = myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].clearancePointsNeeded;
			
			if(areaNo == 10 && !myQuestManager.chapters[chapterNo - 1].isCleared)
			{
				myQuestManager.chapters[chapterNo - 1].isCleared = true;
				string chapterData = "";
				for(int i = 0 ; i < myQuestManager.chapters.Count ; i++)
				{
					if(myQuestManager.chapters [i].isCleared)
					{
						chapterData+="1";
					}
					else
					{
						chapterData+="0";
					}
					if(i < myQuestManager.chapters.Count-1)
					{
						chapterData+=",";
					}
				}
				myQuestManager.chapters[chapterNo - 1].isCleared = false;
				playerParamsDict.Add ("chapters_cleared" , chapterData);
				
			}
			playerParamsDict.Add ("captivesList","");
			playerParamsDict.Add ("event_formation","");
			if(noOfTimesAreaWasCleared == 2 || noOfTimesAreaWasCleared == 4)
			{
				playerParamsDict.Add ("unlocked_stamina_potion" , PlayerParameters._instance.myPlayerParameter.unlocked_stamina_potion+myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].unlockedStaminaPotionReward.ToString ());
				playerParamsDict.Add ("unlocked_attack_potion" , PlayerParameters._instance.myPlayerParameter.unlocked_attack_potion+myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].unlockedAttackPotionReward.ToString ());
				playerParamsDict.Add ("stamina_potion" , PlayerParameters._instance.myPlayerParameter.stamina_potion+myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].staminaPotionReward.ToString ());
				playerParamsDict.Add ("attack_potion" , PlayerParameters._instance.myPlayerParameter.attack_potion+myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].attackPotionReward.ToString ());
				playerParamsDict.Add ("dragon_eggs" , PlayerParameters._instance.myPlayerParameter.dragon_eggs+myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].dragonEggsReward.ToString ());
			}

			avatarExpForReward = 0;
			RewardsData(-1 , playerParamsDict , myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].clearancePointsNeeded , noOfTimesAreaWasCleared ,null);
					
						
						
			
		}
	}

	public void BossUncleared(string bossClearedValue)
	{
		loadingScene.Instance.loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected)
			{

				StartCoroutine (SendAreaData(bossClearedValue, -1 ,null , -1 , null , (isSuccess , msgString) => {
					if(isSuccess)
					{
						loadingScene.Instance.loader.SetActive (false);
						PlayerParameters._instance.myPlayerParameter.stamina = staminaToSend;
						PlayerParameters._instance.myPlayerParameter.wheat+=foodForReward;
						PlayerParameters._instance.myPlayerParameter.gold+=goldForReward;
						PlayerParameters._instance.myPlayerParameter.signal_fire+=signalFireReward;
						if(increaseAvatarLevel)
						{
							PlayerParameters._instance.myPlayerParameter.avatar_level++;
							if(PlayerParameters._instance.myPlayerParameter.avatar_level == 15)
							{
								//YOU ARE NOW ELIGIBLE FOR TRAADE
							}
							PlayerParameters._instance.myPlayerParameter.avatar_stats_pool+=4;
							PlayerParameters._instance.myPlayerParameter.avatar_attack = formalAvatarAttack;
							PlayerParameters._instance.myPlayerParameter.avatar_defense = formalAvatarDefense;
							PlayerParameters._instance.myPlayerParameter.avatar_leadership = formalAvatarLeadership;
							increaseAvatarLevel = false;
						}
						EnemyAI._instance.bossAI[chapterNo-1].chapterAI[areaNo-1].areaAI[myQuestManager.chapters[chapterNo-1].area[areaNo-1].noOfTimesAreaWasCleared].bossHealth = int.Parse (bossClearedValue);
						
						popupFromServer.ShowPopup ("Boss Stage not Cleared. Try Again!");

						
					}
					else
					{
						if(GameObject.Find("enterButton"))
							GameObject.Find("enterButton").GetComponent<Button>().interactable=true;
						popupFromServer.ShowPopup (msgString);
					}
				}));
			}
			else
			{
				if(GameObject.Find("enterButton"))
					GameObject.Find("enterButton").GetComponent<Button>().interactable=true;
				popupFromServer.ShowPopup ("Network Error! Cannot fetch data!");
			}
		});
	}


	public void ClearingArea(int amtCleared ,int noOfTimesAreaWasCleared)
	{
		PlayerParameters._instance.myPlayerParameter.questFormationDeck = 0;
//		for(int k = 0 ; k < myQuestFormation.cardDecks.Count ; k++)
//		{
//			myQuestFormation.cardDecks[k].noOfCardsSelected = 0;
//			for(int l = 0 ; l < myQuestFormation.cardDecks[k].cardRows.Count ; l++)
//			{
//				myQuestFormation.cardDecks[k].cardRows[l].cardIdsForRow.Clear ();
//			}
//		}
		foodForReward = myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].wheatReward;
		goldForReward = myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].goldReward;
		amtCleared = myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].clearancePointsNeeded;
		
		if(areaNo == 10 && !myQuestManager.chapters[chapterNo - 1].isCleared)
		{
			myQuestManager.chapters[chapterNo - 1].isCleared = true;
		}
		
		myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].isCleared = true;
		if(noOfTimesAreaWasCleared == 2 || noOfTimesAreaWasCleared == 4)
		{
			PlayerParameters._instance.myPlayerParameter.unlocked_stamina_potion+=myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].unlockedStaminaPotionReward;
			PlayerParameters._instance.myPlayerParameter.unlocked_attack_potion+=myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].unlockedAttackPotionReward;
			PlayerParameters._instance.myPlayerParameter.stamina_potion+=myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].staminaPotionReward;
			PlayerParameters._instance.myPlayerParameter.attack_potion+=myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].attackPotionReward;
			PlayerParameters._instance.myPlayerParameter.dragon_eggs+=myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].dragonEggsReward;
			areaClearedWithRewards.SetActive (true);
			baseRewards.SetActive (false);
			specialRewards.SetActive (true);
			specialRewardsText[0].text = "Exp \n"+myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].expReward;
			specialRewardsText[1].text = "Food \n"+myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].wheatReward;
			specialRewardsText[2].text = "Gold \n"+myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].goldReward;
			
			if(myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].unlockedStaminaPotionReward > 0)
			{
				specialRewardsText[3].text = "Energy \nDrink\n"+myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].unlockedStaminaPotionReward;
				specialRewardsText[4].text = "Attack \nPotion\n"+myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].unlockedAttackPotionReward;
				specialRewardsText[5].text = "Peace \nTreaty\n"+myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].peaceTreatyReward;
				
			}
			else
			{
				specialRewardsText[3].text = "Energy \nDrink\n"+myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].staminaPotionReward;
				specialRewardsText[4].text = "Attack \nPotion\n"+myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].attackPotionReward;
				specialRewardsText[5].text = "Dragon \nEggs\n"+myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].dragonEggsReward;
				
			}
			
		}
		else if( noOfTimesAreaWasCleared < 5)
		{
			areaClearedWithRewards.SetActive (true);
			baseRewards.SetActive (true);
			specialRewards.SetActive (false);
			baseRewardsText[0].text = "Exp \n"+myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].expReward;
			baseRewardsText[1].text = "Food \n"+myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].wheatReward;
			baseRewardsText[2].text = "Gold \n"+myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].goldReward;
		}
		else
		{
			areaClearedWithoutRewards.SetActive (true);
		}
		if(GameObject.Find("enterButton"))
			GameObject.Find("enterButton").GetComponent<Button>().interactable=true;
		
		warningClear.SetActive(false);
		hotSpotDescription.SetActive(false);
		greenWarning.SetActive(false);
	}

	public void StartBossBattle()
	{
//		int cardDeck = myQuestFormation.TotalSoldiersInDeck();
		int cardDeck = PlayerParameters._instance.myPlayerParameter.questFormationDeck-1;
		if (myQuestFormation.cardDecks [cardDeck].deckSoldiers > 0) {
			noOfOrbsDeduct = BattleLogic._instance.AttackingOrbsUsed (PlayerParameters._instance.myPlayerParameter.avatar_level);
			BattleLogic._instance.orbsTosubtract = noOfOrbsDeduct;
			if (PlayerParameters._instance.myPlayerParameter.orb >= noOfOrbsDeduct) {
				SelectEnemyCards ();
				SetBossStats ();
				BattleLogic._instance.SetPlayerCardForBattle (myQuestFormation);
				loadingScene.Instance.StartBattleQuest ();
			} else {
				popupFromServer.ShowPopup ("Not Enough Orbs!");
			}
		} else {
			popupFromServer.ShowPopup ("Your formation does not hold any soldiers. Replenish your Army!");
		}
	}

	public void ExitBossPopup()
	{
		popupToStartBossBattle.SetActive (false);
	}

	public void CallPopupForBoss()
	{
		if (getReadyForBossBattle)
			popupToStartBossBattle.SetActive (true);
	}

	void RewardsData(int hotSpotId , Dictionary<string, string> playerParamsDict , int amtCleared , int noOfTimesAreaWasCleared , List <int> hotspotNosCleared)
	{
		if(foodForReward > 0)
		{
			playerParamsDict.Add ("wheat", (PlayerParameters._instance.myPlayerParameter.wheat+foodForReward).ToString ());
			playerParamsDict.Add ("wheat_time", PlayerParameters._instance.myPlayerParameter.wheat_time.ToString ());
		}
		if(goldForReward > 0)
		{
			playerParamsDict.Add ("gold", (PlayerParameters._instance.myPlayerParameter.gold+goldForReward).ToString ());
			playerParamsDict.Add ("gold_time", PlayerParameters._instance.myPlayerParameter.gold_time.ToString ());
		}
		if (signalFireReward > 0) {
			playerParamsDict.Add ("signal_fire",(PlayerParameters._instance.myPlayerParameter.signal_fire+signalFireReward).ToString ());
		}
		if (noOfOrbsDeduct > 0) {
			playerParamsDict.Add ("orb",(PlayerParameters._instance.myPlayerParameter.orb-noOfOrbsDeduct).ToString ());
		}

		if (bazarTicketForReward > 0) {
			playerParamsDict.Add ("bazar_tickets",(PlayerParameters._instance.myPlayerParameter.bazaarTickets+bazarTicketForReward).ToString ());
		}
		Int64 finalAvatarExp = PlayerParameters._instance.myPlayerParameter.avatar_exp;
		if (BattleLogic._instance.battleType != BattleLogic.BattleType.CHEST_BOSS) {
			playerParamsDict.Add ("stamina", staminaToSend.ToString ());
			playerParamsDict.Add ("stamina_time", TimeManager._instance.GetCurrentServerTime ().ToString ());
		

			if (avatarExpForReward > 0) {
				finalAvatarExp = PlayerParameters._instance.myPlayerParameter.avatar_exp + avatarExpForReward;
				Int64 reqdAvtarExpForLevelUp = 0;
				if (PlayerParameters._instance.myPlayerParameter.avatar_level < PlayerParameters._instance.avatarReqdExpForLevelUp.Length) {
					reqdAvtarExpForLevelUp = PlayerParameters._instance.avatarReqdExpForLevelUp [PlayerParameters._instance.myPlayerParameter.avatar_level];
				} else {
					reqdAvtarExpForLevelUp = PlayerParameters._instance.avatarReqdExpForLevelUp [PlayerParameters._instance.avatarReqdExpForLevelUp.Length - 1];
				
				}
			
				Debug.Log ("current avater exp = " + PlayerParameters._instance.myPlayerParameter.avatar_exp);
				Debug.Log ("reqdAvtarExpForLevelUp = " + reqdAvtarExpForLevelUp);
				Debug.Log ("finalAvatarExp = " + finalAvatarExp);
				Debug.Log ("avatarExpForReward = " + avatarExpForReward);
			
				if (finalAvatarExp > reqdAvtarExpForLevelUp) {
					PlayerParameters._instance.myPlayerParameter.avatar_level++;
					AvatarStatsIncreasedOnLevelUp ();
					increaseAvatarLevel = true;
					playerParamsDict.Add ("avatar_stats_pool", (PlayerParameters._instance.myPlayerParameter.avatar_stats_pool + 4).ToString ());
					playerParamsDict.Add ("avatar_level", PlayerParameters._instance.myPlayerParameter.avatar_level.ToString ());
					PlayerParameters._instance.myPlayerParameter.avatar_level--;
					finalAvatarExp = finalAvatarExp - PlayerParameters._instance.avatarReqdExpForLevelUp [PlayerParameters._instance.myPlayerParameter.avatar_level];
				}
				playerParamsDict.Add ("avatar_exp", finalAvatarExp.ToString ());
			}

		}
		if (hotspotNosCleared != null) {
			for (int i = 0; i < hotspotNosCleared.Count; i++) {
				myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].isHotSpotCleared [hotspotNosCleared [i]] = true;
			}
		}
		string hotspotData = "";
		for(int i = 0 ; i < myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].isHotSpotCleared.Count; i++)
		{
			if(myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].isHotSpotCleared[i])
			{
				hotspotData+="1";
			}
			else
			{
				hotspotData+="0";
			}
			if(i < myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].isHotSpotCleared.Count-1)
			{
				hotspotData+=",";
			}
		}
		if (hotspotNosCleared != null) {
			for (int i = 0; i < hotspotNosCleared.Count; i++) {
				myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].isHotSpotCleared [hotspotNosCleared [i]] = false;
			}
		}

		loadingScene.Instance.loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected)
			{
				bool isBossCleared = false;
				if(BattleLogic._instance.battleType == BattleLogic.BattleType.CHEST_BOSS)
				{
					isBossCleared = true;
				}
				Debug.Log("------isBossCleared"+isBossCleared);
				StartCoroutine (SendAreaData(isBossCleared.ToString (), amtCleared ,hotspotData , noOfTimesAreaWasCleared , playerParamsDict , (isSuccess , msgString) => {
					if(isSuccess)
					{
						loadingScene.Instance.loader.SetActive (false);
						PlayerParameters._instance.myPlayerParameter.orb-=noOfOrbsDeduct;
						PlayerParameters._instance.myPlayerParameter.stamina = staminaToSend;
						PlayerParameters._instance.myPlayerParameter.bazaarTickets+=bazarTicketForReward;
						if(hotspotNosCleared == null || hotspotNosCleared.Count == 0)
						{
						}
						else
						{
							for (int i = 0; i < hotspotNosCleared.Count; i++) {
								myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].isHotSpotCleared[hotspotNosCleared[i]] = true;
								int hotspotid = myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].hotSpotNoUsed[hotspotNosCleared[i]];
								Rewards(hotspotid);
								hotspotNameClear.text = QuestDataFetch._instance.hotspotTypes[hotspotid].nameOfHotspot;
								Debug.Log("hotspotNosCleared[i] = "+hotspotNosCleared[i]);
								int idOfHotspotBtn = hotspotNosCleared[i] - (hotspotPage*5);
								Debug.Log("idOfHotspotBtn = "+idOfHotspotBtn);
								Debug.Log("hotspot image name = "+randomHotSpot[hotspotid].name);
								hotspotButtons[idOfHotspotBtn].hotspotImage.sprite=randomHotSpot[hotspotid];
								hotspotButtons[idOfHotspotBtn].staminaReqd.text="0";
							}
						}

						PlayerParameters._instance.myPlayerParameter.wheat+=foodForReward;
						PlayerParameters._instance.myPlayerParameter.gold+=goldForReward;
						PlayerParameters._instance.myPlayerParameter.signal_fire+=signalFireReward;

						if(increaseAvatarLevel)
						{
							PlayerParameters._instance.myPlayerParameter.avatar_level++;
							if(PlayerParameters._instance.myPlayerParameter.avatar_level == 15)
							{
								//YOU ARE NOW ELIGIBLE FOR TRAADE
							}
							PlayerParameters._instance.myPlayerParameter.avatar_stats_pool+=4;
							PlayerParameters._instance.myPlayerParameter.avatar_attack = formalAvatarAttack;
							PlayerParameters._instance.myPlayerParameter.avatar_defense = formalAvatarDefense;
							PlayerParameters._instance.myPlayerParameter.avatar_leadership = formalAvatarLeadership;
							increaseAvatarLevel = false;
						}
						PlayerParameters._instance.myPlayerParameter.avatar_exp = finalAvatarExp;
						
						if(amtCleared >= myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].clearancePointsNeeded)
						{
							amtCleared = myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].clearancePointsNeeded;
							myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].isAllHotSpotsCleared = true;
							if(isBossCleared)
							{
								ClearingArea(amtCleared , noOfTimesAreaWasCleared);
							}
							else
							{
								if(!forgingClicked)
								{
									getReadyForBossBattle = true;
									if(!forgingClicked)
										Invoke("enableLucky",0.5f);
									else
									{
										if(hotspotNosCleared == null || hotspotNosCleared.Count == 0)
										{
											popupFromServer.ShowPopup ("No Hotspot Cleared!");
										}
										else
										{
											popupFromServer.ShowPopup ("Forging Successful With Rewards");
										}
									}
								}
								else
								{
									if(hotspotNosCleared == null || hotspotNosCleared.Count == 0)
									{
										popupFromServer.ShowPopup ("No Hotspot Cleared!");
									}
									else
									{
										popupFromServer.ShowPopup ("Forging Successful With Rewards");
										getReadyForBossBattle = true;
									}
								}
							}

						}
						else
						{
							if(!forgingClicked)
								Invoke("enableLucky",0.5f);
							else
							{
								if(hotspotNosCleared == null || hotspotNosCleared.Count == 0)
								{
									popupFromServer.ShowPopup ("No Hotspot Cleared!");
								}
								else
								{
									popupFromServer.ShowPopup ("Forging Successful With Rewards");
								}
							}
						}
						myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].amountCleared = amtCleared;
						float percentageValueToClearArea = (float)myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].amountCleared/myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].areas[noOfTimesAreaWasCleared].clearancePointsNeeded;
						if(percentageValueToClearArea > 1)
							percentageValueToClearArea = 1;
						areaPercentageSlider.value = percentageValueToClearArea;
						areaPercentageText.text = (Mathf.FloorToInt (percentageValueToClearArea*100))+"%";
						areaList[areaNo-1].areaClearedValue.value = percentageValueToClearArea;
						areaList[areaNo-1].areaClearedPercentage.text = areaPercentageText.text;
						int noOfHotpotsCleared = 0;
						for(int j = 0 ; j < myQuestManager.chapters [chapterNo - 1].area [areaNo-1].isHotSpotCleared.Count ; j++)
						{
							if(myQuestManager.chapters [chapterNo - 1].area [areaNo-1].isHotSpotCleared[j])
							{
								noOfHotpotsCleared++;
							}
						}
						areaList [areaNo-1].hotspotNo.text = noOfHotpotsCleared+" / " + myQuestManager.chapters [chapterNo - 1].area [areaNo-1].hotSpotNoUsed.Count;
						greenWarning.SetActive(true);
						DisplayFinalText();
						
					}
					else
					{
						if(GameObject.Find("enterButton"))
							GameObject.Find("enterButton").GetComponent<Button>().interactable=true;
						popupFromServer.ShowPopup (msgString);
					}
				}));
			}
			else
			{
				if(GameObject.Find("enterButton"))
					GameObject.Find("enterButton").GetComponent<Button>().interactable=true;
				popupFromServer.ShowPopup ("Network Error! Cannot fetch data!");
			}
		});
	}
	

	IEnumerator SendAreaData(string  bossPlayed , int amountCleared ,string hotspotData, int noOfTimesHotspotCleared,  Dictionary<string, string> playerParameters , Action <bool,string> callBack )
	{
		Debug.Log ("amount cleared = "+amountCleared);
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag" ,"doEventQuestData");
		wwwForm.AddField ("user_id" , PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id" , SystemInfo.deviceUniqueIdentifier );
		wwwForm.AddField ("chapter_name" , chapterNo );
		wwwForm.AddField ("area_number" , areaNo );
		if(amountCleared != -1)
			wwwForm.AddField ("percentage_cleared" , amountCleared);
		if(hotspotData != null)
			wwwForm.AddField ("hotspots_cleared" , hotspotData);
		if(noOfTimesHotspotCleared != -1)
			wwwForm.AddField ("no_of_hotspots" , noOfTimesHotspotCleared);
		wwwForm.AddField ("boss_played" , bossPlayed);
		if (playerParameters != null) {
			wwwForm.AddField ("array_players", "players");

			for (int i = 0; i < playerParameters.Count; i++) {
				wwwForm.AddField (playerParameters.Keys.ElementAt (i), playerParameters.Values.ElementAt (i));
			}

		}

		if (chestData != null) {
			wwwForm.AddField ("arr_chestData", "chestData");

			for (int i = 1; i < chestData.Count; i++) {
				Debug.Log (chestData.Keys.ElementAt (i).ToString()+"," +chestData.Values.ElementAt (i));
				wwwForm.AddField (chestData.Keys.ElementAt (i), chestData.Values.ElementAt (i));
			}

		}

		WWW wwwAreaData = new WWW(loadingScene.Instance.baseUrl , wwwForm);
		yield return wwwAreaData;
		if (wwwAreaData.error == null) {
			Debug.Log(wwwAreaData.text);
			IDictionary resultDict = (IDictionary)Json.Deserialize (wwwAreaData.text);
			if(wwwAreaData.text.Contains ("error_msg"))
			{
				callBack (false , resultDict["error_msg"].ToString ());
			}
			else
			{
				callBack(true , "");
			}
			
		} else {
			callBack (false , "Network Error!");
		}
	}

	int rewardFetched;
	bool increaseAvatarLevel;
	int formalAvatarAttack, formalAvatarDefense, formalAvatarLeadership;

	void AvatarStatsIncreasedOnLevelUp()
	{
		formalAvatarAttack = PlayerParameters._instance.myPlayerParameter.avatar_attack;
		formalAvatarDefense = PlayerParameters._instance.myPlayerParameter.avatar_defense;
		formalAvatarLeadership = PlayerParameters._instance.myPlayerParameter.avatar_leadership;
		switch (PlayerParameters._instance.myPlayerParameter.avatar_no) {
		case 1:  //Andras
			formalAvatarAttack++;
			break;
		case 2: //Kitni
			formalAvatarDefense++;
			break;
		case 3: //Pvenma
			formalAvatarLeadership++;
			break;
		}
	}


	IEnumerator GetCaptive(int noOfCaptives , Action <bool , string> callBack)
	{
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag" ,"userSingleCardsPlayercards");
		wwwForm.AddField ("user_id" , PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id" , SystemInfo.deviceUniqueIdentifier );
		wwwForm.AddField ("limit" , noOfCaptives );
		//{"success":1,"msg":"Cards data success","Player_Card_detail":
	//[{"card_id":"211","cardCategory":"Andras","cardName":"Lovdione","cardRarity":"Uncommon","cardskillstrength1":"Weak",
		//"cardskillstrength2":"Weak","cardSkillsname1":"Fierce Outrage","cardSkillsname2":"Fierce Outrage",
		//"card_no_in_players_list":981,"experience":100,"cardLevel":1,"subCardId":"264","subCard_cardId":"211",
		//"subCardtype":"Leader","subcardLeadership":"249","subCardAttack":"157","subCardDefense":"146","is_captive":1}]}
		WWW wwwCaptive = new WWW(loadingScene.Instance.baseUrl , wwwForm);
		yield return wwwCaptive;
		if (wwwCaptive.error == null) {
			Debug.Log(wwwCaptive.text);
			IDictionary resultDict = (IDictionary)Json.Deserialize (wwwCaptive.text);
			// {"success":0,"error_msg":"No data available!"}
			if(wwwCaptive.text.Contains ("error_msg"))
			{
				callBack (false , resultDict["error_msg"].ToString ());
			}
			else
			{
				IList captiveList = (IList)resultDict["Player_Card_detail"];
				for(int i = 0 ; i < captiveList.Count ; i++)
				{
					IDictionary captiveDict = (IDictionary)captiveList[i];
					CardsManager.CardParameters captiveCard = new CardsManager.CardParameters();
					captiveCard.card_name = captiveDict["cardName"].ToString ();
					captiveCard.card_id_in_database = int.Parse (captiveDict["card_id"].ToString ());
					captiveCard.card_id_in_playerList = int.Parse (captiveDict["card_no_in_players_list"].ToString ());
					captiveCard.rarity = captiveDict["cardRarity"].ToString ();
					captiveCard.type = captiveDict["subCardtype"].ToString ();
					captiveCard.cardClass = captiveDict["cardCategory"].ToString ();
					captiveCard.attack = int.Parse (captiveDict["subCardAttack"].ToString ());
					captiveCard.defense = int.Parse (captiveDict["subCardDefense"].ToString ());
					captiveCard.leadership = int.Parse (captiveDict["subcardLeadership"].ToString ());
					captiveCard.experience = int.Parse (captiveDict["experience"].ToString ());
					captiveCard.skill_1 = captiveDict["cardSkillsname1"].ToString ();
					captiveCard.skill_1_Strength = captiveDict["cardskillstrength1"].ToString ();
					captiveCard.skill_2 = captiveDict["cardSkillsname2"].ToString ();
					captiveCard.skill_2_Strength = captiveDict["cardskillstrength2"].ToString ();
					captiveCard.cardCost = int.Parse (captiveDict["cardCost"].ToString ());
					captiveCard.cardSpriteFromResources  = (Sprite)Resources.Load<Sprite>("images/"+captiveCard.card_name);
					captiveCard.is_captive = 1;
					captiveCard.fear_factor = 0;
					CardsManager._instance.myCaptives.Add (captiveCard);
				}
				callBack(true , wwwCaptive.text);
			}
			
		} else {
			
			callBack (false , "Network Error!");
		}
	}


	public void FetchBazarTicketReward()
	{
		if((PlayerParameters._instance.myPlayerParameter.totalPostedTrades+PlayerParameters._instance.myPlayerParameter.bazaarTickets) < 10 && PlayerParameters._instance.myPlayerParameter.avatar_level > 14)
		{
			int probabilityOfFinding = Random.Range (0,10);
			if(probabilityOfFinding < 7)
			{
				bazarTicketForReward++;
			}
		}
	}

	Dictionary<string, string> RewardsToSend(int hotspotId)
	{
		chestData = null;
		Dictionary<string, string> playerParams = new Dictionary<string, string>();
		int noOTimeAreaCleared = myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].noOfTimesAreaWasCleared;
		avatarExpForReward+= QuestDataFetch._instance.hotspotTypes [hotspotId].avatarExpFetched;
		switch (hotspotId) {
		case 0:
			FetchBazarTicketReward();
			break;
		case 1:
			FetchBazarTicketReward();
			break;
		case 2:
			FetchBazarTicketReward();
			break;
		case 3:
			//encounet ambush
			foodForReward+= BattleLogic._instance.foodLoot;
			goldForReward+= BattleLogic._instance.goldLoot;
			break;
		case 5:
			//food
			rewardFetched = (int)Random.Range (1,6)*EmpireManager._instance.storage.finalValue1[EmpireManager._instance.storage.currentLevel]/100;
			foodForReward+= rewardFetched;
			break;
		case 6:
			// cargo
			rewardFetched = (int)Random.Range (1,6)*EmpireManager._instance.storage.finalValue1[EmpireManager._instance.storage.currentLevel]/100;
			goldForReward+= rewardFetched;
			break;
		case 7:
			//stamina
			staminaToSend = PlayerParameters._instance.myPlayerParameter.max_stamina;
			break;
		case 8:
			//call toarm
			signalFireReward++;
			break;
		case 9:
			//orb
			playerParams.Add ("orb",PlayerParameters._instance.myPlayerParameter.maxOrb.ToString ());
			playerParams.Add ("orb_time",TimeManager._instance.GetCurrentServerTime ().ToString ());
			break;
		case 10:
			//artefact
			countOfArtefact = 0;
			chestData = ChestData._instance.ArtefactHotspotReward(ref artefactRewardName , ref countOfArtefact);
			break;
		}
		return playerParams;
	}
	string artefactRewardName;
	int countOfArtefact;
	void Rewards(int hotspotId)
	{
		threeRewardsObjectReward1.text = "Exp \n"+QuestDataFetch._instance.hotspotTypes[hotspotId].avatarExpFetched;
		reward1.text = "Exp \n"+QuestDataFetch._instance.hotspotTypes[hotspotId].avatarExpFetched;
		expRewardOnly.text = "Exp \n"+QuestDataFetch._instance.hotspotTypes[hotspotId].avatarExpFetched;
		switch (hotspotId) {
		case 0:
			if(bazarTicketForReward <= 0)
				EnableRewardOnly();
			else
			{
				EnableOtherRewards();
				reward2.text = "Bazar Ticket \n +1";
			}
			break;
		case 1:
			if(bazarTicketForReward <= 0)
				EnableRewardOnly();
			else
			{
				EnableOtherRewards();
				reward2.text = "Bazar Ticket \n +1";
			}
			break;
		case 2:
			if(bazarTicketForReward <= 0)
				EnableRewardOnly();
			else
			{
				EnableOtherRewards();
				reward2.text = "Bazar Ticket \n +1";
			}
			break;
		case 3:
			Enable3OtherRewards();
			threeRewardsObjectReward2.text = "Food \n +"+BattleLogic._instance.foodLoot;
			threeRewardsObjectReward3.text = "Gold \n +"+BattleLogic._instance.goldLoot;
			break;
		case 4:
			EnableOtherRewards();
			reward2.text = "Captive \n +1";
			break;
		case 5:
			//food
			EnableOtherRewards();
			reward2.text = "Food \n +"+rewardFetched;
			break;
		case 6:
			// cargo
			EnableOtherRewards();
			reward2.text = "Gold \n +"+rewardFetched;
			break;
		case 7:
			//stamina
			EnableOtherRewards();
			PlayerParameters._instance.myPlayerParameter.stamina = PlayerParameters._instance.myPlayerParameter.max_stamina;
			PlayerParameters._instance.myPlayerParameter.stamina_time = TimeManager._instance.GetCurrentServerTime ();
			reward2.text = "Stamina \n Filled";
			break;
		case 8:
			//call toarm
			EnableOtherRewards();
			reward2.text = "Call to \nArms \n +1";
			break;
		case 9:
			//orb
			EnableOtherRewards();
			PlayerParameters._instance.myPlayerParameter.orb = PlayerParameters._instance.myPlayerParameter.maxOrb;
			PlayerParameters._instance.myPlayerParameter.orb_time = TimeManager._instance.GetCurrentServerTime ();
			reward2.text = "Attacking \nOrbs \n Filled";
			break;
		case 10:
			EnableOtherRewards ();
			ChestData._instance.UpdateChestDataTableIntVariables (int.Parse (chestData.Values.ElementAt (0)), countOfArtefact);
			if (artefactRewardName.Contains ("Chest"))
				reward2.text = artefactRewardName + " \n +1";
			else
				reward2.text = artefactRewardName + " \n +" + countOfArtefact;

			if (chestData.Count > 2) {
				ChestData._instance.chestData.isBlessingActive = true;
				ChestData._instance.chestData.blessingStartTime = Convert.ToDateTime (chestData.Values.ElementAt (1));
			}
			//artefact
			break;
		}
	}
		

	void EnableRewardOnly()
	{
		threeRewardsObject.SetActive (false);
		expRewardOnly.transform.parent.gameObject.SetActive (true);
		reward1.transform.parent.gameObject.SetActive (false);
		reward2.transform.parent.gameObject.SetActive (false);
	}

	void EnableOtherRewards()
	{
		threeRewardsObject.SetActive (false);
		expRewardOnly.transform.parent.gameObject.SetActive (false);
		reward1.transform.parent.gameObject.SetActive (true);
		reward2.transform.parent.gameObject.SetActive (true);
		reward2.gameObject.SetActive (true);
		rewardImage.gameObject.SetActive (false);
	}

	void Enable3OtherRewards()
	{
		expRewardOnly.transform.parent.gameObject.SetActive (false);
		reward1.transform.parent.gameObject.SetActive (false);
		reward2.transform.parent.gameObject.SetActive (false);
		threeRewardsObject.SetActive (true);
		rewardImage.gameObject.SetActive (false);
	}


	void enableLucky()
	{
		greenWarning.SetActive(false);
		warningClear.SetActive(true);
		Invoke("enableHotSpotClear",0.5f);
	}

	void enableHotSpotClear()
	{
		hotSpotClear.SetActive(true);
	}
	void enableHotSpotFailed()
	{

		hotSpotFailed.SetActive(true);
	}
	public void exitFialed()
	{
		if(GameObject.Find("enterButton"))
			GameObject.Find("enterButton").GetComponent<Button>().interactable=true;

		hotSpotDescription.SetActive(false);
		hotSpotFailed.SetActive(false);
		warningAmbush.SetActive(false);


	}

	public void AreaClearedExit()
	{
		areaClearedWithRewards.SetActive (false);
		areaClearedWithoutRewards.SetActive (false);
	}

	public void exitClear()
	{
		if(GameObject.Find("enterButton"))
			GameObject.Find("enterButton").GetComponent<Button>().interactable=true;

		warningClear.SetActive(false);
		hotSpotDescription.SetActive(false);
		hotSpotClear.SetActive(false);
		if (getReadyForBossBattle) {
			popupToStartBossBattle.SetActive (true);
		}
		getReadyForBossBattle = false;
	}

	int staminaToDeduct;

	public void hotspotClick(Button click)
	{
		noOfOrbsDeduct = 0;
		BattleLogic._instance.orbsTosubtract = noOfOrbsDeduct;
		int amtCleared = myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].amountCleared;
		int noOfTimesAreaWasCleared = myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].noOfTimesAreaWasCleared;

		if (amtCleared < myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].areas [noOfTimesAreaWasCleared].clearancePointsNeeded) {
			hotSpotButton = click;
			Debug.Log ("hotSpotButton.name = " + hotSpotButton.name);
			hotSpotClicked = int.Parse (hotSpotButton.name [hotSpotButton.name.Length - 1].ToString ());
			int hotSpotId = myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].hotSpotNoUsed [(hotspotPage * 5) + (hotSpotClicked - 1)];
			
			if(PlayerParameters._instance.myPlayerParameter.stamina >= QuestDataFetch._instance.hotspotTypes[hotSpotId].staminaReqd)
			{
				bool shouldCarryOn = false;
				if(hotSpotId == 3)
				{
					noOfOrbsDeduct = BattleLogic._instance.AttackingOrbsUsed (PlayerParameters._instance.myPlayerParameter.avatar_level);
					BattleLogic._instance.orbsTosubtract = noOfOrbsDeduct;
					if(PlayerParameters._instance.myPlayerParameter.orb >= noOfOrbsDeduct)
					{
						shouldCarryOn = true;
					}
					else
					{
						popupFromServer.ShowPopup ("Not Enough Orbs");
					}
				}
				else
					shouldCarryOn = true;
				if(shouldCarryOn)
				{
					staminaToDeduct = QuestDataFetch._instance.hotspotTypes[hotSpotId].staminaReqd;
					PlayerPrefs.SetString ("hotSpot", "yes" + hotSpotButton.name);
					warningClear.SetActive (false);
					greenWarning.SetActive (false);
					warningAmbush.SetActive (false);
					warningText.SetActive (false);
					hotSpotDescription.SetActive (true);

					hotspotNameInDescription.text = QuestDataFetch._instance.hotspotTypes [hotSpotId].nameOfHotspot;
					hotspotDescriptionStaminaReqd.text = "?"; //QuestDataFetch._instance.hotspotTypes [hotSpotId].staminaReqd.ToString ();
					hotspotDescriptionText.text = QuestDataFetch._instance.hotspotTypes [hotSpotId].descriptionOfHotspot;

					defaultHotSpot.sprite = randomHotSpot [hotSpotId];
				}
			}
			else
			{
				popupFromServer.ShowPopup ("Not Enough Stamina!\n Use some Energy Drink to replenish!");
			}
		} else {
			if(myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].isCleared)
				popupFromServer.ShowPopup ("Area Completely Cleared!");
			else
			{
				popupToStartBossBattle.SetActive (true);

			}
		}
	}

	public void exitHotspotDescription()
	{		
		GameObject.Find("enterButton").GetComponent<Button>().interactable=true;

		hotSpotDescription.SetActive(false);
		warningAmbush.SetActive(false);
		warningClear.SetActive(false);
		greenWarning.SetActive(false);
		warningText.SetActive(false);
	}
	public void retreat()
	{
		warningAreaExplored.text = "Area Explored: "+areaPercentageText.text;
		warning.SetActive(true);
	}
	public void extiRetreat()
	{
		warning.SetActive(false);
	}

	public void retreatYesClick()
	{
		int noOfTimesAreaWasCleared = myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].noOfTimesAreaWasCleared;
		Dictionary<string, string> playerParamsDict = new Dictionary<string, string>(); 

		playerParamsDict.Add ("captivesList","");
		playerParamsDict.Add ("event_formation","");
		playerParamsDict.Add ("currently_deployed_soldiers",PlayerParameters._instance.myPlayerParameter.maxDeployedSoldiers.ToString ());
		playerParamsDict.Add ("stamina",PlayerParameters._instance.myPlayerParameter.max_stamina.ToString ());
		playerParamsDict.Add ("stamina_time",TimeManager._instance.GetCurrentServerTime().ToString ());
		playerParamsDict.Add ("orb",PlayerParameters._instance.myPlayerParameter.maxOrb.ToString ());
		playerParamsDict.Add ("orb_time",TimeManager._instance.GetCurrentServerTime().ToString ());
		loadingScene.Instance.loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected)
			{
				StartCoroutine (SendAreaData("false", 0 ,"" , noOfTimesAreaWasCleared, playerParamsDict , (isSuccess , msgString) => {
					if(isSuccess)
					{
						loadingScene.Instance.loader.SetActive (false);
						for(int i = 0 ;i < myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].isHotSpotCleared.Count ; i++)
						{
							myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].isHotSpotCleared[i] = false;
						}
						loadingScene.Instance.loader.SetActive (false);
						myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].isCleared = false;
						myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].isAllHotSpotsCleared = false;
						myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].amountCleared = 0;
						PlayerParameters._instance.myPlayerParameter.currentlyDeployedSoldiers = PlayerParameters._instance.myPlayerParameter.maxDeployedSoldiers;
						PlayerParameters._instance.myPlayerParameter.stamina = PlayerParameters._instance.myPlayerParameter.max_stamina;
						PlayerParameters._instance.myPlayerParameter.stamina_time = TimeManager._instance.GetCurrentServerTime ();
						PlayerParameters._instance.myPlayerParameter.orb = PlayerParameters._instance.myPlayerParameter.maxOrb;
						PlayerParameters._instance.myPlayerParameter.orb_time = TimeManager._instance.GetCurrentServerTime ();
						float percentageValueToClearArea = 0;
						areaPercentageSlider.value = 0;
						areaPercentageText.text = "0%";
						areaList[areaNo-1].areaClearedValue.value = 0;
						areaList[areaNo-1].areaClearedPercentage.text = areaPercentageText.text;
						areaList [areaNo - 1].hotspotNo.text = "0 / " + myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].hotSpotNoUsed.Count;
						areaAlreadyClearedPopup.SetActive (false);
						warning.SetActive(false);
						PlayerParameters._instance.myPlayerParameter.currentlyDeployedSoldiers = PlayerParameters._instance.myPlayerParameter.maxDeployedSoldiers;
//						for(int k = 0 ; k < myQuestFormation.cardDecks.Count ; k++)
//						{
//							myQuestFormation.cardDecks[k].noOfCardsSelected = 0;
//							for(int l = 0 ; l < myQuestFormation.cardDecks[k].cardRows.Count ; l++)
//							{
//								myQuestFormation.cardDecks[k].cardRows[l].cardIdsForRow.Clear ();
//							}
//						}
						PlayerParameters._instance.myPlayerParameter.questFormationDeck = 0;
						DisplayFinalText ();
						backFromTimer ();
					}
					else
					{
						popupFromServer.ShowPopup (msgString);
					}
				}));
			}
			else
			{
				popupFromServer.ShowPopup ("Network Error!");
			}
		});

	}

	void loadLevelNew()
	{
		hotspot.SetActive (false);
		chaptersobj.SetActive (true);
		areaSelection.SetActive (false);
		loadingScene.Instance.main();
	}

	public void backFromTimer()
	{
		hotspot.SetActive(false);
		timerMap.SetActive(true);
		clickcounter=1;
	}

	public void confirmClick()
	{
		clickcounter=3;
		hotspot.SetActive(true);
		timerMap.SetActive (true);
	}

	public void selectOrangeButton()
	{
		clickcounter=2;
		timerMap.SetActive(false);
		loadingScene.Instance.EventQuestFormation();

	}

	public void gotoFormation(int areaNumClicked)
	{
		if (areaNumClicked == 1 || myQuestManager.chapters [chapterNo - 1].area [areaNumClicked - 2].isCleared || myQuestManager.chapters [chapterNo - 1].area [areaNumClicked - 2].noOfTimesAreaWasCleared > 0) {
			Debug.Log (" areaNumClicked = " + areaNumClicked);
			areaNo = areaNumClicked;
			if(!myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].isCleared)
			{
				int areaLine = 0;
				int noOfTimesAreaCleared = myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].noOfTimesAreaWasCleared;
				Debug.Log (" noOfTimesAreaCleared = " + noOfTimesAreaCleared);
			
				if (noOfTimesAreaCleared < 5)
					areaLine = (((areaNumClicked - 1) * 5) + noOfTimesAreaCleared + 0);
				else
					areaLine = (((areaNumClicked - 1) * 5) + 4);
//				if(myQuestFormation.cardDecks[0].noOfCardsSelected == 0 && myQuestFormation.cardDecks[1].noOfCardsSelected == 0 && myQuestFormation.cardDecks[2].noOfCardsSelected == 0 )
				if(PlayerParameters._instance.myPlayerParameter.questFormationDeck == 0)
				{
					loadingScene.Instance.EventQuestFormation ();
//					myQuestFormation.RemoveAllCardsFromDecks();
				}
				else
				{
					confirmClick ();
				}
				Debug.Log ("Count = " + myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].isHotSpotCleared.Count);
				myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].areas [noOfTimesAreaCleared].noOfHotspots = myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].isHotSpotCleared.Count;
				noOfHotspotsInArea = myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].areas [noOfTimesAreaCleared].noOfHotspots;
				Debug.Log ("noOfHotspotsInArea = " + noOfHotspotsInArea);
				
				maxHotspotPage = (myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].areas [noOfTimesAreaCleared].noOfHotspots / 5) + 1;
				float percentageValueToClearArea = (float)myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].amountCleared / myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].areas [noOfTimesAreaCleared].clearancePointsNeeded;
				if(percentageValueToClearArea > 1)
					percentageValueToClearArea = 1;
				areaPercentageSlider.value = percentageValueToClearArea;
				areaPercentageText.text = Mathf.FloorToInt(percentageValueToClearArea * 100) + "%";
				prevButtonForHotSpotScroll.SetActive (false);
				nextButtonForHotSpotScroll.SetActive (true);
				hotspotPage = 0;
				HotspotDataDisplay ();
				nameOfAreaInHotspotPage.text = "Area" + areaNo;
				if(myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].isAllHotSpotsCleared)
				{
					popupToStartBossBattle.SetActive (true);
				}
			}
			else
			{
				areaAlreadyClearedPopup.SetActive (true);
			}
		} else {
			popupFromServer.ShowPopup ("Clear the Previous Areas First!");
		}
	}

	public void ConfirmReplayingArea()
	{
		int noOfTimesAreaWasCleared = myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].noOfTimesAreaWasCleared;

		loadingScene.Instance.loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected)
			{
				StartCoroutine (SendAreaData( "false" , 0 ,"" , noOfTimesAreaWasCleared+1 , null , (isSuccess , msgString) => {
					if(isSuccess)
					{
						loadingScene.Instance.loader.SetActive (false);
						myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].isHotSpotCleared.Clear ();
						myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].hotSpotNoUsed.Clear ();
						QuestDataFetch._instance.LoadHotspotsPerArea("ChapterEvent" + chapterNo  , chapterNo-1 , areaNo-1 , ((areaNo-1)*5)+noOfTimesAreaWasCleared+1 , myQuestManager);
						loadingScene.Instance.loader.SetActive (false);
						myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].isCleared = false;
						myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].isAllHotSpotsCleared = false;
						myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].amountCleared = 0;
						myQuestManager.chapters[chapterNo - 1].area[areaNo - 1].noOfTimesAreaWasCleared++;
						float percentageValueToClearArea = 0;
						areaPercentageSlider.value = 0;
						areaPercentageText.text = "0%";
						areaList[areaNo-1].areaClearedValue.value = 0;
						areaList[areaNo-1].areaClearedPercentage.text = areaPercentageText.text;
						areaList [areaNo - 1].hotspotNo.text = "0 / " + myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].hotSpotNoUsed.Count;
						areaAlreadyClearedPopup.SetActive (false);
						gotoFormation (areaNo);
					}
					else
					{
						GameObject.Find("enterButton").GetComponent<Button>().interactable=true;
						popupFromServer.ShowPopup (msgString);
					}
				}));
			}
			else
			{
				popupFromServer.ShowPopup ("Network Error!");
			}
		});
	}


	public void activeAreaPanel()
	{
		areaPanel.SetActive(true);
	}
	public void  exitAreaPanel()
	{
		areaPanel.SetActive(false);
	}
	public void chapter1()
	{
		areaSelection.SetActive(true);
		chaptersobj.SetActive(false);
	}

	public void chapters(int chapterNoClicked)
	{
		if (myQuestManager.chapters [chapterNoClicked - 1].area [0].isHotSpotCleared.Count <= 0) {
			loadingScene.Instance.loader.SetActive (true);
			NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
				if (isConnected)
				{
					StartCoroutine (FetchQuestData (chapterNoClicked , (isSuccess , msgString) => {
						Debug.Log ("isSuccess" + isSuccess + msgString);
						if (isSuccess) {
							loadingScene.Instance.loader.SetActive (false);
							if (msgString.Contains ("No data available!")) {

								List<int> areaLines = new List<int> ();
								for (int i = 0; i < 10; i++) {
									areaLines.Add (i * 5);
								}
								QuestDataFetch._instance.Load ("ChapterEvent" + chapterNoClicked , chapterNoClicked - 1, areaLines , myQuestManager);
								chapterNo = chapterNoClicked;
								if(myQuestManager.chapters [chapterNoClicked - 1].isCleared || chapterNoClicked == 1)
									SetAreaDataDisplay();
								else
									popupFromServer.ShowPopup ("Please Clear the previous Chapters!");
							}
							else
							{
								chapterNo = chapterNoClicked;
								if(myQuestManager.chapters [chapterNoClicked - 1].isCleared || chapterNoClicked == 1)
									SetAreaDataDisplay();
								else
									popupFromServer.ShowPopup ("Please Clear the previous Chapters!");
							}

						} else {
							popupFromServer.ShowPopup (msgString);
						}
					}));
				}
				else
				{
					popupFromServer.ShowPopup ("Network Error! Cannot fetch data!");
				}
			});
		} 
		else {
			chapterNo = chapterNoClicked;
			if(myQuestManager.chapters [chapterNoClicked - 1].isCleared  || chapterNoClicked == 1)
				SetAreaDataDisplay();
			else
				popupFromServer.ShowPopup ("Please Clear the previous Chapters!");
		}

	}


	void SetAreaDataDisplay()
	{
		clickcounter = 1;
		chaptersobj.SetActive (false);
		areaSelection.SetActive (true);
		
		for (int i = 0; i < areaList.Count; i++) {
			int timesAreaCleared = myQuestManager.chapters [chapterNo - 1].area [i].noOfTimesAreaWasCleared;
			if (timesAreaCleared < 5) {
				areaList [i].reward_1.sprite = wheatReward;
				areaList [i].reward_1.color = new Color (1, 1, 1, 1);
				areaList [i].reward_2.sprite = goldReward;
				areaList [i].reward_2.color = new Color (1, 1, 1, 1);
				if (myQuestManager.chapters [chapterNo - 1].area [i].areas [timesAreaCleared].attackPotionReward == 1 || myQuestManager.chapters [chapterNo - 1].area [i].areas [timesAreaCleared].unlockedAttackPotionReward == 1) {
					int spriteToshow = Random.Range (0, 2);
					if (spriteToshow == 0)
						areaList [i].reward_3.sprite = attackPotionReward;
					else if (spriteToshow == 1)
						areaList [i].reward_3.sprite = staminaPotionReward;
					areaList [i].reward_3.color = new Color (1, 1, 1, 3);
				} else {
					areaList [i].reward_3.sprite = null;
					areaList [i].reward_3.color = new Color (1, 1, 1, 0);
				}
			} else {
				areaList [i].reward_1.sprite = null;
				areaList [i].reward_1.color = new Color (1, 1, 1, 0);
				areaList [i].reward_2.sprite = null;
				areaList [i].reward_2.color = new Color (1, 1, 1, 0);
				areaList [i].reward_3.sprite = null;
				areaList [i].reward_3.color = new Color (1, 1, 1, 0);
			}
			areaList [i].itemSetIcon.sprite = Artefacts._instance.gameArtifacts [myQuestManager.chapters [chapterNo - 1].area [i].itemNoSetOfsetGained - 1].itemImage;
			float percentValue = (float)myQuestManager.chapters [chapterNo - 1].area [i].amountCleared / myQuestManager.chapters [chapterNo - 1].area [i].areas [timesAreaCleared].clearancePointsNeeded;
			if(percentValue > 1)
				percentValue = 1;
			areaList [i].areaClearedPercentage.text = Mathf.FloorToInt(percentValue * 100) + "%";
			areaList [i].areaClearedValue.value = percentValue;
			int noOfHotpotsCleared = 0;
//			Debug.Log("hotspot count = "+myQuestManager.chapters [chapterNo - 1].area [i].isHotSpotCleared.Count);
			if(myQuestManager.chapters [chapterNo - 1].area [i].amountCleared >= myQuestManager.chapters [chapterNo - 1].area [i].areas [timesAreaCleared].clearancePointsNeeded)
			{
//				myQuestManager.chapters [chapterNo - 1].area [i].isCleared = true;
				myQuestManager.chapters [chapterNo - 1].area [i].isAllHotSpotsCleared = true;

			}
			for(int j = 0 ; j < myQuestManager.chapters [chapterNo - 1].area [i].isHotSpotCleared.Count ; j++)
			{
				if(myQuestManager.chapters [chapterNo - 1].area [i].isHotSpotCleared[j])
				{
					noOfHotpotsCleared++;
				}
			}
			areaList [i].hotspotNo.text = noOfHotpotsCleared+" / " + myQuestManager.chapters [chapterNo - 1].area [i].hotSpotNoUsed.Count;
		}
	}


	public IEnumerator FetchQuestData(int chapterNoClicked , Action <bool,string> callBack)
	{
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag" ,"doGetAllEventQuestData");
		Debug.Log ("chapterNo = "+chapterNoClicked);
		wwwForm.AddField ("user_id" , PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id" , SystemInfo.deviceUniqueIdentifier );
		wwwForm.AddField ("chapter_name" , chapterNoClicked );
		//{"success":1,"msg":"Quest data success","Quest_detail":[{"quest_id":"7","chapter_name":"1","area_number":"1",
		//"amount_cleared":"15","hotspots_cleared":"1,0,0,0,0,0,0,0,0","hotspot_percentage":"","no_of_time_area_was_cleared":"0"}]}
		WWW wwwChapter = new WWW(loadingScene.Instance.baseUrl , wwwForm);
		yield return wwwChapter;
		if (wwwChapter.error == null) {
			Debug.Log(wwwChapter.text);
			IDictionary resultDict = (IDictionary)Json.Deserialize (wwwChapter.text);
			// {"success":0,"error_msg":"No data available!"}
			if(wwwChapter.text.Contains ("error_msg") || string.IsNullOrEmpty (wwwChapter.text) )
			{
				if(wwwChapter.text.Contains("No data available!"))
					callBack (true , resultDict["error_msg"].ToString ());
				else
					callBack (false , resultDict["error_msg"].ToString ());
			}
			else
			{

				IList areaList = (IList)resultDict["Quest_detail"];
				List<int> areaNos = new List<int> ();
				List<string> hotSpotsValues = new List<string> ();
				List<int> areaLines = new List<int> (){0,5,10,15,20,25,30,35,40,45};
				for(int i = 0 ; i < areaList.Count ; i++)
				{
					IDictionary areaData = (IDictionary)areaList[i];
					int areaNo = int.Parse(areaData["area_number"].ToString ());
					string bossPlayed =areaData["boss_played"].ToString ();
					Debug.Log("bossPlayed = "+bossPlayed);
					myQuestManager.chapters[chapterNoClicked-1].area[areaNo-1].noOfTimesAreaWasCleared = int.Parse(areaData["no_of_time_area_was_cleared"].ToString ());
					int noOfTimesAreaCleared = myQuestManager.chapters[chapterNoClicked-1].area[areaNo-1].noOfTimesAreaWasCleared;
					Debug.Log("noOfTimesAreaCleared = "+noOfTimesAreaCleared);
					Debug.Log("areaNo = "+areaNo);
					Debug.Log("chapterNoClicked = "+chapterNoClicked);
					if(bossPlayed == "true" || bossPlayed == "True")
						myQuestManager.chapters[chapterNoClicked-1].area[areaNo-1].isCleared = true;
					else if((bossPlayed != "false" || bossPlayed != "False") && !string.IsNullOrEmpty (bossPlayed))
					{
						if(noOfTimesAreaCleared > 4)
							noOfTimesAreaCleared = 4;
						int bossHealth = 0;
						int.TryParse (bossPlayed , out bossHealth);
						if(bossHealth > 0)
							EnemyAI._instance.bossAI[chapterNoClicked-1].chapterAI[areaNo-1].areaAI[noOfTimesAreaCleared].bossHealth = bossHealth;
					}
					myQuestManager.chapters[chapterNoClicked-1].area[areaNo-1].amountCleared = int.Parse(areaData["amount_cleared"].ToString ());
					hotSpotsValues.Add(areaData["hotspots_cleared"].ToString ());
					areaLines[areaNo-1] = (areaNo-1)*5 + myQuestManager.chapters[chapterNoClicked-1].area[areaNo-1].noOfTimesAreaWasCleared;
					areaNos.Add (areaNo);
				}
				QuestDataFetch._instance.Load ("ChapterEvent" + chapterNoClicked , chapterNoClicked - 1, areaLines, myQuestManager);
				
				for(int i = 0 ; i < hotSpotsValues.Count ; i++)
				{
					if(!string.IsNullOrEmpty (hotSpotsValues[i]))
					{
						string []hotSpotVal = hotSpotsValues[i].Split (',');
						for(int j = 0 ; j < hotSpotVal.Length ; j++)
						{
							if(hotSpotVal[j] == "1")
							{
								myQuestManager.chapters[chapterNoClicked-1].area[areaNos[i]-1].isHotSpotCleared[j] = true;
							}
						}
					}
				}
				callBack(true , wwwChapter.text);
			}
			
		} else {
			callBack (false , "Network Error!");
		}
	}



	public void HotspotNext()
	{
		if (hotspotPage < maxHotspotPage - 1)
		{
			hotspotPage++;
			prevButtonForHotSpotScroll.SetActive (true);
			if(hotspotPage == maxHotspotPage - 1)
				nextButtonForHotSpotScroll.SetActive (false);
			HotspotDataDisplay ();
		}

	}

	public void HotspotPrevious()
	{
		if (hotspotPage > 0) {
			hotspotPage--;
			nextButtonForHotSpotScroll.SetActive (true);
			if(hotspotPage == 0)
				prevButtonForHotSpotScroll.SetActive (false);
			HotspotDataDisplay ();
		}

	}

	void HotspotDataDisplay()
	{
		for (int i = 0; i< hotspotButtons.Count; i++) {
			if((hotspotPage*5 + i) < noOfHotspotsInArea)
			{
				hotspots[i].SetActive (true);
				if(myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].isHotSpotCleared[hotspotPage*5 + i])
				{
					hotspots[i].GetComponent<Button>().interactable = false;
					hotspotButtons[i].staminaReqd.text = "0";
					hotspotButtons[i].hotspotImage.sprite = randomHotSpot[myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].hotSpotNoUsed[hotspotPage*5 + i]];
				}
				else
				{
					hotspots[i].GetComponent<Button>().interactable = true;
					int hotspotUSed = myQuestManager.chapters [chapterNo - 1].area [areaNo - 1].hotSpotNoUsed[hotspotPage*5 + i];
//					hotspotButtons[i].staminaReqd.text = QuestDataFetch._instance.hotspotTypes[hotspotUSed].staminaReqd.ToString ();
					hotspotButtons[i].staminaReqd.text = "?";
					hotspotButtons[i].hotspotImage.sprite = defaultHotSpotSprite;
				}
			}
			else
				hotspots[i].SetActive (false);
		}
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
	public void chatClick()
	{
		sameStart();

		PlayerPrefs.SetString("chatScene","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
			}
			print("yes this work");
			menu.SetActive(false);
			isMneuActive=false;
			
		}
		loadingScene.Instance.chat ();
	}

	
	public void RootMenuButton()
	{
		sameStart();

		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			print("yes this work");
			menu.SetActive(false);
			isMneuActive=false;
			
		}
		loadingScene.Instance.objectToOpenMenu = this.gameObject;
		loadingScene.Instance.main ();
	}
	public void invecntory()
	{
		sameStart();

		PlayerPrefs.SetString("quest","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			print("yes this work");
			menu.SetActive(false);
			isMneuActive=false;
			
		}
		loadingScene.Instance.inventory ();
	}
	public void shop()
	{
		sameStart();

		PlayerPrefs.SetString("quest","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			print("yes this work");
			menu.SetActive(false);
			isMneuActive=false;
			
		}
		loadingScene.Instance.shop ();
	}


	public void menuPopUp()
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
			menu.SetActive(true);
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
			menu.SetActive(false);
			isMneuActive=false;
			
		}

	}

	public void exitMenu()
	{
		menu.SetActive(false);
	}

	public void backButton()
	{
		Debug.Log ("clickcounter = "+clickcounter);
		if(clickcounter==0)
		{
			sameStart();

			loadingScene.Instance.scenes[loadingScene.Instance.scenes.Count-2].SetActive(true);
			loadingScene.Instance.scenes[loadingScene.Instance.scenes.Count-1].SetActive(false);
			loadingScene.Instance.scenes.RemoveAt(loadingScene.Instance.scenes.Count-1);

				
		}
		else
		{
			if(clickcounter==1)
			{
				chaptersobj.SetActive(true);
				areaSelection.SetActive(false);
				clickcounter=0;
			}
			else if(clickcounter==2)
			{
				timerMap.SetActive(true);
				clickcounter=1;
			}
			else if(clickcounter==3)
			{
				hotspot.SetActive(false);
				timerMap.SetActive (false);
				clickcounter=2;
			}
		}

	}

	



}
