using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MiniJSON;

public class GuildCreation : MonoBehaviour {

	[SerializeField]
	InputField guildName;
	[SerializeField]
	Dropdown timeZone;
	[SerializeField]
	GameObject autoAccept;
	[SerializeField]
	GuildUIManager guildUIManager;
	[SerializeField]
	GuildMainPage guildMainPage;
	string commonURL;
	[SerializeField]
	GameObject guildNameAlreadyExists;
	[SerializeField]
	GameObject insufficientFunds;
	CoolDownTime coolDownTime;

	void Awake() {
		commonURL = API.Instance.commonURL;
	}

	void Start() {
		coolDownTime = CoolDownTime.instance;
	}

	public void GuildNameChanged() {
		guildNameAlreadyExists.SetActive (false);
		if (guildName.text.Length > 20) {
			guildName.text = guildName.text.Remove (20);
		}
	}

	public void Reset() {
		guildNameAlreadyExists.SetActive (false);
		insufficientFunds.SetActive (false);
		guildName.text = "";
		timeZone.value = 0;
		autoAccept.SetActive (true);
	}

	public void Create() {
		if (DoesPlayerHaveRequiredFunds ()) {
			if (PlayerParameters._instance.myPlayerParameter.guildName == "") {
				if (coolDownTime.IsOver ()) {
					guildName.text = guildName.text.TrimStart (' ');
					guildName.text = guildName.text.TrimEnd (' ');
					if (guildName.text != "") {
						StartCoroutine (CreateGuild ());
						guildUIManager.LoadingPopup (true, "Creating guild...");
					} else {
						guildUIManager.WarningPopup ("Guild name can't be empty.");
					}
				} else {
					Debug.Log (coolDownTime.coolDownTime);
					if (coolDownTime.coolDownTime.Hours > 0) {
						guildUIManager.WarningPopup ("Cooldown Time. You will be able to create a guild after " + coolDownTime.coolDownTime.Hours + " hours and " + coolDownTime.coolDownTime.Minutes + " minutes.");
					} else if (coolDownTime.coolDownTime.Minutes > 0) {
						guildUIManager.WarningPopup ("Cooldown Time. You will be able to create a guild after " + coolDownTime.coolDownTime.Minutes + " minutes and " + coolDownTime.coolDownTime.Seconds + " seconds.");
					} else {
						guildUIManager.WarningPopup ("Cooldown Time. You will be able to create a guild after " + coolDownTime.coolDownTime.Seconds + " seconds.");
					}
				}
			} else {
				guildUIManager.WarningPopup ("You are already part of a guild.");
			}
		} else {
			insufficientFunds.SetActive (true);
			guildUIManager.WarningPopup ("You don't have required funds for creating a guild.");
		}
	}

	bool DoesPlayerHaveRequiredFunds() {
		if (PlayerParameters._instance.myPlayerParameter.gold >= 100000 && PlayerParameters._instance.myPlayerParameter.wheat >= 100000) {
			return true;
		} else {
			return false;
		}
	}

	IEnumerator CreateGuild() {
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "createGuild");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("name", guildName.text);
		wwwForm.AddField ("level", "1");
		wwwForm.AddField ("majority_time_zone", timeZone.captionText.text);
		wwwForm.AddField ("auto_accept", autoAccept.activeSelf.ToString ());
		WWW createGuild = new WWW (commonURL, wwwForm);
		yield return createGuild;
		Debug.Log (createGuild.text);
		guildUIManager.LoadingPopup (false);
		if (createGuild.text.Contains ("\"success\":1")) {
			//{"success":1,"msg":"Guild created!","data":{"id":"4","prefix":"Gd04","name":"first guild","user_id":"269","player_ids":"","level":"1","time_zones":"","majority_time_zone":"Europe","player_roles":"","auto_accept":"","created":"2016-09-13 05:20:58","updated":"0000-00-00 00:00:00"}}
			IDictionary data = (Json.Deserialize (createGuild.text) as IDictionary) ["data"] as IDictionary;
			PlayerParameters._instance.myPlayerParameter.gold -= 100000;
			PlayerParameters._instance.myPlayerParameter.wheat -= 100000;
			PlayerParameters._instance.myPlayerParameter.guildID = data ["id"].ToString ();
			PlayerParameters._instance.myPlayerParameter.guildLevel = int.Parse (data ["level"].ToString ());
			PlayerParameters._instance.myPlayerParameter.guildName = data ["name"].ToString ();
			PlayerParameters._instance.myPlayerParameter.guildPrefix = data ["prefix"].ToString ();
			guildMainPage.Show (data, PlayerDataParse._instance.playersParam.userName);
			guildUIManager.OpenGuildPage ();
		} else if (createGuild.text.Contains ("error_msg\":\"name already taken!")) {
			guildNameAlreadyExists.SetActive (true);
		} else if(createGuild.text.Contains("error_msg\":\"You already have a pending guild request.")) {
			guildUIManager.PendingGuildRequestPopup ();
		} else {
			guildUIManager.WarningPopup ("Guild couldn't be created. Please try again.");
		}
	}

	public void CancelRequest() {
		guildUIManager.LoadingPopup (true, "Cancelling pending guild request...");
		StartCoroutine (DeleteRequest ());
	}

	IEnumerator DeleteRequest() {
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "deleteGuildRequestByPlayer");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("player_id", PlayerDataParse._instance.playersParam.userId);
		WWW deleteGuildRequest = new WWW (commonURL, wwwForm);
		yield return deleteGuildRequest;
		Debug.Log (deleteGuildRequest.text);
		guildUIManager.LoadingPopup (false);
		if (deleteGuildRequest.text.Contains ("\"success\":1")) {
			guildUIManager.LoadingPopup (false);
			guildUIManager.WarningPopup ("Guild request was cancelled.");
		} else {
			guildUIManager.WarningPopup ("Guild request couldn't be cancelled. Please try again.");
		}
	}

}