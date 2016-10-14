using UnityEngine;
using UnityEngine.UI;

public class TradeUIManager : MonoBehaviour {

	[SerializeField]
	GameObject loadingPopup;
	[SerializeField]
	Text loadingPopupContent;
	[SerializeField]
	GameObject warningPopup;
	[SerializeField]
	Text warningPopupContent;
	public static TradeUIManager instance;

	void Awake() {
		instance = this;
	}

	public void LoadingPopup(bool value, string content = "") {
		loadingPopupContent.text = content;
		loadingPopup.SetActive (value);
	}

	public void WarningPopup(string content) {
		warningPopupContent.text = content;
		warningPopup.SetActive (true);
	}
}
