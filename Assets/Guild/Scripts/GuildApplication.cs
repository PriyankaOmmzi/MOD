using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GuildApplication : MonoBehaviour {

	[SerializeField]
	Text guildLevel;
	[SerializeField]
	Text guildName;
	IDictionary applicationData;
	string commonURL;
	GuildUIManager guildUIManager;
	CoolDownTime coolDownTime;

	void Awake() {
		coolDownTime = CoolDownTime.instance;
		commonURL = API.Instance.commonURL;
		guildUIManager = GuildUIManager.instance;
	}

	public void SetData(IDictionary data) {
		applicationData = data;
		guildName.text = applicationData ["guild_name"].ToString ();
		guildLevel.text = "Lv. " + applicationData ["guild_level"];
	}

	public void DeleteRequest() {
		guildUIManager.LoadingPopup (true, "Deleting request...");
		StartCoroutine (DeleteRequestCoroutine ());
	}

	IEnumerator DeleteRequestCoroutine() {
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "deleteGuildRequests");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("id", applicationData ["id"].ToString ());
		WWW deleteRequest = new WWW (commonURL, wwwForm);
		yield return deleteRequest;
		Debug.Log (deleteRequest.text);
		guildUIManager.LoadingPopup (false);
		if (deleteRequest.text.Contains ("\"success\":1")) {
			Destroy (gameObject);
			guildUIManager.WarningPopup ("Request was deleted successfully.");
		} else {
			guildUIManager.WarningPopup ("Unable to delete request. Please try again.");
		}
	}

	public void DeclineGuildInvitation() {
		guildUIManager.LoadingPopup (true, "Declining guild invitation...");
		StartCoroutine (DeleteGuildInvitationCoroutine ());
	}

	IEnumerator DeleteGuildInvitationCoroutine() {
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "deleteGuildInvite");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("guild_invite_id", applicationData ["invite_id"].ToString ());
		WWW deleteGuildInvitation = new WWW (commonURL, wwwForm);
		yield return deleteGuildInvitation;
		Debug.Log (deleteGuildInvitation.text);
		guildUIManager.LoadingPopup (false);
		if (deleteGuildInvitation.text.Contains ("\"success\":1")) {
			Destroy (gameObject);
			guildUIManager.WarningPopup ("Invitation declined successfully.");
		} else {
			guildUIManager.WarningPopup ("Unable to decline invitation. Please try again.");
		}
	}

	public void AcceptGuildInvitation() {
		if (coolDownTime.IsOver ()) {
			guildUIManager.LoadingPopup (true, "Accepting guild invitation...");
			StartCoroutine (DeleteAllRequests ());
		} else {
			if (coolDownTime.coolDownTime.Hours > 0) {
				guildUIManager.WarningPopup ("Cooldown Time. You will be able to join a guild after " + coolDownTime.coolDownTime.Hours + " hours and " + coolDownTime.coolDownTime.Minutes + " minutes.");
			} else if (coolDownTime.coolDownTime.Minutes > 0) {
				guildUIManager.WarningPopup ("Cooldown Time. You will be able to join a guild after " + coolDownTime.coolDownTime.Minutes + " minutes and " + coolDownTime.coolDownTime.Seconds + " seconds.");
			} else {
				guildUIManager.WarningPopup ("Cooldown Time. You will be able to join a guild after " + coolDownTime.coolDownTime.Seconds + " seconds.");
			}
		}
	}

	IEnumerator DeleteAllInvitations() {
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "deleteGuildInviteByPlayer");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("player_id", PlayerDataParse._instance.playersParam.userId);
		WWW deleteAllInvitations = new WWW (commonURL, wwwForm);
		yield return deleteAllInvitations;
		Debug.Log (deleteAllInvitations.text);
		if (deleteAllInvitations.text.Contains ("\"success\":1")) {
			guildUIManager.WarningPopup ("Invitation accepted.");
			PlayerParameters._instance.myPlayerParameter.guildID = applicationData ["guild_id"].ToString ();
			Destroy (gameObject);
			guildUIManager.ShowGuild (true);
		} else {
			StartCoroutine (DeleteAllInvitations ());
		}
	}

	IEnumerator DeleteAllRequests() {
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "deleteGuildRequestByPlayer");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("player_id", PlayerDataParse._instance.playersParam.userId);
		WWW deleteAllRequests = new WWW (commonURL, wwwForm);
		yield return deleteAllRequests;
		StartCoroutine (AddPlayerToGuild ());
	}

	IEnumerator AddPlayerToGuild() {
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "updateGuild");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("gid", applicationData ["guild_id"].ToString ());
		wwwForm.AddField ("player_ids", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("player_roles", "Member");
		WWW updateGuild = new WWW (commonURL, wwwForm);
		yield return updateGuild;
		Debug.Log (updateGuild.text);
		if (updateGuild.text.Contains ("\"success\":1")) {
			StartCoroutine (DeleteAllInvitations ());
		} else if(updateGuild.text.Contains("error_msg\":\"Rejected")) {
			guildUIManager.LoadingPopup (false);
			guildUIManager.WarningPopup ("You can't join this guild as it is full.");
		} else {
			StartCoroutine (AddPlayerToGuild ());
		}
	}

}
