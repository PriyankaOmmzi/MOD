
using UnityEngine;
using System.Collections;
using MiniJSON;

namespace Trading {

	public class MyTrades : MonoBehaviour {

		[SerializeField]
		GameObject loading;
		string searchURL = "http://ec2-54-174-178-121.compute-1.amazonaws.com/mod_web_active_api/common.php";
		[SerializeField]
		GameObject tradePrefab;
		[SerializeField]
		Transform myTradesParent;

		public void Refresh() {
			Reset ();
			StartCoroutine (ShowMyPostedTrades ());
		}

		public void Reset() {
			int temp = 0;
			while (temp<myTradesParent.childCount) {
				Destroy(myTradesParent.GetChild(temp).gameObject);
				temp++;
			}
		}

		void OnEnable() {
			Refresh ();
		}

		IEnumerator ShowMyPostedTrades() {
			loading.SetActive (true);
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "userAllTrades");
			wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
			wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
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
					tempTrade.SetParent (myTradesParent);
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
			WWW myTrades = new WWW (loadingScene.Instance.baseUrl, wwwForm);
			yield return myTrades;
			Debug.Log (myTrades.text);
			if (myTrades.text.Contains ("success\":1")) {
				RectTransform tempTrade;
				IDictionary tradeData = (IDictionary)Json.Deserialize (myTrades.text);
				IList data = (IList)tradeData ["Alltrades_detail"];
				foreach (IDictionary trade in data) {
					tempTrade = Instantiate (tradePrefab).GetComponent<RectTransform> ();
					tempTrade.SetParent (myTradesParent);
					tempTrade.localScale = Vector3.one;
					tempTrade.GetComponent<MyTradeData> ().Set (trade);
				}
			}
			loading.SetActive (false);
		}

	}
}