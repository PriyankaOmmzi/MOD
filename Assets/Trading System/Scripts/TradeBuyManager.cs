using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

namespace Trading {

	public class TradeBuyManager : MonoBehaviour {

		public static TradeBuyManager instance;
		IDictionary tradeData;
		TimeManager timeManager;

		[SerializeField]
		Text postID;
		[SerializeField]
		Text timeLeft;
		[SerializeField]
		Text sellerName;
		[SerializeField]
		Image sellerAvatar;
		[SerializeField]
		Sprite[] sellerAvatars;
		[SerializeField]
		Text sellerGuild;
		[SerializeField]
		Transform ratingParent;
		[SerializeField]
		Text sellerTrades;
		[SerializeField]
		Text sellerPosts;
		[SerializeField]
		Image tradingCard;
		[SerializeField]
		Image tradingItem;
		[SerializeField]
		Text tradingItemCount;
		[SerializeField]
		Transform requestedCardsParent;
		[SerializeField]
		Transform requestedItemsParent;
		[SerializeField]
		Transform requestedCountParent;
		[SerializeField]
		Text requestedCount;
		[SerializeField]
		GameObject loadingPopup;
		[SerializeField]
		Text loadingPopupContent;
		[SerializeField]
		GameObject ratingPopup;
		[SerializeField]
		GameObject bazaarSearch;
		[SerializeField]
		GameObject buyPost;
		[SerializeField]
		Text tradePopupContent;
		[SerializeField]
		GameObject tradePopup;
		[SerializeField]
		Text buyTradePopupContent;
		[SerializeField]
		GameObject buyTradePopup;
		List<string> requestedItemNames;
		List<int> requestedItemCount;
		List<string> requestedCardNames;
		List<int> requestedCardIDs;
		List<int> requestedCardLevels;
		List<int> cardIDs;
		[SerializeField]
		GameObject ratingUI;
		int rating;
		[SerializeField]
		TextAsset[] cards;
		int deletedCards;
		IDictionary cardData;
		[SerializeField]
		Button tradingCardsButton;
		[SerializeField]
		Button requestingCardsButton;
		TradeUIManager tradeUIManager;
		[SerializeField]
		Button backButton;

		void Awake() {
			requestedItemNames = new List<string>();
			requestedItemCount = new List<int> ();
			cardIDs = new List<int> ();
			requestedCardNames = new List<string> ();
			requestedCardIDs = new List<int> ();
			requestedCardLevels = new List<int> ();
			instance = this;
		}

		void Start() {
			timeManager = TimeManager._instance;
			tradeUIManager = TradeUIManager.instance;
		}

		public void Reset() {
			ratingUI.SetActive (false);
			ratingPopup.SetActive (false);
			postID.text = "";
			timeLeft.text = "";
			sellerAvatar.sprite = null;
			sellerName.text = "";
			sellerGuild.text = "";
			sellerTrades.text = "";
			sellerPosts.text = "";
			int temp = 0;
			while(temp<ratingParent.childCount) {
				ratingParent.GetChild (temp).GetComponent<Image> ().enabled = false;
				temp++;
			}
			tradingCardsButton.onClick.Invoke ();
			requestingCardsButton.onClick.Invoke ();
			tradingCard.sprite = null;
			tradingCard.color = Color.grey;
			tradingCard.transform.FindChild ("Border").gameObject.SetActive (false);
			tradingItem.sprite = null;
			tradingItem.color = Color.grey;
			tradingItemCount.text = "";
			buyTradePopup.SetActive (false);
			temp = 0;
			while (temp<requestedCardsParent.childCount) {
				requestedCardsParent.GetChild(temp).GetComponent<Image>().sprite = requestedItemsParent.GetChild(temp).GetComponent<Image>().sprite = null;
				requestedCardsParent.GetChild(temp).GetComponent<Image>().color = requestedItemsParent.GetChild(temp).GetComponent<Image>().color = Color.grey;
				requestedCardsParent.GetChild(temp).FindChild ("Border").gameObject.SetActive (false);
				temp++;
			}
		}

		IEnumerator GetPostedTradesCount() {
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag" ,"getCountAllTrades");
			wwwForm.AddField ("user_id", PlayerDataParse._instance.ID (tradeData ["user_id"].ToString ()));
			WWW countTrades = new WWW(loadingScene.Instance.baseUrl, wwwForm);
			yield return countTrades;
			if (countTrades.text.Contains ("success\":1")) {
				sellerPosts.text = (Json.Deserialize (countTrades.text) as IDictionary) ["AllTrades"].ToString();
			} else {
				sellerPosts.text = "0";
			}
			StartCoroutine (GetBoughtTradesCount ());
		}
		
		IEnumerator GetBoughtTradesCount() {
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag" ,"getCountAllTrades");
			wwwForm.AddField ("bidder_id", PlayerDataParse._instance.ID (tradeData ["user_id"].ToString ()));
			WWW countTrades = new WWW(loadingScene.Instance.baseUrl, wwwForm);
			yield return countTrades;
			if (countTrades.text.Contains ("success\":1")) {
				sellerTrades.text = (int.Parse(sellerPosts.text) + int.Parse((Json.Deserialize (countTrades.text) as IDictionary) ["AllTrades"].ToString())).ToString();
			} else {
				sellerTrades.text = sellerPosts.text;
			}
			if (int.Parse (tradeData ["rarity"].ToString ()) > 0) {
				StartCoroutine (GetCardDataForUI ());
			} else if (requestedCardNames.Count > 0) {
				requestedCardIDs.Clear ();
				StartCoroutine (GetCardID (requestedCardNames[0]));
			} else {
				loadingPopup.SetActive (false);
			}
		}

		IEnumerator GetSellerName() {
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "getUserName");
			Debug.Log (PlayerDataParse._instance.ID (tradeData ["user_id"].ToString ()));
			Debug.Log (SystemInfo.deviceUniqueIdentifier);
			wwwForm.AddField ("user_id", PlayerDataParse._instance.ID (tradeData ["user_id"].ToString ()));
			wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
			WWW sellerNameWWW = new WWW (loadingScene.Instance.baseUrl, wwwForm);
			yield return sellerNameWWW;
			if (sellerNameWWW.text.Contains ("success\":1")) {
				sellerName.text = (Json.Deserialize (sellerNameWWW.text) as IDictionary) ["User_name"].ToString();
				StartCoroutine (GetAvatarNumber ());
			} else {
				StartCoroutine (GetSellerName ());
			}
		}

		IEnumerator GetAvatarNumber() {
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "getAvtarnoFromPlayers");
			wwwForm.AddField ("user_id", PlayerDataParse._instance.ID (tradeData ["user_id"].ToString ()));
			WWW avatarNumberWWW = new WWW (loadingScene.Instance.baseUrl, wwwForm);
			yield return avatarNumberWWW;
			if (avatarNumberWWW.text.Contains ("success\":1")) {
				sellerAvatar.sprite = sellerAvatars [int.Parse (((((Json.Deserialize (avatarNumberWWW.text) as IDictionary) ["Players"] as IList) [0] as IDictionary) ["avatar_no"].ToString ())) - 1];
				StartCoroutine (GetSellerReviews ());
			} else {
				StartCoroutine (GetAvatarNumber ());
			}
		}

		IEnumerator GetSellerReviews() {
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "getAllTradesRatingArray");
			wwwForm.AddField ("user_id", PlayerDataParse._instance.ID (tradeData ["user_id"].ToString ()));
			WWW sellerReviews = new WWW (loadingScene.Instance.baseUrl, wwwForm);
			yield return sellerReviews;
			Debug.Log (sellerReviews.text);
			if (sellerReviews.text.Contains ("success\":1")) {
				int ratingsCount = 0;
				int totalRatings = 0;
				IList allRatings = (Json.Deserialize(sellerReviews.text) as IDictionary)["Alltrades"] as IList;
				foreach (IDictionary rating in allRatings) {
					int ratingInt = int.Parse (rating ["rating"].ToString ());
					if (ratingInt != 0) {
						ratingsCount++;
						totalRatings += ratingInt;
					}
				}
				if (ratingsCount > 0) {
					int rating = totalRatings / ratingsCount;
					int temp = 0;
					while (temp < rating) {
						ratingParent.GetChild (temp).GetComponent<Image> ().enabled = true;
						temp++;
					}
				}
			}
			StartCoroutine (GetPostedTradesCount ());
		}

		public void ShowTradeData(IDictionary data) {
			Reset ();
			loadingPopupContent.text = "Loading...";
			loadingPopup.SetActive (true);
			tradeData = data;
			postID.text = "ID: " + tradeData ["id"].ToString ();
			string tradableItemName = tradeData ["item_name"].ToString ();
			if (int.Parse(tradeData ["rarity"].ToString ()) > 0) {
				tradingCard.sprite = Resources.Load<Sprite> ("images/" + tradableItemName);
				tradingCard.color = Color.white;
				tradingCard.transform.FindChild ("Border").FindChild ("Level").GetComponentInChildren<Text> ().text = tradeData ["rarity"].ToString ();
			} else {
				tradingItem.sprite = Resources.Load<Sprite> ("items/" + tradableItemName);
				tradingItem.color = Color.white;
				tradingItemCount.text = tradeData ["item_of_trade"].ToString ().Split (',') [1];
			}
			string requestedItemsString = data["requested_items"].ToString();
			string requestedRarityString = data ["requested_rarity"].ToString ();
			string itemCountString = data ["no_of_items"].ToString ();
			string[] requestedItems = requestedItemsString.Split(',');
			string[] requestedRarity = requestedRarityString.Split(',');
			string[] itemCount = itemCountString.Split(',');
			requestedCount.text = requestedItems.Length + "/5";
			int temp = 0;
			int cards = 0;
			int items = 0;
			requestedItemNames.Clear ();
			requestedItemCount.Clear ();
			requestedCardNames.Clear ();
			requestedCardLevels.Clear ();
			while (temp < requestedItems.Length) {
				if(itemCount[temp] == "0") {
					requestedCardNames.Add(requestedItems[temp]);
					requestedCardLevels.Add (int.Parse (requestedRarity [temp]));
					requestedCardsParent.GetChild(cards).GetComponent<Image>().sprite = Resources.Load<Sprite> ("images/" + requestedItems[temp]);
					requestedCardsParent.GetChild(cards).GetComponent<Image>().color = Color.white;
					requestedCardsParent.GetChild(cards).FindChild ("Border").FindChild ("Level").GetComponentInChildren<Text> ().text = requestedRarity[temp];
					cards++;
				}
				else {
					requestedItemCount.Add(int.Parse(itemCount[temp]));
					requestedItemNames.Add(requestedItems[temp]);
					requestedItemsParent.GetChild(items).GetComponent<Image>().sprite = Resources.Load<Sprite> ("items/" + requestedItems[temp]);
					requestedItemsParent.GetChild(items).GetComponent<Image>().color = Color.white;
					requestedCountParent.GetChild(items).GetComponent<AssociatedTradeValue>().text.text = itemCount[temp];
					items++;
				}
				temp++;
			}
			StartCoroutine (GetSellerName ());
			UpdateTimeAndCheckStatus ();
			BuyPostUI ();
		}

		void OnDisable() {
			CancelInvoke("UpdateTimeAndCheckStatus");
		}

		void UpdateTimeAndCheckStatus() {
			if (tradeData ["bidder_id"].ToString () == "") {
				DateTime endingTime = Convert.ToDateTime (tradeData ["starting_time"].ToString ()).AddDays (2);
				TimeSpan difference = endingTime - timeManager.GetCurrentServerTime ();
				int hours = difference.Days * 24 + difference.Hours;
				int minutes = difference.Minutes;
				if (hours <= 0 && minutes <= 0) {
					buyTradePopupContent.text = "Trade Expired!";
					buyTradePopup.SetActive (true);
					timeLeft.text = "0 hrs 0 mins";
				} else {
					timeLeft.text = hours + " hrs " + minutes + " mins";
					Invoke ("UpdateTimeAndCheckStatus", 1f);
				}
			} else {
				buyTradePopupContent.text = "Trade Expired!";
				buyTradePopup.SetActive (true);
				timeLeft.text = "0 hrs 0 mins";
			}
		}

		public void Refresh() {
			CancelInvoke ("UpdateTimeAndCheckStatus");
			UpdateTimeAndCheckStatus ();
		}

		public void Trade() {
			if (CanTradeBeBought ()) {
				loadingPopupContent.text = "Completing trade...";
				loadingPopup.SetActive (true);
				if (int.Parse(tradeData ["rarity"].ToString ()) > 0) {//is card
					StartCoroutine(GetCardData());
				}
				else {//is item
					StartCoroutine(Buyitem());
				}
			} else {
				buyTradePopupContent.text = "Trade couldn't be completed! You don't fulfill the requirements of the trade!";
				buyTradePopup.SetActive (true);
			}
		}

		int Rarity(string name) {
			int temp = 0;
			while (temp < cards.Length) {
				if (cards [temp].text.Contains (name)) {
					return(temp + 1);
				}
				temp++;
			}
			return 0;
		}

		bool CanTradeBeBought() {
			cardIDs.Clear ();
			int avatarLevel = PlayerParameters._instance.myPlayerParameter.avatar_level + 1;
			int temp = 0;
			while(temp<requestedCardNames.Count) {
				if (avatarLevel < 20) {
					int rarity = Rarity (requestedCardNames[temp]);
					if (avatarLevel < 18) {
						if (rarity > 3) {
							Debug.Log ("rarity > 3");
							return false;
						}
					} else if (rarity > 4) {
						Debug.Log ("rarity > 4");
						return false;
					}
				}
				if(!CardsManager._instance.mycards.Exists(x => x.card_name == requestedCardNames[temp])) {
					return false;
				}
				else {
					int ID = -1;
					List<CardsManager.CardParameters> cards = CardsManager._instance.mycards.FindAll(x => x.card_name == requestedCardNames[temp]);
					foreach(CardsManager.CardParameters card in cards) {
						if(!card.isLocked && card.max_level == requestedCardLevels[temp]) {
							ID = card.card_id_in_playerList;
							break;
						}
					}
					if(ID == -1) {
						return false;
					}
					else {
						cardIDs.Add(ID);
					}
				}
				temp++;
			}
			temp = 0;
			while (temp<requestedItemNames.Count) {
				switch (requestedItemNames [temp]) {
				case "stamina_potion":
					if (PlayerParameters._instance.myPlayerParameter.unlocked_stamina_potion < requestedItemCount [temp]) {
						Debug.Log ("u dont have " + requestedItemNames [temp]);
						return false;
					}
					break;
				case "attack_potion":
					if (PlayerParameters._instance.myPlayerParameter.unlocked_attack_potion < requestedItemCount [temp]) {
						Debug.Log(PlayerParameters._instance.myPlayerParameter.unlocked_attack_potion);
						Debug.Log(requestedItemCount [temp]);
						Debug.Log ("u dont have " + requestedItemNames [temp]);
					return false;
					}
					break;
				case "signal_fire":
					if (PlayerParameters._instance.myPlayerParameter.signal_fire < requestedItemCount [temp]) {
						Debug.Log ("u dont have " + requestedItemNames [temp]);
					return false;
					}
					break;
				case "high_security":
					if (PlayerParameters._instance.myPlayerParameter.no_of_peace_treaties < requestedItemCount [temp]) {
						Debug.Log ("u dont have " + requestedItemNames [temp]);
					return false;
					}
					break;
				case "royal_key":
//					if(PlayerParameters._instance.myPlayerParameter.stamina_potion<requestedItemCount[temp]) {
					Debug.Log ("u dont have " + requestedItemNames [temp]);
					return false;
//					}
				case "peasant_key":
//					if(PlayerParameters._instance.myPlayerParameter.stamina_potion<requestedItemCount[temp]) {
					Debug.Log ("u dont have " + requestedItemNames [temp]);
					return false;
//					}
				case "gold":
					if (PlayerParameters._instance.myPlayerParameter.gold < requestedItemCount [temp]) {
						Debug.Log ("u dont have " + requestedItemNames [temp]);
					return false;
					}
					break;
				case "food":
					if (PlayerParameters._instance.myPlayerParameter.wheat < requestedItemCount [temp]) {
						Debug.Log ("u dont have " + requestedItemNames [temp]);
					return false;
					}
					break;
				}
				temp++;
			}
			return true;
		}


		IEnumerator GetCardID(string cardName) {
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "getCardId");
			wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
			wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
			wwwForm.AddField ("card_name", cardName);
			WWW cardData = new WWW (loadingScene.Instance.baseUrl, wwwForm);
			yield return cardData;
			if (cardData.text.Contains ("success\":1")) {
				requestedCardIDs.Add (int.Parse ((((Json.Deserialize (cardData.text) as IDictionary) ["card_data"] as IList) [0] as IDictionary) ["Card_id"].ToString ()));
				if (requestedCardIDs.Count == requestedCardNames.Count) {
					StartCoroutine (GetRequestedCardsDataForUI ());
				} else {
					StartCoroutine (GetCardID (requestedCardNames[requestedCardIDs.Count]));
				}
			} else {
				StartCoroutine (GetCardID (cardName));
			}
		}

		IEnumerator GetRequestedCardsDataForUI() {
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "getCardData");
			string cardIDs = "";
			foreach(int ID in requestedCardIDs) {
				if(cardIDs != "") {
					cardIDs += ",";
				}
				cardIDs += ID + "";
			}
			wwwForm.AddField ("card_id", cardIDs);
			WWW wwwCards = new WWW(loadingScene.Instance.baseUrl , wwwForm);
			yield return wwwCards;
			if (wwwCards.text.Contains ("success\":1")) {
				IList cardsData = (Json.Deserialize (wwwCards.text) as IDictionary) ["Params"] as IList;
				int temp = 0;
				foreach (IList cardData in cardsData) {
					requestedCardsParent.GetChild (temp).FindChild ("Border").FindChild ("Avatar").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Avatars/" + cardData [1]);
					requestedCardsParent.GetChild (temp).transform.FindChild ("Border").FindChild ("Cost").GetComponentInChildren<Text> ().text = cardData [11].ToString ();
					requestedCardsParent.GetChild(temp).FindChild ("Border").gameObject.SetActive (true);
					temp++;
				}
				StartCoroutine (FetchGuild ());
			} else {
				StartCoroutine (GetRequestedCardsDataForUI ());
			}
		}

		IEnumerator GetCardDataForUI() {
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "getPlayerCardsDataSearch");
			wwwForm.AddField ("user_id", PlayerDataParse._instance.ID (tradeData ["user_id"].ToString ()));
			wwwForm.AddField("card_no_in_players_list", tradeData ["item_of_trade"].ToString ());
			WWW cardDataWWW = new WWW (loadingScene.Instance.baseUrl, wwwForm);
			yield return cardDataWWW;
			Debug.Log (cardDataWWW.text);
			if (cardDataWWW.text.Contains ("success\":1")) {
				cardData = ((Json.Deserialize (cardDataWWW.text) as IDictionary) ["Player_card"] as IList) [0] as IDictionary;
				tradingCard.transform.FindChild ("Border").FindChild ("Avatar").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Avatars/" + cardData ["class"]);
				tradingCard.transform.FindChild ("Border").FindChild ("Cost").GetComponentInChildren<Text> ().text = cardData["card_cost"].ToString();
				tradingCard.transform.FindChild ("Border").FindChild ("Type").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Types/" + cardData ["type"]);
				tradingCard.transform.FindChild ("Border").gameObject.SetActive (true);
				if (requestedCardNames.Count > 0) {
					requestedCardIDs.Clear ();
					StartCoroutine (GetCardID (requestedCardNames[0]));
				} else {
					loadingPopup.SetActive (false);
				}
			} else {
				StartCoroutine(GetCardDataForUI());
			}
		}

		IEnumerator GetCardData() {
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "getPlayerCardsDataSearch");
			wwwForm.AddField ("user_id", PlayerDataParse._instance.ID (tradeData ["user_id"].ToString ()));
			wwwForm.AddField("card_no_in_players_list", tradeData ["item_of_trade"].ToString ());
			WWW cardData = new WWW (loadingScene.Instance.baseUrl, wwwForm);
			yield return cardData;
			Debug.LogError (cardData.text);
			if (cardData.text.Contains ("success\":1")) {
				IDictionary newCardData = ((Json.Deserialize (cardData.text) as IDictionary) ["Player_card"] as IList) [0] as IDictionary;
				StartCoroutine (BuyCard (newCardData));
			} else {
				StartCoroutine(GetCardData());
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
				if(cardIDs.Count>0) {
					DeleteCards();
				}
				else {
					StartCoroutine(SellItems());
				}
			} else {
				StartCoroutine (BuyCard (newCardData));
			}
		}

		IEnumerator Buyitem() {
			int qty = int.Parse (tradeData ["item_of_trade"].ToString ().Split (',') [1]);
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "insertAllPlayer");
			wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
			wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
			switch (tradeData ["item_name"].ToString ()) {
			case "stamina_potion":
				PlayerParameters._instance.myPlayerParameter.unlocked_stamina_potion += qty;
				wwwForm.AddField ("unlocked_stamina_potion", PlayerParameters._instance.myPlayerParameter.unlocked_stamina_potion);
				break;
			case "attack_potion":
				PlayerParameters._instance.myPlayerParameter.unlocked_attack_potion += qty;;
				wwwForm.AddField ("unlocked_attack_potion", PlayerParameters._instance.myPlayerParameter.unlocked_attack_potion);
				break;
			case "signal_fire":
				PlayerParameters._instance.myPlayerParameter.signal_fire += qty;
				wwwForm.AddField ("signal_fire", PlayerParameters._instance.myPlayerParameter.signal_fire);
				break;
			case "high_security":
				PlayerParameters._instance.myPlayerParameter.no_of_peace_treaties += qty;
				wwwForm.AddField ("no_of_peace_treaties", PlayerParameters._instance.myPlayerParameter.no_of_peace_treaties);
				break;
			case "gold":
				PlayerParameters._instance.myPlayerParameter.gold += qty;
				wwwForm.AddField ("gold", PlayerParameters._instance.myPlayerParameter.gold);
				break;
			case "food":
				PlayerParameters._instance.myPlayerParameter.wheat += qty;
				wwwForm.AddField ("wheat", PlayerParameters._instance.myPlayerParameter.wheat);
				break;
			}
			WWW updateItems = new WWW(loadingScene.Instance.baseUrl, wwwForm);
			yield return updateItems;
			if (updateItems.text.Contains ("\"success\":1")) {
				if(cardIDs.Count>0) {
					DeleteCards();
				}
				else {
					StartCoroutine(SellItems());
				}
			} else {
				StartCoroutine (Buyitem ());
			}
		}

		void DeleteCards() {
			deletedCards = 0;
			foreach(int ID in cardIDs) {
				CardsManager._instance.mycards.Remove (CardsManager._instance.mycards.Find (a => a.card_id_in_playerList == ID));
				StartCoroutine (UpdateCardIsDeleted (ID.ToString()));
			}
		}

		IEnumerator UpdateCardIsDeleted(string cardID) {
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "updatePlayerCardsIsLocked");
			wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
			wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
			wwwForm.AddField ("card_no_in_players_list", cardID);
			wwwForm.AddField ("is_deleted", "true");
			WWW deletedCard = new WWW(loadingScene.Instance.baseUrl, wwwForm);
			yield return deletedCard;
			if (deletedCard.text.Contains ("\"success\":1")) {
				deletedCards++;
				if (deletedCards == cardIDs.Count) {
					if(requestedItemNames.Count > 0) {
						StartCoroutine(SellItems());
					}
					else {
						StartCoroutine(UpdateBidderIDActiveTradesTable());
					}
				}
			} else {
				StartCoroutine(UpdateCardIsDeleted(cardID));
			}
		}

		/*IEnumerator SellCard(int ID) {
			CardsManager._instance.mycards.Remove (CardsManager._instance.mycards.Find (a => a.card_id_in_playerList == ID));
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "deletePlayerCardsData");
			wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
			wwwForm.AddField ("card_no_in_players_list", ID);
			WWW deleteCard = new WWW (loadingScene.Instance.baseUrl, wwwForm);
			yield return deleteCard;
			Debug.Log (deleteCard.text);
			if (deleteCard.text.Contains ("\"success\":1")) {
				deletedCardsCount++;
				if (deletedCardsCount == cardIDs.Count) {
					if(requestedItemNames.Count > 0) {
						StartCoroutine(SellItems());
					}
					else {
						StartCoroutine(UpdateBidderIDActiveTradesTable());
					}
				}
			} else {
				StartCoroutine(SellCard(ID));
			}
			
		}*/


		IEnumerator SellItems() {
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "insertAllPlayer");
			wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
			wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
			int temp = 0;
			while (temp<requestedItemNames.Count) {
				switch (requestedItemNames [temp]) {
				case "stamina_potion":
					PlayerParameters._instance.myPlayerParameter.unlocked_stamina_potion -= requestedItemCount [temp];
					wwwForm.AddField ("unlocked_stamina_potion", PlayerParameters._instance.myPlayerParameter.unlocked_stamina_potion);
					break;
				case "attack_potion":
					PlayerParameters._instance.myPlayerParameter.unlocked_attack_potion -= requestedItemCount [temp];
					wwwForm.AddField ("unlocked_attack_potion", PlayerParameters._instance.myPlayerParameter.unlocked_attack_potion);
					break;
				case "signal_fire":
					PlayerParameters._instance.myPlayerParameter.signal_fire -= requestedItemCount [temp];
					wwwForm.AddField ("signal_fire", PlayerParameters._instance.myPlayerParameter.signal_fire);
					break;
				case "high_security":
					PlayerParameters._instance.myPlayerParameter.no_of_peace_treaties -= requestedItemCount [temp];
					wwwForm.AddField ("no_of_peace_treaties", PlayerParameters._instance.myPlayerParameter.no_of_peace_treaties);
					break;
				case "gold":
					PlayerParameters._instance.myPlayerParameter.gold -= requestedItemCount [temp];
					wwwForm.AddField ("gold", PlayerParameters._instance.myPlayerParameter.gold);
					break;
				case "food":
					PlayerParameters._instance.myPlayerParameter.wheat -= requestedItemCount [temp];
					wwwForm.AddField ("wheat", PlayerParameters._instance.myPlayerParameter.wheat);
					break;
				}
				temp++;
			}
			
			WWW sellItems = new WWW (loadingScene.Instance.baseUrl, wwwForm);
			yield return sellItems;
			if (sellItems.text.Contains ("\"success\":1")) {
				StartCoroutine(UpdateBidderIDActiveTradesTable());
			} else {
				StartCoroutine (SellItems ());
			}
		}

		IEnumerator UpdateBidderIDActiveTradesTable() {
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "getUpdateTrades");
			wwwForm.AddField("trade_id", tradeData ["id"].ToString ());
			string bidderData = PlayerDataParse._instance.playersParam.userId;
			foreach (int ID in cardIDs) {
				bidderData += "," + ID;
			}
			wwwForm.AddField("bidder_id", bidderData);
			Debug.Log (bidderData);
			WWW trade = new WWW (loadingScene.Instance.baseUrl, wwwForm);
			yield return trade;
			Debug.Log (trade.text);
			if (trade.text.Contains ("\"success\":1")) {
				StartCoroutine (UpdateBidderIDAllTradesTable ());
			} else {
				StartCoroutine (UpdateBidderIDActiveTradesTable ());
			}
		}
		
		IEnumerator UpdateBidderIDAllTradesTable() {
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "getUpdateAllTrades");
			wwwForm.AddField("trade_id_fkey", tradeData ["id"].ToString ());
			wwwForm.AddField("bidder_id", PlayerDataParse._instance.playersParam.userId);
			WWW trade = new WWW (loadingScene.Instance.baseUrl, wwwForm);
			yield return trade;
			Debug.Log (trade.text);
			if (trade.text.Contains ("\"success\":1")) {
				StartCoroutine (UpdateDailyTrades ());
			} else {
				StartCoroutine (UpdateBidderIDAllTradesTable ());
			}
		}

		IEnumerator UpdateDailyTrades() {
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "getPlayerDailyTrades");
			wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
			wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
			WWW dailyTrades = new WWW(loadingScene.Instance.baseUrl, wwwForm);
			yield return dailyTrades;
			Debug.Log ("dailyTrades: " + dailyTrades.text);
			if (dailyTrades.text.Contains ("\"success\":1")) {
				loadingPopup.SetActive(false);
				RatingUI();
			} else {
				StartCoroutine (UpdateDailyTrades ());
			}
		}
		void RatingUI() {
			ratingUI.SetActive (true);
		}

		void BuyPostUI() {
			bazaarSearch.SetActive (false);
			buyPost.SetActive (true);
		}

		public void Rating() {
			ratingPopup.SetActive (true);
			StartCoroutine (GiveRating ());
		}

		public void SetRating(int value) {
			rating = value;
		}

		IEnumerator GiveRating() {
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "getUserRatingTrades");
			wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
			wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
			wwwForm.AddField("trade_id", tradeData ["id"].ToString ());
			wwwForm.AddField("bidder_id", PlayerDataParse._instance.playersParam.userId);
			wwwForm.AddField("rating", rating);
			WWW wwwRating = new WWW (loadingScene.Instance.baseUrl, wwwForm);
			yield return wwwRating;
			Debug.Log (wwwRating.text);
			if (wwwRating.text.Contains ("\"success\":1")) {
				StartCoroutine(UpdateRatingAllTradesTable());
			} else {
				StartCoroutine (GiveRating ());
			}
		}

		IEnumerator UpdateRatingAllTradesTable() {
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "getUserRatingAllTrades");
			wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
			wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
			wwwForm.AddField("trade_id_fkey", tradeData ["id"].ToString ());
			wwwForm.AddField("bidder_id", PlayerDataParse._instance.playersParam.userId);
			wwwForm.AddField("rating", rating);
			WWW wwwRating = new WWW (loadingScene.Instance.baseUrl, wwwForm);
			yield return wwwRating;
			Debug.Log (wwwRating.text);
			if (wwwRating.text.Contains ("\"success\":1")) {
				ratingPopup.SetActive(false);
				ratingUI.SetActive(false);
				buyPost.SetActive (false);
				bazaarSearch.SetActive (true);
			} else {
				StartCoroutine (UpdateRatingAllTradesTable ());
			}	
		}
			
		IEnumerator FetchGuild() {
//			WWWForm wwwForm = new WWWForm ();
//			wwwForm.AddField ("tag", "fetchPlayerReqData");
//			wwwForm.AddField ("user_id", PlayerDataParse._instance.ID (tradeData ["user_id"]));
//			wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
//			WWW currentGuild = new WWW (API.Instance.commonURL, wwwForm);
//			yield return currentGuild;
//			Debug.Log (currentGuild.text);
//			if (currentGuild.text.Contains ("\"success\":1")) {
//				IDictionary guildData = (Json.Deserialize (currentGuild.text) as IDictionary)["data"] as IDictionary;
//				if (guildData ["guild_id"].ToString () == "0") {
//					
//				} else {
//					sellerGuild.text
//
//				}
//			} else {
//				
//			}
			yield return null;
			loadingPopup.SetActive (false);
		}

		public void Blacklist() {
			Debug.Log ("blacklisting");
			tradeUIManager.LoadingPopup (true, "Blacklisting trade...");
			StartCoroutine (BlackListTrade ());
		}

		IEnumerator BlackListTrade() {
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "addBlacklistedTrades");
			wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
			wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
			wwwForm.AddField ("trade_id", tradeData ["id"].ToString ());
			WWW blackListWWW = new WWW (API.Instance.commonURL, wwwForm);
			yield return blackListWWW;
			tradeUIManager.LoadingPopup (false);
			Debug.Log (blackListWWW.text);
			if (blackListWWW.text.Contains ("\"success\":1")) {
				backButton.onClick.Invoke ();
				tradeUIManager.WarningPopup ("Trade blacklisted successfully.");
			} else {
				tradeUIManager.WarningPopup ("Trade couldn't be blacklisted. Please try again.");
			}
		}

	}

}