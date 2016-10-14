using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using UnityEngine.UI;
using System.Linq;


[System.Serializable]
public class CardDeck
{
	public List <CardRow> cardRows;
	public int noOfCardsSelected;
	public int deckAttack;
	public int deckDefense;
	public int deckLeadership;
	public int deckSoldiers;
	public int deckCost; 

	[System.Serializable]
	public struct CardRow
	{
		public List <int> cardIdsForRow;
		public List <GameObject> cardObjectsForRow;
		public Text rowAttack;
		public Text rowDefense;
		public Text rowLeadership;
		public Text soldierPercentageInRow;
		public Slider soldierSlider;
	}
}

[System.Serializable]
public class DeckCalculations
{
	public List <CardRowCalculations> cardRowCalculations;
	public int noOfCardsSelected;
	public int gateLevel = 1;
	public int avatarAttack;
	public int avatarDefense;
	public int avatarStamina;
	public int avatarNo;
}

[System.Serializable]
public class CardRowCalculations
{
	public List <int> cardsAttack;
	public List <int> cardsDefense;
	public List <int> cardsLeadership;
	public List <int> cardsSoldiers;
	//	public List <string> cardName;
	public List <int> cardIdsInPlayerList;
	public List <string> cardSkill1;
	public List <string> cardSkill2;
	public int rowAP;
	public int rowDP;
	public int rowSoldiers;

}

public class CardsManager : MonoBehaviour
{
	[System.Serializable]
	public struct CardParameters
	{
		public string card_name;
		public int card_id_in_database;
		public int card_id_in_playerList;
		public string rarity;
		public string type;
		public string cardClass;
		public int attack;
		public int defense;
		public int leadership;
		public int card_soldiers;
		public int experience;
		public string skill_1;
		public string skill_1_Strength;
		public string skill_2;
		public string skill_2_Strength;
		public int is_captive;
		public int fear_factor;
		public int card_level;
		public Sprite cardSpriteFromResources;
		public int max_level;
		public bool isLocked;
		public int skill1_level;
		public int skill2_level;
		public int skill1_exp;
		public int skill2_exp;
		public int cardCost;
	}
	public List<CardParameters> mycards;
	public List<CardParameters> myCaptives;
	public List<CardParameters> myStashCards;
	public List<int> cardIdsSacrificed;
	public List<GameObject> cardButtonOfEmpire;
	public List<GameObject> cardButtonOfEmpire1;
	public List<GameObject> cardButtonOfEmpire2;
	public List<GameObject> cardButtonOfEmpire3;

	public static CardsManager _instance;

	public List<int> type_1_Exp;
	public List<int> type_2_Exp;
	public List<int> type_3_Exp;
	public List<int> type_4_Exp;
	public List<int> type_5_Exp;

	public List<int> starCard1;
	public List<int> starCard2;
	public List<int> starCard3;
	public List<int> starCard4;
	public List<int> starCard5;


	public List<int> starCard1Actual;
	public List<int> starCard2Actual;
	public List<int> starCard3Actual;
	public List<int> starCard4Actual;
	public List<int> starCard5Actual;

	public List<int> cardRarity3;
	public List<int> cardRarity4;
	public List<int> cardRarity5;

	public List<int> cardRarityActual3;
	public List<int> cardRarityActual4;
	public List<int> cardRarityActual5;

	public GameObject containerOfCardsList;
	public GameObject containerOfCardsList2;
	public GameObject containerOfCardsList3;
	public GameObject containerOfCardsList4;

	int noOfPlayerCards;

	void Awake() {
		_instance = this;
	}

	void Start () {

		for (int i = 0; i < starCard1.Count; i++) {
			for(int j = 0; j <= i ; j++)
			{
				starCard1Actual[i]+=starCard1[j];
			}
		}

		for (int i = 0; i < starCard2.Count; i++) {
			for(int j = 0; j <= i ; j++)
			{
				starCard2Actual[i]+=starCard2[j];
			}
		}
		for (int i = 0; i < starCard3.Count; i++) {
			for(int j = 0; j <= i ; j++)
			{
				starCard3Actual[i]+=starCard3[j];
			}
		}
		for (int i = 0; i < starCard4.Count; i++) {
			for(int j = 0; j <= i ; j++)
			{
				starCard4Actual[i]+=starCard4[j];
			}
		}
		for (int i = 0; i < starCard5.Count; i++) {
			for(int j = 0; j <= i ; j++)
			{
				starCard5Actual[i]+=starCard5[j];
			}
		}


		for (int i = 0; i < cardRarity3.Count; i++) {
			for(int j = 0; j <= i ; j++)
			{
				cardRarityActual3[i]+=cardRarity3[j];
			}
		}
		for (int i = 0; i < cardRarity4.Count; i++) {
			for(int j = 0; j <= i ; j++)
			{
				cardRarityActual4[i]+=cardRarity4[j];
			}
		}

		for (int i = 0; i < cardRarity5.Count; i++) {
			for(int j = 0; j <= i ; j++)
			{
				cardRarityActual5[i]+=cardRarity5[j];
			}
		}

//		StartCoroutine (GetPlayerCards(isWorking => {}));
	}

	public int PositionOfCardInList(int cardToCheck)
	{
		int spriteToFetch = 0;
		for(int i = 0 ; i < mycards.Count ; i++)
		{
			if(cardToCheck == mycards[i].card_id_in_playerList)
			{
				spriteToFetch = i;
				break;
			}
		}
		return spriteToFetch;
	}

	public int PositionOfCardInStash(int cardToCheck)
	{
		int spriteToFetch = 0;
		for(int i = 0 ; i < myStashCards.Count ; i++)
		{
			if(cardToCheck == myStashCards[i].card_id_in_playerList)
			{
				spriteToFetch = i;
				break;
			}
		}
		return spriteToFetch;
	}

	public int PositionOfEnemyCardInList(int cardToCheck)
	{
		int spriteToFetch = 0;
		for(int i = 0 ; i < OpponentData._instance.enemyCards.Count ; i++)
		{
			if(cardToCheck == OpponentData._instance.enemyCards[i].card_id_in_playerList)
			{
				spriteToFetch = i;
				break;
			}
		}
		return spriteToFetch;
	}


	public int PositionOfCaptiveInList(int cardToCheck)
	{
		int spriteToFetch = 0;
		for(int i = 0 ; i < myCaptives.Count ; i++)
		{
			if(cardToCheck == myCaptives[i].card_id_in_playerList)
			{
				spriteToFetch = i;
				break;
			}
		}
		return spriteToFetch;
	}

	//manish
	/*IEnumerator GetIsLockedStatus(int cardIndex) {
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "getIsLockedPlayerCards");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField("card_no_in_players_list", mycards[cardIndex].card_id_in_playerList);
		WWW isLockedStatus = new WWW (loadingScene.Instance.baseUrl, wwwForm);
		yield return isLockedStatus;
		Debug.Log (isLockedStatus.text);
		if (isLockedStatus.text.Contains ("success\":1")) {
			IDictionary isLockedData = Json.Deserialize(isLockedStatus.text) as IDictionary;
			string isLocked = isLockedData["Islocked"].ToString();
			if(isLocked == "true") {
				CardParameters temp = mycards[cardIndex];
				temp.isLocked = true;
				mycards[cardIndex] = temp;
			}
			else {
				CardParameters temp = mycards[cardIndex];
				temp.isLocked = false;
				mycards[cardIndex] = temp;
			}
			cardIndex++;
			if(cardIndex < mycards.Count) {
				StartCoroutine(GetIsLockedStatus(cardIndex));
			}
			else {
				Trading.BazaarContent.isLocked = false;
			}
		} else {
			StartCoroutine(GetIsLockedStatus(cardIndex));
		}
	}*/


	public bool IsPlayercardLocked(int cardNo)
	{
		
		if(EmpireManager._instance.castle.primaryCardNo != cardNo && EmpireManager._instance.storage.primaryCardNo != cardNo && EmpireManager._instance.storage.secondaryCardNo != cardNo &&
			EmpireManager._instance.barn.primaryCardNo != cardNo && EmpireManager._instance.barn.secondaryCardNo != cardNo &&
			EmpireManager._instance.goldMine.primaryCardNo != cardNo && EmpireManager._instance.goldMine.secondaryCardNo != cardNo &&
			EmpireManager._instance.barracks.primaryCardNo != cardNo && EmpireManager._instance.barracks.secondaryCardNo != cardNo &&
			EmpireManager._instance.trainingGround.primaryCardNo != cardNo && EmpireManager._instance.trainingGround.secondaryCardNo != cardNo
			&& EmpireManager._instance.prison.secondaryCardNo != cardNo && EmpireManager._instance.prison.primaryCardNo != cardNo
			&& EmpireManager._instance.gate.secondaryCardNo != cardNo && EmpireManager._instance.gate.primaryCardNo != cardNo
			&& CardsManager._instance.mycards[CardsManager._instance.PositionOfCardInList(cardNo)].isLocked == false)

		{
			bool isCardLocked = false;
			for (int c = 0; c < loadingScene.Instance.myQuestFormation.cardDecks.Count; c++) {
				for(int  d= 0 ; d < loadingScene.Instance.myQuestFormation.cardDecks[c].cardRows.Count ; d++)
				{
					for(int e = 0 ; e < loadingScene.Instance.myQuestFormation.cardDecks[c].cardRows[d].cardIdsForRow.Count ; e++)
					{
						if(cardNo == loadingScene.Instance.myQuestFormation.cardDecks[c].cardRows[d].cardIdsForRow[e])
						{
							isCardLocked = true;
							break;
						}
					}
				}
			}

			return isCardLocked;
		}
		else
		{
			return true;
		}
	}

	public bool IsPlayercardLocked(int cardNo , bool checkingForQuest ,bool checkingForBattle)
	{

		if(EmpireManager._instance.castle.primaryCardNo != cardNo && EmpireManager._instance.storage.primaryCardNo != cardNo && EmpireManager._instance.storage.secondaryCardNo != cardNo &&
			EmpireManager._instance.barn.primaryCardNo != cardNo && EmpireManager._instance.barn.secondaryCardNo != cardNo &&
			EmpireManager._instance.goldMine.primaryCardNo != cardNo && EmpireManager._instance.goldMine.secondaryCardNo != cardNo &&
			EmpireManager._instance.barracks.primaryCardNo != cardNo && EmpireManager._instance.barracks.secondaryCardNo != cardNo &&
			EmpireManager._instance.trainingGround.primaryCardNo != cardNo && EmpireManager._instance.trainingGround.secondaryCardNo != cardNo
			&& EmpireManager._instance.prison.secondaryCardNo != cardNo && EmpireManager._instance.prison.primaryCardNo != cardNo
			&& EmpireManager._instance.gate.secondaryCardNo != cardNo && EmpireManager._instance.gate.primaryCardNo != cardNo
			&& CardsManager._instance.mycards[CardsManager._instance.PositionOfCardInList(cardNo)].isLocked == false)

		{
			bool isCardLocked = false;
			if (!checkingForQuest) {
				for (int c = 0; c < loadingScene.Instance.myQuestFormation.cardDecks.Count; c++) {
					for (int d = 0; d < loadingScene.Instance.myQuestFormation.cardDecks [c].cardRows.Count; d++) {
						for (int e = 0; e < loadingScene.Instance.myQuestFormation.cardDecks [c].cardRows [d].cardIdsForRow.Count; e++) {
							if (cardNo == loadingScene.Instance.myQuestFormation.cardDecks [c].cardRows [d].cardIdsForRow [e]) {
								isCardLocked = true;
								break;
							}
						}
					}
				}
			}
			if (!checkingForBattle) {
				for (int c = 0; c < loadingScene.Instance.myBattleFormation.cardDecks.Count; c++) {
					for (int d = 0; d < loadingScene.Instance.myBattleFormation.cardDecks [c].cardRows.Count; d++) {
						for (int e = 0; e < loadingScene.Instance.myBattleFormation.cardDecks [c].cardRows [d].cardIdsForRow.Count; e++) {
							if (cardNo == loadingScene.Instance.myBattleFormation.cardDecks [c].cardRows [d].cardIdsForRow [e]) {
								isCardLocked = true;
								break;
							}
						}
					}
				}
			}
			return isCardLocked;
		}
		else
		{
			return true;
		}
	}

	public bool IsPlayercardLocked(int cardNo , bool haveToBattle)
	{

		if(EmpireManager._instance.castle.primaryCardNo != cardNo && EmpireManager._instance.storage.primaryCardNo != cardNo && EmpireManager._instance.storage.secondaryCardNo != cardNo &&
			EmpireManager._instance.barn.primaryCardNo != cardNo && EmpireManager._instance.barn.secondaryCardNo != cardNo &&
			EmpireManager._instance.goldMine.primaryCardNo != cardNo && EmpireManager._instance.goldMine.secondaryCardNo != cardNo &&
			EmpireManager._instance.barracks.primaryCardNo != cardNo && EmpireManager._instance.barracks.secondaryCardNo != cardNo &&
			EmpireManager._instance.trainingGround.primaryCardNo != cardNo && EmpireManager._instance.trainingGround.secondaryCardNo != cardNo
			&& EmpireManager._instance.prison.secondaryCardNo != cardNo && EmpireManager._instance.prison.primaryCardNo != cardNo
			&& EmpireManager._instance.gate.secondaryCardNo != cardNo && EmpireManager._instance.gate.primaryCardNo != cardNo
			&& CardsManager._instance.mycards[CardsManager._instance.PositionOfCardInList(cardNo)].isLocked == false)

		{
			bool isCardLocked = false;
			if (!haveToBattle && PlayerParameters._instance.myPlayerParameter.questFormationDeck > 0) {
				int cardDeckOfQuest = PlayerParameters._instance.myPlayerParameter.questFormationDeck - 1;
				//				for (int cardDeckOfQuest = 0; cardDeckOfQuest < loadingScene.Instance.myQuestFormation.cardDecks.Count; cardDeckOfQuest++) {
				for (int d = 0; d < loadingScene.Instance.myQuestFormation.cardDecks [cardDeckOfQuest].cardRows.Count; d++) {
					for (int e = 0; e < loadingScene.Instance.myQuestFormation.cardDecks [cardDeckOfQuest].cardRows [d].cardIdsForRow.Count; e++) {
						if (cardNo == loadingScene.Instance.myQuestFormation.cardDecks [cardDeckOfQuest].cardRows [d].cardIdsForRow [e]) {
								isCardLocked = true;
								break;
							}
						}
					}
//				}
			}
//			if (!checkingForBattle) {
//				for (int c = 0; c < loadingScene.Instance.myBattleFormation.cardDecks.Count; c++) {
//					for (int d = 0; d < loadingScene.Instance.myBattleFormation.cardDecks [c].cardRows.Count; d++) {
//						for (int e = 0; e < loadingScene.Instance.myBattleFormation.cardDecks [c].cardRows [d].cardIdsForRow.Count; e++) {
//							if (cardNo == loadingScene.Instance.myBattleFormation.cardDecks [c].cardRows [d].cardIdsForRow [e]) {
//								isCardLocked = true;
//								break;
//							}
//						}
//					}
//				}
//			}
			return isCardLocked;
		}
		else
		{
			return true;
		}
	}


	public IEnumerator GetPlayerCards(System.Action<bool> CallBack)
	{
		while (!TimeManager.foundTime) {
			yield return 0;
		}

		WWWForm wform = new WWWForm ();
		wform.AddField ("tag", "doGetAllPlayerCards");
		wform.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wform.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);

		WWW wwwCards = new WWW (loadingScene.Instance.baseUrl, wform);
		yield return wwwCards;
		Debug.Log (wwwCards.text);
		if (wwwCards.error == null) {
			IDictionary cardDict = Json.Deserialize(wwwCards.text) as IDictionary;
			if(wwwCards.text.Contains("error_msg"))
			{
			//{"success":0,"error_msg":"No cards available!"}
				if(wwwCards.text.Contains("No cards available!"))
				{
					CallBack(true);
				}
				else
				{
					CallBack(false);
					newMenuScene.instance.popupFromServer.ShowPopup ("Network Error!");
				}
			}
			else
			{
//{"success":1,"msg":"Player card success","Player_Card_detail":[{"user_id":"62","card_name":"abd","card_id_in_database":"167",
//"card_no_in_players_list":"0","rarity":"1","type":"Common","class":"Andreas","attack":"100","defense":"123",
//"leadership":"1234","experience":"123","skill_1":"FierceOutrage","skill_1_Strength":"Weak","skill_2":"",
//"skill_2_Strength":"","is_captive":"0","fear_factor":"0"}]}
				if (wwwCards.text.Contains ("Player_Card_detail")) {
					IList cardList = (IList)cardDict ["Player_Card_detail"];
					for (int i = 0; i < cardList.Count; i++) {
						IDictionary perCardDict = (IDictionary)cardList [i];
						if (perCardDict ["is_deleted"].ToString () != "true") {
							CardParameters a = new CardParameters ();
							if (perCardDict ["is_locked"].ToString () == "true") {
								a.isLocked = true;
							} else {
								a.isLocked = false;
							}
							a.card_id_in_playerList = int.Parse (perCardDict ["card_no_in_players_list"].ToString ());
							a.card_name = perCardDict ["card_name"].ToString ();
							a.card_id_in_database = int.Parse (perCardDict ["card_id_in_database"].ToString ());
							a.rarity = perCardDict ["rarity"].ToString ();
							a.type = perCardDict ["type"].ToString ();
							a.cardClass = perCardDict ["class"].ToString ();
							int.TryParse (perCardDict ["attack"].ToString (), out a.attack);
							if (PlayerParameters._instance.myPlayerParameter.guildLevel > 10) {
								a.attack += Mathf.CeilToInt(0.05f * a.attack);
							}
							int.TryParse (perCardDict ["defense"].ToString (), out a.defense);
							int.TryParse (perCardDict ["leadership"].ToString (), out a.leadership);
							int.TryParse (perCardDict ["experience"].ToString (), out a.experience);
//						a.attack = int.Parse (perCardDict["attack"].ToString ());
//						a.defense = int.Parse (perCardDict["defense"].ToString ());
//						a.leadership = int.Parse (perCardDict["leadership"].ToString ());
//						a.card_soldiers = a.leadership;
							int.TryParse (perCardDict ["card_soldiers"].ToString (), out a.card_soldiers);
//						a.experience = int.Parse (perCardDict["experience"].ToString ());
							a.skill_1 = perCardDict ["skill_1"].ToString ();
							a.skill_1_Strength = perCardDict ["skill_1_Strength"].ToString ();
							a.skill_2 = perCardDict ["skill_2"].ToString ();
							a.skill_2_Strength = perCardDict ["skill_2_Strength"].ToString ();
							int.TryParse (perCardDict ["is_captive"].ToString (), out a.is_captive);
							int.TryParse (perCardDict ["skill_2_Strength"].ToString (), out a.fear_factor);
							int.TryParse (perCardDict ["card_level"].ToString (), out a.card_level);
							int.TryParse (perCardDict ["max_level"].ToString (), out a.max_level);
							int.TryParse (perCardDict ["skill1_exp"].ToString (), out a.skill1_exp);
							int.TryParse (perCardDict ["skill2_exp"].ToString (), out a.skill2_exp);
							int.TryParse (perCardDict ["skill1_level"].ToString (), out a.skill1_level);
							int.TryParse (perCardDict ["skill2_level"].ToString (), out a.skill2_level);
							int.TryParse (perCardDict ["card_cost"].ToString (), out a.cardCost);
							if (a.skill1_exp == 0) {
								a.skill1_exp = GetBaseExp (a.rarity);
							}
							if (a.skill2_exp == 0) {
								a.skill2_exp = GetBaseExp (a.rarity);
							}
							if (a.skill1_level == 0) {
								a.skill1_level = 1;
							}
							if (a.skill2_level == 0) {
								a.skill2_level = 1;
							}
							if (a.max_level == 0) {
								a.max_level = 25;
							}
							if (a.card_level == 0) {
								a.card_level = 1;
							}
							a.cardSpriteFromResources = (Sprite)Resources.Load<Sprite> ("images/" + a.card_name);

							if (a.is_captive == 0) {

									noOfPlayerCards++;
								AddCardForEmpire (a);
							}


							if (a.is_captive == 0)
								loadingScene.Instance.randomCards.Add (a.card_id_in_playerList);

							if (a.is_captive == 1)
								myCaptives.Add (a);
							else if (a.is_captive == 2) {
								myStashCards.Add (a);
							}
							else{
								mycards.Add (a);
							}
						}
					}
				}
				for(int i = 0 ; i < mycards.Count ; i++)
				{
					PlayerParameters._instance.myPlayerParameter.maxDeployedSoldiers+=mycards[i].leadership;
//					Debug.Log("my maxDeployedSoldiers soldiers! = " +PlayerParameters._instance.myPlayerParameter.maxDeployedSoldiers );

				}
				string cardIds = "";
				string cardSoldiers = "";
				string initialCardSoldiers = "";
				PlayerParameters._instance.SetSoldiersCount (ref cardIds , ref initialCardSoldiers , ref cardSoldiers);

				for(int i =  cardButtonOfEmpire.Count-1 ; i >= noOfPlayerCards ; i--)
				{
					Destroy (cardButtonOfEmpire[i].gameObject);
					Destroy (cardButtonOfEmpire1[i].gameObject);
					Destroy (cardButtonOfEmpire2[i].gameObject);
					Destroy (cardButtonOfEmpire3[i].gameObject);
					cardButtonOfEmpire.RemoveAt (i);
					cardButtonOfEmpire1.RemoveAt (i);
					cardButtonOfEmpire2.RemoveAt (i);
					cardButtonOfEmpire3.RemoveAt (i);
				}
//				loadingScene.Instance.myQuestFormation.AllDeckStats ();
				loadingScene.Instance.myBattleFormation.AllDeckStats ();
				CallBack(true);

			}

//			Debug.Log (wwwCards.text);
		}
		else
		{
			newMenuScene.instance.gameStopPopup.ShowPopup ("Network Error!");
			Debug.Log ("------" + wwwCards.error);
		}

	}

	public void AddCardForEmpire(CardParameters a)
	{
		GameObject newCard1 = (GameObject)Instantiate (Resources.Load ("playerCard"));
		GameObject newCard2 = (GameObject)Instantiate (Resources.Load ("playerCard"));
		GameObject newCard3 = (GameObject)Instantiate (Resources.Load ("playerCard"));
		GameObject newCard4 = (GameObject)Instantiate (Resources.Load ("playerCard"));

		newCard1.transform.SetParent (containerOfCardsList.transform);
		newCard2.transform.SetParent (containerOfCardsList2.transform);
		newCard3.transform.SetParent (containerOfCardsList3.transform);
		newCard4.transform.SetParent (containerOfCardsList4.transform);

		newCard1.transform.localScale = Vector3.one;
		newCard2.transform.localScale = Vector3.one;
		newCard3.transform.localScale = Vector3.one;
		newCard4.transform.localScale = Vector3.one;

		newCard1.name = a.card_id_in_playerList.ToString ();
		newCard2.name = a.card_id_in_playerList.ToString ();
		newCard3.name = a.card_id_in_playerList.ToString ();
		newCard4.name = a.card_id_in_playerList.ToString ();

		newCard1.GetComponent<Image> ().sprite = a.cardSpriteFromResources;
		newCard2.GetComponent<Image> ().sprite = a.cardSpriteFromResources;
		newCard3.GetComponent<Image> ().sprite = a.cardSpriteFromResources;
		newCard4.GetComponent<Image> ().sprite = a.cardSpriteFromResources;
		cardButtonOfEmpire.Add (newCard1.gameObject);
		cardButtonOfEmpire1.Add (newCard2.gameObject);
		cardButtonOfEmpire2.Add (newCard3.gameObject);
		cardButtonOfEmpire3.Add (newCard4.gameObject);
	}


	void updateBuildingButton(Button objClick)
	{
		Debug.Log ("objClick = "+objClick.name);
		empireScene.instance.updateBuildingButton (objClick);
	}

	public string InitialCardSoldiers()
	{
		string cardSoldiersInitially = "";
		for(int i=0 ; i < mycards.Count ; i++)
		{
			cardSoldiersInitially+=mycards[i].card_soldiers+",";
		}
		return cardSoldiersInitially;
	}


	public void DistributeDeloyedSoldiersToCards(ref string cardIds, ref string cardSoldiersInitially , ref string cardSoldiersFinal)
	{
		for(int i=0 ; i < mycards.Count ; i++)
		{
			cardIds+=mycards[i].card_id_in_playerList+",";
			cardSoldiersInitially+=mycards[i].card_soldiers+",";
		}
		if (cardIds.Length > 0) {
			cardIds = cardIds.Remove (cardIds.Length - 1);
			cardSoldiersInitially = cardSoldiersInitially.Remove (cardSoldiersInitially.Length - 1);
		}
		int soldiersToDeploy = PlayerParameters._instance.myPlayerParameter.currentlyDeployedSoldiers;
		int leadershipTotal = 0;
		for (int i = 0; i < mycards.Count; i++) {
			leadershipTotal+=mycards[i].leadership;
		}
		if (soldiersToDeploy > leadershipTotal ) {
			for (int i = 0; i < mycards.Count; i++) {
				CardsManager.CardParameters a = mycards [i]; 
				a.card_soldiers = mycards [i].leadership;
				mycards [i] = a;
				soldiersToDeploy -= mycards [i].leadership;
			}
		} else {
			for (int i = 0; i < mycards.Count; i++) {
				if(soldiersToDeploy > 0)
				{
					CardsManager.CardParameters a = mycards [i]; 
					int initialSoldiersOFCard = a.card_soldiers;
					a.card_soldiers = Mathf.CeilToInt((mycards [i].leadership / (float)leadershipTotal) * PlayerParameters._instance.myPlayerParameter.currentlyDeployedSoldiers);
//					Debug.Log (i+" caRD  initialSoldiersOFCard = "+initialSoldiersOFCard );
//					Debug.Log (i+" caRD  FINALSOLDIERS = "+a.card_soldiers );
					soldiersToDeploy -= (a.card_soldiers - initialSoldiersOFCard);
					mycards [i] = a;
				}
				else
					break;
			}

			if(soldiersToDeploy > 0)
			{
				for (int i = 0; i < mycards.Count; i++) {
					if(soldiersToDeploy > 0)
					{
						if(mycards [i].card_soldiers < mycards [i].leadership )
						{
							CardsManager.CardParameters a = mycards [i]; 
							int valToAdd = a.leadership - a.card_soldiers;
							if (valToAdd > soldiersToDeploy)
								valToAdd = soldiersToDeploy ;
							
							a.card_soldiers+=valToAdd;
							soldiersToDeploy -= valToAdd;
							mycards [i] = a;
						}
					}
					else
						break;
				}
			}
		}
		for(int i=0 ; i < mycards.Count ; i++)
		{
			cardSoldiersFinal+=mycards[i].card_soldiers+",";
		}
		if (cardSoldiersFinal.Length > 0) 
			cardSoldiersFinal = cardSoldiersFinal.Remove (cardSoldiersFinal.Length-1);
		//TODO:CALL NEW API FOR SENDING CARD SOLDIERS
	}

	public IEnumerator SendCardSoldiers(string cardIds , string cardSoldiers , Dictionary<string, string> avatarParameters, System.Action<bool> callBack)
	{
		WWWForm wform = new WWWForm ();
		wform.AddField ("tag", "playerCardPlayerData");
		wform.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wform.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wform.AddField ("card_no_in_players_list", cardIds);
		wform.AddField ("card_soldiers", cardSoldiers);
		if (avatarParameters != null) {
			wform.AddField ("array_players", "players");
			for (int i = 0; i < avatarParameters.Count; i++) {
				Debug.Log ("key sent "+avatarParameters.Keys.ElementAt(i));
				Debug.Log ("value sent "+avatarParameters.Values.ElementAt(i));
				wform.AddField(avatarParameters.Keys.ElementAt(i) , avatarParameters.Values.ElementAt(i));
			}
		}
		WWW wwwCardSoldiers = new WWW (loadingScene.Instance.baseUrl, wform);
		yield return wwwCardSoldiers;
		Debug.Log ("Sending soldiers = "+wwwCardSoldiers.text);
		if (wwwCardSoldiers.error == null && !wwwCardSoldiers.text.Contains ("error_msg")) {
			callBack (true);
		} else {
			callBack(false);
		}
	}

	public int GetNextMaxLevel(int idOfCard)
	{
		int newMaxLevel = 0;
		switch (mycards [idOfCard].max_level)
		{
		case 0:
			int tempMax= mycards [idOfCard].max_level;
			tempMax= 25;

			newMaxLevel = 40;
			break;
		case 25:
			newMaxLevel = 40;
			break;
		case 40:
			newMaxLevel = 55;
			break;
		case 55:
			newMaxLevel = 70;
			break;
		case 70:
			newMaxLevel = 85;
			break;
		case 85:
			newMaxLevel = 100;
			break;
		}
		return newMaxLevel;
	}
	public int GetBaseExp(string rarity)
	{
		int skillExp = 0;
		switch (rarity) {
		case "Common":
			skillExp = 0;
			break;
		case "Uncommon":
			skillExp = 0;
			break;
		case "Super":
			skillExp = 25;
			break;
		case "Mega":
			skillExp = 50;
			break;
		case "Legendary":
			skillExp = 100;
			break;
		}
		return skillExp;
	}

}

[System.Serializable]
public class GameCards
{
	public List <Sprite> gameCards = new List<Sprite>();

	public int GetCardId(string nameOfCard)
	{
		int idOfCard = 0;
		for (int i = 0; i < gameCards.Count; i++) {
			if(gameCards[i].name == nameOfCard)
			{
				idOfCard = i;
				break;
			}
		}
		return idOfCard;
	}
}
