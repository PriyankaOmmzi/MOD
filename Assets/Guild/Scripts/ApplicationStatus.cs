using UnityEngine;
using System.Collections;
using MiniJSON;

public class ApplicationStatus : MonoBehaviour {

	string guildURL;
	GuildUIManager guildUIManager;
	[SerializeField]
	GameObject pendingRequest;
	[SerializeField]
	GameObject declinedRequest;
	[SerializeField]
	GameObject invitation;
	[SerializeField]
	Transform guildApplicationsParent;

	void Awake() {
		guildUIManager = GuildUIManager.instance;
		guildURL = API.Instance.commonURL;
	}

	public void ResetData() {
		int temp = 0;
		while (temp < guildApplicationsParent.childCount) {
			Destroy (guildApplicationsParent.GetChild (temp).gameObject);
			temp++;
		}
	}

	void OnEnable() {
		ResetData ();
		guildUIManager.LoadingPopup (true, "Loading...");
		StartCoroutine (FetchRequestStatus ());
	}

	IEnumerator FetchRequestStatus() {
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "fetchGuildRequestByGID");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("player_id", PlayerDataParse._instance.playersParam.userId);
		WWW requestStatus = new WWW (guildURL, wwwForm);
		yield return requestStatus;
		StartCoroutine (FetchGuildInvitations ());
		Debug.Log (requestStatus.text);
		if (requestStatus.text.Contains ("\"success\":1")) {
			IDictionary data = ((Json.Deserialize (requestStatus.text) as IDictionary) ["data"] as IList) [0] as IDictionary;
			if (data ["request_status"].ToString () == "PENDING") {
				RectTransform tempGuildApplication = Instantiate (pendingRequest).GetComponent<RectTransform> ();
				tempGuildApplication.SetParent (guildApplicationsParent);
				tempGuildApplication.localScale = Vector3.one;
				tempGuildApplication.GetComponent<GuildApplication> ().SetData (data);
			} else if(data ["request_status"].ToString () == "DECLINED") {
				RectTransform tempGuildApplication = Instantiate (declinedRequest).GetComponent<RectTransform> ();
				tempGuildApplication.SetParent (guildApplicationsParent);
				tempGuildApplication.localScale = Vector3.one;
				tempGuildApplication.GetComponent<GuildApplication> ().SetData (data);
			}
		}
	}

	IEnumerator FetchGuildInvitations() {
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "guildInviteByPlayer");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("player_id", PlayerDataParse._instance.playersParam.userId);
		WWW inviteDataWWW = new WWW (guildURL, wwwForm);
		yield return inviteDataWWW;
		Debug.Log (inviteDataWWW.text);
		guildUIManager.LoadingPopup (false);
		if (inviteDataWWW.text.Contains ("\"success\":1")) {
			IList data = (Json.Deserialize (inviteDataWWW.text) as IDictionary) ["data"] as IList;
			foreach(IDictionary invitationData in data) {
				RectTransform tempGuildApplication = Instantiate (invitation).GetComponent<RectTransform> ();
				tempGuildApplication.SetParent (guildApplicationsParent);
				tempGuildApplication.localScale = Vector3.one;
				tempGuildApplication.GetComponent<GuildApplication> ().SetData (invitationData);
			}
		}
	}


}
