using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GuildApplicant : MonoBehaviour {

	[SerializeField]
	Text playerName;
	[SerializeField]
	Text level;
	[SerializeField]
	Text className;
	[SerializeField]
	Image avatar;
	[SerializeField]
	Text lastLoginTime;
	TimeManager timeManager;
	[SerializeField]
	string[] classNames;
	IDictionary requestData;
	string commonURL;
	GuildUIManager guildUIManager;

	void Awake() {
		commonURL = API.Instance.commonURL;
		guildUIManager = GuildUIManager.instance;
		timeManager = TimeManager._instance;
	}

	public void SetData(IDictionary data) {
	//{"player_id":"269","username":"manishjolly","avatar_no":"2","avatar_level":"0","last_login_time":"2016-09-20 09:01:58","card_count":"6","avatar_attack":"20","avatar_defense":"30","avatar_leadership":"20","stamina":"20"}
		requestData = data;
		playerName.text = requestData["username"].ToString();
		level.text = "Lv. " + (int.Parse (requestData ["avatar_level"].ToString ()) + 1);
		className.text = classNames [int.Parse (requestData ["avatar_no"].ToString ()) - 1];
		avatar.sprite = Resources.Load<Sprite> ("Characters/" + className.text);
		DateTime lastLogin = Convert.ToDateTime (requestData ["last_login_time"].ToString ());
		TimeSpan difference = timeManager.GetCurrentServerTime () - lastLogin;
		int hours = difference.Days * 24 + difference.Hours;
		int minutes = difference.Minutes;
		lastLoginTime.text = hours + "h " + minutes + "m";
	}

	IEnumerator AddPlayer() {
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "updateGuild");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("gid", PlayerParameters._instance.myPlayerParameter.guildID);
		wwwForm.AddField ("player_ids", PlayerDataParse._instance.ID (requestData ["player_id"].ToString ()));
		wwwForm.AddField ("player_roles", "Member");
		WWW updateGuild = new WWW (commonURL, wwwForm);
		yield return updateGuild;
		Debug.Log (updateGuild.text);
		if (updateGuild.text.Contains ("\"success\":1")) {
			StartCoroutine (DeleteInvites ());
		} else if(updateGuild.text.Contains("error_msg\":\"Rejected")) {
			guildUIManager.LoadingPopup (false);
			guildUIManager.WarningPopup ("You can't join this guild as it is full.");
		} else {
			StartCoroutine (AddPlayer ());
		}
	}

	IEnumerator DeleteInvites() {
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "deleteGuildInviteByPlayer");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("player_id", PlayerDataParse._instance.ID (requestData ["player_id"].ToString ()));
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		WWW deleteInvites = new WWW (commonURL, wwwForm);
		yield return deleteInvites;
		Debug.Log (deleteInvites.text);
		guildUIManager.LoadingPopup (false);
		guildUIManager.WarningPopup ("Request to join guild was accepted successfully.");
		guildUIManager.DecrementApplicantsCount ();
		Destroy (gameObject);
	}

	IEnumerator DeleteRequest() {
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "deleteGuildRequests");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("id", requestData ["id"].ToString ());
		WWW deleteRequest = new WWW (commonURL, wwwForm);
		yield return deleteRequest;
		Debug.Log (deleteRequest.text);
		if (deleteRequest.text.Contains ("\"success\":1")) {
			StartCoroutine (AddPlayer ());
		} else {
			guildUIManager.LoadingPopup (false);
			guildUIManager.WarningPopup ("Unable to accept request. Please try again.");
		}
	}

	public void Accept() {
		guildUIManager.LoadingPopup (true, "Accepting request to join guild...");
		StartCoroutine (DeleteRequest ());
	}

	public void Decline() {
		guildUIManager.LoadingPopup (true, "Declining request to join guild...");
		StartCoroutine (DeclineRequest ());
	}

	IEnumerator DeclineRequest() {
		Debug.Log (PlayerDataParse._instance.playersParam.userId);
		Debug.Log (SystemInfo.deviceUniqueIdentifier);
		Debug.Log (requestData ["id"].ToString ());
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "updateGuildRequestsStatus");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("id", requestData ["id"].ToString ());
		wwwForm.AddField ("request_status", "DECLINED");
		WWW declineRequest = new WWW (commonURL, wwwForm);
		yield return declineRequest;
		Debug.Log (declineRequest.text);
		guildUIManager.LoadingPopup (false);
		if (declineRequest.text.Contains ("\"success\":1")) {
			guildUIManager.WarningPopup ("Request to join guild was declined successfully.");
			guildUIManager.DecrementApplicantsCount ();
			Destroy (gameObject);
		} else {
			guildUIManager.WarningPopup ("Unable to decline request. Please try again.");
		}
	}

	public void ShowProfile() {
		guildUIManager.ShowProfile (requestData);
	}
}
