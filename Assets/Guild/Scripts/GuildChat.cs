using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class GuildChat : MonoBehaviour {

	[SerializeField]
	InputField chatInput;
	string commonURL;
	[SerializeField]
	GameObject myMessage;
	[SerializeField]
	GameObject playerMessage;
	[SerializeField]
	Transform chatOutput;
	public List<string> messageIDs;
	string myID;
	int maxMessages = 20;
//	GuildUIManager guildUIManager;

	void Awake() {
		messageIDs = new List<string> ();
		commonURL = API.Instance.commonURL;
//		guildUIManager = GuildUIManager.instance;
	}

	void OnEnable() {
		Reset ();
		loadingScene.Instance.loader.SetActive (true);
		if (PlayerParameters._instance.myPlayerParameter.guildName != "") {
			StartCoroutine (FetchChat (true));
		} else {
			loadingScene.Instance.popupFromServer.ShowPopup ("You are not a part of any guild!");
			//WARNING POPUP
		}
	}


	void OnDisable() {
		CancelInvoke ("FetchChatAfterOneSecond");
	}

	void Reset() {
		myID = PlayerDataParse._instance.playersParam.userIdNo + "";
		messageIDs.Clear ();
		int temp = 0;
		while (temp < chatOutput.childCount) {
			Destroy (chatOutput.GetChild (temp).gameObject);
			temp++;
		}
	}

	void RemoveLastMessage() {
		messageIDs.RemoveAt (0);
		Destroy (chatOutput.GetChild (0).gameObject);
	}

	IEnumerator FetchChat(bool didLoad = false) {
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "fetchGuildChats");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("gid", PlayerParameters._instance.myPlayerParameter.guildID);
		wwwForm.AddField ("max_count", "20");
		WWW fetchChat = new WWW (commonURL, wwwForm);
		yield return fetchChat;
		if(didLoad) {
			loadingScene.Instance.loader.SetActive (false);
//			guildUIManager.LoadingPopup (false);
		}
		if (fetchChat.text.Contains ("\"success\":1")) {
			IList messages = (Json.Deserialize (fetchChat.text) as IDictionary) ["data"] as IList;
			int temp = messages.Count - 1;
			while (temp >= 0) {
				IDictionary message = messages [temp] as IDictionary;
				if (!messageIDs.Contains (message ["id"].ToString ())) {
					RectTransform tempMessage = null;
					if (message ["player_id"].ToString () == myID) {
						tempMessage = Instantiate (myMessage).GetComponent<RectTransform> ();
					} else {
						tempMessage = Instantiate (playerMessage).GetComponent<RectTransform> ();
					}
					tempMessage.SetParent (chatOutput);
					tempMessage.localScale = Vector3.one;
					tempMessage.GetComponent<Message> ().Set (message ["message"].ToString (), message ["username"].ToString (), int.Parse (message ["avatar_no"].ToString ()));
					messageIDs.Add (message ["id"].ToString ());
					if (messageIDs.Count > maxMessages) {
						RemoveLastMessage ();
					}
				}
				temp--;
			}
		}
		Invoke ("FetchChatAfterOneSecond", 1f);
	}

	void FetchChatAfterOneSecond() {
		StartCoroutine (FetchChat ());
	}

	public void SendMessage() {
		StartCoroutine (CreateChat ());
	}

	IEnumerator CreateChat() {
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "guildChatsCreate");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wwwForm.AddField ("gid", PlayerParameters._instance.myPlayerParameter.guildID);
		wwwForm.AddField ("message", chatInput.text);
		WWW createChat = new WWW (commonURL, wwwForm);
		chatInput.text = "";
		yield return createChat;
		Debug.Log (createChat.text);
	}

}
