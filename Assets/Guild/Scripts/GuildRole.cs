using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GuildRole : MonoBehaviour {

	[SerializeField]
	GameObject changeRolePopup;
	string commonURL;
	string playerID;
	Text selectedPlayer;
	GuildUIManager guildUIManager;
	[SerializeField]
	Dropdown playerRole;
	public static GuildRole instance;

	void Awake() {
		instance = this;
	}

	void Start() {
		commonURL = API.Instance.commonURL;
		guildUIManager = GuildUIManager.instance;
	}

	public void Set(string userIDNo, Text selectedPlayer) {
		playerID = PlayerDataParse._instance.ID (userIDNo);
		instance.selectedPlayer = selectedPlayer;
		changeRolePopup.SetActive (true);
	}

	public void ChangeRole() {
		if (selectedPlayer.text.Contains (playerRole.captionText.text)) {
			guildUIManager.WarningPopup ("Member already has this role.");
		} else {
			guildUIManager.LoadingPopup (true, "Reassigning role...");
			StartCoroutine (ChangeRoleCoroutine ());
		}
	}

	IEnumerator ChangeRoleCoroutine() {
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "updateGuild");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("gid", PlayerParameters._instance.myPlayerParameter.guildID);
		wwwForm.AddField ("player_ids", playerID);
		wwwForm.AddField ("player_roles", playerRole.captionText.text);
		WWW changePlayerRoleWWW = new WWW (commonURL, wwwForm);
		yield return changePlayerRoleWWW;
		Debug.Log (changePlayerRoleWWW.text);
		guildUIManager.LoadingPopup (false);
		if (changePlayerRoleWWW.text.Contains ("\"success\":1")) {
			selectedPlayer.text = "Role: " + playerRole.captionText.text;
			guildUIManager.WarningPopup ("Role assigned successfully.");
		} else if (changePlayerRoleWWW.text.Contains ("error_msg\":\"Vice Guild Leader Allready exist.")) {
			guildUIManager.WarningPopup ("Vice Guild Leader already exists for this guild.");
		} else {
			guildUIManager.WarningPopup ("Couldn't reassign role. Please try again.");
		}
	}

}
