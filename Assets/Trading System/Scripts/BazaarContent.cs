using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

namespace Trading {
	public class BazaarContent : MonoBehaviour {

		[SerializeField]
		string searchURL;
		[SerializeField]
		string myTradeTag;
		[SerializeField]
		GameObject loading;
		[SerializeField]
		GameObject tradePrefab;
		[SerializeField]
		Transform tradesParent;
		[SerializeField]
		Text tradesCount;
		int maxTrades;
		[SerializeField]
		Button postButton;
		[SerializeField]
		GameObject createPost;
		GameObject bazaarContent;
		int currentPostedTrades;
		[SerializeField]
		GameObject warning;
		[SerializeField]
		Text warningContent;
		[SerializeField]
		Text bazaarTickets;
		int dailyTrades;
		MyTradableItems myTradableItems;
		TradeCreation tradeCreation;

		void Awake() {
			myTradableItems = MyTradableItems.instance;
			tradeCreation = TradeCreation.instance;
			bazaarContent = gameObject;
			maxTrades = 8;
		}

		public void Refresh() {
			StartCoroutine (ShowMyTrades ());
		}

		void OnEnable() {
			Refresh ();
		}
			
		IEnumerator ShowMyTrades() {
			bazaarTickets.text = "Bazaar Tickets: " + PlayerParameters._instance.myPlayerParameter.bazaarTickets;
			loading.SetActive (true);
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", myTradeTag);
			wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
			wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
			WWW myTrades = new WWW (searchURL, wwwForm);
			yield return myTrades;
			Debug.Log (myTrades.text);
			if (myTrades.text != "") {
				IList tradeList = null;

				if (!myTrades.text.Contains ("error_msg")) {
					IDictionary tradeData = (IDictionary)Json.Deserialize (myTrades.text);
					tradeList = (IList)tradeData ["data"];
					RectTransform tempTrade;
					int temp = 0;

//					List<IDictionary> tradesToBeCancelled = new List<IDictionary>();
//
//					Debug.Log(tradeList.Count);
//					foreach(IDictionary trade in tradeList) {
//						DateTime endingTime = Convert.ToDateTime (trade ["starting_time"].ToString ()).AddDays(2);
//						TimeSpan difference = endingTime - timeManager.GetCurrentServerTime();
//						int hours = difference.Days * 24 + difference.Hours;
//						int minutes = difference.Minutes;
//						int seconds = difference.Seconds;
//						if(hours <= 0 && minutes <= 0 && seconds <= 0) {
//						if(hours <= 0 && minutes <= 0) {
//							tradesToBeCancelled.Add(trade);
//						}
//					}
//					foreach(IDictionary trade in tradesToBeCancelled) {
//						tradeDeletion.DeleteTrade(trade["id"].ToString());
//						tradeList.Remove(trade);
//						Debug.Log("delete trade" + trade["id"]);
//					}

					while (temp < tradesParent.childCount && temp < tradeList.Count) {
						tradesParent.GetChild (temp).GetComponent<TradeData> ().Set ((IDictionary)tradeList [temp]);
						temp++;
					}

					while (temp < tradesParent.childCount) {
						Destroy (tradesParent.GetChild (temp).gameObject);
						temp++;
					}

					while (temp<tradeList.Count) {
						tempTrade = Instantiate (tradePrefab).GetComponent<RectTransform> ();
						tempTrade.SetParent (tradesParent);
						tempTrade.localScale = Vector3.one;
						tempTrade.GetComponent<TradeData> ().Set ((IDictionary)tradeList [temp]);
						temp++;
					}

					currentPostedTrades = tradeList.Count;
					tradesCount.text = "Trades remaining: " + (maxTrades - tradeList.Count) + "/" + maxTrades;
				
				} else {
					int temp = 0;
					while (temp < tradesParent.childCount) {
						Destroy (tradesParent.GetChild (temp).gameObject);
						temp++;
					}
					currentPostedTrades = 0;
					tradesCount.text = "Trades remaining: " + maxTrades + "/" + maxTrades;
					Debug.Log ("No trades available");
				}

				StartCoroutine(GetDailyTrades());

			} else if(Application.internetReachability != NetworkReachability.NotReachable) {
				Refresh();
			}
		}

		IEnumerator GetDailyTrades() {
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "getCountDailyTrades");
			wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
			wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
			WWW www = new WWW (loadingScene.Instance.baseUrl, wwwForm);
			yield return www;
			if (www.text.Contains ("\"success\":1")) {
				Debug.Log ("daily: " + www.text);
				dailyTrades = int.Parse ((Json.Deserialize (www.text) as IDictionary) ["Player_DailyTrades"].ToString ());
				loading.SetActive (false);
			} else {
				StartCoroutine(GetDailyTrades());
			}
		}

		public void Post() {
			bool canPost = true;
			if (currentPostedTrades == maxTrades) {
				warningContent.text = "Player does not meet the requirements to trade." + "\n" + 
					"Trade limit reached (8/8)" + "\n" +
					"A player can only have 8 active posts at a time.";
				warning.SetActive (true);
				canPost = false;
			} else if (PlayerParameters._instance.myPlayerParameter.totalPostedTrades < 10 && PlayerParameters._instance.myPlayerParameter.bazaarTickets == 0) {
				warningContent.text = "Player does not meet requirement to trade." + "\n\n" +
					"1 x Bazaar Ticket" + "\n\n" +
					"Players are required to use a bazaar ticket for their first 10 trades. Number of trades: " + 
					PlayerParameters._instance.myPlayerParameter.totalPostedTrades + "/10";
				warning.SetActive (true);
				canPost = false;
			} else if(dailyTrades == TradeConstants.maxDailyTrades) {
				warningContent.text = "Player does not meet the requirements to trade." + "\n" + 
					"Trade limit reached (15/15)" + "\n" +
					"A player can only perform 15 trades a day. Once the limit has been reached, " +
					"the player will not be able to trade anymore for the rest of the day.";
				warning.SetActive (true);
				canPost = false;
			}

//			if (currentPostedTrades < maxTrades) {
//				if(totalPostedTrades >= 10) {
//					canPost = true;
//				}
//				else if(bazaarTickets > 0) {
//					canPost = true;
//				}
//				else {
//					Debug.Log("Can't post! No bazar tickets!");
//					canPost = false;
//				}
//			}
//			else {
//				Debug.Log("Can't post! 8 active posts!");
//				canPost = false;
//			}
			if (canPost) {
				tradeCreation.Reset ();
				myTradableItems.SetCards();
				createPost.SetActive(true);
				bazaarContent.SetActive(false);
			}
		}

	}
}