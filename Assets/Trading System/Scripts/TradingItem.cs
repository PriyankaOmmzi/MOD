using UnityEngine;
using UnityEngine.UI;

namespace Trading {

	public class TradingItem : MonoBehaviour {

		TradeCreation tradeCreation;
		Image image;
		Transform item;
		Button button;
		InputField inputField;
		
		void Start() {
			tradeCreation = TradeCreation.instance;
			image = GetComponent<Image> ();
			item = transform;
			button = GetComponent<Button> ();
			inputField = GetComponentInChildren<InputField> ();
		}

		public void Set() {
			tradeCreation.SetTradingItem (image.sprite, item.GetSiblingIndex(), item);
		}

		public void Deselect() {
			tradeCreation.DeselectTradingItem ();
		}

		public void CountChanged() {
			if (inputField.text == "") {
				inputField.text = "1";
			} else if (int.Parse (inputField.text) < 1) {
				inputField.text = "1";
			}
			if (!button.interactable) {
				tradeCreation.EnableConfirmButton();
			}
		}
	}

}
