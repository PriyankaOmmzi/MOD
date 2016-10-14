using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GuildInvite : MonoBehaviour {

	[SerializeField]
	InputField playerName;
	string commonURL;
	GuildUIManager guildUIManager;

	void Awake() {
		commonURL = API.Instance.commonURL;
		guildUIManager = GuildUIManager.instance;
	}

	public void Invite() {
		guildUIManager.LoadingPopup (true, "Sending invitation...");
		StartCoroutine (InvitePlayer ());
	}

	IEnumerator InvitePlayer() {
		Debug.Log (PlayerDataParse._instance.playersParam.userId);
		Debug.Log (SystemInfo.deviceUniqueIdentifier);
		Debug.Log (PlayerParameters._instance.myPlayerParameter.guildID);
		Debug.Log (playerName.text);
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "createGuildInvite");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("guild_id", PlayerParameters._instance.myPlayerParameter.guildID);
		wwwForm.AddField ("player_name", playerName.text);
		WWW invitePlayer = new WWW (commonURL, wwwForm);
		yield return invitePlayer;
		Debug.Log (invitePlayer.text);
		guildUIManager.LoadingPopup (false);
		if (invitePlayer.text.Contains ("\"success\":1")) {
			guildUIManager.WarningPopup ("Invitation sent to player successfully.");
		} else if (invitePlayer.text.Contains ("error_msg\":\"Allready Invited")) {
			guildUIManager.WarningPopup ("You have already invited this player.");
		} else if (invitePlayer.text.Contains ("error_msg\":\"Player does't exist")) {
			guildUIManager.WarningPopup ("Player doesn't exist.");
		} else if(invitePlayer.text.Contains ("error_msg\":\"Allready Have Guild")) {
			guildUIManager.WarningPopup ("Player is already part of another guild.");
		} else {
			guildUIManager.WarningPopup ("Invitation not sent. Please try again.");
		}
	}

}
