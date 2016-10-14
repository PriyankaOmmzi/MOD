using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
public class empireSceneNew : MonoBehaviour
{
	public static empireSceneNew instance;
	public Text buildingUpdateFoodInCardSelectionLimit;
	public Text buildingUpdateGoldInCardSelectionLimit;
	public Text expTimeInCardSelectionLimit;
	public Text expStatMainLimit;
	public Button confirmButton,confirmSkill;
	public GameObject sacrificeDialogLimit;
	public GameObject warninngLimit;
	public GameObject skillDialog;
	public GameObject skillWarningDialog;
	public Text startMaxLimit, endMaxLimit, startMaxLevel, endMaxLevel, startMaxLvlSkill, endMaxLvlSkill;
	public Text buildingUpdateFoodInCardSelectionSkill;
	public Text buildingUpdateGoldInCardSelectionSkill;
	public Text expTimeInCardSelectionSkill;
	public Text expStatMainSkill;
	public Sprite saveSpriteLocked;
	public GameObject containerOFongoing;
	public string traningButtonClick;

	public GameObject cardLevelWarning;
	public battleFormation myQuestingFormation;

	public Button[] armyButton;
	public  Button[] captiveButton;

	public GameObject[] armyContent;
	public GameObject[] captiveContent;
	public GameObject[] containerOfCatives;
	public List<CaptivesList> captivesPerConatiner;

	public GameObject[] containerOfOngoing;
	public List<CaptivesList> captivesPerOngoing;


[System.Serializable]
public struct CaptivesList
{
	public List<GameObject> captivesList;
}
	public string skillSelection;
	public string selectionSoldiers;
	// Use this for initialization
	public void Awake()
	{
		instance = this;
	}
	public string buttonNameSecond;
	public void onClickPrisonInterogate()
	{
		// if(prisonObj.instance.selectedCardPrisonSecondary==1)
		// {
		// 	prisonObj.instance.lockButtonNamePrisonSecondary.GetComponentInChildren<Text>().text="";
		// 	empireScene.instance.selectedCards.Remove (prisonObj.instance.lockButtonNamePrisonSecondary);
		// 	prisonObj.instance.selectedCardPrisonSecondary=0;
		// }
		buttonNameSecond="prisonSecond";

		empireScene.instance.prisionIntergogate();
		// if (PlayerParameters._instance.myPlayerParameter.wheat >= EmpireManager._instance.prison.foodRequiredForSecondary [EmpireManager._instance.prison.currentLevel])
		// {
		// 	empireScene.instance.wentFromSecondary = true;
		// 	empireScene.instance.buildingUpgradeLayout.SetActive (true);
		// 	empireScene.instance. ShowAllCards ();
		// 	empireScene.instance.confirmButton.interactable=false;
		//
		//
		// }
		// else {
		//  empireScene.instance.insufficientGems.SetActive(true);
		// 	 empireScene.instance.insufficientGems.transform.GetChild(4).GetComponent<Text>().text="Insufficient Resources !";
		// }



	}
	void Start () {

		traningButtonClick="0";
		skillSelection="skill1";
		selectionSoldiers="army";

		for(int i=0;i<armyButton.Length;i++)
		{
			armyButton[i].interactable=false;

		}
		for(int j=0;j<armyButton.Length;j++)
		{
			captiveButton[j].interactable=true;
			armyContent[j].SetActive(true);
			captiveContent[j].SetActive(false);

		}
		LockCards();


	}

	public void OnEnable()
	{
		prisonObj.instance.enableCheck();
		for (int j=0; j<armyButton.Length; j++)
		{
//			GameObject []children =	containerOfCatives[j].GetComponentsInChildren<Image>();
			for(int i = 0 ; i < containerOfCatives[j].transform.childCount ; i++)
			{
				Destroy (containerOfCatives[j].transform.GetChild (i).gameObject);
			}
		}

		if (CardsManager._instance != null)
		{
			if(CardsManager._instance.myCaptives.Count > 0)
			{
				for(int i = 0; i < CardsManager._instance.myCaptives.Count ; i++)
				{
					for(int j=0;j<containerOfCatives.Length;j++)
					{
						GameObject newCard = (GameObject) Instantiate ( Resources.Load ("cardItem"));
						newCard.transform.SetParent (containerOfCatives[j].transform);
						newCard.transform.localScale = Vector3.one;
						newCard.GetComponent<Image>().sprite = CardsManager._instance.myCaptives[i].cardSpriteFromResources;
						newCard.name = CardsManager._instance.myCaptives[i].card_id_in_playerList.ToString ();
						newCard.GetComponent<Button>().onClick.AddListener (() => {updateBuildingButton(newCard.GetComponent<Button>());});
						captivesPerConatiner[j].captivesList.Add(newCard);
					}


					for(int k=0;k<containerOfOngoing.Length;k++)
					{
						GameObject newCard = (GameObject) Instantiate ( Resources.Load ("cardItem"));
						newCard.transform.SetParent (containerOfOngoing[k].transform);
						newCard.transform.localScale = Vector3.one;
						newCard.GetComponent<Image>().sprite = CardsManager._instance.myCaptives[i].cardSpriteFromResources;
						newCard.name = CardsManager._instance.myCaptives[i].card_id_in_playerList.ToString ();
						newCard.GetComponent<Button>().onClick.AddListener (() => {updateBuildingButtonOngoingClick(newCard.GetComponent<Button>());});
						captivesPerOngoing[k].captivesList.Add(newCard);

					}
				}
			}
		}

	}



	void updateBuildingButton(Button objClick)
	{
		empireScene.instance.updateBuildingButtonCaptive (objClick);
	}

	void updateBuildingButtonOngoingClick(Button objClick)
	{
		prisonObj.instance.updateBuildingButtonOngoing (objClick);
	}


	// Update is called once per frame
	void Update ()
	{

	}

	public void captiveClick()
	{

//			for(int i=0;i<armyButton.Length;i++)
//			{
//				armyButton[i].interactable=false;
//
//			}
//			for(int j=0;j<armyButton.Length;j++)
//			{
//				captiveButton[j].interactable=true;
//				captiveContent[j].SetActive(false);
//				armyContent[j].SetActive(true);
//
//			}


	}

	public void clickArmyCaptive (Button selectType)
	{
				int positionOfLockedCard = CardsManager._instance.PositionOfCardInList (empireScene.instance.sacrficeLockedCard);
				if (empireScene.instance. selectedCardGroundSecondary == 1)
				{
				empireScene.instance.SetSelectedCardDataInCardSelection ();
				empireScene.instance.lockButtonNameGroundSecondary.GetComponentInChildren<Text> ().text = "";
				empireScene.instance.selectedCards.Remove (empireScene.instance.lockButtonNameGroundSecondary);
				empireScene.instance.selectedCardGroundSecondary = 0;
				offStatTraining();
				confirmButton.interactable = false;
				confirmSkill.interactable=false;

				}
		if (selectType.name == "army")
		{

			for(int i=0;i<armyButton.Length;i++)
			{
				armyButton[i].interactable=false;

			}
			for(int j=0;j<armyButton.Length;j++)
			{
				captiveButton[j].interactable=true;
				armyContent[j].SetActive(true);
				captiveContent[j].SetActive(false);

			}
			// if(CardsManager._instance.mycards[positionOfLockedCard].card_level==100)
			// {
			//
			// }
			// else
			// {
			// 	GameObject[] allCardButtons = GameObject.FindGameObjectsWithTag ("updateCards");
			//
			// 	for (int j = 0; j < allCardButtons.Length; j ++)
			// 	{
			// 		allCardButtons[j].GetComponent<Button>().interactable=false;
			//
			// 	}
			// }

			selectionSoldiers="army";

		}
		else if(selectType.name=="captive")
		{

			if (empireScene.instance. selectedCardGroundSecondary == 1) {

				empireScene.instance.SetSelectedCardDataInCardSelection ();
				empireScene.instance.lockButtonNameGroundSecondary.GetComponentInChildren<Text> ().text = "";
				empireScene.instance.selectedCards.Remove (empireScene.instance.lockButtonNameGroundSecondary);
				empireScene.instance.selectedCardGroundSecondary = 0;
				offStatTraining();
					confirmButton.interactable = false;
					confirmSkill.interactable=false;


			}
//			empireScene.instance.popupFromServer.ShowPopup ("In Progress");
			for(int i=0;i<armyButton.Length;i++)
			{
				armyButton[i].interactable=true;

			}
			for(int j=0;j<armyButton.Length;j++)
			{
				captiveButton[j].interactable=false;
				armyContent[j].SetActive(false);
				captiveContent[j].SetActive(true);
			}
			selectionSoldiers="captive";
			// if(CardsManager._instance.mycards[positionOfLockedCard].card_level==100)
			// {
			//
			// }
			// else
			// {
			// 	GameObject[] allCardButtons = GameObject.FindGameObjectsWithTag ("updateCards");
			//
			// 	for (int j = 0; j < allCardButtons.Length; j ++)
			// 	{
			// 		allCardButtons[j].GetComponent<Button>().interactable=false;
			//
			// 	}
			// }

		}


		LockCards();




	}

	public void onClickLimit()
	{

		traningButtonClick="limit";
		offStatTraining ();
		if (empireScene.instance. selectedCardGroundSecondary == 1) {

			empireScene.instance.SetSelectedCardDataInCardSelection ();
			empireScene.instance.selectedCards.Remove (empireScene.instance.lockButtonNameGroundSecondary);
			empireScene.instance.selectedCardGroundSecondary = 0;
			offStatTraining();

		}
		//Debug.Log ("sacrficeClickCardPos  = "+empireScene.instance.sacrficeLockedCard);
		int positionOfLockedCard = CardsManager._instance.PositionOfCardInList (empireScene.instance.sacrficeLockedCard);
		//int expToSave = (CardsManager._instance.mycards[positionOfLockedCard].experience);
		string rarityXp = (CardsManager._instance.mycards[positionOfLockedCard].rarity.ToString());
		//string startMax =CardsManager._instance.mycards[positionOfLockedCard].max_level.ToString();
		startMaxLimit.text="Max Lv. "+CardsManager._instance.mycards[positionOfLockedCard].max_level.ToString();
		endMaxLimit.text = "Max Lv. " + CardsManager._instance.GetNextMaxLevel (positionOfLockedCard).ToString ();



		if (rarityXp == "Common")
		{
			print("Common");
			expStatMainLimit.text=CardsManager._instance.mycards[positionOfLockedCard].experience.ToString()+"/"+CardsManager._instance.starCard1Actual[CardsManager._instance.mycards[positionOfLockedCard].card_level-1].ToString();


		}
		else if (rarityXp == "Uncommon")
		{
			print("Uncommon");
			expStatMainLimit.text=CardsManager._instance.mycards[positionOfLockedCard].experience.ToString()+"/"+CardsManager._instance.starCard2Actual[CardsManager._instance.mycards[positionOfLockedCard].card_level-1].ToString();


		}
		else if (rarityXp == "Super")
		{
			print("Super");
			expStatMainLimit.text=CardsManager._instance.mycards[positionOfLockedCard].experience.ToString()+"/"+CardsManager._instance.starCard3Actual[CardsManager._instance.mycards[positionOfLockedCard].card_level-1].ToString();

		}
		else if (rarityXp == "Mega")
		{
			print("Mega");
			expStatMainLimit.text=CardsManager._instance.mycards[positionOfLockedCard].experience.ToString()+"/"+CardsManager._instance.starCard4Actual[CardsManager._instance.mycards[positionOfLockedCard].card_level-1].ToString();



		}
		else if (rarityXp == "Legendary")
		{
			print("Legendary");
			expStatMainLimit.text=CardsManager._instance.mycards[positionOfLockedCard].experience.ToString()+"/"+CardsManager._instance.starCard5Actual[CardsManager._instance.mycards[positionOfLockedCard].card_level-1].ToString();


		}


		empireScene.instance.limitBreak.SetActive(true);
		empireScene.instance.levelUp.SetActive(false);
		empireScene.instance.skill1Obj.SetActive(false);
		empireScene.instance.wentFromSecondary = true;
		confirmButton.interactable = false;
		GameObject.Find ("levelProfileLimit").GetComponent<Image> ().sprite = saveSpriteLocked;
		LockCards();

	}


	public void showStatTraining()
	{

			if (selectionSoldiers == "army") {
			expTimeInCardSelectionLimit.text = (CardsManager._instance.mycards [empireScene.instance.clickforSkill].experience).ToString ();
			buildingUpdateFoodInCardSelectionLimit.text = expTimeInCardSelectionLimit.text;
			buildingUpdateGoldInCardSelectionLimit.text = expTimeInCardSelectionLimit.text;
			expTimeInCardSelectionSkill.text = (CardsManager._instance.mycards [empireScene.instance.clickforSkill].skill1_exp).ToString ();
			buildingUpdateFoodInCardSelectionSkill.text = expTimeInCardSelectionSkill.text;
			buildingUpdateGoldInCardSelectionSkill.text = expTimeInCardSelectionSkill.text;
			empireScene.instance.expTimeInCardSelectionLayoutLvl.text = (CardsManager._instance.mycards [empireScene.instance.clickforSkill].experience).ToString ();
			empireScene.instance.buildingUpdateFoodInCardSelectionLayoutLvl.text = empireScene.instance.expTimeInCardSelectionLayoutLvl.text;
			empireScene.instance.buildingUpdateGoldInCardSelectionLayoutLvl.text = empireScene.instance.expTimeInCardSelectionLayoutLvl.text;
		}
		else if (selectionSoldiers == "captive")
		{
			expTimeInCardSelectionLimit.text = (CardsManager._instance.myCaptives [empireScene.instance.clickforSkill].experience).ToString ();
			buildingUpdateFoodInCardSelectionLimit.text = expTimeInCardSelectionLimit.text;
			buildingUpdateGoldInCardSelectionLimit.text = expTimeInCardSelectionLimit.text;
			expTimeInCardSelectionSkill.text = (CardsManager._instance.myCaptives [empireScene.instance.clickforSkill].skill1_exp).ToString ();
			buildingUpdateFoodInCardSelectionSkill.text = expTimeInCardSelectionSkill.text;
			buildingUpdateGoldInCardSelectionSkill.text = expTimeInCardSelectionSkill.text;
			empireScene.instance.expTimeInCardSelectionLayoutLvl.text = (CardsManager._instance.myCaptives [empireScene.instance.clickforSkill].experience).ToString ();
			empireScene.instance.buildingUpdateFoodInCardSelectionLayoutLvl.text = empireScene.instance.expTimeInCardSelectionLayoutLvl.text;
			empireScene.instance.buildingUpdateGoldInCardSelectionLayoutLvl.text = empireScene.instance.expTimeInCardSelectionLayoutLvl.text;

		}

	}
	public void offStatTraining()
	{
		expTimeInCardSelectionLimit.text = "";
		buildingUpdateFoodInCardSelectionLimit.text = "";
		buildingUpdateGoldInCardSelectionLimit.text = "";
		empireScene.instance.expTimeInCardSelectionLayoutLvl.text = "";
		empireScene.instance.buildingUpdateFoodInCardSelectionLayoutLvl.text = "";
		empireScene.instance.buildingUpdateGoldInCardSelectionLayoutLvl.text = "";
		expTimeInCardSelectionSkill.text = "";
		buildingUpdateFoodInCardSelectionSkill.text = "";
		buildingUpdateGoldInCardSelectionSkill.text = "";
	}

	public void confirmSacrificeYesLimit()
	{
		if (selectionSoldiers == "army") {
			int cardIdLocked = CardsManager._instance.PositionOfCardInList (int.Parse (empireScene.instance.lockButtonNameGroundSecondary.name));
			empireScene.instance.SetSelectedCardDataInCardSelectionAgain (CardsManager._instance.mycards [cardIdLocked]);
			sacrificeCard (CardsManager._instance.mycards [empireScene.instance.sacrficeClickCardPos].card_level, empireScene.instance.sacrficeLockedCard, empireScene.instance.sacrficeClickCard, isSuccess => {

				if (isSuccess) {
					loadingScene.Instance.randomCards.Remove (empireScene.instance.sacrficeClickCard);
					CardsManager._instance.mycards.RemoveAt (empireScene.instance.sacrficeClickCardPos);
					Destroy (CardsManager._instance.cardButtonOfEmpire [empireScene.instance.sacrficeClickCardPos].gameObject);
					Destroy (CardsManager._instance.cardButtonOfEmpire1 [empireScene.instance.sacrficeClickCardPos].gameObject);
					Destroy (CardsManager._instance.cardButtonOfEmpire2 [empireScene.instance.sacrficeClickCardPos].gameObject);
					Destroy (CardsManager._instance.cardButtonOfEmpire3 [empireScene.instance.sacrficeClickCardPos].gameObject);
					CardsManager._instance.cardButtonOfEmpire.RemoveAt (empireScene.instance.sacrficeClickCardPos);
					CardsManager._instance.cardButtonOfEmpire1.RemoveAt (empireScene.instance.sacrficeClickCardPos);
					CardsManager._instance.cardButtonOfEmpire2.RemoveAt (empireScene.instance.sacrficeClickCardPos);
					CardsManager._instance.cardButtonOfEmpire3.RemoveAt (empireScene.instance.sacrficeClickCardPos);
					empireScene.instance.selectedCards.Remove (empireScene.instance.lockButtonNameGroundSecondary);
					empireScene.instance.selectedCardGroundSecondary = 0;
					sacrificeDialogLimit.SetActive (false);
					confirmButton.interactable = false;
					int positionOfLockedCard = CardsManager._instance.PositionOfCardInList (empireScene.instance.sacrficeLockedCard);
					int expToSave = (CardsManager._instance.mycards [positionOfLockedCard].experience);
					string rarityXp = (CardsManager._instance.mycards [positionOfLockedCard].rarity.ToString ());
					string startMax = CardsManager._instance.mycards [positionOfLockedCard].max_level.ToString ();
					startMaxLimit.text = "Max Lv. " + CardsManager._instance.mycards [positionOfLockedCard].max_level.ToString ();
					endMaxLimit.text = "Max Lv. " + CardsManager._instance.GetNextMaxLevel (positionOfLockedCard).ToString ();

				} else {

					empireScene.instance.popupFromServer.ShowPopup ("Cannot Update at this time!");
				}
			});
		}
		else if (selectionSoldiers == "captive")
		{
			int cardIdLocked = CardsManager._instance.PositionOfCaptiveInList (int.Parse (empireScene.instance.lockButtonNameGroundSecondary.name));
			empireScene.instance.SetSelectedCardDataInCardSelectionAgain (CardsManager._instance.myCaptives [cardIdLocked]);
			sacrificeCard (CardsManager._instance.myCaptives [empireScene.instance.sacrficeClickCardPos].card_level, empireScene.instance.sacrficeLockedCard, empireScene.instance.sacrficeClickCard, isSuccess => {

				if (isSuccess) {
					 CardsManager._instance.myCaptives.RemoveAt (empireScene.instance.sacrficeClickCardPos);
					//  Destroy(empireScene.instance.lockButtonNameGroundSecondary);


				for(int j=0;j<armyButton.Length;j++)
				{
					Destroy(captivesPerConatiner[j].captivesList[empireScene.instance.clickforSkill]);
				}

					empireScene.instance.selectedCards.Remove (empireScene.instance.lockButtonNameGroundSecondary);
					empireScene.instance.selectedCardGroundSecondary = 0;
					sacrificeDialogLimit.SetActive (false);
					confirmButton.interactable = false;

					int positionOfLockedCard = CardsManager._instance.PositionOfCaptiveInList (empireScene.instance.sacrficeLockedCard);
					int expToSave = (CardsManager._instance.mycards [positionOfLockedCard].experience);
					string rarityXp = (CardsManager._instance.mycards [positionOfLockedCard].rarity.ToString ());
					string startMax = CardsManager._instance.mycards [positionOfLockedCard].max_level.ToString ();
					startMaxLimit.text = "Max Lv. " + CardsManager._instance.myCaptives [positionOfLockedCard].max_level.ToString ();
					endMaxLimit.text = "Max Lv. " + CardsManager._instance.GetNextMaxLevel (positionOfLockedCard).ToString ();

				} else {

					empireScene.instance.popupFromServer.ShowPopup ("Cannot Update at this time!");
				}
			});

		}

	}


	public void exitSacrificeDialogLimit()
	{
		sacrificeDialogLimit.SetActive (false);
		confirmButton.interactable = false;
		//Debug.Log ("lockButtonNameGroundSecondary" + empireScene.instance.lockButtonNameGroundSecondary.GetComponentInChildren<Text> ().text);
		empireScene.instance.lockButtonNameGroundSecondary.GetComponentInChildren<Text> ().text = "";
		//Debug.Log ("lockButtonNameGroundSecondary" + empireScene.instance.lockButtonNameGroundSecondary.GetComponentInChildren<Text> ().text);
		empireScene.instance.SetSelectedCardDataInCardSelection ();
		empireScene.instance.selectedCards.Remove (empireScene.instance.lockButtonNameGroundSecondary);
		empireScene.instance.selectedCardGroundSecondary = 0;
		offStatTraining();



	}

	public void onclickSacrifice()
	{

		if (empireScene.instance.cardIdLockedDefault == CardsManager._instance.mycards [empireScene.instance.clickforSkill].card_name)
		{

			sacrificeDialogLimit.SetActive (true);
		}
		else
		{
			warninngLimit.SetActive (true);


		}

	}
	public void exitWarning()
	{

		warninngLimit.SetActive (false);
		confirmButton.interactable = false;
		empireScene.instance.lockButtonNameGroundSecondary.GetComponentInChildren<Text> ().text = "";
		empireScene.instance.SetSelectedCardDataInCardSelection ();
		empireScene.instance.selectedCards.Remove (empireScene.instance.lockButtonNameGroundSecondary);
		empireScene.instance.selectedCardGroundSecondary = 0;
		offStatTraining();


	}
	public void exitLimitPanel()
	{
		empireScene.instance.ExitLevel();
		if (empireScene.instance.selectedCardGroundSecondary == 1) {
			empireScene.instance.lockButtonNameGroundSecondary.GetComponentInChildren<Text> ().text = "";
			empireScene.instance.SetSelectedCardDataInCardSelection ();
			empireScene.instance.selectedCards.Remove (empireScene.instance.lockButtonNameGroundSecondary);
			empireScene.instance.selectedCardGroundSecondary = 0;
			offStatTraining();

		}
		EmpireManager._instance.trainingGround.secondaryCardNo = 0;
		empireScene.instance.lockButtonNameGroundSecondary = null;
	}

	//------------------ SKILL --------------------
	public void skillUpClick()
	{
		traningButtonClick="skill";
		offStatTraining ();
		if (empireScene.instance. selectedCardGroundSecondary == 1)
		{

			empireScene.instance.SetSelectedCardDataInCardSelection ();
			empireScene.instance.selectedCards.Remove (empireScene.instance.lockButtonNameGroundSecondary);
			empireScene.instance.selectedCardGroundSecondary = 0;
			offStatTraining();

		}
		Debug.Log ("sacrficeClickCardPos  = "+empireScene.instance.sacrficeLockedCard);
		int positionOfLockedCard = CardsManager._instance.PositionOfCardInList (empireScene.instance.sacrficeLockedCard);
		int expToSave = (CardsManager._instance.mycards[positionOfLockedCard].experience);
		string rarityXp = (CardsManager._instance.mycards[positionOfLockedCard].rarity.ToString());
		string startMax =CardsManager._instance.mycards[positionOfLockedCard].max_level.ToString();
		startMaxLvlSkill.text="Skill Lv. "+CardsManager._instance.mycards[positionOfLockedCard].skill1_level.ToString();
		endMaxLvlSkill.text = "Skill Lv. " + (CardsManager._instance.mycards[positionOfLockedCard].skill1_level+1).ToString ();


		if (rarityXp == "Super")
		{
			print("Super");
			expStatMainSkill.text=CardsManager._instance.mycards[positionOfLockedCard].skill1_exp.ToString()+"/"+CardsManager._instance.cardRarityActual3[CardsManager._instance.mycards[positionOfLockedCard].skill1_level-1].ToString();
		}
		else if (rarityXp == "Mega")
		{
			print("Mega");
			expStatMainSkill.text=CardsManager._instance.mycards[positionOfLockedCard].skill1_exp.ToString()+"/"+CardsManager._instance.cardRarityActual4[CardsManager._instance.mycards[positionOfLockedCard].skill1_level-1].ToString();
		}
		else if (rarityXp == "Legendary")
		{
			print("Legendary");
			expStatMainSkill.text=CardsManager._instance.mycards[positionOfLockedCard].skill1_exp.ToString()+"/"+CardsManager._instance.cardRarityActual5[CardsManager._instance.mycards[positionOfLockedCard].skill1_level-1].ToString();
		}

		empireScene.instance.limitBreak.SetActive(false);
		empireScene.instance.levelUp.SetActive(false);
		empireScene.instance.skill1Obj.SetActive(true);
		skill1Btn.interactable=false;
		GameObject.Find("skillNameDes").GetComponent<Text>().text=CardsManager._instance.mycards[positionOfLockedCard].skill_1.ToString();
		GameObject.Find("skillDes").GetComponent<Text>().text=CardsManager._instance.mycards[positionOfLockedCard].skill_1_Strength.ToString();
		empireScene.instance.wentFromSecondary = true;
		confirmSkill.interactable = false;
		GameObject.Find ("levelProfileSkill").GetComponent<Image> ().sprite = saveSpriteLocked;

//		GameObject.Find("levelProfileLimit").GetComponent<Image> ().sprite=empireScene.instance.lockButtonNameGroundSecondary.GetComponent<Image>().sprite;
		// GameObject[] allCardButtons = GameObject.FindGameObjectsWithTag ("updateCards");

		LockCards();

	}
	public Button skill1Btn, skill2Btn;
	public void skillDescription(Button button)
	{
		int positionOfLockedCard = CardsManager._instance.PositionOfCardInList (empireScene.instance.sacrficeLockedCard);

		if (button.name == "Skill1Button")
		{
			skill1Btn.interactable=false;
			skill2Btn.interactable=true;

			GameObject.Find("skillNameDes").GetComponent<Text>().text=CardsManager._instance.mycards[positionOfLockedCard].skill_1.ToString();
			GameObject.Find("skillDes").GetComponent<Text>().text=CardsManager._instance.mycards[positionOfLockedCard].skill_1_Strength.ToString();
			skillSelection="skill1";
			LockCards();

		}
		else if (button.name == "Skill2Button")
		{
			skill2Btn.interactable=false;
			skill1Btn.interactable=true;
			if(CardsManager._instance.mycards[positionOfLockedCard].card_level==100)
			{
				GameObject.Find("skillNameDes").GetComponent<Text>().text=CardsManager._instance.mycards[positionOfLockedCard].skill_2.ToString();
				GameObject.Find("skillDes").GetComponent<Text>().text=CardsManager._instance.mycards[positionOfLockedCard].skill_2_Strength.ToString();
				LockCards();

			}

			else
			{
				print("captive=====");
				GameObject[] allCardButtons = GameObject.FindGameObjectsWithTag ("updateCards");

				for (int j = 0; j < allCardButtons.Length; j ++)
				{
					allCardButtons[j].GetComponent<Button>().interactable=false;
					print("captive=====");

				}
			GameObject.Find("skillNameDes").GetComponent<Text>().text="Warning !";
			GameObject.Find("skillDes").GetComponent<Text>().text="Please update card to max level to unlock skill 2 !";
			}
			skillSelection="skill2";

		}
	}

	void LockCards()
	{
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
//				 EmpireManager._instance.barn.primaryCardNo != cardNo && EmpireManager._instance.barn.secondaryCardNo != cardNo &&
//				 EmpireManager._instance.goldMine.primaryCardNo != cardNo && EmpireManager._instance.goldMine.secondaryCardNo != cardNo &&
//				 EmpireManager._instance.barracks.primaryCardNo != cardNo && EmpireManager._instance.barracks.secondaryCardNo != cardNo &&
//				 EmpireManager._instance.trainingGround.primaryCardNo != cardNo && EmpireManager._instance.trainingGround.secondaryCardNo != cardNo
//				&& EmpireManager._instance.prison.secondaryCardNo != cardNo && EmpireManager._instance.prison.primaryCardNo != cardNo
//				&& EmpireManager._instance.gate.secondaryCardNo != cardNo && EmpireManager._instance.gate.primaryCardNo != cardNo
//				&& CardsManager._instance.mycards[CardsManager._instance.PositionOfCardInList(cardNo)].isLocked == false)
//
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

	public void exitSkillPanel()
	{
		empireScene.instance.ExitLevel();
		if (empireScene.instance.selectedCardGroundSecondary == 1) {
			empireScene.instance.lockButtonNameGroundSecondary.GetComponentInChildren<Text> ().text = "";
			empireScene.instance.SetSelectedCardDataInCardSelection ();
			empireScene.instance.selectedCards.Remove (empireScene.instance.lockButtonNameGroundSecondary);
			empireScene.instance.selectedCardGroundSecondary = 0;
			offStatTraining();

		}
		EmpireManager._instance.trainingGround.secondaryCardNo = 0;
		empireScene.instance.lockButtonNameGroundSecondary = null;
	}

	public void onclickConfirmSkillUp()
	{

		if (CardsManager._instance.mycards [empireScene.instance.clickforSkill].skill1_level <= 10)
		{
			string rarityXp = "";
			if (selectionSoldiers == "army")
				rarityXp = (CardsManager._instance.mycards [empireScene.instance.clickforSkill].rarity.ToString ());
			else
				rarityXp = (CardsManager._instance.myCaptives [empireScene.instance.clickforSkill].rarity.ToString ());

			string rarityXp2 = (CardsManager._instance.mycards [empireScene.instance.cardIdLocked].rarity.ToString ());
			if (((rarityXp == "Super") || (rarityXp == "Mega") || (rarityXp == "Legendary")) && ((rarityXp2 == "Super") || (rarityXp2 == "Mega") || (rarityXp2 == "Legendary")) )
			{
				skillDialog.SetActive (true);
			}
			else
			{
				skillWarningDialog.SetActive (true);
			}
		}
		else
		{
			cardLevelWarning.SetActive(true);

		}


	}
	public void exitCardLevelWarning()
	{
		cardLevelWarning.SetActive(false);

	}



	public void ExitConfirmSkillUp()
	{
		skillDialog.SetActive (false);
		confirmSkill.interactable = false;
		Debug.Log ("lockButtonNameGroundSecondary" + empireScene.instance.lockButtonNameGroundSecondary.GetComponentInChildren<Text> ().text);
		empireScene.instance.lockButtonNameGroundSecondary.GetComponentInChildren<Text> ().text = "";
		Debug.Log ("lockButtonNameGroundSecondary" + empireScene.instance.lockButtonNameGroundSecondary.GetComponentInChildren<Text> ().text);
		empireScene.instance.SetSelectedCardDataInCardSelection ();
		empireScene.instance.selectedCards.Remove (empireScene.instance.lockButtonNameGroundSecondary);
		empireScene.instance.selectedCardGroundSecondary = 0;
		offStatTraining();




	}

	public void confrimSkillButtonYes()
	{
		if (selectionSoldiers == "army") {
			int cardIdLocked = CardsManager._instance.PositionOfCardInList (int.Parse (empireScene.instance.lockButtonNameGroundSecondary.name));
			empireScene.instance.SetSelectedCardDataInCardSelectionAgain (CardsManager._instance.mycards [cardIdLocked]);
			sacrificeCardSkill (CardsManager._instance.mycards [empireScene.instance.sacrficeClickCardPos].card_level, empireScene.instance.sacrficeLockedCard, empireScene.instance.sacrficeClickCard, isSuccess => {

				if (isSuccess) {
					loadingScene.Instance.randomCards.Remove (empireScene.instance.sacrficeClickCard);
					CardsManager._instance.mycards.RemoveAt (empireScene.instance.sacrficeClickCardPos);
					Destroy (CardsManager._instance.cardButtonOfEmpire [empireScene.instance.sacrficeClickCardPos].gameObject);
					Destroy (CardsManager._instance.cardButtonOfEmpire1 [empireScene.instance.sacrficeClickCardPos].gameObject);
					Destroy (CardsManager._instance.cardButtonOfEmpire2 [empireScene.instance.sacrficeClickCardPos].gameObject);
					Destroy (CardsManager._instance.cardButtonOfEmpire3 [empireScene.instance.sacrficeClickCardPos].gameObject);
					CardsManager._instance.cardButtonOfEmpire.RemoveAt (empireScene.instance.sacrficeClickCardPos);
					CardsManager._instance.cardButtonOfEmpire1.RemoveAt (empireScene.instance.sacrficeClickCardPos);
					CardsManager._instance.cardButtonOfEmpire2.RemoveAt (empireScene.instance.sacrficeClickCardPos);
					CardsManager._instance.cardButtonOfEmpire3.RemoveAt (empireScene.instance.sacrficeClickCardPos);
					empireScene.instance.selectedCards.Remove (empireScene.instance.lockButtonNameGroundSecondary);
					empireScene.instance.selectedCardGroundSecondary = 0;
					skillDialog.SetActive (false);
					confirmSkill.interactable = false;

					int positionOfLockedCard = CardsManager._instance.PositionOfCardInList (empireScene.instance.sacrficeLockedCard);

					if (skillSelection == "skill2") {
						startMaxLvlSkill.text = "Skill Lv. " + CardsManager._instance.mycards [positionOfLockedCard].skill2_level.ToString ();
						endMaxLvlSkill.text = "Skill Lv. " + (CardsManager._instance.mycards [positionOfLockedCard].skill2_level + 1).ToString ();
					} else {
						startMaxLvlSkill.text = "Skill Lv. " + CardsManager._instance.mycards [positionOfLockedCard].skill1_level.ToString ();
						endMaxLvlSkill.text = "Skill Lv. " + (CardsManager._instance.mycards [positionOfLockedCard].skill1_level + 1).ToString ();

					}

				} else {

					empireScene.instance.popupFromServer.ShowPopup ("Cannot Update at this time!");
				}
			});
		}

		else if (selectionSoldiers == "captive")
		{
			int cardIdLocked = CardsManager._instance.PositionOfCaptiveInList (int.Parse (empireScene.instance.lockButtonNameGroundSecondary.name));
			empireScene.instance.SetSelectedCardDataInCardSelectionAgain(CardsManager._instance.myCaptives[cardIdLocked]);
			sacrificeCardSkill (CardsManager._instance.myCaptives [empireScene.instance.sacrficeClickCardPos].card_level, empireScene.instance.sacrficeLockedCard, empireScene.instance.sacrficeClickCard, isSuccess => {

				if (isSuccess)
				{
				 CardsManager._instance.myCaptives.RemoveAt (empireScene.instance.sacrficeClickCardPos);
					for(int j=0;j<armyButton.Length;j++)
					{
						Destroy(captivesPerConatiner[j].captivesList[empireScene.instance.clickforSkill]);
					}

					empireScene.instance.selectedCards.Remove (empireScene.instance.lockButtonNameGroundSecondary);
					empireScene.instance.selectedCardGroundSecondary = 0;
					skillDialog.SetActive (false);
					confirmSkill.interactable=false;

					int positionOfLockedCard = CardsManager._instance.PositionOfCardInList (empireScene.instance.sacrficeLockedCard);
					if(skillSelection=="skill2")
					{
						startMaxLvlSkill.text="Skill Lv. "+CardsManager._instance.mycards[positionOfLockedCard].skill2_level.ToString();
						endMaxLvlSkill.text = "Skill Lv. " + (CardsManager._instance.mycards[positionOfLockedCard].skill2_level+1).ToString ();
					}
					else
					{
						startMaxLvlSkill.text="Skill Lv. "+CardsManager._instance.mycards[positionOfLockedCard].skill1_level.ToString();
						endMaxLvlSkill.text = "Skill Lv. " + (CardsManager._instance.mycards[positionOfLockedCard].skill1_level+1).ToString ();

					}

				}
				else
				{
					empireScene.instance.popupFromServer.ShowPopup ("Cannot Update at this time!");
				}
			});
		}
	}

	public void exitWarningSkill()
	{
		skillWarningDialog.SetActive (false);
		confirmSkill.interactable = false;
		empireScene.instance.lockButtonNameGroundSecondary.GetComponentInChildren<Text> ().text = "";
		empireScene.instance.SetSelectedCardDataInCardSelection ();
		empireScene.instance.selectedCards.Remove (empireScene.instance.lockButtonNameGroundSecondary);
		empireScene.instance.selectedCardGroundSecondary = 0;
		offStatTraining();


	}


	public void sacrificeCard( int cardLvl, int lockedCard ,int deleteCard, Action <bool> callback)
	{

			empireScene.instance.loader.SetActive (true);
			NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
				if (isConnected) {
					WWWForm form_time = new WWWForm ();
					string URltime = loadingScene.Instance.baseUrl;
					form_time.AddField ("tag", "doAddUpdatePlayerCards");
					form_time.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
					form_time.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
					int positionOfLockedCard = CardsManager._instance.PositionOfCardInList (lockedCard);
					// int positionOfSacrificedCard = 0;
					// if (selectionSoldiers == "army")
					// 	positionOfSacrificedCard = CardsManager._instance.PositionOfCardInList (empireScene.instance.sacrficeClickCard);
					// else
					// 	positionOfSacrificedCard = CardsManager._instance.PositionOfCaptiveInList (empireScene.instance.sacrficeClickCard);

					int maxLevelToSend = CardsManager._instance.GetNextMaxLevel (positionOfLockedCard);
					form_time.AddField ("card_no_in_players_list", lockedCard.ToString ());
					form_time.AddField ("max_level", (maxLevelToSend).ToString ());
					form_time.AddField ("delete_card_no_in_players_list", deleteCard.ToString ());
					WWW www = new WWW (URltime, form_time.data);
					StartCoroutine (empireScene.instance.userTIMEfetching2 (www, isSuccess => {
						if (isSuccess) {
							CardsManager.CardParameters a = CardsManager._instance.mycards [positionOfLockedCard];
							CardsManager._instance.mycards [positionOfLockedCard] = a;
							if (selectionSoldiers == "army")
								PlayerParameters._instance.myPlayerParameter.gold -= CardsManager._instance.mycards [empireScene.instance.clickforSkill].experience;
							else
								PlayerParameters._instance.myPlayerParameter.gold -= CardsManager._instance.myCaptives [empireScene.instance.clickforSkill].experience;
							EmpireManager._instance.trainingGround.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
							EmpireManager._instance.trainingGround.goldText2.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
							EmpireManager._instance.trainingGround.goldText3.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
							EmpireManager._instance.trainingGround.goldText4.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();

							if (selectionSoldiers == "army")
								PlayerParameters._instance.myPlayerParameter.wheat -= CardsManager._instance.mycards [empireScene.instance.clickforSkill].experience;
							else
								PlayerParameters._instance.myPlayerParameter.wheat -= CardsManager._instance.myCaptives [empireScene.instance.clickforSkill].experience;

							EmpireManager._instance.trainingGround.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
							EmpireManager._instance.trainingGround.foodText2.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
							EmpireManager._instance.trainingGround.foodText3.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
							EmpireManager._instance.trainingGround.foodText4.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();

							offStatTraining ();
							print ("=== it is working");
							CardsManager.CardParameters b = CardsManager._instance.mycards [positionOfLockedCard];
							b.max_level = maxLevelToSend;
							CardsManager._instance.mycards [positionOfLockedCard] = b;

							callback (true);
						} else
							callback (false);
					}));
				} else {
					empireScene.instance.popupFromServer.ShowPopup ("Network Error!");
				}

			});


	}



	//----------------------------------------  SKILL ADDED ---------------------------------

	public void sacrificeCardSkill(  int cardLvl, int lockedCard ,int deleteCard, Action <bool> callback)
	{
			empireScene.instance.loader.SetActive (true);
			NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
				if (isConnected) {
					WWWForm form_time = new WWWForm ();
					string URltime = loadingScene.Instance.baseUrl;
					form_time.AddField ("tag", "doAddUpdatePlayerCards");
					form_time.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
					form_time.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
					int positionOfLockedCard = CardsManager._instance.PositionOfCardInList (lockedCard);
					int positionOfSacrificedCard = empireScene.instance.clickforSkill;

						int skillToadd = 0;
					if (selectionSoldiers == "army")
						skillToadd = (int)(CardsManager._instance.mycards [positionOfSacrificedCard].skill1_exp * (100 + EmpireManager._instance.trainingGround.finalValue1 [EmpireManager._instance.trainingGround.currentLevel]) / 100);
					else
					 skillToadd = (int)(CardsManager._instance.myCaptives [positionOfSacrificedCard].skill1_exp * (100 + EmpireManager._instance.trainingGround.finalValue1 [EmpireManager._instance.trainingGround.currentLevel]) / 100);

					//	Debug.Log("skillToadd = "+skillToadd);
					int skillToSave = (CardsManager._instance.mycards [positionOfLockedCard].skill1_exp + skillToadd);
					//	Debug.Log("skillToSave = "+skillToSave);
					form_time.AddField ("card_no_in_players_list", lockedCard.ToString ());
					form_time.AddField ("skill1_exp", skillToSave.ToString ());
					form_time.AddField ("delete_card_no_in_players_list", deleteCard.ToString ());
					int myCrdLevel = CardsManager._instance.mycards [positionOfLockedCard].skill1_level;
					int abc = myCrdLevel;
					int cardLevelToChek = myCrdLevel - 1;
					int globalActualCard = 0;
					//Debug.Log("abc = "+abc);
					string rarityXp2 = CardsManager._instance.mycards [positionOfLockedCard].rarity.ToString ();

					if (rarityXp2 == "Super") {
						globalActualCard = CardsManager._instance.cardRarityActual3 [cardLevelToChek];

					} else if (rarityXp2 == "Mega") {
						globalActualCard = CardsManager._instance.cardRarityActual4 [cardLevelToChek];
					} else if (rarityXp2 == "Legendary") {
						globalActualCard = CardsManager._instance.cardRarityActual5 [cardLevelToChek];
					}

					if (skillToSave >= globalActualCard && CardsManager._instance.mycards [positionOfLockedCard].skill1_level < 10) {
						myCrdLevel++;
						for (int i= abc; i < CardsManager._instance.cardRarityActual3.Count; i++) {

							if (rarityXp2 == "Super") {
								globalActualCard = CardsManager._instance.cardRarityActual3 [i];

							} else if (rarityXp2 == "Mega") {
								globalActualCard = CardsManager._instance.cardRarityActual4 [i];
							} else if (rarityXp2 == "Legendary") {
								globalActualCard = CardsManager._instance.cardRarityActual5 [i];
							}
							if (skillToSave >= globalActualCard && myCrdLevel < 10) {
								myCrdLevel++;
							} else {
								break;
							}
						}
						form_time.AddField ("skill1_level", (myCrdLevel).ToString ());


					}
					print ("card_level" + myCrdLevel);

					WWW www = new WWW (URltime, form_time.data);
					StartCoroutine (empireScene.instance.userTIMEfetching2 (www, isSuccess => {
						if (isSuccess) {
							if (selectionSoldiers == "army")
								PlayerParameters._instance.myPlayerParameter.gold -= CardsManager._instance.mycards [empireScene.instance.clickforSkill].experience;
							else
								PlayerParameters._instance.myPlayerParameter.gold -= CardsManager._instance.myCaptives [empireScene.instance.clickforSkill].experience;

							EmpireManager._instance.trainingGround.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
							EmpireManager._instance.trainingGround.goldText2.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
							EmpireManager._instance.trainingGround.goldText3.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
							EmpireManager._instance.trainingGround.goldText4.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();

							if (selectionSoldiers == "army")
								PlayerParameters._instance.myPlayerParameter.wheat -= CardsManager._instance.mycards [empireScene.instance.clickforSkill].experience;
							else
								PlayerParameters._instance.myPlayerParameter.wheat -= CardsManager._instance.myCaptives [empireScene.instance.clickforSkill].experience;
							EmpireManager._instance.trainingGround.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
							EmpireManager._instance.trainingGround.foodText2.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
							EmpireManager._instance.trainingGround.foodText3.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
							EmpireManager._instance.trainingGround.foodText4.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
							offStatTraining ();

							CardsManager.CardParameters a = CardsManager._instance.mycards [positionOfLockedCard];
							a.skill1_exp = skillToSave;
							a.skill1_level = myCrdLevel;
							CardsManager._instance.mycards [positionOfLockedCard] = a;
							string rarityXp = (CardsManager._instance.mycards [positionOfLockedCard].rarity.ToString ());
							if (rarityXp == "Super") {
								expStatMainSkill.text = CardsManager._instance.mycards [positionOfLockedCard].skill1_exp.ToString () + "/" + CardsManager._instance.cardRarityActual3 [CardsManager._instance.mycards [positionOfLockedCard].skill1_level - 1].ToString ();

							} else if (rarityXp == "Mega") {
								expStatMainSkill.text = CardsManager._instance.mycards [positionOfLockedCard].skill1_exp.ToString () + "/" + CardsManager._instance.cardRarityActual4 [CardsManager._instance.mycards [positionOfLockedCard].skill1_level - 1].ToString ();

							} else if (rarityXp == "Legendary") {
								expStatMainSkill.text = CardsManager._instance.mycards [positionOfLockedCard].skill1_exp.ToString () + "/" + CardsManager._instance.cardRarityActual5 [CardsManager._instance.mycards [positionOfLockedCard].skill1_level - 1].ToString ();

							}
							callback (true);
						} else
							callback (false);
					}));
				} else {
					empireScene.instance.popupFromServer.ShowPopup ("Network Error!");
				}

			});
	}
}
