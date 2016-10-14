using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using MiniJSON;

namespace Trading {
	public class MyTradesSearch : MonoBehaviour {

		[SerializeField]
		GameObject searchingPanel;
		string searchTradeTag = "userAllTrades";
		[SerializeField]
		InputField searchBox;
		string searchURL = "http://ec2-54-174-178-121.compute-1.amazonaws.com/mod_web_active_api/common.php";
		[SerializeField]
		GameObject tradePrefab;
		[SerializeField]
		Transform searchResults;

		public void InputEnded() {
			searchBox.text = searchBox.text.TrimStart (' ');
			searchBox.text = searchBox.text.TrimEnd (' ');
		}

		public void SearchTrade() {
			searchingPanel.SetActive (true);
			StartCoroutine (SearchTradeCoroutine ());
		}
		
		IEnumerator SearchTradeCoroutine() {
			ResetData ();
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", searchTradeTag);
			wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
			wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
			wwwForm.AddField ("search", searchBox.text);
			wwwForm.AddField ("max_count", "10");
			WWW myTrades = new WWW (searchURL, wwwForm);
			yield return myTrades;
			Debug.Log (myTrades.text);
			if (myTrades.text.Contains ("success\":1")) {
				RectTransform tempTrade;
				IDictionary tradeData = (IDictionary)Json.Deserialize (myTrades.text);
				IList data = (IList)tradeData ["data"];
				foreach (IDictionary trade in data) {
					tempTrade = Instantiate (tradePrefab).GetComponent<RectTransform> ();
					tempTrade.SetParent (searchResults);
					tempTrade.localScale = Vector3.one;
					tempTrade.GetComponent<MyTradeData> ().Set (trade);
				}
			}
			StartCoroutine (ShowBoughtTrades ());
		}

		IEnumerator ShowBoughtTrades() {
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "getGetAllTradesOnBidderId");
			wwwForm.AddField ("bidder_id", PlayerDataParse._instance.playersParam.userId);
			wwwForm.AddField ("item_name", searchBox.text);
			WWW myTrades = new WWW (loadingScene.Instance.baseUrl, wwwForm);
			yield return myTrades;
			Debug.Log (myTrades.text);
			if (myTrades.text.Contains ("success\":1")) {
				RectTransform tempTrade;
				IDictionary tradeData = (IDictionary)Json.Deserialize (myTrades.text);
				IList data = (IList)tradeData ["Alltrades_detail"];
				foreach (IDictionary trade in data) {
					tempTrade = Instantiate (tradePrefab).GetComponent<RectTransform> ();
					tempTrade.SetParent (searchResults);
					tempTrade.localScale = Vector3.one;
					tempTrade.GetComponent<MyTradeData> ().Set (trade);
				}
			}
			searchingPanel.SetActive (false);
		}

		public void Back() {
			searchBox.text = "";
			ResetData ();
		}

		void ResetData() {
			int temp = 0;
			while (temp<searchResults.childCount) {
				Destroy(searchResults.GetChild(temp).gameObject);
				temp++;
			}
		}

	}
}
