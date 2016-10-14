using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using MiniJSON;

namespace Trading {
	public class MyTradeData : MonoBehaviour {

		[SerializeField]
		Text postID;
		[SerializeField]
		Text status;
		[SerializeField]
		Image statusColor;
		[SerializeField]
		Color complete;
		[SerializeField]
		Color pending;
		[SerializeField]
		Color expired;
		[SerializeField]
		Text timeLeft;
		[SerializeField]
		Text buyerName;
		IDictionary data;
		TimeManager timeManager;
		[SerializeField]
		Transform ratingParent;
		[SerializeField]
		Text trades;
		int totalTrades;

		void Awake() {
			timeManager = TimeManager._instance;
		}

		public void Set(IDictionary tradeData) {
			data = tradeData;
			postID.text = "Post ID: " + data ["trade_id_fkey"].ToString();
			if (data ["bidder_id"].ToString () != "") {
				TradeComplete();
			} else if (data ["trade_type"].ToString () == "EXPIRED") {
				timeLeft.text = "0 hrs 0 mins";
				status.text = "EXPIRED!";
				statusColor.color = expired;
			} else {
				UpdateTimeAndCheckStatus();
			}
		}

		public void UpdateTimeAndCheckStatus() {
			if (data ["bidder_id"].ToString () == "") {
				DateTime endingTime = Convert.ToDateTime (data ["starting_time"].ToString ()).AddDays (2);
				TimeSpan difference = endingTime - timeManager.GetCurrentServerTime ();
				int hours = difference.Days * 24 + difference.Hours;
				int minutes = difference.Minutes;
				if (hours <= 0 && minutes <= 0) {
					timeLeft.text = "0 hrs 0 mins";
					status.text = "EXPIRED!";
					statusColor.color = expired;
				} else {
					timeLeft.text = hours + " hrs " + minutes + " mins";
					status.text = "PENDING...";
					statusColor.color = pending;
					Invoke ("UpdateTimeAndCheckStatus", 1f);
				}
			} else {
				TradeComplete();
			}
		}

		IEnumerator GetPostedTradesCount() {
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag" ,"getCountAllTrades");
			wwwForm.AddField ("user_id", data ["bidder_id"].ToString ().Split (',') [0]);
			WWW countTrades = new WWW(loadingScene.Instance.baseUrl, wwwForm);
			yield return countTrades;
			if (countTrades.text.Contains ("success\":1")) {
				totalTrades = int.Parse((Json.Deserialize (countTrades.text) as IDictionary) ["AllTrades"].ToString());
			} else {
				totalTrades = 0;
			}
			StartCoroutine (GetBoughtTradesCount ());
		}
		
		IEnumerator GetBoughtTradesCount() {
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag" ,"getCountAllTrades");
			wwwForm.AddField ("bidder_id", data ["bidder_id"].ToString ().Split (',') [0]);
			WWW countTrades = new WWW(loadingScene.Instance.baseUrl, wwwForm);
			yield return countTrades;
			if (countTrades.text.Contains ("success\":1")) {
				totalTrades += int.Parse((Json.Deserialize (countTrades.text) as IDictionary) ["AllTrades"].ToString());
			}
			trades.text = "Trades: " + totalTrades;
		}
		
		
		IEnumerator GetBuyerName() {
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "getUserName");
			wwwForm.AddField ("user_id", data ["bidder_id"].ToString ().Split (',') [0]);
			wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
			WWW buyerNameWWW = new WWW (loadingScene.Instance.baseUrl, wwwForm);
			yield return buyerNameWWW;
			Debug.Log (buyerNameWWW.text);
			if (buyerNameWWW.text.Contains ("success\":1")) {
				buyerName.text = (Json.Deserialize (buyerNameWWW.text) as IDictionary) ["User_name"].ToString();
				StartCoroutine (GetPostedTradesCount ());
			} else {
				StartCoroutine (GetBuyerName ());
			}
		}

		void TradeComplete() {
			int rating = 0;
			int.TryParse(data["rating"].ToString(), out rating);
			int temp = 0;
			while(temp<rating) {
				ratingParent.GetChild(temp).GetComponent<Image>().enabled = true;
				temp++;
			}
			totalTrades = 0;
			StartCoroutine (GetBuyerName ());
			status.text = "COMPLETE!";
			statusColor.color = complete;
			timeLeft.text = "0 hrs 0 mins";
		}

		void OnDisable() {
			CancelInvoke("UpdateTimeAndCheckStatus");
		}

	}
}
