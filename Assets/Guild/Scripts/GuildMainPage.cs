using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MiniJSON;

public class GuildMainPage : MonoBehaviour {

	[SerializeField]
	InputField guildName;
	[SerializeField]
	InputField guildPrefix;
	[SerializeField]
	InputField newGuildNote;
	[SerializeField]
	InputField newGuildAnnouncement;
	[SerializeField]
	InputField newGuildRule;
	[SerializeField]
	Text guildNotes;
	[SerializeField]
	Text guildAnnouncements;
	[SerializeField]
	Text guildRules;
	[SerializeField]
	Text guildRank;
	[SerializeField]
	Text guildLeader;
	[SerializeField]
	Text guildMembersCount;
	[SerializeField]
	Text guildTimezone;
	[SerializeField]
	Image guildEmblem;
	IDictionary guildData;
	string commonURL;
	[SerializeField]
	GameObject guildMembers;
	GuildUIManager guildUIManager;
	[SerializeField]
	Button applicants;
	[SerializeField]
	GameObject guildApplicants;
	[SerializeField]
	GameObject[] disabledPanels;
	[SerializeField]
	InputField gold;
	[SerializeField]
	InputField wheat;
	[SerializeField]
	GameObject[] content;
	Vector3[] positions;
	int currentContent;
	[SerializeField]
	Button leftButton;
	[SerializeField]
	Button rightButton;
	[SerializeField]
	Button[] addNewButtons;

	void Awake() {
		positions = new Vector3[content.Length];
		int temp = 0;
		while (temp < content.Length) {
			positions [temp] = content [temp].transform.position;
			temp++;
		}
		commonURL = API.Instance.commonURL;
		guildUIManager = GuildUIManager.instance;
	}

	public void GuildNameChanged() {
		if (guildName.text.Length > 20) {
			guildName.text = guildName.text.Remove (20);
		}
	}

	public void GuildNameEditingComplete() {
		if (guildName.text != "") {
			guildUIManager.LoadingPopup (true, "Updating guild name...");
			StartCoroutine (UpdateGuildData ("name", guildName.text));
		} else {
			guildUIManager.WarningPopup ("Guild name can't be empty.");
			guildName.text = guildData ["name"].ToString ();
		}
	}

	public void GuildPrefixChanged() {
		if (guildPrefix.text.Length > 20) {
			guildPrefix.text = guildPrefix.text.Remove (20);
		}
	}

	public void GuildPrefixEditingComplete() {
		if (guildPrefix.text != "") {
			guildUIManager.LoadingPopup (true, "Updating guild prefix...");
			StartCoroutine (UpdateGuildData ("prefix", guildPrefix.text));
		} else {
			guildUIManager.WarningPopup ("Guild prefix can't be empty.");
			guildPrefix.text = guildData ["prefix"].ToString ();
		}
	}

	public void DonateWheatChanged() {
		if (wheat.text.Length > 10) {
			wheat.text = wheat.text.Remove (10);
		}
	}

	public void DonateGoldChanged() {
		if (gold.text.Length > 10) {
			gold.text = gold.text.Remove (10);
		}
	}

	public void PostNotes() {
		guildNotes.text += "\n" + newGuildNote.text;
		guildUIManager.LoadingPopup (true, "Updating guild notes...");
		StartCoroutine (UpdateGuildData ("notes", guildNotes.text));
	}

	public void PostAnnouncements() {
		guildAnnouncements.text += "\n" + newGuildAnnouncement.text;
		guildUIManager.LoadingPopup (true, "Updating guild announcements...");
		StartCoroutine (UpdateGuildData ("announcements", guildAnnouncements.text));
	}

	public void PostRules() {
		guildRules.text += "\n" + newGuildRule.text;
		guildUIManager.LoadingPopup (true, "Updating guild rules...");
		StartCoroutine (UpdateGuildData ("rules", guildRules.text));
	}

	IEnumerator UpdateGuildData(string key, string value) {
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "updateGuild");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("gid", PlayerParameters._instance.myPlayerParameter.guildID);
		wwwForm.AddField (key, value);
		WWW updateGuild = new WWW (commonURL, wwwForm);
		yield return updateGuild;
		Debug.Log (updateGuild.text);
		guildUIManager.LoadingPopup (false);
		if (updateGuild.text.Contains ("\"success\":1")) {
			guildUIManager.WarningPopup ("Guild " + key + " updated successfully.");
		} else if (updateGuild.text.Contains ("error_msg\":\"prefix already taken")) {
			guildUIManager.WarningPopup ("Guild prefix already exists.");
		}  else if (updateGuild.text.Contains ("error_msg\":\"name already taken")) {
			guildUIManager.WarningPopup ("Guild name already exists.");
		} else {
			guildUIManager.WarningPopup ("Guild " + key + " couldn't be updated. Please try again.");
		}
	}

	void OnEnable() {
		currentContent = 1;
		int temp = 0;
		while (temp < content.Length) {
			content [temp].transform.position = positions[temp];
			temp++;
		}
		SetButtons ();
		foreach (GameObject panel in disabledPanels) {
			panel.SetActive (false);
		}
	}

	public void Left() {
		ResetAddNewButtons ();
		leftButton.interactable = rightButton.interactable = false;
		iTween.MoveTo (content [currentContent], iTween.Hash ("x", positions [2].x, "time", 1f));
		currentContent--;
		iTween.MoveTo (content [currentContent], iTween.Hash ("x", positions [1].x, "time", 1f));
		Invoke ("SetButtons", 1f);
		Invoke ("SetAddNewButtons", 1f);
	}

	void SetAddNewButtons() {
		if (IsGuildLeaderOrViceGuildLeader ()) {
			foreach (Button button in addNewButtons) {
				button.interactable = true;
			}
		}
	}

	void ResetAddNewButtons() {
		if (IsGuildLeaderOrViceGuildLeader ()) {
			foreach (Button button in addNewButtons) {
				button.interactable = false;
			}
		}
	}

	void SetButtons() {
		switch (currentContent) {
		case 0:
			leftButton.interactable = false;
			rightButton.interactable = true;
			break;
		case 1:
			leftButton.interactable = rightButton.interactable = true;
			break;
		case 2:
			leftButton.interactable = true;
			rightButton.interactable = false;
			break;
		}
	}

	public void Right() {
		ResetAddNewButtons ();
		if (IsGuildLeaderOrViceGuildLeader ()) {
			foreach (Button button in addNewButtons) {
				button.interactable = false;
			}
		}
		leftButton.interactable = rightButton.interactable = false;
		iTween.MoveTo (content [currentContent], iTween.Hash ("x", positions [0].x, "time", 1f));
		currentContent++;
		iTween.MoveTo (content [currentContent], iTween.Hash ("x", positions [1].x, "time", 1f));
		Invoke ("SetButtons", 1f);
		Invoke ("SetAddNewButtons", 1f);
	}

	void Reset() {
		guildName.text = guildPrefix.text = guildRank.text = guildLeader.text = guildMembersCount.text = guildTimezone.text = "";
		guildEmblem.sprite = null;
	}

	public void Fetch() {
		guildUIManager.LoadingPopup (true, "Loading...");
		Reset ();
		StartCoroutine (FetchGuildData ());
	}

	IEnumerator FetchGuildData() {
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "guildSearch");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("gid", PlayerParameters._instance.myPlayerParameter.guildID);
		WWW guildDataWWW = new WWW (commonURL, wwwForm);
		yield return guildDataWWW;
		Debug.Log (guildDataWWW.text);
		//"id":"28","prefix":"Gd28","name":"j18","user_id":"3","player_ids":null,"level":"1","majority_time_zone":"Asia","player_roles":null,"auto_accept":"False","gold":"0","wheat":"0","rules":null
		if (guildDataWWW.text.Contains ("\"success\":1")) {
			IDictionary guildData = ((Json.Deserialize (guildDataWWW.text) as IDictionary)["data"] as IList)[0] as IDictionary;
			PlayerParameters._instance.myPlayerParameter.guildLevel = int.Parse (guildData ["level"].ToString ());
			PlayerParameters._instance.myPlayerParameter.guildName = guildData ["name"].ToString ();
			PlayerParameters._instance.myPlayerParameter.guildPrefix = guildData ["prefix"].ToString ();
			Show(guildData);
			guildUIManager.LoadingPopup (false);
		} else {
			StartCoroutine (FetchGuildData ());
		}
	}

	public void Show(IDictionary data, string guildLeaderName = "") {
		guildData = data;
		Reset ();
		guildName.text = guildData ["name"].ToString ();
		guildPrefix.text = guildData ["prefix"].ToString ();
		if (guildLeaderName == "") {
			guildLeader.text = guildData ["leader_name"].ToString ();
		} else {
			guildLeader.text = guildLeaderName;
		}
		if (guildData ["player_ids"] == null || guildData["player_ids"].ToString() == "") {
			Debug.Log ("guild leader only");
			guildMembersCount.text = "1/" + guildData ["max_players"].ToString ();
		} else {
			guildMembersCount.text = guildData ["player_ids"].ToString ().Split (',').Length + 1 + "/" + guildData ["max_players"].ToString ();
		}
		guildAnnouncements.text = guildData ["announcements"] == null ? "" : guildData ["announcements"].ToString ();
		guildNotes.text = guildData ["notes"] == null ? "" : guildData ["notes"].ToString ();
		guildRules.text = guildData ["rules"] == null ? "" : guildData ["rules"].ToString ();
//		if (guildData ["announcements"] == null) {
//			guildAnnouncements.text = "";
//		} else {
//			guildAnnouncements.text = guildData ["announcements"].ToString ();
//		}
//		if (guildData ["notes"] == null) {
//			guildNotes.text = "";
//		} else {
//			guildNotes.text = guildData ["notes"].ToString ();
//		}
//		if (guildData ["rules"] == null) {
//			guildRules.text = "";
//		} else {
//			guildRules.text = guildData ["rules"].ToString ();
//		}
		guildTimezone.text = guildData ["majority_time_zone"].ToString ();
		applicants.interactable = guildPrefix.interactable = guildName.interactable = IsGuildLeaderOrViceGuildLeader ();
		foreach (Button button in addNewButtons) {
			button.gameObject.SetActive (IsGuildLeaderOrViceGuildLeader ());
		}
		SetAddNewButtons ();
	}

	public void Delete() {
		StartCoroutine (DeleteGuild ());
	}

	IEnumerator DeleteGuild() {
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "deleteGuild");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("gid", guildData ["id"].ToString ());
		WWW deleteGuild = new WWW (commonURL, wwwForm);
		yield return deleteGuild;
		Debug.Log (deleteGuild.text);
		if (deleteGuild.text.Contains ("\"success\":1")) {
			PlayerParameters._instance.myPlayerParameter.guildID = PlayerParameters._instance.myPlayerParameter.guildName = PlayerParameters._instance.myPlayerParameter.guildPrefix = "";
			PlayerParameters._instance.myPlayerParameter.guildLevel = 0;
		}
	}

	public void Leave() {
		guildUIManager.LoadingPopup (true, "Leaving guild...");
		if (guildLeader.text == PlayerDataParse._instance.playersParam.userName) {
			StartCoroutine (ChangeLeader ());
		} else {
			StartCoroutine (LeaveGuild ());
		}
	}

	IEnumerator ChangeLeader() {
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "changeGuildLeader");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("gid", guildData ["id"].ToString ());
		WWW changeLeader = new WWW (commonURL, wwwForm);
		yield return changeLeader;
		Debug.Log (changeLeader.text);
		guildUIManager.LoadingPopup (false);
		if (changeLeader.text.Contains ("\"success\":1")) {
			guildUIManager.WarningPopup ("You have left the guild successfully.");
			guildUIManager.OpenCreateGuildPage ();
			PlayerParameters._instance.myPlayerParameter.guildQuitTime = TimeManager._instance.GetCurrentServerTime ();
			PlayerParameters._instance.myPlayerParameter.guildID = PlayerParameters._instance.myPlayerParameter.guildName = PlayerParameters._instance.myPlayerParameter.guildPrefix = "";
			PlayerParameters._instance.myPlayerParameter.guildLevel = 0;
		} else {
			guildUIManager.WarningPopup ("You were not able to leave the guild. Please try again.");
		}
	}


	IEnumerator LeaveGuild() {
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "leaveGuild");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("gid", guildData ["id"].ToString ());
		wwwForm.AddField ("player_id", PlayerDataParse._instance.playersParam.userId);
		WWW leaveGuild = new WWW (commonURL, wwwForm);
		yield return leaveGuild;
		Debug.Log (leaveGuild.text);
		guildUIManager.LoadingPopup (false);
		if (leaveGuild.text.Contains ("\"success\":1")) {
			guildUIManager.WarningPopup ("You have left the guild successfully.");
			guildUIManager.OpenCreateGuildPage ();
			PlayerParameters._instance.myPlayerParameter.guildQuitTime = TimeManager._instance.GetCurrentServerTime ();
			PlayerParameters._instance.myPlayerParameter.guildID = PlayerParameters._instance.myPlayerParameter.guildName = PlayerParameters._instance.myPlayerParameter.guildPrefix = "";
			PlayerParameters._instance.myPlayerParameter.guildLevel = 0;
		} else {
			guildUIManager.WarningPopup ("You were not able to leave the guild. Please try again.");
		}
	}

	public void Members() {
		guildUIManager.LoadingPopup (true, "Loading...");
		StartCoroutine (MembersData (IsGuildLeaderOrViceGuildLeader()));
	}

	IEnumerator MembersData(bool canInvite = false) {
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "fetchGuildPlayersData");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("gid", guildData ["id"].ToString ());
		WWW membersWWW = new WWW (commonURL, wwwForm);
		yield return membersWWW;
		Debug.Log (membersWWW.text);
		guildUIManager.LoadingPopup (false);
		if (membersWWW.text.Contains ("\"success\":1")) {
			guildMembers.GetComponent<GuildMembers> ().Show ((Json.Deserialize (membersWWW.text) as IDictionary) ["data"] as IList, guildData["max_players"].ToString(), canInvite);
			gameObject.SetActive (false);
			guildMembers.SetActive (true);
		} else {
			StartCoroutine (MembersData (canInvite));
		}
	}

	public void Applicants() {
		guildUIManager.LoadingPopup (true, "Loading...");
		StartCoroutine (ApplicantsData ());
	}

	IEnumerator ApplicantsData() {
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "fetchGuildRequestByGLD");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("guild_id", guildData ["id"].ToString ());
		WWW applicantsWWW = new WWW (commonURL, wwwForm);
		yield return applicantsWWW;
		Debug.Log (applicantsWWW.text);
		guildUIManager.LoadingPopup (false);
		if (applicantsWWW.text.Contains ("\"success\":1")) {
			guildApplicants.GetComponent<GuildApplicants> ().Show ((Json.Deserialize (applicantsWWW.text) as IDictionary) ["data"] as IList);
			gameObject.SetActive (false);
			guildApplicants.SetActive (true);
		} else {
			guildUIManager.WarningPopup ("No player has requested to join your guild.");
		}
	}

	public void Donate() {
		if (gold.text == "") {
			gold.text = "0";
		}
		if(wheat.text == "") {
			wheat.text = "0";
		}
		if (PlayerParameters._instance.myPlayerParameter.gold < int.Parse (gold.text) || PlayerParameters._instance.myPlayerParameter.wheat < int.Parse (wheat.text)) {
			guildUIManager.WarningPopup ("You don't have enough gold/wheat.");
		} else if (gold.text == "0" && wheat.text == "0") {
			guildUIManager.WarningPopup ("Please enter valid gold/wheat.");
		} else {
			StartCoroutine (DonateCoroutine ());
		}
	}

	IEnumerator DonateCoroutine() {
		guildUIManager.LoadingPopup (true, "Donating...");
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "guildDonate");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("guild_id", guildData ["id"].ToString ());
		wwwForm.AddField ("player_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("gold", gold.text);
		wwwForm.AddField ("wheat", wheat.text);
		WWW donateWWW = new WWW (commonURL, wwwForm);
		yield return donateWWW;
		Debug.Log (donateWWW.text);
		guildUIManager.LoadingPopup (false);
		if (donateWWW.text.Contains ("\"success\":1")) {
			PlayerParameters._instance.myPlayerParameter.gold -= int.Parse (gold.text);
			PlayerParameters._instance.myPlayerParameter.wheat -= int.Parse (wheat.text);
			guildUIManager.WarningPopup ("Donation successful.");
		} else {
			guildUIManager.WarningPopup ("Unable to donate. Please try again.");
		}
	}

	public bool IsGuildLeaderOrViceGuildLeader() {
		if (guildLeader.text == PlayerDataParse._instance.playersParam.userName) {
			Debug.LogError ("Is Leader");
			return true;
		} else if(guildData["player_roles"].ToString().Contains("Vice Guild Leader")) {
			string[] playerRoles = guildData ["player_roles"].ToString ().Split (',');
			string[] playerIDs = guildData ["player_ids"].ToString ().Split (',');
			int playerIndex = 0;
			while(playerIndex<playerRoles.Length) {
				if (playerRoles [playerIndex] == "Vice Guild Leader") {
					if (playerIDs [playerIndex] == "" + PlayerDataParse._instance.playersParam.userIdNo) {
						Debug.LogError ("Is Vice Leader");
						return true;
					}
				}
				playerIndex++;
			}
		}
		return false;
	}

	public bool isGuildLeader() {
		if (guildLeader.text == PlayerDataParse._instance.playersParam.userName) {
			Debug.LogError ("Is Leader");
			return true;
		}
		return false;
	}

}
