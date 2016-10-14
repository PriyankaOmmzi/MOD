using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Trading {
	public class TradeData : MonoBehaviour {

		[SerializeField]
		Text postID;
		[SerializeField]
		Image itemForSale;
		[SerializeField]
		Text itemName;
		[SerializeField]
		Text timeLeft;
		[SerializeField]
		Text itemValue;
		[SerializeField]
		Transform requestedCardsParent;
		TimeManager timeManager;
		public IDictionary data;
		TradeDeletion tradeDeletion;
		[SerializeField]
		GameObject status;
		[SerializeField]
		GameObject deleteTrade;
		[SerializeField]
		Text statusText;
		[SerializeField]
		Text deleteText;
		TradeSearchManager tradeSearchManager;
		TradeBuyManager tradeBuyManager;
		[SerializeField]
		Transform ratingParent;

		void Awake() {
			timeManager = TimeManager._instance;
			tradeDeletion = TradeDeletion.instance;
			tradeSearchManager = TradeSearchManager.instance;
			tradeBuyManager = TradeBuyManager.instance;
		}

		void OnDisable() {
			CancelInvoke ("UpdateTimeAndCheckStatus");
		}

		public void Set(IDictionary tradeData) {
			data = tradeData;
			postID.text = data ["id"].ToString ();
			if(int.Parse(data["user_id"].ToString()) == PlayerDataParse._instance.playersParam.userIdNo) {
				deleteTrade.SetActive(true);
			}
			itemName.text = data ["item_name"].ToString ();
			if (int.Parse (data ["rarity"].ToString ()) > 0) {//is card
				itemValue.text = "Max Level: " + data ["rarity"].ToString ();
				itemForSale.sprite = Resources.Load<Sprite> ("images/" + itemName.text);
			} else {//is item
				itemValue.text = "Number: " + data ["item_of_trade"].ToString ().Split(',')[1];
				itemForSale.sprite = Resources.Load<Sprite> ("items/" + itemName.text);
			}

			string requestedItemsString = data["requested_items"].ToString();
			string[] requestedItems = requestedItemsString.Split(',');
			string itemCountString = data ["no_of_items"].ToString ();
			string[] itemCount = itemCountString.Split(',');
			string requestedRarityString = data ["requested_rarity"].ToString ();
			string[] requestedRarity = requestedRarityString.Split (',');

			int temp = 0;
			while (temp < requestedItems.Length) {
				if(itemCount[temp] == "0") {
					requestedCardsParent.GetChild(temp).GetComponent<Text>().text = requestedItems[temp] + "\nMax Level: " + requestedRarity[temp];
				}
				else {
					requestedCardsParent.GetChild(temp).GetComponent<Text>().text = requestedItems[temp] + "\nNumber: " + itemCount[temp];
				}
				temp++;
			}
			UpdateTimeAndCheckStatus ();
		}

		void UpdateTimeAndCheckStatus() {
			if (data ["bidder_id"].ToString () == "") {
				int temp = 0;
				while(temp<ratingParent.childCount) {
					ratingParent.GetChild(temp).GetComponent<Image>().enabled = false;
					temp++;
				}
				DateTime endingTime = Convert.ToDateTime (data ["starting_time"].ToString ()).AddDays (2);
				TimeSpan difference = endingTime - timeManager.GetCurrentServerTime ();
				int hours = difference.Days * 24 + difference.Hours;
				int minutes = difference.Minutes;
				if (hours <= 0 && minutes <= 0) {
					if (int.Parse (data ["user_id"].ToString ()) == PlayerDataParse._instance.playersParam.userIdNo) {
						deleteText.text = "Delete";
					}
					statusText.text = "EXPIRED";
					status.SetActive (true);
					timeLeft.text = "0 hrs 0 mins";
				} else {
					if (int.Parse (data ["user_id"].ToString ()) == PlayerDataParse._instance.playersParam.userIdNo) {
						deleteText.text = "Cancel";
					}
					status.SetActive (false);
					timeLeft.text = hours + " hrs " + minutes + " mins";
					Invoke ("UpdateTimeAndCheckStatus", 1f);
				}
			} else {
				Debug.Log(data["rating"].ToString());
				int rating = 0;
				int.TryParse(data["rating"].ToString(), out rating);
				int temp = 0;
				while(temp<rating) {
					ratingParent.GetChild(temp).GetComponent<Image>().enabled = true;
					temp++;
				}
				if (int.Parse (data ["user_id"].ToString ()) == PlayerDataParse._instance.playersParam.userIdNo) {
					deleteText.text = "Delete";
				}
				statusText.text = "COMPLETED";
				status.SetActive (true);
				timeLeft.text = "0 hrs 0 mins";
			}
		}

		public void CancelTrade() {
			if (deleteText.text == "Delete") {
				Debug.Log("delete trade");
				tradeDeletion.DeleteTrade (gameObject);
			} else {
				tradeDeletion.StartCoroutine ("CanTradeBeCancelled", gameObject);
			}
		}

		public void OpenTrade() {
			if (tradeSearchManager.dailyTrades == TradeConstants.maxDailyTrades) {
				tradeSearchManager.SetWarningPanel (true);
			} else {
				tradeBuyManager.ShowTradeData (data);
				tradeSearchManager.Back ();
			}
		}

	}
}
