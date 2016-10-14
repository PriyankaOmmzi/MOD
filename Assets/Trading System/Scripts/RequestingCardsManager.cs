using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Trading {

	public class RequestingCardsManager : MonoBehaviour {

		public static RequestingCardsManager instance;
		TradeCreation tradeCreation;
		
		int maxTradableItems;

		[SerializeField]
		Text requestedCardsText;
		[SerializeField]
		Transform cardsParent;
		[SerializeField]
		Transform itemsParent;
		Sprite spriteToBeSelected;
		GameObject selectedItem;


		[SerializeField]
		Button confirmButton;
		GameObject newTradableItem;
		Button previousTradableItem;
		List<GameObject> currentTradableItems;
		List<GameObject> previousTradableItems;

		bool isItem;

		[SerializeField]
		Button cardsButton;
		[SerializeField]
		Button itemsButton;

		void Awake() {
			instance = this;
			maxTradableItems = 5;
			currentTradableItems = new List<GameObject> ();
			previousTradableItems = new List<GameObject> ();
		}

		void Start() {
			tradeCreation = TradeCreation.instance;
		}

		public void Reset() {
			currentTradableItems.Clear ();
			requestedCardsText.text = "0/" + maxTradableItems;
			cardsButton.onClick.Invoke ();
			int temp = 0;
			while (temp<itemsParent.childCount) {
				itemsParent.GetChild(temp).GetComponent<Image>().sprite = null;
				itemsParent.GetChild(temp).GetComponent<Image>().color = Color.grey;
				itemsParent.GetChild (temp).GetComponent<AssociatedTradeValue> ().text.text = "";
				if(temp == 0) {
					itemsParent.GetChild (temp).GetComponent<Button> ().interactable = true;
				}
				else {
					itemsParent.GetChild (temp).GetComponent<Button> ().interactable = false;
				}
				temp++;
			}
			temp = 0;
			while (temp<cardsParent.childCount) {
				cardsParent.GetChild(temp).GetComponent<Image>().sprite = null;
				cardsParent.GetChild(temp).GetComponent<Image>().color = Color.grey;
				if(temp == 0) {
					cardsParent.GetChild (temp).GetComponent<Button> ().interactable = true;
				}
				else {
					cardsParent.GetChild (temp).GetComponent<Button> ().interactable = false;
				}
				cardsParent.GetChild (temp).FindChild ("Border").FindChild ("Avatar").GetComponent<Image> ().sprite = null;
				cardsParent.GetChild (temp).FindChild ("Border").FindChild ("Cost").GetComponentInChildren<Text> ().text = "";
				cardsParent.GetChild (temp).FindChild ("Border").FindChild ("Level").GetComponentInChildren<Text> ().text = "";
				cardsParent.GetChild (temp).FindChild ("Border").gameObject.SetActive (false);
				temp++;
			}
		}

		void EnableTradableItems(int itemsCount, int cardsCount) {
			DisableTradableItems ();
			int temp = 0;
			while (temp<itemsCount) {
				itemsParent.GetChild(temp).GetComponent<Button>().interactable = true;
				temp++;
			}

			temp = 0;
			while (temp<cardsCount) {
				cardsParent.GetChild(temp).GetComponent<Button>().interactable = true;
				temp++;
			}

			if (currentTradableItems.Count < maxTradableItems) {
				cardsParent.GetChild(cardsCount).GetComponent<Button>().interactable = true;
				itemsParent.GetChild(itemsCount).GetComponent<Button>().interactable = true;
			}
		}

		void DisableTradableItems() {
			int temp = 0;
			while (temp<itemsParent.childCount) {
				itemsParent.GetChild(temp).GetComponent<Button>().interactable = false;
				temp++;
			}
			temp = 0;
			while (temp<cardsParent.childCount) {
				cardsParent.GetChild(temp).GetComponent<Button>().interactable = false;
				temp++;
			}
		}


		public void SetItemData(GameObject item) {
			isItem = true;
			SetData (item);
		}

		public void SetCardData(GameObject card) {
			isItem = false;
			SetData (card);
		}


		/// <summary>
		/// Called when a card in requested panel of create trade is clicked.
		/// Sets the data related to the card.
		/// </summary>
		public void SetData(GameObject clickedGameobject) {
//			spriteToBeSelected = clickedGameobject.GetComponent<Image> ().sprite;
//			if (spriteToBeSelected == null) {
				previousTradableItems.Clear();
				foreach(GameObject tradableItem in currentTradableItems) {
					previousTradableItems.Add(tradableItem);
				}
//			} else {
//				selectedItem = clickedGameobject;
//				foreach(GameObject tradableItem in currentTradableItems) {
//					if(tradableItem.GetComponent<Image>().sprite == selectedItem.GetComponent<Image>().sprite) {
//						previousTradableItem = tradableItem.GetComponent<Button>();
//					}
//				}
//				newTradableItem = null;
//			}
			confirmButton.interactable = false;
		}

		public void SaveData(GameObject tradableItem) {
//			if (spriteToBeSelected == null) {
				if (currentTradableItems.Count < maxTradableItems && !currentTradableItems.Contains(tradableItem)) {
					currentTradableItems.Add (tradableItem);
					Select(tradableItem.transform);
				}
//			} else if(newTradableItem == null) {
//				newTradableItem = tradableItem;
//				Deselect(previousTradableItem.transform);
//				Select(tradableItem.transform);
//			}
			confirmButton.interactable = true;
		}

		public void DeleteData(GameObject tradableItem) {
//			if (spriteToBeSelected == null) {
			if (currentTradableItems.Contains (tradableItem)) {
				currentTradableItems.Remove (tradableItem);
				Deselect (tradableItem.transform);
			} else {
				Debug.LogError ("doesn't exist, how did it get selected?");
			}
				confirmButton.interactable = true;
//			}
		}

		void Deselect(Transform tradableItem) {
			tradableItem.GetComponent<Button> ().interactable = true;
			tradableItem.transform.FindChild ("Deselect").GetComponent<Image> ().raycastTarget = false;
		}

		void Select(Transform tradableItem) {
			tradableItem.GetComponent<Button>().interactable = false;
			tradableItem.transform.FindChild("Deselect").GetComponent<Image>().raycastTarget = true;
		}

		public void Confirm() {
//			if (spriteToBeSelected!=null && newTradableItem != null) {
//				currentTradableItems.Remove (previousTradableItem.gameObject);
//				currentTradableItems.Add (newTradableItem);
//			}
			UpdateItems();
			tradeCreation.ToggleCreateButton ();
		}

		void UpdateItems() {
			int itemIndex = 0;
			int cardIndex = 0;
			if (isItem) {
				foreach (GameObject tradableItem in currentTradableItems) {
					if (tradableItem.name == "Item") {
						itemsParent.GetChild (itemIndex).GetComponent<Image> ().sprite = tradableItem.GetComponent<Image> ().sprite;
						itemsParent.GetChild (itemIndex).GetComponent<Image> ().color = Color.white;
						itemsParent.GetChild (itemIndex).GetComponent<AssociatedTradeValue> ().text.text = tradableItem.GetComponentInChildren<InputField> ().text;
						itemIndex++;
					}
					int temp = itemIndex;
					while(temp<itemsParent.childCount) {
						itemsParent.GetChild (temp).GetComponent<Image> ().sprite = null;
						itemsParent.GetChild (temp).GetComponent<Image> ().color = Color.grey;
						itemsParent.GetChild (temp).GetComponent<AssociatedTradeValue> ().text.text = "";
						temp++;
					}
				}
				cardIndex = currentTradableItems.Count - itemIndex;
			} else {
				Debug.Log ("cards updated");
				foreach (GameObject tradableItem in currentTradableItems) {
					if(tradableItem.name == "Card") {
						cardsParent.GetChild (cardIndex).FindChild ("Border").gameObject.SetActive (true);
						cardsParent.GetChild (cardIndex).GetComponent<Image> ().sprite = tradableItem.GetComponent<Image> ().sprite;
						cardsParent.GetChild (cardIndex).GetComponent<Image> ().color = Color.white;
						cardsParent.GetChild (cardIndex).FindChild ("Border").FindChild ("Avatar").GetComponent<Image> ().sprite = tradableItem.transform.FindChild ("Border").FindChild ("Avatar").GetComponent<Image> ().sprite;
						cardsParent.GetChild (cardIndex).FindChild ("Border").FindChild ("Cost").GetComponentInChildren<Text> ().text = tradableItem.transform.FindChild ("Border").FindChild ("Cost").GetComponentInChildren<Text> ().text;
						cardsParent.GetChild (cardIndex).FindChild ("Border").FindChild ("Level").GetComponentInChildren<Text> ().text = tradableItem.transform.FindChild ("Border").FindChild ("Level").GetComponent<Dropdown> ().captionText.text;
						cardIndex++;
					}
				}
				int temp = cardIndex;
				while(temp<cardsParent.childCount) {
					cardsParent.GetChild (cardIndex).FindChild ("Border").gameObject.SetActive (false);
					cardsParent.GetChild (temp).GetComponent<Image> ().sprite = null;
					cardsParent.GetChild (temp).GetComponent<Image> ().color = Color.grey;
					cardsParent.GetChild (cardIndex).FindChild ("Border").FindChild ("Avatar").GetComponent<Image> ().sprite = null;
					cardsParent.GetChild (cardIndex).FindChild ("Border").FindChild ("Cost").GetComponentInChildren<Text> ().text = "";
					cardsParent.GetChild (cardIndex).FindChild ("Border").FindChild ("Level").GetComponentInChildren<Text> ().text = "";
					temp++;
				}
				itemIndex = currentTradableItems.Count - cardIndex;
			}
			EnableTradableItems (itemIndex, cardIndex);
			requestedCardsText.text = currentTradableItems.Count + "/" + maxTradableItems;
		}

		public void Back() {
			confirmButton.interactable = false;
//			if (spriteToBeSelected == null) {
				foreach(GameObject card in currentTradableItems) {
					Deselect(card.transform);
				}
				foreach(GameObject card in previousTradableItems) {
					Select(card.transform);
				}

				currentTradableItems.Clear();
				foreach(GameObject tradableItem in previousTradableItems) {
					currentTradableItems.Add(tradableItem);
				}

//			} else if(newTradableItem != null) {
//				Select(previousTradableItem.transform);
//				Deselect(newTradableItem.transform);
//			}
		}

		public void EnableConfirmButton() {
			confirmButton.interactable = true;
		}

	}

}