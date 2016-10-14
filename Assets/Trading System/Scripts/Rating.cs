using UnityEngine;
using UnityEngine.UI;

public class Rating : MonoBehaviour {

	Image circle;
	[SerializeField]
	Image[] rate;
	[SerializeField]
	Image[] dontRate;
	[SerializeField]
	bool shouldReset;

	void OnEnable() {
		if (shouldReset) {
			foreach (Image circle in rate) {
				circle.color = Color.white;
			}
		}
	}


	void Awake() {
		circle = GetComponent<Image> ();
	}

	public void Rate() {
		foreach (Image circle in rate) {
			circle.color = Color.red;
		}
		foreach (Image circle in dontRate) {
			circle.color = Color.white;
		}
	}

	public void Reset() {
		circle.color = Color.white;
	}

}
