using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	[SerializeField]
	Button[] enabledButtons;
	[SerializeField]
	Button[] disabledButtons;
	[SerializeField]
	GameObject[] disabledGameobjects;
	[SerializeField]
	GameObject[] enabledGameobjects;


	void OnEnable() {
		Debug.Log ("reset");
		Reset ();
	}

	public void Reset() {
		foreach (Button button in enabledButtons) {
			button.interactable = true;
		}
		foreach (Button button in disabledButtons) {
			button.interactable = false;
		}
		foreach (GameObject panel in disabledGameobjects) {
			panel.SetActive (false);
		}
		foreach (GameObject panel in enabledGameobjects) {
			panel.SetActive (true);
		}
	}

}