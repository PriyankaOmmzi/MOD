using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;



[System.Serializable]
public class SoldiersDeckCalculations
{
	public List <CardRowSoldiers> cardRowCalculations;
}

[System.Serializable]
public class CardRowSoldiers
{
	public List <float> cardsSoldiersPercentage;
}

public class BattleLogic : MonoBehaviour {
	

	public enum BattleType
	{
		QUEST,
		QUEST_BOSS,
		BATTLE,
		CHEST_EVENT,
		RAID,
		BATTLE_ROYAL,
		CONQUEROR,
		PUZZLE,
		FLOOR_CLEARING,
		CASTLE_DEFEND,
		PET_BREEDING,
		NONE,
		CHEST_BOSS
	}
	int rowNo;
	bool isPlayerTurn = true;
	bool gateHit;
	int noOfTimesSkillActivatedForEnemy;
	int noOfTimesSkillActivatedForPlayer;

	public BattleType battleType;
	public int totalNoOfWaves;
	public int noOfWavesCompleted;
	public List<int> enemyRowsThatCanBeAttacked;
	public List<int> playerRowsThatCanBeAttacked;
	public List<int> orderOfEnemyAttackOnPlayerRows; // player rows being attacked by enemy
	public List<int> orderOfPlayerAttackOnEnemyRows;// enemy rows being attacked by player
	public List<int> orderOfPlayerAttack;  // order in which player row attacked
	public List<int> orderOfEnemyAttack;  // order in which enemy row attacked
	public List<int> orderOfAttacks;  //1 - player , 0 - enemy
	public List<int> orderOfSkillActivation;  // -1 if no skill , if skill, then correspoding position of card for it
	public List<string> activatedSkillName;  // -1 if no skill , if skill, then correspoding position of card for it

	public int goldLoot , playerGoldLoot;
	public int foodLoot , playerFoodLoot;
	public int orbsTosubtract;
	public static bool isBattleWin; 
	public static BattleLogic _instance;

	public DeckCalculations /*playerDeckSaved ,*/ playerDeckTempSkills;
	public DeckCalculations /*enemyDeckSaved ,*/  enemyDeckTempSkills;

	[System.Serializable]
	public struct BattleDetails
	{
		public int totalDamage;
		public int totalDefense;
		public int totalSkillDamage;
		public int totalSkillDefense;
		public int skillDamageFromTotal;
		public int skillDefenseFromTotal;
		public float percentageOfSkillDamage;
		public float percentageOfSkillDefense;

	}
	public BattleDetails enemyDetails,playerDetails;

	public SoldiersDeckCalculations playerSoldiers, initialPlayerSoldiers;
	public SoldiersDeckCalculations enemySoldiers, initialEnemySoldiers;

	void Awake()
	{
		_instance = this;
	}

	void SetPlayerStats()
	{
		loadingScene.Instance.battleInstance.playerDeck.gateLevel = EmpireManager._instance.gate.currentLevel;
		loadingScene.Instance.battleInstance.playerDeck.avatarAttack = PlayerParameters._instance.myPlayerParameter.avatar_attack;
		loadingScene.Instance.battleInstance.playerDeck.avatarStamina = PlayerParameters._instance.myPlayerParameter.stamina;
		loadingScene.Instance.battleInstance.playerDeck.avatarNo = PlayerParameters._instance.myPlayerParameter.avatar_no-1;
	}

	public void SetPlayerCardForBattle(battleFormation playerFormation)
	{
		SetPlayerStats ();
//		int cardDeck = playerFormation.TotalSoldiersInDeck();
		int cardDeck = (PlayerParameters._instance.myPlayerParameter.questFormationDeck != 0) ? PlayerParameters._instance.myPlayerParameter.questFormationDeck-1 : PlayerParameters._instance.myPlayerParameter.battleFormationDeck-1;
//		Debug.LogError ("PLAYER CARD DECK  = "+cardDeck);
		
		for (int i = 0; i < playerFormation.cardDecks [cardDeck].cardRows.Count; i++) {
			for(int j = 0 ; j < playerFormation.cardDecks [cardDeck].cardRows[i].cardIdsForRow.Count ; j++){
				Debug.Log ("deck  = "+ i +" , row = "+j);
				if (!CardsManager._instance.IsPlayercardLocked (playerFormation.cardDecks [cardDeck].cardRows [i].cardIdsForRow [j] , true)) {
					Debug.Log ("iput in card deck");
					int cardNoInMyCards = CardsManager._instance.PositionOfCardInList (playerFormation.cardDecks [cardDeck].cardRows [i].cardIdsForRow [j]);
					loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations [i].cardIdsInPlayerList.Add (playerFormation.cardDecks [cardDeck].cardRows [i].cardIdsForRow [j]);
					loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations [i].cardsAttack.Add (CardsManager._instance.mycards [cardNoInMyCards].attack);
					loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations [i].cardsDefense.Add (CardsManager._instance.mycards [cardNoInMyCards].defense);
					loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations [i].cardsLeadership.Add (CardsManager._instance.mycards [cardNoInMyCards].leadership);
					loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations [i].cardsSoldiers.Add (CardsManager._instance.mycards [cardNoInMyCards].card_soldiers);
					loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations [i].cardSkill1.Add (CardsManager._instance.mycards [cardNoInMyCards].skill_1);
					loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations [i].cardSkill2.Add (CardsManager._instance.mycards [cardNoInMyCards].skill_2);
					loadingScene.Instance.battleInstance.playerCards [i].cardEntity [j].GetComponent<Image> ().sprite = CardsManager._instance.mycards [cardNoInMyCards].cardSpriteFromResources;
				}
			}


			for(int j = loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations [i].cardIdsInPlayerList.Count ; j < 5 ; j++){
				Destroy(loadingScene.Instance.battleInstance.playerCards[i].cardEntity[j]);
				Destroy(loadingScene.Instance.battleInstance.playerHealth[i].cardEntity[j].transform.parent.gameObject);
			}
		}
	}

	public int AttackingOrbsUsed(int levelOfOpponent)
	{

//		l.i. Consume 1 attack point for targeting a higher level players or a player within 4 level below him
//			l.ii. Consume 2 attack point for targeting players 5-9 level lower
//				l.iii. Consume 3 attack point for targeting players 10 and above
		int orbsToSubtract = 0;
		if (levelOfOpponent - PlayerParameters._instance.myPlayerParameter.avatar_level >= -4) {
			orbsToSubtract = 1;

		} else if (levelOfOpponent - PlayerParameters._instance.myPlayerParameter.avatar_level >= -9) {
			orbsToSubtract = 2;
		} else {
			orbsToSubtract = 3;
		}
		return orbsToSubtract;
	}
	

	public void EnemyResourcesLoot(int levelOfOpponent , int enemyFood , int enemyGold)
	{
		//		m.i. Loot 25% of current enemy resources for targeting a higher level players or a player within 4 level below him
		//			m.ii. Loot 18% of current enemy resources for targeting players 5-9 level lower
		//				m.iii. Loot 8% of current enemy resources for targeting players 10 and above
		if (levelOfOpponent - PlayerParameters._instance.myPlayerParameter.avatar_level >= -4) {
			goldLoot = Mathf.CeilToInt (enemyGold*0.25f);
			foodLoot = Mathf.CeilToInt (enemyFood*0.25f);
			
		} else if (levelOfOpponent - PlayerParameters._instance.myPlayerParameter.avatar_level >= -9) {
			goldLoot = Mathf.CeilToInt (enemyGold*0.18f);
			foodLoot = Mathf.CeilToInt (enemyFood*0.18f);
		} else {
			goldLoot = Mathf.CeilToInt (enemyGold*0.08f);
			foodLoot = Mathf.CeilToInt (enemyFood*0.08f);
		}
	}

	public void PlayerResourcesLoot(int levelOfEnemy , int playerFood , int playerGold)
	{
		//		m.i. Loot 25% of current enemy resources for targeting a higher level players or a player within 4 level below him
		//			m.ii. Loot 18% of current enemy resources for targeting players 5-9 level lower
		//				m.iii. Loot 8% of current enemy resources for targeting players 10 and above
		if ( PlayerParameters._instance.myPlayerParameter.avatar_level - levelOfEnemy >= -4) {
			playerGoldLoot = Mathf.CeilToInt (playerGold*0.25f);
			playerFoodLoot = Mathf.CeilToInt (playerFood*0.25f);

		} else if (PlayerParameters._instance.myPlayerParameter.avatar_level - levelOfEnemy >= -9) {
			playerGoldLoot = Mathf.CeilToInt (playerGold*0.18f);
			playerFoodLoot = Mathf.CeilToInt (playerFood*0.18f);
		} else {
			playerGoldLoot = Mathf.CeilToInt (playerGold*0.08f);
			playerFoodLoot = Mathf.CeilToInt (playerFood*0.08f);
		}
	}

	public void CalculateRowStatsFirstTime()
	{
		playerSoldiers.cardRowCalculations.Clear ();
		enemySoldiers.cardRowCalculations.Clear ();
		enemyRowsThatCanBeAttacked.Clear ();
		playerRowsThatCanBeAttacked.Clear ();
		for (int i = 0; i < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations.Count; i++) {

			BattleLogic._instance.initialEnemySoldiers.cardRowCalculations[i].cardsSoldiersPercentage.Clear ();
			loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].rowAP = 0;
			loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].rowDP = 0;
			loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].rowSoldiers = 0;

			for (int j = 0; j < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsAttack.Count; j++) {
				if(i == 0)
				{
					loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsAttack[j]+=Mathf.CeilToInt (0.01f*loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsAttack[j]);
				}
				else if(i == 1)
				{
					loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsDefense[j]+=Mathf.CeilToInt (0.01f*loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsDefense[j]);
				}
				else if(i == 2)
				{
					loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsSoldiers[j]+=Mathf.CeilToInt (0.01f*loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsSoldiers[j]);
				}
//				enemyDeckSaved.cardRowCalculations[i].cardsSoldiers.Add (loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsSoldiers[j]);
//				enemyDeckSaved.cardRowCalculations[i].cardsDefense.Add (loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsDefense[j]);
//				enemyDeckSaved.cardRowCalculations[i].cardsAttack.Add (loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsAttack[j]);
				loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].rowAP+=(loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsAttack[j]*loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsSoldiers[j]);
				loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].rowDP+=(loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsDefense[j]*loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsSoldiers[j]);
				loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].rowSoldiers+=(loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsSoldiers[j]);
				float soldierPercentage = (float)loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsSoldiers[j]/loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsLeadership[j];
				if (soldierPercentage > 1)
					soldierPercentage = 1;
				initialEnemySoldiers.cardRowCalculations [i].cardsSoldiersPercentage.Add (soldierPercentage);
			}
			if(loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].rowSoldiers > 0)
			{
				enemyRowsThatCanBeAttacked.Add (i);
			}
		}
		
		for (int i = 0; i < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations.Count; i++) {
			BattleLogic._instance.initialPlayerSoldiers.cardRowCalculations[i].cardsSoldiersPercentage.Clear ();
			loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].rowAP = 0;
			loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].rowDP = 0;
			loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].rowSoldiers = 0;
			
			for (int j = 0; j < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsAttack.Count; j++) {
				if(i == 0)
				{
					loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsAttack[j]+=Mathf.CeilToInt (0.01f*loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsAttack[j]);
				}
				else if(i == 1)
				{
					loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsDefense[j]+=Mathf.CeilToInt (0.01f*loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsDefense[j]);
				}
				else if(i == 2)
				{
					loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsSoldiers[j]+=Mathf.CeilToInt (0.01f*loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsSoldiers[j]);
				}
//				playerDeckSaved.cardRowCalculations[i].cardsSoldiers.Add (loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsSoldiers[j]);
//				playerDeckSaved.cardRowCalculations[i].cardsDefense.Add (loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsDefense[j]);
//				playerDeckSaved.cardRowCalculations[i].cardsAttack.Add (loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsAttack[j]);
				loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].rowAP+=(loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsAttack[j]*loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsSoldiers[j]);
				loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].rowDP+=(loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsDefense[j]*loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsSoldiers[j]);
				loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].rowSoldiers+=(loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsSoldiers[j]);
				float soldierPercentage = (float)loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsSoldiers[j]/loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsLeadership[j];
				if (soldierPercentage > 1)
					soldierPercentage = 1;
				initialPlayerSoldiers.cardRowCalculations [i].cardsSoldiersPercentage.Add (soldierPercentage);
			}
			Debug.Log ("Row "+i+" , soldiers = "+loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].rowSoldiers);
			if(loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].rowSoldiers > 0)
			{
				playerRowsThatCanBeAttacked.Add (i);
			}
		}
	}

	void UpdateRowStats(int playerRowNo = -1, int enemyRowNo = -1 , bool isPlayerTurnParam = false)
	{
		isPlayerTurnParam = isPlayerTurn;
		int playerRowAPInitially = 0;
		int playerRowDpInitially = 0;
		int enemyRowAPInitially = 0;
		int enemyRowDPInitially = 0;
		if (playerRowNo > -1) {
			playerRowAPInitially = loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations [playerRowNo].rowAP;
			playerRowDpInitially = loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations [playerRowNo].rowAP;
			int totalSoldiersInitially = 0;
			int soldiersNow = loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[playerRowNo].rowSoldiers;
			Debug.Log("player soldiersNow   "+playerRowNo+" = "+soldiersNow);
			for (int j = 0; j < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[playerRowNo].cardsAttack.Count; j++) {
				totalSoldiersInitially+=(loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[playerRowNo].cardsSoldiers[j]);
			}
			Debug.Log("player totalSoldiersInitially   "+playerRowNo+" = "+totalSoldiersInitially);
			
			for (int j = 0; j < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[playerRowNo].cardsAttack.Count; j++) {
				if(totalSoldiersInitially > 0)
				{
					float ratio = (loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[playerRowNo].cardsSoldiers[j]/(float)totalSoldiersInitially);
					loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[playerRowNo].cardsSoldiers[j] = Mathf.CeilToInt(loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[playerRowNo].rowSoldiers*ratio);
				}
			}
			CardRowSoldiers playerRowSoldiers = new CardRowSoldiers();
			playerRowSoldiers.cardsSoldiersPercentage = new List<float> ();
			playerRowSoldiers.cardsSoldiersPercentage.Clear ();
			for (int j = 0; j < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[playerRowNo].cardsAttack.Count; j++) {
				float soldierPercentage = (float)loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[playerRowNo].cardsSoldiers[j]/loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[playerRowNo].cardsLeadership[j];
				if (soldierPercentage > 1)
					soldierPercentage = 1;
//				Debug.Log ("soldierPercentage = "+soldierPercentage);
//				Debug.Log ("playerRowSoldiers.cardsSoldiersPercentage = "+playerRowSoldiers.cardsSoldiersPercentage);

				playerRowSoldiers.cardsSoldiersPercentage.Add (soldierPercentage);
			}
			playerSoldiers.cardRowCalculations.Add (playerRowSoldiers);
			Debug.Log ("playerSoldiers.cardRowCalculations = "+playerSoldiers.cardRowCalculations);
			Debug.Log ("playerRowSoldiers = "+playerRowSoldiers);
		}
		if (enemyRowNo > -1) {
			enemyRowAPInitially = loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations [enemyRowNo].rowAP;
			enemyRowDPInitially = loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations [enemyRowNo].rowDP;
			int totalSoldiersInitially = 0;
			int soldiersNow = loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations [enemyRowNo].rowSoldiers;
			Debug.Log ("enemy soldiersNow for row  " + enemyRowNo + " = " + soldiersNow);
			for (int j = 0; j < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations [enemyRowNo].cardsAttack.Count; j++) {
				totalSoldiersInitially += (loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations [enemyRowNo].cardsSoldiers [j]);
			}
			Debug.Log ("enemy totalSoldiersInitially  " + enemyRowNo + " = " + totalSoldiersInitially);
			
			for (int j = 0; j < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations [enemyRowNo].cardsAttack.Count; j++) {
				if (totalSoldiersInitially > 0) {
					float ratio = (loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations [enemyRowNo].cardsSoldiers [j] / (float)totalSoldiersInitially);
					loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations [enemyRowNo].cardsSoldiers [j] = Mathf.CeilToInt (loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations [enemyRowNo].rowSoldiers * ratio);
				}
			}

			CardRowSoldiers enemyRowSoldiers  = new CardRowSoldiers();
			enemyRowSoldiers.cardsSoldiersPercentage = new List<float> ();
			for (int j = 0; j < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[enemyRowNo].cardsAttack.Count; j++) {
				float soldierPercentage = loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[enemyRowNo].cardsSoldiers[j]/(float)loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[enemyRowNo].cardsLeadership[j];
				if (soldierPercentage > 1)
					soldierPercentage = 1;

				enemyRowSoldiers.cardsSoldiersPercentage.Add (soldierPercentage);
			}
			enemySoldiers.cardRowCalculations.Add (enemyRowSoldiers);

		} else {
			CardRowSoldiers enemyRowSoldiers = new CardRowSoldiers ();
			enemySoldiers.cardRowCalculations.Add (enemyRowSoldiers);
		}
		playerRowsThatCanBeAttacked.Clear ();
		enemyRowsThatCanBeAttacked.Clear ();
		for (int i = 0; i < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations.Count; i++) {
			loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].rowAP = 0;
			loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].rowDP = 0;
			loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].rowSoldiers = 0;

			for (int j = 0; j < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsAttack.Count; j++) {
				loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].rowAP+=(loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsAttack[j]*loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsSoldiers[j]);
				loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].rowDP+=(loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsDefense[j]*loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsSoldiers[j]);
				loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].rowSoldiers+=(loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsSoldiers[j]);
			}
			Debug.Log(i+ " row , Enemy soldiers = "+loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].rowSoldiers);
			if(loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].rowSoldiers > 0)
			{
				enemyRowsThatCanBeAttacked.Add (i);
			}
		}

		for (int i = 0; i < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations.Count; i++) {
			loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].rowAP = 0;
			loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].rowDP = 0;
			loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].rowSoldiers = 0;
			
			for (int j = 0; j < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsAttack.Count; j++) {
				loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].rowAP+=(loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsAttack[j]*loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsSoldiers[j]);
				loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].rowDP+=(loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsDefense[j]*loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsSoldiers[j]);
				loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].rowSoldiers+=(loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsSoldiers[j]);
			}
			Debug.Log(i+ " row , player soldiers = "+loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].rowSoldiers);
			
			if(loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].rowSoldiers > 0)
			{
				playerRowsThatCanBeAttacked.Add (i);
			}
		}

		if (playerRowNo > -1) {
			int playerApDiff = playerRowAPInitially - loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations [playerRowNo].rowAP;
			playerDetails.totalDamage += playerApDiff;
			if (playerDetails.percentageOfSkillDamage > 0) {
				playerDetails.skillDamageFromTotal += Mathf.CeilToInt (playerDetails.percentageOfSkillDamage*playerApDiff/100f);
			}
			if (!isPlayerTurnParam) {
				int playerDpDiff = loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations [playerRowNo].rowDP;
				playerDetails.totalDefense += playerDpDiff;
				if (playerDetails.percentageOfSkillDefense > 0) 
					playerDetails.skillDefenseFromTotal += Mathf.CeilToInt (playerDetails.percentageOfSkillDefense*playerDpDiff/100f);
			}
		}
		if (enemyRowNo > -1) {
			int enemyApDiff = enemyRowAPInitially - loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations [enemyRowNo].rowAP;
			enemyDetails.totalDamage +=enemyApDiff;
			if (enemyDetails.percentageOfSkillDamage > 0) 
				enemyDetails.skillDamageFromTotal += Mathf.CeilToInt (enemyDetails.percentageOfSkillDamage*enemyApDiff/100f);
			if (isPlayerTurnParam) {
				int enemyDpDiff = loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations [enemyRowNo].rowDP;
				enemyDetails.totalDefense += enemyDpDiff;
				if (enemyDetails.percentageOfSkillDefense > 0) 
					enemyDetails.skillDefenseFromTotal += Mathf.CeilToInt (enemyDetails.percentageOfSkillDefense*enemyDpDiff/100f);
			}

		}
	}

	public void ResetStats()
	{
		rowNo = 0;
		isPlayerTurn = true;
		gateHit = false;
		noOfWavesCompleted = 0;
		noOfTimesSkillActivatedForPlayer = 0;
		noOfTimesSkillActivatedForEnemy = 0;
		enemyRowsThatCanBeAttacked.Clear ();
		playerRowsThatCanBeAttacked.Clear ();
		orderOfEnemyAttackOnPlayerRows.Clear ();
		orderOfPlayerAttackOnEnemyRows.Clear ();
		orderOfPlayerAttack.Clear ();  
		orderOfEnemyAttack.Clear ();  
		orderOfAttacks.Clear ();  
		orderOfSkillActivation.Clear ();
		activatedSkillName.Clear ();

	}
	
	public void Battle()
	{
		Debug.Log ("rowNo = "+rowNo);
		Debug.Log ("isPlayerTurn = "+isPlayerTurn);
		Debug.Log ("enemyRowsThatCanBeAttacked = "+enemyRowsThatCanBeAttacked.Count);
		Debug.Log ("playerRowsThatCanBeAttacked = "+playerRowsThatCanBeAttacked.Count);
		if (noOfWavesCompleted < totalNoOfWaves && enemyRowsThatCanBeAttacked.Count > 0 && playerRowsThatCanBeAttacked.Count > 0) {
			if(isPlayerTurn)
			{
				if (!gateHit && !playerRowsThatCanBeAttacked.Contains (rowNo)) {
					rowNo++;
					if(rowNo > 2)
					{
						noOfWavesCompleted++;
					}
					Battle();
				}
				else if(playerRowsThatCanBeAttacked.Contains (rowNo) && enemyRowsThatCanBeAttacked.Count > 0)
				{
					if(!gateHit)
					{
						gateHit = true;
						int soldiersReduced = Mathf.FloorToInt(loadingScene.Instance.battleInstance.enemyDeck.gateLevel*loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowNo].rowSoldiers/100f);
						Debug.Log("soldiersReduced = "+soldiersReduced);
						loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowNo].rowSoldiers -=soldiersReduced;
						if (loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations [rowNo].rowSoldiers < 0)
							loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations [rowNo].rowSoldiers = 0;
						orderOfAttacks.Add (1);
						orderOfSkillActivation.Add(-1);
						activatedSkillName.Add ("");
						orderOfPlayerAttack.Add (rowNo);
						orderOfPlayerAttackOnEnemyRows.Add (-1);
						UpdateRowStats(rowNo , -1);
						Battle();
					}
					else
					{
						Debug.Log("player--attacking ? "+rowNo);
						int rowToAttack = Random.Range (0,enemyRowsThatCanBeAttacked.Count);
						orderOfPlayerAttackOnEnemyRows.Add (rowToAttack);
						orderOfAttacks.Add (1);
						if (noOfTimesSkillActivatedForPlayer < 1 && (Random.Range (1, 100) < 100)) {
							int skillOfCardToUpdate = Random.Range (0, loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations [rowNo].cardIdsInPlayerList.Count);
							int cardIdInPlayerList = loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations [rowNo].cardIdsInPlayerList [skillOfCardToUpdate];
							int cardId = CardsManager._instance.PositionOfCardInList (cardIdInPlayerList);
							string cardRarity = CardsManager._instance.mycards [cardId].rarity;
							if (cardRarity != "Common" && cardRarity != "Uncommon") {
								if (CheckIfSkilIsTriggered (cardId, true, skillOfCardToUpdate, rowNo)) {
									orderOfSkillActivation.Add (skillOfCardToUpdate);
									string skillName = CardsManager._instance.mycards[cardId].skill_1;
									activatedSkillName.Add (skillName);
									noOfTimesSkillActivatedForPlayer++;
								} else {
									orderOfSkillActivation.Add (-1);
									activatedSkillName.Add ("");
								}
							} else {
								orderOfSkillActivation.Add (-1);
								activatedSkillName.Add ("");
							}
						} else {
							orderOfSkillActivation.Add (-1);
							activatedSkillName.Add ("");
						}

						orderOfPlayerAttack.Add (rowNo);
						int totalValue = loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowNo].rowAP+loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[rowToAttack].rowDP;
						Debug.Log("totalValue = "+totalValue);
						float ratioOfPlayer = loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowNo].rowAP/(float)totalValue;
						float ratioOfEnemy = loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[rowNo].rowDP/(float)totalValue;
						Debug.Log("ratioOfPlayer = "+ratioOfPlayer);
						Debug.Log("ratioOfEnemy = "+ratioOfEnemy);
						loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowNo].rowSoldiers -= Mathf.CeilToInt(ratioOfEnemy*loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowNo].rowSoldiers);
						if (loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations [rowNo].rowSoldiers < 0)
							loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations [rowNo].rowSoldiers = 0;
						loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[rowToAttack].rowSoldiers -= Mathf.CeilToInt(ratioOfPlayer*loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[rowToAttack].rowSoldiers);					
						if (loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[rowToAttack].rowSoldiers < 0)
							loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[rowToAttack].rowSoldiers = 0;
						UpdateRowStats(rowNo , rowToAttack);
						isPlayerTurn = false;
						Battle();
					}
				}
				else
				{
					// enemy should be given attack
					Debug.Log("enemy should be given attack");
					isPlayerTurn = false;
					Battle();
				}
			}
			else
			{

				//Enemy Turn
				if(enemyRowsThatCanBeAttacked.Contains (rowNo) && playerRowsThatCanBeAttacked.Count > 0)
				{
					Debug.Log("enemey--attacked ? "+rowNo);
					int rowToAttack = Random.Range (0,playerRowsThatCanBeAttacked.Count);
					orderOfEnemyAttackOnPlayerRows.Add (rowToAttack);
					orderOfAttacks.Add (0);
					if (battleType == BattleType.BATTLE) {


						int skillOfCardToUpdate = Random.Range (0, loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations [rowNo].cardIdsInPlayerList.Count);
						int cardIdInPlayerList = loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations [rowNo].cardIdsInPlayerList [skillOfCardToUpdate];
						int cardId = CardsManager._instance.PositionOfEnemyCardInList (cardIdInPlayerList);
						string cardRarity = OpponentData._instance.enemyCards [cardId].rarity;
						if (cardRarity != "Common" && cardRarity != "Uncommon") {
							if (CheckIfSkilIsTriggered (cardId, false, skillOfCardToUpdate, rowNo)) {
								orderOfSkillActivation.Add (skillOfCardToUpdate);
								string skillName = OpponentData._instance.enemyCards[cardId].skill_1;
								activatedSkillName.Add (skillName);
								noOfTimesSkillActivatedForEnemy++;
							} else {
								orderOfSkillActivation.Add (-1);
								activatedSkillName.Add ("");
							}
						} else {
							orderOfSkillActivation.Add (-1);
							activatedSkillName.Add ("");
						}
//						orderOfSkillActivation.Add (Random.Range (0, loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations [rowNo].cardsSoldiers.Count));
//						noOfTimesSkillActivatedForEnemy++;
					} else {
						orderOfSkillActivation.Add (-1);
						activatedSkillName.Add ("");
					}
					orderOfEnemyAttack.Add (rowNo);
					int totalValue = loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[rowNo].rowAP+loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowToAttack].rowDP;
					float ratioOfPlayer = loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowNo].rowDP/(float)totalValue;
					float ratioOfEnemy = loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[rowNo].rowAP/(float)totalValue;
					Debug.Log("ratioOfPlayer = "+ratioOfPlayer);
					Debug.Log("ratioOfEnemy = "+ratioOfEnemy);
					loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[rowNo].rowSoldiers -= Mathf.CeilToInt(ratioOfPlayer*loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[rowNo].rowSoldiers);
					if (loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[rowNo].rowSoldiers < 0)
						loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[rowNo].rowSoldiers = 0;
					loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowToAttack].rowSoldiers -= Mathf.CeilToInt(ratioOfEnemy*loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowToAttack].rowSoldiers);					
					if (loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowToAttack].rowSoldiers < 0)
						loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowToAttack].rowSoldiers = 0;
					UpdateRowStats(rowToAttack , rowNo);
					rowNo++;
					if(rowNo > 2)
					{
						noOfWavesCompleted++;
					}
					isPlayerTurn = true;
					Battle();
				}
				else
				{
					Debug.Log("enemy gave turn to player");
					isPlayerTurn = true;
					rowNo++;
					if(rowNo > 2)
					{
						noOfWavesCompleted++;
					}
					Battle();
				}

			}
		}
		else
		{
		
			int finalEnemyTotalSoldiers = 0;
			int finalPlayerTotalSoldiers = 0;

			for (int i = 0; i < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations.Count; i++) {
				for (int j = 0; j < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsAttack.Count; j++) {
					finalEnemyTotalSoldiers+=(loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsSoldiers[j]);
				}
			}
			
			for (int i = 0; i < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations.Count; i++) {
				for (int j = 0; j < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsAttack.Count; j++) {
					finalPlayerTotalSoldiers+=(loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsSoldiers[j]);
				}
			}

			Debug.Log("finalEnemyTotalSoldiers = "+finalEnemyTotalSoldiers);
			Debug.Log("finalPlayerTotalSoldiers = "+finalPlayerTotalSoldiers);
			if(battleType == BattleType.QUEST_BOSS || battleType == BattleType.CHEST_BOSS)
				BattleResult.bossSoldiersLeft = finalEnemyTotalSoldiers;
			int enemyAvatar = finalEnemyTotalSoldiers * (loadingScene.Instance.battleInstance.enemyDeck.avatarDefense + loadingScene.Instance.battleInstance.enemyDeck.avatarStamina);
			int playerAvatar = finalPlayerTotalSoldiers * (loadingScene.Instance.battleInstance.playerDeck.avatarAttack + loadingScene.Instance.battleInstance.playerDeck.avatarStamina);
			if(playerAvatar > enemyAvatar)
			{
				Debug.Log("win");
				isBattleWin = true;
				//WIN..!!
			}
			else
			{
				Debug.Log("lose");
				isBattleWin = false;
				//LOSE..!!
			}
			UpdateLostSoldiersInCards();
			CalculateDamageStats ();

		}
	}

	public void CalculateDamageStats()
	{
		playerDetails.totalDamage += playerDetails.totalSkillDamage;
		playerDetails.totalDefense += playerDetails.totalSkillDefense;
		playerDetails.totalSkillDamage += playerDetails.skillDamageFromTotal;
		playerDetails.totalSkillDefense += playerDetails.skillDefenseFromTotal;

		enemyDetails.totalDamage += enemyDetails.totalSkillDamage;
		enemyDetails.totalDefense += enemyDetails.totalSkillDefense;
		enemyDetails.totalSkillDamage += enemyDetails.skillDamageFromTotal;
		enemyDetails.totalSkillDefense += enemyDetails.skillDefenseFromTotal;
	}
		
	void UpdateLostSoldiersInCards()
	{
		if (battleType == BattleType.QUEST || battleType == BattleType.QUEST_BOSS || battleType == BattleType.CHEST_EVENT || battleType == BattleType.CHEST_BOSS) {
			string cardIds = "";
			string cardSoldiers = "";
			string initialCardSoldiers = "";
			string initialAllSoldiers = CardsManager._instance.InitialCardSoldiers ();
			for (int i = 0; i < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations.Count; i++) {
				for (int j = 0; j < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardIdsInPlayerList.Count; j++) {
					int cardNoInMyCards = CardsManager._instance.PositionOfCardInList (loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations [i].cardIdsInPlayerList [j]); 
					if (!cardIds.Contains (loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations [i].cardIdsInPlayerList [j] + ",")) {
						cardIds += loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations [i].cardIdsInPlayerList [j] + ",";
						cardSoldiers += loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations [i].cardsSoldiers[j] + ",";
						initialCardSoldiers += CardsManager._instance.mycards [cardNoInMyCards].card_soldiers + ",";
					}
					CardsManager.CardParameters a = CardsManager._instance.mycards [cardNoInMyCards];
					a.card_soldiers = loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations [i].cardsSoldiers [j];
					CardsManager._instance.mycards [cardNoInMyCards] = a;
				}
			}

			int initialDeployedSoldiers = PlayerParameters._instance.myPlayerParameter.currentlyDeployedSoldiers;
			int initialAvailableSoldiers = PlayerParameters._instance.myPlayerParameter.currentlyAvailableSoldiers;
			string initialCardIds = cardIds;
			PlayerParameters._instance.SetSoldiersCount (ref cardIds , ref initialCardSoldiers , ref cardSoldiers);
			if (initialCardIds != cardIds)
				initialCardSoldiers = initialAllSoldiers;
			if (cardIds.Length > 0) {
				cardIds = cardIds.Remove (cardIds.Length - 1);
				cardSoldiers = cardSoldiers.Remove (cardSoldiers.Length - 1);
				initialCardSoldiers = initialCardSoldiers.Remove (initialCardSoldiers.Length - 1);
			}
			Dictionary<string, string> avatarParameters = new Dictionary<string, string> ();
			avatarParameters.Add ("currently_deployed_soldiers", PlayerParameters._instance.myPlayerParameter.currentlyDeployedSoldiers.ToString ());
			avatarParameters.Add ("currently_available_soldiers", PlayerParameters._instance.myPlayerParameter.currentlyAvailableSoldiers.ToString ());
			StartCoroutine (CardsManager._instance.SendCardSoldiers (cardIds, cardSoldiers, avatarParameters, isSuccess => {
				if (!isSuccess) {
					PlayerParameters._instance.myPlayerParameter.currentlyDeployedSoldiers = initialDeployedSoldiers;
					PlayerParameters._instance.myPlayerParameter.currentlyAvailableSoldiers = initialAvailableSoldiers;
					string [] cardIdArray = cardIds.Split (',');
					string [] cardSoldiersArrayInitially = initialCardSoldiers.Split (',');
					for (int i = 0; i < cardIdArray.Length; i++) {
						int cardNoInMyCards = CardsManager._instance.PositionOfCardInList (int.Parse (cardIdArray [i]));
						CardsManager.CardParameters a = CardsManager._instance.mycards [cardNoInMyCards];
						a.card_soldiers = int.Parse (cardSoldiersArrayInitially [i]);
						CardsManager._instance.mycards [cardNoInMyCards] = a;
					}
				}
			}));
		}
	}

	public void GetNoOfWaves()
	{
		int totalAp = 0;
		int totalDp = 0;
		for (int i = 0; i < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations.Count; i++) {
			for (int j = 0; j < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsAttack.Count; j++) {
				totalDp+=loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].rowDP;
			}
		}
		
		for (int i = 0; i < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations.Count; i++) {
			for (int j = 0; j < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsAttack.Count; j++) {
				totalAp+=loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].rowAP;
			}
		}
		Debug.Log ("totalAp = "+totalAp);
		Debug.Log ("totalDp = "+totalDp);
		float ratio = totalAp / (float)totalDp;
		if ( ratio  < 1)
			totalNoOfWaves = Mathf.FloorToInt(ratio * 7);
		else  if(ratio >2)
			totalNoOfWaves = 1;
		else
		{
			totalNoOfWaves = Mathf.FloorToInt((2-ratio)*7);
		}
		Debug.Log ("ratio = "+ratio);
		if (totalNoOfWaves <= 0)
			totalNoOfWaves = 1;
		else if (totalNoOfWaves > 7)
			totalNoOfWaves = 7;
	}

	bool CheckIfSkilIsTriggered(int cardPosition , bool isPlayer , int positionIdOfcardInRow , int rowNoAffected)
	{
		if (battleType == BattleType.BATTLE || battleType == BattleType.QUEST) {
			Skills.SkillRange range = Skills.SkillRange.ARMY;
			Skills.SkillEffectingParameter effectingParam = Skills.SkillEffectingParameter.AMPLIFY_DAMAGE;
			string skillStrength = "";
			int skillLevel = 1;
			SkillsManager._instance.Skill1OfCard(cardPosition ,isPlayer, ref range , ref effectingParam , ref skillStrength, ref skillLevel );
			Debug.Log ("effectingParam = "+effectingParam);	
			Debug.Log ("range = "+range);	
			float incrementValuePerSkill = SkillsManager._instance.PercentageEffectOfSkill(skillStrength , skillLevel);
			int incrementValuePerSkillInteger = 0;
			if((int)effectingParam < 7)
			{
				switch(effectingParam)
				{
				case Skills.SkillEffectingParameter.ATTACK:
					incrementValuePerSkillInteger = Mathf.CeilToInt((incrementValuePerSkill+100)/100f);
					if(isPlayer)
					{
						switch (range)
						{
						case Skills.SkillRange.CHARACTER:
							playerDetails.percentageOfSkillDamage += incrementValuePerSkill;
							Debug.Log ("old = "+loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowNoAffected].cardsAttack[positionIdOfcardInRow]);
							loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowNoAffected].cardsAttack[positionIdOfcardInRow]*=incrementValuePerSkillInteger;
							Debug.Log ("now = "+loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowNoAffected].cardsAttack[positionIdOfcardInRow]);

							UpdateRowSkillsStats (Skills.SkillEffectingParameter.ATTACK);
							return true;
							break;
						case Skills.SkillRange.ROW:
							for (int j = 0; j < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowNoAffected].cardsAttack.Count; j++) {
								loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowNoAffected].cardsAttack[j]*=incrementValuePerSkillInteger;
								playerDetails.percentageOfSkillDamage += incrementValuePerSkill;
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.ATTACK);
							return true;
							break;
						case Skills.SkillRange.ARMY:
							for(int i = 0 ; i < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations.Count ; i++){
								for (int j = 0; j < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsAttack.Count; j++) {
									loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsAttack[j]*=incrementValuePerSkillInteger;
									playerDetails.percentageOfSkillDamage += incrementValuePerSkill;
								}
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.ATTACK);
							return true;
							break;
						case Skills.SkillRange.SPECIFIC_ARMY:
							string cardClass = CardsManager._instance.mycards[cardPosition].cardClass;
							for(int i = 0 ; i < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations.Count ; i++){
								for (int j = 0; j < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsAttack.Count; j++) {
									int cardPositionOfCards = CardsManager._instance.PositionOfCardInList (loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardIdsInPlayerList[j]); 
									if(CardsManager._instance.mycards[cardPositionOfCards].cardClass == cardClass){
										loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsAttack[j]*=incrementValuePerSkillInteger;
										playerDetails.percentageOfSkillDamage += incrementValuePerSkill;
									}
								}
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.ATTACK);
							return true;
							break;
						default:
							return false;
							break;
						}
					}
					else
					{
						switch (range)
						{
						case Skills.SkillRange.CHARACTER:
							loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[rowNoAffected].cardsAttack[positionIdOfcardInRow]*=incrementValuePerSkillInteger;
							enemyDetails.percentageOfSkillDamage += incrementValuePerSkill;
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.ATTACK);
							return true;
							break;
						case Skills.SkillRange.ROW:
							for (int j = 0; j < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[rowNoAffected].cardsAttack.Count; j++) {
								loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[rowNoAffected].cardsAttack[j]*=incrementValuePerSkillInteger;
								enemyDetails.percentageOfSkillDamage += incrementValuePerSkill;
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.ATTACK);
							return true;
							break;
						case Skills.SkillRange.ARMY:
							for(int i = 0 ; i < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations.Count ; i++){
								for (int j = 0; j < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsAttack.Count; j++) {
									loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsAttack[j]*=incrementValuePerSkillInteger;
									enemyDetails.percentageOfSkillDamage += incrementValuePerSkill;
								}
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.ATTACK);
							return true;
							break;
						case Skills.SkillRange.SPECIFIC_ARMY:
							string cardClass = OpponentData._instance.enemyCards[cardPosition].cardClass;
							for(int i = 0 ; i < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations.Count ; i++){
								for (int j = 0; j < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsAttack.Count; j++) {
									int cardPositionOfCards = CardsManager._instance.PositionOfEnemyCardInList (loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardIdsInPlayerList[j]); 
									if(OpponentData._instance.enemyCards[cardPositionOfCards].cardClass == cardClass){
										loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsAttack[j]*=incrementValuePerSkillInteger;
										enemyDetails.percentageOfSkillDamage += incrementValuePerSkill;
									}
								}
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.ATTACK);
							return true;
							break;
						default:
							return false;
							break;
						}
					}

					break;
				case Skills.SkillEffectingParameter.DEFEND:
					incrementValuePerSkillInteger = Mathf.CeilToInt((incrementValuePerSkill+100)/100f);
					if(isPlayer)
					{
						switch (range)
						{
						case Skills.SkillRange.CHARACTER:
							loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowNoAffected].cardsDefense[positionIdOfcardInRow]*=incrementValuePerSkillInteger;
							playerDetails.percentageOfSkillDefense += incrementValuePerSkill;
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.DEFEND);
							return true;
							break;
						case Skills.SkillRange.ROW:
							for (int j = 0; j < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowNoAffected].cardsDefense.Count; j++) {
								loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowNoAffected].cardsDefense[j]*=incrementValuePerSkillInteger;
								playerDetails.percentageOfSkillDefense += incrementValuePerSkill;
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.DEFEND);
							return true;
							break;
						case Skills.SkillRange.ARMY:
							for(int i = 0 ; i < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations.Count ; i++){
								for (int j = 0; j < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsDefense.Count; j++) {
									loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsDefense[j]*=incrementValuePerSkillInteger;
									playerDetails.percentageOfSkillDefense += incrementValuePerSkill;
								}
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.DEFEND);
							return true;
							break;
						case Skills.SkillRange.SPECIFIC_ARMY:
							string cardClass = CardsManager._instance.mycards[cardPosition].cardClass;
							for(int i = 0 ; i < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations.Count ; i++){
								for (int j = 0; j < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsDefense.Count; j++) {
									int cardPositionOfCards = CardsManager._instance.PositionOfCardInList (loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardIdsInPlayerList[j]); 
									if(CardsManager._instance.mycards[cardPositionOfCards].cardClass == cardClass){
										loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsDefense[j]*=incrementValuePerSkillInteger;
										playerDetails.percentageOfSkillDefense += incrementValuePerSkill;
									}
								}
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.DEFEND);
							return true;
							break;
						default:
							return false;
							break;
						}
					}
					else
					{
						switch (range)
						{
						case Skills.SkillRange.CHARACTER:
							loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[rowNoAffected].cardsDefense[positionIdOfcardInRow]*=incrementValuePerSkillInteger;
							enemyDetails.percentageOfSkillDefense += incrementValuePerSkill;
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.DEFEND);
							return true;
							break;
						case Skills.SkillRange.ROW:
							for (int j = 0; j < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[rowNoAffected].cardsDefense.Count; j++) {
								loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[rowNoAffected].cardsDefense[j]*=incrementValuePerSkillInteger;
								enemyDetails.percentageOfSkillDefense += incrementValuePerSkill;
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.DEFEND);
							return true;
							break;
						case Skills.SkillRange.ARMY:
							for(int i = 0 ; i < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations.Count ; i++){
								for (int j = 0; j < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsDefense.Count; j++) {
									loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsDefense[j]*=incrementValuePerSkillInteger;
									enemyDetails.percentageOfSkillDefense += incrementValuePerSkill;
								}
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.DEFEND);
							return true;
							break;
						case Skills.SkillRange.SPECIFIC_ARMY:
							string cardClass = OpponentData._instance.enemyCards[cardPosition].cardClass;
							for(int i = 0 ; i < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations.Count ; i++){
								for (int j = 0; j < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsDefense.Count; j++) {
									int cardPositionOfCards = CardsManager._instance.PositionOfEnemyCardInList (loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardIdsInPlayerList[j]); 
									if(OpponentData._instance.enemyCards[cardPositionOfCards].cardClass == cardClass){
										loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsDefense[j]*=incrementValuePerSkillInteger;
										enemyDetails.percentageOfSkillDefense += incrementValuePerSkill;
									}
								}
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.DEFEND);
							return true;
							break;
						default:
							return false;
							break;
						}
					}

					break;
				case Skills.SkillEffectingParameter.LEADERSHIP:
					incrementValuePerSkillInteger = Mathf.CeilToInt((incrementValuePerSkill+100)/100f);
					if(isPlayer)
					{
						switch (range)
						{
						case Skills.SkillRange.CHARACTER:
//							loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowNoAffected].cardsLeadership[positionIdOfcardInRow]*=incrementValuePerSkillInteger;
							loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowNoAffected].cardsSoldiers[positionIdOfcardInRow]*=incrementValuePerSkillInteger;
							playerDetails.percentageOfSkillDamage += incrementValuePerSkill;
							playerDetails.percentageOfSkillDefense += incrementValuePerSkill;
							return true;
							break;
						case Skills.SkillRange.ROW:
							for (int j = 0; j < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowNoAffected].cardsLeadership.Count; j++) {
								loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowNoAffected].cardsSoldiers[j]*=incrementValuePerSkillInteger;
								playerDetails.percentageOfSkillDamage += incrementValuePerSkill;
								playerDetails.percentageOfSkillDefense += incrementValuePerSkill;
							}
							return true;
							break;
						case Skills.SkillRange.ARMY:
							for(int i = 0 ; i < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations.Count ; i++){
								for (int j = 0; j < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsLeadership.Count; j++) {
									loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsSoldiers[j]*=incrementValuePerSkillInteger;
									playerDetails.percentageOfSkillDamage += incrementValuePerSkill;
									playerDetails.percentageOfSkillDefense += incrementValuePerSkill;
								}
							}
							return true;
							break;
						case Skills.SkillRange.SPECIFIC_ARMY:
							string cardClass = CardsManager._instance.mycards[cardPosition].cardClass;
							for(int i = 0 ; i < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations.Count ; i++){
								for (int j = 0; j < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsLeadership.Count; j++) {
									int cardPositionOfCards = CardsManager._instance.PositionOfCardInList (loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardIdsInPlayerList[j]); 
									if(CardsManager._instance.mycards[cardPositionOfCards].cardClass == cardClass){
										loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsSoldiers[j]*=incrementValuePerSkillInteger;
										playerDetails.percentageOfSkillDamage += incrementValuePerSkill;
										playerDetails.percentageOfSkillDefense += incrementValuePerSkill;
									}
								}
							}
							return true;
							break;
						default:
							return false;
							break;
						}
					}
					else
					{
						switch (range)
						{
						case Skills.SkillRange.CHARACTER:
							loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[rowNoAffected].cardsSoldiers[positionIdOfcardInRow]*=incrementValuePerSkillInteger;
							enemyDetails.percentageOfSkillDamage += incrementValuePerSkill;
							enemyDetails.percentageOfSkillDefense += incrementValuePerSkill;
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.LEADERSHIP);
							return true;
							break;
						case Skills.SkillRange.ROW:
							for (int j = 0; j < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[rowNoAffected].cardsLeadership.Count; j++) {
								loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[rowNoAffected].cardsLeadership[j]*=incrementValuePerSkillInteger;
								enemyDetails.percentageOfSkillDamage += incrementValuePerSkill;
								enemyDetails.percentageOfSkillDefense += incrementValuePerSkill;
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.LEADERSHIP);
							return true;
							break;
						case Skills.SkillRange.ARMY:
							for(int i = 0 ; i < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations.Count ; i++){
								for (int j = 0; j < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsLeadership.Count; j++) {
									loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsSoldiers[j]*=incrementValuePerSkillInteger;
									enemyDetails.percentageOfSkillDamage += incrementValuePerSkill;
									enemyDetails.percentageOfSkillDefense += incrementValuePerSkill;
								}
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.LEADERSHIP);
							return true;
							break;
						case Skills.SkillRange.SPECIFIC_ARMY:
							string cardClass = OpponentData._instance.enemyCards[cardPosition].cardClass;
							for(int i = 0 ; i < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations.Count ; i++){
								for (int j = 0; j < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsLeadership.Count; j++) {
									int cardPositionOfCards = CardsManager._instance.PositionOfEnemyCardInList (loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardIdsInPlayerList[j]); 
									if(OpponentData._instance.enemyCards[cardPositionOfCards].cardClass == cardClass){
										loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsSoldiers[j]*=incrementValuePerSkillInteger;
										enemyDetails.percentageOfSkillDamage += incrementValuePerSkill;
										enemyDetails.percentageOfSkillDefense += incrementValuePerSkill;
									}
								}
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.LEADERSHIP);
							return true;
							break;
						default:
							return false;
							break;
						}
					}

					break;
				case Skills.SkillEffectingParameter.ATTACK_DEFEND:
					incrementValuePerSkillInteger = Mathf.CeilToInt((incrementValuePerSkill+100)/100f);
					if(isPlayer)
					{
						switch (range)
						{
						case Skills.SkillRange.CHARACTER:
							loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowNoAffected].cardsAttack[positionIdOfcardInRow]*=incrementValuePerSkillInteger;
							loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowNoAffected].cardsDefense[positionIdOfcardInRow]*=incrementValuePerSkillInteger;
							playerDetails.percentageOfSkillDamage += incrementValuePerSkill;
							playerDetails.percentageOfSkillDefense += incrementValuePerSkill;
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.ATTACK_DEFEND);
							return true;
							break;
						case Skills.SkillRange.ROW:
							for (int j = 0; j < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowNoAffected].cardsAttack.Count; j++) {
								loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowNoAffected].cardsAttack[j]*=incrementValuePerSkillInteger;
								loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowNoAffected].cardsDefense[j]*=incrementValuePerSkillInteger;
								playerDetails.percentageOfSkillDamage += incrementValuePerSkill;
								playerDetails.percentageOfSkillDefense += incrementValuePerSkill;
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.ATTACK_DEFEND);
							return true;
							break;
						case Skills.SkillRange.ARMY:
							for(int i = 0 ; i < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations.Count ; i++){
								for (int j = 0; j < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsAttack.Count; j++) {
									loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsAttack[j]*=incrementValuePerSkillInteger;
									loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsDefense[j]*=incrementValuePerSkillInteger;
									playerDetails.percentageOfSkillDamage += incrementValuePerSkill;
									playerDetails.percentageOfSkillDefense += incrementValuePerSkill;
								}
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.ATTACK_DEFEND);
							return true;
							break;
						case Skills.SkillRange.SPECIFIC_ARMY:
							string cardClass = CardsManager._instance.mycards[cardPosition].cardClass;
							for(int i = 0 ; i < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations.Count ; i++){
								for (int j = 0; j < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsAttack.Count; j++) {
									int cardPositionOfCards = CardsManager._instance.PositionOfCardInList (loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardIdsInPlayerList[j]); 
									if(CardsManager._instance.mycards[cardPositionOfCards].cardClass == cardClass){
										loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsAttack[j]*=incrementValuePerSkillInteger;
										loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsDefense[j]*=incrementValuePerSkillInteger;
										playerDetails.percentageOfSkillDamage += incrementValuePerSkill;
										playerDetails.percentageOfSkillDefense += incrementValuePerSkill;
									}
								}
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.ATTACK_DEFEND);
							return true;
							break;
						default:
							return false;
							break;
						}
					}
					else
					{
						switch (range)
						{
						case Skills.SkillRange.CHARACTER:
							loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[rowNoAffected].cardsAttack[positionIdOfcardInRow]*=incrementValuePerSkillInteger;
							loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[rowNoAffected].cardsDefense[positionIdOfcardInRow]*=incrementValuePerSkillInteger;
							enemyDetails.percentageOfSkillDamage += incrementValuePerSkill;
							enemyDetails.percentageOfSkillDefense += incrementValuePerSkill;
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.ATTACK_DEFEND);
							return true;
							break;
						case Skills.SkillRange.ROW:
							for (int j = 0; j < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[rowNoAffected].cardsAttack.Count; j++) {
								loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[rowNoAffected].cardsAttack[j]*=incrementValuePerSkillInteger;
								loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[rowNoAffected].cardsDefense[j]*=incrementValuePerSkillInteger;
								enemyDetails.percentageOfSkillDamage += incrementValuePerSkill;
								enemyDetails.percentageOfSkillDefense += incrementValuePerSkill;
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.ATTACK_DEFEND);
							return true;
							break;
						case Skills.SkillRange.ARMY:
							for(int i = 0 ; i < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations.Count ; i++){
								for (int j = 0; j < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsAttack.Count; j++) {
									loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsAttack[j]*=incrementValuePerSkillInteger;
									loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsDefense[j]*=incrementValuePerSkillInteger;
									enemyDetails.percentageOfSkillDamage += incrementValuePerSkill;
									enemyDetails.percentageOfSkillDefense += incrementValuePerSkill;
								}
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.ATTACK_DEFEND);
							return true;
							break;
						case Skills.SkillRange.SPECIFIC_ARMY:
							string cardClass = OpponentData._instance.enemyCards[cardPosition].cardClass;
							for(int i = 0 ; i < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations.Count ; i++){
								for (int j = 0; j < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsAttack.Count; j++) {
									int cardPositionOfCards = CardsManager._instance.PositionOfEnemyCardInList (loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardIdsInPlayerList[j]); 
									if(OpponentData._instance.enemyCards[cardPositionOfCards].cardClass == cardClass){
										loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsAttack[j]*=incrementValuePerSkillInteger;
										loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsDefense[j]*=incrementValuePerSkillInteger;
										enemyDetails.percentageOfSkillDamage += incrementValuePerSkill;
										enemyDetails.percentageOfSkillDefense += incrementValuePerSkill;
									}
								}
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.ATTACK_DEFEND);
							return true;
							break;
						default:
							return false;
							break;
						}
					}

					break;
				case Skills.SkillEffectingParameter.HEAL:
					if(isPlayer)
					{
						switch (range)
						{
						case Skills.SkillRange.CHARACTER:
							loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowNoAffected].cardsSoldiers[positionIdOfcardInRow] = loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowNoAffected].cardsLeadership[positionIdOfcardInRow];
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.HEAL);
							return true;
							break;
						case Skills.SkillRange.ROW:
							for (int j = 0; j < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowNoAffected].cardsLeadership.Count; j++) {
								loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowNoAffected].cardsSoldiers[j] = loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[rowNoAffected].cardsLeadership[j];
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.HEAL);
							return true;
							break;
						case Skills.SkillRange.ARMY:
							for(int i = 0 ; i < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations.Count ; i++){
								for (int j = 0; j < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsLeadership.Count; j++) {
									loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsSoldiers[j] = loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsLeadership[j];
								}
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.HEAL);
							return true;
							break;
						case Skills.SkillRange.SPECIFIC_ARMY:
							string cardClass = CardsManager._instance.mycards[cardPosition].cardClass;
							for(int i = 0 ; i < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations.Count ; i++){
								for (int j = 0; j < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsLeadership.Count; j++) {
									int cardPositionOfCards = CardsManager._instance.PositionOfCardInList (loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardIdsInPlayerList[j]); 
									if(CardsManager._instance.mycards[cardPositionOfCards].cardClass == cardClass){
										loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsSoldiers[j] = loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsLeadership[j];
									}
								}
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.HEAL);
							return true;
							break;
						default:
							return false;
							break;
						}
					}
					else
					{
						switch (range)
						{
						case Skills.SkillRange.CHARACTER:
							loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[rowNoAffected].cardsSoldiers[positionIdOfcardInRow] = loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[rowNoAffected].cardsLeadership[positionIdOfcardInRow];
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.HEAL);
							return true;
							break;
						case Skills.SkillRange.ROW:
							for (int j = 0; j < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[rowNoAffected].cardsLeadership.Count; j++) {
								loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[rowNoAffected].cardsSoldiers[j] = loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[rowNoAffected].cardsLeadership[j];
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.HEAL);
							return true;
							break;
						case Skills.SkillRange.ARMY:
							for(int i = 0 ; i < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations.Count ; i++){
								for (int j = 0; j < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsLeadership.Count; j++) {
									loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsSoldiers[j] = loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsLeadership[j];
								}
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.HEAL);
							return true;
							break;
						case Skills.SkillRange.SPECIFIC_ARMY:
							string cardClass = OpponentData._instance.enemyCards[cardPosition].cardClass;
							for(int i = 0 ; i < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations.Count ; i++){
								for (int j = 0; j < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsLeadership.Count; j++) {
									int cardPositionOfCards = CardsManager._instance.PositionOfEnemyCardInList (loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardIdsInPlayerList[j]); 
									if(OpponentData._instance.enemyCards[cardPositionOfCards].cardClass == cardClass){
										loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsSoldiers[j] = loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsLeadership[j];
									}
								}
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.HEAL);
							return true;
							break;
						default:
							return false;
							break;
						}
					}
					break;
				case Skills.SkillEffectingParameter.WEAK_RIVAL_ATTACK:
					incrementValuePerSkillInteger = Mathf.CeilToInt((100-incrementValuePerSkill)/100f);
					if(isPlayer)
					{
						switch (range)
						{
						case Skills.SkillRange.ARMY:
							for(int i = 0 ; i < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations.Count ; i++){
								for (int j = 0; j < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsAttack.Count; j++) {
									loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsAttack[j]*=incrementValuePerSkillInteger;
									playerDetails.percentageOfSkillDamage += incrementValuePerSkill;
								}
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.WEAK_RIVAL_ATTACK);
							return true;
							break;
						case Skills.SkillRange.SPECIFIC_ARMY:
							string cardClass = CardsManager._instance.mycards[cardPosition].cardClass;
							for(int i = 0 ; i < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations.Count ; i++){
								for (int j = 0; j < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsAttack.Count; j++) {
									int cardPositionOfCards = CardsManager._instance.PositionOfEnemyCardInList (loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardIdsInPlayerList[j]); 
									if(OpponentData._instance.enemyCards[cardPositionOfCards].cardClass == cardClass){
										loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsAttack[j]*=incrementValuePerSkillInteger;
										playerDetails.percentageOfSkillDamage += incrementValuePerSkill;
									}
								}
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.WEAK_RIVAL_ATTACK);
							return true;
							break;
						default:
							return false;
							break;
						}
					}
					else
					{
						switch (range)
						{
						case Skills.SkillRange.ARMY:
							for(int i = 0 ; i < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations.Count ; i++){
								for (int j = 0; j < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsAttack.Count; j++) {
									loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsAttack[j]*=incrementValuePerSkillInteger;
									enemyDetails.percentageOfSkillDamage += incrementValuePerSkill;
								}
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.WEAK_RIVAL_ATTACK);
							return true;
							break;
						case Skills.SkillRange.SPECIFIC_ARMY:
							string cardClass = OpponentData._instance.enemyCards[cardPosition].cardClass;
							for(int i = 0 ; i < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations.Count ; i++){
								for (int j = 0; j < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsAttack.Count; j++) {
									int cardPositionOfCards = CardsManager._instance.PositionOfCardInList (loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardIdsInPlayerList[j]); 
									if(CardsManager._instance.mycards[cardPositionOfCards].cardClass == cardClass){
										loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsAttack[j]*=incrementValuePerSkillInteger;
										enemyDetails.percentageOfSkillDamage += incrementValuePerSkill;
									}
								}
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.WEAK_RIVAL_ATTACK);
							return true;
							break;
						default:
							return false;
							break;
						}
					}
					UpdateRowSkillsStats (Skills.SkillEffectingParameter.WEAK_RIVAL_ATTACK);
					break;
				case Skills.SkillEffectingParameter.WEAK_RIVAL_DEFENSE:
					incrementValuePerSkillInteger = Mathf.CeilToInt((100-incrementValuePerSkill)/100f);
					if(isPlayer)
					{
						switch (range)
						{
						case Skills.SkillRange.ARMY:
							for(int i = 0 ; i < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations.Count ; i++){
								for (int j = 0; j < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsDefense.Count; j++) {
									loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsDefense[j]*=incrementValuePerSkillInteger;
									playerDetails.percentageOfSkillDamage += incrementValuePerSkill;
								}
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.WEAK_RIVAL_DEFENSE);
							return true;
							break;
						case Skills.SkillRange.SPECIFIC_ARMY:
							string cardClass = CardsManager._instance.mycards[cardPosition].cardClass;
							for(int i = 0 ; i < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations.Count ; i++){
								for (int j = 0; j < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsDefense.Count; j++) {
									int cardPositionOfCards = CardsManager._instance.PositionOfEnemyCardInList (loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardIdsInPlayerList[j]); 
									if(OpponentData._instance.enemyCards[cardPositionOfCards].cardClass == cardClass){
										loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsDefense[j]*=incrementValuePerSkillInteger;
										playerDetails.percentageOfSkillDamage += incrementValuePerSkill;
									}
								}
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.WEAK_RIVAL_DEFENSE);
							return true;
							break;
						default:
							return false;
							break;
						}
					}
					else
					{
						switch (range)
						{
						case Skills.SkillRange.ARMY:
							for(int i = 0 ; i < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations.Count ; i++){
								for (int j = 0; j < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsDefense.Count; j++) {
									int initialVal = loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsDefense[j];
									loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsDefense[j]*=incrementValuePerSkillInteger;
//									playerSkillDefVal+=loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsDefense[j]-initialVal;
									enemyDetails.percentageOfSkillDamage += incrementValuePerSkill;
								}
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.WEAK_RIVAL_DEFENSE);
							return true;
							break;
						case Skills.SkillRange.SPECIFIC_ARMY:
							string cardClass = OpponentData._instance.enemyCards[cardPosition].cardClass;
							for(int i = 0 ; i < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations.Count ; i++){
								for (int j = 0; j < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsDefense.Count; j++) {
									int cardPositionOfCards = CardsManager._instance.PositionOfCardInList (loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardIdsInPlayerList[j]); 
									if(CardsManager._instance.mycards[cardPositionOfCards].cardClass == cardClass){
										loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsDefense[j]*=incrementValuePerSkillInteger;
										enemyDetails.percentageOfSkillDamage += incrementValuePerSkill;
									}
								}
							}
							UpdateRowSkillsStats (Skills.SkillEffectingParameter.WEAK_RIVAL_DEFENSE);
							return true;
							break;
						default:
							return false;
							break;
						}
					}
					break;
				default:
					return false;
					break;
				}
			}
			else
				return false;
		}
		else
			return false;
	}

	void UpdateRowSkillsStats(Skills.SkillEffectingParameter effectingParam)
	{
		playerRowsThatCanBeAttacked.Clear ();
		enemyRowsThatCanBeAttacked.Clear ();
//		Debug.LogError ("effectingParam = "+effectingParam);
		Debug.Log ("isPlayerTurn = "+isPlayerTurn);
		for (int i = 0; i < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations.Count; i++) {
			enemyDeckTempSkills.cardRowCalculations [i].rowAP = loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations [i].rowAP;
			enemyDeckTempSkills.cardRowCalculations [i].rowDP = loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations [i].rowDP;
			loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].rowAP = 0;
			loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].rowDP = 0;
			loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].rowSoldiers = 0;

			for (int j = 0; j < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsAttack.Count; j++) {
				loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].rowAP+=(loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsAttack[j]*loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsSoldiers[j]);
				loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].rowDP+=(loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsDefense[j]*loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsSoldiers[j]);
				loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].rowSoldiers+=(loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardsSoldiers[j]);
			}
			if ((effectingParam != Skills.SkillEffectingParameter.WEAK_RIVAL_ATTACK) && (effectingParam != Skills.SkillEffectingParameter.WEAK_RIVAL_ATTACK) && !isPlayerTurn) {
				enemyDetails.totalSkillDamage += Mathf.Abs (enemyDeckTempSkills.cardRowCalculations [i].rowAP - loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations [i].rowAP);
				enemyDetails.totalSkillDefense += Mathf.Abs (enemyDeckTempSkills.cardRowCalculations [i].rowDP - loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations [i].rowDP);
			} else if(isPlayerTurn) {
				if(effectingParam == Skills.SkillEffectingParameter.WEAK_RIVAL_ATTACK)
					playerDetails.totalSkillDamage += Mathf.Abs (enemyDeckTempSkills.cardRowCalculations [i].rowAP - loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations [i].rowAP);
				else if(effectingParam == Skills.SkillEffectingParameter.WEAK_RIVAL_DEFENSE)
					playerDetails.totalSkillDefense += Mathf.Abs (enemyDeckTempSkills.cardRowCalculations [i].rowDP - loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations [i].rowDP);
			}
			if(loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].rowSoldiers > 0)
			{
				enemyRowsThatCanBeAttacked.Add (i);
			}
		}

		for (int i = 0; i < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations.Count; i++) {
			playerDeckTempSkills.cardRowCalculations [i].rowAP = loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations [i].rowAP;
			playerDeckTempSkills.cardRowCalculations [i].rowDP = loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations [i].rowDP;

			loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].rowAP = 0;
			loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].rowDP = 0;
			loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].rowSoldiers = 0;

			for (int j = 0; j < loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsAttack.Count; j++) {
				loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].rowAP+=(loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsAttack[j]*loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsSoldiers[j]);
				loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].rowDP+=(loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsDefense[j]*loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsSoldiers[j]);
				loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].rowSoldiers+=(loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].cardsSoldiers[j]);
			}
			if ((effectingParam != Skills.SkillEffectingParameter.WEAK_RIVAL_ATTACK) && (effectingParam != Skills.SkillEffectingParameter.WEAK_RIVAL_ATTACK) && isPlayerTurn) {
				playerDetails.totalSkillDamage += Mathf.Abs (playerDeckTempSkills.cardRowCalculations [i].rowAP - loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations [i].rowAP);
				playerDetails.totalSkillDefense += Mathf.Abs (playerDeckTempSkills.cardRowCalculations [i].rowDP - loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations [i].rowDP);
//				Debug.Log ("player rowAp temp= "+playerDeckTempSkills.cardRowCalculations [i].rowAP );
//				Debug.Log ("player rowDp temp= "+playerDeckTempSkills.cardRowCalculations [i].rowDP );
//				Debug.Log ("player rowAp final= "+loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations [i].rowAP );
//				Debug.Log ("player rowDp final= "+loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations [i].rowDP );
			}
			else if(!isPlayerTurn) {
				if(effectingParam == Skills.SkillEffectingParameter.WEAK_RIVAL_ATTACK)
					enemyDetails.totalSkillDamage += Mathf.Abs (playerDeckTempSkills.cardRowCalculations [i].rowAP - loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations [i].rowAP);
				else if(effectingParam == Skills.SkillEffectingParameter.WEAK_RIVAL_DEFENSE)
					enemyDetails.totalSkillDefense += Mathf.Abs (playerDeckTempSkills.cardRowCalculations [i].rowDP - loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations [i].rowDP);
			}
			if(loadingScene.Instance.battleInstance.playerDeck.cardRowCalculations[i].rowSoldiers > 0)
			{
				playerRowsThatCanBeAttacked.Add (i);
			}
		}

	}


}
