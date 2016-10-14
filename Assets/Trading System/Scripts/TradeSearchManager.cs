using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using MiniJSON;

namespace Trading {
	public class TradeSearchManager : MonoBehaviour {

		[SerializeField]
		GameObject warning;
		[SerializeField]
		GameObject loadingPanel;
		[SerializeField]
		Text loadingPanelText;
		string searchTradeTag = "searchTrades";
		[SerializeField]
		InputField searchBox;
		[SerializeField]
		InputField postID;
		[SerializeField]
		InputField guild;
		[SerializeField]
		Dropdown buyOrSell;
		[SerializeField]
		List<string> buyOrSellOptions;
		[SerializeField]
		Dropdown rating;
		[SerializeField]
		List<string> ratingOptions;
		[SerializeField]
		InputField cost;
		[SerializeField]
		Dropdown type;
		[SerializeField]
		List<string> typeOptions;
		[SerializeField]
		Dropdown selectedClass;
		[SerializeField]
		List<string> classOptions;
		[SerializeField]
		Dropdown rarity;
		[SerializeField]
		List<string> rarityOptions;
		[SerializeField]
		Dropdown items;
		[SerializeField]
		List<string> itemOptions;
		string searchURL = "http://ec2-54-174-178-121.compute-1.amazonaws.com/mod_web_active_api/common.php";
		[SerializeField]
		GameObject tradePrefab;
		[SerializeField]
		Transform searchResults;
		public static TradeSearchManager instance;
		int count;
		int maxCount;
		public int dailyTrades;

		void Awake() {
			instance = this;
		}

		public void InputEnded() {
			searchBox.text = searchBox.text.TrimStart (' ');
			searchBox.text = searchBox.text.TrimEnd (' ');
		}

		public void SetWarningPanel(bool value) {
			warning.SetActive (value);
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
				SetPanel (false);
			} else {
				StartCoroutine(GetDailyTrades());
			}
		}

		void Start() {
			AddDropDownOptions ();
		}

		void AddDropDownOptions() {
			type.AddOptions (typeOptions);
			selectedClass.AddOptions (classOptions);
			rarity.AddOptions (rarityOptions);
			rating.AddOptions (ratingOptions);
			buyOrSell.AddOptions (buyOrSellOptions);
			itemOptions = new List<string> ();
			itemOptions.Add ("");
			foreach (TradableItem item in AllTradableItems.instance.tradableItems) {
				if (Resources.Load<Sprite>("items/" + item.name) != null) {
					itemOptions.Add (item.name);
				}
			}
			items.AddOptions (itemOptions);
		}

		public void SetPanel(bool value, string text = "") {
			loadingPanelText.text = text;
			loadingPanel.SetActive (value);
		}

		public void SearchTrade() {
			if (searchBox.text != "" && items.value != 0 && searchBox.text.ToLower() != items.captionText.text.ToLower()) {
				ResetData ();
				return;
			}
			SetPanel (true, "Searching...");
			StartCoroutine (SearchTradeCoroutine ());
		}
		
		IEnumerator SearchTradeCoroutine() {
			ResetData ();
			Debug.Log (PlayerDataParse._instance.playersParam.userId);
			Debug.Log (SystemInfo.deviceUniqueIdentifier);
			Debug.Log (guild.text);
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", searchTradeTag);
			wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
			wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
			if (buyOrSell.value == 0) {
				if (searchBox.text != "") {
					wwwForm.AddField ("item_name", searchBox.text);
				} else {
					wwwForm.AddField ("item_name", items.captionText.text);
				}
			} else {
				if (searchBox.text != "") {
					wwwForm.AddField ("requested_items", searchBox.text);
				} else {
					wwwForm.AddField ("requested_items", items.captionText.text);
				}
			}
			wwwForm.AddField ("post_id", postID.text);
			wwwForm.AddField ("guild_name", guild.text);
			wwwForm.AddField ("max_count", "10");
			wwwForm.AddField ("rating", rating.value);
			WWW www = new WWW (searchURL, wwwForm);
			yield return www;
			Debug.Log (www.text);
			
			if (!www.text.Contains ("error_msg")) {
				if (www.text != "") {
					IDictionary tradeData = (IDictionary)Json.Deserialize (www.text);
					IList data = (IList)tradeData ["data"];
					count = 0;
					maxCount = data.Count;
					foreach (IDictionary trade in data) {
						if (int.Parse(trade ["rarity"].ToString()) != 0) {
							StartCoroutine (GetCardData (trade));
						} else {
							count++;
							RectTransform tempTrade = Instantiate (tradePrefab).GetComponent<RectTransform> ();
							tempTrade.SetParent (searchResults);
							tempTrade.localScale = Vector3.one;
							tempTrade.GetComponent<TradeData> ().Set (trade);
							if(count == maxCount) {
								StartCoroutine (GetDailyTrades ());
							}
						}
					}
				} else if (Application.internetReachability != NetworkReachability.NotReachable) {
					StartCoroutine (SearchTradeCoroutine ());
				}
				else {
					StartCoroutine (GetDailyTrades ());
				}
			} else {
				StartCoroutine (GetDailyTrades ());
			}

		}


		IEnumerator GetCardData(IDictionary trade) {
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "getPlayerCardsDataSearch");
			wwwForm.AddField ("user_id", PlayerDataParse._instance.ID (trade ["user_id"].ToString ()));
			wwwForm.AddField("card_no_in_players_list", trade ["item_of_trade"].ToString ());
			WWW cardData = new WWW (loadingScene.Instance.baseUrl, wwwForm);
			yield return cardData;
			Debug.Log (cardData.text);
			if (cardData.text.Contains ("success\":1")) {
				count++;
				IDictionary data = ((Json.Deserialize(cardData.text) as IDictionary)["Player_card"] as IList)[0] as IDictionary;
				if (ShouldShow (data)) {
					RectTransform tempTrade = Instantiate (tradePrefab).GetComponent<RectTransform> ();
					tempTrade.SetParent (searchResults);
					tempTrade.localScale = Vector3.one;
					tempTrade.GetComponent<TradeData> ().Set (trade);
				}
				if(count == maxCount) {
					StartCoroutine (GetDailyTrades ());
				}
			} else {
				StartCoroutine(GetCardData(trade));
			}
		}

		bool ShouldShow(IDictionary data) {
			if(selectedClass.value == 0 && rarity.value == 0 && type.value == 0 && cost.text == "") {
				return true;
			}
			if (selectedClass.value != 0 && data ["class"].ToString () != selectedClass.captionText.text) {
				return false;
			}
			if (rarity.value != 0 && data ["rarity"].ToString () != rarity.captionText.text) {
				return false;
			}
			if (type.value != 0 && data ["type"].ToString () != type.captionText.text) {
				return false;
			}
			if (cost.text != "" && data ["card_cost"].ToString () != cost.text) {
				return false;
			}
			return true;
		}

		public void Back() {
			searchBox.text = "";
			ResetData ();
		}

		public void ResetUI() {
			searchBox.text = "";
			cost.text = "";
			postID.text = "";
			guild.text = "";
			type.value = 0;
			buyOrSell.value = 0;
			selectedClass.value = 0;
			rarity.value = 0;
			items.value = 0;
			rating.value = 0;
		}

		public void ResetData() {
			int temp = 0;
			while (temp<searchResults.childCount) {
				Destroy(searchResults.GetChild(temp).gameObject);
				temp++;
			}
		}

	}
}
