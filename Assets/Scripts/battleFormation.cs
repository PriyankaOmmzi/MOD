using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;

public class battleFormation : MonoBehaviour 
{
	public GameObject sort;
	public GameObject setting;
	public GameObject cardSelection1;
	public GameObject editFormation;
	bool isMneuActive=false;
	public Button[] bottomsButtons;
	public GameObject chatBtn;
	public GameObject menu;
	public Transform leftPosition;
	public Transform rightPosition;
	public Transform midPos;
	public int leftCount=0;
	public GameObject deck1;
	public GameObject deck2;
	public GameObject deck3;
	bool iseditDormation=true;

	public List <CardDeck> cardDecks;
	public List <CardDeck> savedCardDeck;
	public string deckSelectCount;
	public Button confirm;
	public GameObject containerOfCardScroll;
	public List <Image> myCardsInScroll = new List<Image>();

	public Text selectedCardAttack;
	public Text selectedCardDefense;
	public Text selectedCardLeaderShip;
	public Text deckAttack;
	public Text deckDefense;
	public Text deckLeaderShip;
	public Text rankText;
	public Text costText;
	public List <int> currentSelectedCards = new List<int>();
	public static bool isQuest;

	// Use this for initialization
	void Start () 
	{
		resetPrevious();
		deckSelectCount="0";
		editFormation.SetActive(true);
		cardSelection1.SetActive(false);
		loadingScene.Instance.playerProfilePanel.SetActive(false);
		setting.SetActive(false);
		sort.SetActive(false);
		menu.SetActive(false);
	}

	void OnDisable()
	{
		if (isMneuActive) {
			backButton ();
		}
	}

	void OnEnable()
	{
		Debug.Log ("leftCount"+leftCount);
		for (int i = myCardsInScroll.Count - 1; i >= 0; i--) {
			if (myCardsInScroll [i] != null) {
				Destroy (myCardsInScroll [i].gameObject);
			}
		}
		myCardsInScroll.Clear ();
//		if (isQuest) {
//			ShowDeckStats (0);
//
//		} else {


		for (int k = 0; k < savedCardDeck.Count; k++) {
			savedCardDeck [k].noOfCardsSelected = 0;
			for (int l = 0; l < savedCardDeck [k].cardRows.Count; l++) {
				savedCardDeck [k].cardRows [l].cardIdsForRow.Clear ();
			}
		}

		for (int j = 0; j < cardDecks.Count; j++) {
			cardDecks [j].deckCost = 0;
			savedCardDeck [j].noOfCardsSelected = cardDecks [j].noOfCardsSelected;
			for (int k = 0; k < cardDecks [j].cardRows.Count; k++) {
				
				int attackOfRow = 0;
				int defenseOfRow = 0;
				int leadershipOfRow = 0;
				int soldiersOfRow = 0;
				for (int t = 0; t < cardDecks [j].cardRows [k].cardIdsForRow.Count; t++) {
					savedCardDeck [j].cardRows [k].cardIdsForRow.Add (cardDecks [j].cardRows [k].cardIdsForRow [t]);
					int positionOfCard = CardsManager._instance.PositionOfCardInList (cardDecks [j].cardRows [k].cardIdsForRow [t]);
					cardDecks [j].deckCost += CardsManager._instance.mycards [positionOfCard].cardCost;
					cardDecks [j].cardRows [k].cardObjectsForRow [t].transform.FindChild ("cardImage").GetComponent<Image> ().sprite = CardsManager._instance.mycards [positionOfCard].cardSpriteFromResources;
					cardDecks [j].cardRows [k].cardObjectsForRow [t].transform.FindChild ("cardImage").GetComponent<Image> ().color = new Color(1,1,1,1);
					int cardID = CardsManager._instance.PositionOfCardInList (cardDecks[j].cardRows[k].cardIdsForRow[t]);
					attackOfRow+=CardsManager._instance.mycards[cardID].attack;
					defenseOfRow+=CardsManager._instance.mycards[cardID].defense;
					leadershipOfRow+=CardsManager._instance.mycards[cardID].leadership;
					soldiersOfRow+=CardsManager._instance.mycards[cardID].card_soldiers;

				}
				cardDecks[j].cardRows[k].rowAttack.text = attackOfRow.ToString ();
				cardDecks[j].cardRows[k].rowDefense.text = defenseOfRow.ToString ();
				cardDecks[j].cardRows[k].rowLeadership.text = leadershipOfRow.ToString ();
				if (leadershipOfRow > 0) {

					float soldierPercentage = soldiersOfRow / (float)leadershipOfRow;
					if (soldierPercentage > 1)
						soldierPercentage = 1;
					cardDecks [j].cardRows [k].soldierPercentageInRow.text = Mathf.FloorToInt (soldierPercentage * 100).ToString () + "%";
					cardDecks [j].cardRows [k].soldierSlider.value = soldierPercentage;
				} else {
					cardDecks [j].cardRows [k].soldierPercentageInRow.text = "0%";
					cardDecks [j].cardRows [k].soldierSlider.value = 0;
				}
			}
		}


		for (int i = 0; i < CardsManager._instance.mycards.Count; i++) {
			GameObject newCard = (GameObject)Instantiate (Resources.Load ("cardItem"));
			newCard.transform.SetParent (containerOfCardScroll.transform);
			newCard.transform.localScale = Vector3.one;
			newCard.GetComponent<Image> ().sprite = CardsManager._instance.mycards [i].cardSpriteFromResources;
			newCard.name = CardsManager._instance.mycards [i].card_id_in_playerList.ToString ();
			newCard.GetComponent<Button> ().onClick.AddListener (() => {
				cardSelectionButton (newCard.GetComponent<Button> ());
			});
			myCardsInScroll.Add (newCard.GetComponent<Image> ());

			int cardNo = CardsManager._instance.mycards [i].card_id_in_playerList;
			if (CardsManager._instance.IsPlayercardLocked (cardNo, false)) {
				newCard.GetComponent<Button> ().interactable = false;
				newCard.GetComponentInChildren<Text> ().text = "LOCKED";
			} else {
				newCard.GetComponent<Button> ().interactable = true;
				newCard.GetComponentInChildren<Text> ().text = "";
			}

		}

		ShowDeckStats (leftCount);
//		}
	}


	void resetPrevious()
	{
		iseditDormation=true;
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
	public void onSort()
	{
		sort.SetActive(true);
	}
	public void exitSort()
	{
		sort.SetActive(false);
	}
	public void logOut()
	{
		Start();
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

	public int rowNo;
	public int deckNo;

	public void clickOnDeck(Button nameOfButtons)
	{
		int.TryParse (nameOfButtons.name.Substring (4, 1) , out deckNo);
		int.TryParse (nameOfButtons.name.Substring(nameOfButtons.name.Length-1) , out rowNo);
		if (deckNo != PlayerParameters._instance.myPlayerParameter.questFormationDeck) {
			selectedCardAttack.text = "";
			selectedCardDefense.text = "";
			selectedCardLeaderShip.text = "";
			cardSelection1.SetActive (true);
//			OnEnable ();
			currentSelectedCards.Clear ();
			for (int i = 0; i < myCardsInScroll.Count; i++) {

				int cardNo = int.Parse(myCardsInScroll [i].name);
				bool isPresentInDeck = false;
				for (int j = 0; j < cardDecks [deckNo - 1].cardRows.Count; j++) {
					for (int k = 0; k < cardDecks [deckNo - 1].cardRows [j].cardIdsForRow.Count; k++) {
						if (cardNo == cardDecks [deckNo - 1].cardRows [j].cardIdsForRow [k]) {
							myCardsInScroll[i].GetComponent<Image> ().color = new Color32 (200, 200, 200, 128);
							isPresentInDeck = true;
							break;
						}
					}
				}
				if (!isPresentInDeck) {
					myCardsInScroll[i].GetComponent<Image> ().color = new Color (1, 1, 1, 1);
				}

				for (int k = 0; k < cardDecks [deckNo - 1].cardRows [rowNo - 1].cardIdsForRow.Count; k++) {
					if (cardNo == cardDecks [deckNo - 1].cardRows [rowNo - 1].cardIdsForRow [k]) {
						currentSelectedCards.Add (cardNo);
					}
				}
			}
			Debug.Log ("count of selection--- " + currentSelectedCards.Count);
			editFormation.SetActive (false);
			iseditDormation = false;
			deckSelectCount = ((deckNo - 1) * 3 + rowNo).ToString ();
		} else {
			loadingScene.Instance.popupFromServer.ShowPopup ("You cannot Edit this Deck. It has aready been locked in quest!");
		}
		
	}
	
	public void cardSelectionButton(Button buttonName)
	{
		if(buttonName.GetComponent<Image>().color==new Color32(200,200,200,128))
		{
			if(cardDecks[deckNo-1].noOfCardsSelected >0 && cardDecks[deckNo-1].cardRows[rowNo-1].cardIdsForRow.Count > 0)
			{
				int cardNoClicked = int.Parse (buttonName.name);
				bool cardWasSelected = false;
				for(int i = 0; i < cardDecks[deckNo-1].cardRows[rowNo-1].cardIdsForRow.Count ; i++)
				{
					if(cardNoClicked == cardDecks[deckNo-1].cardRows[rowNo-1].cardIdsForRow[i])
					{
						cardWasSelected = true;
						buttonName.GetComponent<Image>().color=new Color32(255,255,255,255);
						cardDecks[deckNo-1].cardRows[rowNo-1].cardIdsForRow.Remove (cardNoClicked);
						int positionOfCard = CardsManager._instance.PositionOfCardInList (cardNoClicked);
						cardDecks[deckNo-1].deckCost -= CardsManager._instance.mycards[positionOfCard].cardCost;
						cardDecks[deckNo-1].noOfCardsSelected--;
						selectedCardAttack.text = "";
						selectedCardDefense.text = "";
						selectedCardLeaderShip.text = "";
						break;
					}
				}
				if(!cardWasSelected)
				{
					loadingScene.Instance.popupFromServer.ShowPopup ("The card has been selected for another row!");
				}
			}
		}
		else
		{
			int positionOfCard = CardsManager._instance.PositionOfCardInList (int.Parse (buttonName.name));
			int finalMaxCost = cardDecks[deckNo-1].deckCost + CardsManager._instance.mycards[positionOfCard].cardCost;
			if(cardDecks[deckNo-1].noOfCardsSelected < 9 && cardDecks[deckNo-1].cardRows[rowNo-1].cardIdsForRow.Count < 5 && finalMaxCost <= PlayerParameters._instance.myPlayerParameter.maxBattleCost)
			{
				buttonName.GetComponent<Image>().color=new Color32(200,200,200,128);		
				cardDecks[deckNo-1].cardRows[rowNo-1].cardIdsForRow.Add (int.Parse (buttonName.name));
				cardDecks[deckNo-1].noOfCardsSelected++;
				selectedCardAttack.text = CardsManager._instance.mycards[positionOfCard].attack.ToString ();
				selectedCardDefense.text = CardsManager._instance.mycards[positionOfCard].defense.ToString ();
				selectedCardLeaderShip.text = CardsManager._instance.mycards[positionOfCard].leadership.ToString ();
				cardDecks[deckNo-1].deckCost += CardsManager._instance.mycards[positionOfCard].cardCost;
			}
			else if(finalMaxCost > PlayerParameters._instance.myPlayerParameter.maxBattleCost)
			{
				loadingScene.Instance.popupFromServer.ShowPopup ("Max Battle Cost Reached!");
			}
			
		}

	}


	public void backFromCards()
	{
		editFormation.SetActive(true);
		cardSelection1.SetActive(false);
		iseditDormation=true;

		int countOfCardsInTheRow = cardDecks [deckNo - 1].cardRows [rowNo-1].cardIdsForRow.Count;
		cardDecks [deckNo - 1].noOfCardsSelected -= countOfCardsInTheRow;
		cardDecks [deckNo - 1].cardRows [rowNo-1].cardIdsForRow.Clear ();

		Debug.Log ("currentSelectedCards.Count   = "+currentSelectedCards.Count);
		for(int k = 0 ; k < currentSelectedCards.Count ; k++)
		{
			cardDecks[deckNo-1].cardRows[rowNo-1].cardIdsForRow.Add (currentSelectedCards[k]);
			cardDecks [deckNo - 1].noOfCardsSelected++;
		}
		cardDecks [deckNo - 1].deckCost = 0;
		for(int k = 0 ; k < cardDecks[deckNo-1].cardRows.Count ; k++)
		{
			for (int t = 0; t < cardDecks [deckNo - 1].cardRows [k].cardIdsForRow.Count; t++) {
				int positionOfCard = CardsManager._instance.PositionOfCardInList (cardDecks [deckNo - 1].cardRows [k].cardIdsForRow[t]);
				cardDecks [deckNo - 1].deckCost += CardsManager._instance.mycards [positionOfCard].cardCost;
			}
		}

	}

	public void TotalSoldiersInDeck() 
	{
		for (int i = 0; i < cardDecks.Count; i++) {
			cardDecks [i].deckSoldiers = 0;
			cardDecks [i].deckLeadership = 0;
				
			for (int j = 0; j < cardDecks [i].cardRows.Count; j++) {
				for (int k = 0; k < cardDecks [i].cardRows [j].cardIdsForRow.Count; k++) {
					int cardId = CardsManager._instance.PositionOfCardInList (cardDecks [i].cardRows [j].cardIdsForRow [k]);
					cardDecks [i].deckSoldiers += CardsManager._instance.mycards [cardId].card_soldiers;
					cardDecks [i].deckLeadership += CardsManager._instance.mycards [cardId].leadership;
				}
			}
		}
	}

	public int FindCardDeck()
	{
		int cardDeckToSelect = 0;
		if ((cardDecks [0].deckSoldiers >= cardDecks [1].deckSoldiers) && (cardDecks [0].deckSoldiers >= cardDecks [2].deckSoldiers)) {

		} else if ((cardDecks [1].deckSoldiers >= cardDecks [0].deckSoldiers) && (cardDecks [1].deckSoldiers >= cardDecks [2].deckSoldiers)) {
			cardDeckToSelect = 1;
		} else {
			cardDeckToSelect = 2;
		}
		return cardDeckToSelect;
	}

	public void confirmCardRow()
	{
		
		editFormation.SetActive(true);
		cardSelection1.SetActive(false);
		iseditDormation=true;

//		for (int i = 0; i < cardDecks.Count; i++) {
//			for (int j = 0; j < cardDecks[i].cardRows.Count; j++) {
		int i = deckNo - 1;
		int j = rowNo - 1;

		int attackOfRow = 0;
		int defenseOfRow = 0;
		int leadershipOfRow = 0;
		int soldiersOfRow = 0;

		for (int k = 0; k < cardDecks[i].cardRows[j].cardIdsForRow.Count; k++) {
			cardDecks[i].cardRows[j].cardObjectsForRow[k].transform.FindChild ("cardImage").GetComponent<Image>().color = new Color(1,1,1,1);
			int spriteToFetch = CardsManager._instance.PositionOfCardInList (cardDecks[i].cardRows[j].cardIdsForRow[k]);
			cardDecks[i].cardRows[j].cardObjectsForRow[k].transform.FindChild ("cardImage").GetComponent<Image>().sprite = CardsManager._instance.mycards[spriteToFetch].cardSpriteFromResources;
			attackOfRow+=CardsManager._instance.mycards[spriteToFetch].attack;
			defenseOfRow+=CardsManager._instance.mycards[spriteToFetch].defense;
			leadershipOfRow+=CardsManager._instance.mycards[spriteToFetch].leadership;
			soldiersOfRow+=CardsManager._instance.mycards[spriteToFetch].card_soldiers;
		}
		cardDecks[i].cardRows[j].rowAttack.text = attackOfRow.ToString ();
		cardDecks[i].cardRows[j].rowDefense.text = defenseOfRow.ToString ();
		cardDecks[i].cardRows[j].rowLeadership.text = leadershipOfRow.ToString ();

		if (leadershipOfRow > 0) {
			float soldierPercentage = soldiersOfRow / (float)leadershipOfRow;
			if (soldierPercentage > 1)
				soldierPercentage = 1;
			cardDecks[i].cardRows[j].soldierPercentageInRow.text = Mathf.FloorToInt (soldierPercentage * 100).ToString () + "%";
			cardDecks[i].cardRows[j].soldierSlider.value = soldierPercentage;
		} else {
			cardDecks[i].cardRows[j].soldierPercentageInRow.text = "0%";
			cardDecks[i].cardRows[j].soldierSlider.value = 0;
		}




		for (int k = cardDecks[i].cardRows[j].cardIdsForRow.Count; k < cardDecks[i].cardRows[j].cardObjectsForRow.Count; k++) {
			cardDecks[i].cardRows[j].cardObjectsForRow[k].transform.FindChild ("cardImage").GetComponent<Image>().color = new Color(1,1,1,0);
		}

//			}
//		}

		ShowDeckStats (i);

	}

	public void DeckStats(int deckinArrayNo , ref int attackOfDeck , ref int defenseOfDeck , ref int leadershipOfDeck)
	{
		for (int l = 0; l < cardDecks[deckinArrayNo].cardRows.Count; l++) {
			for(int m = 0 ; m < cardDecks[deckinArrayNo].cardRows[l].cardIdsForRow.Count ; m++)
			{
				int cardNoInMyCards = CardsManager._instance.PositionOfCardInList (cardDecks[deckinArrayNo].cardRows[l].cardIdsForRow[m]);
				attackOfDeck+=CardsManager._instance.mycards[cardNoInMyCards].attack;
				defenseOfDeck+=CardsManager._instance.mycards[cardNoInMyCards].defense;
				leadershipOfDeck+=CardsManager._instance.mycards[cardNoInMyCards].leadership;
			}
		}
	}

	public void AllDeckStats()
	{
		for (int k = 0; k < cardDecks.Count; k++) {
			for (int l = 0; l < cardDecks[k].cardRows.Count; l++) {
				for(int m = 0 ; m < cardDecks[k].cardRows[l].cardIdsForRow.Count ; m++)
				{
					int cardNoInMyCards = CardsManager._instance.PositionOfCardInList (cardDecks[k].cardRows[l].cardIdsForRow[m]);
					cardDecks[k].deckAttack+=CardsManager._instance.mycards[cardNoInMyCards].attack;
					cardDecks[k].deckDefense+=CardsManager._instance.mycards[cardNoInMyCards].defense;
					cardDecks[k].deckLeadership+=CardsManager._instance.mycards[cardNoInMyCards].leadership;
				}
			}
		}
	}

	void ShowDeckStats(int deckinArrayNo)
	{
		int attackOfDeck = 0;
		int defenseOfDeck = 0;
		int leadershipOfDeck = 0;
		for (int l = 0; l < cardDecks[deckinArrayNo].cardRows.Count; l++) {
			int attackOfDeckPerRow = 0;
			int defenseOfDeckPerRow = 0;
			int leadershipOfDeckPerRow = 0;
			int.TryParse (cardDecks[deckinArrayNo].cardRows[l].rowAttack.text , out attackOfDeckPerRow);
			int.TryParse (cardDecks[deckinArrayNo].cardRows[l].rowDefense.text, out defenseOfDeckPerRow);
			int.TryParse (cardDecks[deckinArrayNo].cardRows[l].rowLeadership.text, out leadershipOfDeckPerRow);
//			Debug.Log("attackOfDeckPerRow = "+attackOfDeckPerRow);
//			Debug.Log("defenseOfDeckPerRow = "+defenseOfDeckPerRow);
//			Debug.Log("leadershipOfDeckPerRow = "+leadershipOfDeckPerRow);
			attackOfDeck+=attackOfDeckPerRow;
			defenseOfDeck+=defenseOfDeckPerRow;
			leadershipOfDeck+=leadershipOfDeckPerRow;
		}
		deckAttack.text = attackOfDeck.ToString ();
		deckDefense.text = defenseOfDeck.ToString ();
		deckLeaderShip.text = leadershipOfDeck.ToString ();
		costText.text = cardDecks[deckinArrayNo].deckCost+"/"+PlayerParameters._instance.myPlayerParameter.maxBattleCost;
	}

	public void RemoveAllCardsFromDecks()
	{
		Debug.Log("removing cards...");
		for (int i = 0; i < cardDecks.Count; i++) {
			for (int j = 0; j < cardDecks[i].cardRows.Count; j++) {
				for (int k = 0; k < cardDecks[i].cardRows[j].cardObjectsForRow.Count; k++) {
					cardDecks[i].cardRows[j].cardObjectsForRow[k].transform.FindChild ("cardImage").GetComponent<Image>().color = new Color(1,1,1,0);
					cardDecks[i].cardRows[j].cardObjectsForRow[k].transform.FindChild ("cardImage").GetComponent<Image>().sprite = null;

				}
				cardDecks[i].cardRows[j].rowAttack.text = "";
				cardDecks[i].cardRows[j].rowDefense.text = "";
				cardDecks[i].cardRows[j].rowLeadership.text = "";
				cardDecks[i].cardRows[j].cardIdsForRow.Clear ();

			}
			cardDecks [i].deckCost = 0;
			cardDecks [i].deckAttack = 0;
			cardDecks [i].deckDefense = 0;
			cardDecks [i].deckLeadership = 0;
			cardDecks [i].noOfCardsSelected = 0;
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
	
	public void leftButton()
	{	
		if(leftCount==0)
		{
			leftCount+=1;
			iTween.MoveTo(deck1,iTween.Hash("x",leftPosition.transform.position.x,"time",0.5f,"onComplete","deck2Object","onCompleteTarget",this.gameObject));
			
		}
		else if(leftCount==1)
		{
			leftCount+=1;
			iTween.MoveTo(deck2,iTween.Hash("x",leftPosition.transform.position.x,"time",0.5f,"onComplete","deck3Object","onCompleteTarget",this.gameObject));
		}
		ChangeRightLeftButtons ();
		
	}

	void ChangeRightLeftButtons()
	{
		if(leftCount>0)
		{
			GameObject.Find("rightButton").GetComponent<Button>().interactable=true;
		}
		if(leftCount>1)
		{
			GameObject.Find("leftButton").GetComponent<Button>().interactable=false;

		}
		if(leftCount==0)
		{
			GameObject.Find("leftButton").GetComponent<Button>().interactable=true;
			GameObject.Find("rightButton").GetComponent<Button>().interactable=false;


		}
		if(leftCount==1)
		{
			GameObject.Find("leftButton").GetComponent<Button>().interactable=true;
			GameObject.Find("rightButton").GetComponent<Button>().interactable=true;


		}
	}

	public void confirmButton()
	{
		if (cardDecks [0].noOfCardsSelected == 0 && cardDecks [1].noOfCardsSelected == 0 && cardDecks [2].noOfCardsSelected == 0) {
			loadingScene.Instance.popupFromServer.ShowPopup("You need to set up atleast 1 card in any deck to proceed!");
		} else {

			loadingScene.Instance.loader.SetActive (true);
			NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
				if (isConnected)
				{
					int []a = new int[]{1,2,3};
					JSONObject j = new JSONObject(JSONObject.Type.OBJECT);
					JSONObject arr1 = new JSONObject(JSONObject.Type.ARRAY);
					List<JSONObject> arrRows = new List<JSONObject>();

					for(int k = 0 ; k < 3 ; k++)
					{
						string dataForRow = "";
						for(int i = 0 ; i < cardDecks [0].cardRows[k].cardIdsForRow.Count ; i++)
						{
							dataForRow+= (cardDecks [0].cardRows[k].cardIdsForRow[i]);
							if(i < cardDecks [0].cardRows[k].cardIdsForRow.Count-1)
								dataForRow+=",";
						}
						JSONObject abc = new JSONObject(JSONObject.Type.OBJECT);
						abc.AddField ("row"+k , dataForRow);
						arrRows.Add (abc);
					}
					arr1.Add (arrRows[0]);
					arr1.Add (arrRows[1]);
					arr1.Add (arrRows[2]);
					j.AddField("deck1", arr1);

					JSONObject arr2 = new JSONObject(JSONObject.Type.ARRAY);
					List<JSONObject> arrRows2 = new List<JSONObject>();
					
					for(int k = 0 ; k < 3 ; k++)
					{
						string dataForRow = "";
						for(int i = 0 ; i < cardDecks [1].cardRows[k].cardIdsForRow.Count ; i++)
						{
							dataForRow+= (cardDecks [1].cardRows[k].cardIdsForRow[i]);
							if(i < cardDecks [1].cardRows[k].cardIdsForRow.Count-1)
								dataForRow+=",";
						}
						JSONObject abc = new JSONObject(JSONObject.Type.OBJECT);
						abc.AddField ("row"+k , dataForRow);
						arrRows2.Add (abc);
					}
					arr2.Add (arrRows2[0]);
					arr2.Add (arrRows2[1]);
					arr2.Add (arrRows2[2]);
					j.AddField("deck2", arr2);

					JSONObject arr3 = new JSONObject(JSONObject.Type.ARRAY);
					List<JSONObject> arrRows3 = new List<JSONObject>();
					
					for(int k = 0 ; k < 3 ; k++)
					{
						string dataForRow = "";
						for(int i = 0 ; i < cardDecks [2].cardRows[k].cardIdsForRow.Count ; i++)
						{
							dataForRow+= (cardDecks [2].cardRows[k].cardIdsForRow[i]);
							if(i < cardDecks [2].cardRows[k].cardIdsForRow.Count-1)
								dataForRow+=",";
						}
						JSONObject abc = new JSONObject(JSONObject.Type.OBJECT);
						abc.AddField ("row"+k , dataForRow);
						arrRows3.Add (abc);
					}
					arr3.Add (arrRows3[0]);
					arr3.Add (arrRows3[1]);
					arr3.Add (arrRows3[2]);
					j.AddField("deck3", arr3);

					
					string encodedString = j.ToString();
					Debug.Log("encodedString = "+encodedString);
					Dictionary<string, string> avatarParameters = new Dictionary<string, string>();
					if(BattleLogic._instance.battleType == BattleLogic.BattleType.CHEST_EVENT  || BattleLogic._instance.battleType == BattleLogic.BattleType.CHEST_BOSS || isQuest)
					{
						avatarParameters.Add ("captivesList" , (leftCount+1).ToString());
					}
					else{
						avatarParameters.Add ("interrogationList" , (leftCount+1).ToString());
					}

					avatarParameters.Add ("BattleCardFormation" , encodedString);

					StartCoroutine (PlayerParameters._instance.SendPlayerParameters( avatarParameters , (isSuccess) => {
						if(isSuccess)
						{

							if(BattleLogic._instance.battleType == BattleLogic.BattleType.CHEST_EVENT  || BattleLogic._instance.battleType == BattleLogic.BattleType.CHEST_BOSS || isQuest)
							{
								PlayerParameters._instance.myPlayerParameter.questFormationDeck =  leftCount+1;
							}
							else{
								PlayerParameters._instance.myPlayerParameter.battleFormationDeck = leftCount+1;
							}


							loadingScene.Instance.loader.SetActive (false);
							Start ();
							if(BattleLogic._instance.battleType == BattleLogic.BattleType.CHEST_EVENT || BattleLogic._instance.battleType == BattleLogic.BattleType.CHEST_BOSS )
							{
								EventQuest.instance.confirmClick ();
								loadingScene.Instance.EventQuest ();
							}
							else
							{
								if(isQuest)
								{
									questNew.instance.confirmClick ();
									loadingScene.Instance.quest ();
								}
								else
								{
									loadingScene.Instance.battleItemSelectionScene ();
								}
							}
						}
						else
						{
							loadingScene.Instance.popupFromServer.ShowPopup ("Network Error!");
						}
					}));
				}
				else
				{
					loadingScene.Instance.popupFromServer.ShowPopup ("Network Error!");
				}
			});


		}
	}



	public void rightButton()
	{
		if(leftCount==1)
		{
			leftCount-=1;
			iTween.MoveTo(deck2,iTween.Hash("x",rightPosition.transform.position.x,"time",0.5f,"onComplete","deck1Object","onCompleteTarget",this.gameObject));
		}
		else if(leftCount==2)
		{
			leftCount-=1;
			iTween.MoveTo(deck3,iTween.Hash("x",rightPosition.transform.position.x,"time",0.5f,"onComplete","deck2Object","onCompleteTarget",this.gameObject));
		}
		ChangeRightLeftButtons ();
	}
	void deck2Object()
	{
		ShowDeckStats (1);
		iTween.MoveTo(deck2,iTween.Hash("x",midPos.transform.position.x,"time",0.5));
	}
	void deck3Object()
	{
		ShowDeckStats (2);
		iTween.MoveTo(deck3,iTween.Hash("x",midPos.transform.position.x,"time",0.5));
	}
	void deck1Object()
	{
		ShowDeckStats (0);
		iTween.MoveTo(deck1,iTween.Hash("x",midPos.transform.position.x,"time",0.5));
	}
	
	

	public void shopScene()
	{
		Start();

		PlayerPrefs.SetString("cardCollection","yes");
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
		loadingScene.Instance.shop();
	}
	

	public void inventory()
	{
		Start();

		PlayerPrefs.SetString("cardCollection","yes");
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
		loadingScene.Instance.inventory();
	}
	public void backButton()
	{
		Start();
		Debug.Log (isQuest);
		RemoveAllCardsFromDecks ();

		for (int k = 0; k < cardDecks.Count; k++) {
			cardDecks [k].noOfCardsSelected = 0;
			Debug.Log (" deck  = "+k+","+cardDecks [k].noOfCardsSelected);
			for (int l = 0; l < cardDecks [k].cardRows.Count; l++) {
				cardDecks [k].cardRows [l].cardIdsForRow.Clear ();
			}
			cardDecks [k].deckCost = 0;
		}

		for (int k = 0; k < savedCardDeck.Count; k++) {
			cardDecks [k].noOfCardsSelected = savedCardDeck [k].noOfCardsSelected;
			for (int l = 0; l < savedCardDeck [k].cardRows.Count; l++) {
				for (int t = 0; t < savedCardDeck [k].cardRows [l].cardIdsForRow.Count; t++) {
					cardDecks [k].cardRows [l].cardIdsForRow.Add (savedCardDeck [k].cardRows [l].cardIdsForRow[t]);
				}
			}
		}

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
	public void chatClick()
	{
		Start();

		PlayerPrefs.SetString("cardCollections","yes");
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
		loadingScene.Instance.chat();
	}
	public void community()
	{
		Start();

		PlayerPrefs.SetString("cardCollections","yes");
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
		loadingScene.Instance.community();
		
	}
	public void trade()
	{
		Start();

		PlayerPrefs.SetString("cardCollections","yes");
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
		loadingScene.Instance.trade();
	}
	public void empire()
	{
		Start();

		PlayerPrefs.SetString("cardCollections","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			menu.SetActive(false);
			isMneuActive=false;
			
		}
		loadingScene.Instance.empire();
	}

	public void exitMenu()
	{
		menu.SetActive (false);
		
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
	public void battle()
	{
		Start();

		PlayerPrefs.SetString("cardCollections","yes");
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
		loadingScene.Instance.battleScene();
		
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

	public void RootMenuButton()
	{
		Start();
		loadingScene.Instance.objectToOpenMenu = this.gameObject;
		loadingScene.Instance.main();
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
	}
	
	
	
}
