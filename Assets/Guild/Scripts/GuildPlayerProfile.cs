using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using MiniJSON;

public class GuildPlayerProfile : MonoBehaviour {

	[SerializeField]
	Text playerName;
	[SerializeField]
	Text iD;
	[SerializeField]
	Text level;
	[SerializeField]
	Text playerClass;
	[SerializeField]
	Text guild;
	[SerializeField]
	Text lastLogin;
	[SerializeField]
	Text cardsCount;
	[SerializeField]
	Text friendsCount;
	[SerializeField]
	Transform reviews;
	[SerializeField]
	Text trades;
	[SerializeField]
	Text points;
	[SerializeField]
	Text attack;
	[SerializeField]
	Text defense;
	[SerializeField]
	Text leadership;
	[SerializeField]
	Text stamina;
	[SerializeField]
	Image avatar;
	[SerializeField]
	string[] classNames;
	TimeManager timeManager;
	Text role;
	GameObject member;
	GuildRole guildRole;
	string commonURL;
	[SerializeField]
	GameObject removePlayerButton;
	[SerializeField]
	GameObject updateRoleButton;
	[SerializeField]
	GuildMainPage guildMainPage;
	[SerializeField]
	Text membersCount;
	[SerializeField]
	GuildMembers guildMembers;
	GuildUIManager guildUIManager;
	[SerializeField]
	GameObject removeMemberPopup;
	[SerializeField]
	Text removeMemberPopupContent;
	[SerializeField]
	GameObject updateRolePopup;
	[SerializeField]
	Text updateRolePopupContent;
	[SerializeField]
	Text description;

	void Awake() {
		commonURL = API.Instance.commonURL;
		guildRole = GuildRole.instance;
		guildUIManager = GuildUIManager.instance;
		timeManager = TimeManager._instance;
	}

	void OnEnable() {
		removeMemberPopup.SetActive (false);
		updateRolePopup.SetActive (false);
		Debug.LogError (role.text);
		if (role.text == "Role: Guild Leader") {
			updateRoleButton.SetActive (false);
			removePlayerButton.SetActive (false);
		}
		else if (guildMainPage.IsGuildLeaderOrViceGuildLeader ()) {
			if (guildMainPage.isGuildLeader ()) {
				updateRoleButton.SetActive (true);
				removePlayerButton.SetActive (true);
			} else if (role.text == "Role: Vice Guild Leader") {
				updateRoleButton.SetActive (false);
				removePlayerButton.SetActive (false);
			} else {
				updateRoleButton.SetActive (true);
				removePlayerButton.SetActive (true);
			}
		} else {
			updateRoleButton.SetActive (false);
			removePlayerButton.SetActive (false);
		}
	}

	IEnumerator GetPlayerDescription(string playerID) {
		guildUIManager.LoadingPopup (true, "Loading...");
	//	tag=doGetPlayerDescription&device_id=123&user_id=123&player_id=123
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "doGetPlayerDescription");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("player_id", playerID);
		WWW playerData = new WWW (API.Instance.indexURL, wwwForm);
		yield return playerData;
		Debug.Log (playerData.text);
		if (playerData.text.Contains ("\"success\":1")) {
			IDictionary data = (Json.Deserialize (playerData.text) as IDictionary) ["data"] as IDictionary;
			description.text = data["Description"].ToString();
			points.text = data["Avatar_stats_pool"].ToString();
		} else {
			description.text = "";
			points.text = "";
		}
		if (points.text == "") {
			points.text = "0";
		}
		guildUIManager.LoadingPopup (false);
	}


	public void SetData(IDictionary data, bool isMember = false, Text role = null, GameObject member = null) {
		this.role = role;
		this.member = member;
		if (isMember) {
			guild.text = PlayerParameters._instance.myPlayerParameter.guildName;
		} else {
			guild.text = "";
		}
		playerName.text = data["username"].ToString();
		iD.text = data["player_id"].ToString();
		level.text = (int.Parse (data ["avatar_level"].ToString ()) + 1).ToString ();
		cardsCount.text = data ["card_count"].ToString ();
		friendsCount.text = data ["friends_count"].ToString ();
		gameObject.SetActive (true);
		StartCoroutine (GetPlayerDescription (PlayerDataParse._instance.ID (iD.text)));

		DateTime lastLoginTime = Convert.ToDateTime (data ["last_login_time"].ToString ());
		TimeSpan difference = timeManager.GetCurrentServerTime () - lastLoginTime;
		int hours = difference.Days * 24 + difference.Hours;
		int minutes = difference.Minutes;
		lastLogin.text = hours + "h " + minutes + "m";

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

		trades.text = data ["trades_count"].ToString ();
		attack.text = data ["avatar_attack"].ToString ();
		defense.text = data ["avatar_defense"].ToString ();
		leadership.text = data ["avatar_leadership"].ToString ();
		stamina.text = data ["stamina"].ToString ();
		playerClass.text = classNames [int.Parse (data ["avatar_no"].ToString ()) - 1];
		avatar.sprite = Resources.Load<Sprite> ("Characters/" + playerClass.text);
	}

	public void UpdateRolePopup() {
		updateRolePopupContent.text = playerName.text;
		guildRole.Set (iD.text, role);
	}

	public void OpenRemoveMemberPopup() {
		removeMemberPopupContent.text = "Are you sure you want to\nremove " + playerName.text + " from Guild?";
		removeMemberPopup.SetActive (true);
	}

	public void RemoveMember() {
		guildUIManager.LoadingPopup (true, "Removing player...");
		StartCoroutine (LeaveGuild ());
	}

	IEnumerator LeaveGuild() {
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "leaveGuild");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("gid", PlayerParameters._instance.myPlayerParameter.guildID);
		wwwForm.AddField ("player_id", PlayerDataParse._instance.ID (iD.text));
		WWW leaveGuild = new WWW (commonURL, wwwForm);
		yield return leaveGuild;
		Debug.Log (leaveGuild.text);
		guildUIManager.LoadingPopup (false);
		if (leaveGuild.text.Contains ("\"success\":1")) {
			guildUIManager.WarningPopup ("Player has been removed from guild successfully.");
			Destroy (member);
			guildMembers.SetMembers ();
			gameObject.SetActive (false);
		} else {
			guildUIManager.WarningPopup ("Player wasn't removed from guild. Please try again.");
		}
	}

}
