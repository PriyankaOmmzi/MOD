using UnityEngine;
using System.Collections;
using Facebook.Unity;
using System.Collections.Generic;
using MiniJSON;

public class FacebookController : MonoBehaviour {

	private string status = "Ready";
	private string lastResponse = string.Empty;
	System.Action<bool> callBack;

	protected string LastResponse
	{
		get
		{
			return this.lastResponse;
		}
		
		set
		{
			this.lastResponse = value;
		}
	}

	protected string Status
	{
		get
		{
			return this.status;
		}
		
		set
		{
			this.status = value;
		}
	}
	public Popup popupFromServer;

	public static FacebookController _instance;

	// Use this for initialization
	void Start () {
		_instance = this;
		FB.Init(this.OnInitComplete, this.OnHideUnity);
	}


	private void OnInitComplete()
	{
		this.Status = "Success - Check log for details";
		this.LastResponse = "Success Response: OnInitComplete Called\n";
		string logMessage = string.Format(
			"OnInitCompleteCalled IsLoggedIn='{0}' IsInitialized='{1}'",
			FB.IsLoggedIn,
			FB.IsInitialized);
		Debug.Log(logMessage);
		if (AccessToken.CurrentAccessToken != null)
		{
			Debug.Log(AccessToken.CurrentAccessToken.ToString());
		}
//		if(PlayerDataParse._instance.playersParam.fblogin == 1)
//			Debug.Log ("isfblogin = "+PlayerDataParse._instance.playersParam.fblogin);
//		if (FB.IsLoggedIn && PlayerDataParse._instance.playersParam.fblogin == 1) {
//			StartCoroutine (SendData ((success) => {
//			}));
//		}
	}
	
	private void OnHideUnity(bool isGameShown)
	{
		this.Status = "Success - Check log for details";
		this.LastResponse = string.Format("Success Response: OnHideUnity Called {0}\n", isGameShown);
		Debug.Log("Is game shown: " + isGameShown);
	}

	public void FbLogin(System.Action<bool> fcnCallBack)
	{
		if (FB.IsInitialized) {
			FB.LogInWithReadPermissions (new List<string> () { "public_profile", "email", "user_friends" }, this.HandleResult);
			newMenuScene.instance.loader.SetActive (true);
			callBack = fcnCallBack;

		} else {
			fcnCallBack(false);
		}
	}

	protected void HandleResult(IResult result)
	{
		if (result == null)
		{
			this.LastResponse = "Null Response\n";
			Debug.Log(this.LastResponse);
			callBack(false);
			return;
		}

		// Some platforms return the empty string instead of null.
		if (!string.IsNullOrEmpty(result.Error))
		{
			this.Status = "Error - Check log for details";
			this.LastResponse = "Error Response:\n" + result.Error;
			callBack(false);
		}
		else if (result.Cancelled)
		{
			this.Status = "Cancelled - Check log for details";
			this.LastResponse = "Cancelled Response:\n" + result.RawResult;
			callBack(false);
		}
		else if (!string.IsNullOrEmpty(result.RawResult))
		{
			this.Status = "Success - Check log for details";
			this.LastResponse = "Success Response:\n" + result.RawResult;
			StartCoroutine (SendData(callBack));
		}
		else
		{
			this.LastResponse = "Empty Response\n";
			callBack(false);
		}
		
		Debug.Log(result.ToString());
	}

	IEnumerator SendData(System.Action<bool> fcnCallBack)
	{
		while (!TimeManager.foundTime) {
			yield return 0;
		}
		WWWForm wform = new WWWForm ();
//		wform.AddField ("tag", "userCheckFacebookLoginRegister");
		wform.AddField ("tag", "facebookUserRegisterLogin");
		Debug.Log ("accesstoken = "+AccessToken.CurrentAccessToken.TokenString);
//		Debug.Log ("user id = "+AccessToken.CurrentAccessToken.UserId);
//		Debug.Log ("device id = "+SystemInfo.deviceUniqueIdentifier);
		wform.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
		wform.AddField ("fb_access_token", AccessToken.CurrentAccessToken.TokenString);

		WWW wwwRegister = new WWW (loadingScene.Instance.baseUrl, wform);
		yield return wwwRegister;
		if (wwwRegister.error == null) {
			IDictionary fbDict = Json.Deserialize(wwwRegister.text) as IDictionary; 
			if(wwwRegister.text.Contains("err_msg") || wwwRegister.text.Contains("error_msg"))
			{
//				{"err_msg":"Invalid facebook token!"}
				fcnCallBack(false);
//				popupFromServer.ShowPopup (fbDict["err_msg"].ToString());
			}
			else
			{
				Debug.Log(wwwRegister.text);
//				{"msg":"User loggedin successfully","user":{"user_id":"74","username":"Ommzi","email":"ommziteamlead06@gmail.com","my_referral_code":"2e790c","active_status":"1"},"player":{"avatar_exp":"","gems":"","gold":"","gold_time":"0000-00-00 00:00:00","wheat":"","wheat_time":"0000-00-00 00:00:00","card_count":"","stamina":"","stamina_time":"0000-00-00 00:00:00","orb":"","orb_time":"0000-00-00 00:00:00","avatar_no":"","peace_treaty":"1","peace_treaty_start_time":"2016-07-05 05:03:05","peace_treaty_active_time":"5","avatar_stats_pool":"","avatar_defense":"","avatar_attack":"","avatar_leadership":"","max_ally":"","ally_count":"","registration_date_time":"","notification_on":"","QuestCardFormation":"","BattleCardFormation":"","FriendList":"","BlackList":"","captivesList":"","researchItems":"","interrogationList":"","ongoingResearch":"","availableResearch":"","membership_no":"","time_of_membership_no":"","first_time_login":""}}
//				"msg":"User first time register successfully","user":{"user_id":"76","username":"Ommzi","email":"ommziteamlead06@gmail.com","my_referral_code":"fcf414","active_status":"1"},"player":{"avatar_exp":"","gems":"","gold":"","gold_time":"0000-00-00 00:00:00","wheat":"","wheat_time":"0000-00-00 00:00:00","card_count":"","stamina":"","stamina_time":"0000-00-00 00:00:00","orb":"","orb_time":"0000-00-00 00:00:00","avatar_no":"","peace_treaty":"1","peace_treaty_start_time":"2016-07-05 05:20:56","peace_treaty_active_time":"5","avatar_stats_pool":"","avatar_defense":"","avatar_attack":"","avatar_leadership":"","max_ally":"","ally_count":"","registration_date_time":"","notification_on":"","QuestCardFormation":"","BattleCardFormation":"","FriendList":"","BlackList":"","captivesList":"","researchItems":"","interrogationList":"","ongoingResearch":"","availableResearch":"","membership_no":"","time_of_membership_no":"","first_time_login":""}}
				IDictionary userDict = (IDictionary) fbDict["user"]; 

				Debug.Log ("user_id"+userDict["user_id"].ToString ());
				Debug.Log ("name"+userDict["my_referral_code"].ToString ());
				Debug.Log ("msg"+fbDict["msg"].ToString ());
				Debug.Log ("my_referral_code"+userDict["my_referral_code"].ToString ());
				PlayerDataParse._instance.playersParam.userIdNo =  int.Parse (userDict["user_id"].ToString ());
				PlayerDataParse._instance.playersParam.userName =  userDict["username"].ToString ();
				PlayerDataParse._instance.playersParam.fblogin =  1;
				PlayerDataParse._instance.playersParam.referralCode = userDict["my_referral_code"].ToString ();
				PlayerDataParse._instance.SaveData ();
//				PlayerDataParse._instance.modifyXml ("userId" , userDict["user_id"].ToString ());
//				PlayerDataParse._instance.modifyXml ("userName" , userDict["username"].ToString ());
//				PlayerDataParse._instance.modifyXml ("fbLogin" , "1");
//				PlayerDataParse._instance.modifyXml ("referralCode" , userDict["my_referral_code"].ToString ());

				IDictionary playerParamaters = (IDictionary)fbDict["player"];
				IList buildingParameters = (IList)fbDict["building_data"];
//				if(buildingParameters.Count > 2)
//				{
					EmpireManager._instance.EmpireValues(buildingParameters);
					int.TryParse (playerParamaters["avatar_no"].ToString (), out PlayerParameters._instance.myPlayerParameter.avatar_no);
					newMenuScene.instance.TakeToHomeScreenFromLogin();
					PlayerParameters._instance.SavePlayerParameters (playerParamaters , true , shouldProceed => {
						if(shouldProceed)
						{
							if(PlayerParameters._instance.myPlayerParameter.avatar_no == 0)
							{
								fcnCallBack(true);
							}
							else
							{
								StartCoroutine (CardsManager._instance.GetPlayerCards (fcnCallBack));
							}
						}
						else
						{
							fcnCallBack(false);
						}
					});

	//				if(int.Parse (fbDict["login"].ToString ()) == 1)  // FIRST TIME LOGIN
	//				{
	//					// set peace treaty as 1 and save peace treaty time , show peace bird
	//					PlayerParameters._instance.myPlayerParameter.peace_treaty = 1;
	//					PlayerParameters._instance.myPlayerParameter.peace_treaty_active_time = 5;
	//					PlayerParameters._instance.myPlayerParameter.peace_treaty_start_time = System.DateTime.Now;
	//				}

//				}
//				else
//				{
//					StartCoroutine (SendData(fcnCallBack));
//				}
				Debug.Log (wwwRegister.text);
			}
		} else {
			popupFromServer.ShowPopup ("Network Error!");
			Debug.Log ("------" + wwwRegister.error);
		}
	}

	public void Logout()
	{
		if (FB.IsInitialized && FB.IsLoggedIn) {
			FB.LogOut ();
			//Remove from xml file
		}
	}
}
