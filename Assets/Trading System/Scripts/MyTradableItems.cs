using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Trading {
	public class MyTradableItems : MonoBehaviour {

		[SerializeField]
		GameObject cardPrefab;
		[SerializeField]
		Transform cardsParent;
		[SerializeField]
		GameObject itemPrefab;
		[SerializeField]
		Transform itemsParent;
		AllTradableItems allTradableItems;
		public static MyTradableItems instance;

		void Awake() {
			instance = this;
		}

		void Start() {
			allTradableItems = AllTradableItems.instance;
			SetItems ();
		}

		/// <summary>
		/// Shows cards of player.
		/// </summary>
		public void SetCards() {
			int temp = 0;
			while (temp<cardsParent.childCount) {
				Destroy(cardsParent.GetChild(temp).gameObject);
				temp++;
			}
			List<CardsManager.CardParameters> myCards = CardsManager._instance.mycards;
			int count = myCards.Count;
			temp = 0;
			RectTransform currentCard = null;

			while(temp < count) {
				Debug.Log ("manish");
				Debug.Log (Resources.Load<Sprite>("Avatars/" + myCards [temp].cardClass));
				Debug.Log (Resources.Load<Sprite> ("Types/" + myCards [temp].type));
				currentCard = Instantiate(cardPrefab).GetComponent<RectTransform>();
				currentCard.SetParent(cardsParent);
				currentCard.localScale = Vector3.one;
				currentCard.GetComponent<Image>().sprite = myCards[temp].cardSpriteFromResources;
				currentCard.FindChild("Border").FindChild("Level").GetComponentInChildren<Text>().text = "" + myCards[temp].max_level;
				currentCard.FindChild("Border").FindChild("Cost").GetComponentInChildren<Text>().text = "" + myCards[temp].cardCost;
				currentCard.FindChild ("Border").FindChild ("Avatar").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Avatars/" + myCards [temp].cardClass);
				currentCard.FindChild ("Border").FindChild ("Type").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Types/" + myCards [temp].type);
				currentCard.GetComponent<Button>().interactable = !LockCards(temp);
				currentCard.FindChild("Locked").gameObject.SetActive(LockCards(temp));
				temp++;
			}
		}

		bool LockCards( int cardID)
		{

			int cardNo = CardsManager._instance.mycards[cardID].card_id_in_playerList;
			if(EmpireManager._instance.castle.primaryCardNo != cardNo && EmpireManager._instance.storage.primaryCardNo != cardNo && EmpireManager._instance.storage.secondaryCardNo != cardNo &&
				EmpireManager._instance.barn.primaryCardNo != cardNo && EmpireManager._instance.barn.secondaryCardNo != cardNo &&
				EmpireManager._instance.goldMine.primaryCardNo != cardNo && EmpireManager._instance.goldMine.secondaryCardNo != cardNo &&
				EmpireManager._instance.barracks.primaryCardNo != cardNo && EmpireManager._instance.barracks.secondaryCardNo != cardNo &&
				EmpireManager._instance.trainingGround.primaryCardNo != cardNo && EmpireManager._instance.trainingGround.secondaryCardNo != cardNo
			&& EmpireManager._instance.prison.secondaryCardNo != cardNo && EmpireManager._instance.prison.primaryCardNo != cardNo
			&& EmpireManager._instance.gate.secondaryCardNo != cardNo && EmpireManager._instance.gate.primaryCardNo != cardNo
			&& CardsManager._instance.mycards[cardID].isLocked == false)

			{
				bool selectedInQuest = false;
				for (int c = 0; c < loadingScene.Instance.myQuestFormation.cardDecks.Count; c++) {
					for(int  d= 0 ; d < loadingScene.Instance.myQuestFormation.cardDecks[c].cardRows.Count ; d++)
					{
						for(int e = 0 ; e < loadingScene.Instance.myQuestFormation.cardDecks[c].cardRows[d].cardIdsForRow.Count ; e++)
						{
							if(cardNo == loadingScene.Instance.myQuestFormation.cardDecks[c].cardRows[d].cardIdsForRow[e])
							{
								selectedInQuest = true;
								break;
							}
						}
					}
				}

				if(!selectedInQuest)
				{
					return false; // not locked
				}
				else
				{
					return true; // locked
				}
			}
			else
			{
				//locked
				return true;
			}
			
		}


		/// <summary>
		/// Show items of player.
		/// </summary>
		void SetItems() {
			RectTransform currentItem = null;
			foreach(TradableItem tradableItem in allTradableItems.tradableItems) {
				Sprite sprite = Resources.Load<Sprite>("items/" + tradableItem.name);
				if(sprite != null) {
					currentItem = Instantiate(itemPrefab).GetComponent<RectTransform>();
					currentItem.SetParent(itemsParent);
					currentItem.localScale = Vector3.one;
					currentItem.GetComponent<Image>().sprite = sprite;
				}
			}
		}

		public void Reset() {
			int temp = 0;
			while (temp<itemsParent.childCount) {
				itemsParent.GetChild(temp).GetComponent<Button>().interactable = true;
				itemsParent.GetChild(temp).FindChild("Deselect").GetComponent<Image>().raycastTarget = false;
				itemsParent.GetChild(temp).FindChild("InputField").GetComponent<InputField>().text = "1";
				temp++;
			}
			temp = 0;
			while (temp<cardsParent.childCount) {
				Destroy(cardsParent.GetChild(temp).gameObject);
				temp++;
			}
		}

	}
}