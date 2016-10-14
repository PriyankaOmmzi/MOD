using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

namespace Trading {

	public class TradeDeletion : MonoBehaviour {

		public static TradeDeletion instance;
		[SerializeField]
		BazaarContent bazaarContent;
		[SerializeField]
		GameObject loadingPopup;
		[SerializeField]
		Text loadingPopupContent;
		GameObject tradeToBeCancelled;
		[SerializeField]
		GameObject warning;
		[SerializeField]
		Text warningContent;
		IDictionary tradeData;
		List<string> itemNames;
		List<int> itemQty;
		int cardsAdded;
		int cardsBuyerDeleted;
		int newCardsCount;

		void Awake() {
			itemNames = new List<string> ();
			itemQty = new List<int> ();
			instance = this;
		}

		public IEnumerator CanTradeBeCancelled(GameObject trade) {
			Debug.Log("cancelling trade");
			loadingPopupContent.text = "Cancelling Trade...";
			loadingPopup.SetActive(true);
			tradeToBeCancelled = trade;
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "getPlayerDailyCancelled");
			wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
			wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
			WWW dailyCancelledTrades = new WWW(loadingScene.Instance.baseUrl, wwwForm);
			yield return dailyCancelledTrades;
			Debug.Log ("Daily Cancelled Trades: " + dailyCancelledTrades.text);
			if (dailyCancelledTrades.text.Contains ("\"success\":1")) {
				if (int.Parse (((IDictionary)Json.Deserialize (dailyCancelledTrades.text)) ["daily_cancelled"].ToString ()) <= TradeConstants.maxDailyCancelledTrades) {
					StartCoroutine(SetStatusTrade());
				} else {
					loadingPopup.SetActive (false);
					warningContent.text = "Trade can't be cancelled. You have already cancelled 10 trades today.";
					warning.SetActive (true);
					Debug.Log ("trade can't be cancelled");
				}
			} else {
				loadingPopup.SetActive (false);
				warningContent.text = "Trade couldn't be cancelled. Please try again.";
				warning.SetActive (true);
				Debug.Log ("trade couldn't be cancelled. Please try again.");
			}
		}

		IEnumerator SetStatusTrade() {
			string iD = tradeToBeCancelled.GetComponent<TradeData> ().data ["id"].ToString ();
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "userAllTradesFunction");
			wwwForm.AddField("trade_id_fkey", iD);
			wwwForm.AddField ("user_id" , PlayerDataParse._instance.playersParam.userId);
			wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
			wwwForm.AddField ("trade_type", "EXPIRED");
			WWW trade = new WWW (loadingScene.Instance.baseUrl, wwwForm);
			yield return trade;
			Debug.Log (trade.text);
			if (trade.text.Contains ("\"success\":1")) {
				DeleteTrade ();
			}
			else {
				StartCoroutine(SetStatusTrade());
			}
		}

		public void DeleteTrade(GameObject trade = null) {
			if (trade == null) {
				tradeData = tradeToBeCancelled.GetComponent<TradeData> ().data;
			} else {
				loadingPopupContent.text = "Deleting Trade...";
				loadingPopup.SetActive (true);
				tradeData = trade.GetComponent<TradeData> ().data;
				Debug.Log(tradeData);
			}
			if (tradeData ["bidder_id"].ToString () == "") {
				Debug.Log ("trade did not complete");
				if (int.Parse (tradeData ["rarity"].ToString ()) > 0) {
					StartCoroutine (UnlockCard ());
				} else {
					StartCoroutine (UpdateItemsOnTradeCancellation ());
				}
			} else {
				Debug.Log ("trade completed" + tradeData ["bidder_id"].ToString ());
				string requestedItemsString = tradeData ["requested_items"].ToString ();
				string[] requestedItems = requestedItemsString.Split (',');
				string itemCountString = tradeData ["no_of_items"].ToString ();
				string[] itemCount = itemCountString.Split (',');
				string requestedMaxLevelString = tradeData ["requested_rarity"].ToString ();
				string[] requestedMaxLevels = requestedMaxLevelString.Split (',');
				int temp = 0;
				itemNames.Clear ();
				itemQty.Clear ();
				while (temp < requestedItems.Length) {
					if (itemCount [temp] != "0") {//is item
						itemNames.Add (requestedItems [temp]);
						itemQty.Add (int.Parse (itemCount [temp]));
					}
					temp++;
				}
				newCardsCount = requestedItems.Length - itemNames.Count;
				if (newCardsCount > 0) {
					cardsAdded = 0;
					cardsBuyerDeleted = 0;
					string[] buyerData = tradeData ["bidder_id"].ToString ().Split (',');
					string buyerID = buyerData [0];
					int index = 1;
					while (index < buyerData.Length) {
						StartCoroutine (GetCardData (buyerID, buyerData[index]));
						index++;
					}
				} else {
					StartCoroutine (UpdateItemsOnTradeCompletion ());
				}
			}
		}

		IEnumerator UnlockCard() {
			Debug.Log("unlocking locked card");
			int cardIndex = CardsManager._instance.mycards.FindIndex (a => a.card_id_in_playerList == int.Parse (tradeData ["item_of_trade"].ToString ()));
			Debug.Log (cardIndex);
			CardsManager.CardParameters tempCard = CardsManager._instance.mycards [cardIndex];
			tempCard.isLocked = false;
			CardsManager._instance.mycards [cardIndex] = tempCard;
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "updatePlayerCardsIsLocked");
			wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
			wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
			wwwForm.AddField ("card_no_in_players_list", tradeData ["item_of_trade"].ToString ());
			wwwForm.AddField ("is_locked", "false");
			WWW unlockCard = new WWW(loadingScene.Instance.baseUrl, wwwForm);
			yield return unlockCard;
			if (unlockCard.text.Contains ("\"success\":1")) {
				StartCoroutine (DeleteTradeCoroutine ());
			} else {
				StartCoroutine (UnlockCard ());
			}
		}

		IEnumerator UpdateItemsOnTradeCancellation() {
			Debug.Log("updating items on cancellation");
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag" ,"insertAllPlayer");
			wwwForm.AddField ("user_id" , PlayerDataParse._instance.playersParam.userId);
			wwwForm.AddField ("device_id" , SystemInfo.deviceUniqueIdentifier);
			int qty = int.Parse (tradeData ["item_of_trade"].ToString ().Split (',') [1]);
			switch (tradeData["item_name"].ToString()) {
			case "stamina_potion":
				PlayerParameters._instance.myPlayerParameter.unlocked_stamina_potion += qty;
				wwwForm.AddField("unlocked_stamina_potion", PlayerParameters._instance.myPlayerParameter.unlocked_stamina_potion);
				break;
			case "attack_potion":
				PlayerParameters._instance.myPlayerParameter.unlocked_attack_potion += qty;
				wwwForm.AddField("unlocked_attack_potion", PlayerParameters._instance.myPlayerParameter.unlocked_attack_potion);
				break;
			case "signal_fire":
				PlayerParameters._instance.myPlayerParameter.signal_fire += qty;
				wwwForm.AddField("signal_fire", PlayerParameters._instance.myPlayerParameter.signal_fire);
				break;
			case "high_security":
				PlayerParameters._instance.myPlayerParameter.no_of_peace_treaties += qty;
				wwwForm.AddField("no_of_peace_treaties", PlayerParameters._instance.myPlayerParameter.no_of_peace_treaties);
				break;
			case "gold":
				PlayerParameters._instance.myPlayerParameter.gold += qty;
				wwwForm.AddField("gold", PlayerParameters._instance.myPlayerParameter.gold);
				break;
			case "food":
				PlayerParameters._instance.myPlayerParameter.wheat += qty;
				wwwForm.AddField("wheat", PlayerParameters._instance.myPlayerParameter.wheat);
				break;
			}
			WWW updateItems = new WWW(loadingScene.Instance.baseUrl, wwwForm);
			yield return updateItems;
			if (updateItems.text.Contains ("\"success\":1")) {
				StartCoroutine (DeleteTradeCoroutine ());
			} else {
				StartCoroutine (UpdateItemsOnTradeCancellation ());
			}
		}

		/*IEnumerator GetCardID() {
			string cardName = newCards [newCardIndex].card_name;
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "getCardId");
			wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
			wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
			wwwForm.AddField ("card_name", cardName);
			WWW cardData = new WWW (loadingScene.Instance.baseUrl, wwwForm);
			yield return cardData;
			if (cardData.text.Contains ("success\":1")) {
				CardsManager.CardParameters tempCard = newCards [newCardIndex];
				tempCard.card_id_in_database = int.Parse ((((Json.Deserialize (cardData.text) as IDictionary) ["card_data"] as IList) [0] as IDictionary) ["Card_id"].ToString ());
				newCards[newCardIndex] = tempCard;
				newCardIndex++;
				if(newCardIndex<newCards.Count) {
					StartCoroutine(GetCardID());
				}
				else {
					string cardIDs = "";
					foreach(CardsManager.CardParameters card in newCards) {
						if(cardIDs != "") {
							cardIDs += ",";
						}
						cardIDs += card.card_id_in_database;
					}
					Debug.Log(cardIDs);
					StartCoroutine(GetCardData(cardIDs));
				}
			} else {
				StartCoroutine(GetCardID());
			}

		}

		IEnumerator GetCardData(string cardIDs) {
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "getCardData");
			wwwForm.AddField ("card_id", cardIDs);
			WWW wwwCards = new WWW(loadingScene.Instance.baseUrl , wwwForm);
			yield return wwwCards;
			//{"success":1,"msg":"Card detail","Params":[["id 170","class Andras","name Quintilianus","images\/card11f_logo.jpg","174","113","rarity Common","ss1Weak","ss2Weak","sn1Fierce Outrage","sn2Fierce Outrage","s12","s20","118",""]],"Subcard_details":[{"card_id":"170","type":"Perfect","leadership":"130","attack":"191","defense":"124"},{"card_id":"170","type":"Warrior","leadership":"118","attack":"191","defense":"113"},{"card_id":"170","type":"Defend","leadership":"118","attack":"174","defense":"124"},{"card_id":"170","type":"Leader","leadership":"130","attack":"174","defense":"113"},{"card_id":"170","type":"Average","leadership":"118","attack":"174","defense":"113"}]}
			Debug.LogError ("Data2 " + wwwCards.text);
			if (wwwCards.text.Contains ("success\":1")) {
				IList cardsData = (Json.Deserialize (wwwCards.text) as IDictionary) ["Params"] as IList;
				newCardIndex = 0;
				Debug.Log(cardsData.ToString());
				foreach (IList cardData in cardsData) {
					CardsManager.CardParameters tempCard = newCards [newCardIndex];
					tempCard.cardClass = cardData [1].ToString ();
					tempCard.rarity = cardData [6].ToString ();
					tempCard.skill_1 = cardData [7].ToString ();
					tempCard.skill_1_Strength = cardData [9].ToString ();
					tempCard.skill_2 = cardData [8].ToString ();
					tempCard.skill_2_Strength = cardData [10].ToString ();
					newCards [newCardIndex] = tempCard;
					newCardIndex++;
				}
				cardsAdded = 0;
				foreach (CardsManager.CardParameters newCard in newCards) {
					StartCoroutine (AddNewCard (newCard));
				}
			} else {
				StartCoroutine(GetCardData(cardIDs));
			}
		}

		IEnumerator AddNewCard(CardsManager.CardParameters newCard) {
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "doAddUpdatePlayerCards");
			wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
			wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
			wwwForm.AddField ("card_name", newCard.card_name);
			wwwForm.AddField ("max_level", newCard.max_level);
			wwwForm.AddField ("card_id_in_database", newCard.card_id_in_database);
			wwwForm.AddField ("class", newCard.cardClass);
			wwwForm.AddField ("rarity", newCard.rarity);
			wwwForm.AddField ("skill_1", newCard.skill_1);
			wwwForm.AddField ("skill_2", newCard.skill_2);
			wwwForm.AddField ("skill_1_Strength", newCard.skill_1_Strength);
			wwwForm.AddField ("skill_2_Strength", newCard.skill_2_Strength);
			WWW wwwCards = new WWW (loadingScene.Instance.baseUrl, wwwForm);
			yield return wwwCards;
			Debug.Log (wwwCards.text);
			if (wwwCards.text.Contains ("\"success\":1")) {
				cardsAdded++;
				IDictionary cardData = (Json.Deserialize(wwwCards.text) as IDictionary)["Player_Card_detail"] as IDictionary;
				CardsManager.CardParameters card = new CardsManager.CardParameters();
				card.card_name = newCard.card_name;
				card.max_level = newCard.max_level;
				card.card_id_in_database = newCard.card_id_in_database;
				card.card_id_in_playerList = int.Parse(cardData["card_no_in_players_list"].ToString());
				card.rarity = newCard.rarity;
				card.cardClass = newCard.cardClass;
				card.skill_1 = newCard.skill_1;
				card.skill_1_Strength = newCard.skill_1_Strength;
				card.skill_2 = newCard.skill_2;
				card.skill_2_Strength = newCard.skill_2_Strength;
				card.skill1_level = 1;
				card.skill2_level = 1;
				card.skill1_exp = CardsManager._instance.GetBaseExp(card.rarity);
				card.skill2_exp = CardsManager._instance.GetBaseExp(card.rarity);
				card.cardSpriteFromResources = Resources.Load<Sprite>("images/"+card.card_name);
				CardsManager._instance.mycards.Add(card);
				if(cardsAdded == newCards.Count) {
					StartCoroutine(UpdateItemsOnTradeCompletion());
				}
			} else {
				StartCoroutine (AddNewCard (newCard));
			}
		}*/

		IEnumerator GetCardData(string buyerID, string cardID) {
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "getPlayerCardsDataSearch");
			wwwForm.AddField ("user_id", buyerID);
			wwwForm.AddField("card_no_in_players_list", cardID);
			WWW cardData = new WWW (loadingScene.Instance.baseUrl, wwwForm);
			yield return cardData;
			Debug.LogError (cardData.text);
			if (cardData.text.Contains ("success\":1")) {
				StartCoroutine (DeleteBuyerCard (buyerID, cardID));
				IDictionary newCardData = ((Json.Deserialize (cardData.text) as IDictionary) ["Player_card"] as IList) [0] as IDictionary;
				StartCoroutine (BuyCard (newCardData));
			} else {
				StartCoroutine (GetCardData (buyerID, cardID));
			}
		}

		IEnumerator BuyCard(IDictionary newCardData) {
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "doAddUpdatePlayerCards");
			wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
			wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
			wwwForm.AddField ("card_name", newCardData["card_name"].ToString());
			wwwForm.AddField ("card_level", newCardData["card_level"].ToString());
			wwwForm.AddField ("card_id_in_database", newCardData["card_id_in_database"].ToString());
			wwwForm.AddField ("class", newCardData["class"].ToString());
			wwwForm.AddField ("rarity", newCardData["rarity"].ToString());
			wwwForm.AddField ("type", newCardData["type"].ToString());
			wwwForm.AddField ("attack", newCardData["attack"].ToString());
			wwwForm.AddField ("defense", newCardData["defense"].ToString());
			wwwForm.AddField ("leadership", newCardData["leadership"].ToString());
			wwwForm.AddField ("experience", newCardData["experience"].ToString());
			wwwForm.AddField ("skill_1", newCardData["skill_1"].ToString());
			wwwForm.AddField ("skill_1_Strength", newCardData["skill_1_Strength"].ToString());
			wwwForm.AddField ("skill_2", newCardData["skill_2"].ToString());
			wwwForm.AddField ("skill_2_Strength", newCardData["skill_2_Strength"].ToString());
			wwwForm.AddField ("max_level", newCardData["max_level"].ToString());
			wwwForm.AddField ("card_cost", newCardData["card_cost"].ToString());
			wwwForm.AddField ("fear_factor", newCardData["fear_factor"].ToString());
			wwwForm.AddField ("card_soldiers", newCardData["card_soldiers"].ToString());
			wwwForm.AddField ("skill1_exp", newCardData["skill1_exp"].ToString());
			wwwForm.AddField ("skill2_exp", newCardData["skill2_exp"].ToString());
			wwwForm.AddField ("skill1_level", newCardData["skill1_level"].ToString());
			wwwForm.AddField ("skill2_level", newCardData["skill2_level"].ToString());
			WWW wwwCards = new WWW (loadingScene.Instance.baseUrl, wwwForm);
			yield return wwwCards;
			Debug.Log (wwwCards.text);
			if (wwwCards.text.Contains ("\"success\":1")) {
				IDictionary cardData = (Json.Deserialize(wwwCards.text) as IDictionary)["Player_Card_detail"] as IDictionary;
				CardsManager.CardParameters card = new CardsManager.CardParameters();
				card.card_name = newCardData["card_name"].ToString();
				card.card_level = int.Parse(newCardData["card_level"].ToString());
				card.card_id_in_database = int.Parse(newCardData["card_id_in_database"].ToString());
				card.cardClass = newCardData["class"].ToString();
				card.rarity = newCardData["rarity"].ToString();
				card.type = newCardData["type"].ToString();
				int.TryParse (newCardData ["attack"].ToString (), out card.attack);
				int.TryParse (newCardData ["defense"].ToString (), out card.defense);
				int.TryParse (newCardData ["leadership"].ToString (), out card.leadership);
				int.TryParse (newCardData ["experience"].ToString (), out card.experience);
				card.skill_1 = newCardData["skill_1"].ToString();
				card.skill_1_Strength = newCardData["skill_1_Strength"].ToString();
				card.skill_2 = newCardData["skill_2"].ToString();
				card.skill_2_Strength = newCardData["skill_2_Strength"].ToString();
				int.TryParse (newCardData ["max_level"].ToString (), out card.max_level);
				if (card.max_level == 0) {
					card.max_level = 25;
				}
				int.TryParse (newCardData ["card_cost"].ToString (), out card.cardCost);
				int.TryParse (newCardData ["fear_factor"].ToString (), out card.fear_factor);
				int.TryParse (newCardData ["card_soldiers"].ToString (), out card.card_soldiers);
				int.TryParse (newCardData ["skill1_exp"].ToString (), out card.skill1_exp);
				int.TryParse (newCardData ["skill2_exp"].ToString (), out card.skill2_exp);
				int.TryParse (newCardData ["skill1_level"].ToString (), out card.skill1_level);
				int.TryParse (newCardData ["skill2_level"].ToString (), out card.skill2_level);
				card.card_id_in_playerList = int.Parse(cardData["card_no_in_players_list"].ToString());
				if (card.skill1_level == 0) {
					card.skill1_level = 1;
				}
				if (card.skill2_level == 0) {
					card.skill2_level = 1;
				}
				if (card.skill1_level == 0) {
					card.skill1_exp = CardsManager._instance.GetBaseExp (card.rarity);
				}
				if (card.skill1_level == 0) {
					card.skill2_exp = CardsManager._instance.GetBaseExp (card.rarity);
				}
				card.cardSpriteFromResources = Resources.Load<Sprite> ("images/" + card.card_name);
				CardsManager._instance.mycards.Add(card);
				cardsAdded++;
				if (cardsAdded == newCardsCount && cardsBuyerDeleted == newCardsCount) {
					StartCoroutine (UpdateItemsOnTradeCompletion ());
				}
			} else {
				StartCoroutine (BuyCard (newCardData));
			}
		}

		IEnumerator UpdateItemsOnTradeCompletion() {
			Debug.Log("updating items on completion");
			Debug.Log (itemNames.Count);
			Debug.Log (itemQty.Count);
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "insertAllPlayer");
			wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
			wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
			int temp = 0;
			while (temp<itemNames.Count) {
				switch (itemNames [temp]) {
				case "stamina_potion":
					PlayerParameters._instance.myPlayerParameter.unlocked_stamina_potion += itemQty [temp];
					wwwForm.AddField ("unlocked_stamina_potion", PlayerParameters._instance.myPlayerParameter.unlocked_stamina_potion);
					break;
				case "attack_potion":
					PlayerParameters._instance.myPlayerParameter.unlocked_attack_potion += itemQty [temp];
					wwwForm.AddField ("unlocked_attack_potion", PlayerParameters._instance.myPlayerParameter.unlocked_attack_potion);
					break;
				case "signal_fire":
					PlayerParameters._instance.myPlayerParameter.signal_fire += itemQty [temp];
					wwwForm.AddField ("signal_fire", PlayerParameters._instance.myPlayerParameter.signal_fire);
					break;
				case "high_security":
					PlayerParameters._instance.myPlayerParameter.no_of_peace_treaties += itemQty [temp];
					wwwForm.AddField ("no_of_peace_treaties", PlayerParameters._instance.myPlayerParameter.no_of_peace_treaties);
					break;
				case "gold":
					PlayerParameters._instance.myPlayerParameter.gold += itemQty [temp];
					wwwForm.AddField ("gold", PlayerParameters._instance.myPlayerParameter.gold);
					break;
				case "food":
					PlayerParameters._instance.myPlayerParameter.wheat += itemQty [temp];
					wwwForm.AddField ("wheat", PlayerParameters._instance.myPlayerParameter.wheat);
					break;
				}
				temp++;
			}

			WWW updateItems = new WWW (loadingScene.Instance.baseUrl, wwwForm);
			yield return updateItems;
			if (updateItems.text.Contains ("\"success\":1")) {
				if(int.Parse(tradeData["rarity"].ToString()) != 0) {
					StartCoroutine(DeleteCard());
				}
				else {
					StartCoroutine (DeleteTradeCoroutine ());
				}
			} else {
				StartCoroutine (UpdateItemsOnTradeCompletion ());
			}
		}


		IEnumerator DeleteCard() {
			CardsManager.CardParameters cardToBeDeleted = CardsManager._instance.mycards.Find (a => a.card_id_in_playerList == int.Parse (tradeData ["item_of_trade"].ToString ()));
			CardsManager._instance.mycards.Remove (cardToBeDeleted);
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "deletePlayerCardsData");
			wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
			wwwForm.AddField ("card_no_in_players_list", tradeData ["item_of_trade"].ToString ());
			WWW deleteCard = new WWW (loadingScene.Instance.baseUrl, wwwForm);
			yield return deleteCard;
			Debug.Log (deleteCard.text);
			if (deleteCard.text.Contains ("\"success\":1")) {
				StartCoroutine (DeleteTradeCoroutine ());
			}
			else {
				StartCoroutine(DeleteCard());
			}
		}

		IEnumerator DeleteTradeCoroutine() {
			Debug.Log("deleting trade");
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "userDeleteTradeId");
			wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
			wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
			wwwForm.AddField ("trade_id", tradeData ["id"].ToString ());
			WWW deleteTrade = new WWW (loadingScene.Instance.baseUrl, wwwForm);
			yield return deleteTrade;
			Debug.Log (deleteTrade.text);
			if (deleteTrade.text.Contains ("\"success\":1")) {
				Destroy (tradeToBeCancelled);
				loadingPopup.SetActive(false);
				bazaarContent.Refresh();
			} else {
				StartCoroutine(DeleteTradeCoroutine());
			}
		}

		IEnumerator DeleteBuyerCard(string userID, string cardID) {
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "deletePlayerCardsData");
			wwwForm.AddField ("user_id", userID);
			wwwForm.AddField ("card_no_in_players_list", cardID);
			WWW deleteCard = new WWW (loadingScene.Instance.baseUrl, wwwForm);
			yield return deleteCard;
			Debug.Log (deleteCard.text);
			if (deleteCard.text.Contains ("\"success\":1")) {
				cardsBuyerDeleted++;
				if (cardsAdded == newCardsCount && cardsBuyerDeleted == newCardsCount) {
					StartCoroutine (UpdateItemsOnTradeCompletion ());
				}
			}
			else {
				StartCoroutine (DeleteBuyerCard (userID, cardID));
			}
		}

	}

}