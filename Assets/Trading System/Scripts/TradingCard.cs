using UnityEngine;
using UnityEngine.UI;

namespace Trading {

	public class TradingCard : MonoBehaviour {

		TradeCreation tradeCreation;
		Image image;
		Transform card;

		void Start() {
			tradeCreation = TradeCreation.instance;
			image = GetComponent<Image> ();
			card = transform;
		}

		public void Set() {
			tradeCreation.SetTradingCard (image.sprite, card.GetSiblingIndex(), card);
		}

		public void Deselect() {
			tradeCreation.DeselectTradingItem ();
		}

	}

}
