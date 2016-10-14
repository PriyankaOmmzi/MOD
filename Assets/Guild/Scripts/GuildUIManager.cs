using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MiniJSON;

public class GuildUIManager : MonoBehaviour {

	[SerializeField]
	GameObject autoAccept;
	bool isMneuActive;
	public Button[] bottomsButtons;
	public GameObject menuScreen;
	[SerializeField]
	GameObject guildUI;
	[SerializeField]
	GameObject createGuildUI;
	[SerializeField]
	GameObject searchGuildUI;
	[SerializeField]
	GameObject applicationStatus;
	[SerializeField]
	GameObject loadingPopup;
	[SerializeField]
	Text loadingPopupContent;
	[SerializeField]
	GameObject warningPopup;
	[SerializeField]
	GameObject pendingGuildRequestPopup;
	[SerializeField]
	Text warningPopupContent;
	GuildCreation guildCreation;
	[SerializeField]
	GuildApplicants guildApplicants;
	[SerializeField]
	GuildPlayerProfile playerProfile;
	[SerializeField]
	GameObject title;
	[SerializeField]
	GameObject tabs;
	public static GuildUIManager instance;
	string guildURL;
//	[SerializeField]
//	GameObject requestPopup;
//	[SerializeField]
//	Text requestPopupTitle;
//	[SerializeField]

	void Awake() {
		instance = this;
		guildURL = API.Instance.commonURL;
		guildCreation = GetComponent<GuildCreation> ();
	}

	public void AutoAccept () {
		autoAccept.SetActive (!autoAccept.activeSelf);
	}

//	public void ShowRequestPopup() {
//	}

	public void OpenGuildPage(bool fromSearch = false, IDictionary data = null, string leaderName = "") {
		if (fromSearch) {
			searchGuildUI.SetActive (false);
			guildUI.GetComponent<GuildMainPage> ().Show (data, leaderName);
		} else {
			createGuildUI.SetActive (false);
		}
		guildUI.SetActive (true);
	}

	public void OpenCreateGuildPage() {
		guildCreation.Reset ();
		guildUI.SetActive (false);
		createGuildUI.SetActive (true);
	}

	public void LoadingPopup(bool value, string content = "") {
		loadingPopupContent.text = content;
		loadingPopup.SetActive (value);
	}

	public void WarningPopup(string content) {
		warningPopupContent.text = content;
		warningPopup.SetActive (true);
	}

	public void PendingGuildRequestPopup() {
		pendingGuildRequestPopup.SetActive (true);
	}

	public void DecrementApplicantsCount() {
		guildApplicants.DecrementApplicantsCount ();
	}

	public void ShowProfile(IDictionary data, bool isMember = false, Text role = null, GameObject member = null) {
		playerProfile.SetData (data, isMember, role, member);
	}

	public void ShowGuild(bool fromApplicationStatus = false) {
		guildUI.SetActive (true);
		guildUI.GetComponent<GuildMainPage> ().Fetch ();
		if (fromApplicationStatus) {
			applicationStatus.SetActive (false);
		}
	}

	void OnEnable()
	{
		StartCoroutine (FetchCurrentGuild ());
		isMneuActive = false;
		for(int j=0;j<bottomsButtons.Length;j++)
		{
			bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
			bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
			bottomsButtons[j].GetComponent<Button>().interactable=true;
		}
		menuScreen.SetActive(false);
	}

	IEnumerator FetchCurrentGuild() {
		LoadingPopup (true, "Loading...");
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag", "fetchPlayerReqData");
		wwwForm.AddField ("user_id", PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		WWW currentGuild = new WWW (guildURL, wwwForm);
		yield return currentGuild;
		Debug.Log (currentGuild.text);
		title.SetActive (true);
		tabs.SetActive (true);
		if (currentGuild.text.Contains ("\"success\":1")) {
			IDictionary guildData = (Json.Deserialize (currentGuild.text) as IDictionary)["data"] as IDictionary;
			if (guildData ["guild_id"].ToString () == "0") {
				LoadingPopup (false);
				if (guildData ["quit_time"].ToString () != "0000-00-00 00:00:00") {
					PlayerParameters._instance.myPlayerParameter.guildQuitTime = System.Convert.ToDateTime (guildData ["quit_time"].ToString ());
				} else {
					PlayerParameters._instance.myPlayerParameter.guildQuitTime = System.Convert.ToDateTime ("01/01/0001 00:00:00");
				}
				PlayerParameters._instance.myPlayerParameter.guildID = PlayerParameters._instance.myPlayerParameter.guildName = PlayerParameters._instance.myPlayerParameter.guildPrefix = "";
				PlayerParameters._instance.myPlayerParameter.guildLevel = 0;
				guildCreation.Reset ();
				createGuildUI.SetActive (true);
			} else {
				PlayerParameters._instance.myPlayerParameter.guildID = guildData ["guild_id"].ToString ();
				if (guildData ["quit_time"].ToString () != "0000-00-00 00:00:00") {
					PlayerParameters._instance.myPlayerParameter.guildQuitTime = System.Convert.ToDateTime (guildData ["quit_time"].ToString ());
				} else {
					PlayerParameters._instance.myPlayerParameter.guildQuitTime = System.Convert.ToDateTime ("01/01/0001 00:00:00");
				}
				Debug.Log (PlayerParameters._instance.myPlayerParameter.guildID);
				ShowGuild ();
			}
		} else {
			LoadingPopup (false);
			PlayerParameters._instance.myPlayerParameter.guildID = PlayerParameters._instance.myPlayerParameter.guildName = PlayerParameters._instance.myPlayerParameter.guildPrefix = "";
			PlayerParameters._instance.myPlayerParameter.guildLevel = 0;
			guildCreation.Reset ();
			createGuildUI.SetActive (true);	
		}
	}

	public void Back() {
		for(int i=0;i<loadingScene.Instance.scenes.Count;i++)
		{

			if(i==loadingScene.Instance.scenes.Count-1)
			{
				loadingScene.Instance.scenes[i].SetActive(false);
				loadingScene.Instance.scenes.RemoveAt(loadingScene.Instance.scenes.Count-1);
			}
			else
			{
				loadingScene.Instance.scenes[loadingScene.Instance.scenes.Count-2].SetActive(true);

			}

		}
	}

	public void ResetValues()
	{
		isMneuActive = false;
		for(int j=0;j<bottomsButtons.Length;j++)
		{
			bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
			bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
			bottomsButtons[j].GetComponent<Button>().interactable=true;
		}
	}

	public void Menu() {
		loadingScene.Instance.objectToOpenMenu = this.gameObject;
		if(isMneuActive==false)
		{
			print("a");
			for(int i=0;i<bottomsButtons.Length;i++)
			{
				bottomsButtons[i].GetComponent<Button>().interactable=false;

				bottomsButtons[i].GetComponent<Image>().color=new Color32(131,106,106,255);
				bottomsButtons[i].GetComponentInChildren<Text>().color=new Color32(131,106,106,255);
			}


			menuScreen.SetActive(true);


			isMneuActive=true;
		}
		else
		{
			print("d");
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			menuScreen.SetActive(false);
			isMneuActive=false;

		}
	}

}
