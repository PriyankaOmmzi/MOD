using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using UnityEngine.UI;


public class OpponentData : MonoBehaviour {
	public PlayerParameterValues myPlayerParameter;
	public int playerID;
	public List<CardsManager.CardParameters> enemyCards;
	public List <CardDeck> cardDecks;
	public static OpponentData _instance;
//	public EmpireManager.BuildingParamters castle, storage,gate,trainingGround,prison,barracks,lab,treeOfKnowledge,goldMine,barn;
	public EmpireManager.BuildingParamters []buildings; //castle = 0, storage = 1,gate = 2,trainingGround = 3,prison = 4,barracks = 5,lab = 6,
														//treeOfKnowledge = 7,goldMine = 8,barn = 9


	// Use this for initialization
	void Start () {
		_instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	int GetBuildingID(string buildingName){
		int buildingId = 0;
		switch (buildingName) {
		case "castle":
			buildingId = 0;
			break;
		case "storage":
			buildingId = 1;
			break;
		case "gate":
			buildingId = 2;
			break;
		case "traningGround":
			buildingId = 3;
			break;
		case "prison":
			buildingId = 4;
			break;
		case "barrack":
			buildingId = 5;
			break;
		case "lab":
			buildingId = 6;
			break;
		case "treeOfKnowledge":
			buildingId = 7;
			break;
		case "goldMine":
			buildingId = 8;
			break;
		case "barn":
			buildingId = 9;
			break;
		}
		return buildingId;
	}

	float GetTimeRequiredForPrimary(int buildingID , int buildingLevel){
		float timeRequiredPerLevel = 0;
		switch (buildingID) {
		case 0:
			timeRequiredPerLevel = EmpireManager._instance.castle.timeRequiredPerLevel [buildingLevel];
			break;
		case 1:
			timeRequiredPerLevel = EmpireManager._instance.storage.timeRequiredPerLevel [buildingLevel];
			break;
		case 2:
			timeRequiredPerLevel = EmpireManager._instance.gate.timeRequiredPerLevel [buildingLevel];
			break;
		case 3:
			timeRequiredPerLevel = EmpireManager._instance.trainingGround.timeRequiredPerLevel [buildingLevel];
			break;
		case 4:
			timeRequiredPerLevel = EmpireManager._instance.prison.timeRequiredPerLevel [buildingLevel];
			break;
		case 5:
			timeRequiredPerLevel = EmpireManager._instance.barracks.timeRequiredPerLevel [buildingLevel];
			break;
		case 6:
			timeRequiredPerLevel = EmpireManager._instance.lab.timeRequiredPerLevel [buildingLevel];
			break;
		case 7:
			timeRequiredPerLevel = EmpireManager._instance.treeOfKnowledge.timeRequiredPerLevel [buildingLevel];
			break;
		case 8:
			timeRequiredPerLevel = EmpireManager._instance.goldMine.timeRequiredPerLevel [buildingLevel];
			break;
		case 9:
			timeRequiredPerLevel = EmpireManager._instance.barn.timeRequiredPerLevel [buildingLevel];
			break;
		}
		return timeRequiredPerLevel;
	}


	int TimeForSecondaryUpgrade(int buildingID){
		int timer = 0;
		//castle = 0, storage = 1,gate = 2,trainingGround = 3,prison = 4,barracks = 5,lab = 6,
		//treeOfKnowledge = 7,goldMine = 8,barn = 9
		if (buildingID == 0 || buildingID == 1 || buildingID == 2 || buildingID == 4 || buildingID == 7 || buildingID == 8 || buildingID == 9) {
			timer = 3600;
		} else if( buildingID == 5) {
			timer = 180;
		}
		return timer;
	}

	public void ResetOpponentsData()
	{
		for (int i = 0; i < cardDecks.Count; i++) {
			for (int j = 0; j < cardDecks[i].cardRows.Count; j++) {
				cardDecks[i].cardRows[j].cardIdsForRow.Clear ();

			}
			cardDecks [i].deckCost = 0;
			cardDecks [i].deckAttack = 0;
			cardDecks [i].deckDefense = 0;
			cardDecks [i].deckLeadership = 0;
			cardDecks [i].noOfCardsSelected = 0;
		}
		buildings = new EmpireManager.BuildingParamters[10];
		enemyCards.Clear ();

	}

	public void GetOpponentsData(IDictionary opponentDict , System.Action<bool>  callBack)
	{
		ResetOpponentsData ();
		IDictionary dataDict = (IDictionary)opponentDict["data"];

		IList opponentData = (IList)dataDict["Players"];
		IDictionary opponentParamaters = (IDictionary)opponentData[0];


		if(opponentParamaters["gems"] != null)
			int.TryParse (opponentParamaters["gems"].ToString (), out myPlayerParameter.gems);

		if(opponentParamaters["captivesList"] != null)
			int.TryParse (opponentParamaters["captivesList"].ToString (), out myPlayerParameter.questFormationDeck);

		if(opponentParamaters["interrogationList"] != null)
			int.TryParse (opponentParamaters["interrogationList"].ToString (), out myPlayerParameter.battleFormationDeck);
		
		if(opponentParamaters["gold"] != null)
			int.TryParse (opponentParamaters["gold"].ToString (), out myPlayerParameter.gold);
		if (opponentParamaters["gold_time"] != null && opponentParamaters ["gold_time"].ToString () != "0000-00-00 00:00:00" && !string.IsNullOrEmpty(opponentParamaters ["gold_time"].ToString ()) && opponentParamaters ["gold_time"].ToString () != "01/01/0001 00:00:00") {
			myPlayerParameter.gold_time = System.Convert.ToDateTime (opponentParamaters ["gold_time"].ToString ());
		}
		if(opponentParamaters["wheat"] != null)
			int.TryParse (opponentParamaters["wheat"].ToString (), out myPlayerParameter.wheat);
		if (opponentParamaters["wheat_time"] != null && opponentParamaters ["wheat_time"].ToString () != "0000-00-00 00:00:00" && !string.IsNullOrEmpty (opponentParamaters ["wheat_time"].ToString ()) && opponentParamaters ["wheat_time"].ToString () != "01/01/0001 00:00:00") {
			myPlayerParameter.wheat_time = System.Convert.ToDateTime (opponentParamaters ["wheat_time"].ToString ());
		}

		if(opponentParamaters["stamina"] != null)
			int.TryParse (opponentParamaters["stamina"].ToString (), out myPlayerParameter.stamina);
		if(opponentParamaters["stamina_time"] != null && opponentParamaters["stamina_time"].ToString () != "0000-00-00 00:00:00"  && !string.IsNullOrEmpty(opponentParamaters ["stamina_time"].ToString ()) && opponentParamaters ["stamina_time"].ToString () != "01/01/0001 00:00:00")
			myPlayerParameter.stamina_time = System.Convert.ToDateTime (opponentParamaters["stamina_time"].ToString ());
		if(opponentParamaters["orb"] != null)
			int.TryParse (opponentParamaters["orb"].ToString (), out myPlayerParameter.orb);
		

		if(opponentParamaters["avatar_defense"] != null)
			int.TryParse (opponentParamaters["avatar_defense"].ToString (), out myPlayerParameter.avatar_defense);
		if(opponentParamaters["avatar_attack"] != null)
			int.TryParse (opponentParamaters["avatar_attack"].ToString (), out myPlayerParameter.avatar_attack);
		if(opponentParamaters["avatar_leadership"] != null)	
			int.TryParse (opponentParamaters["avatar_leadership"].ToString (), out myPlayerParameter.avatar_leadership);
		
		if (opponentParamaters["item_set"] != null && !string.IsNullOrEmpty (opponentParamaters ["item_set"].ToString())) {
			string[] itmSets = opponentParamaters ["item_set"].ToString ().Split (',');
			for (int i = 0; i < itmSets.Length; i++) {
				int.TryParse (itmSets[i],  out myPlayerParameter.artefacts[i]);
			}
		}
		if(opponentParamaters["currently_available_soldiers"] != null)
			int.TryParse (opponentParamaters["currently_available_soldiers"].ToString (), out myPlayerParameter.currentlyAvailableSoldiers);
		if(opponentParamaters["currently_deployed_soldiers"] != null)
			int.TryParse (opponentParamaters["currently_deployed_soldiers"].ToString (), out myPlayerParameter.currentlyDeployedSoldiers);

		if(opponentParamaters["membership_no"] != null)
			int.TryParse (opponentParamaters["membership_no"].ToString (), out myPlayerParameter.membership_no);
		if (opponentParamaters ["time_of_membership_no"] != null && opponentParamaters ["time_of_membership_no"].ToString () != "0000-00-00 00:00:00" && !string.IsNullOrEmpty (opponentParamaters ["time_of_membership_no"].ToString ()) && opponentParamaters ["time_of_membership_no"].ToString () != "01/01/0001 00:00:00") {
			myPlayerParameter.time_of_membership_no = System.Convert.ToDateTime (opponentParamaters ["time_of_membership_no"].ToString ());
			System.TimeSpan timeSpan_membership_over = TimeManager._instance.GetCurrentServerTime () - myPlayerParameter.time_of_membership_no;
		}

		if(opponentParamaters["avatar_no"] != null)
			int.TryParse (opponentParamaters ["avatar_no"].ToString (), out myPlayerParameter.avatar_no);





		IList opponentcards = (IList)dataDict["Player cards"];
		for (int i = 0; i < opponentcards.Count; i++) {
			IDictionary perCardDict = (IDictionary)opponentcards [i];
			if (perCardDict ["is_deleted"].ToString () != "true") {
				CardsManager.CardParameters a = new CardsManager.CardParameters ();
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
				int.TryParse (perCardDict ["defense"].ToString (), out a.defense);
				int.TryParse (perCardDict ["leadership"].ToString (), out a.leadership);
				int.TryParse (perCardDict ["experience"].ToString (), out a.experience);
				int.TryParse (perCardDict ["card_soldiers"].ToString (), out a.card_soldiers);
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
					a.skill1_exp = CardsManager._instance.GetBaseExp (a.rarity);
				}
				if (a.skill2_exp == 0) {
					a.skill2_exp = CardsManager._instance.GetBaseExp (a.rarity);
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

				enemyCards.Add (a);
			}
		}

		IList opponentBuildings = (IList)dataDict["Building data"];
		for (int i = 0; i < opponentBuildings.Count; i++) {
			IDictionary perBuildingDict = (IDictionary)opponentBuildings [i];
			int buildingId = GetBuildingID(perBuildingDict ["building_name"].ToString ());
			int.TryParse (perBuildingDict ["level"].ToString (), out buildings[buildingId].currentLevel);
			int.TryParse (perBuildingDict ["primary_card_locked_no"].ToString (), out buildings[buildingId].primaryCardNo);
			int.TryParse (perBuildingDict ["secondary_card_no"].ToString (), out buildings[buildingId].secondaryCardNo);
			if (perBuildingDict ["currentt_time"].ToString () != "0000-00-00 00:00:00" && !string.IsNullOrEmpty (perBuildingDict ["currentt_time"].ToString ())) {
				buildings[buildingId].timeOfLockOfPrimary = System.Convert.ToDateTime (perBuildingDict ["currentt_time"].ToString ());
			}
			if (perBuildingDict ["time_of_secondary_upgrade"].ToString () != "0000-00-00 00:00:00" && !string.IsNullOrEmpty (perBuildingDict ["time_of_secondary_upgrade"].ToString ())) {
				buildings[buildingId].timeOfLockOfSecondary = System.Convert.ToDateTime (perBuildingDict ["time_of_secondary_upgrade"].ToString ());
			}

			if (buildings [buildingId].primaryCardNo > 0 && isCardPresentInList(buildings [buildingId].primaryCardNo)) {
				System.TimeSpan diff = TimeManager._instance.GetCurrentServerTime () - buildings [buildingId].timeOfLockOfPrimary;
				float diffSeconds = Mathf.Abs ((float)diff.TotalSeconds);
				if (GetTimeRequiredForPrimary(buildingId ,buildings [buildingId].currentLevel ) * 3600 > diffSeconds && diffSeconds > 0) {
					enemyCards.RemoveAt (CardsManager._instance.PositionOfEnemyCardInList(buildings [buildingId].primaryCardNo));
				}
			}
			if (buildings [buildingId].secondaryCardNo > 0 && isCardPresentInList(buildings [buildingId].secondaryCardNo)) {
				System.TimeSpan diff = TimeManager._instance.GetCurrentServerTime () - buildings [buildingId].timeOfLockOfSecondary;
				float diffSeconds = Mathf.Abs ((float)diff.TotalSeconds);
				if (TimeForSecondaryUpgrade(buildingId) > diffSeconds && diffSeconds > 0) {
					enemyCards.RemoveAt (CardsManager._instance.PositionOfEnemyCardInList(buildings [buildingId].secondaryCardNo));
				}
			}
		}

		if (opponentParamaters ["orb_time"] != null && opponentParamaters ["orb_time"].ToString () != "0000-00-00 00:00:00" && !string.IsNullOrEmpty (opponentParamaters ["orb_time"].ToString ()) && opponentParamaters ["orb_time"].ToString () != "01/01/0001 00:00:00") {
			myPlayerParameter.orb_time = System.Convert.ToDateTime (opponentParamaters ["orb_time"].ToString ());
			System.TimeSpan timeSpanOfGold = TimeManager._instance.GetCurrentServerTime () - myPlayerParameter.gold_time;
			myPlayerParameter.gold+= (int)timeSpanOfGold.TotalHours*EmpireManager._instance.goldMine.finalValue1[EmpireManager._instance.goldMine.currentLevel];

			System.TimeSpan timeSpanOfWheat = TimeManager._instance.GetCurrentServerTime () - myPlayerParameter.wheat_time;
			myPlayerParameter.wheat+= (int)timeSpanOfWheat.TotalHours*buildings[2].finalValue1[buildings[2].currentLevel];
		}


//		if (opponentParamaters ["QuestCardFormation"] != null) {
//			if (!string.IsNullOrEmpty (opponentParamaters ["QuestCardFormation"].ToString ())) {
//				Debug.Log (opponentParamaters ["QuestCardFormation"].ToString ());
//				IDictionary questData = (IDictionary)Json.Deserialize (opponentParamaters ["QuestCardFormation"].ToString ());
//				for (int k = 1; k <=3; k++) {
//					IList deck = (IList)questData ["deck" + k];
//					for (int i = 0; i < deck.Count; i++) {
//						IDictionary deckDic = (IDictionary)deck [i];
//						if (!string.IsNullOrEmpty (deckDic ["row" + i].ToString ())) {
//							string [] row = deckDic ["row" + i].ToString ().Split (',');
//							for (int j = 0; j < row.Length; j++) {
//								int cardIdInEnemyList = int.Parse (row [j]);
//								if (isCardPresentInList (cardIdInEnemyList)) {
//									enemyCards.RemoveAt (CardsManager._instance.PositionOfEnemyCardInList (cardIdInEnemyList));
//								}
//							}
//						}
//					}
//				}
//			}
//		}

		if (opponentParamaters ["BattleCardFormation"] != null) {
			if (!string.IsNullOrEmpty (opponentParamaters ["BattleCardFormation"].ToString ())) {
				Debug.Log (opponentParamaters ["BattleCardFormation"].ToString ());
				IDictionary questData = (IDictionary)Json.Deserialize (opponentParamaters ["BattleCardFormation"].ToString ());
				for (int k = 1; k <=3; k++) {
					IList deck = (IList)questData ["deck" + k];
					for (int i = 0; i < deck.Count; i++) {
						IDictionary deckDic = (IDictionary)deck [i];
						if (!string.IsNullOrEmpty (deckDic ["row" + i].ToString ())) {
							string [] row = deckDic ["row" + i].ToString ().Split (',');
							for (int j = 0; j < row.Length; j++) {
								int cardIdInEnemyList = int.Parse (row [j]);
								if (isCardPresentInList (cardIdInEnemyList)) {
									cardDecks [k - 1].cardRows [i].cardIdsForRow.Add (cardIdInEnemyList);
									cardDecks [k - 1].noOfCardsSelected++;
								}
							}
						}
					}
				}
			}
			SetEnemyCardForBattle ();
		}



		callBack (true);
		// Add Enemy cards
		// add to deck depending on rows

	}

	public int TotalSoldiersInDeck() 
	{
		for (int i = 0; i < cardDecks.Count; i++) {
			cardDecks [i].deckSoldiers = 0;
			cardDecks [i].deckLeadership = 0;

			for (int j = 0; j < cardDecks [i].cardRows.Count; j++) {
				for (int k = 0; k < cardDecks [i].cardRows [j].cardIdsForRow.Count; k++) {
					int cardId = CardsManager._instance.PositionOfEnemyCardInList (cardDecks [i].cardRows [j].cardIdsForRow [k]);
					cardDecks [i].deckSoldiers += enemyCards [cardId].card_soldiers;
					cardDecks [i].deckLeadership += enemyCards [cardId].leadership;
				}
			}
		}

		int cardDeckToSelect = (myPlayerParameter.questFormationDeck !=0) ? (myPlayerParameter.questFormationDeck-1) : (myPlayerParameter.battleFormationDeck-1) ;
		if (cardDeckToSelect < 0) {
			if ((cardDecks [0].deckSoldiers >= cardDecks [1].deckSoldiers) && (cardDecks [0].deckSoldiers >= cardDecks [2].deckSoldiers)) {
				cardDeckToSelect = 0;
			} else if ((cardDecks [1].deckSoldiers >= cardDecks [0].deckSoldiers) && (cardDecks [1].deckSoldiers >= cardDecks [2].deckSoldiers)) {
				cardDeckToSelect = 1;
			} else {
				cardDeckToSelect = 2;
			}
		}
		Debug.LogError ("ENEMY CARD DECK DELECTED  = "+cardDeckToSelect);
		return cardDeckToSelect;


	}

	void SetEnemyCardForBattle()
	{
		loadingScene.Instance.battleInstance.enemyDeck.gateLevel = buildings[2].currentLevel;
		loadingScene.Instance.battleInstance.enemyDeck.avatarAttack = myPlayerParameter.avatar_attack;
		loadingScene.Instance.battleInstance.enemyDeck.avatarStamina = myPlayerParameter.stamina;
		loadingScene.Instance.battleInstance.enemyDeck.avatarNo = myPlayerParameter.avatar_no-1;
		int cardDeck = TotalSoldiersInDeck();
		for (int i = 0; i <cardDecks [cardDeck].cardRows.Count; i++) {
			for(int j = 0 ; j <cardDecks [cardDeck].cardRows[i].cardIdsForRow.Count ; j++){
				int cardNoInMyCards = CardsManager._instance.PositionOfEnemyCardInList (cardDecks[cardDeck].cardRows[i].cardIdsForRow[j]);
				loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardIdsInPlayerList.Add (cardDecks[cardDeck].cardRows[i].cardIdsForRow[j]);
				loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsAttack.Add (enemyCards[cardNoInMyCards].attack);
				loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsDefense.Add (enemyCards[cardNoInMyCards].defense);
				loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsLeadership.Add (enemyCards[cardNoInMyCards].leadership);
				loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsSoldiers.Add (enemyCards[cardNoInMyCards].card_soldiers);
				loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardSkill1.Add (enemyCards[cardNoInMyCards].skill_1);
				loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardSkill2.Add (enemyCards[cardNoInMyCards].skill_2);
				loadingScene.Instance.battleInstance.enemyCards[i].cardEntity[j].GetComponent<Image>().sprite = enemyCards[cardNoInMyCards].cardSpriteFromResources;
			}


			for(int j =cardDecks [cardDeck].cardRows[i].cardIdsForRow.Count ; j < 5 ; j++){
				Destroy(loadingScene.Instance.battleInstance.enemyCards[i].cardEntity[j]);
				Destroy(loadingScene.Instance.battleInstance.enemyHealth[i].cardEntity[j].transform.parent.gameObject);
			}
		}
	}

	public bool isCardPresentInList(int cardToCheck)
	{
		int cardToFetch = -1;
		for(int i = 0 ; i < OpponentData._instance.enemyCards.Count ; i++)
		{
			if(cardToCheck == OpponentData._instance.enemyCards[i].card_id_in_playerList)
			{
				cardToFetch = i;
				break;
			}
		}
		if (cardToFetch > -1)
			return true;
		else
			return false;
	}
}
