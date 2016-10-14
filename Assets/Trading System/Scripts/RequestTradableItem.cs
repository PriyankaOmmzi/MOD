using UnityEngine;
using UnityEngine.UI;

namespace Trading {

	public class RequestTradableItem : MonoBehaviour {

		RequestingCardsManager requestingCardsManager;
		GameObject tradableItem;
		Button button;
		InputField inputField;

		void Start() {
			requestingCardsManager = RequestingCardsManager.instance;
			tradableItem = gameObject;
			button = GetComponent<Button> ();
			inputField = GetComponentInChildren<InputField> ();
		}

		public void Set() {
			Debug.Log ("set");
			requestingCardsManager.SaveData (tradableItem);
		}

		public void Deselect() {
			Debug.Log ("deselect");
			requestingCardsManager.DeleteData (tradableItem);
		}

		public void LevelChanged() {
			if (!button.interactable) {
				requestingCardsManager.EnableConfirmButton();
			}
		}

		public void CountChanged() {
			if (inputField.text == "") {
				inputField.text = "1";
			} else if (int.Parse (inputField.text) < 1) {
				inputField.text = "1";
			}
			if (!button.interactable) {
				requestingCardsManager.EnableConfirmButton();
			}
		}

	}

}
