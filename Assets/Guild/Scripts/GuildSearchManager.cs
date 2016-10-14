using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MiniJSON;

public class GuildSearchManager : MonoBehaviour {

	[SerializeField]
	InputField searchInputField;
	[SerializeField]
	Dropdown searchDropdown;
	[SerializeField]
	GameObject loadingPopup;
	[SerializeField]
	GuildUIManager guildUIManager;
	[SerializeField]
	GameObject guildPrefab;
	[SerializeField]
	Transform guildResults;
	string guildURL;

	void Awake() {
		guildURL = API.Instance.commonURL;
	}

	public void Reset() {
		ResetContent ();
		searchInputField.text = "";
		searchDropdown.value = 0;
	}

	public void Search() {
		ResetContent ();
		guildUIManager.LoadingPopup (true, "Searching...");
		searchInputField.text = searchInputField.text.TrimStart (' ');
		searchInputField.text = searchInputField.text.TrimEnd (' ');
		StartCoroutine (SearchCoroutine ());
	}

	IEnumerator SearchCoroutine() {
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "guildSearch");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("max_count", "10");
		if (searchInputField.text != "") {
			if (searchDropdown.value == 1) {
				wwwForm.AddField ("name", searchInputField.text);
			} else if (searchDropdown.value == 0) {
				wwwForm.AddField ("user_name", searchInputField.text);
			}
		}
		Debug.Log (PlayerDataParse._instance.playersParam.userId);
		Debug.Log (SystemInfo.deviceUniqueIdentifier);
		Debug.Log (searchInputField.text);
		Debug.Log (searchDropdown.value);
		WWW searchGuild = new WWW (guildURL, wwwForm);
		yield return searchGuild;
		Debug.Log (searchGuild.text);
		if (searchGuild.text.Contains ("\"success\":1")) {
			IList guildData = (Json.Deserialize (searchGuild.text) as IDictionary)["data"] as IList;
			foreach(IDictionary guild in guildData) {
				RectTransform tempGuild = Instantiate (guildPrefab).GetComponent<RectTransform> ();
				tempGuild.SetParent (guildResults);
				tempGuild.localScale = Vector3.one;
				tempGuild.GetComponent<Guild> ().Show (guild);
			}
		}
		guildUIManager.LoadingPopup (false);
	}

	void ResetContent() {
		int temp = 0;
		while (temp<guildResults.childCount) {
			Destroy(guildResults.GetChild(temp).gameObject);
			temp++;
		}
	}

}