using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

namespace Trading {

	public enum TradeItem {Card, Item, Null}

	public class TradeCreation : MonoBehaviour {

		public static TradeCreation instance;
		[SerializeField]
		Image tradingCardImage;
		int rarity;
		[SerializeField]
		Image tradingItemImage;
		[SerializeField]
		Text tradingItemCount;

		[SerializeField]
		Text tradableItemsCount;

		int currentTradableItemID;
		int previousTradableItemID;
		
		[SerializeField]
		string tradeData;

		[SerializeField]
		string baseURL;

		[SerializeField]
		Transform requestedCardsParent;
		[SerializeField]
		Transform requestedItemsParent;

		string requestedItems;
		string requestedRarity;
		string itemsNumber;

		int temp;
		Sprite previousSprite;

		[SerializeField]
		Button confirmButton;
		[SerializeField]
		Button createButton;

		MyTradableItems myTradableItems;
		AllTradableItems allTradableItems;
		RequestingCardsManager requestingCardsManager;

		TradeItem previousTradeItem;
		TradeItem currentTradeItem;

		[SerializeField]
		GameObject createPost;
		[SerializeField]
		GameObject bazaarContent;
		[SerializeField]
		GameObject createPostLoading;
		[SerializeField]
		Text createPostLoadingContent;
		Transform previous;
		Transform current;

		[SerializeField]
		Button cardsButton;
		[SerializeField]
		Button itemsButton;
		[SerializeField]
		GameObject tradePosted;
		[SerializeField]
		Text tradePostedContent;
		[SerializeField]
		GameObject bazaarTicketWarning;
		[SerializeField]
		Text bazaarTicketWarningContent;
		[SerializeField]
		TextAsset[] cards;

		void Awake() {
			instance = this;
		}

		void Start() {
			currentTradeItem = TradeItem.Null;
			allTradableItems = AllTradableItems.instance;
			myTradableItems = MyTradableItems.instance;
			requestingCardsManager = RequestingCardsManager.instance;
		}

		public void Reset() {
			createButton.interactable = false;
			tradableItemsCount.text = "0/1";
			cardsButton.onClick.Invoke ();
			tradingCardImage.transform.FindChild ("Border").FindChild ("Avatar").GetComponent<Image> ().sprite = null;
			tradingCardImage.transform.FindChild ("Border").FindChild ("Cost").GetComponentInChildren<Text> ().text = "";
			tradingCardImage.transform.FindChild ("Border").FindChild ("Level").GetComponentInChildren<Text> ().text = "";
			tradingCardImage.transform.FindChild ("Border").gameObject.SetActive (false);
			tradingCardImage.sprite = null;
			tradingCardImage.color = Color.grey;
			tradingItemImage.sprite = null;
			tradingItemCount.text = "";
			tradingItemImage.color = Color.grey;
			current = null;
			currentTradableItemID = 0;
			currentTradeItem = TradeItem.Null;
			myTradableItems.Reset ();
			allTradableItems.Reset ();
			requestingCardsManager.Reset ();
		}

		public void SavePreviousData() {
			confirmButton.interactable = false;
			previous = current;
			current = null;
			previousTradeItem = currentTradeItem;
			previousTradableItemID = currentTradableItemID;
			if (previousTradeItem == TradeItem.Card) {
				previousSprite = tradingCardImage.sprite;
			} else if (previousTradeItem == TradeItem.Item) {
				previousSprite = tradingItemImage.sprite;
			} else {
				previousSprite = null;
			}
		}

		public void SetTradingCard(Sprite image, int myCardIndex, Transform button) {
			if (current == null) {
				current = button;
				if (previous != null) {
					previous.GetComponent<Button>().interactable = true;
					previous.FindChild("Deselect").GetComponent<Image>().raycastTarget = false;
				}
				current.GetComponent<Button>().interactable = false;
				current.transform.FindChild("Deselect").GetComponent<Image>().raycastTarget = true;
				currentTradeItem = TradeItem.Card;
				currentTradableItemID = CardsManager._instance.mycards [myCardIndex].card_id_in_playerList;
				Debug.Log (currentTradableItemID);
				tradingCardImage.sprite = image;
				tradingCardImage.color = Color.white;
				tradingItemImage.sprite = null;
				tradingItemImage.color = Color.grey;
				confirmButton.interactable = true;
			}
		}


		public void DeselectTradingItem() {
			if (current == null) {
				current = previous;
			}
			current.GetComponent<Button>().interactable = true;
			current.transform.FindChild ("Deselect").GetComponent<Image> ().raycastTarget = false;
			current = null;
			currentTradeItem = TradeItem.Null;
			currentTradableItemID = 0;
			tradingCardImage.sprite = null;
			tradingCardImage.color = Color.grey;
			tradingItemImage.sprite = null;
			tradingItemImage.color = Color.grey;
			confirmButton.interactable = true;
		}

		public void SetTradingItem(Sprite image, int ID, Transform button) {
			if (current == null) {
				current = button;
				if (previous != null) {
					previous.GetComponent<Button>().interactable = true;
					previous.FindChild("Deselect").GetComponent<Image>().raycastTarget = false;
				}
				current.GetComponent<Button>().interactable = false;
				current.transform.FindChild("Deselect").GetComponent<Image>().raycastTarget = true;
				currentTradeItem = TradeItem.Item;
				currentTradableItemID = ID;
				tradingItemImage.sprite = image;
				tradingItemImage.color = Color.white;
				tradingCardImage.sprite = null;
				tradingCardImage.color = Color.grey;
				confirmButton.interactable = true;
			}
		}

		public void Confirm() {
			if (tradingCardImage.sprite == null && tradingItemImage.sprite == null) {
				tradingItemCount.text = "";
				tradableItemsCount.text = "0/1";
				tradingCardImage.transform.FindChild ("Border").FindChild ("Avatar").GetComponent<Image> ().sprite = null;
				tradingCardImage.transform.FindChild ("Border").FindChild ("Cost").GetComponentInChildren<Text> ().text = "";
				tradingCardImage.transform.FindChild ("Border").FindChild ("Level").GetComponentInChildren<Text> ().text = "";
				tradingCardImage.transform.FindChild ("Border").gameObject.SetActive (false);
			}
			else {
				if(current == null) {
					current = previous;
				}
				if(tradingCardImage.sprite != null) {
					tradingCardImage.transform.FindChild ("Border").gameObject.SetActive (true);
					tradingCardImage.transform.FindChild ("Border").FindChild ("Avatar").GetComponent<Image> ().sprite = current.FindChild ("Border").FindChild ("Avatar").GetComponent<Image> ().sprite;
					tradingCardImage.transform.FindChild ("Border").FindChild ("Cost").GetComponentInChildren<Text> ().text = current.FindChild ("Border").FindChild ("Cost").GetComponentInChildren<Text> ().text;
					tradingCardImage.transform.FindChild ("Border").FindChild ("Level").GetComponentInChildren<Text> ().text = current.FindChild ("Border").FindChild ("Level").GetComponentInChildren<Text> ().text;
					tradingItemCount.text = "";
				}
				else {
					tradingCardImage.transform.FindChild ("Border").FindChild ("Avatar").GetComponent<Image> ().sprite = null;
					tradingCardImage.transform.FindChild ("Border").FindChild ("Cost").GetComponentInChildren<Text> ().text = "";
					tradingCardImage.transform.FindChild ("Border").FindChild ("Level").GetComponentInChildren<Text> ().text = "";
					tradingCardImage.transform.FindChild ("Border").gameObject.SetActive (false);
					tradingItemCount.text = current.GetComponentInChildren<InputField> ().text;
				}
				tradableItemsCount.text = "1/1";
			}
			ToggleCreateButton ();
		}
		
		public void Back() {
			confirmButton.interactable = false;
			if (previousTradeItem == TradeItem.Card) {
				tradingItemImage.sprite = null;
				tradingItemImage.color = Color.grey;
				tradingCardImage.sprite = previousSprite;
				tradingCardImage.color = Color.white;
			} else if (previousTradeItem == TradeItem.Item) {
				tradingCardImage.sprite = null;
				tradingCardImage.color = Color.grey;
				tradingItemImage.sprite = previousSprite;
				tradingItemImage.color = Color.white;
			} else {
				tradingCardImage.sprite = null;
				tradingCardImage.color = Color.grey;
				tradingItemImage.sprite = null;
				tradingItemImage.color = Color.grey;
			}

			if (current != null) {
				current.GetComponent<Button>().interactable = true;
				current.transform.FindChild("Deselect").GetComponent<Image>().raycastTarget = false;
			}
			if (previous != null) {
				previous.GetComponent<Button>().interactable = false;
				previous.FindChild("Deselect").GetComponent<Image>().raycastTarget = true;
			}

			current = previous;
			currentTradeItem = previousTradeItem;
			currentTradableItemID = previousTradableItemID;
		}

		public void CreateTrade() {
			if (PlayerParameters._instance.myPlayerParameter.totalPostedTrades < 10) {
				bazaarTicketWarningContent.text = "Trading requires a bazaar ticket.\n\nPlayers are " +
					"required to use a bazaar ticket for their first 10 trades. Number of trades: " +
					PlayerParameters._instance.myPlayerParameter.totalPostedTrades + "/10";
				bazaarTicketWarning.SetActive(true);
			} else {
				ConfirmTradeCreation();
			}
		}

		public void ConfirmTradeCreation() {
			createPostLoadingContent.text = "Creating post...";
			createPostLoading.SetActive (true);
			temp = 0;
			requestedItems = null;
			requestedRarity = null;
			itemsNumber = null;
			GetCardData ();
		}

		void GetCardData() {
			int temp = 0;
			while (temp < requestedCardsParent.childCount) {
				Sprite cardSprite = requestedCardsParent.GetChild(temp).GetComponent<Image>().sprite;
				if(cardSprite != null) {
					if(requestedItems != null) {
						requestedItems += ",";
						requestedRarity += ",";
						itemsNumber += ",";
					}
					requestedItems += cardSprite.name;
					requestedRarity += requestedCardsParent.GetChild(temp).FindChild ("Border").FindChild ("Level").GetComponentInChildren<Text> ().text;
					itemsNumber += "0";
				}
				else {
					GetItemData();
					return;
				}
				temp++;
			}
			GetItemData();
		}

		void GetItemData() {
			Transform item = requestedItemsParent.GetChild (temp);
			Sprite itemSprite = item.GetComponent<Image> ().sprite;
			if (itemSprite != null) {
				if(requestedItems != null) {
					requestedItems += ",";
					requestedRarity += ",";
					itemsNumber += ",";
				}
				requestedItems += itemSprite.name;
				requestedRarity += "0";
				itemsNumber += item.GetComponent<AssociatedTradeValue>().text.text;

				temp++;
				if (temp < requestedItemsParent.childCount) {
					GetItemData ();
				} else {
					StartCoroutine(CreateTradeCoroutine());
				}
			} else {
				StartCoroutine(CreateTradeCoroutine());
			}

		}

		IEnumerator CreateTradeCoroutine() {
			Debug.Log ("creating trade");
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", tradeData);
			wwwForm.AddField ("user_id" , PlayerDataParse._instance.playersParam.userId);
			wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
			if (currentTradeItem == TradeItem.Card) {
				wwwForm.AddField ("item_of_trade", currentTradableItemID);
				wwwForm.AddField ("rarity", tradingCardImage.transform.FindChild ("Border").FindChild ("Level").GetComponentInChildren<Text> ().text);
				wwwForm.AddField ("item_name", tradingCardImage.sprite.name);
			} else {
				Debug.Log(currentTradableItemID + "," + tradingItemCount.text);
				wwwForm.AddField ("item_of_trade", currentTradableItemID + "," + tradingItemCount.text);
				wwwForm.AddField ("rarity", 0);
				wwwForm.AddField ("item_name", tradingItemImage.sprite.name);
			}
			wwwForm.AddField ("requested_items", requestedItems);
			wwwForm.AddField ("requested_rarity", requestedRarity);
			wwwForm.AddField ("no_of_items", itemsNumber);
			WWW trade = new WWW (loadingScene.Instance.baseUrl, wwwForm);
			yield return trade;
			if (trade.text.Contains ("\"success\":1")) {
				int id = int.Parse ((((Json.Deserialize(trade.text) as IDictionary)["Trade_detail"] as IList)[0] as IDictionary)["id"].ToString());
				string startingTime = (((Json.Deserialize(trade.text) as IDictionary)["Trade_detail"] as IList)[0] as IDictionary)["starting_time"].ToString();
				wwwForm.AddField ("tag", "userAllTradesFunction");
				wwwForm.AddField("trade_id_fkey", id);
				wwwForm.AddField("starting_time", startingTime);
				StartCoroutine(CreateTradeInAllTradesTable(wwwForm));
				Debug.Log ("trade created successfully" + trade.text);
			} else if (Application.internetReachability != NetworkReachability.NotReachable) {
				StartCoroutine ("CreateTradeCoroutine");
			} else {
				Debug.Log ("Couldn't create trade");
				createPostLoading.SetActive (false);
				tradePostedContent.text = "Trade couldn't be posted. Please try again.";
				tradePosted.SetActive (true);
			}
		}

		IEnumerator CreateTradeInAllTradesTable(WWWForm wwwForm) {
			Debug.Log ("creating all trade");

			WWW allTradesTable = new WWW (loadingScene.Instance.baseUrl, wwwForm);
			yield return allTradesTable;
			if (allTradesTable.text.Contains ("\"success\":1")) {
				if (currentTradeItem == TradeItem.Card) {
					StartCoroutine(LockCard());
				} else {
					StartCoroutine(UpdateItemsCount());
				}
			} else {
				StartCoroutine (CreateTradeInAllTradesTable (wwwForm));
			}
		}
		
		IEnumerator LockCard() {
			Debug.Log ("locking card");
			int cardIndex = CardsManager._instance.mycards.FindIndex (a => a.card_id_in_playerList == currentTradableItemID);
			Debug.Log (cardIndex);
			Debug.Log (currentTradableItemID);
			Debug.Log (SystemInfo.deviceUniqueIdentifier);
			CardsManager.CardParameters tempCard = CardsManager._instance.mycards [cardIndex];
			tempCard.isLocked = true;
			CardsManager._instance.mycards [cardIndex] = tempCard;
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "updatePlayerCardsIsLocked");
			wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
			wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
			wwwForm.AddField ("card_no_in_players_list", currentTradableItemID);
			wwwForm.AddField ("is_locked", "true");
			WWW lockCard = new WWW(loadingScene.Instance.baseUrl, wwwForm);
			yield return lockCard;
			Debug.Log (lockCard.text);
			if (lockCard.text.Contains ("\"success\":1")) {
				StartCoroutine(UpdateDailyTrades());
			} else {
				StartCoroutine(LockCard());
			}
		}

		IEnumerator UpdateItemsCount() {
			Debug.Log ("updating items");
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag" ,"insertAllPlayer");
			wwwForm.AddField ("user_id" , PlayerDataParse._instance.playersParam.userId);
			wwwForm.AddField ("device_id" , SystemInfo.deviceUniqueIdentifier);
			switch (tradingItemImage.sprite.name) {
			case "stamina_potion":
				PlayerParameters._instance.myPlayerParameter.unlocked_stamina_potion -= int.Parse(tradingItemCount.text);
				wwwForm.AddField("unlocked_stamina_potion", PlayerParameters._instance.myPlayerParameter.unlocked_stamina_potion);
				break;
			case "attack_potion":
				PlayerParameters._instance.myPlayerParameter.unlocked_attack_potion -= int.Parse(tradingItemCount.text);
				wwwForm.AddField("unlocked_attack_potion", PlayerParameters._instance.myPlayerParameter.unlocked_attack_potion);
				break;
			case "signal_fire":
				PlayerParameters._instance.myPlayerParameter.signal_fire -= int.Parse(tradingItemCount.text);
				wwwForm.AddField("signal_fire", PlayerParameters._instance.myPlayerParameter.signal_fire);
				break;
			case "high_security":
				PlayerParameters._instance.myPlayerParameter.no_of_peace_treaties -= int.Parse(tradingItemCount.text);
				wwwForm.AddField("no_of_peace_treaties", PlayerParameters._instance.myPlayerParameter.no_of_peace_treaties);
				break;
			case "gold":
				PlayerParameters._instance.myPlayerParameter.gold -= int.Parse(tradingItemCount.text);
				wwwForm.AddField("gold", PlayerParameters._instance.myPlayerParameter.gold);
				break;
			case "food":
				PlayerParameters._instance.myPlayerParameter.wheat -= int.Parse(tradingItemCount.text);
				wwwForm.AddField("wheat", PlayerParameters._instance.myPlayerParameter.wheat);
				break;
			}
			WWW updateItems = new WWW(loadingScene.Instance.baseUrl, wwwForm);
			yield return updateItems;
			if (updateItems.text.Contains ("\"success\":1")) {
				StartCoroutine(UpdateDailyTrades());
			} else {
				StartCoroutine(UpdateItemsCount());
			}
		}

		public void ToggleCreateButton() {
			createButton.interactable = CanTradeBePosted ();
		}

		bool CanTradeBePosted() {
			if (tradingCardImage.sprite == null && tradingItemImage.sprite == null) {
				Debug.Log ("Trade can't be posted because no item selected for selling.");
				return false;
			} else if (tradingItemImage.sprite != null) {
				int qty = int.Parse (tradingItemCount.text);
				switch (tradingItemImage.sprite.name) {
				case "stamina_potion":
					if (qty > PlayerParameters._instance.myPlayerParameter.unlocked_stamina_potion) {
						Debug.Log ("Trade can't be posted because not enough stamina.");
						return false;
					}
					break;
				case "attack_potion":
					if (qty > PlayerParameters._instance.myPlayerParameter.unlocked_attack_potion) {
						Debug.Log ("Trade can't be posted because not enough attack potion.");
						return false;
					}
					break;
				case "signal_fire":
					if (qty > PlayerParameters._instance.myPlayerParameter.signal_fire) {
						Debug.Log ("Trade can't be posted because not enough signal fire.");
						return false;
					}
					break;
				case "high_security":
					if (qty > PlayerParameters._instance.myPlayerParameter.no_of_peace_treaties) {
						Debug.Log ("Trade can't be posted because not enough high security.");
						return false;
					}
					break;
				case "royal_key":
					Debug.Log ("Trade can't be posted because not enough royal keys.");
					return false;
				case "peasant_key":
					Debug.Log ("Trade can't be posted because not enough peasant keys.");
					return false;
				case "gold":
					if (qty > PlayerParameters._instance.myPlayerParameter.gold) {
						Debug.Log ("Trade can't be posted because not enough gold.");
						return false;
					}
					break;
				case "food":
					if (qty > PlayerParameters._instance.myPlayerParameter.wheat) {
						Debug.Log ("Trade can't be posted because not enough food.");
						return false;
					}
					break;
				}
			} else if(PlayerParameters._instance.myPlayerParameter.avatar_level < 19) {
				int level = PlayerParameters._instance.myPlayerParameter.avatar_level + 1;
				int rarity = Rarity (tradingCardImage.sprite.name);
				if (level < 18) {
					if (rarity > 3) {
						Debug.Log ("rarity > 3");
						return false;
					}
				} else if (rarity > 4) {
					Debug.Log ("rarity > 4");
					return false;
				}
 			}
			temp = 0;
			while (temp < requestedCardsParent.childCount) {
				if (requestedCardsParent.GetChild (temp).GetComponent<Image> ().sprite != null) {
					if(PlayerParameters._instance.myPlayerParameter.avatar_level < 19) {
						int level = PlayerParameters._instance.myPlayerParameter.avatar_level + 1;
						int rarity = Rarity (requestedCardsParent.GetChild (temp).GetComponent<Image> ().sprite.name);
						if (level < 18) {
							if (rarity > 3) {
								Debug.Log ("rarity > 3 of requested card");
								return false;
							}
						} else if (rarity > 4) {
							Debug.Log ("rarity > 4 of requested card");
							return false;
						}
					}
				}
				temp++;
			}
			temp = 0;
			while (temp < requestedCardsParent.childCount) {
				if (requestedCardsParent.GetChild (temp).GetComponent<Image> ().sprite != null) {
					return true;
				}
				temp++;
			}
			temp = 0;
			while (temp < requestedItemsParent.childCount) {
				if (requestedItemsParent.GetChild (temp).GetComponent<Image> ().sprite != null) {
			return true;
				}
				temp++;
			}
			return false;
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

		int RarityInt(string rarity)
		{
			int rarityInt = 0;
			Debug.Log (rarity);
			switch (rarity) {
			case "Common":
				rarityInt = 1;
				break;
			case "Uncommon":
				rarityInt = 2;
				break;
			case "Super":
				rarityInt = 3;
				break;
			case "Mega":
				rarityInt = 4;
				break;
			case "Legendary":
				rarityInt = 5;
				break;
			}
			Debug.Log (rarityInt);
			return rarityInt;
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
				if (PlayerParameters._instance.myPlayerParameter.totalPostedTrades < 10) {
					IncrementTradeCount ();
				} else {
					bazaarContent.SetActive (true);
					createPost.SetActive (false);
					createPostLoading.SetActive (false);
					Reset ();
					tradePostedContent.text = "Trade Posted!";
					tradePosted.SetActive (true);
				}
			} else {
				StartCoroutine (UpdateDailyTrades ());
			}
		}

		void IncrementTradeCount() {
			PlayerParameters._instance.myPlayerParameter.totalPostedTrades += 1;
			PlayerParameters._instance.myPlayerParameter.bazaarTickets -= 1;
			StartCoroutine (UpdateTradeCount ());
		}

		IEnumerator UpdateTradeCount() {
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "insertAllPlayer");
			wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
			wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
			wwwForm.AddField ("total_trades", PlayerParameters._instance.myPlayerParameter.totalPostedTrades);
			wwwForm.AddField ("bazar_tickets", PlayerParameters._instance.myPlayerParameter.bazaarTickets);
			WWW trade = new WWW(loadingScene.Instance.baseUrl, wwwForm);
			yield return trade;
			Debug.Log (trade.text);
			if (trade.text.Contains ("\"success\":1")) {
				bazaarContent.SetActive (true);
				createPost.SetActive (false);
				createPostLoading.SetActive (false);
				Reset ();
				tradePostedContent.text = "Trade Posted!";
				tradePosted.SetActive (true);
			} else {
				StartCoroutine (UpdateTradeCount ());
			}
		}
		public void EnableConfirmButton() {
			confirmButton.interactable = true;
		}

	}

}