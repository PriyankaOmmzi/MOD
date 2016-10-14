using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using MiniJSON;

public class GuildMember : MonoBehaviour {

	[SerializeField]
	Text playerName;
	[SerializeField]
	Text level;
	[SerializeField]
	Text role;
	[SerializeField]
	Image avatar;
	[SerializeField]
	Text lastLoginTime;
	[SerializeField]
	Text goldDonation;
	[SerializeField]
	Text wheatDonation;
	TimeManager timeManager;
	[SerializeField]
	string[] classNames;
	IDictionary requestData;
	public Button changeRoleButton;
	GuildUIManager guildUIManager;
//	GuildRole guildRole;
//	string commonURL;

	void Awake() {
//		commonURL = API.Instance.commonURL;
//		guildRole = GuildRole.instance;
		timeManager = TimeManager._instance;
		guildUIManager = GuildUIManager.instance;
	}

	public void SetData(IDictionary data) {
		requestData = data;
		if (requestData ["gold_donation"] == null) {
			requestData ["gold_donation"] = "0";
		}
		if (requestData ["wheat_donation"] == null) {
			requestData ["wheat_donation"] = "0";
		}
		playerName.text = requestData["username"].ToString();
		level.text = "Lv. " + (int.Parse (requestData ["avatar_level"].ToString ()) + 1);
		role.text = "Role: " + requestData ["guild_role"];
		avatar.sprite = Resources.Load<Sprite> ("Characters/" + classNames [int.Parse (requestData ["avatar_no"].ToString ()) - 1]);
		goldDonation.text = requestData ["gold_donation"].ToString ();
		wheatDonation.text = requestData ["wheat_donation"].ToString ();
		DateTime lastLogin = Convert.ToDateTime (requestData ["last_login_time"].ToString ());
		TimeSpan difference = timeManager.GetCurrentServerTime () - lastLogin;
		int hours = difference.Days * 24 + difference.Hours;
		int minutes = difference.Minutes;
		lastLoginTime.text = "Last Login: " + hours + "h " + minutes + "m";
	}

	public void ShowProfile() {
		guildUIManager.ShowProfile (requestData, true, role, gameObject);
	}

//	public void ChangeRole() {
//		guildRole.Set (requestData ["player_id"].ToString (), role);
//	}
//
//	public void RemoveMember() {
//		StartCoroutine (LeaveGuild ());
//	}
//
//	IEnumerator LeaveGuild() {
//		WWWForm wwwForm = new WWWForm ();
//		wwwForm.AddField ("tag", "leaveGuild");
//		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
//		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
//		wwwForm.AddField ("gid", PlayerParameters._instance.myPlayerParameter.guildID);
//		wwwForm.AddField ("player_id", PlayerDataParse._instance.ID (requestData ["player_id"].ToString ()));
//		WWW leaveGuild = new WWW (commonURL, wwwForm);
//		yield return leaveGuild;
//		Debug.Log (leaveGuild.text);
//		guildUIManager.LoadingPopup (false);
//		if (leaveGuild.text.Contains ("\"success\":1")) {
//		}
//	}

}
