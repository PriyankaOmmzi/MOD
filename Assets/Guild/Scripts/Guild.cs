using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MiniJSON;

public class Guild : MonoBehaviour {

	[SerializeField]
	Text guildName;
	[SerializeField]
	Text guildRank;
	[SerializeField]
	Text guildLeader;
	[SerializeField]
	Text guildMembersCount;
	[SerializeField]
	Image guildEmblem;
	[SerializeField]
	Text guildLevel;
	[SerializeField]
	Text guildRequestText;
	IDictionary guildData;
	string commonURL;
	GuildUIManager guildUIManager;
	CoolDownTime coolDownTime;

	void Awake() {
		coolDownTime = CoolDownTime.instance;
		commonURL = API.Instance.commonURL;
		guildUIManager = GuildUIManager.instance;
	}

	public void Show(IDictionary data) {
		guildData = data;
		if (guildData ["auto_accept"].ToString () == "True") {
			guildRequestText.text = "Join\nGuild";
		} else {
			guildRequestText.text = "Guild\nRequest";
		}
		guildName.text = guildData ["name"].ToString ();
		guildLeader.text = guildData ["leader_name"].ToString ();
		if (guildData ["player_ids"] == null || guildData["player_ids"].ToString() == "") {
			guildMembersCount.text = "1/" + guildData ["max_players"].ToString ();
		} else {
			guildMembersCount.text = guildData ["player_ids"].ToString ().Split (',').Length + 1 + "/" + guildData ["max_players"].ToString ();
		}
		guildLevel.text = "Lv. " + guildData ["level"];
	}

	/*public void ShowRequestPopup() {
		if (PlayerParameters._instance.myPlayerParameter.guildName == "") {
			if (coolDownTime.IsOver ()) {
				if (guildData ["auto_accept"].ToString () == "True") {
				} else {
				}
			} else {
				if (coolDownTime.coolDownTime.Hours > 0) {
					guildUIManager.WarningPopup ("Cooldown Time. You will be able to join a guild after " + coolDownTime.coolDownTime.Hours + " hours and " + coolDownTime.coolDownTime.Minutes + " minutes.");
				} else if (coolDownTime.coolDownTime.Minutes > 0) {
					guildUIManager.WarningPopup ("Cooldown Time. You will be able to join a guild after " + coolDownTime.coolDownTime.Minutes + " minutes and " + coolDownTime.coolDownTime.Seconds + " seconds.");
				} else {
					guildUIManager.WarningPopup ("Cooldown Time. You will be able to join a guild after " + coolDownTime.coolDownTime.Seconds + " seconds.");
				}
			}
		} else {
			guildUIManager.WarningPopup ("You are already part of a guild.");
		}
	}*/

	public void SendRequest() {
		if (PlayerParameters._instance.myPlayerParameter.guildName == "") {
			if (coolDownTime.IsOver ()) {
				if (guildData ["auto_accept"].ToString () == "True") {
					guildUIManager.LoadingPopup (true, "Joining guild...");
					StartCoroutine (UpdateGuild ());
				} else {
					guildUIManager.LoadingPopup (true, "Sending request to join guild...");
					StartCoroutine (SendRequestCoroutine ());
				}
			} else {
				if (coolDownTime.coolDownTime.Hours > 0) {
					guildUIManager.WarningPopup ("Cooldown Time. You will be able to join a guild after " + coolDownTime.coolDownTime.Hours + " hours and " + coolDownTime.coolDownTime.Minutes + " minutes.");
				} else if (coolDownTime.coolDownTime.Minutes > 0) {
					guildUIManager.WarningPopup ("Cooldown Time. You will be able to join a guild after " + coolDownTime.coolDownTime.Minutes + " minutes and " + coolDownTime.coolDownTime.Seconds + " seconds.");
				} else {
					guildUIManager.WarningPopup ("Cooldown Time. You will be able to join a guild after " + coolDownTime.coolDownTime.Seconds + " seconds.");
				}
			}
		} else {
			guildUIManager.WarningPopup ("You are already part of a guild.");
		}
	}


	IEnumerator SendRequestCoroutine() {
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "createGuildRequests");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("guild_id", guildData ["id"].ToString ());
		wwwForm.AddField ("player_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("request_status", "PENDING");
		WWW sendRequest = new WWW (commonURL, wwwForm);
		yield return sendRequest;
		Debug.Log (sendRequest.text);
		guildUIManager.LoadingPopup (false);
		if (sendRequest.text.Contains ("\"success\":1")) {
			guildUIManager.WarningPopup ("Request to join guild sent.");
		} else if (sendRequest.text.Contains ("error_msg\":\"You already have a pending guild request.")) {
			guildUIManager.PendingGuildRequestPopup ();
		}
		else {
			guildUIManager.WarningPopup ("Request to join guild wasn't sent. Please try again.");
		}
	}

	IEnumerator UpdateGuild() {
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "updateGuild");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("gid", guildData ["id"].ToString ());
		wwwForm.AddField ("player_ids", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("player_roles", "Member");
		WWW updateGuild = new WWW (commonURL, wwwForm);
		yield return updateGuild;
		Debug.Log (updateGuild.text);
		guildUIManager.LoadingPopup (false);
		if (updateGuild.text.Contains ("\"success\":1")) {
			IDictionary data = (Json.Deserialize (updateGuild.text) as IDictionary) ["data"] as IDictionary;
			PlayerParameters._instance.myPlayerParameter.guildID = data ["id"].ToString ();
			PlayerParameters._instance.myPlayerParameter.guildLevel = int.Parse (data ["level"].ToString ());
			PlayerParameters._instance.myPlayerParameter.guildName = data ["name"].ToString ();
			PlayerParameters._instance.myPlayerParameter.guildPrefix = data ["prefix"].ToString ();
			guildUIManager.OpenGuildPage (true, data, guildData ["leader_name"].ToString ());
			guildUIManager.WarningPopup ("You have joined the guild successfully.");
		} else if(updateGuild.text.Contains("error_msg\":\"You already have a pending guild request.")) {
			guildUIManager.PendingGuildRequestPopup ();
		} else if(updateGuild.text.Contains("error_msg\":\"Rejected")) {
			guildUIManager.WarningPopup ("You can't join this guild as it is full.");
		} else {
			guildUIManager.WarningPopup ("You were not able to join the guild. Please try again.");
		}
	}

}
