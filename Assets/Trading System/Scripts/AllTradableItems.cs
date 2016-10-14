
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Collections;
using MiniJSON;

namespace Trading {

	[Serializable]
	public class TradableItem {
		
		public string name;
		public int iD;

	}

	public class AllTradableItems : MonoBehaviour {

		[SerializeField]
		GameObject cardPrefab;
		[SerializeField]
		Transform cardsParent;
		[SerializeField]
		GameObject itemPrefab;
		[SerializeField]
		Transform itemsParent;
		[SerializeField]
		string baseURL;
		public List<TradableItem> tradableItems;
		public static AllTradableItems instance;

		void Awake() {
			instance = this;
			AssignIDs ();
		}

		void AssignIDs() {
			int temp = 0;
			foreach (TradableItem tradableItem in tradableItems) {
				tradableItem.iD = temp;
				temp++;
			}
		}

		void Start() {
			StartCoroutine (FetchAllCards ());
		}

		IEnumerator FetchAllCards() {
			WWWForm wwwForm = new WWWForm ();
			wwwForm.AddField ("tag", "doGetRequiredCardData");
			WWW wwwCards = new WWW(baseURL , wwwForm);
			yield return wwwCards;
			Debug.Log (wwwCards.text);
			if (wwwCards.text.Contains("\"success\":1")) {
				IList cardsData = ((IDictionary)Json.Deserialize (wwwCards.text) as IDictionary)["Players"] as IList;
				RectTransform currentCard = null;
				foreach (IDictionary card in cardsData) {
					if (Resources.Load<Sprite> ("images/" + card ["name"].ToString ()) != null) {
						currentCard = Instantiate (cardPrefab).GetComponent<RectTransform> ();
						currentCard.name = "Card";
						currentCard.SetParent (cardsParent);
						currentCard.localScale = Vector3.one;
						currentCard.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("images/" + card ["name"].ToString ());
						currentCard.FindChild ("Border").FindChild ("Avatar").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Avatars/" + card ["category"].ToString ());
						currentCard.FindChild ("Border").FindChild ("Cost").GetComponentInChildren<Text> ().text = card ["cost"].ToString ();
					}
				}
				FetchAllItems ();
			} else if (Application.internetReachability != NetworkReachability.NotReachable) {
					Debug.Log ("fetching again");
					StartCoroutine (FetchAllCards ());
			}
		}

		void FetchAllItems() {
			RectTransform currentItem = null;
			foreach(TradableItem tradableItem in tradableItems) {
				Sprite sprite = Resources.Load<Sprite>("items/" + tradableItem.name);
				if(sprite != null) {
					currentItem = Instantiate(itemPrefab).GetComponent<RectTransform>();
					currentItem.name = "Item";
					currentItem.SetParent(itemsParent);
					currentItem.localScale = Vector3.one;
					currentItem.GetComponent<Image>().sprite = sprite;
				}
			}
		}

		//TODO
		public int ItemID(string name) {
			foreach (TradableItem tradableItem in tradableItems) {
				if(tradableItem.name == name) {
					Debug.Log(tradableItem.iD);
					return tradableItem.iD;
				}
			}
			return -1;
		}

		public void Reset() {
			int temp = 0;
			while (temp<cardsParent.childCount) {
				cardsParent.GetChild(temp).GetComponent<Button>().interactable = true;
				cardsParent.GetChild(temp).FindChild("Deselect").GetComponent<Image>().raycastTarget = false;
				cardsParent.GetChild (temp).FindChild ("Border").FindChild ("Level").GetComponent<Dropdown> ().value = 0;
				temp++;
			}
			temp = 0;
			while (temp<itemsParent.childCount) {
				itemsParent.GetChild(temp).GetComponent<Button>().interactable = true;
				itemsParent.GetChild(temp).FindChild("Deselect").GetComponent<Image>().raycastTarget = false;
				itemsParent.GetChild(temp).FindChild("InputField").GetComponent<InputField>().text = "1";
				temp++;
			}
		}

	}
	
}