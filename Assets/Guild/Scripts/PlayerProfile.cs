using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MiniJSON;

public class PlayerProfile : MonoBehaviour {

	[SerializeField]
	Text friendsCount;
	[SerializeField]
	Transform reviews;
	[SerializeField]
	Text tradesCount;
	string commonURL;

	void Awake() {
		commonURL = API.Instance.commonURL;
	}

	void OnEnable() {
		loadingScene.Instance.loader.SetActive (true);
		StartCoroutine (GetPlayerData ());
	}

	IEnumerator GetPlayerData() {
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "returnCountsOfTFR");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		WWW playerData = new WWW (commonURL, wwwForm);
		yield return playerData;
		Debug.Log (playerData.text);
//		{"success":1,"msg":"User Data:","data":{"trades_count":0,"friends_count":"2","rating_average":0}}
		if (playerData.text.Contains ("\"success\":1")) {
			IDictionary data = (Json.Deserialize (playerData.text) as IDictionary) ["data"] as IDictionary;
			tradesCount.text = data ["trades_count"].ToString ();
			friendsCount.text = data ["friends_count"].ToString ();
			if (data ["rating_average"] != null) {
				int rating = Mathf.FloorToInt (float.Parse (data ["rating_average"].ToString ()));
				int temp = 0;
				while (temp < rating) {
					reviews.GetChild (temp).GetComponent<Image> ().enabled = true;
					temp++;
				}
				while (temp < reviews.childCount) {
					reviews.GetChild (temp).GetComponent<Image> ().enabled = false;
					temp++;
				}
			} else {
				int temp = 0;
				while (temp < reviews.childCount) {
					reviews.GetChild (temp).GetComponent<Image> ().enabled = false;
					temp++;
				}
			}
		} else {
			tradesCount.text = "0";
			friendsCount.text = "0";
			int temp = 0;
			while (temp < reviews.childCount) {
				reviews.GetChild (temp).GetComponent<Image> ().enabled = false;
				temp++;
			}
		}
		loadingScene.Instance.loader.SetActive (false);
		//close loading popup
	}

}
