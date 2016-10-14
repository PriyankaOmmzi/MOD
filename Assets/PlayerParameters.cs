using UnityEngine;
using System.Collections;
using MiniJSON;
using System.Linq;
using System.Collections.Generic;
using System;
public class PlayerParameters : MonoBehaviour {

	public PlayerParameterValues myPlayerParameter;
	public static PlayerParameters _instance;
	public System.Int64 []avatarReqdExpForLevelUp;
	public string emailAddress;
	// Use this for initialization
	void Awake () {
		_instance = this;
	}

	int timeForInvokeGold = 0;
	int timeForInvokeWheat = 0;

	public void SavePlayerParameters(IDictionary playerParamaters , bool fromLogin , System.Action<bool>  callBack)
	{
//		Debug.Log(playerParamaters["avatar_exp"]);
//		Debug.Log(playerParamaters["gems"]);
//		Debug.Log(playerParamaters["gold"]);
//		Debug.Log(playerParamaters["gold_time"]);
//		Debug.Log(playerParamaters["wheat"]);
//		Debug.Log(playerParamaters["wheat_time"]);
//		Debug.Log(playerParamaters["card_count"]);
//		Debug.Log(playerParamaters["stamina"]);
//		Debug.Log(playerParamaters["stamina_time"]);
//		Debug.Log(playerParamaters["orb"]);
//		Debug.Log(playerParamaters["orb_time"]);
//		Debug.Log(playerParamaters["avatar_no"]);
//		Debug.Log(playerParamaters["peace_treaty"]);
//		Debug.Log(playerParamaters["peace_treaty_start_time"].ToString ());
//		Debug.Log(playerParamaters["peace_treaty_active_time"]);
//		Debug.Log(playerParamaters["avatar_stats_pool"]);
//		Debug.Log(playerParamaters["avatar_defense"]);
//		Debug.Log(playerParamaters["avatar_attack"]);
//		Debug.Log(playerParamaters["avatar_leadership"]);
//		Debug.Log(playerParamaters["max_ally"]);
//		Debug.Log(playerParamaters["ally_count"]);
//		Debug.Log(playerParamaters["registration_date_time"].ToString ());
//		Debug.Log(playerParamaters["notification_on"]);
//		Debug.Log(playerParamaters["QuestCardFormation"]);
//		Debug.Log(playerParamaters["BattleCardFormation"]);
//		Debug.Log(playerParamaters["FriendList"]);
//		Debug.Log(playerParamaters["BlackList"]);
//		Debug.Log(playerParamaters["captivesList"]);
//		Debug.Log(playerParamaters["researchItems"]);
//		Debug.Log(playerParamaters["interrogationList"]);
//		Debug.Log(playerParamaters["ongoingResearch"]);
//		Debug.Log(playerParamaters["event_formation"]);
//		Debug.Log(playerParamaters["membership_no"]);
//		Debug.Log(playerParamaters["time_of_membership_no"]);
//		Debug.Log(playerParamaters["first_time_login"]);
//		Debug.Log(playerParamaters["item_set"]);

		myPlayerParameter.loginTime = TimeManager._instance.GetCurrentServerTime ();
		if (playerParamaters ["guild_name"] != null) {
			myPlayerParameter.guildID = playerParamaters ["guild_id"].ToString ();
			myPlayerParameter.guildName = playerParamaters ["guild_name"].ToString ();
			myPlayerParameter.guildPrefix = playerParamaters ["guild_prefix"].ToString ();
			myPlayerParameter.guildLevel = int.Parse (playerParamaters ["guild_level"].ToString ());
		} else {
			myPlayerParameter.guildLevel = 0;
			myPlayerParameter.guildID = myPlayerParameter.guildName = myPlayerParameter.guildPrefix = "";
		}
		Debug.Log (myPlayerParameter.guildQuitTime);
		if (playerParamaters ["quit_time"].ToString () != "0000-00-00 00:00:00") {
			myPlayerParameter.guildQuitTime = Convert.ToDateTime (playerParamaters ["quit_time"].ToString ());
		} else {
			myPlayerParameter.guildQuitTime = Convert.ToDateTime ("01/01/0001 00:00:00");
		}
		Debug.Log (myPlayerParameter.guildQuitTime);

		int.TryParse(playerParamaters ["total_trades"].ToString (), out myPlayerParameter.totalPostedTrades);
		int.TryParse (playerParamaters ["bazar_tickets"].ToString (), out myPlayerParameter.bazaarTickets);
	
		if(playerParamaters["avatar_exp"] != null)
			Int64.TryParse (playerParamaters["avatar_exp"].ToString (), out myPlayerParameter.avatar_exp);
		if(playerParamaters["gems"] != null)
			int.TryParse (playerParamaters["gems"].ToString (), out myPlayerParameter.gems);
		if(playerParamaters["gold"] != null)
			int.TryParse (playerParamaters["gold"].ToString (), out myPlayerParameter.gold);
		if (playerParamaters["gold_time"] != null && playerParamaters ["gold_time"].ToString () != "0000-00-00 00:00:00" && !string.IsNullOrEmpty(playerParamaters ["gold_time"].ToString ()) && playerParamaters ["gold_time"].ToString () != "01/01/0001 00:00:00") {
			myPlayerParameter.gold_time = System.Convert.ToDateTime (playerParamaters ["gold_time"].ToString ());
		}
		if(playerParamaters["wheat"] != null)
			int.TryParse (playerParamaters["wheat"].ToString (), out myPlayerParameter.wheat);
		if (playerParamaters["wheat_time"] != null && playerParamaters ["wheat_time"].ToString () != "0000-00-00 00:00:00" && !string.IsNullOrEmpty (playerParamaters ["wheat_time"].ToString ()) && playerParamaters ["wheat_time"].ToString () != "01/01/0001 00:00:00") {
			myPlayerParameter.wheat_time = System.Convert.ToDateTime (playerParamaters ["wheat_time"].ToString ());
			Debug.Log("myPlayerParameter.wheat_time   = "+myPlayerParameter.wheat_time);
		}
		if(playerParamaters["card_count"] != null)
			int.TryParse (playerParamaters["card_count"].ToString (), out myPlayerParameter.card_count);
		if(playerParamaters["FriendList"] != null)
			int.TryParse (playerParamaters["FriendList"].ToString (), out myPlayerParameter.FriendList);
		if(playerParamaters["stamina"] != null)
			int.TryParse (playerParamaters["stamina"].ToString (), out myPlayerParameter.stamina);
		if(playerParamaters["stamina_time"] != null && playerParamaters["stamina_time"].ToString () != "0000-00-00 00:00:00"  && !string.IsNullOrEmpty(playerParamaters ["stamina_time"].ToString ()) && playerParamaters ["stamina_time"].ToString () != "01/01/0001 00:00:00")
			myPlayerParameter.stamina_time = System.Convert.ToDateTime (playerParamaters["stamina_time"].ToString ());
		if(playerParamaters["orb"] != null)
			int.TryParse (playerParamaters["orb"].ToString (), out myPlayerParameter.orb);

		if (playerParamaters["item_set"] != null && !string.IsNullOrEmpty (playerParamaters ["item_set"].ToString())) {
			string[] itmSets = playerParamaters ["item_set"].ToString ().Split (',');
			for (int i = 0; i < itmSets.Length; i++) {
				int.TryParse (itmSets[i],  out myPlayerParameter.artefacts[i]);
			}
		}
			

		//Shivam 
		if(playerParamaters["membership_no"] != null)
			int.TryParse (playerParamaters["membership_no"].ToString (), out myPlayerParameter.membership_no);
		if(playerParamaters["day_of_membership_reward_fetched"] != null)
			int.TryParse (playerParamaters["day_of_membership_reward_fetched"].ToString (), out myPlayerParameter.day_of_membership_reward_fetched);
		if (playerParamaters ["time_of_membership_no"] != null && playerParamaters ["time_of_membership_no"].ToString () != "0000-00-00 00:00:00" && !string.IsNullOrEmpty (playerParamaters ["time_of_membership_no"].ToString ()) && playerParamaters ["time_of_membership_no"].ToString () != "01/01/0001 00:00:00") {
			myPlayerParameter.time_of_membership_no = System.Convert.ToDateTime (playerParamaters ["time_of_membership_no"].ToString ());
			TimeSpan timeSpan_membership_over = TimeManager._instance.GetCurrentServerTime () - myPlayerParameter.time_of_membership_no;
			Debug.Log ("TimeCheck "+timeSpan_membership_over.Days);
			MembershipRewardManager._instance.daysLeft = timeSpan_membership_over.Days;
			loadingScene.Instance.tempMembership_time = timeSpan_membership_over;
		}
		MembershipRewardManager._instance.FetchRewardDay = myPlayerParameter.day_of_membership_reward_fetched;
		// Shivam end

		if(playerParamaters["no_of_peace_treaties"] != null && !string.IsNullOrEmpty (playerParamaters["no_of_peace_treaties"].ToString ()))
			int.TryParse (playerParamaters["no_of_peace_treaties"].ToString (), out myPlayerParameter.no_of_peace_treaties);
		if(playerParamaters["signal_fire"] != null && !string.IsNullOrEmpty (playerParamaters["signal_fire"].ToString ()))
			int.TryParse (playerParamaters["signal_fire"].ToString (), out myPlayerParameter.signal_fire);
		if(playerParamaters["attack_potion"] != null && !string.IsNullOrEmpty (playerParamaters["attack_potion"].ToString ()))
			int.TryParse (playerParamaters["attack_potion"].ToString (), out myPlayerParameter.attack_potion);
		if(playerParamaters["stamina_potion"] != null && !string.IsNullOrEmpty (playerParamaters["stamina_potion"].ToString ()))
			int.TryParse (playerParamaters["stamina_potion"].ToString (), out myPlayerParameter.stamina_potion);

		if (playerParamaters["orb_time"] != null && playerParamaters ["orb_time"].ToString () != "0000-00-00 00:00:00"  && !string.IsNullOrEmpty(playerParamaters ["orb_time"].ToString ()) && playerParamaters ["orb_time"].ToString () != "01/01/0001 00:00:00") {
			myPlayerParameter.orb_time = System.Convert.ToDateTime (playerParamaters ["orb_time"].ToString ());
			System.TimeSpan timeSpanOgGold = TimeManager._instance.GetCurrentServerTime () - myPlayerParameter.gold_time;
			myPlayerParameter.gold+= (int)timeSpanOgGold.TotalHours*EmpireManager._instance.goldMine.finalValue1[EmpireManager._instance.goldMine.currentLevel];
			timeForInvokeGold = timeSpanOgGold.Minutes*60 + timeSpanOgGold.Seconds;
			
			System.TimeSpan timeSpanOgWheat = TimeManager._instance.GetCurrentServerTime () - myPlayerParameter.wheat_time;
			myPlayerParameter.wheat+= (int)timeSpanOgWheat.TotalHours*EmpireManager._instance.barn.finalValue1[EmpireManager._instance.barn.currentLevel];
			timeForInvokeWheat = timeSpanOgWheat.Minutes*60 + timeSpanOgWheat.Seconds;

		} else {
			myPlayerParameter.orb_time = TimeManager._instance.GetCurrentServerTime ();
			myPlayerParameter.orb = 5;
			myPlayerParameter.gold_time = TimeManager._instance.GetCurrentServerTime ();
			myPlayerParameter.gold = 1500;
			myPlayerParameter.wheat_time = TimeManager._instance.GetCurrentServerTime ();
			myPlayerParameter.wheat = 1500;
			myPlayerParameter.stamina_time = TimeManager._instance.GetCurrentServerTime ();
			myPlayerParameter.stamina = 20;
			myPlayerParameter.max_stamina = 20;
			myPlayerParameter.maxOrb = 5;
			myPlayerParameter.no_of_peace_treaties = 5;
			myPlayerParameter.stamina_potion = 10;
			myPlayerParameter.attack_potion = 10;
			myPlayerParameter.gems = 100;
			timeForInvokeGold = 3600;
			timeForInvokeWheat  = 3600;
		}
//		if (playerParamaters ["QuestCardFormation"] != null) {
//			if (!string.IsNullOrEmpty (playerParamaters ["QuestCardFormation"].ToString ())) {
//				Debug.Log (playerParamaters ["QuestCardFormation"].ToString ());
//				IDictionary questData = (IDictionary)Json.Deserialize (playerParamaters ["QuestCardFormation"].ToString ());
//				for (int k = 1; k <=3; k++) {
//					IList deck = (IList)questData ["deck" + k];
//					for (int i = 0; i < deck.Count; i++) {
//						IDictionary deckDic = (IDictionary)deck [i];
//						if (!string.IsNullOrEmpty (deckDic ["row" + i].ToString ())) {
//							string [] row = deckDic ["row" + i].ToString ().Split (',');
//							for (int j = 0; j < row.Length; j++) {
//								loadingScene.Instance.myQuestFormation.cardDecks [k - 1].cardRows [i].cardIdsForRow.Add (int.Parse (row [j]));
//								loadingScene.Instance.myQuestFormation.cardDecks [k - 1].noOfCardsSelected++;
//							}
//						}
//					
//					}
//				}
//
//			}
//		}

		if (playerParamaters ["captivesList"] != null) {
			if (!string.IsNullOrEmpty (playerParamaters ["captivesList"].ToString ())) {
				int.TryParse (playerParamaters["captivesList"].ToString (), out  myPlayerParameter.questFormationDeck);

			}
		}
		if (playerParamaters ["interrogationList"] != null) {
			if (!string.IsNullOrEmpty (playerParamaters ["interrogationList"].ToString ())) {
				int.TryParse (playerParamaters["interrogationList"].ToString (), out  myPlayerParameter.battleFormationDeck);

			}
		}
//		if (playerParamaters ["event_formation"] != null) {
//			if (!string.IsNullOrEmpty (playerParamaters ["event_formation"].ToString ())) {
//				Debug.Log (playerParamaters ["event_formation"].ToString ());
//				IDictionary questData = (IDictionary)Json.Deserialize (playerParamaters ["event_formation"].ToString ());
//				for (int k = 1; k <=3; k++) {
//					IList deck = (IList)questData ["deck" + k];
//					for (int i = 0; i < deck.Count; i++) {
//						IDictionary deckDic = (IDictionary)deck [i];
//						if (!string.IsNullOrEmpty (deckDic ["row" + i].ToString ())) {
//							string [] row = deckDic ["row" + i].ToString ().Split (',');
//							for (int j = 0; j < row.Length; j++) {
//								loadingScene.Instance.eventQuestFormation.cardDecks [k - 1].cardRows [i].cardIdsForRow.Add (int.Parse (row [j]));
//								loadingScene.Instance.eventQuestFormation.cardDecks [k - 1].noOfCardsSelected++;
//							}
//						}
//
//					}
//				}
//
//			}
//		}
		if (playerParamaters ["BattleCardFormation"] != null) {
			if (!string.IsNullOrEmpty (playerParamaters ["BattleCardFormation"].ToString ())) {
				Debug.Log (playerParamaters ["BattleCardFormation"].ToString ());
				IDictionary questData = (IDictionary)Json.Deserialize (playerParamaters ["BattleCardFormation"].ToString ());
				for (int k = 1; k <=3; k++) {
					IList deck = (IList)questData ["deck" + k];
					for (int i = 0; i < deck.Count; i++) {
						IDictionary deckDic = (IDictionary)deck [i];
						if (!string.IsNullOrEmpty (deckDic ["row" + i].ToString ())) {
							string [] row = deckDic ["row" + i].ToString ().Split (',');
							for (int j = 0; j < row.Length; j++) {
								loadingScene.Instance.myBattleFormation.cardDecks [k - 1].cardRows [i].cardIdsForRow.Add (int.Parse (row [j]));
								loadingScene.Instance.myBattleFormation.cardDecks [k - 1].noOfCardsSelected++;
							}
						}

					}
				}

			}
		}
		if(playerParamaters["avatar_no"] != null)
			int.TryParse (playerParamaters ["avatar_no"].ToString (), out myPlayerParameter.avatar_no);

		if(playerParamaters["avatar_level"] != null)
			int.TryParse (playerParamaters["avatar_level"].ToString (), out myPlayerParameter.avatar_level);

		int peaceTreatyAtServer = 0;
		if(playerParamaters["peace_treaty"] != null)
			int.TryParse (playerParamaters["peace_treaty"].ToString (), out peaceTreatyAtServer);
		if(playerParamaters["peace_treaty_start_time"] != null && playerParamaters["peace_treaty_start_time"].ToString () != "0000-00-00 00:00:00"  && !string.IsNullOrEmpty(playerParamaters ["peace_treaty_start_time"].ToString ()) && playerParamaters ["peace_treaty_start_time"].ToString () != "01/01/0001 00:00:00")
		{
			Debug.Log(playerParamaters["peace_treaty_start_time"].ToString ());
			myPlayerParameter.peace_treaty_start_time = System.Convert.ToDateTime (playerParamaters["peace_treaty_start_time"].ToString ());
			Debug.Log("peacetreatyStart = "+myPlayerParameter.peace_treaty_start_time);
		}
		if(playerParamaters["peace_treaty_active_time"] != null)
			int.TryParse (playerParamaters["peace_treaty_active_time"].ToString (), out myPlayerParameter.peace_treaty_active_time);


		System.DateTime TimeOfPeaceTreaty = myPlayerParameter.peace_treaty_start_time;
		Debug.Log("TimeOfPeaceTreaty = "+TimeOfPeaceTreaty);

		System.TimeSpan differenceTimeForPeace = TimeManager._instance.GetCurrentServerTime () - TimeOfPeaceTreaty;
//		Debug.Log("differenceTimeForPeace = "+differenceTimeForPeace);
//		Debug.Log("differenceTimeForPeace.Days = "+differenceTimeForPeace.Days);

		if (peaceTreatyAtServer == 1 && myPlayerParameter.peace_treaty_active_time > 0 && (differenceTimeForPeace.TotalHours <= myPlayerParameter.peace_treaty_active_time)) {
			Debug.Log("current server time= "+TimeManager._instance.GetCurrentServerTime ());
			myPlayerParameter.peace_treaty = 1;
			newMenuScene.instance.peaceTreaty.SetActive (true);
			StartCoroutine (PeaceEndTime((float)differenceTimeForPeace.TotalSeconds));

		} else {
			myPlayerParameter.peace_treaty = 0;
			Debug.Log ("myPlayerParameter.peace_treaty = "+myPlayerParameter.peace_treaty);
			myPlayerParameter.peace_treaty_active_time = 0;

			Debug.Log("no change");
		}



		newMenuScene.instance.ChangeDefaultPlayerSprite ();
		if(playerParamaters["avatar_stats_pool"] != null)
			int.TryParse (playerParamaters["avatar_stats_pool"].ToString (), out myPlayerParameter.avatar_stats_pool);
		if(playerParamaters["avatar_defense"] != null)
			int.TryParse (playerParamaters["avatar_defense"].ToString (), out myPlayerParameter.avatar_defense);
		if(playerParamaters["avatar_attack"] != null)
			int.TryParse (playerParamaters["avatar_attack"].ToString (), out myPlayerParameter.avatar_attack);
		if(playerParamaters["avatar_leadership"] != null)	
			int.TryParse (playerParamaters["avatar_leadership"].ToString (), out myPlayerParameter.avatar_leadership);
		if(playerParamaters["max_ally"] != null)
			int.TryParse (playerParamaters["max_ally"].ToString (), out myPlayerParameter.max_ally);
		if(playerParamaters["ally_count"] != null)
			int.TryParse (playerParamaters["ally_count"].ToString (), out myPlayerParameter.ally_count);
		if(playerParamaters["currently_available_soldiers"] != null)
			int.TryParse (playerParamaters["currently_available_soldiers"].ToString (), out myPlayerParameter.currentlyAvailableSoldiers);
		if(playerParamaters["currently_deployed_soldiers"] != null)
			int.TryParse (playerParamaters["currently_deployed_soldiers"].ToString (), out myPlayerParameter.currentlyDeployedSoldiers);
		if(playerParamaters["unlocked_stamina_potion"] != null)
			int.TryParse (playerParamaters["unlocked_stamina_potion"].ToString (), out myPlayerParameter.unlocked_stamina_potion);
		if(playerParamaters["unlocked_attack_potion"] != null)
			int.TryParse (playerParamaters["unlocked_attack_potion"].ToString (), out myPlayerParameter.unlocked_attack_potion);
		if(playerParamaters["dragon_eggs"] != null)
			int.TryParse (playerParamaters["dragon_eggs"].ToString (), out myPlayerParameter.dragon_eggs);
		if(playerParamaters["dragon_coins"] != null)
			int.TryParse (playerParamaters["dragon_coins"].ToString (), out myPlayerParameter.dragon_coins);

		if(playerParamaters["registration_date_time"] != null && playerParamaters["registration_date_time"].ToString () != "0000-00-00 00:00:00" && !string.IsNullOrEmpty (playerParamaters["registration_date_time"].ToString ())  && playerParamaters ["registration_date_time"].ToString () != "01/01/0001 00:00:00")
			myPlayerParameter.registration_date_time = System.Convert.ToDateTime (playerParamaters["registration_date_time"].ToString ());

		if(playerParamaters["notification_on"] != null)
			int.TryParse (playerParamaters["notification_on"].ToString (), out myPlayerParameter.notification_on);

		Dictionary<string, string> avatarParameters = new Dictionary<string, string> ();
		avatarParameters.Add ("gems", myPlayerParameter.gems.ToString ());
		avatarParameters.Add ("gold" , myPlayerParameter.gold.ToString ());
		avatarParameters.Add ("gold_time" , myPlayerParameter.gold_time.ToString ());
		avatarParameters.Add ("wheat" , myPlayerParameter.wheat.ToString ());
		avatarParameters.Add ("wheat_time" , myPlayerParameter.wheat_time.ToString ());
		avatarParameters.Add ("stamina" , myPlayerParameter.stamina.ToString ());
		avatarParameters.Add ("orb_time" , myPlayerParameter.orb_time.ToString ());
		avatarParameters.Add ("orb" , myPlayerParameter.orb.ToString ());
		avatarParameters.Add ("peace_treaty" , myPlayerParameter.peace_treaty.ToString ());
		avatarParameters.Add ("no_of_peace_treaties" , myPlayerParameter.no_of_peace_treaties.ToString ());
		avatarParameters.Add ("peace_treaty_active_time" , myPlayerParameter.peace_treaty_active_time.ToString ());
		avatarParameters.Add ("stamina_potion" , myPlayerParameter.stamina_potion.ToString ());
		avatarParameters.Add ("attack_potion" , myPlayerParameter.attack_potion.ToString ());
		avatarParameters.Add ("avatar_level" , myPlayerParameter.avatar_level.ToString());
		avatarParameters.Add ("membership_no", myPlayerParameter.membership_no.ToString ());
		//Shivam
		avatarParameters.Add ("time_of_membership_no", myPlayerParameter.time_of_membership_no.ToString ());
		avatarParameters.Add ("day_of_membership_reward_fetched", myPlayerParameter.day_of_membership_reward_fetched.ToString ());
//		avatarParameters.Add ("BattleCardFormation", "");


		//Shivam end

		myPlayerParameter.gold = 9000000;
		myPlayerParameter.wheat = 9000000;
		myPlayerParameter.gems = 9000000;
		myPlayerParameter.stamina = 20000;
		myPlayerParameter.signal_fire = 200;
		if(myPlayerParameter.max_stamina == 0)
			myPlayerParameter.max_stamina = 20000;
		InvokeRepeating ("goldProduction", timeForInvokeGold,3600);
		InvokeRepeating ("wheatProduction", timeForInvokeWheat, 3600);
		GetMaxOrb ();
		GetMaxBattleCost ();
		myPlayerParameter.orb = myPlayerParameter.maxOrb;
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected)
				StartCoroutine (SendPlayerParameters (avatarParameters ,fromLogin, callBack));
			else {
				newMenuScene.instance.gameStopPopup.ShowPopup ("Network Error! You cannot proceed");
				callBack(false);
			}
		});
		newMenuScene.instance.fetchDetails ();

	}

	public void ShowPeaceTreatyBird(float timeOfPeaceTreatyInSeconds)
	{
		myPlayerParameter.peace_treaty = 1;
		newMenuScene.instance.peaceTreaty.SetActive (true);
		StartCoroutine (PeaceEndTime(timeOfPeaceTreatyInSeconds));
	}

	public void goldProduction()
	{
		if (EmpireManager._instance.goldMine.secondaryCardNo > 0) 
		{
			myPlayerParameter.gold += EmpireManager._instance.goldMine.finalValue1 [EmpireManager._instance.goldMine.currentLevel*2];
		}
		else 
		{
			myPlayerParameter.gold += EmpireManager._instance.goldMine.finalValue1 [EmpireManager._instance.goldMine.currentLevel];
			
		}
		myPlayerParameter.gold_time = TimeManager._instance.GetCurrentServerTime ();
	}
	public void wheatProduction()
	{
		if (EmpireManager._instance.barn.secondaryCardNo > 0) 
		{
			myPlayerParameter.wheat += EmpireManager._instance.barn.finalValue1 [EmpireManager._instance.barn.currentLevel*2];
		}
		else 
		{
			myPlayerParameter.wheat += EmpireManager._instance.barn.finalValue1 [EmpireManager._instance.barn.currentLevel];
			
		}
		myPlayerParameter.wheat_time = TimeManager._instance.GetCurrentServerTime ();
	}


	//http://ommzi.com/new_app/index.php?tag=insertAllPlayer&user_id=62&device_id=741760E3-1F28-5C0A-8F1A-096CC5878FB4&gems=34&gold=12&avatar_exp=123
	public IEnumerator SendPlayerParameters(Dictionary<string, string> avatarParametersFormal  , bool fromLogin, System.Action <bool> callback)
	{
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag" ,"insertAllPlayer");
		wwwForm.AddField ("user_id" , PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id" , SystemInfo.deviceUniqueIdentifier );
		for (int i = 0; i < avatarParametersFormal.Count; i++) {
			wwwForm.AddField(avatarParametersFormal.Keys.ElementAt(i) , avatarParametersFormal.Values.ElementAt(i));
//			Debug.Log (avatarParametersFormal.Keys.ElementAt(i) +","+avatarParametersFormal.Values.ElementAt(i));
		}
		WWW wwwLogin = new WWW(loadingScene.Instance.baseUrl , wwwForm);
		yield return wwwLogin;
		//{"success":1,"msg":"All players params:","Player_data":{"id":"6","user_id":"62","avatar_exp":"123","gems":"34","gold":"12","gold_time":"0000-00-00 00:00:00","wheat":"43","wheat_time":"0000-00-00 00:00:00","card_count":"","stamina":"","stamina_time":"0000-00-00 00:00:00","orb":"","orb_time":"0000-00-00 00:00:00","avatar_no":"1","peace_treaty":"","peace_treaty_start_time":"0000-00-00 00:00:00","peace_treaty_active_time":"","avatar_stats_pool":"","avatar_defense":"34","avatar_attack":"","avatar_leadership":"","max_ally":"","ally_count":"","registration_date_time":"","notification_on":"","QuestCardFormation":"","BattleCardFormation":"","FriendList":"","BlackList":"","captivesList":"","researchItems":"","interrogationList":"","ongoingResearch":"","event_formation":"","membership_no":"","time_of_membership_no":"","first_time_login":"","created":"2016-07-01 00:51:30","modified":"2016-07-06 05:13:53","device_id":""}}
		//{"success":0,"error_msg":"Invalid details!"}
		if (wwwLogin.error == null) {
			Debug.Log(wwwLogin.text);
			IDictionary resultDict = (IDictionary)Json.Deserialize (wwwLogin.text);
//			Debug.Log("TEXT = "+wwwLogin.text);
			if(wwwLogin.text.Contains ("error_msg"))
			{
				if(wwwLogin.text.Contains("Invalid details!") && !fromLogin)
					newMenuScene.instance.popupFromServer.ShowPopup ("Logging you out... :P");
				else if(!fromLogin)
					newMenuScene.instance.gameStopPopup.ShowPopup ("Try Again Later!");
				callback(false);
			}
			else
			{
				if(!fromLogin && myPlayerParameter.avatar_no > 0)
				{
					StartCoroutine(CardsManager._instance.GetPlayerCards (gotCards => {
						if(!gotCards)
						{
							Debug.Log("could not get cards......");
							callback(false);
						}
						else
						{
							callback(true);
//							tutorialScript.instance.callMenuTutorial ();

							newMenuScene.instance.loader.SetActive(false);
						}
					}));
				}
				else
				{
//					tutorialScript.instance.callMenuTutorial ();

					newMenuScene.instance.loader.SetActive(false);
					callback(true);
				}
			}
			
		} else {
			newMenuScene.instance.gameStopPopup.ShowPopup ("Network Error!");
			callback(false);
		}

		
	}

	public IEnumerator SendPlayerParameters(Dictionary<string, string> avatarParametersFormal , System.Action <bool> callback)
	{
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag" ,"insertAllPlayer");
		wwwForm.AddField ("user_id" , PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id" , SystemInfo.deviceUniqueIdentifier );
		for (int i = 0; i < avatarParametersFormal.Count; i++) {
			wwwForm.AddField(avatarParametersFormal.Keys.ElementAt(i) , avatarParametersFormal.Values.ElementAt(i));
		}
		WWW wwwLogin = new WWW(loadingScene.Instance.baseUrl , wwwForm);
		yield return wwwLogin;
		Debug.Log ("text on sending ====   "+wwwLogin.text);
		if (wwwLogin.error == null) {
			IDictionary resultDict = (IDictionary)Json.Deserialize (wwwLogin.text);
			if(wwwLogin.text.Contains ("error_msg"))
			{
				if(callback != null)
					callback(false);
			}
			else
			{
				if(callback != null)
					callback(true);
			}
			
		} else {
			newMenuScene.instance.gameStopPopup.ShowPopup ("Network Error!");
			if(callback != null)
				callback(false);
		}
		
		
	}


	public void RefillOrbs(Action<bool,string> callBack)
	{
		if (myPlayerParameter.attack_potion > 0 || myPlayerParameter.unlocked_attack_potion > 0) {
			Dictionary<string, string> avatarParameters = new Dictionary<string, string> ();
			avatarParameters.Add ("orb",myPlayerParameter.maxOrb.ToString ());
			avatarParameters.Add ("orb_time", TimeManager._instance.GetCurrentServerTime().ToString ());
			if(myPlayerParameter.attack_potion > 0)
				avatarParameters.Add ("attack_potion",(myPlayerParameter.attack_potion-1).ToString ());
			else
				avatarParameters.Add ("unlocked_attack_potion",(myPlayerParameter.unlocked_attack_potion-1).ToString ());
			StartCoroutine (SendPlayerParameters (avatarParameters, isSuccess => {
				if(isSuccess)
				{
					myPlayerParameter.orb = myPlayerParameter.maxOrb;
					myPlayerParameter.orb_time = TimeManager._instance.GetCurrentServerTime();
					if(myPlayerParameter.attack_potion > 0)
						myPlayerParameter.attack_potion--;
					else
						myPlayerParameter.unlocked_attack_potion--;
					callBack(true, "");
				}
				else
				{
					callBack(false, "Could not rfill at this time!");
				}
			}));
		}
		else
		{
			callBack(false, "You do not have any attack potion to refill!");
		}
	}


	IEnumerator PeaceEndTime(float secondsToWait)
	{
		yield return new WaitForSeconds (secondsToWait);
		myPlayerParameter.peace_treaty = 0;
		newMenuScene.instance.peaceTreaty.SetActive (false);
		//Remove Bird
	}

	void GetMaxOrb()
	{
		if (myPlayerParameter.avatar_level < 10) {
			myPlayerParameter.maxOrb = 5;
		} else if (myPlayerParameter.avatar_level < 25) {
			myPlayerParameter.maxOrb = 6;
		} else if (myPlayerParameter.avatar_level < 50) {
			myPlayerParameter.maxOrb = 7;
		} else if (myPlayerParameter.avatar_level < 75) {
			myPlayerParameter.maxOrb = 8;
		} else if (myPlayerParameter.avatar_level < 99) {
			myPlayerParameter.maxOrb = 9;
		} else {
			myPlayerParameter.maxOrb = 10;
		}
	}

	public void IncrementTradeCount() {
		myPlayerParameter.totalPostedTrades += 1;
		myPlayerParameter.bazaarTickets -= 1;
		StartCoroutine (UpdateTradeCount ());
	}
	
	IEnumerator UpdateTradeCount() {
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "insertAllPlayer");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("total_trades", myPlayerParameter.totalPostedTrades);
		wwwForm.AddField ("bazar_tickets", myPlayerParameter.bazaarTickets);
		WWW trade = new WWW(loadingScene.Instance.baseUrl, wwwForm);
		yield return trade;
		Debug.Log (trade.text);
	}

	void GetMaxBattleCost()
	{
		if (myPlayerParameter.avatar_level < 70) {
			myPlayerParameter.maxBattleCost = 30+(myPlayerParameter.avatar_level)*2;
		}  else {
			myPlayerParameter.maxOrb = 168+(myPlayerParameter.avatar_level-69);
		}
	}

	public void SetSoldiersCount(ref string cardIds, ref string cardSoldiersInitially, ref string cardSoldiersFinal)
	{
		myPlayerParameter.currentlyDeployedSoldiers = 0;
		for(int i = 0 ; i < CardsManager._instance.mycards.Count ; i++)
		{
			myPlayerParameter.currentlyDeployedSoldiers+=CardsManager._instance.mycards[i].card_soldiers;
		}
		if (myPlayerParameter.currentlyDeployedSoldiers < myPlayerParameter.maxDeployedSoldiers && myPlayerParameter.currentlyAvailableSoldiers > 0) {

			int amountOfLessDeployed = myPlayerParameter.maxDeployedSoldiers - myPlayerParameter.currentlyDeployedSoldiers;
			if(myPlayerParameter.currentlyAvailableSoldiers - amountOfLessDeployed > 0 )
			{
				myPlayerParameter.currentlyAvailableSoldiers-=amountOfLessDeployed;
				myPlayerParameter.currentlyDeployedSoldiers = myPlayerParameter.maxDeployedSoldiers;
			}
			else
			{
				myPlayerParameter.currentlyAvailableSoldiers = 0;
				myPlayerParameter.currentlyDeployedSoldiers += myPlayerParameter.currentlyAvailableSoldiers;

			}
			cardIds = "";
			cardSoldiersInitially = "";
			cardSoldiersFinal = "";
			CardsManager._instance.DistributeDeloyedSoldiersToCards (ref cardIds, ref cardSoldiersInitially, ref cardSoldiersFinal);
		}
	}
}



[System.Serializable]
public class PlayerParameterValues
{
	public Int64 avatar_exp;
	public int gems;
	public int 	gold;
	public System.DateTime gold_time;
	public int wheat;
	public System.DateTime wheat_time;
	public int card_count;
	public int stamina;
	public System.DateTime stamina_time;
	public int orb;
	public System.DateTime orb_time;
	public int avatar_no;
	public int user_id;
	public int peace_treaty;
	public System.DateTime peace_treaty_start_time;
	public int peace_treaty_active_time;
	public int avatar_stats_pool;
	public int avatar_defense;
	public int avatar_attack;
	public int avatar_leadership;
	public int max_ally;
	public int ally_count;
	public System.DateTime registration_date_time;
	public System.DateTime loginTime;
	public int notification_on;
	public int membership_no;
	public System.DateTime time_of_membership_no;
	public int signal_fire;
	public int attack_potion;
	public int stamina_potion;
	public int no_of_peace_treaties;
	public int first_time_login;
	public int currentlyDeployedSoldiers;
	public int currentlyAvailableSoldiers;
	public int maxDeployedSoldiers;
	public int avatar_level;
	public int day_of_membership_reward_fetched;
	//public System.DateTime timespan_membership_over;
	public int maxBattleCost;

	public int []artefacts;

	public int maxOrb;
	public int max_stamina;
	public int unlocked_stamina_potion;
	public int unlocked_attack_potion;
	public int dragon_eggs;
	public int dragon_coins;
	public int FriendList;
	//	FriendList;
	//	BlackList;
	//	CaptivesList;
	//	ResearchItems;
	//	InterrogationList;
	//	OngoingResearch;
	//	event_formation;  // FOR CHEST FORMATION
	public int totalPostedTrades;//total_trades
	public int bazaarTickets;//bazar_tickets
	public string guildID;
	public string guildName;
	public string guildPrefix;
	public int guildLevel;
	public DateTime guildQuitTime;

	public int questFormationDeck;
	public int battleFormationDeck;

}


