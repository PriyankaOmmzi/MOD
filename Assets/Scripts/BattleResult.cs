using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BattleResult : MonoBehaviour
{
	public GameObject setting;
	public GameObject menu;
	public GameObject chatBtn;
	public GameObject battleBtn;
	public GameObject itemsBtn;
	public GameObject menuBtn;
	public GameObject bottomBtns;
	public GameObject refillBtn;
	public Button[] bottomsButtons;
	bool isMneuActive=false;
	public GameObject refillDialog;

	public Text avatarLevel;
	public Text playerName;
	public Text avatarAttack;
	public Text avatarDefense;
	public Text avatarLeadership;
	public Text avatarRank;

	public GameObject battleInfo;
	public GameObject battleDetails;
	public GameObject battleInfoBtn;
	public GameObject battleDetailsBtn;
	public GameObject battleWin;
	public GameObject battleLost;

	public Text detailsPlayerDamage;
	public Text detailsPlayerSkillDamage;
	public Text detailsPlayerDefense;
	public Text detailsPlayerSkillDefense;

	public Text detailsOpponentDamage;
	public Text detailsOpponentSkillDamage;
	public Text detailsOpponentDefense;
	public Text detailsOpponentSkillDefense;

	public Text totalPoints;
	public Text BonusPoints;
	public Text orbsPoints;
	public Image avatarImage;
	public bool fromQuest = true;

	public static int bossSoldiersLeft;
	public static BattleResult _instance;

	// Use this for initialization
	void Start () 
	{
		_instance = this;
		loadingScene.Instance.playerProfilePanel.SetActive(false);
		setting.SetActive(false);
		if (fromQuest) {
			bottomBtns.SetActive (false);
		}
		else
			bottomBtns.SetActive (true);
	}

	public static bool requireSendingResult;
	void OnEnable()
	{
		avatarLevel.text = "Lvl "+(PlayerParameters._instance.myPlayerParameter.avatar_level+1);
		playerName.text = PlayerDataParse._instance.playersParam.userName;
		avatarAttack.text = PlayerParameters._instance.myPlayerParameter.avatar_attack.ToString ();
		avatarDefense.text = PlayerParameters._instance.myPlayerParameter.avatar_defense.ToString ();
		avatarLeadership.text = PlayerParameters._instance.myPlayerParameter.avatar_leadership.ToString ();
		if (BattleLogic._instance.battleType == BattleLogic.BattleType.CHEST_EVENT || BattleLogic._instance.battleType == BattleLogic.BattleType.CHEST_BOSS) {
			totalPoints.color = Color.black; 
			BonusPoints.color = Color.black; 

		} else {
			totalPoints.color = new Color (141 / 255f, 141 / 255f, 141 / 255f); 
			BonusPoints.color = new Color (141 / 255f, 141 / 255f, 141 / 255f); 
		}
		orbsPoints.text = (PlayerParameters._instance.myPlayerParameter.orb - BattleLogic._instance.orbsTosubtract) + "/" + PlayerParameters._instance.myPlayerParameter.maxOrb;
		if (PlayerParameters._instance.myPlayerParameter.avatar_no == 1) 
		{
			avatarImage.sprite = loadingScene.Instance.playerSprite[0];
		}
		else if (PlayerParameters._instance.myPlayerParameter.avatar_no == 2) 
		{
			avatarImage.sprite = loadingScene.Instance.playerSprite[1];
		}
		else if (PlayerParameters._instance.myPlayerParameter.avatar_no == 3)
		{
			avatarImage.sprite = loadingScene.Instance.playerSprite[2];
		}

		detailsPlayerDamage.text = BattleLogic._instance.playerDetails.totalDamage.ToString();
		detailsPlayerSkillDamage.text = BattleLogic._instance.playerDetails.totalSkillDamage.ToString();
		detailsPlayerDefense.text = BattleLogic._instance.playerDetails.totalDefense.ToString();
		detailsPlayerSkillDefense.text = BattleLogic._instance.playerDetails.totalSkillDefense.ToString();
		
		detailsOpponentDamage.text = BattleLogic._instance.enemyDetails.totalDamage.ToString();
		detailsOpponentSkillDamage.text = BattleLogic._instance.enemyDetails.totalSkillDamage.ToString();
		detailsOpponentDefense.text = BattleLogic._instance.enemyDetails.totalDefense.ToString();
		detailsOpponentSkillDefense.text = BattleLogic._instance.enemyDetails.totalSkillDefense.ToString();

		BattleInfo ();
		if(requireSendingResult)
			CalculateResultToSend ();

		requireSendingResult = false;
	}

	int initialDeployedSoldiers = 0;
	int initialAvailableSoldiers = 0;
	string cardIds = "";
	string initialCardSoldiers = "";

	void CalculateResultToSend()
	{
		if (BattleLogic._instance.battleType == BattleLogic.BattleType.BATTLE) {
			cardIds = "";
			string cardSoldiers = "";
			initialCardSoldiers = "";

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
				cardIds = cardIds.Remove (cardIds.Length - 1);        //card_no_in_players_list
				cardSoldiers = cardSoldiers.Remove (cardSoldiers.Length - 1); //card_soldiers
				initialCardSoldiers = initialCardSoldiers.Remove (initialCardSoldiers.Length - 1);
			}


			string enemyCardIds = "";
			string enemyCardSoldiers = "";
			string enemyInitialCardSoldiers = "";

			int soldiersToReduce = 0;

			for (int i = 0; i < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations.Count; i++) {
				for (int j = 0; j < loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations[i].cardIdsInPlayerList.Count; j++) {
					int cardNoInMyCards = CardsManager._instance.PositionOfEnemyCardInList (loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations [i].cardIdsInPlayerList [j]); 
					if (!enemyCardIds.Contains (loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations [i].cardIdsInPlayerList [j] + ",")) {
						enemyCardIds += loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations [i].cardIdsInPlayerList [j] + ",";
						enemyCardSoldiers += loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations [i].cardsSoldiers[j] + ",";
						enemyInitialCardSoldiers += OpponentData._instance.enemyCards [cardNoInMyCards].card_soldiers + ",";
						soldiersToReduce += (OpponentData._instance.enemyCards [cardNoInMyCards].card_soldiers - loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations [i].cardsSoldiers [j]);
					}
					CardsManager.CardParameters a = OpponentData._instance.enemyCards [cardNoInMyCards];
					a.card_soldiers = loadingScene.Instance.battleInstance.enemyDeck.cardRowCalculations [i].cardsSoldiers [j];
					OpponentData._instance.enemyCards [cardNoInMyCards] = a;
				}
			}
			if (enemyCardIds.Length > 0) {
				enemyCardIds = enemyCardIds.Remove (enemyCardIds.Length - 1);       
				enemyCardSoldiers = enemyCardSoldiers.Remove (enemyCardSoldiers.Length - 1); 
				enemyInitialCardSoldiers = enemyInitialCardSoldiers.Remove (enemyInitialCardSoldiers.Length - 1);
			}

			Debug.Log ("enemy soldiersToReduce = "+soldiersToReduce);
			OpponentData._instance.myPlayerParameter.currentlyDeployedSoldiers -= soldiersToReduce;

			PlayerParameters._instance.myPlayerParameter.orb -= BattleLogic._instance.orbsTosubtract;
			Debug.Log ("BattleLogic._instance.orbsTosubtract = "+BattleLogic._instance.orbsTosubtract);
			Debug.Log ("orb final  = "+PlayerParameters._instance.myPlayerParameter.orb);
			if(BattleLogic.isBattleWin)
			{
				PlayerParameters._instance.myPlayerParameter.wheat += BattleLogic._instance.foodLoot;
				PlayerParameters._instance.myPlayerParameter.gold += BattleLogic._instance.goldLoot;
				OpponentData._instance.myPlayerParameter.wheat -= BattleLogic._instance.foodLoot;
				OpponentData._instance.myPlayerParameter.gold -= BattleLogic._instance.goldLoot;

			}
			else
			{
				PlayerParameters._instance.myPlayerParameter.wheat -= BattleLogic._instance.playerFoodLoot;
				PlayerParameters._instance.myPlayerParameter.gold -= BattleLogic._instance.playerGoldLoot;
				OpponentData._instance.myPlayerParameter.wheat += BattleLogic._instance.playerFoodLoot;
				OpponentData._instance.myPlayerParameter.gold += BattleLogic._instance.playerGoldLoot;
			}
			//	doGetUpdatePlayerData&players=[{"user_id":"VhvGK3gtrB4=","gems":"71000","gold":"1000","wheat":"1000",
			//"card_no_in_players_list":"636,554","card_level":"9,9"},{"user_id":"TQtFe41ejZk=",
			//"gems":"2000","gold":"2000","wheat":"2000","card_no_in_players_list":"335,488","card_level":"3,3"}]
			string valueToSend = EncodeData( cardIds , cardSoldiers , enemyCardIds ,  enemyCardSoldiers );
			StartCoroutine (SendBattleResult(valueToSend));

		}
	}

	IEnumerator SendBattleResult(string valueToSend)
	{
		loadingScene.Instance.loader.SetActive (true);
		WWWForm wform = new WWWForm ();
		wform.AddField ("tag", "doGetUpdatePlayerData");
		wform.AddField ("players", valueToSend);
		WWW wwwBattleResult = new WWW (loadingScene.Instance.baseUrl, wform);
		yield return wwwBattleResult;
		Debug.Log (wwwBattleResult.text);
		if (wwwBattleResult.error == null && !wwwBattleResult.text.Contains ("error_msg")) {
			//DONE!
		} else {
			loadingScene.Instance.popupFromServer.ShowPopup ("Could not save data!");
			PlayerParameters._instance.myPlayerParameter.currentlyDeployedSoldiers = initialDeployedSoldiers;
			PlayerParameters._instance.myPlayerParameter.currentlyAvailableSoldiers = initialAvailableSoldiers;
			if (battleLayout.itemToSend > 0) {
					PlayerParameters._instance.myPlayerParameter.artefacts [battleLayout.itemToSend-1]--;
			}
			string [] cardIdArray = cardIds.Split (',');
			string [] cardSoldiersArrayInitially = initialCardSoldiers.Split (',');
			for (int i = 0; i < cardIdArray.Length; i++) {
				int cardNoInMyCards = CardsManager._instance.PositionOfCardInList (int.Parse (cardIdArray [i]));
				CardsManager.CardParameters a = CardsManager._instance.mycards [cardNoInMyCards];
				a.card_soldiers = int.Parse (cardSoldiersArrayInitially [i]);
				CardsManager._instance.mycards [cardNoInMyCards] = a;
			}
			if(BattleLogic.isBattleWin)
			{
				PlayerParameters._instance.myPlayerParameter.wheat -= BattleLogic._instance.foodLoot;
				PlayerParameters._instance.myPlayerParameter.gold -= BattleLogic._instance.goldLoot;

			}
			else
			{
				PlayerParameters._instance.myPlayerParameter.wheat += BattleLogic._instance.playerFoodLoot;
				PlayerParameters._instance.myPlayerParameter.gold += BattleLogic._instance.playerGoldLoot;
			}
		}
		loadingScene.Instance.loader.SetActive (false);
	}


	public string EncodeData(string playerCardIds ,string playerCardSoldiers , string enemyCardIds , string enemyCardSoldiers )
	{
		JSONObject playerArray = new JSONObject(JSONObject.Type.ARRAY);

		JSONObject playerData = new JSONObject(JSONObject.Type.OBJECT);
		playerData.AddField ("user_id",PlayerDataParse._instance.playersParam.userId);
		playerData.AddField ("wheat",PlayerParameters._instance.myPlayerParameter.wheat.ToString());
		playerData.AddField ("wheat_time",TimeManager._instance.GetCurrentServerTime().ToString());
		playerData.AddField ("orb",PlayerParameters._instance.myPlayerParameter.orb.ToString());
		playerData.AddField ("orb_time",TimeManager._instance.GetCurrentServerTime().ToString());
		playerData.AddField ("gold",PlayerParameters._instance.myPlayerParameter.gold.ToString());
		playerData.AddField ("gold_time",TimeManager._instance.GetCurrentServerTime().ToString());
		playerData.AddField ("currently_deployed_soldiers", PlayerParameters._instance.myPlayerParameter.currentlyDeployedSoldiers.ToString ());
		playerData.AddField ("currently_available_soldiers", PlayerParameters._instance.myPlayerParameter.currentlyAvailableSoldiers.ToString ());
		if (battleLayout.itemToSend > 0) {
			string itemSet = "";
			for (int i = 0; i < PlayerParameters._instance.myPlayerParameter.artefacts.Length; i++) {
				if ((battleLayout.itemToSend-1) == i) {
					PlayerParameters._instance.myPlayerParameter.artefacts [i]++;
				}
				itemSet += (PlayerParameters._instance.myPlayerParameter.artefacts [i] + ",");
			}
			itemSet = itemSet.Remove (itemSet.Length-1);
			playerData.AddField ("item_set",itemSet);
		}
		playerData.AddField ("card_no_in_players_list",playerCardIds);
		playerData.AddField ("card_soldiers",playerCardSoldiers);
		playerArray.Add(playerData);


		JSONObject enemyData = new JSONObject(JSONObject.Type.OBJECT);
		enemyData.AddField ("user_id",PlayerDataParse._instance.ID(OpponentData._instance.playerID.ToString()));
		enemyData.AddField ("wheat",OpponentData._instance.myPlayerParameter.wheat.ToString());
		enemyData.AddField ("wheat_time",TimeManager._instance.GetCurrentServerTime().ToString());
		enemyData.AddField ("gold",OpponentData._instance.myPlayerParameter.gold.ToString());
		enemyData.AddField ("gold_time",TimeManager._instance.GetCurrentServerTime().ToString());
		enemyData.AddField ("currently_deployed_soldiers", OpponentData._instance.myPlayerParameter.currentlyDeployedSoldiers.ToString ());
		enemyData.AddField ("card_no_in_players_list",enemyCardIds);
		enemyData.AddField ("card_soldiers",enemyCardSoldiers);
		if (battleLayout.itemToSend > 0) {
			string itemSetEnemy = "";
			for (int i = 0; i < OpponentData._instance.myPlayerParameter.artefacts.Length; i++) {
				if ((battleLayout.itemToSend - 1) == i) {
					OpponentData._instance.myPlayerParameter.artefacts [i]--;
				}
				itemSetEnemy += (OpponentData._instance.myPlayerParameter.artefacts [i] + ",");
			}
			itemSetEnemy = itemSetEnemy.Remove (itemSetEnemy.Length - 1);
			enemyData.AddField ("item_set", itemSetEnemy);
		}
		playerArray.Add(enemyData);

		string encodedString = playerArray.ToString();
		Debug.Log ("encodedString = "+encodedString);
		return encodedString;
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
	public void Refill()
	{
		refillDialog.SetActive(true);
		
	}
	public void RefillYes()
	{
		loadingScene.Instance.loader.SetActive (true);
		PlayerParameters._instance.RefillOrbs ((isSuccess, msgString) => {
			loadingScene.Instance.popupFromServer.ShowPopup(msgString);
			refillDialog.SetActive(false);
			if(isSuccess)
				orbsPoints.text = (PlayerParameters._instance.myPlayerParameter.maxOrb) + "/" + PlayerParameters._instance.myPlayerParameter.maxOrb;
		});
		
		
	}
	public void Items()
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
		loadingScene.Instance.battleScene();
	}

	public void backFromResult()
	{

		if (BattleLogic._instance.battleType == BattleLogic.BattleType.CHEST_BOSS || BattleLogic._instance.battleType == BattleLogic.BattleType.CHEST_EVENT) {
			loadingScene.Instance.loads [20].SetActive (true);
			loadingScene.Instance.scenes.RemoveAt (loadingScene.Instance.scenes.Count - 1);
			if (PlayerPrefs.GetString ("win") == "no") {
				if (BattleLogic._instance.battleType == BattleLogic.BattleType.CHEST_BOSS)
					EventQuest.instance.BossUncleared (bossSoldiersLeft.ToString ());
				else
					EventQuest.instance.enableWrningText ();
			} else
				EventQuest.instance.enableGreenWarning ();
			transform.parent.gameObject.SetActive (false);
		}
		else if (BattleLogic._instance.battleType != BattleLogic.BattleType.BATTLE) {
			loadingScene.Instance.loads [4].SetActive (true);
			loadingScene.Instance.scenes.RemoveAt (loadingScene.Instance.scenes.Count - 1);
			Debug.Log ("isWin = " + PlayerPrefs.GetString ("win"));
			if (PlayerPrefs.GetString ("win") == "no") {

				if (BattleLogic._instance.battleType == BattleLogic.BattleType.QUEST_BOSS)
					questNew.instance.BossUncleared (bossSoldiersLeft.ToString ());
				else
					questNew.instance.enableWrningText ();
			} else
				questNew.instance.enableGreenWarning ();

			transform.parent.gameObject.SetActive (false);
		} else {
			loadingScene.Instance.BattleOpponentSelection ();
		}
	}

	public void detail()
	{
		battleDetailsBtn.GetComponent<Image>().color = new Color32(158,148,148,255);
		battleInfo.SetActive (false);
		battleInfoBtn.GetComponent<Image>().color = new Color32(255,231,231,255);
		battleDetails.SetActive (true);
	}
	
	public void BattleInfo()
	{
		battleDetailsBtn.GetComponent<Image>().color = new Color32(255,231,231,255);
		battleInfo.SetActive (true);
		battleInfoBtn.GetComponent<Image>().color = new Color32(158,148,148,255);
		battleDetails.SetActive (false);
	}

	public void logOut()
	{
		onClickSettingExit();
		PlayerPrefs.SetString("logout","yes");
		loadingScene.Instance.main();
	}

	public void chatScene()
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
		loadingScene.Instance.chat ();
	}
	
	public void empire()
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
		loadingScene.Instance.empire ();
	}
	public void quest()
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
		loadingScene.Instance.quest ();
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
	public void community()
	{
		loadingScene.Instance.community();
	}

	// Update is called once per frame
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
		loadingScene.Instance.main();
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
	public void trade()
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
		loadingScene.Instance.trade();
	}

	public void battleAgain()
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
		BattleOpponentSelection.wentFromBattleResult = true;
		loadingScene.Instance.battleScene();
	}
	public void selectItem()
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
		loadingScene.Instance.battleItemSelectionScene();
	}
	public void cardCollections()
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
		loadingScene.Instance.BattleOpponentSelection();
	}
	public void invecntory()
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
		loadingScene.Instance.inventory();
	}
	public void replay()
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
		loadingScene.Instance.battleScene();
	}


	public void shop()
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
		loadingScene.Instance.shop();
	}
	
	public void menuPopUp()
	{
		loadingScene.Instance.objectToOpenMenu = this.gameObject;
		menu.SetActive(true);
	}
	public void exitMenu()
	{
		menu.SetActive(false);
	}
	public void battle()
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
		loadingScene.Instance.battleScene();

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

}
