using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
public class prisonObj : MonoBehaviour
{
	public Sprite afterRest;
	public Text prisonerQueue;
	public GameObject interroDialog;
	public static prisonObj instance;
	public Image prisonMinePrimaryImage;
	public float prisonPrimaryClockText;
	public Text prisonPrimaryClockString;
	//	public Image updategoldMine;
	public Text prisonUpdateImageText;
	public Text prisonLevel;
	public Text prisonNow, prisonNext;
	public Button prisonUpgradeButton;
	public bool isprisonPrimary=false;
	public Sprite prisonloadImage;
	public Image prisonSecondaryImage;
	public float prisonSecondaryText;
	public Text prisonSecondaryString;
	public bool isprisonSecondary=false;
	public Sprite prisonloadImageSecondary;
	public GameObject prison;

	public int selectedCardPrison=0;
	public int selectedCardPrisonSecondary=0;

	public Button lockButtonNamePrison;
	public Button lockButtonNamePrisonSecondary;

	public  Text lockButtonPrison;
	public  Text lockButtonPrisonSecondary;

	string tempButtonPrisonSecondary;
	string tempButtonPrisonPrimary;
	public int randomLoadPrison;
	public Sprite saveSpriteLockedPrison;
	public Text gemsPrison, foodPrison, GodlPrison;
	public Text totalFood, totalGold, totalTime;
	public GameObject containerOfCardsList;
	public List<GameObject> ongoingList;
	public List<GameObject> ongoingContent;
	public List<Button> buttonAdded;
	public List<Text> textAdded;
	public List<int> cardCount;
	public int selectedFearCard=0;

	public Button selectInterrogateButtonClick;
	public bool isFilled;
	public float waitTime;
	public GameObject useGemsPanel;
	public GameObject cancelInterrogationPanel; 
	public Text defaultFearText;
	public void Awake()
	{
		instance = this;
	}




	public void openInterroDialog()
	{
		interroDialog.SetActive(true);
	}
	void Start ()
	{
		useGemsPanel.SetActive (false);
		cancelInterrogationPanel.SetActive (false);
		prisonPrimaryClockText=EmpireManager._instance.prison.timeRequiredPerLevel[EmpireManager._instance.prison.currentLevel]*3600f;
		prisonNow.text=EmpireManager._instance.prison.finalValue1[EmpireManager._instance.prison.currentLevel].ToString();
		prisonNext.text=EmpireManager._instance.prison.finalValue1[EmpireManager._instance.prison.currentLevel+1].ToString();
		EmpireManager._instance.prison.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString();
		EmpireManager._instance.prison.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();


	}

	public void cancelInterrogationClick()
	{
		cancelInterrogationPanel.SetActive (true);
	}
	public void useGemsClick()
	{
		useGemsPanel.SetActive (true);

	}

	public void yesNoInterrogation(Button type)
	{
		if (type.name == "yes") {
			
		for (int i = 0; i < ongoingList.Count; i++) {
				
				Destroy (ongoingList [selectedFearCard].gameObject);
				ongoingList.RemoveAt (selectedFearCard);
				buttonAdded[selectedFearCard].interactable=true;
				Destroy (ongoingContent [selectedFearCard].gameObject);
				ongoingContent.RemoveAt (selectedFearCard);
				cardCount.RemoveAt (selectedFearCard);
				buttonAdded.RemoveAt (selectedFearCard);
				textAdded[selectedFearCard].text="";
				textAdded.RemoveAt (selectedFearCard);
				EmpireManager._instance.prison.storgeLockDown.interactable = true;
				prisonSecondaryImage.sprite=prisonloadImageSecondary;
				prisonerQueue.text= "Prisoner queue: "+ongoingList.Count+" / 5";



			}
			prisonerQueue.text= "Prisoner queue: "+ongoingList.Count+" / 5";
			cancelInterrogationPanel.SetActive (false);

			
		} else {
			cancelInterrogationPanel.SetActive (false);

		}
	}
	public void yesNoUseGems(Button type)
	{
		if (type.name == "yes")
		{

			secondaryCardFearAdded( isSuccess =>
				{
					if(isSuccess)
					{


						for (int i = 0; i < ongoingList.Count; i++) {
							buttonAdded[selectedFearCard].interactable=true;
							Destroy (ongoingList [selectedFearCard].gameObject);
							ongoingList.RemoveAt (selectedFearCard);
							Destroy (ongoingContent [selectedFearCard].gameObject);
							ongoingContent.RemoveAt (selectedFearCard);
							//----
							int tempAdd = EmpireManager._instance.prison.fearIncrease[EmpireManager._instance.prison.currentLevel];
							int positionOfLockedCard = CardsManager._instance.PositionOfCardInList (selectedFearCard);
							int fearToSave = (CardsManager._instance.myCaptives[positionOfLockedCard].fear_factor)+tempAdd;
							CardsManager.CardParameters a = CardsManager._instance.myCaptives [positionOfLockedCard];
							a.fear_factor = fearToSave;
							CardsManager._instance.myCaptives [positionOfLockedCard] = a;
							//----
							cardCount.RemoveAt (selectedFearCard);
							buttonAdded.RemoveAt (selectedFearCard);
							textAdded[selectedFearCard].text="";
							textAdded.RemoveAt (selectedFearCard);
							EmpireManager._instance.prison.storgeLockDown.interactable = true;
							prisonSecondaryImage.sprite=prisonloadImageSecondary;
							prisonerQueue.text= "Prisoner queue: "+ongoingList.Count+" / 5";



						}
					}
					else
					{
						empireScene.instance.popupFromServer.ShowPopup ("Cannot Update at this time!");

					}
				});
			
		
			prisonerQueue.text= "Prisoner queue: "+ongoingList.Count+" / 5";
			useGemsPanel.SetActive (false);


		}
		else 
		{
			useGemsPanel.SetActive (false);

		}

	}

	public void secondaryCardFearAdded(Action<bool>callback)
	{
		empireScene.instance.loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected)
			{
				WWWForm form_time= new WWWForm ();
				EmpireManager._instance.prison.secondaryCardNo = int.Parse (lockButtonNamePrisonSecondary.name);

				string lockedCard= CardsManager._instance.mycards[selectedCardPrisonSecondary].card_name.ToString();

				//int cardIdLocked = CardsManager._instance.PositionOfCaptiveInList (int.Parse (clickforOngoing.name));

								int tempAdd = EmpireManager._instance.prison.fearIncrease[EmpireManager._instance.prison.currentLevel];
				int positionOfLockedCard = CardsManager._instance.PositionOfCardInList (selectedFearCard);
								int fearToSave = (CardsManager._instance.myCaptives[positionOfLockedCard].fear_factor)+tempAdd;
				CardsManager.CardParameters a = CardsManager._instance.myCaptives [positionOfLockedCard];
								a.fear_factor = fearToSave;
				CardsManager._instance.myCaptives [positionOfLockedCard] = a;

				//string cardNo CardsManager._instance.myCaptives[clickforOngoing].cardNo.ToString();
				form_time.AddField ("tag","getRequiredMultipleData");
				form_time.AddField ("user_id",PlayerDataParse._instance.playersParam.userId);
				form_time.AddField ("device_id",SystemInfo.deviceUniqueIdentifier);
				form_time.AddField ("secondary_card_no",EmpireManager._instance.prison.secondaryCardNo.ToString());
				form_time.AddField ("secondary_card_locked",lockedCard);
				form_time.AddField ("interrogation_list",ongoingList.Count.ToString());
				form_time.AddField ("time_of_secondary_upgrade",TimeManager._instance.GetCurrentServerTime ().ToString ());
				form_time.AddField ("card_no_in_player_list_of_captive_to_update",positionOfLockedCard.ToString());
				form_time.AddField ("fear_factor",fearToSave.ToString ());
				form_time.AddField ("array_players","players");


				print("positionOfLockedCard"+positionOfLockedCard);
				print("fearToSave"+fearToSave);
				print("secondary_card_no"+EmpireManager._instance.prison.secondaryCardNo.ToString());
				print("interrogation_list"+ongoingList.Count.ToString());

				WWW www = new WWW(loadingScene.Instance.baseUrl,form_time.data);
				StartCoroutine(empireScene.instance.userTIMEfetching2(www , callback));
			}
			else
			{
				empireScene.instance.loader.SetActive (false);
				callback(false);
			}
		});


	}





	public void secondaryCardFear(Action<bool>callback)
	{
		empireScene.instance.loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected)
			{
				WWWForm form_time= new WWWForm ();
				EmpireManager._instance.prison.secondaryCardNo = int.Parse (lockButtonNamePrisonSecondary.name);

				string lockedCard= CardsManager._instance.mycards[selectedCardPrisonSecondary].card_name.ToString();

				//int cardIdLocked = CardsManager._instance.PositionOfCaptiveInList (int.Parse (clickforOngoing.name));

//				int tempAdd = EmpireManager._instance.prison.fearIncrease[EmpireManager._instance.prison.currentLevel];
				int positionOfLockedCard = CardsManager._instance.PositionOfCardInList (selectedFearCard);
//				int fearToSave = (CardsManager._instance.myCaptives[positionOfLockedCard].fear_factor)+tempAdd;
				CardsManager.CardParameters a = CardsManager._instance.myCaptives [positionOfLockedCard];
//				a.fear_factor = fearToSave;
				CardsManager._instance.myCaptives [positionOfLockedCard] = a;

				//string cardNo CardsManager._instance.myCaptives[clickforOngoing].cardNo.ToString();
				int fear = CardsManager._instance.myCaptives[positionOfLockedCard].fear_factor;
				form_time.AddField ("tag","getRequiredMultipleData");
				form_time.AddField ("user_id",PlayerDataParse._instance.playersParam.userId);
				form_time.AddField ("device_id",SystemInfo.deviceUniqueIdentifier);
				form_time.AddField ("secondary_card_no",EmpireManager._instance.prison.secondaryCardNo.ToString());
				form_time.AddField ("secondary_card_locked",lockedCard);
				form_time.AddField ("interrogation_list",ongoingList.Count.ToString());
				form_time.AddField ("time_of_secondary_upgrade",TimeManager._instance.GetCurrentServerTime ().ToString ());

//				form_time.AddField ("fear_factor",fearToSave.ToString());
				form_time.AddField ("array_players","players");
				//form_time.AddField ("avatar_exp",CardsManager._instance.mycards[selectedCardPrisonSecondary].experience.ToString());
//				form_time.AddField ("wheat",wheatSave.ToString ());
//				form_time.AddField ("gold",goldSave.ToString ());
//				form_time.AddField ("active_time",active_time);

				WWW www = new WWW(loadingScene.Instance.baseUrl,form_time.data);
				StartCoroutine(empireScene.instance.userTIMEfetching2(www , callback));
			}
			else
			{
				empireScene.instance.loader.SetActive (false);
				callback(false);
			}
		});


}




	public void enableCheck()
	{
		if(ongoingList != null)
		{
			prisonerQueue.text= "Prisoner queue: 0 / 5";

		}
		else
		{
			for(int i=0;i<ongoingList.Count;i++)
			{
				print("ongoingList"+ongoingList.Count);
				prisonerQueue.text= "Prisoner queue: "+ongoingList.Count+" / 5";
			}
		}
	}
	public void updateBuildingButtonEmpire()
	{

		if(!empireScene.instance.wentFromSecondary)
		{
			if (selectedCardPrison == 0)
			{
				print ("lockedPrimary");

				lockButtonNamePrison = empireScene.instance.objbtn;
				empireScene.instance.confirmButton.interactable = true;
				lockButtonNamePrison.GetComponentInChildren<Text> ().text = "SELECTED";
				empireScene.instance.selectedCards.Add (lockButtonNamePrison);
				selectedCardPrison = 1;
				int cardIdLocked = CardsManager._instance.PositionOfCardInList (int.Parse (empireScene.instance.objbtn.name));
				empireScene.instance.SetSelectedCardDataInCardSelection(CardsManager._instance.mycards[cardIdLocked]);

			}
			else
			{
				print ("unlockedPrimary");

				empireScene.instance.confirmButton.interactable = false;
				lockButtonNamePrison.GetComponentInChildren<Text> ().text = "";
				empireScene.instance.selectedCards.Remove (lockButtonNamePrison);
				selectedCardPrison = 0;
				empireScene.instance.SetSelectedCardDataInCardSelection();

			}
		}
		else
		{
			if (selectedCardPrisonSecondary == 0)
			{
				print ("locked");

				lockButtonNamePrisonSecondary = empireScene.instance.objbtn;
				empireScene.instance.confirmButton.interactable = true;
				lockButtonNamePrisonSecondary.GetComponentInChildren<Text> ().text = "SELECTED";
				empireScene.instance.selectedCards.Add (lockButtonNamePrisonSecondary);
				selectedCardPrisonSecondary = 1;
				int cardIdLocked = CardsManager._instance.PositionOfCardInList (int.Parse (empireScene.instance.objbtn.name));
				empireScene.instance.SetSelectedCardDataInCardSelection(CardsManager._instance.mycards[cardIdLocked]);
			}
			else
			{
				print ("unlocked");

				empireScene.instance.confirmButton.interactable = false;
				lockButtonNamePrisonSecondary.GetComponentInChildren<Text> ().text = "";
				empireScene.instance.selectedCards.Remove (lockButtonNamePrisonSecondary);
				selectedCardPrisonSecondary = 0;
				empireScene.instance.SetSelectedCardDataInCardSelection();

			}
		}



	}
	public Button clickforOngoing;
	public void updateBuildingButtonOngoing(Button objClick)
	{
		// if (selectedCardPrisonSecondary == 0)
		// {

		lockButtonNamePrisonSecondary = objClick;
		clickforOngoing=objClick;
		lockButtonNamePrisonSecondary.GetComponentInChildren<Text> ().text = "SELECTED";
		lockButtonNamePrisonSecondary.interactable=false;
		interroDialog.SetActive(true);
	}

	public void cancelIntterogation()
	{
		lockButtonNamePrisonSecondary=clickforOngoing;
		lockButtonNamePrisonSecondary.GetComponentInChildren<Text> ().text = "";
		lockButtonNamePrisonSecondary.interactable=true;
		interroDialog.SetActive(false);
		clickforOngoing=null;
	}
	public void selectInterrogateButton()
	{
	if (PlayerParameters._instance.myPlayerParameter.wheat >= EmpireManager._instance.prison.foodRequiredForSecondary [EmpireManager._instance.prison.currentLevel])
	{
		empireScene.instance.wentFromSecondary = true;
		empireScene.instance.buildingUpgradeLayout.SetActive (true);
		empireScene.instance. ShowAllCards ();
		empireScene.instance.confirmButton.interactable=false;


	}
	else {
	 empireScene.instance.insufficientGems.SetActive(true);
		 empireScene.instance.insufficientGems.transform.GetChild(4).GetComponent<Text>().text="Insufficient Resources !";
	}
}
	public GameObject stackOngoing;
	public GameObject stackOngoingContent;

	public void yesIntterogation()
	{

		if (ongoingList.Count <= 5) 
		{
			secondaryCardFear( isSuccess =>
			{
				if(isSuccess)
				{
					int wheatToSave = PlayerParameters._instance.myPlayerParameter.wheat - EmpireManager._instance.prison.foodRequiredForSecondary [EmpireManager._instance.prison.currentLevel];
					int goldToSave = PlayerParameters._instance.myPlayerParameter.gold - EmpireManager._instance.prison.foodRequiredForSecondary [EmpireManager._instance.prison.currentLevel];
					foodPrison.text=wheatToSave.ToString();
					GodlPrison.text=goldToSave.ToString();
					saveSpriteLockedPrison=lockButtonNamePrisonSecondary.GetComponent<Image>().sprite;
					lockButtonNamePrisonSecondary = clickforOngoing;
					buttonAdded.Add (lockButtonNamePrisonSecondary);
					textAdded.Add (lockButtonNamePrisonSecondary.GetComponentInChildren<Text> ());
					GameObject newCard = (GameObject)Instantiate (Resources.Load ("ongoingContent"));
					newCard.transform.SetParent (empireSceneNew.instance.containerOFongoing.transform);
					newCard.transform.localScale = Vector3.one;
					stackOngoingContent = newCard;
					newCard.transform.GetChild (2).GetComponent<Image> ().sprite = clickforOngoing.GetComponent<Image> ().sprite;
					//newCard.transform.GetChild(13).GetComponent<Text>().text=waitTime.ToString();
					newCard.transform.GetChild(11).GetComponent<Text>().text=EmpireManager._instance.prison.foodRequiredForSecondary [EmpireManager._instance.prison.currentLevel].ToString();
					newCard.transform.GetChild(12).GetComponent<Text>().text=EmpireManager._instance.prison.foodRequiredForSecondary [EmpireManager._instance.prison.currentLevel].ToString();
					int positionOfLockedCard = CardsManager._instance.PositionOfCardInList (selectedFearCard);
					int fearToSave = (CardsManager._instance.myCaptives[positionOfLockedCard].fear_factor);
						newCard.transform.GetChild(7).GetComponent<Text>().text=fearToSave.ToString();
						newCard.transform.GetChild(14).GetComponent<Button>().onClick.AddListener (() => {cancelInterrogationClick();});
						newCard.transform.GetChild(15).GetComponent<Button>().onClick.AddListener (() => {useGemsClick();});
						newCard.transform.GetChild(15).GetComponent<Button>().interactable=false;
						newCard.transform.GetChild(15).GetComponent<Button>().interactable=false;

//
//					int tempAdd= EmpireManager._instance.prison.fearIncrease[EmpireManager._instance.prison.currentLevel];
//					newCard.transform.GetChild(6).transform.GetChild(3).GetComponent<Text>().text=tempAdd.ToString();
					int cardIdLocked = CardsManager._instance.PositionOfCaptiveInList (int.Parse (clickforOngoing.name));
					newCard.transform.GetChild (0).GetComponent<Text> ().text = "Prisoner : " + CardsManager._instance.myCaptives [cardIdLocked].card_name;
					ongoingContent.Add (newCard.gameObject);
					cardCount.Add (selectedFearCard);
					GameObject newCard2 = (GameObject)Instantiate (Resources.Load ("listCard"));
					newCard2.transform.SetParent (containerOfCardsList.transform);
					newCard2.transform.localScale = Vector3.one;

					newCard2.GetComponent<Image> ().sprite = clickforOngoing.GetComponent<Image> ().sprite;
					stackOngoing = newCard2;
					ongoingList.Add (newCard2.gameObject);
					for (int i=0; i<ongoingList.Count; i++) {
						print (" " + ongoingList.Count);
						prisonerQueue.text = "Prisoner queue: " + ongoingList.Count + " / 5";
					}

					//isFilled = true;
					lockButtonNamePrisonSecondary.GetComponentInChildren<Text> ().text = "SELECTED";
					lockButtonNamePrisonSecondary.interactable = false;
					interroDialog.SetActive (false);
					clickforOngoing = null;

				}
				else
				{
					  empireScene.instance.popupFromServer.ShowPopup ("Cannot Update at this time!");

				}
				});






		}
	}




	public void prisonShownCards()
	{

		if (selectedCardPrison == 1)
		{
			if(lockButtonPrison != null)
			{
				lockButtonPrison.text = "";
				empireScene.instance.selectedCards.Remove (lockButtonNamePrison);
			}
			selectedCardPrison = 0;
		}
		if (selectedCardPrisonSecondary == 1)
		{
			if(lockButtonPrisonSecondary != null)
			{
				lockButtonPrisonSecondary.text = "";
				empireScene.instance.selectedCards.Remove (lockButtonNamePrisonSecondary);
			}
			selectedCardPrisonSecondary = 0;
		}

		if(!empireScene.instance.wentFromSecondary)
		{


			empireScene.instance.buildingUpdateFoodInCardSelectionLayout.text = EmpireManager._instance.prison.foodRequiredForPrimary[EmpireManager._instance.prison.currentLevel].ToString ();
			empireScene.instance.buildingUpdateGoldInCardSelectionLayout.text = EmpireManager._instance.prison.foodRequiredForPrimary[EmpireManager._instance.prison.currentLevel].ToString ();
			float timerReqd = EmpireManager._instance.prison.timeRequiredPerLevel[EmpireManager._instance.prison.currentLevel]*180;
			int minutesText = Mathf.FloorToInt( timerReqd/ 60);
			int secondsText = Mathf.FloorToInt(timerReqd % 60);
			empireScene.instance.buildingUpdateTimeInCardSelectionLayout.text = minutesText.ToString("00")+":"+secondsText.ToString("00");
		}
		else
		{
			empireScene.instance.buildingUpdateFoodInCardSelectionLayout.text = ((int)EmpireManager._instance.prison.foodRequiredForSecondary[EmpireManager._instance.prison.currentLevel]).ToString ();
			empireScene.instance.buildingUpdateGoldInCardSelectionLayout.text = ((int)EmpireManager._instance.prison.foodRequiredForSecondary[EmpireManager._instance.prison.currentLevel]).ToString ();
			empireScene.instance.buildingUpdateTimeInCardSelectionLayout.text = "15:00";
		}
	}

	// public void updateBuildingPrison()
	// {
	// 	if (EmpireManager._instance.prison.castleLevelRequired [EmpireManager._instance.prison.currentLevel] <= EmpireManager._instance.castle.currentLevel + 1) {
	// 		int foodREquirementStorage = EmpireManager._instance.prison.foodRequiredForPrimary [EmpireManager._instance.prison.currentLevel];
	// 		if (PlayerParameters._instance.myPlayerParameter.wheat >= foodREquirementStorage && PlayerParameters._instance.myPlayerParameter.gold >= foodREquirementStorage) {
	// 			empireScene.instance.buildingUpgradeLayout.SetActive (true);
	// 			empireScene.instance.wentFromSecondary = false;
	// 			empireScene.instance.ShowAllCards ();
	// 		} else {
	// 			empireScene.instance.insufficientGems.SetActive (true);
	// 			empireScene.instance.insufficientGems.transform.GetChild (4).GetComponent<Text> ().text = "Insufficient Resources !";
	// 		}
	// 	} else {
	// 		empireScene.instance.insufficientGems.SetActive (true);
	// 		empireScene.instance.insufficientGems.transform.GetChild (4).GetComponent<Text> ().text = "First Update Castle to Proceed !";
	//
	//
	// 	}
	//
	// }

	public void prisonCompleteupdate()
	{

		if (!empireScene.instance.wentFromSecondary)
		{
			EmpireManager._instance.prison.primaryCardNo = int.Parse (lockButtonNamePrison.name);
			int wheatTosend = PlayerParameters._instance.myPlayerParameter.wheat - EmpireManager._instance.prison.foodRequiredForPrimary [EmpireManager._instance.prison.currentLevel];
			int goldTosend = PlayerParameters._instance.myPlayerParameter.gold - EmpireManager._instance.prison.foodRequiredForPrimary [EmpireManager._instance.prison.currentLevel];
			empireScene.instance.createCard (empireScene.instance.buttonName, EmpireManager._instance.prison.currentLevel, EmpireManager._instance.prison.primaryCardNo, EmpireManager._instance.prison.primaryCardNo.ToString (), EmpireManager._instance.prison.currentExp, (EmpireManager._instance.prison.timeRequiredPerLevel [EmpireManager._instance.prison.currentLevel] * 60).ToString (), wheatTosend.ToString (), goldTosend.ToString (), isSucess => {
				if (isSucess) {
					prisonMinePrimaryImage.sprite = lockButtonNamePrison.GetComponent<Image> ().sprite;
					PlayerParameters._instance.myPlayerParameter.wheat -= EmpireManager._instance.prison.foodRequiredForPrimary [EmpireManager._instance.prison.currentLevel];
					PlayerParameters._instance.myPlayerParameter.gold -= EmpireManager._instance.prison.foodRequiredForPrimary [EmpireManager._instance.prison.currentLevel];
					Debug.Log ("wheat final = " + PlayerParameters._instance.myPlayerParameter.wheat);
					EmpireManager._instance.prison.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
					EmpireManager._instance.prison.goldText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
					loadingScene.Instance.randomCards.Remove (int.Parse (lockButtonNamePrison.name));
					tempButtonPrisonPrimary = (string)lockButtonNamePrison.name;
					PlayerPrefs.SetString ("tempButtonPrison", tempButtonPrisonPrimary);
					empireScene.instance.confirmButton.interactable = false;
					GameObject[] deleteCards = GameObject.FindGameObjectsWithTag ("updateCards");
					for (int j = 0; j < deleteCards.Length; j ++) {
						deleteCards [j].GetComponent<Button> ().interactable = false;

					}
					empireScene.instance.updateBuilding.SetActive (false);
					prisonPrimaryClockText = EmpireManager._instance.prison.timeRequiredPerLevel [EmpireManager._instance.prison.currentLevel] * 3600f;
					prisonUpgradeButton.interactable = false;
					isprisonPrimary = true;
					PlayerPrefs.SetString ("updatingPrison", "yes");
				} else {
					EmpireManager._instance.prison.primaryCardNo = -1;
					empireScene.instance.popupFromServer.ShowPopup ("Cannot lock the card at this time!");
				}
			});
		}
		else
		{
			empireScene.instance.prisionIntergogate();
			isFilled=true;



			EmpireManager._instance.prison.secondaryCardNo = int.Parse (lockButtonNamePrisonSecondary.name);
			// int wheatToSave = PlayerParameters._instance.myPlayerParameter.wheat - EmpireManager._instance.prison.foodRequiredForSecondary [EmpireManager._instance.prison.currentLevel];
			// int goldToSave = PlayerParameters._instance.myPlayerParameter.gold - EmpireManager._instance.prison.foodRequiredForSecondary [EmpireManager._instance.prison.currentLevel];
			saveSpriteLockedPrison=lockButtonNamePrisonSecondary.GetComponent<Image>().sprite;
			GameObject.Find("interogatePlayer").GetComponent<Image>().sprite=saveSpriteLockedPrison;
			selectInterrogateButtonClick.transform.GetChild(1).GetComponent<Text>().text="LOCKED";
			selectInterrogateButtonClick.transform.GetChild(1).GetComponent<Text>().color=new Color32(255,255,255,255);

			selectInterrogateButtonClick.interactable=false;
			for(int i = 0; i < CardsManager._instance.myCaptives.Count ; i++)
			{
				int tempCount=CardsManager._instance.myCaptives.Count;
				int tempcount2 =(EmpireManager._instance.prison.foodRequiredForSecondary [EmpireManager._instance.prison.currentLevel])*tempCount;
				totalFood.text=tempcount2.ToString();
				totalGold.text=tempcount2.ToString();
				int tempcount3=(15)*tempCount;
				totalTime.text=tempcount3.ToString();
				//	totalFood.text=EmpireManager._instance.prison.foodRequiredForSecondary [EmpireManager._instance.prison.currentLevel].ToString();
				// (totalFood.text) *tempCount.ToString();
				//=(EmpireManager._instance.prison.foodRequiredForSecondary [EmpireManager._instance.prison.currentLevel].ToString())*tempCount;
				//	totalFood.text=((EmpireManager._instance.prison.foodRequiredForSecondary [EmpireManager._instance.prison.currentLevel].ToString())*tempCount);

			}

			gemsPrison.text=PlayerParameters._instance.myPlayerParameter.gems.ToString();
			foodPrison.text=PlayerParameters._instance.myPlayerParameter.wheat.ToString();
			GodlPrison.text=PlayerParameters._instance.myPlayerParameter.gold.ToString();
			empireScene.instance.confirmButton.interactable=false;
			empireScene.instance.updateBuilding.SetActive(false);
			EmpireManager._instance.prison.storgeLockDown.interactable=true;
			prisonSecondaryImage.sprite=saveSpriteLockedPrison;
			loadingScene.Instance.randomCards.Remove (EmpireManager._instance.prison.secondaryCardNo);


		}

	}


	public void prisonChosenCard()
	{
		if (EmpireManager._instance.prison.castleLevelRequired [EmpireManager._instance.prison.currentLevel] <= EmpireManager._instance.castle.currentLevel + 1) {
			empireScene.instance.randomLoadGround = (int)UnityEngine.Random.Range (0, loadingScene.Instance.randomCards.Count);
			EmpireManager._instance.prison.primaryCardNo = loadingScene.Instance.randomCards [empireScene.instance.randomLoadGround];
			int wheatTosend = PlayerParameters._instance.myPlayerParameter.wheat - EmpireManager._instance.prison.foodRequiredForPrimary [EmpireManager._instance.prison.currentLevel];
			int goldTosend = PlayerParameters._instance.myPlayerParameter.gold;
			empireScene.instance.createCard (empireScene.instance.buttonName, EmpireManager._instance.prison.currentLevel, EmpireManager._instance.prison.primaryCardNo, EmpireManager._instance.prison.primaryCardNo.ToString (), EmpireManager._instance.prison.currentExp, (EmpireManager._instance.prison.timeRequiredPerLevel [EmpireManager._instance.prison.currentLevel] * 60).ToString (),wheatTosend.ToString (), goldTosend.ToString (), isSuccess => {
				if (isSuccess) {
					prisonPrimaryClockText = EmpireManager._instance.prison.timeRequiredPerLevel [EmpireManager._instance.prison.currentLevel] * 3600f;
					EmpireManager._instance.prison.chosenCardButton.interactable = false;
					PlayerParameters._instance.myPlayerParameter.wheat -= EmpireManager._instance.prison.foodRequiredForPrimary [EmpireManager._instance.prison.currentLevel];
					EmpireManager._instance.prison.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString();
					int spriteToFetch = CardsManager._instance.PositionOfCardInList (loadingScene.Instance.randomCards [ randomLoadPrison]);
					prisonMinePrimaryImage.sprite = CardsManager._instance.mycards [spriteToFetch].cardSpriteFromResources;
					PlayerPrefs.SetString ("tempButtonPrison", loadingScene.Instance.randomCards [randomLoadPrison].ToString ());
					PlayerPrefs.SetString ("chosenCardPrison", "yes");
					PlayerPrefs.SetString ("updatingPrison", "yes");
					loadingScene.Instance.randomCards.Remove (EmpireManager._instance.prison.primaryCardNo);
					isprisonPrimary = true;
				}
				else
				{
					EmpireManager._instance.prison.primaryCardNo = -1;
					empireScene.instance. popupFromServer.ShowPopup ("Cannot lock the card at this time!");
				}
			});
		} else {
			empireScene.instance. insufficientGems.SetActive (true);
			empireScene.instance.insufficientGems.transform.GetChild (4).GetComponent<Text> ().text = "First Update Castle to Proceed !";
		}
	}

	public void prisonUpdateInstantToEmpire()
	{
		print("=========  Prison CLICK =========");
		instantPrison(true , PlayerParameters._instance.myPlayerParameter.gems- loadingScene.Instance.gemsToDeductOnInstantUpgrade , isSuccess =>{
			if(isSuccess)
			{
				if(PlayerPrefs.GetString("tempButtonPrion")==tempButtonPrisonPrimary)
				{

					lockButtonNamePrison.name = tempButtonPrisonPrimary;
					lockButtonNamePrison.interactable=true;

				}
				PlayerParameters._instance.myPlayerParameter.gems -= loadingScene.Instance.gemsToDeductOnInstantUpgrade;
				empireScene.instance. GemsText[13].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();
				//				GemsText[10].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();
				//				GemsText[11].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();
				//				GemsText[12].text = PlayerParameters._instance.myPlayerParameter.gems.ToString();


				EmpireManager._instance.prison.chosenCardButton.interactable=true;
				EmpireManager._instance.prison.instantUpdateButton.interactable=false;
				empireScene.instance. instant1.SetActive(false);
				empireScene.instance.instant2.SetActive(true);
			}
		});
	}


	public IEnumerator CheckForPrisonInStart()
	{
		while (!loadingScene.Instance.readyTogo) {
			yield return 0;
		}
		float percentageVal = (EmpireManager._instance.prison.currentExp/(float)EmpireManager._instance.prison.requiredExpPerLevel[EmpireManager._instance.prison.currentLevel]);
		prisonUpdateImageText.text = Mathf.FloorToInt (percentageVal*100)+"%";
		EmpireManager._instance.prison.levelSlider.value = percentageVal;

		//		float finalScale = 1+(7/100f*percentageVal);
		//		float finalScale = (8/100f*percentageVal);
		//		storageUpdateImageV = updateStorage.transform.localScale;
		//		iTween.ScaleTo(updateStorage.gameObject,iTween.Hash("x",finalScale,"time",1.5f));
		if (EmpireManager._instance.prison.primaryCardNo > 0) {
			if (EmpireManager._instance.prison.timeOfLockOfPrimary != null) {
				System.TimeSpan diff = TimeManager._instance.GetCurrentServerTime () - EmpireManager._instance.prison.timeOfLockOfPrimary;
				float diffSeconds = Mathf.Abs ((float)diff.TotalSeconds);
				Debug.Log ("diffSeconds  = " + diffSeconds);
				Debug.Log ("timeReqd = " + EmpireManager._instance.prison.timeRequiredPerLevel [EmpireManager._instance.prison.currentLevel] * 3600);
				if (EmpireManager._instance.prison.timeRequiredPerLevel [EmpireManager._instance.prison.currentLevel] * 3600 > diffSeconds && diffSeconds > 0) {
					Debug.Log ("time done???");
					isprisonPrimary = true;
					PlayerPrefs.SetString ("updatingPrison", "yes");
					PlayerPrefs.SetString ("tempButtonPrison", EmpireManager._instance.prison.primaryCardNo.ToString ());

					loadingScene.Instance.randomCards.Remove (EmpireManager._instance.prison.primaryCardNo);
					int spriteToFetch = 0;
					for (int i = 0; i < CardsManager._instance.mycards.Count; i++) {
						if (EmpireManager._instance.prison.primaryCardNo == CardsManager._instance.mycards [i].card_id_in_playerList) {
							spriteToFetch = i;
							break;
						}
					}
					EmpireManager._instance.prison.instantUpdateButton.interactable = true;
					prisonPrimaryClockText = EmpireManager._instance.prison.timeRequiredPerLevel [EmpireManager._instance.prison.currentLevel] * 3600 - diffSeconds;
					prisonMinePrimaryImage.sprite = CardsManager._instance.mycards [spriteToFetch].cardSpriteFromResources;
					// add card to the primary image
				} else {
					PlayerPrefs.SetString ("tempButtonPrison", EmpireManager._instance.prison.primaryCardNo.ToString ());
					empireScene.instance.buttonName = "prison";

					loadingScene.Instance.randomCards.Remove (EmpireManager._instance.prison.primaryCardNo);
					instantPrison ();
					EmpireManager._instance.storage.primaryCardNo = -1;
					isprisonPrimary = false;
					PlayerPrefs.SetString ("updatingPrison", "no");
				}
			}
			else {
				isprisonPrimary = false;
				PlayerPrefs.SetString ("updatingPrison", "no");
			}
		} else {
			isprisonPrimary = false;
			PlayerPrefs.SetString ("updatingPrison", "no");
		}

	}

	public void instantPrison(bool wasInstant = false , int gems = 0  , Action<bool> callBack = null)
	{
		if (!wasInstant) {
			UpdatePrisonFcn ();
			loadingScene.Instance.updateBuilding(empireScene.instance.buttonName , EmpireManager._instance.prison.currentLevel ,  -1 , EmpireManager._instance.prison.primaryCardNo.ToString() ,EmpireManager._instance.prison.currentExp  ,(EmpireManager._instance.prison.timeRequiredPerLevel[EmpireManager._instance.prison.currentLevel]*60).ToString());

		} else {
			newMenuScene.instance.loader.SetActive (true);
			NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
				if (isConnected)
				{

					int cardLocked = CardsManager._instance.PositionOfCardInList (int.Parse (PlayerPrefs.GetString("tempButtonPrison")));
					int expToSend = empireScene.instance.currentExperienceOfBuilding+CardsManager._instance.mycards[cardLocked].leadership;
					int levelToSend = EmpireManager._instance.storage.currentLevel;
					if(expToSend >= empireScene.instance.finalExperienceOfBuilding)
					{
						expToSend = - empireScene.instance.finalExperienceOfBuilding + expToSend;
						levelToSend++;
					}


					loadingScene.Instance.updateBuildingInstant(empireScene.instance.buttonName , levelToSend ,  -1 , EmpireManager._instance.storage.primaryCardNo.ToString() , expToSend ,(EmpireManager._instance.storage.timeRequiredPerLevel[EmpireManager._instance.storage.currentLevel]*60).ToString() , gems , isSuccess =>{
						if(isSuccess)
						{
							UpdatePrisonFcn();
							callBack(true);
						}
						else
						{
							newMenuScene.instance.loader.SetActive (false);
							newMenuScene.instance.popupFromServer.ShowPopup ("Cannot Update at this time");
						}
						newMenuScene.instance.loader.SetActive (false);
					});

				}
				else
				{
					newMenuScene.instance.loader.SetActive (false);
					newMenuScene.instance.popupFromServer.ShowPopup ("Cannot Update at this time");
				}
			});
		}


	}



	void UpdatePrisonFcn()
	{
		print("========  PRISON PRIMARY UPDATED =======");
		PlayerPrefs.SetString("chosenCardPrison","no");
		loadingScene.Instance.randomCards.Add (EmpireManager._instance.storage.primaryCardNo);
		EmpireManager._instance.storage.primaryCardNo = -1;
		prisonMinePrimaryImage.sprite=prisonloadImage;
		isprisonPrimary=false;
		prisonUpgradeButton.interactable=true;
		//		empireScene.instance.currentExperienceOfBuilding+=1000;

		int cardLocked = CardsManager._instance.PositionOfCardInList (int.Parse (PlayerPrefs.GetString("tempButtonPrison")));
		empireScene.instance.currentExperienceOfBuilding+=CardsManager._instance.mycards[cardLocked].leadership;

		EmpireManager._instance.prison.currentExp = empireScene.instance.currentExperienceOfBuilding;
		if(empireScene.instance.currentExperienceOfBuilding >= empireScene.instance.finalExperienceOfBuilding)
		{
			//			updateStorage.transform.localScale = Vector3.one;
			empireScene.instance.currentExperienceOfBuilding = - empireScene.instance.finalExperienceOfBuilding + empireScene.instance.currentExperienceOfBuilding;
			EmpireManager._instance.prison.currentExp = empireScene.instance.currentExperienceOfBuilding;
			EmpireManager._instance.prison.currentLevel++;
			int currentVal = EmpireManager._instance.prison.finalValue1[EmpireManager._instance.prison.currentLevel];
			int finalVal = EmpireManager._instance.prison.finalValue1[EmpireManager._instance.prison.currentLevel+1];
			prisonNow.text=currentVal.ToString();
			prisonNext.text=finalVal.ToString();
			prisonLevel.text="Lvl "+(EmpireManager._instance.prison.currentLevel+1).ToString();
			empireScene.instance.finalExperienceOfBuilding = EmpireManager._instance.prison.requiredExpPerLevel[EmpireManager._instance.prison.currentLevel];


			if(EmpireManager._instance.prison.castleLevelRequired[EmpireManager._instance.prison.currentLevel] > EmpireManager._instance.castle.currentLevel)
			{
				EmpireManager._instance.prison.currentExp = 0;
				empireScene.instance.currentExperienceOfBuilding = 0;
			}
		}

		float percentageVal = (empireScene.instance.currentExperienceOfBuilding/(float)empireScene.instance.finalExperienceOfBuilding);
		prisonUpdateImageText.text = Mathf.FloorToInt (percentageVal*100)+"%";
		EmpireManager._instance.prison.levelSlider.value = percentageVal;

		print ("====== CURRENT LEVEL prison???  ====" + EmpireManager._instance.prison.currentLevel);
		prisonPrimaryClockString.text="00:00";
		PlayerPrefs.SetString("updatingPrison","no");
		PlayerPrefs.SetString("tempButtonPrison","no");
		if(empireScene.instance.buildingUpgradeLayout.activeInHierarchy==true)
		{
			empireScene.instance.ShowAllCards();
		}
		//EmpireManager._instance.currentFood -=EmpireManager._instance.storage.foodRequiredForPrimary [EmpireManager._instance.storage.currentLevel];

		EmpireManager._instance.prison.upgradeFoodCostPrimary.text=EmpireManager._instance.prison.foodRequiredForPrimary [EmpireManager._instance.prison.currentLevel].ToString();
		EmpireManager._instance.prison.upgradeGoldCostPrimary.text=EmpireManager._instance.prison.foodRequiredForPrimary [EmpireManager._instance.prison.currentLevel].ToString();
		float tempTimer=EmpireManager._instance.prison.timeRequiredPerLevel [EmpireManager._instance.prison.currentLevel]*3600f/60;
		EmpireManager._instance.prison.upgradeTimePrimary.text = tempTimer.ToString ();

	}



	// Update is called once per frame
	public void Update ()
	{



		if (!loadingScene.Instance.readyTogo)
			return;
		if(isprisonPrimary==true)
		{

			prisonPrimaryClockText-=Time.deltaTime;
			if(prisonPrimaryClockText>0)
			{

				EmpireManager._instance.prison.chosenCardButton.interactable=false;
				EmpireManager._instance.prison.instantUpdateButton.interactable=true;
				prisonUpgradeButton.interactable=false;
				int minutesText = Mathf.FloorToInt(prisonPrimaryClockText/60);
				int secondsText = Mathf.FloorToInt(prisonPrimaryClockText % 60);
				int hours = (int)(Mathf.FloorToInt(prisonPrimaryClockText/3600)%24);
				int minutesText2 = minutesText - (hours*60);


				if(minutesText<=59)
				{
					prisonPrimaryClockString.text =   "00:" +minutesText.ToString("00")+":"+secondsText.ToString("00");

				}
				else
				{
					prisonPrimaryClockString.text =   hours.ToString("00")+":" +minutesText2.ToString("00")+":"+secondsText.ToString("00");
				}
				if(empireScene.instance != null && loadingScene.Instance.instantGemsTextForPopup.gameObject.activeInHierarchy && empireScene.instance.buttonName == "prison")
				{
					if(secondsText != 0)
						minutesText++;
					loadingScene.Instance.gemsToDeductOnInstantUpgrade = minutesText;
					loadingScene.Instance.instantGemsTextForPopup.text = loadingScene.Instance.gemsToDeductOnInstantUpgrade.ToString ();
				}


			}
			else
			{
				EmpireManager._instance.prison.chosenCardButton.interactable=true;
				EmpireManager._instance.prison.instantUpdateButton.interactable=false;
				print("========  PRISON PRIMARY UPDATED =======");
				instantPrison();
			}


		}


		if(!isFilled)
		{
//			ongoingContent[ongoingContent.Count].transform.GetChild(13).GetComponent<Button>().interactable=false;
//			ongoingContent[ongoingContent.Count].transform.GetChild(14).GetComponent<Button>().interactable=false;
			if(ongoingList.Count==0)
			{
				defaultFearText.text = ""; 
				selectInterrogateButtonClick.interactable=false;
				selectInterrogateButtonClick.transform.GetChild(0).GetComponent<Image>().sprite=afterRest;
				selectInterrogateButtonClick.transform.GetChild(0).GetComponent<Image>().color=new Color32(143,109,109,255);
				EmpireManager._instance.prison.storgeLockDown.interactable = true;
				prisonSecondaryImage.sprite = prisonloadImageSecondary;
				selectInterrogateButtonClick.GetComponent<Image>().color=new Color32(255,255,255,255);
				selectInterrogateButtonClick.transform.GetChild(1).GetComponent<Text>().color=new Color32(255,255,255,255);
				selectInterrogateButtonClick.transform.GetChild(1).GetComponent<Text>().text="First select Prisoners from Available";
				totalFood.text="";
				totalGold.text="";

				totalTime.text="";


			}
			else
			{

				selectInterrogateButtonClick.interactable=true;
				selectInterrogateButtonClick.transform.GetChild(1).GetComponent<Text>().text="Select Interrogator click here";
				selectInterrogateButtonClick.transform.GetChild(0).GetComponent<Image>().color=new Color32(255,255,255,255);
				selectInterrogateButtonClick.transform.GetChild(1).GetComponent<Text>().color=new Color32(0,0,0,255);

			}
		}


	if (isFilled == true)
	{
			

		for(int i=0; i<ongoingList.Count;i++)
		{
			waitTime=900;
			ongoingList[0].GetComponent<Image>().fillAmount -= 1.0f*Time.deltaTime/waitTime;

			//ongoingList[i].GetComponent<Image>().fillAmount -= 1.0f*Time.deltaTime/waitTime;
			float minutes = Mathf.Floor(waitTime / 60);
			minutes-= Time.deltaTime/waitTime;
			string tempR=ongoingContent[i].transform.GetChild(12).GetComponent<Text>().ToString();
			EmpireManager._instance.prison.storgeLockDown.interactable=false;
				int tempCount= ongoingList.Count;
				int tempcount2 =(EmpireManager._instance.prison.foodRequiredForSecondary [ongoingList.Count])*tempCount;
				int tempcount3=(15)*tempCount;
				totalFood.text=tempcount2.ToString();
				totalGold.text=tempcount2.ToString();

				totalTime.text=tempcount3.ToString();
				ongoingContent[i].transform.GetChild(14).GetComponent<Button>().interactable=true;
				ongoingContent[i].transform.GetChild(15).GetComponent<Button>().interactable=true;
				defaultFearText.text = CardsManager._instance.myCaptives [0].fear_factor.ToString();


		}

	}

	for(int i=0; i<ongoingList.Count;i++)
	{

		if(ongoingList[i].GetComponent<Image>().fillAmount == 0)
		{

//			int tempAdd = EmpireManager._instance.prison.fearIncrease[EmpireManager._instance.prison.currentLevel];
//			int positionOfLockedCard = CardsManager._instance.PositionOfCardInList (selectedFearCard);
//			int fearToSave = (CardsManager._instance.myCaptives[positionOfLockedCard].fear_factor)+tempAdd;
//				CardsManager.CardParameters a = CardsManager._instance.myCaptives [positionOfLockedCard];
//				a.fear_factor = fearToSave;
//				CardsManager._instance.myCaptives [positionOfLockedCard] = a;
			//----
			int tempCount= ongoingList.Count;
			int tempcount2 =(EmpireManager._instance.prison.foodRequiredForSecondary [ongoingList.Count])*tempCount;
			int tempcount3=(15)*tempCount;
			totalFood.text=tempcount2.ToString();
			totalGold.text=tempcount2.ToString();

			totalTime.text=tempcount3.ToString();
				ongoingContent[i].transform.GetChild(14).GetComponent<Button>().interactable=true;
				ongoingContent[i].transform.GetChild(15).GetComponent<Button>().interactable=true;
				//--
//			ongoingContent[i].transform.GetChild(7).GetComponent<Text>().text=fearToSave.ToString();
			buttonAdded[i].interactable=true;
			textAdded[i].text="";
			buttonAdded.RemoveAt(i);
			cardCount.RemoveAt (i);
			textAdded.RemoveAt(i);
			Destroy(ongoingList[i].gameObject);
			ongoingList.RemoveAt(i);
			Destroy(ongoingContent[i].gameObject);
			ongoingContent.RemoveAt(i);
			prisonerQueue.text= "Prisoner queue: "+ongoingList.Count+" / 5";

//				for(int i = 0; i < CardsManager._instance.myCaptives.Count ; i++)
//				{
//					int tempCount=CardsManager._instance.myCaptives.Count;
//					int tempcount2 =(EmpireManager._instance.prison.foodRequiredForSecondary [EmpireManager._instance.prison.currentLevel])*tempCount;
//					totalFood.text=tempcount2.ToString();
//					totalGold.text=tempcount2.ToString();
//					int tempcount3=(15)*tempCount;
//					totalTime.text=tempcount3.ToString();
//					//	totalFood.text=EmpireManager._instance.prison.foodRequiredForSecondary [EmpireManager._instance.prison.currentLevel].ToString();
//					// (totalFood.text) *tempCount.ToString();
//					//=(EmpireManager._instance.prison.foodRequiredForSecondary [EmpireManager._instance.prison.currentLevel].ToString())*tempCount;
//					//	totalFood.text=((EmpireManager._instance.prison.foodRequiredForSecondary [EmpireManager._instance.prison.currentLevel].ToString())*tempCount);
//
//				}
			if(ongoingList.Count.Equals(0))
			{
				selectInterrogateButtonClick.transform.GetChild(1).GetComponent<Text>().text="Select Interrogator click here";
				selectInterrogateButtonClick.transform.GetChild(0).GetComponent<Image>().sprite=afterRest;
				selectInterrogateButtonClick.transform.GetChild(0).GetComponent<Image>().color=new Color32(255,255,255,255);
					defaultFearText.text = "";
				selectInterrogateButtonClick.interactable=true;
				EmpireManager._instance.prison.storgeLockDown.interactable=true;
				prisonSecondaryImage.GetComponent<Image>().sprite= prisonloadImageSecondary;
				isFilled=false;


			}
			else
			{
				return;
			}

				//---------
				{
					secondaryCardFearAdded( isSuccess =>
						{
							if(isSuccess)
							{
							


							}
							else
							{
								empireScene.instance.popupFromServer.ShowPopup ("Cannot Update at this time!");

							}
						});






				}

				//-------

		}

}




	}
}
