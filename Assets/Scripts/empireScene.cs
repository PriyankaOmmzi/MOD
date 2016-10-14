using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections.Generic;
using System;


 public class empireScene : MonoBehaviour
{

	public static empireScene instance;
	public GameObject Sort;
	public GameObject soldierRecruit;
	public GameObject setting;
	public GameObject[] empireBuldings;
	public GameObject scrollWindows;
	public Scrollbar scrollButton;
	public  Button lockButtonName;
	public Button lockButtonNameStorage;
	public Button lockButtonNameStorageSecondary;

	public Button lockButtonNameGate;
	public Button lockButtonNameGateSecondary;
	public Button lockButtonNameBarn;
	public Button lockButtonNameBarnSecondary;
	public Button lockButtonNameGround;
	public Button lockButtonNameGroundSecondary;
	public Button lockButtonNameGoldMine;
	public Button lockButtonGoldMineSecondary;
	public Button lockButtonNameBarrakcs;
	public Button lockButtonBarrakcsSecondary;
	public  Text lockButton;
	public  Text lockButtonGround;
	public  Text lockButtonStorage;
	public  Text lockButtonGate;

	public  Text lockButtonBarn;
	public  Text lockButtonGoldMine;
	public  Text lockBarracks;
	public List<Button>selectedCards;
	public List<Button>RECRUITCARD;
	public GameObject instant1;
	public GameObject instant2;
	int gemsCount=99999;
	public bool coolingDown;
	public float waitTime = 01.0f;
	public Image []clock1;
	public Image []clock2;
	float timerText1=29*60;
	float timerText2=14*60;
	public Text[] clockString1;
	public Text [] clockString2;
	public GameObject [] lockCards;
	public GameObject gemsClick;
	public GameObject gemsClick2;
	public GameObject levelLimit;
	public GameObject limitBtn,levelBtn;
	public GameObject updateBuilding;
	public GameObject updateCompleted;
	public GameObject researchLab;
	public GameObject researchCompleted;
	public GameObject warningPanel;
	public GameObject sortPanel;
	public GameObject sortPanelLVL;
	public GameObject levelSelectionLayout;
	public Button skill1Btn, skill2Btn;
	public GameObject limitBreakButtons;
	public GameObject researchLaboraotry;
	public GameObject labGoing;
	public GameObject labAvailable;
	public GameObject[] forTrainingtime;
	//---------------------------------
	public GameObject prisonAvailable;
	public GameObject prisonOngoing;
	public GameObject prisonInterogate;
	public GameObject interoGationCards;
	//---------------------------------
	public GameObject buildingUpgradeLayout;
	public GameObject TrainingInventory;
	public GameObject trainingCards;
	public GameObject skillPage;
	public Button[] clickedBuutons;
	public GameObject skillUp;
	public GameObject showGate;
	public GameObject Castle;
	public GameObject GoldMine;
	public GameObject Barn;
	public GameObject Storage;
	public GameObject TrainingGround;
	public GameObject Prison;
	public GameObject Laboratory;
	public GameObject knowledgeTree;
	public GameObject showBarracks;
	public GameObject recruiterLayout;
	public GameObject mainScroll;
	public Button[] bottomsButtons;
	public GameObject menuScreen;
	public GameObject chatBtn;
	public GameObject recruitmentUpdate;
	bool isMneuActive=false;
	string clickOn100;
	bool clickOnBuilding=false;
	string clickObj;
	public int selectedCard=0;
	public int selectedCardStorage=0;
	public int selectedCardStorageSecondary=0;

	public int selectedCardGate=0;
	public int selectedCardGateSecondary=0;
	public int selectedCardBarn=0;
	public int selectedCardBarnSecondary=0;
	public int selectedCardGoldMine=0;
	public int selectedCardGoldMineSecondary=0;
	public int selectedCardBarrakcs=0;
	public int selectedCardBarrakcsSecondary=0;
	public int selectedCardGround=0;
	public int selectedCardGroundSecondary=0;
	int selectedRecruiter=0;
	Button recruiterCards;
	bool armyCaptive=true;
	public Button ArmyButton;
	public Button CaptiveButton;
	public GameObject levelUp;
	public GameObject limitBreak;
	public GameObject skill1Obj;
	public GameObject skill2Obj;
	public Button Ongoing;
	public Button Available;
	public Button lockUnlock;
	bool isUnlock=true;
	int interogation=0;
	Button interroPlay;
	bool isInterrogate=false;
	int cardselectorCounter;
	public Button interroGatorCard;
	public Button confirmButton;
	public int cardResetting=0;
	// ------- PRIMARY/ SECONDAY CASTLE --------
	public string buttonName;
	string tempButton;
	string tempButtonStorage;
	string tempButtonStorageSecondary;

	string tempButtonGate;
	string tempButtonGateSecondary;
	string tempButtonBarn;
	string tempButtonBarnSecondary;
	string tempButtonGoldMine;
	string tempButtonGoldMineSecondary;
	string tempButtonBarrakSecondary;
	string tempButtonBarrakPrimary;
	string tempButtonGroundSecondary;
	string tempButtonGroundPrimary;
	public Button storageUpgradeButton;
	public int currentExperienceOfBuilding;
	public int finalExperienceOfBuilding;
	public List<Text> GemsText;
	public GameObject insufficientGems;
	GameObject[] randomPick;
	public  int randomLoad;
	public int randomLoadStorage;
	public int randomLoadGate;

	public int randomLoadBarn;
	public int randomLoadGoldMine;
	public int randomLoadBarracks;
	public int randomLoadGround;
	bool isChosenStorage=false;
	public List<Sprite> myCards = new List<Sprite>();
	public Popup popupFromServer;
	public GameObject loader;
	public Text cardSelectedAttackInCardSelectionLayout;
	public Text cardSelectedDefenseInCardSelectionLayout;
	public Text cardSelectedLeadershipInCardSelectionLayout;
	public Text buildingUpdateFoodInCardSelectionLayout;
	public Text buildingUpdateGoldInCardSelectionLayout;
	public Text buildingUpdateTimeInCardSelectionLayout;
	public Button confirmTrainingBtn;
	public Text buildingUpdateFoodInCardSelectionLayoutLvl;
	public Text buildingUpdateGoldInCardSelectionLayoutLvl;
	public Text expTimeInCardSelectionLayoutLvl;
	public GameObject sacrificeDialog;
	public Text expStatMain;
	// Use this for initialization
	public void Awake()
	{
		instance = this;

		transform.parent.gameObject.SetActive (false);
	}

	public void start ()
	{
		sacrificeDialog.SetActive (false);
//		for(int i=0;i<clickedBuutons.Length;i++)
//		{
//			clickedBuutons[i].interactable=true;
//		}

		buttonName="0";
		interogation=0;
		selectedCard=0;
		selectedCardStorage = 0;
		selectedCardStorageSecondary = 0;

		selectedCardGate = 0;
		selectedCardGateSecondary = 0;
		selectedCardBarn=0;
		selectedCardBarnSecondary=0;
		selectedCardGoldMine=0;
		selectedCardGoldMineSecondary=0;
		selectedCardGround = 0;
		selectedCardGroundSecondary = 0;
		selectedRecruiter=0;
		cardselectorCounter=0;
		clickOnBuilding=false;
		clickOn100="1";
		clickObj="0";
		levelUp.SetActive(false);
		limitBreak.SetActive(false);
		skill1Obj.SetActive(false);
		skill2Obj.SetActive(false);
		scrollWindows.SetActive(false);
		instant1.SetActive(false);
		instant2.SetActive(false);
		gemsClick.SetActive(false);
		gemsClick2.SetActive(false);
		researchLab.SetActive(false);
		researchCompleted.SetActive(false);
		updateBuilding.SetActive(false);
		updateCompleted.SetActive(false);
		warningPanel.SetActive(false);
		levelSelectionLayout.SetActive(false);
		sortPanelLVL.SetActive(false);
		sortPanel.SetActive(false);
		showGate.SetActive(false);
		TrainingInventory.SetActive(false);
		researchLaboraotry.SetActive(false);
		prisonInterogate.SetActive(false);
		Castle.SetActive(false);
		Storage.SetActive(false);
		Barn.SetActive(false);
		GoldMine.SetActive(false);
		TrainingGround.SetActive(false);
		Prison.SetActive(false);
		Laboratory.SetActive(false);
		if(menuScreen != null)
			menuScreen.SetActive (false);
		knowledgeTree.SetActive(false);
		showBarracks.SetActive(false);
		recruiterLayout.SetActive(false);
		buildingUpgradeLayout.SetActive(false);
		if(loadingScene.Instance != null)
			loadingScene.Instance.playerProfilePanel.SetActive(false);
		skillUp.SetActive(false);
		if(setting != null)
			setting.SetActive(false);
		soldierRecruit.SetActive(false);
		recruitmentUpdate.SetActive(false);
		Sort.SetActive(false);
		CaptiveButton.interactable=true;
		ArmyButton.interactable=false;
		confirmButton.interactable=false;

	}

	void OnDisable()
	{

		start();
	}

	void resetPrevious()
	{
		if(clickOnBuilding)
		{
			clickOnBuilding=false;
		}
		if(armyCaptive==false)
		{
			armyCaptive=true;
		}
		if(isUnlock==false)
		{
			isUnlock=true;
		}
		if(isInterrogate==true)
		{
			isInterrogate=false;
		}


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
	public void selectInterrogate(Button card)
	{

		if(interogation==0)
		{
			interroPlay=card;
		}

	}


	public void unlockLock()
	{
		if(isUnlock)
		{
			lockUnlock.GetComponentInChildren<Text>().text="Locked";
			isUnlock=false;
		}

		else
		{
			lockUnlock.GetComponentInChildren<Text>().text="Unlocked";
			isUnlock=true;

		}
	}
	public void armyCptive()
	{
		if(armyCaptive)
		{
			CaptiveButton.interactable=false;
			ArmyButton.interactable=true;
			armyCaptive=false;

		}
		else
		{
			CaptiveButton.interactable=true;
			ArmyButton.interactable=false;
			armyCaptive=true;

		}

	}

	public void onclickLevel()
	{
		empireSceneNew.instance.traningButtonClick="level";

		empireSceneNew.instance.offStatTraining();

		if (selectedCardGroundSecondary == 1) {
			SetSelectedCardDataInCardSelection ();
			selectedCards.Remove (lockButtonNameGroundSecondary);

			selectedCardGroundSecondary = 0;
			empireSceneNew.instance.offStatTraining();

		}

		Debug.Log ("sacrficeClickCardPos  = "+sacrficeLockedCard);
		int positionOfLockedCard = CardsManager._instance.PositionOfCardInList (sacrficeLockedCard);
		int expToSave = (CardsManager._instance.mycards[positionOfLockedCard].experience);
		string rarityXp = (CardsManager._instance.mycards[positionOfLockedCard].rarity.ToString());
		empireSceneNew.instance.startMaxLevel.text="Card Lv. "+CardsManager._instance.mycards[positionOfLockedCard].card_level.ToString();
		empireSceneNew.instance.endMaxLevel.text="Card Lv. "+(CardsManager._instance.mycards[positionOfLockedCard].card_level+1).ToString ();

		if (rarityXp == "Common")
		{
			print("Common");
			//expTimeInCardSelectionLayoutLvl.text =  (CardsManager._instance.starCard1Actual[CardsManager._instance.mycards[positionOfLockedCard].card_level-1].ToString());
			expStatMain.text=CardsManager._instance.mycards[positionOfLockedCard].experience.ToString()+"/"+CardsManager._instance.starCard1Actual[CardsManager._instance.mycards[positionOfLockedCard].card_level-1].ToString();


		}
		else if (rarityXp == "Uncommon")
		{
			print("Uncommon");
			//expTimeInCardSelectionLayoutLvl.text =  (CardsManager._instance.starCard2Actual[CardsManager._instance.mycards[positionOfLockedCard].card_level-1].ToString());
			expStatMain.text=CardsManager._instance.mycards[positionOfLockedCard].experience.ToString()+"/"+CardsManager._instance.starCard2Actual[CardsManager._instance.mycards[positionOfLockedCard].card_level-1].ToString();


		}
		else if (rarityXp == "Super")
		{
			print("Super");
			//expTimeInCardSelectionLayoutLvl.text =  (CardsManager._instance.starCard3Actual[CardsManager._instance.mycards[positionOfLockedCard].card_level-1].ToString());
			expStatMain.text=CardsManager._instance.mycards[positionOfLockedCard].experience.ToString()+"/"+CardsManager._instance.starCard3Actual[CardsManager._instance.mycards[positionOfLockedCard].card_level-1].ToString();


		}
		else if (rarityXp == "Mega")
		{
			print("Mega");
			//expTimeInCardSelectionLayoutLvl.text =  (CardsManager._instance.starCard4Actual[CardsManager._instance.mycards[positionOfLockedCard].card_level-1].ToString());
			expStatMain.text=CardsManager._instance.mycards[positionOfLockedCard].experience.ToString()+"/"+CardsManager._instance.starCard4Actual[CardsManager._instance.mycards[positionOfLockedCard].card_level-1].ToString();



		}
		else if (rarityXp == "Legendary")
		{
			print("Legendary");
			//expTimeInCardSelectionLayoutLvl.text =  (CardsManager._instance.starCard5Actual[CardsManager._instance.mycards[positionOfLockedCard].card_level-1].ToString());
			expStatMain.text=CardsManager._instance.mycards[positionOfLockedCard].experience.ToString()+"/"+CardsManager._instance.starCard5Actual[CardsManager._instance.mycards[positionOfLockedCard].card_level-1].ToString();


		}

		limitBreak.SetActive(false);
		skill1Obj.SetActive(false);
		levelUp.SetActive(true);
		wentFromSecondary = true;

		confirmTrainingBtn.interactable = false;
		GameObject.Find ("levelProfile").GetComponent<Image> ().sprite = empireSceneNew.instance.saveSpriteLocked;
		GameObject[] allCardButtons = GameObject.FindGameObjectsWithTag ("updateCards");

		for (int j = 0; j < allCardButtons.Length; j ++)
		{
			int cardNo = int.Parse (allCardButtons [j].name);

			if (CardsManager._instance.IsPlayercardLocked (cardNo, false)) {
				allCardButtons [j].GetComponent<Button> ().interactable = false;
				allCardButtons [j].GetComponentInChildren<Text>().text="LOCKED";
			} else {
				allCardButtons [j].GetComponent<Button> ().interactable = true;
				allCardButtons [j].GetComponentInChildren<Text>().text="";
			}


//			if(EmpireManager._instance.castle.primaryCardNo != cardNo && EmpireManager._instance.storage.primaryCardNo != cardNo && EmpireManager._instance.storage.secondaryCardNo != cardNo &&
//			   EmpireManager._instance.barn.primaryCardNo != cardNo && EmpireManager._instance.barn.secondaryCardNo != cardNo &&
//			   EmpireManager._instance.goldMine.primaryCardNo != cardNo && EmpireManager._instance.goldMine.secondaryCardNo != cardNo &&
//			   EmpireManager._instance.barracks.primaryCardNo != cardNo && EmpireManager._instance.barracks.secondaryCardNo != cardNo &&
//			   EmpireManager._instance.trainingGround.primaryCardNo != cardNo && EmpireManager._instance.trainingGround.secondaryCardNo != cardNo
//				&& EmpireManager._instance.prison.primaryCardNo != cardNo && EmpireManager._instance.prison.secondaryCardNo != cardNo
//				&& EmpireManager._instance.gate.primaryCardNo != cardNo && EmpireManager._instance.gate.secondaryCardNo != cardNo)
//			{
//				bool selectedInQuest = false;
//				for (int c = 0; c < myQuestingFormation.cardDecks.Count; c++) {
//					for(int  d= 0 ; d < myQuestingFormation.cardDecks[c].cardRows.Count ; d++)
//					{
//						for(int e = 0 ; e < myQuestingFormation.cardDecks[c].cardRows[d].cardIdsForRow.Count ; e++)
//						{
//							if(cardNo == myQuestingFormation.cardDecks[c].cardRows[d].cardIdsForRow[e])
//							{
//								selectedInQuest = true;
//								break;
//							}
//						}
//					}
//				}
//
//				if(!selectedInQuest)
//				{
//					allCardButtons [j].GetComponent<Button> ().interactable = true;
//					allCardButtons [j].GetComponentInChildren<Text>().text="";
//				}
//				else
//				{
//					allCardButtons [j].GetComponent<Button> ().interactable = false;
//					allCardButtons [j].GetComponentInChildren<Text>().text="LOCKED";
//				}
//			}
//			else
//			{
//				allCardButtons [j].GetComponent<Button> ().interactable = false;
//				allCardButtons [j].GetComponentInChildren<Text>().text="LOCKED";
//			}
		}

	}

	public void ExitLevel()
	{
		confirmButton.interactable = false;
		confirmTrainingBtn.interactable = false;

		empireSceneNew.instance.confirmButton.interactable=false;
		empireSceneNew.instance.confirmSkill.interactable=true;

		if (selectedCardGroundSecondary == 1) {
			
			SetSelectedCardDataInCardSelection();

//		lockButtonNameGroundSecondary.GetComponentInChildren<Text> ().text = "";
			selectedCards.Remove (lockButtonNameGroundSecondary);
			selectedCardGroundSecondary = 0;
			empireSceneNew.instance.offStatTraining();

		}
		EmpireManager._instance.trainingGround.secondaryCardNo = 0;
		lockButtonNameGroundSecondary = null;
		limitBreak.SetActive(false);
		skill1Obj.SetActive (false);
		levelUp.SetActive(false);
		backButtonSkillPage ();
		//CardsManager.CardParameters b = CardsManager._instance.mycards[sacrficeLockedCard];

		//GameObject.Find("lvlStat").GetComponent<Text> ().text="Lvl "+CardsManager._instance.mycards[sacrficeLockedCard].card_level.ToString();

	}




	public void onSort()
	{
		Sort.SetActive(true);
	}
	public void exitSort()
	{
		Sort.SetActive(false);
	}
	public void logOut()
	{
		onClickSettinExitg();
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
		menuScreen.SetActive(false);
		isMneuActive=false;
		setting.SetActive(true);
	}
	public void onClickSettinExitg()
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
	void deactivateBuildings(int index)
	{
		for(int i=0;i<empireBuldings.Length;i++)
		{
			if(i==index)
			{
				empireBuldings[i].SetActive(true);
			}
			else
			{
				empireBuldings[i].SetActive(false);
			}
		}
	}
	public void rightMoveBuilding()
	{
		clickObj="0";
		scrollButton.value+=.1f;

	}
	public void leftMoveBuilding()
	{
		clickObj="0";
		scrollButton.value-=.1f;

	}

	public void instantClick100()
	{
		clickOn100="2";
		instant1.SetActive(true);
		GameObject.Find("200Text").GetComponent<Text>().text="100";

	}
	public void instantClick()
	{

		if (buttonName == "castle")
		{
			if (PlayerParameters._instance.myPlayerParameter.gems < (int)loadingScene.Instance.castlePrimaryClockText / 59)
			{
				insufficientGems.SetActive (true);
			}
			else
			{

				instant1.SetActive (true);
				int gemsToDeduct = 	Mathf.FloorToInt(loadingScene.Instance.castlePrimaryClockText / 60);
				Debug.Log("Gems to deduct = "+ (gemsToDeduct) );
				loadingScene.Instance.instantGemsTextForPopup.text = gemsToDeduct.ToString ();

			}
		}


		if (buttonName == "storage")
		{
			if (PlayerParameters._instance.myPlayerParameter.gems < (int)loadingScene.Instance.storagePrimaryClockText / 59) {

				insufficientGems.SetActive (true);
			}
			else
			{
				instant1.SetActive (true);
				int gemsToDeduct = 	Mathf.FloorToInt(loadingScene.Instance.storagePrimaryClockText / 60);
				Debug.Log("Gems to deduct = "+ (gemsToDeduct) );
				loadingScene.Instance.instantGemsTextForPopup.text = gemsToDeduct.ToString ();

			}
		}
		if (buttonName == "barn")
		{
			if (PlayerParameters._instance.myPlayerParameter.gems < (int)loadingScene.Instance.barnPrimaryClockText / 59) {
				insufficientGems.SetActive (true);
			}
			else
			{
				instant1.SetActive (true);
				int gemsToDeduct = 	Mathf.FloorToInt(loadingScene.Instance.barnPrimaryClockText / 60);
				Debug.Log("Gems to deduct = "+ (gemsToDeduct) );
				loadingScene.Instance.instantGemsTextForPopup.text = gemsToDeduct.ToString ();
			}
		}

		if (buttonName == "goldMine")
		{
			if (PlayerParameters._instance.myPlayerParameter.gems < (int)loadingScene.Instance.barnPrimaryClockText / 59) {
				insufficientGems.SetActive (true);
			}
			else
			{
				instant1.SetActive (true);
				int gemsToDeduct = 	Mathf.FloorToInt(loadingScene.Instance.goldMinePrimaryClockText / 60);
				Debug.Log("Gems to deduct = "+ (gemsToDeduct) );
				loadingScene.Instance.instantGemsTextForPopup.text = gemsToDeduct.ToString ();

			}
		}

		if (buttonName == "barrack")
		{
			if (PlayerParameters._instance.myPlayerParameter.gems < (int)loadingScene.Instance.barrackPrimaryClockText / 59) {
				insufficientGems.SetActive (true);
			}
			else
			{
				instant1.SetActive (true);
				int gemsToDeduct = 	Mathf.FloorToInt(loadingScene.Instance.barrackPrimaryClockText / 60);
				Debug.Log("Gems to deduct = "+ (gemsToDeduct) );
				loadingScene.Instance.instantGemsTextForPopup.text = gemsToDeduct.ToString ();

			}
		}


		if (buttonName == "traningGround")
		{
			if (PlayerParameters._instance.myPlayerParameter.gems < (int)loadingScene.Instance.trainingGroundPrimaryClockText / 59) {

				insufficientGems.SetActive (true);
			}
			else
			{

				instant1.SetActive (true);
				int gemsToDeduct = 	Mathf.FloorToInt(loadingScene.Instance.trainingGroundPrimaryClockText / 60);
				Debug.Log("Gems to deduct = "+ (gemsToDeduct) );
				loadingScene.Instance.instantGemsTextForPopup.text = gemsToDeduct.ToString ();

			}
		}

		if (buttonName == "prison")
		{
			if (PlayerParameters._instance.myPlayerParameter.gems < (int)loadingScene.Instance.trainingGroundPrimaryClockText / 59) {

				insufficientGems.SetActive (true);
			}
			else
			{

				instant1.SetActive (true);
				int gemsToDeduct = 	Mathf.FloorToInt(loadingScene.Instance.trainingGroundPrimaryClockText / 60);
				Debug.Log("Gems to deduct = "+ (gemsToDeduct) );
				loadingScene.Instance.instantGemsTextForPopup.text = gemsToDeduct.ToString ();

			}
		}
		if (buttonName == "gate")
		{
			if (PlayerParameters._instance.myPlayerParameter.gems < (int)loadingScene.Instance.gatePrimaryClockText / 59) {

				insufficientGems.SetActive (true);
			}
			else
			{

				instant1.SetActive (true);
				int gemsToDeduct = 	Mathf.FloorToInt(loadingScene.Instance.gatePrimaryClockText / 60);
				Debug.Log("Gems to deduct = "+ (gemsToDeduct) );
				loadingScene.Instance.instantGemsTextForPopup.text = gemsToDeduct.ToString ();

			}
		}
	}
	public void disAbleinsufficientGems()
	{
		insufficientGems.SetActive(false);

	}
	public void instantYes()
	{

		if(buttonName=="castle")
		{

			loadingScene.Instance.instantUpdateCastle(true , PlayerParameters._instance.myPlayerParameter.gems- loadingScene.Instance.gemsToDeductOnInstantUpgrade , isSuccess =>{
				if(isSuccess)
				{
					PlayerParameters._instance.myPlayerParameter.gems-= loadingScene.Instance.gemsToDeductOnInstantUpgrade;
					GemsText[9].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();
					EmpireManager._instance.castle.chosenCardButton.interactable=true;
					EmpireManager._instance.castle.instantUpdateButton.interactable=false;
					instant1.SetActive(false);
					instant2.SetActive(true);
				}
			});
		}


		if(buttonName=="storage")
		{
			loadingScene.Instance.instantStorage(true , PlayerParameters._instance.myPlayerParameter.gems- loadingScene.Instance.gemsToDeductOnInstantUpgrade , isSuccess =>{
				if(isSuccess)
				{
					if(PlayerPrefs.GetString("tempButtonStorage")==tempButtonStorage)
					{

						lockButtonNameStorage.name = tempButtonStorage;
						lockButtonNameStorage.interactable=true;

					}
					PlayerParameters._instance.myPlayerParameter.gems -= loadingScene.Instance.gemsToDeductOnInstantUpgrade;
					GemsText[8].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();
					EmpireManager._instance.storage.chosenCardButton.interactable=true;
					EmpireManager._instance.storage.instantUpdateButton.interactable=false;
					instant1.SetActive(false);
					instant2.SetActive(true);
				}
			});

		}

		if(buttonName=="gate")
		{
			loadingScene.Instance.instantGate(true , PlayerParameters._instance.myPlayerParameter.gems- loadingScene.Instance.gemsToDeductOnInstantUpgrade , isSuccess =>{
				if(isSuccess)
				{
					if(PlayerPrefs.GetString("tempButtonGate")==tempButtonGate)
					{

						lockButtonNameGate.name = tempButtonGate;
						lockButtonNameGate.interactable=true;

					}
					PlayerParameters._instance.myPlayerParameter.gems -= loadingScene.Instance.gemsToDeductOnInstantUpgrade;
					GemsText[14].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();
					EmpireManager._instance.gate.chosenCardButton.interactable=true;
					EmpireManager._instance.gate.instantUpdateButton.interactable=false;
					instant1.SetActive(false);
					instant2.SetActive(true);
				}
			});

		}
		if(buttonName=="barn")
		{
			loadingScene.Instance.instantBarn(true , PlayerParameters._instance.myPlayerParameter.gems- loadingScene.Instance.gemsToDeductOnInstantUpgrade , isSuccess =>{
				if(isSuccess)
				{
					if(PlayerPrefs.GetString("tempButtonBarn")==tempButtonBarn)
					{

						lockButtonNameBarn.name = tempButtonBarn;
						lockButtonNameBarn.interactable=true;

					}
					PlayerParameters._instance.myPlayerParameter.gems -= loadingScene.Instance.gemsToDeductOnInstantUpgrade;
					GemsText[0].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();
					EmpireManager._instance.barn.chosenCardButton.interactable=true;
					EmpireManager._instance.barn.instantUpdateButton.interactable=false;
					instant1.SetActive(false);
					instant2.SetActive(true);
				}
			});
		}
		if(buttonName=="goldMine")
		{
			loadingScene.Instance.instantGoldMine(true , PlayerParameters._instance.myPlayerParameter.gems- loadingScene.Instance.gemsToDeductOnInstantUpgrade , isSuccess =>{
				if(isSuccess)
				{
					if(PlayerPrefs.GetString("tempButtonGoldMine")==tempButtonGoldMine)
					{

						lockButtonNameGoldMine.name = tempButtonGoldMine;
						lockButtonNameGoldMine.interactable=true;

					}
					PlayerParameters._instance.myPlayerParameter.gems -= loadingScene.Instance.gemsToDeductOnInstantUpgrade;
					GemsText[1].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();
					EmpireManager._instance.goldMine.chosenCardButton.interactable=true;
					EmpireManager._instance.goldMine.instantUpdateButton.interactable=false;
					instant1.SetActive(false);
					instant2.SetActive(true);
				}
			});
		}


		if(buttonName=="barrack")
		{
			print("=========  BARRRACK CLICK =========");
			loadingScene.Instance.instantBarrack(true , PlayerParameters._instance.myPlayerParameter.gems- loadingScene.Instance.gemsToDeductOnInstantUpgrade , isSuccess =>{
				if(isSuccess)
				{
					if(PlayerPrefs.GetString("tempButtonBarrack")==tempButtonBarrakPrimary)
					{

						lockButtonNameBarrakcs.name = tempButtonBarrakPrimary;
						lockButtonNameBarrakcs.interactable=true;

					}
					PlayerParameters._instance.myPlayerParameter.gems -= loadingScene.Instance.gemsToDeductOnInstantUpgrade;
					GemsText[3].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();
					EmpireManager._instance.barracks.chosenCardButton.interactable=true;
					EmpireManager._instance.barracks.instantUpdateButton.interactable=false;
					instant1.SetActive(false);
					instant2.SetActive(true);
				}
			});
		}

		if(buttonName=="traningGround")
		{
			print("=========  GROUND CLICK =========");
			loadingScene.Instance.instantUpdateTrainingGround(true , PlayerParameters._instance.myPlayerParameter.gems- loadingScene.Instance.gemsToDeductOnInstantUpgrade , isSuccess =>{
				if(isSuccess)
				{
					if(PlayerPrefs.GetString("tempButtonGround")==tempButtonGroundPrimary)
					{

						lockButtonNameGround.name = tempButtonGroundPrimary;
						lockButtonNameGround.interactable=true;

					}
					PlayerParameters._instance.myPlayerParameter.gems -= loadingScene.Instance.gemsToDeductOnInstantUpgrade;
					GemsText[4].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();
					GemsText[10].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();
					GemsText[11].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();
					GemsText[12].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();


					EmpireManager._instance.trainingGround.chosenCardButton.interactable=true;
					EmpireManager._instance.trainingGround.instantUpdateButton.interactable=false;
					instant1.SetActive(false);
					instant2.SetActive(true);
				}
			});
		}


		if(buttonName=="prison")
		{
			prisonObj.instance.prisonUpdateInstantToEmpire();
		}


	}
	public void exitInstant1()
	{
		clickOn100="1";
		instant1.SetActive(false);
	}
	public void exitInstant2()
	{
		clickOn100="1";

		instant2.SetActive(false);

	}

	public void destroyContent(GameObject contentlist)
	{

		Destroy(contentlist.gameObject);

	}

	public void onClickGems()
	{
		gemsClick.SetActive(true);
	}

	public void onClickGemsExit()
	{
		gemsClick.SetActive(false);
	}


	public void onClickGemsYes()
	{
		gemsClick.SetActive(false);
		gemsClick2.SetActive(true);
	}
	public void onClickGemsYesExit()
	{
		gemsClick2.SetActive(false);
	}

	public void recruitCardClick(Button recruiterCards)
	{
		if(selectedRecruiter==0)
		{
		recruiterCards.GetComponentInChildren<Text>().text="SELECTED";
		RECRUITCARD.Add(recruiterCards);
		}
		selectedRecruiter=1;
	}
	public void completeRecruitment()
	{
		for(int i=0;i<RECRUITCARD.Count;i++)
		{
			recruiterCards=RECRUITCARD[i];
			recruiterCards.interactable=false;
			recruiterCards.GetComponentInChildren<Text>().text="LOCKED";
		}
		recruitmentUpdate.SetActive(false);

	}

	public void recruitmentCompleteExit()
	{
		recruitmentUpdate.SetActive(false);
	}
	public void confirmRecruite()
	{
		recruitmentUpdate.SetActive(true);

	}

	public  void SetSelectedCardDataInCardSelection()
	{
		cardSelectedAttackInCardSelectionLayout.text = "";
		cardSelectedDefenseInCardSelectionLayout.text = "";
		cardSelectedLeadershipInCardSelectionLayout.text = "";
	}

	public void SetSelectedCardDataInCardSelection( CardsManager.CardParameters cardSelected)
	{

		cardSelectedAttackInCardSelectionLayout.text = cardSelected.attack.ToString ();
		cardSelectedDefenseInCardSelectionLayout.text = cardSelected.defense.ToString ();
		cardSelectedLeadershipInCardSelectionLayout.text = cardSelected.leadership.ToString ();

	}

	public void updateBuildingButtonCaptive(Button objClick)
	{
		if (selectedCardGroundSecondary == 0)
		{

			lockButtonNameGroundSecondary = objClick;
			confirmButton.interactable = true;
			confirmTrainingBtn.interactable = true;
			empireSceneNew.instance.confirmButton.interactable=true;
			empireSceneNew.instance.confirmSkill.interactable=true;
			lockButtonNameGroundSecondary.GetComponentInChildren<Text> ().text = "SELECTED";
			selectedCards.Add (lockButtonNameGroundSecondary);
			selectedCardGroundSecondary = 1;
			int cardIdLocked = CardsManager._instance.PositionOfCaptiveInList (int.Parse (objClick.name));
			clickforSkill =  CardsManager._instance.PositionOfCaptiveInList (int.Parse (objClick.name));
			SetSelectedCardDataInCardSelection(CardsManager._instance.myCaptives[cardIdLocked]);
			empireSceneNew.instance.showStatTraining();
		}
		else
		{
			confirmButton.interactable = false;
			confirmTrainingBtn.interactable = false;
			empireSceneNew.instance.confirmButton.interactable=false;
			empireSceneNew.instance.confirmSkill.interactable=false;
			lockButtonNameGroundSecondary.GetComponentInChildren<Text> ().text = "";
			selectedCards.Remove (lockButtonNameGroundSecondary);
			selectedCardGroundSecondary = 0;
			SetSelectedCardDataInCardSelection();
			empireSceneNew.instance.offStatTraining();
		}
	}
	public Button objbtn;
	public void updateBuildingButton(Button objClick)
	{
		objbtn = objClick;
		if (buttonName == "castle") {
			if (selectedCard == 0) {
				lockButtonName = objClick;
				confirmButton.interactable = true;
				lockButtonName.GetComponentInChildren<Text> ().text = "SELECTED";
				selectedCards.Add (lockButtonName);
				selectedCard = 1;
				int cardIdLocked = CardsManager._instance.PositionOfCardInList (int.Parse (objClick.name));
				SetSelectedCardDataInCardSelection (CardsManager._instance.mycards [cardIdLocked]);

			} else {
				confirmButton.interactable = false;
				lockButtonName.GetComponentInChildren<Text> ().text = "";
				selectedCards.Remove (lockButtonName);
				selectedCard = 0;
				SetSelectedCardDataInCardSelection ();

			}
		}
    else if (buttonName == "prison")
    {
    //  confirmButton.interactable = false;
      //prisonObj.instance.updateBuildingButtonEmpire();

      if(!wentFromSecondary)
      {
        if  (prisonObj.instance.selectedCardPrison == 0)
        {
          prisonObj.instance.lockButtonNamePrison  = objClick;
          confirmButton.interactable = true;
          prisonObj.instance.lockButtonNamePrison.GetComponentInChildren<Text> ().text = "SELECTED";
          selectedCards.Add (prisonObj.instance.lockButtonNamePrison);
          prisonObj.instance.selectedCardPrison = 1;
          int cardIdLocked = CardsManager._instance.PositionOfCardInList (int.Parse (objClick.name));
          SetSelectedCardDataInCardSelection(CardsManager._instance.mycards[cardIdLocked]);

        }
        else
        {
          confirmButton.interactable = false;
          prisonObj.instance.lockButtonNamePrison.GetComponentInChildren<Text> ().text = "";
          selectedCards.Remove (prisonObj.instance.lockButtonNamePrison);
          prisonObj.instance.selectedCardPrison = 0;
          SetSelectedCardDataInCardSelection();

        }
      }
      else
      {

        if (prisonObj.instance.selectedCardPrisonSecondary == 0)
        {
          prisonObj.instance.lockButtonNamePrisonSecondary = objClick;
          confirmButton.interactable = true;
          prisonObj.instance.lockButtonNamePrisonSecondary.GetComponentInChildren<Text> ().text = "SELECTED";

          selectedCards.Add (prisonObj.instance.lockButtonNamePrisonSecondary);
          prisonObj.instance.selectedCardPrisonSecondary = 1;
          int cardIdLocked = CardsManager._instance.PositionOfCardInList (int.Parse (objClick.name));
          SetSelectedCardDataInCardSelection(CardsManager._instance.mycards[cardIdLocked]);

        }
        else
        {
          confirmButton.interactable = false;
          prisonObj.instance.lockButtonNamePrisonSecondary.GetComponentInChildren<Text> ().text = "";
          selectedCards.Remove (prisonObj.instance. lockButtonNamePrisonSecondary);
          prisonObj.instance.selectedCardPrisonSecondary = 0;
          SetSelectedCardDataInCardSelection();

        }
      }



		}
		//-------- STORAGE ----
		else if (buttonName == "storage")
		{
			if(!wentFromSecondary)
			{
				if (selectedCardStorage == 0)
				{
					lockButtonNameStorage = objClick;
					confirmButton.interactable = true;
					lockButtonNameStorage.GetComponentInChildren<Text> ().text = "SELECTED";
					selectedCards.Add (lockButtonNameStorage);
					selectedCardStorage = 1;
					int cardIdLocked = CardsManager._instance.PositionOfCardInList (int.Parse (objClick.name));
					SetSelectedCardDataInCardSelection(CardsManager._instance.mycards[cardIdLocked]);

				}
				else
				{
					confirmButton.interactable = false;
					lockButtonNameStorage.GetComponentInChildren<Text> ().text = "";
					selectedCards.Remove (lockButtonNameStorage);
					selectedCardStorage = 0;
					SetSelectedCardDataInCardSelection();

				}
			}
			else
			{
				if (selectedCardStorageSecondary == 0)
				{
					lockButtonNameStorageSecondary = objClick;
					confirmButton.interactable = true;
					lockButtonNameStorageSecondary.GetComponentInChildren<Text> ().text = "SELECTED";
					selectedCards.Add (lockButtonNameStorageSecondary);
					selectedCardStorageSecondary = 1;
					int cardIdLocked = CardsManager._instance.PositionOfCardInList (int.Parse (objClick.name));
					SetSelectedCardDataInCardSelection(CardsManager._instance.mycards[cardIdLocked]);
				}
				else
				{
					confirmButton.interactable = false;
					lockButtonNameStorageSecondary.GetComponentInChildren<Text> ().text = "";
					selectedCards.Remove (lockButtonNameStorageSecondary);
					selectedCardStorageSecondary = 0;
					SetSelectedCardDataInCardSelection();

				}
			}
		}
		else if (buttonName == "gate")
		{
			if(!wentFromSecondary)
			{
				if (selectedCardGate == 0)
				{
					lockButtonNameGate = objClick;
					confirmButton.interactable = true;
					lockButtonNameGate.GetComponentInChildren<Text> ().text = "SELECTED";
					selectedCards.Add (lockButtonNameGate);
					selectedCardGate = 1;
					int cardIdLocked = CardsManager._instance.PositionOfCardInList (int.Parse (objClick.name));
					SetSelectedCardDataInCardSelection(CardsManager._instance.mycards[cardIdLocked]);

				}
				else
				{
					confirmButton.interactable = false;
					lockButtonNameGate.GetComponentInChildren<Text> ().text = "";
					selectedCards.Remove (lockButtonNameGate);
					selectedCardGate = 0;
					SetSelectedCardDataInCardSelection();

				}
			}
			else
			{
				if (selectedCardGateSecondary == 0)
				{
					lockButtonNameGateSecondary = objClick;
					confirmButton.interactable = true;
					lockButtonNameGateSecondary.GetComponentInChildren<Text> ().text = "SELECTED";
					selectedCards.Add (lockButtonNameGateSecondary);
					selectedCardGateSecondary = 1;
					int cardIdLocked = CardsManager._instance.PositionOfCardInList (int.Parse (objClick.name));
					SetSelectedCardDataInCardSelection(CardsManager._instance.mycards[cardIdLocked]);
				}
				else
				{
					confirmButton.interactable = false;
					lockButtonNameGateSecondary.GetComponentInChildren<Text> ().text = "";
					selectedCards.Remove (lockButtonNameGateSecondary);
					selectedCardGateSecondary = 0;
					SetSelectedCardDataInCardSelection();

				}
			}
		}
		//=====================
		else if (buttonName == "barn")
		{
			if(!wentFromSecondary)
			{
				if (selectedCardBarn == 0)
				{
					lockButtonNameBarn = objClick;
					confirmButton.interactable = true;
					lockButtonNameBarn.GetComponentInChildren<Text> ().text = "SELECTED";
					selectedCards.Add (lockButtonNameBarn);
					selectedCardBarn = 1;
					int cardIdLocked = CardsManager._instance.PositionOfCardInList (int.Parse (objClick.name));
					SetSelectedCardDataInCardSelection(CardsManager._instance.mycards[cardIdLocked]);
				}
				else
				{
					confirmButton.interactable = false;
					lockButtonNameBarn.GetComponentInChildren<Text> ().text = "";
					selectedCards.Remove (lockButtonNameBarn);
					selectedCardBarn = 0;
					SetSelectedCardDataInCardSelection();

				}
			}
			else
			{
				if (selectedCardBarnSecondary == 0)
				{
					lockButtonNameBarnSecondary = objClick;
					confirmButton.interactable = true;
					lockButtonNameBarnSecondary.GetComponentInChildren<Text> ().text = "SELECTED";
					selectedCards.Add (lockButtonNameBarnSecondary);
					selectedCardBarnSecondary = 1;
					int cardIdLocked = CardsManager._instance.PositionOfCardInList (int.Parse (objClick.name));
					SetSelectedCardDataInCardSelection(CardsManager._instance.mycards[cardIdLocked]);
				}
				else
				{
					confirmButton.interactable = false;
					lockButtonNameBarnSecondary.GetComponentInChildren<Text> ().text = "";
					selectedCards.Remove (lockButtonNameBarnSecondary);
					selectedCardBarnSecondary = 0;
					SetSelectedCardDataInCardSelection();

				}
			}
		}

		//-----





		//---------------------------
		else if (buttonName == "goldMine")
		{
			if(!wentFromSecondary)
			{
				if (selectedCardGoldMine == 0)
				{
					lockButtonNameGoldMine = objClick;
					confirmButton.interactable = true;
					lockButtonNameGoldMine.GetComponentInChildren<Text> ().text = "SELECTED";
					selectedCards.Add (lockButtonNameGoldMine);
					selectedCardGoldMine = 1;
					int cardIdLocked = CardsManager._instance.PositionOfCardInList (int.Parse (objClick.name));
					SetSelectedCardDataInCardSelection(CardsManager._instance.mycards[cardIdLocked]);

				}
				else
				{
					confirmButton.interactable = false;
					lockButtonNameGoldMine.GetComponentInChildren<Text> ().text = "";
					selectedCards.Remove (lockButtonNameGoldMine);
					selectedCardGoldMine = 0;
					SetSelectedCardDataInCardSelection();

				}
			}
			else
			{
				if (selectedCardGoldMineSecondary == 0)
				{
					lockButtonGoldMineSecondary = objClick;
					confirmButton.interactable = true;
					lockButtonGoldMineSecondary.GetComponentInChildren<Text> ().text = "SELECTED";
					selectedCards.Add (lockButtonGoldMineSecondary);
					selectedCardGoldMineSecondary = 1;
					int cardIdLocked = CardsManager._instance.PositionOfCardInList (int.Parse (objClick.name));
					SetSelectedCardDataInCardSelection(CardsManager._instance.mycards[cardIdLocked]);
				}
				else
				{
					confirmButton.interactable = false;
					lockButtonGoldMineSecondary.GetComponentInChildren<Text> ().text = "";
					selectedCards.Remove (lockButtonGoldMineSecondary);
					selectedCardGoldMineSecondary = 0;
					SetSelectedCardDataInCardSelection();

				}
			}
		}



		//---------------------------
		else if (buttonName == "barrack")
		{
			if(!wentFromSecondary)
			{
				if (selectedCardBarrakcs == 0)
				{
					lockButtonNameBarrakcs = objClick;
					confirmButton.interactable = true;
					lockButtonNameBarrakcs.GetComponentInChildren<Text> ().text = "SELECTED";
					selectedCards.Add (lockButtonNameBarrakcs);
					selectedCardBarrakcs = 1;
					int cardIdLocked = CardsManager._instance.PositionOfCardInList (int.Parse (objClick.name));
					SetSelectedCardDataInCardSelection(CardsManager._instance.mycards[cardIdLocked]);

				}
				else
				{
					confirmButton.interactable = false;
					lockButtonNameBarrakcs.GetComponentInChildren<Text> ().text = "";
					selectedCards.Remove (lockButtonNameBarrakcs);
					selectedCardBarrakcs = 0;
					SetSelectedCardDataInCardSelection();


				}
			}
			else
			{
				if (selectedCardBarrakcsSecondary == 0)
				{
					lockButtonBarrakcsSecondary = objClick;
					confirmButton.interactable = true;
					lockButtonBarrakcsSecondary.GetComponentInChildren<Text> ().text = "SELECTED";
					selectedCards.Add (lockButtonBarrakcsSecondary);
					selectedCardBarrakcsSecondary = 1;
					int cardIdLocked = CardsManager._instance.PositionOfCardInList (int.Parse (objClick.name));
					SetSelectedCardDataInCardSelection(CardsManager._instance.mycards[cardIdLocked]);
				}
				else
				{
					confirmButton.interactable = false;
					lockButtonBarrakcsSecondary.GetComponentInChildren<Text> ().text = "";
					selectedCards.Remove (lockButtonBarrakcsSecondary);
					selectedCardBarrakcsSecondary = 0;
					SetSelectedCardDataInCardSelection();

				}
			}
		}


		//---------------------------
		else if (buttonName == "traningGround")
		{
			if(!wentFromSecondary)
			{
				if (selectedCardGround == 0)
				{
					lockButtonNameGround = objClick;
					confirmButton.interactable = true;
					confirmTrainingBtn.interactable = true;
					empireSceneNew.instance.confirmButton.interactable=true;
					empireSceneNew.instance.confirmSkill.interactable=true;

					empireSceneNew.instance.confirmButton.interactable=true;
					lockButtonNameGround.GetComponentInChildren<Text> ().text = "SELECTED";
					selectedCards.Add (lockButtonNameGround);
					selectedCardGround = 1;
					int cardIdLocked = CardsManager._instance.PositionOfCardInList (int.Parse (objClick.name));
					SetSelectedCardDataInCardSelection(CardsManager._instance.mycards[cardIdLocked]);

				}
				else
				{
					confirmButton.interactable = false;
					confirmTrainingBtn.interactable = false;
					empireSceneNew.instance.confirmButton.interactable=false;
					empireSceneNew.instance.confirmSkill.interactable=false;

					lockButtonNameGround.GetComponentInChildren<Text> ().text = "";
					selectedCards.Remove (lockButtonNameGround);
					selectedCardGround = 0;
					SetSelectedCardDataInCardSelection();

				}
			}
			else
			{
				if (selectedCardGroundSecondary == 0)
				{
					lockButtonNameGroundSecondary = objClick;
					confirmButton.interactable = true;
					confirmTrainingBtn.interactable = true;
					empireSceneNew.instance.confirmButton.interactable=true;
					empireSceneNew.instance.confirmSkill.interactable=true;

					lockButtonNameGroundSecondary.GetComponentInChildren<Text> ().text = "SELECTED";

					selectedCards.Add (lockButtonNameGroundSecondary);
					selectedCardGroundSecondary = 1;
					int cardIdLocked = CardsManager._instance.PositionOfCardInList (int.Parse (objClick.name));
					clickforSkill =  CardsManager._instance.PositionOfCardInList (int.Parse (objClick.name));

					SetSelectedCardDataInCardSelection(CardsManager._instance.mycards[cardIdLocked]);
          if((GameObject.Find("LimitBreak"))||(GameObject.Find("LevelUp"))||(GameObject.Find("Skill1")))
          {
            empireSceneNew.instance.showStatTraining();

          }



				}
				else
				{
					confirmButton.interactable = false;
					confirmTrainingBtn.interactable = false;
					empireSceneNew.instance.confirmButton.interactable=false;
					empireSceneNew.instance.confirmSkill.interactable=false;

					lockButtonNameGroundSecondary.GetComponentInChildren<Text> ().text = "";
					selectedCards.Remove (lockButtonNameGroundSecondary);
					selectedCardGroundSecondary = 0;
					SetSelectedCardDataInCardSelection();
          if((GameObject.Find("LimitBreak"))||(GameObject.Find("LevelUp"))||(GameObject.Find("Skill1")))
          {
            empireSceneNew.instance.offStatTraining();

          }



				}
			}
		}


	}
	public int  clickforSkill;



	public void updateBuildingButtonExit()
	{
		updateBuilding.SetActive(false);
	}
	public void confirmButtn()
	{

		updateBuilding.SetActive(true);
    if((buttonName=="prison") &&(empireSceneNew.instance.buttonNameSecond=="prisonSecond"))
    {
      updateBuilding.transform.GetChild(7).GetComponent<Text>().text="Are you sure to use this card as a Interrogator?";
    }
    else
    {
      updateBuilding.transform.GetChild(7).GetComponent<Text>().text="Are you sure to use this card for upgrade?";
    }

	}


	public void chosenCard()
	{
		if (loadingScene.Instance.randomCards.Count > 0)
		{
			if (buttonName == "castle") {


				randomLoad = (int)UnityEngine.Random.Range (0, loadingScene.Instance.randomCards.Count);
				EmpireManager._instance.castle.primaryCardNo = loadingScene.Instance.randomCards [randomLoad];
				int wheatTosend = PlayerParameters._instance.myPlayerParameter.wheat - EmpireManager._instance.castle.foodRequiredForPrimary [EmpireManager._instance.castle.currentLevel];
				int goldTosend = PlayerParameters._instance.myPlayerParameter.gold - EmpireManager._instance.castle.foodRequiredForPrimary [EmpireManager._instance.castle.currentLevel];

				createCard (buttonName, EmpireManager._instance.castle.currentLevel, EmpireManager._instance.castle.primaryCardNo, EmpireManager._instance.castle.primaryCardNo.ToString (), EmpireManager._instance.castle.currentExp, (EmpireManager._instance.castle.timeRequiredPerLevel [EmpireManager._instance.castle.currentLevel] * 60).ToString (), wheatTosend.ToString (), goldTosend.ToString (),   isSuccess => {

					if (isSuccess) {
						loadingScene.Instance.castlePrimaryClockText = EmpireManager._instance.castle.timeRequiredPerLevel [EmpireManager._instance.castle.currentLevel] * 3600f;
						EmpireManager._instance.castle.chosenCardButton.interactable = false;
						int spriteToFetch = CardsManager._instance.PositionOfCardInList (loadingScene.Instance.randomCards [randomLoad]);
						loadingScene.Instance.castlePrimaryImage.sprite = CardsManager._instance.mycards [spriteToFetch].cardSpriteFromResources;
						PlayerPrefs.SetString ("tempButton", loadingScene.Instance.randomCards [randomLoad].ToString ());
						PlayerParameters._instance.myPlayerParameter.wheat -= EmpireManager._instance.castle.foodRequiredForPrimary [EmpireManager._instance.castle.currentLevel];
						PlayerParameters._instance.myPlayerParameter.gold -= EmpireManager._instance.castle.foodRequiredForPrimary [EmpireManager._instance.castle.currentLevel];
						EmpireManager._instance.castle.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString();
						EmpireManager._instance.castle.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString();
						PlayerPrefs.SetString ("chosenCardCastle", "yes");
						PlayerPrefs.SetString ("updating", "yes");
						loadingScene.Instance.randomCards.Remove (EmpireManager._instance.castle.primaryCardNo);
						loadingScene.Instance.isCastlePrimary = true;
					} else {
						EmpireManager._instance.castle.primaryCardNo = -1;
						popupFromServer.ShowPopup ("Cannot lock the card at this time!");
					}
				});


			}

			if (buttonName == "storage") {
				if (EmpireManager._instance.storage.castleLevelRequired [EmpireManager._instance.storage.currentLevel] <= EmpireManager._instance.castle.currentLevel + 1) {
					randomLoadStorage = (int)UnityEngine.Random.Range (0, loadingScene.Instance.randomCards.Count);
					EmpireManager._instance.storage.primaryCardNo = loadingScene.Instance.randomCards [randomLoadStorage];
					int wheatTosend = PlayerParameters._instance.myPlayerParameter.wheat - EmpireManager._instance.storage.foodRequiredForPrimary [EmpireManager._instance.storage.currentLevel];
					int goldTosend = PlayerParameters._instance.myPlayerParameter.gold - EmpireManager._instance.storage.foodRequiredForPrimary [EmpireManager._instance.storage.currentLevel];

					createCard (buttonName, EmpireManager._instance.storage.currentLevel, EmpireManager._instance.storage.primaryCardNo, EmpireManager._instance.storage.primaryCardNo.ToString (), EmpireManager._instance.storage.currentExp, (EmpireManager._instance.storage.timeRequiredPerLevel [EmpireManager._instance.castle.currentLevel] * 60).ToString (), wheatTosend.ToString (),goldTosend.ToString (), isSuccess => {
						if (isSuccess) {
							loadingScene.Instance.storagePrimaryClockText = EmpireManager._instance.storage.timeRequiredPerLevel [EmpireManager._instance.storage.currentLevel] * 3600f;
							EmpireManager._instance.storage.chosenCardButton.interactable = false;
							int spriteToFetch = CardsManager._instance.PositionOfCardInList (loadingScene.Instance.randomCards [randomLoadStorage]);
							loadingScene.Instance.storagePrimaryImage.sprite = CardsManager._instance.mycards [spriteToFetch].cardSpriteFromResources;
							PlayerPrefs.SetString ("tempButtonStorage", loadingScene.Instance.randomCards [randomLoadStorage].ToString ());
							PlayerParameters._instance.myPlayerParameter.wheat -= EmpireManager._instance.storage.foodRequiredForPrimary [EmpireManager._instance.storage.currentLevel];
							PlayerParameters._instance.myPlayerParameter.gold -= EmpireManager._instance.storage.foodRequiredForPrimary [EmpireManager._instance.storage.currentLevel];
							EmpireManager._instance.storage.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString();
							EmpireManager._instance.storage.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString();
							PlayerPrefs.SetString ("chosenCardStorage", "yes");
							PlayerPrefs.SetString ("updatingStorage", "yes");
							loadingScene.Instance.randomCards.Remove (EmpireManager._instance.storage.primaryCardNo);
							loadingScene.Instance.isStoragePrimary = true;
						} else {
							EmpireManager._instance.storage.primaryCardNo = -1;
							popupFromServer.ShowPopup ("Cannot lock the card at this time!");
						}
					});
				} else {
					insufficientGems.SetActive (true);
					insufficientGems.transform.GetChild (4).GetComponent<Text> ().text = "First Update Castle to Proceed !";

				}
			}


			if (buttonName == "gate") 
			{
				if (EmpireManager._instance.gate.castleLevelRequired [EmpireManager._instance.gate.currentLevel] <= EmpireManager._instance.castle.currentLevel + 1) {
					randomLoadGate = (int)UnityEngine.Random.Range (0, loadingScene.Instance.randomCards.Count);
					EmpireManager._instance.gate.primaryCardNo = loadingScene.Instance.randomCards [randomLoadGate];
					int wheatTosend = PlayerParameters._instance.myPlayerParameter.wheat - EmpireManager._instance.gate.foodRequiredForPrimary [EmpireManager._instance.gate.currentLevel];
					int goldTosend = PlayerParameters._instance.myPlayerParameter.gold - EmpireManager._instance.gate.foodRequiredForPrimary [EmpireManager._instance.gate.currentLevel];

					createCard (buttonName, EmpireManager._instance.gate.currentLevel, EmpireManager._instance.gate.primaryCardNo, EmpireManager._instance.gate.primaryCardNo.ToString (), EmpireManager._instance.gate.currentExp, (EmpireManager._instance.gate.timeRequiredPerLevel [EmpireManager._instance.castle.currentLevel] * 60).ToString (), wheatTosend.ToString (),goldTosend.ToString (), isSuccess => {
						if (isSuccess) {
							loadingScene.Instance.gatePrimaryClockText = EmpireManager._instance.gate.timeRequiredPerLevel [EmpireManager._instance.gate.currentLevel] * 3600f;
							EmpireManager._instance.gate.chosenCardButton.interactable = false;
							int spriteToFetch = CardsManager._instance.PositionOfCardInList (loadingScene.Instance.randomCards [randomLoadGate]);
							loadingScene.Instance.gatePrimaryImage.sprite = CardsManager._instance.mycards [spriteToFetch].cardSpriteFromResources;
							PlayerPrefs.SetString ("tempButtonGate", loadingScene.Instance.randomCards [randomLoadGate].ToString ());
							PlayerParameters._instance.myPlayerParameter.wheat -= EmpireManager._instance.gate.foodRequiredForPrimary [EmpireManager._instance.gate.currentLevel];
							PlayerParameters._instance.myPlayerParameter.gold -= EmpireManager._instance.gate.foodRequiredForPrimary [EmpireManager._instance.gate.currentLevel];
							EmpireManager._instance.gate.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString();
							EmpireManager._instance.gate.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString();
							PlayerPrefs.SetString ("chosenCardGate", "yes");
							PlayerPrefs.SetString ("updatingGate", "yes");
							loadingScene.Instance.randomCards.Remove (EmpireManager._instance.gate.primaryCardNo);
							loadingScene.Instance.isGatePrimary = true;
						} else {
							EmpireManager._instance.gate.primaryCardNo = -1;
							popupFromServer.ShowPopup ("Cannot lock the card at this time!");
						}
					});
				} else {
					insufficientGems.SetActive (true);
					insufficientGems.transform.GetChild (4).GetComponent<Text> ().text = "First Update Castle to Proceed !";

				}
			}


			if (buttonName == "barn")
			{
				if (EmpireManager._instance.barn.castleLevelRequired [EmpireManager._instance.barn.currentLevel] <= EmpireManager._instance.castle.currentLevel + 1) {
					randomLoadBarn = (int)UnityEngine.Random.Range (0, loadingScene.Instance.randomCards.Count);
					EmpireManager._instance.barn.primaryCardNo = loadingScene.Instance.randomCards [randomLoadBarn];
					int wheatTosend = PlayerParameters._instance.myPlayerParameter.wheat ;
					int goldTosend = PlayerParameters._instance.myPlayerParameter.gold - EmpireManager._instance.storage.foodRequiredForPrimary [EmpireManager._instance.storage.currentLevel];

					createCard (buttonName, EmpireManager._instance.barn.currentLevel, EmpireManager._instance.barn.primaryCardNo, EmpireManager._instance.barn.primaryCardNo.ToString (), EmpireManager._instance.barn.currentExp, (EmpireManager._instance.barn.timeRequiredPerLevel [EmpireManager._instance.barn.currentLevel] * 60).ToString (), wheatTosend.ToString (), goldTosend.ToString (), isSuccess => {
						if (isSuccess) {
							loadingScene.Instance.barnPrimaryClockText = EmpireManager._instance.barn.timeRequiredPerLevel [EmpireManager._instance.barn.currentLevel] * 3600f;
							EmpireManager._instance.barn.chosenCardButton.interactable = false;
							PlayerParameters._instance.myPlayerParameter.gold -= EmpireManager._instance.barn.foodRequiredForPrimary [EmpireManager._instance.barn.currentLevel];
							EmpireManager._instance.barn.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString();
							int spriteToFetch = CardsManager._instance.PositionOfCardInList (loadingScene.Instance.randomCards [randomLoadBarn]);
							loadingScene.Instance.barnPrimaryImage.sprite = CardsManager._instance.mycards [spriteToFetch].cardSpriteFromResources;
							PlayerPrefs.SetString ("tempButtonBarn", loadingScene.Instance.randomCards [randomLoadBarn].ToString ());
							PlayerPrefs.SetString ("chosenCardBarn", "yes");
							print ("print" + loadingScene.Instance.barnPrimaryImage.sprite);
							PlayerPrefs.SetString ("updatingBarn", "yes");
							loadingScene.Instance.randomCards.Remove (EmpireManager._instance.barn.primaryCardNo);
							loadingScene.Instance.isBarnPrimary = true;
						} else {
							EmpireManager._instance.barn.primaryCardNo = -1;
							popupFromServer.ShowPopup ("Cannot lock the card at this time!");
						}
					});
				} else {
					insufficientGems.SetActive (true);
					insufficientGems.transform.GetChild (4).GetComponent<Text> ().text = "First Update Castle to Proceed !";
				}
			}
			if (buttonName == "barrack")
			{
				if (EmpireManager._instance.barracks.castleLevelRequired [EmpireManager._instance.barracks.currentLevel] <= EmpireManager._instance.castle.currentLevel + 1) {
					randomLoadBarracks = (int)UnityEngine.Random.Range (0, loadingScene.Instance.randomCards.Count);
					EmpireManager._instance.barracks.primaryCardNo = loadingScene.Instance.randomCards [randomLoadBarracks];
					int wheatTosend = PlayerParameters._instance.myPlayerParameter.wheat - EmpireManager._instance.barracks.foodRequiredForPrimary [EmpireManager._instance.barracks.currentLevel];;
					int goldTosend = PlayerParameters._instance.myPlayerParameter.gold - EmpireManager._instance.barracks.foodRequiredForPrimary [EmpireManager._instance.barracks.currentLevel];

					createCard (buttonName, EmpireManager._instance.barracks.currentLevel, EmpireManager._instance.barracks.primaryCardNo, EmpireManager._instance.barracks.primaryCardNo.ToString (), EmpireManager._instance.barracks.currentExp, (EmpireManager._instance.barracks.timeRequiredPerLevel [EmpireManager._instance.barracks.currentLevel] * 60).ToString (), wheatTosend.ToString (), goldTosend.ToString (), isSuccess => {
						if (isSuccess) {
							loadingScene.Instance.barrackPrimaryClockText = EmpireManager._instance.barracks.timeRequiredPerLevel [EmpireManager._instance.barracks.currentLevel] * 3600f;
							EmpireManager._instance.barracks.chosenCardButton.interactable = false;
							PlayerParameters._instance.myPlayerParameter.gold -= EmpireManager._instance.barracks.foodRequiredForPrimary [EmpireManager._instance.barracks.currentLevel];
							EmpireManager._instance.barracks.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString();
							PlayerParameters._instance.myPlayerParameter.wheat -= EmpireManager._instance.barracks.foodRequiredForPrimary [EmpireManager._instance.barracks.currentLevel];
							EmpireManager._instance.barracks.foodText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString();
							int spriteToFetch = CardsManager._instance.PositionOfCardInList (loadingScene.Instance.randomCards [randomLoadBarracks]);
							loadingScene.Instance.barrackPrimaryImage.sprite = CardsManager._instance.mycards [spriteToFetch].cardSpriteFromResources;
							PlayerPrefs.SetString ("tempButtonBarrack", loadingScene.Instance.randomCards [randomLoadBarracks].ToString ());
							PlayerPrefs.SetString ("chosenCardBarrack", "yes");
							PlayerPrefs.SetString ("updatingBarrack", "yes");
							loadingScene.Instance.randomCards.Remove (EmpireManager._instance.barracks.primaryCardNo);
							loadingScene.Instance.isBarrackPrimary = true;
						} else {
							EmpireManager._instance.barracks.primaryCardNo = -1;
							popupFromServer.ShowPopup ("Cannot lock the card at this time!");
						}
					});
				} else {
					insufficientGems.SetActive (true);
					insufficientGems.transform.GetChild (4).GetComponent<Text> ().text = "First Update Castle to Proceed !";
				}
			}

			if (buttonName == "goldMine")
			{
				if (EmpireManager._instance.goldMine.castleLevelRequired [EmpireManager._instance.goldMine.currentLevel] <= EmpireManager._instance.castle.currentLevel + 1) {
					randomLoadGoldMine = (int)UnityEngine.Random.Range (0, loadingScene.Instance.randomCards.Count);
					EmpireManager._instance.goldMine.primaryCardNo = loadingScene.Instance.randomCards [randomLoadGoldMine];
					int wheatTosend = PlayerParameters._instance.myPlayerParameter.wheat - EmpireManager._instance.goldMine.foodRequiredForPrimary [EmpireManager._instance.storage.currentLevel];
					int goldTosend = PlayerParameters._instance.myPlayerParameter.gold;
					createCard (buttonName, EmpireManager._instance.goldMine.currentLevel, EmpireManager._instance.goldMine.primaryCardNo, EmpireManager._instance.goldMine.primaryCardNo.ToString (), EmpireManager._instance.goldMine.currentExp, (EmpireManager._instance.goldMine.timeRequiredPerLevel [EmpireManager._instance.goldMine.currentLevel] * 60).ToString (),wheatTosend.ToString (), goldTosend.ToString (), isSuccess => {
						if (isSuccess) {
							loadingScene.Instance.goldMinePrimaryClockText = EmpireManager._instance.goldMine.timeRequiredPerLevel [EmpireManager._instance.goldMine.currentLevel] * 3600f;
							EmpireManager._instance.goldMine.chosenCardButton.interactable = false;
							PlayerParameters._instance.myPlayerParameter.wheat -= EmpireManager._instance.goldMine.foodRequiredForPrimary [EmpireManager._instance.goldMine.currentLevel];
							EmpireManager._instance.goldMine.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString();
							int spriteToFetch = CardsManager._instance.PositionOfCardInList (loadingScene.Instance.randomCards [randomLoadGoldMine]);
							loadingScene.Instance.goldMinePrimaryImage.sprite = CardsManager._instance.mycards [spriteToFetch].cardSpriteFromResources;
							PlayerPrefs.SetString ("tempButtonGoldMine", loadingScene.Instance.randomCards [randomLoadGoldMine].ToString ());
							PlayerPrefs.SetString ("chosenCardGoldMine", "yes");
							PlayerPrefs.SetString ("updatingGoldMine", "yes");
							loadingScene.Instance.randomCards.Remove (EmpireManager._instance.goldMine.primaryCardNo);
							loadingScene.Instance.isgoldMinePrimary = true;
						} else {
							EmpireManager._instance.goldMine.primaryCardNo = -1;
							popupFromServer.ShowPopup ("Cannot lock the card at this time!");
						}
					});
				} else {
					insufficientGems.SetActive (true);
					insufficientGems.transform.GetChild (4).GetComponent<Text> ().text = "First Update Castle to Proceed !";
				}
			}

			if (buttonName == "traningGround")
			{
				if (EmpireManager._instance.trainingGround.castleLevelRequired [EmpireManager._instance.trainingGround.currentLevel] <= EmpireManager._instance.castle.currentLevel + 1) {
					randomLoadGround = (int)UnityEngine.Random.Range (0, loadingScene.Instance.randomCards.Count);
					EmpireManager._instance.trainingGround.primaryCardNo = loadingScene.Instance.randomCards [randomLoadGround];
					int wheatTosend = PlayerParameters._instance.myPlayerParameter.wheat - EmpireManager._instance.trainingGround.foodRequiredForPrimary [EmpireManager._instance.trainingGround.currentLevel];
					int goldTosend = PlayerParameters._instance.myPlayerParameter.gold;
					createCard (buttonName, EmpireManager._instance.trainingGround.currentLevel, EmpireManager._instance.trainingGround.primaryCardNo, EmpireManager._instance.trainingGround.primaryCardNo.ToString (), EmpireManager._instance.trainingGround.currentExp, (EmpireManager._instance.trainingGround.timeRequiredPerLevel [EmpireManager._instance.trainingGround.currentLevel] * 60).ToString (),wheatTosend.ToString (), goldTosend.ToString (), isSuccess => {
						if (isSuccess) {
							loadingScene.Instance.trainingGroundPrimaryClockText = EmpireManager._instance.trainingGround.timeRequiredPerLevel [EmpireManager._instance.trainingGround.currentLevel] * 3600f;
							EmpireManager._instance.trainingGround.chosenCardButton.interactable = false;
							PlayerParameters._instance.myPlayerParameter.wheat -= EmpireManager._instance.trainingGround.foodRequiredForPrimary [EmpireManager._instance.trainingGround.currentLevel];
							EmpireManager._instance.trainingGround.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString();
							int spriteToFetch = CardsManager._instance.PositionOfCardInList (loadingScene.Instance.randomCards [randomLoadGround]);
							loadingScene.Instance.trainingGroundPrimaryImage.sprite = CardsManager._instance.mycards [spriteToFetch].cardSpriteFromResources;
							PlayerPrefs.SetString ("tempButtonGround", loadingScene.Instance.randomCards [randomLoadGround].ToString ());
							PlayerPrefs.SetString ("chosenCardGround", "yes");
							PlayerPrefs.SetString ("updatingGround", "yes");
							loadingScene.Instance.randomCards.Remove (EmpireManager._instance.trainingGround.primaryCardNo);
							loadingScene.Instance.istrainingGroundPrimary = true;
						}
						else
						{
							EmpireManager._instance.trainingGround.primaryCardNo = -1;
							popupFromServer.ShowPopup ("Cannot lock the card at this time!");
						}
					});
				} else {
					insufficientGems.SetActive (true);
					insufficientGems.transform.GetChild (4).GetComponent<Text> ().text = "First Update Castle to Proceed !";
				}
			}
			if (buttonName == "prison")
			{
				prisonObj.instance.prisonChosenCard();
			}

		}
		else
		{
			insufficientGems.SetActive (true);
			insufficientGems.transform.GetChild (4).GetComponent<Text> ().text = "All cards are locked";
		}

	}

	public  bool wentFromSecondary;

	public void completeUpdate()
	{

		if (buttonName == "castle") 
		{

			EmpireManager._instance.castle.primaryCardNo = int.Parse (lockButtonName.name);
			int wheatTosend = PlayerParameters._instance.myPlayerParameter.wheat - EmpireManager._instance.castle.foodRequiredForPrimary [EmpireManager._instance.castle.currentLevel];
			int goldTosend = PlayerParameters._instance.myPlayerParameter.gold - EmpireManager._instance.castle.foodRequiredForPrimary [EmpireManager._instance.castle.currentLevel];

			createCard (buttonName, EmpireManager._instance.castle.currentLevel, EmpireManager._instance.castle.primaryCardNo, EmpireManager._instance.castle.primaryCardNo.ToString (), EmpireManager._instance.castle.currentExp, (EmpireManager._instance.castle.timeRequiredPerLevel [EmpireManager._instance.castle.currentLevel] * 60).ToString (), wheatTosend.ToString (), goldTosend.ToString (), isSuccess => {
				if (isSuccess) {
					loadingScene.Instance.castlePrimaryImage.sprite = lockButtonName.GetComponent<Image> ().sprite;
					loadingScene.Instance.randomCards.Remove (int.Parse (lockButtonName.name));
					PlayerParameters._instance.myPlayerParameter.wheat -= EmpireManager._instance.castle.foodRequiredForPrimary [EmpireManager._instance.castle.currentLevel];
					PlayerParameters._instance.myPlayerParameter.gold -= EmpireManager._instance.castle.foodRequiredForPrimary [EmpireManager._instance.castle.currentLevel];
					EmpireManager._instance.castle.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
					EmpireManager._instance.castle.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
					tempButton = (string)lockButtonName.name;
					PlayerPrefs.SetString ("tempButton", tempButton);
					loadingScene.Instance.castlePrimaryClockText = EmpireManager._instance.castle.timeRequiredPerLevel [EmpireManager._instance.castle.currentLevel] * 3600f;
					confirmButton.interactable = false;
					GameObject[] deleteCards = GameObject.FindGameObjectsWithTag ("updateCards");
					for (int j = 0; j < deleteCards.Length; j ++) {
						deleteCards [j].GetComponent<Button> ().interactable = false;

					}
					updateBuilding.SetActive (false);
					loadingScene.Instance.isCastlePrimary = true;
					PlayerPrefs.SetString ("updating", "yes");
				} else {
					EmpireManager._instance.castle.primaryCardNo = -1;
					popupFromServer.ShowPopup ("Cannot lock the card at this time!");
				}
			});
		} 
		else if (buttonName == "storage") 
		{
			if (!wentFromSecondary) {
				EmpireManager._instance.storage.primaryCardNo = int.Parse (lockButtonNameStorage.name);
				int wheatTosend = PlayerParameters._instance.myPlayerParameter.wheat - EmpireManager._instance.storage.foodRequiredForPrimary [EmpireManager._instance.storage.currentLevel];
				int goldTosend = PlayerParameters._instance.myPlayerParameter.gold - EmpireManager._instance.storage.foodRequiredForPrimary [EmpireManager._instance.storage.currentLevel];

				createCard (buttonName, EmpireManager._instance.storage.currentLevel, EmpireManager._instance.storage.primaryCardNo, EmpireManager._instance.storage.primaryCardNo.ToString (), EmpireManager._instance.storage.currentExp, (EmpireManager._instance.storage.timeRequiredPerLevel [EmpireManager._instance.storage.currentLevel] * 60).ToString (), wheatTosend.ToString (), goldTosend.ToString (), isSucess => {
					if (isSucess) {
						loadingScene.Instance.storagePrimaryImage.sprite = lockButtonNameStorage.GetComponent<Image> ().sprite;
						PlayerParameters._instance.myPlayerParameter.wheat -= EmpireManager._instance.storage.foodRequiredForPrimary [EmpireManager._instance.storage.currentLevel];
						PlayerParameters._instance.myPlayerParameter.gold -= EmpireManager._instance.storage.foodRequiredForPrimary [EmpireManager._instance.storage.currentLevel];
						EmpireManager._instance.storage.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
						EmpireManager._instance.storage.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
						loadingScene.Instance.randomCards.Remove (int.Parse (lockButtonNameStorage.name));
						tempButtonStorage = (string)lockButtonNameStorage.name;
						PlayerPrefs.SetString ("tempButtonStorage", tempButtonStorage);
						confirmButton.interactable = false;
						GameObject[] deleteCards = GameObject.FindGameObjectsWithTag ("updateCards");
						for (int j = 0; j < deleteCards.Length; j ++) {
							deleteCards [j].GetComponent<Button> ().interactable = false;

						}
						updateBuilding.SetActive (false);
						loadingScene.Instance.storagePrimaryClockText = EmpireManager._instance.storage.timeRequiredPerLevel [EmpireManager._instance.storage.currentLevel] * 3600f;
						loadingScene.Instance.storageUpgradeButton.interactable = false;
						loadingScene.Instance.isStoragePrimary = true;
						PlayerPrefs.SetString ("updatingStorage", "yes");
					} else {
						EmpireManager._instance.storage.primaryCardNo = -1;
						popupFromServer.ShowPopup ("Cannot lock the card at this time!");
					}
				});
			} else {

				print ("lockDown");
				EmpireManager._instance.storage.secondaryCardNo = int.Parse (lockButtonNameStorageSecondary.name);
				int wheatToSave = PlayerParameters._instance.myPlayerParameter.wheat - EmpireManager._instance.storage.foodRequiredForSecondary [EmpireManager._instance.storage.currentLevel];
				int goldToSave = PlayerParameters._instance.myPlayerParameter.gold - EmpireManager._instance.storage.foodRequiredForSecondary [EmpireManager._instance.storage.currentLevel];
				secondaryCard (buttonName, EmpireManager._instance.storage.secondaryCardNo, EmpireManager._instance.storage.secondaryCardNo.ToString (), 50, (60).ToString (), wheatToSave, goldToSave, isSuccess => {
					if (isSuccess) {
						loadingScene.Instance.storageSecondaryImage.sprite = lockButtonNameStorageSecondary.GetComponent<Image> ().sprite;
						PlayerParameters._instance.myPlayerParameter.wheat -= EmpireManager._instance.storage.foodRequiredForSecondary [EmpireManager._instance.storage.currentLevel];
						PlayerParameters._instance.myPlayerParameter.gold -= EmpireManager._instance.storage.foodRequiredForSecondary [EmpireManager._instance.storage.currentLevel];
						EmpireManager._instance.storage.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
						EmpireManager._instance.storage.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
						loadingScene.Instance.randomCards.Remove (EmpireManager._instance.storage.secondaryCardNo);
						tempButtonStorageSecondary = (string)lockButtonNameStorageSecondary.name;
						PlayerPrefs.SetString ("tempButtonStorageSecondary", tempButtonStorageSecondary);
						confirmButton.interactable = false;
						GameObject[] deleteCards = GameObject.FindGameObjectsWithTag ("updateCards");
						for (int j = 0; j < deleteCards.Length; j ++) {
							deleteCards [j].GetComponent<Button> ().interactable = false;
						}
						updateBuilding.SetActive (false);
						loadingScene.Instance.storageSecondaryText = 3600f;
						EmpireManager._instance.storage.storgeLockDown.interactable = false;
						PlayerPrefs.SetString ("updatingStorageSecondary", "yes");
						loadingScene.Instance.isStorageSecondary = true;
					} else {
						EmpireManager._instance.storage.secondaryCardNo = -1;
						popupFromServer.ShowPopup ("Cannot Lock the card at this time!");
					}
				});

			}

		}

		else if (buttonName == "gate")
		{
			if (!wentFromSecondary) {
				EmpireManager._instance.gate.primaryCardNo = int.Parse (lockButtonNameGate.name);
				int wheatTosend = PlayerParameters._instance.myPlayerParameter.wheat - EmpireManager._instance.gate.foodRequiredForPrimary [EmpireManager._instance.gate.currentLevel];
				int goldTosend = PlayerParameters._instance.myPlayerParameter.gold - EmpireManager._instance.gate.foodRequiredForPrimary [EmpireManager._instance.gate.currentLevel];

				createCard (buttonName, EmpireManager._instance.gate.currentLevel, EmpireManager._instance.gate.primaryCardNo, EmpireManager._instance.gate.primaryCardNo.ToString (), EmpireManager._instance.gate.currentExp, (EmpireManager._instance.gate.timeRequiredPerLevel [EmpireManager._instance.gate.currentLevel] * 60).ToString (), wheatTosend.ToString (), goldTosend.ToString (), isSucess => {
					if (isSucess) {
						loadingScene.Instance.gatePrimaryImage.sprite = lockButtonNameGate.GetComponent<Image> ().sprite;
						PlayerParameters._instance.myPlayerParameter.wheat -= EmpireManager._instance.gate.foodRequiredForPrimary [EmpireManager._instance.gate.currentLevel];
						PlayerParameters._instance.myPlayerParameter.gold -= EmpireManager._instance.gate.foodRequiredForPrimary [EmpireManager._instance.gate.currentLevel];
						EmpireManager._instance.gate.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
						EmpireManager._instance.gate.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
						loadingScene.Instance.randomCards.Remove (int.Parse (lockButtonNameGate.name));
						tempButtonGate = (string)lockButtonNameGate.name;
						PlayerPrefs.SetString ("tempButtonGate", tempButtonGate);
						confirmButton.interactable = false;
						GameObject[] deleteCards = GameObject.FindGameObjectsWithTag ("updateCards");
						for (int j = 0; j < deleteCards.Length; j ++) {
							deleteCards [j].GetComponent<Button> ().interactable = false;

						}
						updateBuilding.SetActive (false);
						loadingScene.Instance.gatePrimaryClockText = EmpireManager._instance.gate.timeRequiredPerLevel [EmpireManager._instance.gate.currentLevel] * 3600f;
						loadingScene.Instance.gateUpgradeButton.interactable = false;
						loadingScene.Instance.isGatePrimary = true;
						PlayerPrefs.SetString ("updatingGate", "yes");
					} else {
						EmpireManager._instance.gate.primaryCardNo = -1;
						popupFromServer.ShowPopup ("Cannot lock the card at this time!");
					}
				});
			} else {

				print ("lockDown");
				EmpireManager._instance.gate.secondaryCardNo = int.Parse (lockButtonNameGateSecondary.name);
				int wheatToSave = PlayerParameters._instance.myPlayerParameter.wheat - EmpireManager._instance.gate.foodRequiredForSecondary [EmpireManager._instance.gate.currentLevel];
				int goldToSave = PlayerParameters._instance.myPlayerParameter.gold - EmpireManager._instance.gate.foodRequiredForSecondary [EmpireManager._instance.gate.currentLevel];
				secondaryCard (buttonName, EmpireManager._instance.gate.secondaryCardNo, EmpireManager._instance.gate.secondaryCardNo.ToString (), 50, (60).ToString (), wheatToSave, goldToSave, isSuccess => {
					if (isSuccess) {
						loadingScene.Instance.gateSecondaryImage.sprite = lockButtonNameGateSecondary.GetComponent<Image> ().sprite;
						PlayerParameters._instance.myPlayerParameter.wheat -= EmpireManager._instance.gate.foodRequiredForSecondary [EmpireManager._instance.gate.currentLevel];
						PlayerParameters._instance.myPlayerParameter.gold -= EmpireManager._instance.gate.foodRequiredForSecondary [EmpireManager._instance.gate.currentLevel];
						EmpireManager._instance.gate.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
						EmpireManager._instance.gate.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
						loadingScene.Instance.randomCards.Remove (EmpireManager._instance.gate.secondaryCardNo);
						tempButtonGateSecondary = (string)lockButtonNameGateSecondary.name;
						PlayerPrefs.SetString ("tempButtonGateSecondary", tempButtonGateSecondary);
						confirmButton.interactable = false;
						GameObject[] deleteCards = GameObject.FindGameObjectsWithTag ("updateCards");
						for (int j = 0; j < deleteCards.Length; j ++) {
							deleteCards [j].GetComponent<Button> ().interactable = false;
						}
						updateBuilding.SetActive (false);
						loadingScene.Instance.gateSecondaryText = 3600f;
						EmpireManager._instance.gate.storgeLockDown.interactable = false;
						PlayerPrefs.SetString ("updatingGateSecondary", "yes");
						loadingScene.Instance.isGateSecondary = true;
					} else {
						EmpireManager._instance.gate.secondaryCardNo = -1;
						popupFromServer.ShowPopup ("Cannot Lock the card at this time!");
					}
				});

			}

		}
		else if (buttonName == "barn") {
			if (!wentFromSecondary) {
				EmpireManager._instance.barn.primaryCardNo = int.Parse (lockButtonNameBarn.name);
				int wheatTosend = PlayerParameters._instance.myPlayerParameter.wheat;
				int goldTosend = PlayerParameters._instance.myPlayerParameter.gold - EmpireManager._instance.barn.foodRequiredForPrimary [EmpireManager._instance.barn.currentLevel];
				createCard (buttonName, EmpireManager._instance.barn.currentLevel, EmpireManager._instance.barn.primaryCardNo, EmpireManager._instance.barn.primaryCardNo.ToString (), EmpireManager._instance.barn.currentExp, (EmpireManager._instance.barn.timeRequiredPerLevel [EmpireManager._instance.barn.currentLevel] * 60).ToString (), wheatTosend.ToString (), goldTosend.ToString (), isSucess => {
					if (isSucess) {
						loadingScene.Instance.barnPrimaryImage.sprite = lockButtonNameBarn.GetComponent<Image> ().sprite;
						PlayerParameters._instance.myPlayerParameter.gold -= EmpireManager._instance.barn.foodRequiredForPrimary [EmpireManager._instance.barn.currentLevel];
						EmpireManager._instance.barn.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
						EmpireManager._instance.barn.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
						loadingScene.Instance.randomCards.Remove (int.Parse (lockButtonNameBarn.name));
						tempButtonBarn = (string)lockButtonNameBarn.name;
						PlayerPrefs.SetString ("tempButtonBarn", tempButtonBarn);
						confirmButton.interactable = false;

						GameObject[] deleteCards = GameObject.FindGameObjectsWithTag ("updateCards");
						for (int j = 0; j < deleteCards.Length; j ++) {
							deleteCards [j].GetComponent<Button> ().interactable = false;


						}
						updateBuilding.SetActive (false);
						loadingScene.Instance.barnPrimaryClockText = EmpireManager._instance.barn.timeRequiredPerLevel [EmpireManager._instance.barn.currentLevel] * 3600f;
						loadingScene.Instance.barnUpgradeButton.interactable = false;
						loadingScene.Instance.isBarnPrimary = true;
						PlayerPrefs.SetString ("updatingBarn", "yes");
					} else {
						EmpireManager._instance.barn.primaryCardNo = -1;
						popupFromServer.ShowPopup ("Cannot lock the card at this time!");
					}
				});
			} else {

				EmpireManager._instance.barn.secondaryCardNo = int.Parse (lockButtonNameBarnSecondary.name);
				int wheatToSave = PlayerParameters._instance.myPlayerParameter.wheat;
				int goldToSave = PlayerParameters._instance.myPlayerParameter.gold - EmpireManager._instance.barn.foodRequiredForSecondary [EmpireManager._instance.barn.currentLevel];
				secondaryCard (buttonName, EmpireManager._instance.barn.secondaryCardNo, EmpireManager._instance.barn.secondaryCardNo.ToString (), 50, (60).ToString (), wheatToSave, goldToSave,
				               isSuccess => {
					if (isSuccess) {
						loadingScene.Instance.barnSecondaryImage.sprite = lockButtonNameBarnSecondary.GetComponent<Image> ().sprite;
						PlayerParameters._instance.myPlayerParameter.gold -= EmpireManager._instance.barn.foodRequiredForSecondary [EmpireManager._instance.barn.currentLevel];
						EmpireManager._instance.barn.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
						Debug.Log ("gold final = " + PlayerParameters._instance.myPlayerParameter.gold);
						loadingScene.Instance.randomCards.Remove (EmpireManager._instance.barn.secondaryCardNo);
						confirmButton.interactable = false;
						GameObject[] deleteCards = GameObject.FindGameObjectsWithTag ("updateCards");
						for (int j = 0; j < deleteCards.Length; j ++) {
							deleteCards [j].GetComponent<Button> ().interactable = false;
						}
						updateBuilding.SetActive (false);
						loadingScene.Instance.barnSecondaryText = 3600f;
						EmpireManager._instance.barn.storgeLockDown.interactable = false;
						PlayerPrefs.SetString ("updatingBarnSecondary", "yes");
						loadingScene.Instance.isBarnSecondary = true;
					} else {
						EmpireManager._instance.barn.secondaryCardNo = -1;
						popupFromServer.ShowPopup ("Cannot Lock the card at this time!");
					}
				});


			}

		} else if (buttonName == "goldMine") {
			if (!wentFromSecondary) {
				EmpireManager._instance.goldMine.primaryCardNo = int.Parse (lockButtonNameGoldMine.name);
				int wheatTosend = PlayerParameters._instance.myPlayerParameter.wheat - EmpireManager._instance.goldMine.foodRequiredForPrimary [EmpireManager._instance.goldMine.currentLevel];
				int goldTosend = PlayerParameters._instance.myPlayerParameter.gold;
				createCard (buttonName, EmpireManager._instance.goldMine.currentLevel, EmpireManager._instance.goldMine.primaryCardNo, EmpireManager._instance.goldMine.primaryCardNo.ToString (), EmpireManager._instance.goldMine.currentExp, (EmpireManager._instance.goldMine.timeRequiredPerLevel [EmpireManager._instance.goldMine.currentLevel] * 60).ToString (), wheatTosend.ToString (), goldTosend.ToString (), isSucess => {
					if (isSucess) {
						//lockButtonNameGoldMine.GetComponentInChildren<Text>().text = "LOCKED";
						loadingScene.Instance.goldMinePrimaryImage.sprite = lockButtonNameGoldMine.GetComponent<Image> ().sprite;
						PlayerParameters._instance.myPlayerParameter.wheat -= EmpireManager._instance.goldMine.foodRequiredForPrimary [EmpireManager._instance.goldMine.currentLevel];
						Debug.Log ("wheat final = " + PlayerParameters._instance.myPlayerParameter.wheat);
						EmpireManager._instance.goldMine.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
						loadingScene.Instance.randomCards.Remove (int.Parse (lockButtonNameGoldMine.name));
						tempButtonGoldMine = (string)lockButtonNameGoldMine.name;
						PlayerPrefs.SetString ("tempButtonGoldMine", tempButtonGoldMine);
						confirmButton.interactable = false;
						GameObject[] deleteCards = GameObject.FindGameObjectsWithTag ("updateCards");
						for (int j = 0; j < deleteCards.Length; j ++) {
							deleteCards [j].GetComponent<Button> ().interactable = false;

						}
						updateBuilding.SetActive (false);
						loadingScene.Instance.goldMinePrimaryClockText = EmpireManager._instance.goldMine.timeRequiredPerLevel [EmpireManager._instance.goldMine.currentLevel] * 3600f;
						loadingScene.Instance.goldMineUpgradeButton.interactable = false;
						loadingScene.Instance.isgoldMinePrimary = true;
						PlayerPrefs.SetString ("updatingGoldMine", "yes");
					} else {
						EmpireManager._instance.goldMine.primaryCardNo = -1;
						popupFromServer.ShowPopup ("Cannot lock the card at this time!");
					}
				});
			} else {

				EmpireManager._instance.goldMine.secondaryCardNo = int.Parse (lockButtonGoldMineSecondary.name);
				int wheatToSave = PlayerParameters._instance.myPlayerParameter.wheat - EmpireManager._instance.goldMine.foodRequiredForSecondary [EmpireManager._instance.goldMine.currentLevel];
				int goldToSave = PlayerParameters._instance.myPlayerParameter.gold;
				secondaryCard (buttonName, EmpireManager._instance.goldMine.secondaryCardNo, EmpireManager._instance.goldMine.secondaryCardNo.ToString (), 50, (60).ToString (), wheatToSave, goldToSave,
				               isSuccess => {
					if (isSuccess) {
						loadingScene.Instance.goldMineSecondaryImage.sprite = lockButtonGoldMineSecondary.GetComponent<Image> ().sprite;
						PlayerParameters._instance.myPlayerParameter.wheat -= EmpireManager._instance.goldMine.foodRequiredForSecondary [EmpireManager._instance.goldMine.currentLevel];
						Debug.Log ("wheat final = " + PlayerParameters._instance.myPlayerParameter.wheat);
						EmpireManager._instance.goldMine.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
						loadingScene.Instance.randomCards.Remove (EmpireManager._instance.goldMine.secondaryCardNo);
						confirmButton.interactable = false;
						GameObject[] deleteCards = GameObject.FindGameObjectsWithTag ("updateCards");
						for (int j = 0; j < deleteCards.Length; j ++) {
							deleteCards [j].GetComponent<Button> ().interactable = false;
						}
						updateBuilding.SetActive (false);
						loadingScene.Instance.goldMineSecondaryText = 3600f;
						EmpireManager._instance.goldMine.storgeLockDown.interactable = false;
						PlayerPrefs.SetString ("updatingGoldMineSecondary", "yes");
						loadingScene.Instance.isgoldMineSecondary = true;
					} else {
						EmpireManager._instance.goldMine.secondaryCardNo = -1;
						popupFromServer.ShowPopup ("Cannot Lock the card at this time!");
					}
				});


			}

		} else if (buttonName == "barrack") {
			if (!wentFromSecondary) {
				EmpireManager._instance.barracks.primaryCardNo = int.Parse (lockButtonNameBarrakcs.name);
				int wheatTosend = PlayerParameters._instance.myPlayerParameter.wheat - EmpireManager._instance.barracks.foodRequiredForPrimary [EmpireManager._instance.barracks.currentLevel];
				int goldTosend = PlayerParameters._instance.myPlayerParameter.gold - EmpireManager._instance.barracks.foodRequiredForPrimary [EmpireManager._instance.barracks.currentLevel];
				createCard (buttonName, EmpireManager._instance.barracks.currentLevel, EmpireManager._instance.barracks.primaryCardNo, EmpireManager._instance.barracks.primaryCardNo.ToString (), EmpireManager._instance.barracks.currentExp, (EmpireManager._instance.barracks.timeRequiredPerLevel [EmpireManager._instance.barracks.currentLevel] * 60).ToString (), wheatTosend.ToString (), goldTosend.ToString (), isSucess => {
					if (isSucess) {
						loadingScene.Instance.barrackPrimaryImage.sprite = lockButtonNameBarrakcs.GetComponent<Image> ().sprite;
						PlayerParameters._instance.myPlayerParameter.wheat -= EmpireManager._instance.barracks.foodRequiredForPrimary [EmpireManager._instance.barracks.currentLevel];
						PlayerParameters._instance.myPlayerParameter.gold -= EmpireManager._instance.barracks.foodRequiredForPrimary [EmpireManager._instance.barracks.currentLevel];
						Debug.Log ("wheat final = " + PlayerParameters._instance.myPlayerParameter.wheat);
						EmpireManager._instance.barracks.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
						EmpireManager._instance.barracks.goldText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
						loadingScene.Instance.randomCards.Remove (int.Parse (lockButtonNameBarrakcs.name));
						tempButtonBarrakPrimary = (string)lockButtonNameBarrakcs.name;
						PlayerPrefs.SetString ("tempButtonBarrack", tempButtonBarrakPrimary);
						confirmButton.interactable = false;
						GameObject[] deleteCards = GameObject.FindGameObjectsWithTag ("updateCards");
						for (int j = 0; j < deleteCards.Length; j ++) {
							deleteCards [j].GetComponent<Button> ().interactable = false;

						}
						updateBuilding.SetActive (false);
						loadingScene.Instance.barrackPrimaryClockText = EmpireManager._instance.barracks.timeRequiredPerLevel [EmpireManager._instance.barracks.currentLevel] * 3600f;
						loadingScene.Instance.barrackUpgradeButton.interactable = false;
						loadingScene.Instance.isBarrackPrimary = true;
						PlayerPrefs.SetString ("updatingBarrack", "yes");
					} else {
						EmpireManager._instance.barracks.primaryCardNo = -1;
						popupFromServer.ShowPopup ("Cannot lock the card at this time!");
					}
				});
			} else {

				EmpireManager._instance.barracks.secondaryCardNo = int.Parse (lockButtonBarrakcsSecondary.name);
				int wheatToSave = PlayerParameters._instance.myPlayerParameter.wheat - EmpireManager._instance.barracks.foodRequiredForSecondary [EmpireManager._instance.barracks.currentLevel];
				int goldToSave = PlayerParameters._instance.myPlayerParameter.gold - EmpireManager._instance.barracks.foodRequiredForSecondary [EmpireManager._instance.barracks.currentLevel];
//				Debug.Log(goldToSave +" , "+PlayerParameters._instance.myPlayerParameter.gold +" , "+EmpireManager._instance.goldMine.foodRequiredForSecondary [EmpireManager._instance.goldMine.currentLevel]);
//				Debug.Log(wheatToSave +" , "+PlayerParameters._instance.myPlayerParameter.wheat +" , "+EmpireManager._instance.goldMine.foodRequiredForSecondary [EmpireManager._instance.goldMine.currentLevel]);

				secondaryCard (buttonName, EmpireManager._instance.barracks.secondaryCardNo, EmpireManager._instance.barracks.secondaryCardNo.ToString (), 50, (60).ToString (), wheatToSave, goldToSave,
				              isSuccess => {
					if (isSuccess) {
						loadingScene.Instance.barrackSecondaryImage.sprite = lockButtonBarrakcsSecondary.GetComponent<Image> ().sprite;
						PlayerParameters._instance.myPlayerParameter.wheat -= EmpireManager._instance.barracks.foodRequiredForSecondary [EmpireManager._instance.barracks.currentLevel];
						EmpireManager._instance.barracks.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
						loadingScene.Instance.randomCards.Remove (EmpireManager._instance.barracks.secondaryCardNo);
						confirmButton.interactable = false;
						GameObject[] deleteCards = GameObject.FindGameObjectsWithTag ("updateCards");
						for (int j = 0; j < deleteCards.Length; j ++) {
							deleteCards [j].GetComponent<Button> ().interactable = false;
						}
						updateBuilding.SetActive (false);
						loadingScene.Instance.barrackSecondaryText = 180f;
						EmpireManager._instance.barracks.storgeLockDown.interactable = false;
						PlayerPrefs.SetString ("updatingBarrackSecondary", "yes");
						loadingScene.Instance.isBarrackSecondary = true;
					} else {
						EmpireManager._instance.barracks.secondaryCardNo = -1;
						popupFromServer.ShowPopup ("Cannot Lock the card at this time!");
					}
				});


			}

		}
		else if (buttonName == "prison")
		{
			prisonObj.instance.prisonCompleteupdate();
		}

		else if(buttonName=="traningGround")
		{
			print("buttonName"+buttonName);
			if(!wentFromSecondary)
			{
				EmpireManager._instance.trainingGround.primaryCardNo = int.Parse (lockButtonNameGround.name);
				int wheatTosend = PlayerParameters._instance.myPlayerParameter.wheat - EmpireManager._instance.barracks.foodRequiredForPrimary [EmpireManager._instance.trainingGround.currentLevel];
				int goldTosend = PlayerParameters._instance.myPlayerParameter.gold- EmpireManager._instance.barracks.foodRequiredForPrimary [EmpireManager._instance.trainingGround.currentLevel];
				createCard (buttonName, EmpireManager._instance.trainingGround.currentLevel, EmpireManager._instance.trainingGround.primaryCardNo, EmpireManager._instance.trainingGround.primaryCardNo.ToString (), EmpireManager._instance.trainingGround.currentExp, (EmpireManager._instance.trainingGround.timeRequiredPerLevel [EmpireManager._instance.trainingGround.currentLevel] * 60).ToString (), wheatTosend.ToString (), goldTosend.ToString (), isSucess => {
					if(isSucess)
					{
						loadingScene.Instance.trainingGroundPrimaryImage.sprite=lockButtonNameGround.GetComponent<Image>().sprite;
						PlayerParameters._instance.myPlayerParameter.wheat -=EmpireManager._instance.barracks.foodRequiredForPrimary [EmpireManager._instance.trainingGround.currentLevel];
						PlayerParameters._instance.myPlayerParameter.gold -=EmpireManager._instance.barracks.foodRequiredForPrimary [EmpireManager._instance.trainingGround.currentLevel];
						Debug.Log("wheat final = "+PlayerParameters._instance.myPlayerParameter.wheat);
						EmpireManager._instance.trainingGround.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
						EmpireManager._instance.trainingGround.goldText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
						EmpireManager._instance.trainingGround.foodText2.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
						EmpireManager._instance.trainingGround.goldText2.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
						EmpireManager._instance.trainingGround.foodText3.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
						EmpireManager._instance.trainingGround.goldText3.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
						EmpireManager._instance.trainingGround.foodText4.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
						EmpireManager._instance.trainingGround.goldText4.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
						loadingScene.Instance.randomCards.Remove (int.Parse (lockButtonNameGround.name));
						tempButtonGroundPrimary= (string)lockButtonNameGround.name;
						PlayerPrefs.SetString("tempButtonGround",tempButtonGroundPrimary);
						confirmButton.interactable=false;

						GameObject[] deleteCards = GameObject.FindGameObjectsWithTag ("updateCards");
						for (int j = 0; j < deleteCards.Length; j ++)
						{
							deleteCards [j].GetComponent<Button> ().interactable = false;


						}
						updateBuilding.SetActive(false);
						loadingScene.Instance.trainingGroundPrimaryClockText=EmpireManager._instance.trainingGround.timeRequiredPerLevel[EmpireManager._instance.trainingGround.currentLevel]*3600f;
						loadingScene.Instance.trainingGroundUpgradeButton.interactable=false;
						loadingScene.Instance.istrainingGroundPrimary=true;
						PlayerPrefs.SetString("updatingGround","yes");
					}
					else
					{
						EmpireManager._instance.trainingGround.primaryCardNo = -1;
						popupFromServer.ShowPopup ("Cannot lock the card at this time!");
					}
				});
			}
			else
			{
				print("level up");
				EmpireManager._instance.trainingGround.secondaryCardNo = int.Parse (lockButtonNameGroundSecondary.name);
				int wheatToSave = PlayerParameters._instance.myPlayerParameter.wheat  - EmpireManager._instance.trainingGround.foodRequiredForSecondary [EmpireManager._instance.trainingGround.currentLevel];
				int goldToSave = PlayerParameters._instance.myPlayerParameter.gold - EmpireManager._instance.trainingGround.foodRequiredForSecondary [EmpireManager._instance.trainingGround.currentLevel];
//				secondaryCard( buttonName,  EmpireManager._instance.trainingGround.secondaryCardNo , EmpireManager._instance.trainingGround.secondaryCardNo.ToString() , 50, (60).ToString() , wheatToSave , goldToSave ,
//				              isSuccess => {
//					if(isSuccess)
//					{
						//loadingScene.Instance.trainingGroundSecondaryImage.sprite=lockButtonNameGroundSecondary.GetComponent<Image>().sprite;
						//PlayerParameters._instance.myPlayerParameter.wheat -=EmpireManager._instance.trainingGround.foodRequiredForSecondary [EmpireManager._instance.trainingGround.currentLevel];
						//EmpireManager._instance.trainingGround.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
						confirmButton.interactable=false;
						GameObject[] deleteCards = GameObject.FindGameObjectsWithTag ("updateCards");
//						for (int j = 0; j < deleteCards.Length; j ++)
//						{
//							deleteCards [j].GetComponent<Button> ().interactable = false;
//						}

//							GameObject[] allCardButtons = GameObject.FindGameObjectsWithTag ("updateCards");
//							for (int j = 0; j < allCardButtons.Length; j ++)
//							{
//								int cardNo = int.Parse (allCardButtons [j].name);
//								if( EmpireManager._instance.trainingGround.secondaryCardNo == cardNo)
//
//
//								{
//									allCardButtons [j].GetComponent<Button> ().interactable = true;
//									allCardButtons [j].GetComponentInChildren<Text>().text="";
//								}
//								else
//								{
//									allCardButtons [j].GetComponent<Button> ().interactable = true;
//									allCardButtons [j].GetComponentInChildren<Text>().text="";
//								}
//							}


						updateBuilding.SetActive(false);
						//loadingScene.Instance.trainingGroundSecondaryText=180f;
						//print("lockDown"+loadingScene.Instance.trainingGroundSecondaryText);
						EmpireManager._instance.trainingGround.storgeLockDown.interactable=true;
						//PlayerPrefs.SetString("updatingGroundSecondary","yes");
						//loadingScene.Instance.istrainingGroundSecondary=true;
						TrainingInventory.SetActive(true);
						skillPage.SetActive(true);
						empireSceneNew.instance.saveSpriteLocked=lockButtonNameGroundSecondary.GetComponent<Image>().sprite;
						GameObject.Find("profileStat").GetComponent<Image> ().sprite=empireSceneNew.instance.saveSpriteLocked;
						int cardIdLocked = CardsManager._instance.PositionOfCardInList (int.Parse (lockButtonNameGroundSecondary.name));
						cardIdLocked= CardsManager._instance.PositionOfCardInList (int.Parse (lockButtonNameGroundSecondary.name));
						SetSelectedCardDataInCardSelectionAgain2(CardsManager._instance.mycards[cardIdLocked]);
						GameObject.Find("nameStat").GetComponent<Text> ().text=CardsManager._instance.mycards[cardIdLocked].card_name.ToString();
						cardIdLockedDefault=CardsManager._instance.mycards[cardIdLocked].card_name.ToString();

						GameObject.Find("lvlStat").GetComponent<Text> ().text="Lvl "+CardsManager._instance.mycards[cardIdLocked].card_level.ToString();
						GameObject.Find("maxLvlStat").GetComponent<Text>().text="Max Lvl "+CardsManager._instance.mycards[cardIdLocked].max_level.ToString();
						GameObject.Find("leaderStat").GetComponent<Text> ().text=CardsManager._instance.mycards[cardIdLocked].leadership.ToString();
						GameObject.Find("attackStat").GetComponent<Text> ().text=CardsManager._instance.mycards[cardIdLocked].attack.ToString();
						GameObject.Find("defenseStat").GetComponent<Text> ().text=CardsManager._instance.mycards[cardIdLocked].defense.ToString();
						GameObject.Find("skillText1").GetComponent<Text> ().text=CardsManager._instance.mycards[cardIdLocked].skill_1.ToString();
						GameObject.Find("skillText2").GetComponent<Text> ().text=CardsManager._instance.mycards[cardIdLocked].skill_2.ToString();
						GameObject.Find("description1").GetComponent<Text> ().text=CardsManager._instance.mycards[cardIdLocked].skill_1_Strength.ToString();
						GameObject.Find("description2").GetComponent<Text> ().text=CardsManager._instance.mycards[cardIdLocked].skill_2_Strength.ToString();
						GameObject.Find("lvlText1").GetComponent<Text> ().text="Lvl "+ CardsManager._instance.mycards[cardIdLocked].skill1_level.ToString();
						GameObject.Find("lvlText2").GetComponent<Text> ().text="Lvl "+ CardsManager._instance.mycards[cardIdLocked].skill1_level.ToString();
						GameObject.Find("expStat").GetComponent<Text> ().text="Exp "+ CardsManager._instance.mycards[cardIdLocked].experience.ToString();
						GameObject.Find("rarity").GetComponent<Text> ().text= CardsManager._instance.mycards[cardIdLocked].rarity.ToString();

						loadingScene.Instance.randomCards.Remove (EmpireManager._instance.trainingGround.secondaryCardNo);

					}
//					else
//					{
//						EmpireManager._instance.trainingGround.secondaryCardNo = -1;
//						popupFromServer.ShowPopup ("Cannot Lock the card at this t  ime!");
//						Debug.Log("showpopup---");
//					}
//				});


			//}

		}

		backbuildingUpgrade ();

	}



	//---------  API -------------
	public void createCard(string buildingName , int curLevel , int primaryCardNo , string primaryCardName , int buildingExp , string updateTime , string wheatSave, string goldSave, Action <bool> callback)
	{
		loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected)
			{
				WWWForm form_time= new WWWForm ();
//				string URltime = "http://www.ommzi.com/new_app/create_building.php";
				string URltime = "http://ec2-54-174-178-121.compute-1.amazonaws.com/mod_web_active_api/create_building.php";

				form_time.AddField ("tag","create_building");
				form_time.AddField ("user_id",PlayerDataParse._instance.playersParam.userId);
				form_time.AddField ("device_id",SystemInfo.deviceUniqueIdentifier);
				form_time.AddField ("building_name",buildingName);
				form_time.AddField ("building_level", curLevel.ToString ());
				form_time.AddField ("primary_card_locked_no",primaryCardNo.ToString());
				form_time.AddField ("primary_card_locked",primaryCardName);
				form_time.AddField ("building_exp",buildingExp.ToString());
				form_time.AddField ("upgrade_time",updateTime.ToString());
				form_time.AddField ("wheat",wheatSave.ToString ());
				form_time.AddField ("gold",goldSave.ToString ());
				form_time.AddField ("wheat_time",PlayerParameters._instance.myPlayerParameter.wheat_time.ToString ());
				form_time.AddField ("gold_time",PlayerParameters._instance.myPlayerParameter.gold_time.ToString ());
				form_time.AddField ("currentt_time",TimeManager._instance.GetCurrentServerTime ().ToString ());
				print ("building_level" + EmpireManager._instance.castle.currentLevel.ToString ());
				WWW www = new WWW(URltime,form_time.data);
				StartCoroutine(userTIMEfetching2(www , callback));
			}
			else
			{
				loader.SetActive (false);
				callback(false);
			}
		});


	}

	public IEnumerator userTIMEfetching2(WWW www , Action <bool> callback)
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
			loader.SetActive (false);
			Debug.Log (www.text);

		}
		else
		{
			callback(false);
			loader.SetActive (false);
			Debug.Log ("ERROR MESSAGE"+www.text);

		}
	}
	//------  secondary ---
	public void secondaryCard(string buildingName  , int card_number , string card_name , int upgraded_value ,  string active_time ,int wheatSave , int goldSave , Action<bool> callBack)
	{
		loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected)
			{
				WWWForm form_time= new WWWForm ();
//				string URltime = "http://www.ommzi.com/new_app/secondary_lock_card_building.php";
				string URltime = "http://ec2-54-174-178-121.compute-1.amazonaws.com/mod_web_active_api/secondary_lock_card_building.php";

				form_time.AddField ("tag","secondarylock");
				form_time.AddField ("user_id",PlayerDataParse._instance.playersParam.userId);
				form_time.AddField ("device_id",SystemInfo.deviceUniqueIdentifier);
				form_time.AddField ("building_name",buildingName);
				form_time.AddField ("secondary_card_no",card_number.ToString());
				form_time.AddField ("secondary_card_locked",card_name);
				form_time.AddField ("upgraded_value",upgraded_value.ToString());
				form_time.AddField ("wheat",wheatSave.ToString ());
				Debug.Log("goldSave   ===    "+goldSave);
				Debug.Log("wheatSave   ===    "+wheatSave);
				form_time.AddField ("gold",goldSave.ToString ());
				form_time.AddField ("wheat_time",PlayerParameters._instance.myPlayerParameter.wheat_time.ToString ());
				form_time.AddField ("gold_time",PlayerParameters._instance.myPlayerParameter.gold_time.ToString ());
				form_time.AddField ("active_time",active_time);
				form_time.AddField ("time_of_secondary_upgrade",TimeManager._instance.GetCurrentServerTime ().ToString ());
				print ("building_level" + EmpireManager._instance.castle.currentLevel.ToString ());
				WWW www = new WWW(URltime,form_time.data);
				StartCoroutine(userTIMEfetching4(www , callBack));
			}
			else
			{
				loader.SetActive (false);
				callBack(false);
			}
		});
	}

	IEnumerator userTIMEfetching4(WWW www , Action <bool> callBack)
	{
		yield return www;

		if (www.error == null)
		{
			Debug.Log (www.text);
			if(www.text.Contains ("error_msg"))
			{
				callBack(false);
			}
			else
			{
				callBack(true);
			}
			loader.SetActive (false);

		}
		else
		{
			loader.SetActive (false);
			Debug.Log ("ERROR MESSAGE"+www.error);
			callBack(false);
		}
	}

	public void updateCompled()
	{

		lockButtonName.interactable=false;
		lockButtonName.GetComponentInChildren<Text>().text="SELECTED";

	}
	public void updateCompleteExit()
	{
		updateCompleted.SetActive(false);
	}


	//---------------  Research ---------------
	public void ResearchBuildingButton()
	{
		researchLab.SetActive(true);
	}
	public void researchBuildingButtonExit()
	{
		researchLab.SetActive(false);
	}

	public void researchCompled()
	{
		researchCompleted.SetActive(true);
		updateBuilding.SetActive(false);

	}
	public void researchCompleteExit()
	{
		researchCompleted.SetActive(false);
	}

	//-----------------------------------------

	public void warningActive()
	{
		warningPanel.SetActive(true);
	}

	public void exitWarning()
	{
		warningPanel.SetActive(false);
	}

	public void sortButtonPressedLvl()
	{
		sortPanelLVL.SetActive(true);
	}
	public void exitSortButtonlvl()
	{
		sortPanelLVL.SetActive(false);
	}


	public void exitLevelSelection()
	{
		levelSelectionLayout.SetActive(false);
	}
	public void LevelSelection()
	{
		warningPanel.SetActive(false);
		levelSelectionLayout.SetActive(true);

	}

	public void sortButtonPressed()
	{
		sortPanel.SetActive(true);
	}
	public void exitSortButton()
	{
		sortPanel.SetActive(false);
	}

	public void skill1button()
	{
		GameObject.Find("1").GetComponent<Outline>().effectColor=new Color32(240,240,14,255);
		GameObject.Find("2").GetComponent<Outline>().effectColor=new Color32(0,0,0,255);
	}
	public void skill2button()
	{
		GameObject.Find("2").GetComponent<Outline>().effectColor=new Color32(240,240,14,255);
		GameObject.Find("1").GetComponent<Outline>().effectColor=new Color32(0,0,0,255);
	}


	public void skillUpTraining()
	{
		skillUp.SetActive(true);
		for(int i=0; i<forTrainingtime.Length;i++)
		{
			forTrainingtime[i].SetActive(true);
		}
		limitBreakButtons.SetActive(false);
		GameObject.Find("1").GetComponent<Outline>().effectColor=new Color32(240,240,14,255);
	}


	public void backFromSkillUp()
	{
		skillUp.SetActive(false);
		for(int i=0; i<forTrainingtime.Length;i++)
		{
			forTrainingtime[i].SetActive(false);
		}
		limitBreakButtons.SetActive(true);
		skill1Btn.GetComponent<Outline>().effectColor= new Color32(0,0,0,255);
		skill2Btn.GetComponent<Outline>().effectColor= new Color32(0,0,0,255);


	}



	public void levelUpTraining()
	{
		for(int i=0; i<forTrainingtime.Length;i++)
		{
			forTrainingtime[i].SetActive(true);
		}
		limitBreakButtons.SetActive(false);
		levelBtn.GetComponent<Outline>().effectColor=new Color32(240,240,14,255);
		limitBtn.GetComponent<Outline>().effectColor=new Color32(0,0,0,255);

	}




	public void limitUpTraining()
	{
		for(int i=0; i<forTrainingtime.Length;i++)
		{
			forTrainingtime[i].SetActive(true);
		}
		limitBreakButtons.SetActive(false);
		limitBtn.GetComponent<Outline>().effectColor=new Color32(240,240,14,255);
		levelBtn.GetComponent<Outline>().effectColor=new Color32(0,0,0,255);

	}


	public void backFromlimitUp()
	{
		for(int i=0; i<forTrainingtime.Length;i++)
		{
			forTrainingtime[i].SetActive(false);
		}
		limitBreakButtons.SetActive(true);
	}



	public void onClickTrainingButton()
	{

		wentFromSecondary = true;
		buildingUpgradeLayout.SetActive (true);
		ShowAllCards ();
		empireSceneNew.instance.captiveClick ();


	}
	public void trainingButton(Button button)
	{

		button.GetComponent<Image>().color=new Color32(0,0,0,255);
		skillPage.SetActive(true);
		trainingCards.SetActive(false);

	}



	public void backButtonSkillPage()
	{

		skillPage.SetActive(false);
		trainingCards.SetActive(false);
		EmpireManager._instance.trainingGround.secondaryCardNo = 0;
		lockButtonNameGroundSecondary = null;

	}
	public void backTrainingCards()
	{
		TrainingInventory.SetActive(false);
	}

	// Update is called once per frame
	void Update ()
	{
		if (buildingUpgradeLayout.activeInHierarchy == true) 
		{
			if (buttonName == "prison") {
				GameObject.Find ("buildingUpgradeCost").GetComponent<Text> ().text = "Prison Interrogation cost";
			} else {
				GameObject.Find ("buildingUpgradeCost").GetComponent<Text> ().text = "Building Upgrade cost";
			}
		}
		#if UNITY_ANDROID

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(loadingScene.Instance.playerProfilePanel.activeInHierarchy==true)
			{
				onClickProfileExit();
			}
			else if(isMneuActive==true)
			{
				for(int j=0;j<bottomsButtons.Length;j++)
				{
					bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
					bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
					bottomsButtons[j].GetComponent<Button>().interactable=true;
				}
				menuScreen.SetActive(false);
				isMneuActive=false;

			}

			else
			{
				if(Sort.activeInHierarchy==true)
				{
					exitSort();
				}
				else if(loadingScene.Instance.playerProfilePanel.activeInHierarchy==true)
				{
					onClickProfileExit();
				}
				else if(setting.activeInHierarchy==true)
				{
					onClickSettinExitg();
				}

				else if(updateCompleted.activeInHierarchy==true)
				{
					updateCompleteExit();
				}
				else if(updateBuilding.activeInHierarchy==true)
				{
					updateBuildingButtonExit();
				}
				else if(researchLaboraotry.activeInHierarchy==true)
				{
					backFromLabResearchClick();
				}
				else if(prisonInterogate.activeInHierarchy==true)
				{
					BackToprisionIntergogate();
				}
				else if(soldierRecruit.activeInHierarchy==true)
				{
					backFromRecruiter();
				}
				else if(buildingUpgradeLayout.activeInHierarchy==true)
				{
					backbuildingUpgrade();
				}
				else if(levelUp.activeInHierarchy==true)
				{
					ExitLevel();
				}

//				else if(skill2Obj.activeInHierarchy==true)
//				{
//					skill2Exit();
//				}
//				else if(skill1Obj.activeInHierarchy==true)
//				{
//					skill1Exit();
//				}
//
//				else if(limitBreak.activeInHierarchy==true)
//				{
//					ExitLimit();
//				}
				else if(skillPage.activeInHierarchy==true)
				{
					backButtonSkillPage();
				}

				else if(TrainingInventory.activeInHierarchy==true)
				{
					backTrainingCards();
				}

				else if(instant1.activeInHierarchy==true)
				{
					instant1.SetActive(false);

				}
				else if(instant2.activeInHierarchy==true)
				{
					instant2.SetActive(false);

				}

				else if(scrollWindows.activeInHierarchy==true)
				{

					BarracksPressExit();
					CastleButtonExit();
					BarnButtonExit();
					TrainingButtonExit();
					StorageButtonExit();
					PrisonButtonExit();
					GoldButtonExit();
					LaboratoryButtonExit();
					KnowledgeTreeButtonExit();
					GatePressExit();

				}
				else if(instant1.activeInHierarchy==true)
				{
					instant1.SetActive(false);

				}
				else if(instant2.activeInHierarchy==true)
				{
					instant2.SetActive(false);

				}
				else
				{

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

						}

					}

				}

			}
		}


		#endif

		if(clickObj=="1")
		{
			scrollButton.value=0f;
		}

		else if(clickObj=="2")
		{

			scrollButton.value=.1111111f;
		}
		else if(clickObj=="3")
		{

			scrollButton.value=.2222222f;
		}
		else if(clickObj=="4")
		{

			scrollButton.value=.3333333f;
		}
		else if(clickObj=="5")
		{

			scrollButton.value=.4444444f;
		}
		else if(clickObj=="6")
		{


			scrollButton.value=.5555555f;
		}
		else if(clickObj=="7")
		{

			scrollButton.value=.6666666f;
		}
		else if(clickObj=="8")
		{

			scrollButton.value=.7777777f;
		}
		else if(clickObj=="9")
		{

			scrollButton.value=.8888888f;
		}
		else if(clickObj=="10")
		{

			scrollButton.value=1f;
		}
		else
		{

		}

		if(clickOnBuilding==true)
		{

			if(scrollButton.value==0f)
			{
				if(GameObject.Find("leftBuilding"))
					GameObject.Find("leftBuilding").GetComponent<Button>().interactable=false;
				if(GameObject.Find("rightBuilding"))
					GameObject.Find("rightBuilding").GetComponent<Button>().interactable=true;
				EmpireManager._instance.barracks.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
				EmpireManager._instance.barracks.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
				finalExperienceOfBuilding = EmpireManager._instance.barracks.requiredExpPerLevel[EmpireManager._instance.barracks.currentLevel];
				GemsText[3].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();
				EmpireManager._instance.barracks.upgradeFoodCostPrimary.text=EmpireManager._instance.barracks.foodRequiredForPrimary [EmpireManager._instance.barracks.currentLevel].ToString();
				EmpireManager._instance.barracks.upgradeGoldCostPrimary.text=EmpireManager._instance.barracks.foodRequiredForPrimary [EmpireManager._instance.barracks.currentLevel].ToString();
				EmpireManager._instance.barracks.upgradeGoldCostSecondary.text=EmpireManager._instance.barracks.foodRequiredForSecondary [EmpireManager._instance.barracks.currentLevel].ToString();
				EmpireManager._instance.barracks.upgradeFoodCostSecondary.text=EmpireManager._instance.barracks.foodRequiredForSecondary [EmpireManager._instance.barracks.currentLevel].ToString();
				float tempTimer=EmpireManager._instance.barracks.timeRequiredPerLevel [EmpireManager._instance.barracks.currentLevel]*3600f/60;
				EmpireManager._instance.barracks.upgradeTimePrimary.text=tempTimer.ToString();
				int currentVal = EmpireManager._instance.barracks.finalValue1[EmpireManager._instance.barracks.currentLevel];
				int finalVal = EmpireManager._instance.barracks.finalValue1[EmpireManager._instance.barracks.currentLevel+1];
				loadingScene.Instance.barrackIncreaseLimitNow.text=currentVal.ToString();
				loadingScene.Instance.barrackIncreaseLimitNext.text=finalVal.ToString();
				loadingScene.Instance.barrackLevel.text="Lvl "+(EmpireManager._instance.barracks.currentLevel+1).ToString();
				loadingScene.Instance.DeployedSoldiers.text=PlayerParameters._instance.myPlayerParameter.currentlyDeployedSoldiers.ToString ()+"/"+PlayerParameters._instance.myPlayerParameter.maxDeployedSoldiers.ToString ();
				loadingScene.Instance.AvailableSoldiers.text=PlayerParameters._instance.myPlayerParameter.currentlyAvailableSoldiers.ToString ()+"/"+EmpireManager._instance.barracks.finalValue1[EmpireManager._instance.barracks.currentLevel].ToString();
				buttonName="barrack";
				deactivateBuildings(0);

			}
			else if(Mathf.Approximately(scrollButton.value ,.1111111f))
			{
				GameObject.Find("leftBuilding").GetComponent<Button>().interactable=true;
				GameObject.Find("rightBuilding").GetComponent<Button>().interactable=true;
				deactivateBuildings(1);

			}
			else if(Mathf.Approximately(scrollButton.value ,.2222222f))
			{
				GameObject.Find("leftBuilding").GetComponent<Button>().interactable=true;
				GameObject.Find("rightBuilding").GetComponent<Button>().interactable=true;
				deactivateBuildings(2);

			}
			else if(Mathf.Approximately(scrollButton.value ,.3333333f))
			{


				if(GameObject.Find("leftBuilding")!=null)
				{
				GameObject.Find("leftBuilding").GetComponent<Button>().interactable=true;
				}
				if(GameObject.Find("rightBuilding")!=null)
				{
				GameObject.Find("rightBuilding").GetComponent<Button>().interactable=true;
				}
				deactivateBuildings(3);

				EmpireManager._instance.prison.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
				EmpireManager._instance.prison.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
				finalExperienceOfBuilding = EmpireManager._instance.prison.requiredExpPerLevel[EmpireManager._instance.prison.currentLevel];
				GemsText[13].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();
				EmpireManager._instance.prison.upgradeFoodCostPrimary.text=EmpireManager._instance.prison.foodRequiredForPrimary [EmpireManager._instance.prison.currentLevel].ToString();
				EmpireManager._instance.prison.upgradeGoldCostPrimary.text=EmpireManager._instance.prison.foodRequiredForPrimary [EmpireManager._instance.prison.currentLevel].ToString();
				EmpireManager._instance.prison.upgradeGoldCostSecondary.text=EmpireManager._instance.prison.foodRequiredForSecondary [EmpireManager._instance.prison.currentLevel].ToString();
				EmpireManager._instance.prison.upgradeFoodCostSecondary.text=EmpireManager._instance.prison.foodRequiredForSecondary [EmpireManager._instance.prison.currentLevel].ToString();
				float tempTimer=EmpireManager._instance.prison.timeRequiredPerLevel [EmpireManager._instance.prison.currentLevel]*3600f/60;
				EmpireManager._instance.prison.upgradeTimePrimary.text=tempTimer.ToString();
				int currentVal = EmpireManager._instance.prison.finalValue1[EmpireManager._instance.prison.currentLevel];
				int finalVal = EmpireManager._instance.prison.finalValue1[EmpireManager._instance.prison.currentLevel+1];
				prisonObj.instance.prisonNow.text=currentVal.ToString();
				prisonObj.instance.prisonNext.text=finalVal.ToString();
				prisonObj.instance.prisonLevel.text="Lvl "+(EmpireManager._instance.prison.currentLevel+1).ToString();
				buttonName="prison";

			}
			else if(Mathf.Approximately(scrollButton.value ,.4444444f))
			{
				if(GameObject.Find("leftBuilding")!=null)
				{
				GameObject.Find("leftBuilding").GetComponent<Button>().interactable=true;
				}
				if(GameObject.Find("rightBuilding")!=null)
				{
				GameObject.Find("rightBuilding").GetComponent<Button>().interactable=true;
				}
				EmpireManager._instance.trainingGround.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
				EmpireManager._instance.trainingGround.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
				EmpireManager._instance.trainingGround.foodText2.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
				EmpireManager._instance.trainingGround.goldText2.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
				EmpireManager._instance.trainingGround.foodText3.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
				EmpireManager._instance.trainingGround.goldText3.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
				EmpireManager._instance.trainingGround.foodText4.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
				EmpireManager._instance.trainingGround.goldText4.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
				finalExperienceOfBuilding = EmpireManager._instance.trainingGround.requiredExpPerLevel[EmpireManager._instance.trainingGround.currentLevel];
				GemsText[4].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();
				EmpireManager._instance.trainingGround.upgradeFoodCostPrimary.text=EmpireManager._instance.trainingGround.foodRequiredForPrimary [EmpireManager._instance.trainingGround.currentLevel].ToString();
				EmpireManager._instance.trainingGround.upgradeGoldCostPrimary.text=EmpireManager._instance.trainingGround.foodRequiredForPrimary [EmpireManager._instance.trainingGround.currentLevel].ToString();
				EmpireManager._instance.trainingGround.upgradeGoldCostSecondary.text=EmpireManager._instance.trainingGround.foodRequiredForSecondary [EmpireManager._instance.trainingGround.currentLevel].ToString();
				EmpireManager._instance.trainingGround.upgradeFoodCostSecondary.text=EmpireManager._instance.trainingGround.foodRequiredForSecondary [EmpireManager._instance.trainingGround.currentLevel].ToString();
				float tempTimer=EmpireManager._instance.trainingGround.timeRequiredPerLevel [EmpireManager._instance.trainingGround.currentLevel]*3600f/60;
				EmpireManager._instance.trainingGround.upgradeTimePrimary.text=tempTimer.ToString();
				int currentVal = EmpireManager._instance.trainingGround.finalValue1[EmpireManager._instance.trainingGround.currentLevel];
				int finalVal = EmpireManager._instance.trainingGround.finalValue1[EmpireManager._instance.trainingGround.currentLevel+1];
				loadingScene.Instance.GroundNow.text=currentVal.ToString();
				loadingScene.Instance.GroundNew.text=finalVal.ToString();

				loadingScene.Instance.GroundNowSkill.text=currentVal.ToString();
				loadingScene.Instance.GroundNewSkill.text=finalVal.ToString();
				loadingScene.Instance.trainingGroundLevel.text="Lvl "+(EmpireManager._instance.trainingGround.currentLevel+1).ToString();
				buttonName="traningGround";
				deactivateBuildings(4);

			}
			else if(Mathf.Approximately(scrollButton.value ,.5555555f))
			{
				GameObject.Find("leftBuilding").GetComponent<Button>().interactable=true;
				GameObject.Find("rightBuilding").GetComponent<Button>().interactable=true;
				EmpireManager._instance.goldMine.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
				EmpireManager._instance.goldMine.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
				finalExperienceOfBuilding = EmpireManager._instance.goldMine.requiredExpPerLevel[EmpireManager._instance.goldMine.currentLevel];
				GemsText[1].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();
				EmpireManager._instance.goldMine.upgradeFoodCostPrimary.text=EmpireManager._instance.goldMine.foodRequiredForPrimary [EmpireManager._instance.goldMine.currentLevel].ToString();
				EmpireManager._instance.goldMine.upgradeGoldCostPrimary.text=EmpireManager._instance.goldMine.foodRequiredForPrimary [EmpireManager._instance.goldMine.currentLevel].ToString();
				EmpireManager._instance.goldMine.upgradeGoldCostSecondary.text=EmpireManager._instance.goldMine.foodRequiredForSecondary [EmpireManager._instance.goldMine.currentLevel].ToString();
				EmpireManager._instance.goldMine.upgradeFoodCostSecondary.text=EmpireManager._instance.goldMine.foodRequiredForSecondary [EmpireManager._instance.goldMine.currentLevel].ToString();
				float tempTimer=EmpireManager._instance.goldMine.timeRequiredPerLevel [EmpireManager._instance.goldMine.currentLevel]*3600f/60;
				EmpireManager._instance.goldMine.upgradeTimePrimary.text=tempTimer.ToString();
				int currentVal = EmpireManager._instance.goldMine.finalValue1[EmpireManager._instance.goldMine.currentLevel];
				int finalVal = EmpireManager._instance.goldMine.finalValue1[EmpireManager._instance.goldMine.currentLevel+1];
				loadingScene.Instance.goldMineNow.text=currentVal.ToString();
				loadingScene.Instance.goldMineNext.text=finalVal.ToString();
				loadingScene.Instance.goldMineLevel.text="Lvl "+(EmpireManager._instance.goldMine.currentLevel+1).ToString();
				buttonName="goldMine";
				deactivateBuildings(5);

			}
			else if(Mathf.Approximately(scrollButton.value ,.6666666f))
			{
				GameObject.Find("leftBuilding").GetComponent<Button>().interactable=true;
				GameObject.Find("rightBuilding").GetComponent<Button>().interactable=true;
				EmpireManager._instance.barn.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
				EmpireManager._instance.barn.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
				finalExperienceOfBuilding = EmpireManager._instance.barn.requiredExpPerLevel[EmpireManager._instance.barn.currentLevel];
				GemsText[0].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();
				EmpireManager._instance.barn.upgradeFoodCostPrimary.text=EmpireManager._instance.barn.foodRequiredForPrimary [EmpireManager._instance.barn.currentLevel].ToString();
				EmpireManager._instance.barn.upgradeGoldCostPrimary.text=EmpireManager._instance.barn.foodRequiredForPrimary [EmpireManager._instance.barn.currentLevel].ToString();
				EmpireManager._instance.barn.upgradeGoldCostSecondary.text=EmpireManager._instance.barn.foodRequiredForSecondary [EmpireManager._instance.barn.currentLevel].ToString();
				EmpireManager._instance.barn.upgradeFoodCostSecondary.text=EmpireManager._instance.barn.foodRequiredForSecondary [EmpireManager._instance.barn.currentLevel].ToString();
				float tempTimer=EmpireManager._instance.barn.timeRequiredPerLevel [EmpireManager._instance.barn.currentLevel]*3600f/60;
				EmpireManager._instance.barn.upgradeTimePrimary.text=tempTimer.ToString();
				int currentVal = EmpireManager._instance.barn.finalValue1[EmpireManager._instance.barn.currentLevel];
				int finalVal = EmpireManager._instance.barn.finalValue1[EmpireManager._instance.barn.currentLevel+1];
				loadingScene.Instance.barnNow.text=currentVal.ToString();
				loadingScene.Instance.barnNext.text=finalVal.ToString();
				loadingScene.Instance.barnLevel.text="Lvl "+(EmpireManager._instance.barn.currentLevel+1).ToString();
				buttonName="barn";
				deactivateBuildings(6);

			}
			else if(Mathf.Approximately(scrollButton.value ,.7777777f))
			{
				GameObject.Find("leftBuilding").GetComponent<Button>().interactable=true;
				GameObject.Find("rightBuilding").GetComponent<Button>().interactable=true;
				EmpireManager._instance.storage.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
				EmpireManager._instance.storage.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
				finalExperienceOfBuilding = EmpireManager._instance.storage.requiredExpPerLevel[EmpireManager._instance.storage.currentLevel];
				GemsText[8].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();
				EmpireManager._instance.storage.upgradeFoodCostPrimary.text=EmpireManager._instance.storage.foodRequiredForPrimary [EmpireManager._instance.storage.currentLevel].ToString();
				EmpireManager._instance.storage.upgradeGoldCostPrimary.text=EmpireManager._instance.storage.foodRequiredForPrimary [EmpireManager._instance.storage.currentLevel].ToString();
				EmpireManager._instance.storage.upgradeGoldCostSecondary.text=EmpireManager._instance.storage.foodRequiredForSecondary [EmpireManager._instance.storage.currentLevel].ToString();
				EmpireManager._instance.storage.upgradeFoodCostSecondary.text=EmpireManager._instance.storage.foodRequiredForSecondary [EmpireManager._instance.storage.currentLevel].ToString();
				float tempTimer=EmpireManager._instance.storage.timeRequiredPerLevel [EmpireManager._instance.storage.currentLevel]*3600f/60;
				EmpireManager._instance.storage.upgradeTimePrimary.text=tempTimer.ToString();
				int currentVal = EmpireManager._instance.storage.finalValue1[EmpireManager._instance.storage.currentLevel];
				int finalVal = EmpireManager._instance.storage.finalValue1[EmpireManager._instance.storage.currentLevel+1];
				loadingScene.Instance.storageNow.text=currentVal.ToString();
				loadingScene.Instance.storageNext.text=finalVal.ToString();
				loadingScene.Instance.storageLevel.text="Lvl "+(EmpireManager._instance.storage.currentLevel+1).ToString();
				buttonName="storage";
				deactivateBuildings(7);

			}
			else if(Mathf.Approximately(scrollButton.value ,.8888888f))
			{
				GameObject.Find("leftBuilding").GetComponent<Button>().interactable=true;
				GameObject.Find("rightBuilding").GetComponent<Button>().interactable=true;
				buttonName="castle";
				EmpireManager._instance.castle.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
				EmpireManager._instance.castle.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
				finalExperienceOfBuilding = EmpireManager._instance.castle.requiredExpPerLevel[EmpireManager._instance.castle.currentLevel];
				GemsText[9].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();
				EmpireManager._instance.castle.upgradeFoodCostPrimary.text=EmpireManager._instance.castle.foodRequiredForPrimary [EmpireManager._instance.castle.currentLevel].ToString();
				EmpireManager._instance.castle.upgradeGoldCostPrimary.text=EmpireManager._instance.castle.foodRequiredForPrimary [EmpireManager._instance.castle.currentLevel].ToString();
				float tempTimer=EmpireManager._instance.castle.timeRequiredPerLevel [EmpireManager._instance.castle.currentLevel]*3600f/60;
				EmpireManager._instance.castle.upgradeTimePrimary.text=tempTimer.ToString();
				loadingScene.Instance.castleLevel.text="Lvl "+(EmpireManager._instance.castle.currentLevel+1).ToString();
				int currentVal = EmpireManager._instance.castle.finalValue1[EmpireManager._instance.castle.currentLevel];
				int finalVal = EmpireManager._instance.castle.finalValue1[EmpireManager._instance.castle.currentLevel+1];
				loadingScene.Instance.castleNow.text=currentVal.ToString();
				loadingScene.Instance.castleNext.text=finalVal.ToString();
				deactivateBuildings(8);
			}
			else if(scrollButton.value==1f)
			{
//				GameObject.Find("rightBuilding").GetComponent<Button>().interactable=false;
//				GameObject.Find("leftBuilding").GetComponent<Button>().interactable=true;
				//if(GameObject.Find("leftBuilding"))
				GameObject.Find("leftBuilding").GetComponent<Button>().interactable=true;
				//if(GameObject.Find("rightBuilding"))
				GameObject.Find("rightBuilding").GetComponent<Button>().interactable=false;
				EmpireManager._instance.gate.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
				EmpireManager._instance.gate.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
				finalExperienceOfBuilding = EmpireManager._instance.gate.requiredExpPerLevel[EmpireManager._instance.gate.currentLevel];
				GemsText[14].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();
				EmpireManager._instance.gate.upgradeFoodCostPrimary.text=EmpireManager._instance.gate.foodRequiredForPrimary [EmpireManager._instance.gate.currentLevel].ToString();
				EmpireManager._instance.gate.upgradeGoldCostPrimary.text=EmpireManager._instance.gate.foodRequiredForPrimary [EmpireManager._instance.gate.currentLevel].ToString();
				EmpireManager._instance.gate.upgradeGoldCostSecondary.text=EmpireManager._instance.gate.foodRequiredForSecondary [EmpireManager._instance.gate.currentLevel].ToString();
				EmpireManager._instance.gate.upgradeFoodCostSecondary.text=EmpireManager._instance.gate.foodRequiredForSecondary [EmpireManager._instance.gate.currentLevel].ToString();
				float tempTimer=EmpireManager._instance.gate.timeRequiredPerLevel [EmpireManager._instance.gate.currentLevel]*3600f/60;
				EmpireManager._instance.gate.upgradeTimePrimary.text=tempTimer.ToString();
				int currentVal = EmpireManager._instance.gate.finalValue1[EmpireManager._instance.gate.currentLevel];
				int finalVal = EmpireManager._instance.gate.finalValue1[EmpireManager._instance.gate.currentLevel+1];
				loadingScene.Instance.gateNow.text=currentVal.ToString();
				loadingScene.Instance.gateNext.text=finalVal.ToString();
				loadingScene.Instance.gateLevel.text="Lvl "+(EmpireManager._instance.gate.currentLevel+1).ToString();
//				loadingScene.Instance.DeployedSoldiers.text=PlayerParameters._instance.myPlayerParameter.currentlyDeployedSoldiers.ToString ()+"/"+PlayerParameters._instance.myPlayerParameter.maxDeployedSoldiers.ToString ();
//				loadingScene.Instance.AvailableSoldiers.text=PlayerParameters._instance.myPlayerParameter.currentlyAvailableSoldiers.ToString ()+"/"+EmpireManager._instance.barracks.finalValue1[EmpireManager._instance.barracks.currentLevel].ToString();
				buttonName="gate";
				deactivateBuildings(9);

			}

		}

		else
		{

		}

		if(isInterrogate==true)
		{
			interroPlay.GetComponent<Image>().fillAmount-=1.0f*Time.deltaTime/0.2f/60;

			if(interroPlay.GetComponent<Image>().fillAmount==0)
			{
				interroGatorCard.interactable=false;
				isInterrogate=false;
			}
		}
		else

		{

		}
	}


	public void CastleButton()
	{
		buttonName="castle";
		clickObj="9";
		clickOnBuilding=true;

		if (PlayerPrefs.GetString ("updatingStorage") == tempButtonStorage)
		{
			int clickAbleCatle = int.Parse (tempButtonStorage);
			GameObject[] deleteCards = GameObject.FindGameObjectsWithTag ("updateCards");
			for (int j = 0; j < deleteCards.Length; j ++)
			{
				deleteCards [j-clickAbleCatle].GetComponent<Button> ().interactable = false;



			}
		}
		else
		{
			GameObject[] deleteCards = GameObject.FindGameObjectsWithTag ("updateCards");
			for (int j = 0; j < deleteCards.Length; j ++)
			{
				deleteCards [j].GetComponent<Button> ().interactable = false;
			}

		}
		currentExperienceOfBuilding = EmpireManager._instance.castle.currentExp;
		EmpireManager._instance.castle.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
		EmpireManager._instance.castle.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
		EmpireManager._instance.castle.upgradeFoodCostPrimary.text=EmpireManager._instance.castle.foodRequiredForPrimary [EmpireManager._instance.storage.currentLevel].ToString();
		EmpireManager._instance.castle.upgradeGoldCostPrimary.text=EmpireManager._instance.castle.foodRequiredForPrimary [EmpireManager._instance.storage.currentLevel].ToString();
		finalExperienceOfBuilding = EmpireManager._instance.castle.requiredExpPerLevel[EmpireManager._instance.castle.currentLevel];
		GemsText[9].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();
		float tempTimer=EmpireManager._instance.castle.timeRequiredPerLevel [EmpireManager._instance.castle.currentLevel]*3600f/60;
		EmpireManager._instance.castle.upgradeTimePrimary.text = tempTimer.ToString ();

//		for(int i=0;i<clickedBuutons.Length;i++)
//		{
//			clickedBuutons[i].interactable=false;
//		}
		scrollButton.value=.8888888f;
		Castle.SetActive(true);
		scrollWindows.SetActive(true);

	}






	public void CastleButtonExit()
	{
		clickOnBuilding=false;

//		for(int i=0;i<clickedBuutons.Length;i++)
//		{
//			clickedBuutons[i].interactable=true;
//		}
		Castle.SetActive(false);
		scrollWindows.SetActive(false);

	}


	public void StorageButton()
	{
		buttonName="storage";
		clickObj="8";
		clickOnBuilding=true;
		currentExperienceOfBuilding = EmpireManager._instance.storage.currentExp;
		finalExperienceOfBuilding = EmpireManager._instance.storage.requiredExpPerLevel[EmpireManager._instance.storage.currentLevel];
		EmpireManager._instance.storage.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
		EmpireManager._instance.storage.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
		GemsText[8].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();
		EmpireManager._instance.storage.upgradeFoodCostPrimary.text=EmpireManager._instance.storage.foodRequiredForPrimary [EmpireManager._instance.storage.currentLevel].ToString();
		EmpireManager._instance.storage.upgradeGoldCostPrimary.text=EmpireManager._instance.storage.foodRequiredForPrimary [EmpireManager._instance.storage.currentLevel].ToString();
		EmpireManager._instance.storage.upgradeGoldCostSecondary.text=EmpireManager._instance.storage.foodRequiredForSecondary [EmpireManager._instance.storage.currentLevel].ToString();
		EmpireManager._instance.storage.upgradeFoodCostSecondary.text=EmpireManager._instance.storage.foodRequiredForSecondary [EmpireManager._instance.storage.currentLevel].ToString();
		float tempTimer=EmpireManager._instance.storage.timeRequiredPerLevel [EmpireManager._instance.storage.currentLevel]*3600f/60;
		EmpireManager._instance.storage.upgradeTimePrimary.text = tempTimer.ToString ();
//		for(int i=0;i<clickedBuutons.Length;i++)
//		{
//			clickedBuutons[i].interactable=false;
//		}

		scrollButton.value=.7777777f;
		Storage.SetActive(true);
		scrollWindows.SetActive(true);

	}
	public void StorageButtonExit()
	{
		clickOnBuilding=false;

//		for(int i=0;i<clickedBuutons.Length;i++)
//		{
//			clickedBuutons[i].interactable=true;
//		}
		Storage.SetActive(false);
		scrollWindows.SetActive(false);

	}


	public void BarnButton()
	{
		clickOnBuilding=true;
		buttonName="barn";
		currentExperienceOfBuilding = EmpireManager._instance.barn.currentExp;
		finalExperienceOfBuilding = EmpireManager._instance.barn.requiredExpPerLevel[EmpireManager._instance.barn.currentLevel];
		EmpireManager._instance.barn.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
		EmpireManager._instance.barn.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
		GemsText[0].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();
		EmpireManager._instance.barn.upgradeFoodCostPrimary.text=EmpireManager._instance.barn.foodRequiredForPrimary [EmpireManager._instance.barn.currentLevel].ToString();
		EmpireManager._instance.barn.upgradeGoldCostPrimary.text=EmpireManager._instance.barn.foodRequiredForPrimary [EmpireManager._instance.barn.currentLevel].ToString();
		EmpireManager._instance.barn.upgradeGoldCostSecondary.text=EmpireManager._instance.barn.foodRequiredForSecondary [EmpireManager._instance.barn.currentLevel].ToString();
		EmpireManager._instance.barn.upgradeFoodCostSecondary.text=EmpireManager._instance.barn.foodRequiredForSecondary [EmpireManager._instance.barn.currentLevel].ToString();
		float tempTimer=EmpireManager._instance.barn.timeRequiredPerLevel [EmpireManager._instance.barn.currentLevel]*3600f/60;
		EmpireManager._instance.barn.upgradeTimePrimary.text = tempTimer.ToString ();
//		for(int i=0;i<clickedBuutons.Length;i++)
//		{
//			clickedBuutons[i].interactable=false;
//		}
		clickObj="7";

//		for(int i=0;i<clickedBuutons.Length;i++)
//		{
//			clickedBuutons[i].interactable=false;
//		}
		scrollButton.value=.6666666f;
		Barn.SetActive(true);
		scrollWindows.SetActive(true);

	}
	public void BarnButtonExit()
	{
		clickOnBuilding=false;

//		for(int i=0;i<clickedBuutons.Length;i++)
//		{
//			clickedBuutons[i].interactable=true;
//		}
		Barn.SetActive(false);
		scrollWindows.SetActive(false);

	}

	public void GoldButton()
	{
		clickObj="6";
		buttonName="goldMine";
		clickOnBuilding=true;
		currentExperienceOfBuilding = EmpireManager._instance.goldMine.currentExp;
		finalExperienceOfBuilding = EmpireManager._instance.goldMine.requiredExpPerLevel[EmpireManager._instance.goldMine.currentLevel];
		EmpireManager._instance.goldMine.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
		EmpireManager._instance.goldMine.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
		GemsText[1].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();
		EmpireManager._instance.goldMine.upgradeFoodCostPrimary.text=EmpireManager._instance.goldMine.foodRequiredForPrimary [EmpireManager._instance.goldMine.currentLevel].ToString();
		EmpireManager._instance.goldMine.upgradeGoldCostPrimary.text=EmpireManager._instance.goldMine.foodRequiredForPrimary [EmpireManager._instance.goldMine.currentLevel].ToString();
		EmpireManager._instance.goldMine.upgradeGoldCostSecondary.text=EmpireManager._instance.goldMine.foodRequiredForSecondary [EmpireManager._instance.goldMine.currentLevel].ToString();
		EmpireManager._instance.goldMine.upgradeFoodCostSecondary.text=EmpireManager._instance.goldMine.foodRequiredForSecondary [EmpireManager._instance.goldMine.currentLevel].ToString();
		float tempTimer=EmpireManager._instance.goldMine.timeRequiredPerLevel [EmpireManager._instance.goldMine.currentLevel]*3600f/60;
		EmpireManager._instance.goldMine.upgradeTimePrimary.text = tempTimer.ToString ();



//		for(int i=0;i<clickedBuutons.Length;i++)
//		{
//			clickedBuutons[i].interactable=false;
//		}
		scrollButton.value=.5555555f;
		GoldMine.SetActive(true);
		scrollWindows.SetActive(true);

	}

	public void GoldButtonExit()
	{
		clickOnBuilding=false;

//		for(int i=0;i<clickedBuutons.Length;i++)
//		{
//			clickedBuutons[i].interactable=true;
//		}
		GoldMine.SetActive(false);
		scrollWindows.SetActive(false);

	}
	public void TrainingButton()
	{
		clickObj="5";
		buttonName="traningGround";
		clickOnBuilding=true;
		currentExperienceOfBuilding = EmpireManager._instance.trainingGround.currentExp;
		finalExperienceOfBuilding = EmpireManager._instance.trainingGround.requiredExpPerLevel[EmpireManager._instance.trainingGround.currentLevel];
		EmpireManager._instance.trainingGround.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
		EmpireManager._instance.trainingGround.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
		EmpireManager._instance.trainingGround.foodText2.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
		EmpireManager._instance.trainingGround.goldText2.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
		EmpireManager._instance.trainingGround.foodText3.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
		EmpireManager._instance.trainingGround.goldText3.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
		EmpireManager._instance.trainingGround.foodText4.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
		EmpireManager._instance.trainingGround.goldText4.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
		GemsText[4].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();
		GemsText[10].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();
		GemsText[11].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();
		GemsText[12].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();


		EmpireManager._instance.trainingGround.upgradeFoodCostPrimary.text=EmpireManager._instance.trainingGround.foodRequiredForPrimary [EmpireManager._instance.trainingGround.currentLevel].ToString();
		EmpireManager._instance.trainingGround.upgradeGoldCostPrimary.text=EmpireManager._instance.trainingGround.foodRequiredForPrimary [EmpireManager._instance.trainingGround.currentLevel].ToString();
		EmpireManager._instance.trainingGround.upgradeGoldCostSecondary.text=EmpireManager._instance.trainingGround.foodRequiredForSecondary [EmpireManager._instance.trainingGround.currentLevel].ToString();
		EmpireManager._instance.trainingGround.upgradeFoodCostSecondary.text=EmpireManager._instance.trainingGround.foodRequiredForSecondary [EmpireManager._instance.trainingGround.currentLevel].ToString();
		float tempTimer=EmpireManager._instance.trainingGround.timeRequiredPerLevel [EmpireManager._instance.trainingGround.currentLevel]*3600f/60;
		EmpireManager._instance.trainingGround.upgradeTimePrimary.text = tempTimer.ToString ();
//		for(int i=0;i<clickedBuutons.Length;i++)
//		{
//			clickedBuutons[i].interactable=false;
//		}
		scrollButton.value=.4444444f;
		TrainingGround.SetActive(true);
		scrollWindows.SetActive(true);

	}
	public void TrainingButtonExit()
	{
		clickOnBuilding=false;

//		for(int i=0;i<clickedBuutons.Length;i++)
//		{
//			clickedBuutons[i].interactable=true;
//		}
		TrainingGround.SetActive(false);
		scrollWindows.SetActive(false);

	}
	public void PrisonButton()
	{
		clickObj="4";
		buttonName="prison";
		clickOnBuilding=true;
		currentExperienceOfBuilding = EmpireManager._instance.prison.currentExp;
		finalExperienceOfBuilding = EmpireManager._instance.prison.requiredExpPerLevel[EmpireManager._instance.prison.currentLevel];
		EmpireManager._instance.prison.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
		EmpireManager._instance.prison.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
		GemsText[13].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();
		EmpireManager._instance.prison.upgradeFoodCostPrimary.text=EmpireManager._instance.prison.foodRequiredForPrimary [EmpireManager._instance.prison.currentLevel].ToString();
		EmpireManager._instance.prison.upgradeGoldCostPrimary.text=EmpireManager._instance.prison.foodRequiredForPrimary [EmpireManager._instance.prison.currentLevel].ToString();
		EmpireManager._instance.prison.upgradeGoldCostSecondary.text=EmpireManager._instance.prison.foodRequiredForSecondary [EmpireManager._instance.prison.currentLevel].ToString();
		EmpireManager._instance.prison.upgradeFoodCostSecondary.text=EmpireManager._instance.prison.foodRequiredForSecondary [EmpireManager._instance.prison.currentLevel].ToString();
		float tempTimer=EmpireManager._instance.prison.timeRequiredPerLevel [EmpireManager._instance.prison.currentLevel]*3600f/60;
		EmpireManager._instance.prison.upgradeTimePrimary.text = tempTimer.ToString ();
    // for(int i=0;i< prisonObj.instance.ongoingList.Count;i++)
		// {
		// 	print("ongoingList"+prisonObj.instance.ongoingList.Count);
		// `prisonObj.instance.prisonerQueue.text= "Prisoner queue: "+prisonObj.instance.ongoingList.Count+" / 5";
		// }


//		for(int i=0;i<clickedBuutons.Length;i++)
//		{
//			clickedBuutons[i].interactable=false;
//		}
		scrollButton.value=.3333333f;

		Prison.SetActive(true);
		scrollWindows.SetActive(true);

	}

	public void PrisonButtonExit()
	{
		clickOnBuilding=false;

//		for(int i=0;i<clickedBuutons.Length;i++)
//		{
//			clickedBuutons[i].interactable=true;
//		}
		Prison.SetActive(false);
		scrollWindows.SetActive(false);

	}
	public void LaboratoryButton()
	{
		clickObj="3";
		clickOnBuilding=true;
//
//		for(int i=0;i<clickedBuutons.Length;i++)
//		{
//			clickedBuutons[i].interactable=false;
//		}
		scrollButton.value=.2222222f;

		Laboratory.SetActive(true);
		scrollWindows.SetActive(true);


	}
	public void LaboratoryButtonExit()
	{
		clickOnBuilding=false;

//		for(int i=0;i<clickedBuutons.Length;i++)
//		{
//			clickedBuutons[i].interactable=true;
//		}
		Laboratory.SetActive(false);
		scrollWindows.SetActive(false);


	}

	public void KnowledgeTreeButton()
	{
		clickObj="2";
		clickOnBuilding=true;

//		for(int i=0;i<clickedBuutons.Length;i++)
//		{
//			clickedBuutons[i].interactable=false;
//		}
		scrollButton.value=.1111111f;

		knowledgeTree.SetActive(true);
		scrollWindows.SetActive(true);


	}
	public void KnowledgeTreeButtonExit()
	{
		clickOnBuilding=false;

//		for(int i=0;i<clickedBuutons.Length;i++)
//		{
//			clickedBuutons[i].interactable=true;
//		}
		knowledgeTree.SetActive(false);
		scrollWindows.SetActive(false);


	}
	public void empireScen()
	{

		loadingScene.Instance.empire();
	}
	public void cardCollections()
	{

		PlayerPrefs.SetString("empireScene","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			menuScreen.SetActive(false);
			isMneuActive=false;

		}
		else
		{

		}
		loadingScene.Instance.cardCollecton ();

	}
	public void shopScene()
	{

		PlayerPrefs.SetString("empireScene","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			menuScreen.SetActive(false);
			isMneuActive=false;

		}
		else
		{

		}
		loadingScene.Instance.shop();

	}
	public void inventory()
	{

		PlayerPrefs.SetString("empireScene","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			menuScreen.SetActive(false);
			isMneuActive=false;

		}
		else
		{

		}
		loadingScene.Instance.inventory();

	}



	public void battle()
	{

		PlayerPrefs.SetString("empireScene","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			menuScreen.SetActive(false);
			isMneuActive=false;

		}
		else
		{

		}
		loadingScene.Instance.battleScene();

	}
	public void chatClick()
	{
		//Start();

		PlayerPrefs.SetString("empireScene","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			menuScreen.SetActive(false);
			isMneuActive=false;

		}
		else
		{

		}
			loadingScene.Instance.chat();

	}
	public void quest()
	{

		PlayerPrefs.SetString("empireScene","yes");
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
		loadingScene.Instance.quest();


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
			menuScreen.SetActive(false);
			isMneuActive=false;

		}

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

	void OnEnable()
	{
		
		ResetMenu ();
		mainScroll.SetActive (true);
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
		loadingScene.Instance.main ();

	}

	public void BarracksPress()
	{
		clickObj="1";
		clickOnBuilding=true;
		buttonName = "barrack";
		currentExperienceOfBuilding = EmpireManager._instance.barracks.currentExp;
		finalExperienceOfBuilding = EmpireManager._instance.barracks.requiredExpPerLevel[EmpireManager._instance.barracks.currentLevel];
		EmpireManager._instance.barracks.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
		EmpireManager._instance.barracks.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
		GemsText[3].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();
		EmpireManager._instance.barracks.upgradeFoodCostPrimary.text=EmpireManager._instance.barracks.foodRequiredForPrimary [EmpireManager._instance.barracks.currentLevel].ToString();
		EmpireManager._instance.barracks.upgradeGoldCostPrimary.text=EmpireManager._instance.barracks.foodRequiredForPrimary [EmpireManager._instance.barracks.currentLevel].ToString();
		loadingScene.Instance.DeployedSoldiers.text=PlayerParameters._instance.myPlayerParameter.currentlyDeployedSoldiers.ToString ()+"/"+PlayerParameters._instance.myPlayerParameter.maxDeployedSoldiers.ToString ();
		loadingScene.Instance.AvailableSoldiers.text=PlayerParameters._instance.myPlayerParameter.currentlyAvailableSoldiers.ToString ()+"/"+EmpireManager._instance.barracks.finalValue1[EmpireManager._instance.barracks.currentLevel].ToString();
		EmpireManager._instance.barracks.upgradeGoldCostSecondary.text=EmpireManager._instance.goldMine.foodRequiredForSecondary [EmpireManager._instance.goldMine.currentLevel].ToString();
		EmpireManager._instance.barracks.upgradeFoodCostSecondary.text=EmpireManager._instance.goldMine.foodRequiredForSecondary [EmpireManager._instance.goldMine.currentLevel].ToString();
		float tempTimer=EmpireManager._instance.barracks.timeRequiredPerLevel [EmpireManager._instance.barracks.currentLevel]*3600f/60;
		EmpireManager._instance.barracks.upgradeTimePrimary.text = tempTimer.ToString ();

//		for(int i=0;i<clickedBuutons.Length;i++)
//		{
//			clickedBuutons[i].interactable=false;
//		}
		scrollWindows.SetActive(true);

		showBarracks.SetActive(true);

	}


	public void BarracksPressExit()
	{
		clickOnBuilding=false;

//		for(int i=0;i<clickedBuutons.Length;i++)
//		{
//			clickedBuutons[i].interactable=true;
//		}
		scrollWindows.SetActive(false);
		showBarracks.SetActive(false);

	}

	public void GatePress()
	{
		clickObj="10";
		clickOnBuilding=true;
		buttonName = "gate";
		currentExperienceOfBuilding = EmpireManager._instance.gate.currentExp;
		finalExperienceOfBuilding = EmpireManager._instance.gate.requiredExpPerLevel[EmpireManager._instance.gate.currentLevel];
		EmpireManager._instance.gate.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
		EmpireManager._instance.gate.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
		GemsText[14].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();
		EmpireManager._instance.gate.upgradeFoodCostPrimary.text=EmpireManager._instance.gate.foodRequiredForPrimary [EmpireManager._instance.gate.currentLevel].ToString();
		EmpireManager._instance.gate.upgradeGoldCostPrimary.text=EmpireManager._instance.gate.foodRequiredForPrimary [EmpireManager._instance.gate.currentLevel].ToString();
		EmpireManager._instance.gate.upgradeGoldCostSecondary.text=EmpireManager._instance.gate.foodRequiredForSecondary [EmpireManager._instance.gate.currentLevel].ToString();
		EmpireManager._instance.gate.upgradeFoodCostSecondary.text=EmpireManager._instance.gate.foodRequiredForSecondary [EmpireManager._instance.gate.currentLevel].ToString();
		float tempTimer=EmpireManager._instance.gate.timeRequiredPerLevel [EmpireManager._instance.gate.currentLevel]*3600f/60;
		EmpireManager._instance.gate.upgradeTimePrimary.text = tempTimer.ToString ();



//		for(int i=0;i<clickedBuutons.Length;i++)
//		{
//			clickedBuutons[i].interactable=false;
//		}
		scrollWindows.SetActive(true);
		showGate.SetActive(true);

	}

	public void GatePressExit()
	{
		clickOnBuilding=false;

//		for(int i=0;i<clickedBuutons.Length;i++)
//		{
//			clickedBuutons[i].interactable=true;
//		}
		scrollWindows.SetActive(false);
		showGate.SetActive(false);

	}

	public void backButton()
	{


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

			}

		}


	}
	public void community()
	{

		PlayerPrefs.SetString("empireScene","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			menuScreen.SetActive(false);
			isMneuActive=false;

		}
		else
		{

		}
		loadingScene.Instance.community ();


	}
	public void trade()
	{
		//Start();

		PlayerPrefs.SetString("empireScene","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			menuScreen.SetActive(false);
			isMneuActive=false;

		}
		else
		{

		}
		loadingScene.Instance.trade();

	}
	public void recruiterClick()
	{
		soldierRecruit.SetActive(true);
		showBarracks.SetActive(false);
		mainScroll.SetActive(false);
		selectedRecruiter=0;


	}
	public void backFromRecruiter()
	{
		soldierRecruit.SetActive(false);
		showBarracks.SetActive(true);
		mainScroll.SetActive(true);

	}

	public void ShowAllCards()
	{
		GameObject[] allCardButtons = GameObject.FindGameObjectsWithTag ("updateCards");
		SetSelectedCardDataInCardSelection();
		for (int j = 0; j < allCardButtons.Length; j ++)
		{
			int cardNo = int.Parse (allCardButtons [j].name);

			if (CardsManager._instance.IsPlayercardLocked (cardNo, false)) {
				allCardButtons [j].GetComponent<Button> ().interactable = false;
				allCardButtons [j].GetComponentInChildren<Text>().text="LOCKED";
			} else {
				allCardButtons [j].GetComponent<Button> ().interactable = true;
				allCardButtons [j].GetComponentInChildren<Text>().text="";
			}

//			if(EmpireManager._instance.castle.primaryCardNo != cardNo && EmpireManager._instance.storage.primaryCardNo != cardNo && EmpireManager._instance.storage.secondaryCardNo != cardNo &&
//			   EmpireManager._instance.barn.primaryCardNo != cardNo && EmpireManager._instance.barn.secondaryCardNo != cardNo &&
//			   EmpireManager._instance.goldMine.primaryCardNo != cardNo && EmpireManager._instance.goldMine.secondaryCardNo != cardNo &&
//				EmpireManager._instance.barracks.primaryCardNo != cardNo && EmpireManager._instance.barracks.secondaryCardNo != cardNo &&
//			   EmpireManager._instance.trainingGround.primaryCardNo != cardNo && EmpireManager._instance.prison.primaryCardNo != cardNo
//				&& EmpireManager._instance.prison.secondaryCardNo != cardNo && EmpireManager._instance.gate.secondaryCardNo != cardNo &&
//				EmpireManager._instance.gate.secondaryCardNo != cardNo)
//				//&& EmpireManager._instance.trainingGround.secondaryCardNo != cardNo)
//
//
//			{
//				allCardButtons [j].GetComponent<Button> ().interactable = true;
//				allCardButtons [j].GetComponentInChildren<Text>().text="";
//			}
//			else
//			{
//
//				allCardButtons [j].GetComponent<Button> ().interactable = false;
//				allCardButtons [j].GetComponentInChildren<Text>().text="LOCKED";
//
//			}
		}

		//============ CASTLE UPGRADE =============
		if (buttonName == "castle") {
			if (selectedCard == 1) {
				if (lockButton != null)
					lockButton.text = "";
				selectedCards.Remove (lockButtonName);
				selectedCard = 0;
			}



			buildingUpdateFoodInCardSelectionLayout.text = EmpireManager._instance.castle.foodRequiredForPrimary [EmpireManager._instance.castle.currentLevel].ToString ();
			buildingUpdateGoldInCardSelectionLayout.text = EmpireManager._instance.castle.foodRequiredForPrimary [EmpireManager._instance.castle.currentLevel].ToString ();
			float timerReqd = EmpireManager._instance.castle.timeRequiredPerLevel [EmpireManager._instance.castle.currentLevel] * 3600;
			int minutesText = Mathf.FloorToInt (timerReqd / 60);
			int secondsText = Mathf.FloorToInt (timerReqd % 60);
			buildingUpdateTimeInCardSelectionLayout.text = minutesText.ToString ("00") + ":" + secondsText.ToString ("00");
		}

		//============ STORAGE UPGRADE =============

		else if (buttonName == "storage") {
			if (selectedCardStorage == 1) {
				if (lockButtonStorage != null) {
					lockButtonStorage.text = "";
					selectedCards.Remove (lockButtonNameStorage);
				}
				selectedCardStorage = 0;
			}

			if (!wentFromSecondary) {
				buildingUpdateFoodInCardSelectionLayout.text = EmpireManager._instance.storage.foodRequiredForPrimary [EmpireManager._instance.storage.currentLevel].ToString ();
				buildingUpdateGoldInCardSelectionLayout.text = EmpireManager._instance.storage.foodRequiredForPrimary [EmpireManager._instance.storage.currentLevel].ToString ();
				float timerReqd = EmpireManager._instance.storage.timeRequiredPerLevel [EmpireManager._instance.storage.currentLevel] * 3600;
				int minutesText = Mathf.FloorToInt (timerReqd / 60);
				int secondsText = Mathf.FloorToInt (timerReqd % 60);
				buildingUpdateTimeInCardSelectionLayout.text = minutesText.ToString ("00") + ":" + secondsText.ToString ("00");
			} else {
				buildingUpdateFoodInCardSelectionLayout.text = ((int)EmpireManager._instance.storage.foodRequiredForSecondary [EmpireManager._instance.storage.currentLevel]).ToString ();
				buildingUpdateGoldInCardSelectionLayout.text = ((int)EmpireManager._instance.storage.foodRequiredForSecondary [EmpireManager._instance.storage.currentLevel]).ToString ();
				buildingUpdateTimeInCardSelectionLayout.text = "60:00";
			}

		}

		else if (buttonName == "gate") {
			if (selectedCardGate == 1) {
				if (lockButtonGate != null) {
					lockButtonGate.text = "";
					selectedCards.Remove (lockButtonNameGate);
				}
				selectedCardGate = 0;
			}

			if (!wentFromSecondary) {
				buildingUpdateFoodInCardSelectionLayout.text = EmpireManager._instance.gate.foodRequiredForPrimary [EmpireManager._instance.gate.currentLevel].ToString ();
				buildingUpdateGoldInCardSelectionLayout.text = EmpireManager._instance.gate.foodRequiredForPrimary [EmpireManager._instance.gate.currentLevel].ToString ();
				float timerReqd = EmpireManager._instance.gate.timeRequiredPerLevel [EmpireManager._instance.gate.currentLevel] * 3600;
				int minutesText = Mathf.FloorToInt (timerReqd / 60);
				int secondsText = Mathf.FloorToInt (timerReqd % 60);
				buildingUpdateTimeInCardSelectionLayout.text = minutesText.ToString ("00") + ":" + secondsText.ToString ("00");
			} else {
				buildingUpdateFoodInCardSelectionLayout.text = ((int)EmpireManager._instance.gate.foodRequiredForSecondary [EmpireManager._instance.gate.currentLevel]).ToString ();
				buildingUpdateGoldInCardSelectionLayout.text = ((int)EmpireManager._instance.gate.foodRequiredForSecondary [EmpireManager._instance.gate.currentLevel]).ToString ();
				buildingUpdateTimeInCardSelectionLayout.text = "60:00";
			}

		}
		//------------  barn --------
		else if (buttonName == "barn") {
			if (selectedCardBarn == 1) {
				if (lockButtonBarn != null) {
					lockButtonBarn.text = "";
					selectedCards.Remove (lockButtonNameBarn);
				}
				selectedCardBarn = 0;
			}
      if (selectedCardBarnSecondary == 1) {
        if (lockButtonBarn != null) {
          lockButtonBarn.text = "";
          selectedCards.Remove (lockButtonNameBarnSecondary);
        }
        selectedCardBarnSecondary = 0;
      }
			if (!wentFromSecondary) {
				buildingUpdateFoodInCardSelectionLayout.text = EmpireManager._instance.barn.foodRequiredForPrimary [EmpireManager._instance.barn.currentLevel].ToString ();
				buildingUpdateGoldInCardSelectionLayout.text = EmpireManager._instance.barn.foodRequiredForPrimary [EmpireManager._instance.barn.currentLevel].ToString ();
				float timerReqd = EmpireManager._instance.barn.timeRequiredPerLevel [EmpireManager._instance.barn.currentLevel] * 3600;
				int minutesText = Mathf.FloorToInt (timerReqd / 60);
				int secondsText = Mathf.FloorToInt (timerReqd % 60);
				buildingUpdateTimeInCardSelectionLayout.text = minutesText.ToString ("00") + ":" + secondsText.ToString ("00");
			} else {
				buildingUpdateFoodInCardSelectionLayout.text = ((int)EmpireManager._instance.barn.foodRequiredForSecondary [EmpireManager._instance.barn.currentLevel]).ToString ();
				buildingUpdateGoldInCardSelectionLayout.text = ((int)EmpireManager._instance.barn.foodRequiredForSecondary [EmpireManager._instance.barn.currentLevel]).ToString ();
				buildingUpdateTimeInCardSelectionLayout.text = "60:00";
			}

		}
		//--------   GOLD MINE UPDATE ---------------

		else if (buttonName == "goldMine") {
			if (selectedCardGoldMine == 1) {
				if (lockButtonGoldMine != null) {
					lockButtonGoldMine.text = "";
					selectedCards.Remove (lockButtonNameGoldMine);
				}
				selectedCardGoldMine = 0;
			}
      if (selectedCardGoldMineSecondary == 1) {
        if (lockButtonGoldMine != null) {
          lockButtonGoldMine.text = "";
          selectedCards.Remove (lockButtonGoldMineSecondary);
        }
        selectedCardGoldMineSecondary = 0;
      }

			if (!wentFromSecondary) {
				buildingUpdateFoodInCardSelectionLayout.text = EmpireManager._instance.goldMine.foodRequiredForPrimary [EmpireManager._instance.goldMine.currentLevel].ToString ();
				buildingUpdateGoldInCardSelectionLayout.text = EmpireManager._instance.goldMine.foodRequiredForPrimary [EmpireManager._instance.goldMine.currentLevel].ToString ();
				float timerReqd = EmpireManager._instance.goldMine.timeRequiredPerLevel [EmpireManager._instance.goldMine.currentLevel] * 3600;
				int minutesText = Mathf.FloorToInt (timerReqd / 60);
				int secondsText = Mathf.FloorToInt (timerReqd % 60);
				buildingUpdateTimeInCardSelectionLayout.text = minutesText.ToString ("00") + ":" + secondsText.ToString ("00");
			} else {
				buildingUpdateFoodInCardSelectionLayout.text = ((int)EmpireManager._instance.goldMine.foodRequiredForSecondary [EmpireManager._instance.goldMine.currentLevel]).ToString ();
				buildingUpdateGoldInCardSelectionLayout.text = ((int)EmpireManager._instance.goldMine.foodRequiredForSecondary [EmpireManager._instance.goldMine.currentLevel]).ToString ();
				buildingUpdateTimeInCardSelectionLayout.text = "60:00";
			}

		}
		//--------------  BARRACKS ------------
		else if (buttonName == "barrack") {
			if (selectedCardBarrakcs == 1) {
				if (lockBarracks != null) {
					lockBarracks.text = "";
					selectedCards.Remove (lockButtonNameBarrakcs);
				}
				selectedCardBarrakcs = 0;
			}
      if (selectedCardBarrakcsSecondary == 1) {
				if (lockBarracks != null) {
					lockBarracks.text = "";
					selectedCards.Remove (lockButtonBarrakcsSecondary);
				}
				selectedCardBarrakcsSecondary = 0;
			}

			if (!wentFromSecondary) {
				buildingUpdateFoodInCardSelectionLayout.text = EmpireManager._instance.barracks.foodRequiredForPrimary [EmpireManager._instance.barracks.currentLevel].ToString ();
				buildingUpdateGoldInCardSelectionLayout.text = EmpireManager._instance.barracks.foodRequiredForPrimary [EmpireManager._instance.barracks.currentLevel].ToString ();
				float timerReqd = EmpireManager._instance.barracks.timeRequiredPerLevel [EmpireManager._instance.barracks.currentLevel] * 180;
				int minutesText = Mathf.FloorToInt (timerReqd / 60);
				int secondsText = Mathf.FloorToInt (timerReqd % 60);
				buildingUpdateTimeInCardSelectionLayout.text = minutesText.ToString ("00") + ":" + secondsText.ToString ("00");
			} else {
				buildingUpdateFoodInCardSelectionLayout.text = ((int)EmpireManager._instance.barracks.foodRequiredForSecondary [EmpireManager._instance.barracks.currentLevel]).ToString ();
				buildingUpdateGoldInCardSelectionLayout.text = ((int)EmpireManager._instance.barracks.foodRequiredForSecondary [EmpireManager._instance.barracks.currentLevel]).ToString ();
				buildingUpdateTimeInCardSelectionLayout.text = "03:00";
			}

		}
    else if (buttonName == "prison")
		{
			prisonObj.instance. prisonShownCards();

		}


		else if (buttonName == "traningGround")
		{
			if (selectedCardGround == 1)
			{
				if(lockButtonGround != null)
				{
					lockButtonGround.text = "";
					selectedCards.Remove (lockButtonNameGround);
				}
				selectedCardGround = 0;
			}

			if(!wentFromSecondary)
			{
				buildingUpdateFoodInCardSelectionLayout.text = EmpireManager._instance.trainingGround.foodRequiredForPrimary[EmpireManager._instance.trainingGround.currentLevel].ToString ();
				buildingUpdateGoldInCardSelectionLayout.text = EmpireManager._instance.trainingGround.foodRequiredForPrimary[EmpireManager._instance.trainingGround.currentLevel].ToString ();
				buildingUpdateFoodInCardSelectionLayoutLvl.text = EmpireManager._instance.trainingGround.foodRequiredForPrimary[EmpireManager._instance.trainingGround.currentLevel].ToString ();
				buildingUpdateGoldInCardSelectionLayoutLvl.text = EmpireManager._instance.trainingGround.foodRequiredForPrimary[EmpireManager._instance.trainingGround.currentLevel].ToString ();
				expTimeInCardSelectionLayoutLvl.text =  (EmpireManager._instance.trainingGround.requiredExpPerLevel[EmpireManager._instance.trainingGround.currentLevel]).ToString();
				float timerReqd = EmpireManager._instance.barracks.timeRequiredPerLevel[EmpireManager._instance.trainingGround.currentLevel]*3600;
				int minutesText = Mathf.FloorToInt( timerReqd/ 60);
				int secondsText = Mathf.FloorToInt(timerReqd % 60);
				buildingUpdateTimeInCardSelectionLayout.text = minutesText.ToString("00")+":"+secondsText.ToString("00");
			}
			else
			{
				buildingUpdateFoodInCardSelectionLayout.text = "0";
					//((int)EmpireManager._instance.trainingGround.foodRequiredForSecondary[EmpireManager._instance.trainingGround.currentLevel]).ToString ();
				buildingUpdateGoldInCardSelectionLayout.text = "0";
						//((int)EmpireManager._instance.trainingGround.foodRequiredForSecondary[EmpireManager._instance.trainingGround.currentLevel]).ToString ();
				buildingUpdateFoodInCardSelectionLayoutLvl.text = ((int)EmpireManager._instance.trainingGround.foodRequiredForSecondary[EmpireManager._instance.trainingGround.currentLevel]/2).ToString ();
				buildingUpdateGoldInCardSelectionLayoutLvl.text = ((int)EmpireManager._instance.trainingGround.foodRequiredForSecondary[EmpireManager._instance.trainingGround.currentLevel]/2).ToString ();
				buildingUpdateTimeInCardSelectionLayout.text = "00:00";



			}







		}

	}

	public void buildingUpgradeClick()
	{
    empireSceneNew.instance.buttonNameSecond="1313";
    confirmButton.interactable=false;
		if (buttonName == "castle") {
			int foodREquirementCastle = EmpireManager._instance.castle.foodRequiredForPrimary [EmpireManager._instance.castle.currentLevel];
			if (PlayerParameters._instance.myPlayerParameter.wheat >= foodREquirementCastle && PlayerParameters._instance.myPlayerParameter.gold >= foodREquirementCastle) {
				buildingUpgradeLayout.SetActive (true);
				wentFromSecondary = false;
				ShowAllCards ();
			} else {
				insufficientGems.SetActive (true);
				insufficientGems.transform.GetChild (4).GetComponent<Text> ().text = "Insufficient Resources !";
			}
		} else if (buttonName == "storage") {
			if (EmpireManager._instance.storage.castleLevelRequired [EmpireManager._instance.storage.currentLevel] <= EmpireManager._instance.castle.currentLevel + 1) {
				int foodREquirementStorage = EmpireManager._instance.storage.foodRequiredForPrimary [EmpireManager._instance.storage.currentLevel];
				if (PlayerParameters._instance.myPlayerParameter.wheat >= foodREquirementStorage && PlayerParameters._instance.myPlayerParameter.gold >= foodREquirementStorage) {
					buildingUpgradeLayout.SetActive (true);
					wentFromSecondary = false;
					ShowAllCards ();
				} else {
					insufficientGems.SetActive (true);
					insufficientGems.transform.GetChild (4).GetComponent<Text> ().text = "Insufficient Resources !";
				}
			} else {
				insufficientGems.SetActive (true);
				insufficientGems.transform.GetChild (4).GetComponent<Text> ().text = "First Update Castle to Proceed !";
				print ("current level is not as level castle required");
			}
		} else if (buttonName == "barn") {
			if (EmpireManager._instance.barn.castleLevelRequired [EmpireManager._instance.barn.currentLevel] <= EmpireManager._instance.castle.currentLevel + 1) {
				int foodREquirementStorage = EmpireManager._instance.barn.foodRequiredForPrimary [EmpireManager._instance.barn.currentLevel];
				if (PlayerParameters._instance.myPlayerParameter.wheat >= foodREquirementStorage && PlayerParameters._instance.myPlayerParameter.gold >= foodREquirementStorage) {
					buildingUpgradeLayout.SetActive (true);
					wentFromSecondary = false;
					ShowAllCards ();
				} else {
					insufficientGems.SetActive (true);
					insufficientGems.transform.GetChild (4).GetComponent<Text> ().text = "Insufficient Resources !";
				}
			} else {
				insufficientGems.SetActive (true);
				insufficientGems.transform.GetChild (4).GetComponent<Text> ().text = "First Update Castle to Proceed !";


			}
		} else if (buttonName == "goldMine") {
			if (EmpireManager._instance.goldMine.castleLevelRequired [EmpireManager._instance.goldMine.currentLevel] <= EmpireManager._instance.castle.currentLevel + 1) {
				int foodREquirementStorage = EmpireManager._instance.goldMine.foodRequiredForPrimary [EmpireManager._instance.goldMine.currentLevel];
				if (PlayerParameters._instance.myPlayerParameter.wheat >= foodREquirementStorage && PlayerParameters._instance.myPlayerParameter.gold >= foodREquirementStorage) {
					buildingUpgradeLayout.SetActive (true);
					wentFromSecondary = false;
					ShowAllCards ();
				} else {
					insufficientGems.SetActive (true);
					insufficientGems.transform.GetChild (4).GetComponent<Text> ().text = "Insufficient Resources !";
				}
			} else {
				insufficientGems.SetActive (true);
				insufficientGems.transform.GetChild (4).GetComponent<Text> ().text = "First Update Castle to Proceed !";


			}
		} else if (buttonName == "barrack") {
			if (EmpireManager._instance.barracks.castleLevelRequired [EmpireManager._instance.barracks.currentLevel] <= EmpireManager._instance.castle.currentLevel + 1) {
				int foodREquirementStorage = EmpireManager._instance.barracks.foodRequiredForPrimary [EmpireManager._instance.barracks.currentLevel];
				if (PlayerParameters._instance.myPlayerParameter.wheat >= foodREquirementStorage && PlayerParameters._instance.myPlayerParameter.gold >= foodREquirementStorage) {
					buildingUpgradeLayout.SetActive (true);
					wentFromSecondary = false;
					ShowAllCards ();
				} else {
					insufficientGems.SetActive (true);
					insufficientGems.transform.GetChild (4).GetComponent<Text> ().text = "Insufficient Resources !";
				}
			} else {
				insufficientGems.SetActive (true);
				insufficientGems.transform.GetChild (4).GetComponent<Text> ().text = "First Update Castle to Proceed !";


			}
		} else if (buttonName == "traningGround") {
			if (EmpireManager._instance.trainingGround.castleLevelRequired [EmpireManager._instance.trainingGround.currentLevel] <= EmpireManager._instance.castle.currentLevel + 1) {
				int foodREquirementStorage = EmpireManager._instance.trainingGround.foodRequiredForPrimary [EmpireManager._instance.trainingGround.currentLevel];
				if (PlayerParameters._instance.myPlayerParameter.wheat >= foodREquirementStorage && PlayerParameters._instance.myPlayerParameter.gold >= foodREquirementStorage) {
					buildingUpgradeLayout.SetActive (true);
					wentFromSecondary = false;
					ShowAllCards ();
				} else {
					insufficientGems.SetActive (true);
					insufficientGems.transform.GetChild (4).GetComponent<Text> ().text = "Insufficient Resources !";
				}
			} else {
				insufficientGems.SetActive (true);
				insufficientGems.transform.GetChild (4).GetComponent<Text> ().text = "First Update Castle to Proceed !";


			}
		}
		else if (buttonName == "prison")
		{

      if (EmpireManager._instance.prison.castleLevelRequired [EmpireManager._instance.prison.currentLevel] <= EmpireManager._instance.castle.currentLevel + 1) {
				int foodREquirementStorage = EmpireManager._instance.prison.foodRequiredForPrimary [EmpireManager._instance.prison.currentLevel];
				if (PlayerParameters._instance.myPlayerParameter.wheat >= foodREquirementStorage && PlayerParameters._instance.myPlayerParameter.gold >= foodREquirementStorage) {
					buildingUpgradeLayout.SetActive (true);
					wentFromSecondary = false;
					ShowAllCards ();
				} else {
					insufficientGems.SetActive (true);
					insufficientGems.transform.GetChild (4).GetComponent<Text> ().text = "Insufficient Resources !";
				}
			} else {
				insufficientGems.SetActive (true);
				insufficientGems.transform.GetChild (4).GetComponent<Text> ().text = "First Update Castle to Proceed !";


			}
			// prisonObj.instance.updateBuildingPrison();
		}
		else if (buttonName == "gate")
		{

			if (EmpireManager._instance.gate.castleLevelRequired [EmpireManager._instance.gate.currentLevel] <= EmpireManager._instance.castle.currentLevel + 1) {
				int foodREquirementStorage = EmpireManager._instance.gate.foodRequiredForPrimary [EmpireManager._instance.gate.currentLevel];
				if (PlayerParameters._instance.myPlayerParameter.wheat >= foodREquirementStorage && PlayerParameters._instance.myPlayerParameter.gold >= foodREquirementStorage) {
					buildingUpgradeLayout.SetActive (true);
					wentFromSecondary = false;
					ShowAllCards ();
				} else {
					insufficientGems.SetActive (true);
					insufficientGems.transform.GetChild (4).GetComponent<Text> ().text = "Insufficient Resources !";
				}
			} else {
				insufficientGems.SetActive (true);
				insufficientGems.transform.GetChild (4).GetComponent<Text> ().text = "First Update Castle to Proceed !";


			}
			// prisonObj.instance.updateBuildingPrison();
		}
	}


	public void confirmSacrifice()
	{
		sacrificeDialog.SetActive (true);


	}

	public void confirmSacrificeExit()
	{
		sacrificeDialog.SetActive (false);
		confirmTrainingBtn.interactable = false;
		lockButtonNameGroundSecondary.GetComponentInChildren<Text> ().text = "";

		//SetSelectedCardDataInCardSelectionAgain(CardsManager._instance.mycards[cardIdLocked]);
		SetSelectedCardDataInCardSelection ();
		selectedCards.Remove (empireScene.instance.lockButtonNameGroundSecondary);

		selectedCardGroundSecondary = 0;
		empireSceneNew.instance.offStatTraining();


	}
	public void confirmSacrificeYes()
	{
		sacrificeDialog.SetActive (false);
    if(empireSceneNew.instance.selectionSoldiers=="army")
    {
//		lockButtonNameGroundSecondary.GetComponentInChildren<Text> ().text = "";
		int cardIdLocked = CardsManager._instance.PositionOfCardInList (int.Parse (lockButtonNameGroundSecondary.name));
		SetSelectedCardDataInCardSelectionAgain(CardsManager._instance.mycards[cardIdLocked]);
		selectedCards.Remove (lockButtonNameGroundSecondary);

		selectedCardGroundSecondary = 0;
		empireSceneNew.instance.offStatTraining();

		sacrificeCard (CardsManager._instance.mycards[sacrficeClickCardPos].card_level, sacrficeLockedCard, sacrficeClickCard,  isSuccess => {

			if (isSuccess)
			{
				loadingScene.Instance.randomCards.Remove(sacrficeClickCard);
				CardsManager._instance.mycards.RemoveAt(sacrficeClickCardPos);
				Destroy(CardsManager._instance.cardButtonOfEmpire[sacrficeClickCardPos].gameObject);
				Destroy(CardsManager._instance.cardButtonOfEmpire1[sacrficeClickCardPos].gameObject);
				Destroy(CardsManager._instance.cardButtonOfEmpire3[sacrficeClickCardPos].gameObject);
				Destroy(CardsManager._instance.cardButtonOfEmpire2[sacrficeClickCardPos].gameObject);

				CardsManager._instance.cardButtonOfEmpire.RemoveAt (sacrficeClickCardPos);
				CardsManager._instance.cardButtonOfEmpire1.RemoveAt (sacrficeClickCardPos);
				CardsManager._instance.cardButtonOfEmpire2.RemoveAt (sacrficeClickCardPos);

				CardsManager._instance.cardButtonOfEmpire3.RemoveAt (sacrficeClickCardPos);

				confirmTrainingBtn.interactable = false;


				int positionOfLockedCard = CardsManager._instance.PositionOfCardInList (sacrficeLockedCard);
				int expToSave = (CardsManager._instance.mycards[positionOfLockedCard].experience);
				string rarityXp = (CardsManager._instance.mycards[positionOfLockedCard].rarity.ToString());
				empireSceneNew.instance.startMaxLevel.text="Card Lv. "+CardsManager._instance.mycards[positionOfLockedCard].card_level.ToString();
				empireSceneNew.instance.endMaxLevel.text="Card Lv. "+(CardsManager._instance.mycards[positionOfLockedCard].card_level+1).ToString ();


			}
			else
			{

				popupFromServer.ShowPopup ("Cannot Update at this time!");
			}
		});
  }
  else if(empireSceneNew.instance.selectionSoldiers=="captive")
  {
    int cardIdLocked = CardsManager._instance.PositionOfCaptiveInList (int.Parse (lockButtonNameGroundSecondary.name));
    SetSelectedCardDataInCardSelectionAgain(CardsManager._instance.myCaptives[cardIdLocked]);
    selectedCards.Remove (lockButtonNameGroundSecondary);

    selectedCardGroundSecondary = 0;
    empireSceneNew.instance.offStatTraining();

    sacrificeCard (CardsManager._instance.myCaptives[sacrficeClickCardPos].card_level, sacrficeLockedCard, sacrficeClickCard,  isSuccess => {

      if (isSuccess)
      {
        CardsManager._instance.myCaptives.RemoveAt(sacrficeClickCardPos);

        for(int j=0;j<empireSceneNew.instance.armyButton.Length;j++)
        {
//						Destroy(empireSceneNew.instance.captivesPerConatiner[j].captivesList[clickforSkill]);
						Destroy(empireSceneNew.instance.captivesPerConatiner[j].captivesList[sacrficeClickCardPos]);
        }
        confirmTrainingBtn.interactable = false;


        int positionOfLockedCard = CardsManager._instance.PositionOfCaptiveInList (sacrficeLockedCard);
        print("positionOfLockedCard"+positionOfLockedCard);
        print("sacrficeLockedCard"+sacrficeLockedCard);
        int expToSave = (CardsManager._instance.mycards[positionOfLockedCard].experience);
        string rarityXp = (CardsManager._instance.mycards[positionOfLockedCard].rarity.ToString());
        empireSceneNew.instance.startMaxLevel.text="Card Lv. "+CardsManager._instance.mycards[positionOfLockedCard].card_level.ToString();
        empireSceneNew.instance.endMaxLevel.text="Card Lv. "+(CardsManager._instance.mycards[positionOfLockedCard].card_level+1).ToString ();
      }
      else
      {

        popupFromServer.ShowPopup ("Cannot Update at this time!");
      }
    });

  }
//

	}



	public void SetSelectedCardDataInCardSelectionAgain( CardsManager.CardParameters cardSelected)
	{
		if (empireSceneNew.instance.selectionSoldiers == "army")
    {
			int cardIdSelected = cardSelected.card_id_in_playerList;
			sacrficeClickCard = cardIdSelected;
			sacrficeClickCardPos = CardsManager._instance.PositionOfCardInList (int.Parse (lockButtonNameGroundSecondary.name));
			print ("cardId" + cardIdSelected);
		}
     else if (empireSceneNew.instance.selectionSoldiers == "captive")
     {
			int cardIdSelected = cardSelected.card_id_in_playerList;
			sacrficeClickCard = cardIdSelected;
			sacrficeClickCardPos = CardsManager._instance.PositionOfCaptiveInList (int.Parse (lockButtonNameGroundSecondary.name));
			print ("cardId" + cardIdSelected);
		}

	}
	public int sacrficeClickCard;
	public int sacrficeClickCardPos;
	public int sacrficeLockedCard;

	void SetSelectedCardDataInCardSelectionAgain2( CardsManager.CardParameters cardSelected)
	{

		int cardIdLocked = cardSelected.card_id_in_playerList;
		sacrficeLockedCard = cardIdLocked;
		print ("SelectedcardId" + cardIdLocked);
		lockButtonNameGroundSecondary.interactable = true;
		selectedCards.Remove (lockButtonNameGroundSecondary);
		selectedCardGroundSecondary = 0;
		empireSceneNew.instance.offStatTraining();


	}

	public int cardIdLocked;
	public string cardIdLockedDefault;

	public void sacrificeCard(  int cardLvl, int lockedCard ,int deleteCard, Action <bool> callback)
	{
		loader.SetActive (true);
		  NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected)
			{
				WWWForm form_time= new WWWForm ();
				string URltime = loadingScene.Instance.baseUrl;
				form_time.AddField ("tag","doAddUpdatePlayerCards");
				form_time.AddField ("user_id",PlayerDataParse._instance.playersParam.userId);
				form_time.AddField ("device_id",SystemInfo.deviceUniqueIdentifier);
				int positionOfLockedCard = CardsManager._instance.PositionOfCardInList (lockedCard);
        int positionOfSacrificedCard = 0;
        int expToadd = 0;
        if(empireSceneNew.instance.selectionSoldiers=="army")
        {
				    positionOfSacrificedCard = CardsManager._instance.PositionOfCardInList (sacrficeClickCard);
            expToadd = (int)CardsManager._instance.mycards[positionOfSacrificedCard].experience*(100+EmpireManager._instance.trainingGround.finalValue1[EmpireManager._instance.trainingGround.currentLevel])/100;
        }
        else
        {
          positionOfSacrificedCard = CardsManager._instance.PositionOfCaptiveInList (sacrficeClickCard);
          expToadd = (int)CardsManager._instance.myCaptives[positionOfSacrificedCard].experience*(100+EmpireManager._instance.trainingGround.finalValue1[EmpireManager._instance.trainingGround.currentLevel])/100;
        }
				int expToSave = (CardsManager._instance.mycards[positionOfLockedCard].experience+expToadd);
				Debug.Log ("expToSave"+expToSave);
				form_time.AddField ("card_no_in_players_list",lockedCard.ToString());
				form_time.AddField ("experience",expToSave.ToString());
				form_time.AddField ("delete_card_no_in_players_list",deleteCard.ToString());

				int myCrdLevel = CardsManager._instance.mycards[positionOfLockedCard].card_level;
				int cardAttack = CardsManager._instance.mycards[positionOfLockedCard].attack;
				int cardDefense = CardsManager._instance.mycards[positionOfLockedCard].defense;
				int cardLeaderShip = CardsManager._instance.mycards[positionOfLockedCard].leadership;
				int abc = myCrdLevel;
				int  cardLevelToChek=myCrdLevel-1;
				int globalActualCard = 0;
//				globalActualCard[CardsManager._instance.mycards[positionOfLockedCard].card_level-1]
				Debug.Log("abc = "+abc);
				string rarityXp2 = CardsManager._instance.mycards[positionOfLockedCard].rarity.ToString();
				if(rarityXp2=="Common")
				{
					globalActualCard=CardsManager._instance.starCard1Actual[cardLevelToChek];
				}
				else if(rarityXp2=="Uncommon")
				{
					globalActualCard=CardsManager._instance.starCard2Actual[cardLevelToChek];
				}
				else if(rarityXp2=="Super")
				{
					globalActualCard=CardsManager._instance.starCard3Actual[cardLevelToChek];

				}
				else if(rarityXp2=="Mega")
				{
					globalActualCard=CardsManager._instance.starCard4Actual[cardLevelToChek];
				}
				else if(rarityXp2=="Legendary")
				{
					globalActualCard=CardsManager._instance.starCard5Actual[cardLevelToChek];
				}

				if(expToSave>=globalActualCard && CardsManager._instance.mycards[positionOfLockedCard].card_level < CardsManager._instance.mycards[positionOfLockedCard].max_level)
				{
					myCrdLevel++;
					cardAttack+= Mathf.CeilToInt(0.01f* cardAttack);
					cardDefense+= Mathf.CeilToInt(0.01f* cardDefense);
					cardLeaderShip+= Mathf.CeilToInt(0.01f* cardLeaderShip);

					for(int i= abc ; i < CardsManager._instance.starCard5Actual.Count ; i++)
					{
						if(rarityXp2=="Common")
						{
							globalActualCard=CardsManager._instance.starCard1Actual[i];
						}
						else if(rarityXp2=="Uncommon")
						{
							globalActualCard=CardsManager._instance.starCard2Actual[i];
						}
						else if(rarityXp2=="Super")
						{
							globalActualCard=CardsManager._instance.starCard3Actual[i];

						}
						else if(rarityXp2=="Mega")
						{
							globalActualCard=CardsManager._instance.starCard4Actual[i];
						}
						else if(rarityXp2=="Legendary")
						{
							globalActualCard=CardsManager._instance.starCard5Actual[i];
						}
//						Debug.Log ("myCrdLevel = "+myCrdLevel);
//						Debug.Log ("reqd sp = "+globalActualCard[i]);
						if(expToSave>=globalActualCard && myCrdLevel < CardsManager._instance.mycards[positionOfLockedCard].max_level)
						{
							myCrdLevel++;
							cardAttack+= Mathf.CeilToInt(0.01f* cardAttack);
							cardDefense+= Mathf.CeilToInt(0.01f* cardDefense);
							cardLeaderShip+= Mathf.CeilToInt(0.01f* cardLeaderShip);

						}
						else
						{
							break;
						}
					}
					form_time.AddField ("card_level",(myCrdLevel).ToString());
					form_time.AddField ("attack",(cardAttack).ToString());
					form_time.AddField ("defense",(cardDefense).ToString());
					form_time.AddField ("leadership",(cardLeaderShip).ToString());


				}
//				print("card_level"+myCrdLevel);
				WWW www = new WWW(URltime,form_time.data);
				StartCoroutine(userTIMEfetching2(www , isSuccess => {
					if(isSuccess)
					{
						CardsManager.CardParameters a = CardsManager._instance.mycards[positionOfLockedCard];
            if(empireSceneNew.instance.selectionSoldiers=="army")
            {
						      PlayerParameters._instance.myPlayerParameter.gold -= CardsManager._instance.mycards[clickforSkill].experience;
                	PlayerParameters._instance.myPlayerParameter.wheat -= CardsManager._instance.mycards[clickforSkill].experience;
            }
            else
            {
              PlayerParameters._instance.myPlayerParameter.gold -= CardsManager._instance.myCaptives[clickforSkill].experience;
              PlayerParameters._instance.myPlayerParameter.wheat -= CardsManager._instance.myCaptives[clickforSkill].experience;
            }
						EmpireManager._instance.trainingGround.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString();
						EmpireManager._instance.trainingGround.goldText2.text = PlayerParameters._instance.myPlayerParameter.gold.ToString();
						EmpireManager._instance.trainingGround.goldText3.text = PlayerParameters._instance.myPlayerParameter.gold.ToString();
						EmpireManager._instance.trainingGround.goldText4.text = PlayerParameters._instance.myPlayerParameter.gold.ToString();

						EmpireManager._instance.trainingGround.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString();
						EmpireManager._instance.trainingGround.foodText2.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString();
						EmpireManager._instance.trainingGround.foodText3.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString();
						EmpireManager._instance.trainingGround.foodText4.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString();
						empireSceneNew.instance.offStatTraining();
						a.experience = expToSave;
						Debug.Log("a.experience = "+a.experience);
						a.card_level=myCrdLevel;
						a.attack = cardAttack;
						a.defense = cardDefense;
						a.leadership = cardLeaderShip;
						CardsManager._instance.mycards[positionOfLockedCard] = a;
						string rarityXp = (CardsManager._instance.mycards[positionOfLockedCard].rarity.ToString());

						if (rarityXp == "Common")
						{
							expStatMain.text=CardsManager._instance.mycards[positionOfLockedCard].experience.ToString()+"/"+CardsManager._instance.starCard1Actual[CardsManager._instance.mycards[positionOfLockedCard].card_level-1].ToString();
						}
						else if (rarityXp == "Uncommon")
						{
							expStatMain.text=CardsManager._instance.mycards[positionOfLockedCard].experience.ToString()+"/"+CardsManager._instance.starCard2Actual[CardsManager._instance.mycards[positionOfLockedCard].card_level-1].ToString();
						}
						else if (rarityXp == "Super")
						{
							expStatMain.text=CardsManager._instance.mycards[positionOfLockedCard].experience.ToString()+"/"+CardsManager._instance.starCard3Actual[CardsManager._instance.mycards[positionOfLockedCard].card_level-1].ToString();
						}
						else if (rarityXp == "Mega")
						{
							expStatMain.text=CardsManager._instance.mycards[positionOfLockedCard].experience.ToString()+"/"+CardsManager._instance.starCard4Actual[CardsManager._instance.mycards[positionOfLockedCard].card_level-1].ToString();
						}
						else if (rarityXp == "Legendary")
						{
							expStatMain.text=CardsManager._instance.mycards[positionOfLockedCard].experience.ToString()+"/"+CardsManager._instance.starCard5Actual[CardsManager._instance.mycards[positionOfLockedCard].card_level-1].ToString();
						}


						callback(true);
					}
					else
						callback(false);
				}));
			}
			else
			{
				popupFromServer.ShowPopup ("Network Error!");
			}

		});
	}


	//----------  SECONDARY LOCKDOWN STORAGE ----------
	public void lockDownStorage()
	{
    confirmButton.interactable=false;
		if (PlayerParameters._instance.myPlayerParameter.wheat >= EmpireManager._instance.storage.foodRequiredForSecondary [EmpireManager._instance.storage.currentLevel])
		{
			buildingUpgradeLayout.SetActive (true);
			wentFromSecondary=true;
			ShowAllCards ();
		} else {
			insufficientGems.SetActive(true);
			insufficientGems.transform.GetChild(4).GetComponent<Text>().text="Insufficient Resources !";
		}
	}
	public void fortifyClick()
	{
		confirmButton.interactable=false;
		if (PlayerParameters._instance.myPlayerParameter.wheat >= EmpireManager._instance.gate.foodRequiredForSecondary [EmpireManager._instance.gate.currentLevel])
		{
			buildingUpgradeLayout.SetActive (true);
			wentFromSecondary=true;
			ShowAllCards ();
		} else {
			insufficientGems.SetActive(true);
			insufficientGems.transform.GetChild(4).GetComponent<Text>().text="Insufficient Resources !";
		}
	}


	//----------  SECONDARY LOCKDOWN STORAGE ----------
	public void soldierRecruitClick()
	{
    confirmButton.interactable=false;
		if (PlayerParameters._instance.myPlayerParameter.wheat >= EmpireManager._instance.barracks.foodRequiredForSecondary [EmpireManager._instance.barracks.currentLevel])
		{
			wentFromSecondary = true;
			buildingUpgradeLayout.SetActive (true);
			ShowAllCards ();
		} else {
			insufficientGems.SetActive(true);
			insufficientGems.transform.GetChild(4).GetComponent<Text>().text="Insufficient Resources !";
		}
	}


	public void harvestBarn()
	{
    confirmButton.interactable=false;

		if (PlayerParameters._instance.myPlayerParameter.wheat >= EmpireManager._instance.barn.foodRequiredForSecondary [EmpireManager._instance.barn.currentLevel])
		{
			wentFromSecondary = true;
			buildingUpgradeLayout.SetActive (true);
			ShowAllCards ();
		}
		else
		{
			insufficientGems.SetActive(true);
			insufficientGems.transform.GetChild(4).GetComponent<Text>().text="Insufficient Resources !";
		}
	}
	public void goldMineClick()
	{
    confirmButton.interactable=false;

		if (PlayerParameters._instance.myPlayerParameter.wheat >= EmpireManager._instance.goldMine.foodRequiredForSecondary [EmpireManager._instance.goldMine.currentLevel])
		{
			wentFromSecondary = true;
			buildingUpgradeLayout.SetActive (true);
			ShowAllCards ();
		}
		else
		{
			insufficientGems.SetActive(true);
			insufficientGems.transform.GetChild(4).GetComponent<Text>().text="Insufficient Resources !";
		}
	}

	public void backbuildingUpgrade()
	{
//		for(int i=0;i<clickedBuutons.Length;i++)
//		{
//			clickedBuutons[i].interactable=false;
//		}
		buildingUpgradeLayout.SetActive(false);
		mainScroll.SetActive(true);

	}


	public void prisionIntergogate()
	{
		prisonInterogate.SetActive(true);
		Prison.SetActive(false);
		mainScroll.SetActive(false);
		prisonOngoing.SetActive(true);
		prisonAvailable.SetActive(false);
		Ongoing.GetComponent<Image>().color=new Color32(110,110,110,255);
		Available.GetComponent<Image>().color=new Color32(255,255,255,255);
	}
	public void BackToprisionIntergogate()
	{
		prisonInterogate.SetActive(false);
    buildingUpgradeLayout.SetActive(false);
		Prison.SetActive(true);
		mainScroll.SetActive(true);

	}
	public void prisonOngoingClick()
	{

		Ongoing.GetComponent<Image>().color=new Color32(110,110,110,255);
		Available.GetComponent<Image>().color=new Color32(255,255,255,255);
		prisonOngoing.SetActive(true);
		prisonAvailable.SetActive(false);
	}
	public void prisonAvailableClick()
	{
		Ongoing.GetComponent<Image>().color=new Color32(255,255,255,255);
		Available.GetComponent<Image>().color=new Color32(110,110,110,255);
		prisonOngoing.SetActive(false);
		prisonAvailable.SetActive(true);
	}
	public void onClickInterogationButton()
	{
		prisonOngoing.SetActive(false);
		prisonAvailable.SetActive(false);
	}
	public void backFromInterogationClick()
	{
		prisonOngoing.SetActive(true);
		prisonAvailable.SetActive(false);
	}



	public void LabOngoingClick()
	{
		GameObject.Find("Available").GetComponent<Button>().interactable=false;
		GameObject.Find("Ongoing").GetComponent<Button>().interactable=true;

		labGoing.SetActive(true);
		labAvailable.SetActive(false);
	}

	public void LabAvailableClick()
	{
		labGoing.SetActive(false);
		labAvailable.SetActive(true);
		GameObject.Find("Available").GetComponent<Button>().interactable=true;
		GameObject.Find("Ongoing").GetComponent<Button>().interactable=false;


	}

	public void onClickLABResearchButton()
	{
		researchLaboraotry.SetActive(true);
		labGoing.SetActive(true);
		labAvailable.SetActive(false);
		GameObject.Find("Available").GetComponent<Button>().interactable=false;
		GameObject.Find("Ongoing").GetComponent<Button>().interactable=true;
	}

	public void backFromLabResearchClick()
	{
		researchLaboraotry.SetActive(false);
		labGoing.SetActive(true);
		labAvailable.SetActive(false);
	}

}
