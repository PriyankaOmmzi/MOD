using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using MiniJSON;
using System.Linq;

public class newMenuScene : MonoBehaviour
{
	public Sprite kitniSP, andreasSP, pnevaSP;
	public Image defaultPlayer;
	public static newMenuScene instance;
	public GameObject tapStart, signUpScene,LoginScene, charachterSelection;
	public InputField userNameField, PasswordField;
//	public AudioSource mainSound;
	public GameObject setting;
	public GameObject profileClick;
	public GameObject imageLeft;
	public GameObject imageRight;
	public GameObject imageMid;
	public Button l,m,r;
	public Transform imageLeftPos;
 	public Transform imageRightPos;
	public Transform imageMidPos;
	bool isMneuActive=false;
	public Button[] bottomsButtons;
	public GameObject menuScreen;
	public GameObject chatBtn;
	DateTime currentDate;
	DateTime oldDate;
	public float timerDecrease=8*60;
	public float timerDecreaseOrbs=9*60;
	public Text orbsText;
	public GameObject bar123;
	Vector3 imageLow2;
	public int imageMoved=0;
	public Button onSound,offSound;
	bool isSoundOn=true;
	public Sprite Andras,Ktini,Pnevma;
	public Image defaultDescription;
	public GameObject desriptionShow;
	public GameObject signUp;
	public Popup popupFromServer;
	public Popup gameStopPopup;
	public GameObject loader;
	public GameObject peaceTreaty;
	public List<Image> attackingOrbs = new List<Image>();
	public Sprite activatedOrb , deactivedOrb;
	public Text timerText;
	public int currentStamina;
	public List<Text> textFeching;
	public Slider avatarPercentage;

	// Use this for initialization


	public void Awake()
	{
		instance = this;
		Debug.Log (System.Convert.ToDateTime ("2016-07-01 00:51:30"));

	}

public void fetchDetails()
{
		

		for (int i = 0; i < attackingOrbs.Count; i++) {
			if( i < PlayerParameters._instance.myPlayerParameter.orb)
			{
				attackingOrbs[i].sprite = activatedOrb;
			}
			else
			{
				attackingOrbs[i].sprite = deactivedOrb;
			}
		}




		Int64 reqdAvtarExpForLevelUp = 0;
		if (PlayerParameters._instance.myPlayerParameter.avatar_level < PlayerParameters._instance.avatarReqdExpForLevelUp.Length) {
			reqdAvtarExpForLevelUp = PlayerParameters._instance.avatarReqdExpForLevelUp [PlayerParameters._instance.myPlayerParameter.avatar_level];
		} else {
			reqdAvtarExpForLevelUp = PlayerParameters._instance.avatarReqdExpForLevelUp [PlayerParameters._instance.avatarReqdExpForLevelUp.Length-1];

		}
		double percentValForAvatar = PlayerParameters._instance.myPlayerParameter.avatar_exp / (double)reqdAvtarExpForLevelUp;
		avatarPercentage.value = (float)percentValForAvatar;
		textFeching [7].GetComponent<Text> ().text = Mathf.FloorToInt (avatarPercentage.value * 100) + "%";
		if (PlayerParameters._instance.myPlayerParameter.guildName != "") {
			textFeching [8].text = "(" + PlayerParameters._instance.myPlayerParameter.guildPrefix + ") " + PlayerParameters._instance.myPlayerParameter.guildName;
		} else {
			textFeching [8].text = "(guild prefix) guild name";
		}
		//avatarPercentageText.text = Mathf.FloorToInt(avatarPercentage.value*100)+"%";
		textFeching[0].GetComponent<Text>().text="LV. "+(PlayerParameters._instance.myPlayerParameter.avatar_level+1);
		textFeching[1].GetComponent<Text>().text=PlayerParameters._instance.myPlayerParameter.stamina_potion.ToString();
		textFeching[2].GetComponent<Text>().text=PlayerParameters._instance.myPlayerParameter.attack_potion.ToString();
		textFeching[3].GetComponent<Text>().text=PlayerParameters._instance.myPlayerParameter.wheat.ToString();
		textFeching[4].GetComponent<Text>().text=PlayerParameters._instance.myPlayerParameter.gold.ToString();
		textFeching[5].GetComponent<Text>().text=PlayerParameters._instance.myPlayerParameter.gems.ToString();
		textFeching[6].GetComponent<Text>().text=PlayerParameters._instance.myPlayerParameter.stamina.ToString() +"/"+PlayerParameters._instance.myPlayerParameter.max_stamina.ToString();

}
	public void sliderChange(Slider sliderValue)
	{
		loadingScene.Instance.sliderValue = sliderValue.value;
		for(int i=0;i<loadingScene.Instance.allSounds.Length;i++)
		{
			if(loadingScene.Instance.allSounds[i] != null)
			{
				loadingScene.Instance.allSounds[i].volume = sliderValue.value;
			}
		}
	}

	void Start ()
	{
		Invoke("imageMove",10f);
		PlayerPrefs.SetString("newMain","yes");
//		Debug.Log ("PlayerDataParse._instance.playersParam.userId = "+PlayerDataParse._instance.playersParam.userIdNo);
		if(PlayerDataParse._instance.playersParam.userIdNo == 0)
		{
			signUpScene.SetActive(false);
			LoginScene.SetActive(false);
			charachterSelection.SetActive(false);
			tapStart.SetActive(true);
		}
		else
		{
			loader.SetActive (true);
			signUpScene.SetActive(false);
			LoginScene.SetActive(false);
			TakeToHomeScreenFromLogin ();
			tapStart.SetActive(false);
			NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
				if (isConnected)
					StartCoroutine (LoginViaUserIdApi ());
				else
				{
					gameStopPopup.ShowPopup ("Network Error!");
				}
			});



		}


		desriptionShow.SetActive(false);
		l.interactable=true;
		r.interactable=false;
		m.interactable=false;
		profileClick.SetActive(false);

		imageLow2=new Vector3(0.5f,bar123.transform.localScale.y,0);
		setting.SetActive(false);


		//scaleTimer();
		menuScreen.SetActive (false);
		currentDate = System.DateTime.Now;
		//mainSound.volume=1;
		if(!PlayerPrefs.HasKey("sysString"))
			PlayerPrefs.SetString("sysString", System.DateTime.Now.ToBinary().ToString());
		long temp = Convert.ToInt64(PlayerPrefs.GetString("sysString"));
		DateTime oldDate = DateTime.FromBinary(temp);
		print("oldDate: " + oldDate);
		TimeSpan difference = currentDate.Subtract(oldDate);
		print("Difference: " + difference);
	//	mainSound.volume=GameObject.BGMSlider.value;
		chatButton();
		//mainSound.Play();



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


	public void facebook()
	{

		Application.OpenURL("https://www.facebook.com/Myriadofdragons");
	}

	public void FbLogin()
	{
		FacebookController._instance.FbLogin ((success) =>{
			if(success)
			{
				loader.SetActive (false);
				loadingScene.Instance.readyTogo = true;
				TakeToHomeScreenFromLogin();
			}
			else
			{
				popupFromServer.ShowPopup("Could not Login");
			}
		});
	}

	public void TakeToHomeScreenFromLogin()
	{
		userNameField.text = "";
		PasswordField.text = "";
//		loader.SetActive (false);
		LoginScene.SetActive(false);
		if (PlayerParameters._instance.myPlayerParameter.avatar_no == 0) {
			charachterSelection.SetActive (true);
		} else {

			charachterSelection.SetActive (false);
			// fetchDetails();




		}
	}

	public void tapStartButton()
	{
		tapStart.SetActive(false);
		LoginScene.SetActive(true);
	}

	public void signUpClick()
	{
		SignUp.instance.onSignUp();
	}

	void OnEnable()
	{
		loadingScene.Instance.SoundPlay (0);
			 fetchDetails();
	}

	public void newUser()
	{
		LoginScene.SetActive(false);
		signUpScene.SetActive(true);
	}
	public void signUpExit()
	{
		signUp.GetComponent<SignUp>().email.text="";
		signUp.GetComponent<SignUp>().confirmEmail.text="";
		signUp.GetComponent<SignUp>().password.text="";
		signUp.GetComponent<SignUp>().confirmPassword.text="";
		signUp.GetComponent<SignUp>().userName.text="";
		signUp.GetComponent<SignUp>().refferalCode.text="";

		signUpScene.SetActive(false);
		LoginScene.SetActive(true);

	}

	public void loginClick()
	{
		if (string.IsNullOrEmpty (userNameField.text) || string.IsNullOrEmpty (PasswordField.text)) {
			popupFromServer.ShowPopup ("Please fill the required fields");
		}
		else {
			newMenuScene.instance.loader.SetActive (true);
			NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
				if (isConnected)
					StartCoroutine (LoginApi (userNameField.text, PasswordField.text));
				else
				{
					popupFromServer.ShowPopup ("Network Error!");
#if UNITY_EDITOR
					if((userNameField.text=="test@gmail.com" )&&(PasswordField.text=="test@123"))
					{
						userNameField.text="";
						PasswordField.text="";
						PlayerPrefs.SetString ("PlayerEmail" , userNameField.text);
						PlayerPrefs.SetString ("PlayerPassword" , PasswordField.text);
						TakeToHomeScreenFromLogin ();
						loader.SetActive (false);
					}
#endif
				}
			});
		}
	}

	IEnumerator LoginViaUserIdApi( )
	{
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag" ,"userGetLogin");
		wwwForm.AddField ("user_id" , PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id" , SystemInfo.deviceUniqueIdentifier );
		WWW wwwLogin = new WWW(loadingScene.Instance.baseUrl , wwwForm);
		while (!TimeManager.foundTime) {
			yield return 0;
		}
		yield return wwwLogin;
		if (wwwLogin.error == null) {
			Debug.Log(wwwLogin.text);
//			string loginText = wwwLogin.text.Replace (@"\","");
			string loginText = wwwLogin.text;
			IDictionary resultDict = (IDictionary)Json.Deserialize (loginText);
			//{"success":0,"error_msg":"User does't exist!"}
			//{"success":0,"error_msg":"Invalid login details!"}
			//{"user":{"username":"Lakhbir","email":"ommziunity02@gmail.com","my_referral_code":"9cf07e"},"success":1,"msg":"Welcome, for your first time login"}
			//{"player":{"avatar_exp":"","gems":"","gold":"","gold_time":"0000-00-00 00:00:00","wheat":"","card_count":"","stamina":"","stamina_time":"0000-00-00 00:00:00","orb":"","avatar_no":"","peace_treaty":"1","peace_treaty_start_time":"2016-07-01 00:51:30","peace_treaty_active_time":"5","avatar_stats_pool":"","avatar_defense":"","avatar_attack":"","avatar_leadership":"","max_ally":"","ally_count":"","registration_date_time":"","notification_on":"","QuestCardFormation":"","BattleCardFormation":"","FriendList":"","BlackList":"","captivesList":"","researchItems":"","interrogationList":"","ongoingResearch":"","event_formation":"","membership_no":"","time_of_membership_no":"","first_time_login":""},"user":{"username":"Lakhbir","email":"ommziunity02@gmail.com","my_referral_code":"9cf07e","user_id":"62"},"success":1,"msg":"Logged in successfully"}
			if(wwwLogin.text.Contains ("error_msg"))
			{
				charachterSelection.SetActive (false);
//				popupFromServer.ShowPopup (resultDict["error_msg"].ToString ()+"\n \n Note: You cannot play on two devices simultaneously!");
				PlayerPrefs.DeleteKey ("PlayerEmail");
				PlayerPrefs.DeleteKey ("PlayerPassword");
				PlayerPrefs.SetString("logout","yes");
				FacebookController._instance.Logout ();
//				PlayerDataParse._instance.modifyXml ("fbLogin" , "0");
//				PlayerDataParse._instance.modifyXml ("userId" , "0");
//				PlayerDataParse._instance.modifyXml ("userName" , "0");
//				PlayerDataParse._instance.modifyXml ("referralCode" , "0");
				PlayerDataParse._instance.playersParam.userIdNo =  0;
				PlayerDataParse._instance.playersParam.userName =  "";
				PlayerDataParse._instance.playersParam.fblogin =  0;
				PlayerDataParse._instance.playersParam.referralCode = "";
				PlayerDataParse._instance.SaveData ();
				onClickSettingExit();
				LoginScene.SetActive(true);
//				mainSound.time = 1;
				loader.SetActive (false);

			}
			else
			{
//				loader.SetActive (false);
				IDictionary userParamaters = (IDictionary)resultDict["user"];
				Debug.Log(userParamaters["username"]);
				Debug.Log(userParamaters["email"]);
				Debug.Log(userParamaters["my_referral_code"]);
				Debug.Log(userParamaters["user_id"]);
				
//				PlayerDataParse._instance.playersParam.userIdNo =  int.Parse (userParamaters["user_id"].ToString ());
//				PlayerDataParse._instance.playersParam.userName =  userParamaters["username"].ToString ();
//				PlayerDataParse._instance.playersParam.referralCode = userParamaters["my_referral_code"].ToString ();
//				PlayerDataParse._instance.SaveData ();
//				PlayerDataParse._instance.modifyXml ("userId" , userParamaters["user_id"].ToString ());
//				PlayerDataParse._instance.modifyXml ("userName" , userParamaters["username"].ToString ());
//				PlayerDataParse._instance.modifyXml ("fbLogin" , "0");
//				PlayerDataParse._instance.modifyXml ("referralCode" , userParamaters["my_referral_code"].ToString ());

				if (userParamaters ["email"] != null)
					PlayerParameters._instance.emailAddress = userParamaters ["email"].ToString ();
				
				int.TryParse (userParamaters["user_id"].ToString () , out PlayerParameters._instance.myPlayerParameter.user_id);

				IDictionary playerParamaters = (IDictionary)resultDict["player"];
				IList buildingParameters = (IList)resultDict["building_data"];
				EmpireManager._instance.EmpireValues(buildingParameters);
//				if(!string.IsNullOrEmpty (playerParamaters["avatar_no"].ToString ()) && !(playerParamaters["avatar_no"].ToString() == null))
				if(playerParamaters["avatar_no"] != null)
					int.TryParse (playerParamaters["avatar_no"].ToString (), out PlayerParameters._instance.myPlayerParameter.avatar_no);
				PlayerParameters._instance.SavePlayerParameters (playerParamaters , false , shouldProceed => {
					if(shouldProceed)
					{
						loadingScene.Instance.readyTogo = true;
						loader.SetActive (false);
						TakeToHomeScreenFromLogin ();
					}
					else
					{
						gameStopPopup.ShowPopup ("Network Error");
					}
				});

			}

		} else {
			Debug.Log ("eoor = "+wwwLogin.error);
			gameStopPopup.ShowPopup ("Network Error!");
		}

	}






	IEnumerator LoginApi( string userName , string password)
	{
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag" ,"userGetLogin");
		wwwForm.AddField ("username" , userName);
		wwwForm.AddField ("password" , password);
		wwwForm.AddField ("device_id" , SystemInfo.deviceUniqueIdentifier );
		WWW wwwLogin = new WWW(loadingScene.Instance.baseUrl , wwwForm);
		yield return wwwLogin;
		if (wwwLogin.error == null) {
			Debug.Log(wwwLogin.text);
			string loginText = wwwLogin.text;
//			string loginText = wwwLogin.text.Replace (@"\","");
			IDictionary resultDict = (IDictionary)Json.Deserialize (loginText);
			Debug.Log("after replace  === "+loginText);
			//{"success":0,"error_msg":"User does't exist!"}
			//{"success":0,"error_msg":"Invalid login details!"}
			//{"user":{"username":"Lakhbir","email":"ommziunity02@gmail.com","my_referral_code":"9cf07e"},"success":1,"msg":"Welcome, for your first time login"}
			//{"player":{"avatar_exp":"","gems":"","gold":"","gold_time":"0000-00-00 00:00:00","wheat":"","card_count":"","stamina":"","stamina_time":"0000-00-00 00:00:00","orb":"","avatar_no":"","peace_treaty":"1","peace_treaty_start_time":"2016-07-01 00:51:30","peace_treaty_active_time":"5","avatar_stats_pool":"","avatar_defense":"","avatar_attack":"","avatar_leadership":"","max_ally":"","ally_count":"","registration_date_time":"","notification_on":"","QuestCardFormation":"","BattleCardFormation":"","FriendList":"","BlackList":"","captivesList":"","researchItems":"","interrogationList":"","ongoingResearch":"","event_formation":"","membership_no":"","time_of_membership_no":"","first_time_login":""},"user":{"username":"Lakhbir","email":"ommziunity02@gmail.com","my_referral_code":"9cf07e","user_id":"62"},"success":1,"msg":"Logged in successfully"}

			//{"building_data":{"0":"","building_id":"0","level":"","primary_card_locked":"","primary_card_locked_no":"","upgrade_time":"","initial_value1":"","final_value1":"","currentt_time":"","max_deployed":"","currently_deployed":"","max_available":"","currently_available":"","secondary_card_locked":"","secondary_card_no":"0","sec_card_status":"0","soldiers_to_recruit":"","time_of_secondary_upgrade":"","building_exp":"","user_id":"62","building_name":"treeOfKnowledge","status":"0","upgraded_value":"0","active_time":""},"success":1,"msg":"Welcome, for your first time login"}
			if(wwwLogin.text.Contains ("error_msg"))
			{
				popupFromServer.ShowPopup (resultDict["error_msg"].ToString ()+"\n \n Note: You cannot play on two devices simultaneously!");
				userNameField.text = "";
				PasswordField.text = "";
			}
			else
			{

				userNameField.text = "";
				PasswordField.text = "";
				PlayerPrefs.SetString ("PlayerEmail" , userNameField.text);
				PlayerPrefs.SetString ("PlayerPassword" , PasswordField.text);

				IDictionary userParamaters = (IDictionary)resultDict["user"];
				Debug.Log(userParamaters["username"]);
				Debug.Log(userParamaters["email"]);
				Debug.Log(userParamaters["my_referral_code"]);
				Debug.Log(userParamaters["user_id"]);
				PlayerDataParse._instance.playersParam.userIdNo =  int.Parse (userParamaters["user_id"].ToString ());
				PlayerDataParse._instance.playersParam.userName =  userParamaters["username"].ToString ();
				PlayerDataParse._instance.playersParam.fblogin =  0;
				PlayerDataParse._instance.playersParam.referralCode = userParamaters["my_referral_code"].ToString ();
				PlayerDataParse._instance.SaveData ();
//				PlayerDataParse._instance.modifyXml ("userName" , userParamaters["username"].ToString ());
//				PlayerDataParse._instance.modifyXml ("fbLogin" , "0");
//				PlayerDataParse._instance.modifyXml ("referralCode" , userParamaters["my_referral_code"].ToString ());
//				PlayerDataParse._instance.modifyXml ("userId" , userParamaters["user_id"].ToString ());

				int.TryParse (userParamaters["user_id"].ToString () , out PlayerParameters._instance.myPlayerParameter.user_id);

				IDictionary playerParamaters = (IDictionary)resultDict["player"];
				IList buildingParameters = (IList)resultDict["building_data"];

				EmpireManager._instance.EmpireValues(buildingParameters);
//				if(!string.IsNullOrEmpty (playerParamaters["avatar_no"].ToString ()) && !(playerParamaters["avatar_no"].ToString() == null))
				if(playerParamaters["avatar_no"] != null)
					int.TryParse (playerParamaters["avatar_no"].ToString (), out PlayerParameters._instance.myPlayerParameter.avatar_no);
				TakeToHomeScreenFromLogin ();
				loader.SetActive (true);
				PlayerParameters._instance.SavePlayerParameters (playerParamaters , true , shouldProceed => {
					if(shouldProceed)
					{
						if(PlayerParameters._instance.myPlayerParameter.avatar_no != 0)
						{
							loader.SetActive (true);
							StartCoroutine (CardsManager._instance.GetPlayerCards (gotCard => {
								if(!gotCard)
								{
									gameStopPopup.ShowPopup ("Error in fetching data");
									Debug.Log ("error in geting cards player parameters .....");
								}
								else
								{
									loadingScene.Instance.readyTogo = true;
									loader.SetActive (false);
									TakeToHomeScreenFromLogin ();
								}
							}));
						}
						else
						{
							loader.SetActive (false);
							loadingScene.Instance.readyTogo = true;
						}
					}
					else
					{
						Debug.Log ("error in sending player parameters .....");
						userNameField.text = "";
						PasswordField.text = "";
						gameStopPopup.ShowPopup ("Error in fetching data");
					}
				});



			}

		} else {
			userNameField.text = "";
			PasswordField.text = "";
			popupFromServer.ShowPopup ("Network Error!");
		}

	}


	public void ForgotPassword()
	{

		if (string.IsNullOrEmpty (userNameField.text)) {
			popupFromServer.ShowPopup ("Please fill the username field.");

		} else {
			newMenuScene.instance.loader.SetActive (true);
			NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
				if (isConnected)
					StartCoroutine (PasswordApi (userNameField.text));
				else
					popupFromServer.ShowPopup ("Network Error!");
			});
		}
	}

	IEnumerator PasswordApi( string userName)
	{
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag" ,"userForgotPassword");
		wwwForm.AddField ("username" , userName);
		WWW wwwForgotPassword = new WWW(loadingScene.Instance.baseUrl , wwwForm);
		yield return wwwForgotPassword;
		if (wwwForgotPassword.error == null) {
			IDictionary resultDict = (IDictionary)Json.Deserialize (wwwForgotPassword.text);
			//{"success":0,"error_msg":"Email does't exist!"}
			//{"success":1,"msg":"Email has been sent successfully"}

			//{"success":0,"error_msg":"User does't exist!"}
			//{"success":1,"msg":"Email has been sent successfully"}

			if(wwwForgotPassword.text.Contains ("error_msg"))
			{
				popupFromServer.ShowPopup (resultDict["error_msg"].ToString ());
			}
			else
			{
				popupFromServer.ShowPopup (resultDict["msg"].ToString ());
			}

		} else {
			popupFromServer.ShowPopup ("Network Error!");
		}

	}

	public void cancelProceed()
	{
		//LoginScene.SetActive(true);
		desriptionShow.SetActive(false);

	}

	public void chrachterSelectionClick(Button name)
	{
		if(name.name=="Andras")
		{
			desriptionShow.SetActive(true);
			defaultDescription.sprite=Andras;
			defaultPlayer.sprite=andreasSP;
			PlayerParameters._instance.myPlayerParameter.avatar_no = 1;
		}
		else if(name.name=="Ktini")
		{
			desriptionShow.SetActive(true);

			defaultDescription.sprite=Ktini;
			defaultPlayer.sprite=kitniSP;
			PlayerParameters._instance.myPlayerParameter.avatar_no = 2;

		}
		else if(name.name=="Pnevma")
		{
			desriptionShow.SetActive(true);

			defaultDescription.sprite=Pnevma;
			defaultPlayer.sprite=pnevaSP;
			PlayerParameters._instance.myPlayerParameter.avatar_no = 3;


		}
	}

	public void ChangeDefaultPlayerSprite()
	{
		switch (PlayerParameters._instance.myPlayerParameter.avatar_no) {
		case 1:
			defaultPlayer.sprite=andreasSP;
			break;
		case 2:
			defaultPlayer.sprite=kitniSP;
			break;
		case 3:
			defaultPlayer.sprite=pnevaSP;
			break;
		}
	}

	public void gameStart()
	{
		newMenuScene.instance.loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected)
				StartCoroutine (CharacterSelectApi ());
			else
			{

				popupFromServer.ShowPopup ("Network Error!");
				#if UNITY_EDITOR
				charachterSelection.SetActive(false);
				desriptionShow.SetActive(false);

				PlayerPrefs.SetString("logout","no");
				PlayerPrefs.SetString("tap","no");
				#endif

			}
		});

	}

	void AvatarStats()
	{
		switch (PlayerParameters._instance.myPlayerParameter.avatar_no) {
		case 1:  //Andras
			PlayerParameters._instance.myPlayerParameter.avatar_attack = 30;
			PlayerParameters._instance.myPlayerParameter.avatar_defense = 20;
			PlayerParameters._instance.myPlayerParameter.avatar_leadership = 20;
			break;
		case 2: //Kitni
			PlayerParameters._instance.myPlayerParameter.avatar_attack = 20;
			PlayerParameters._instance.myPlayerParameter.avatar_defense = 30;
			PlayerParameters._instance.myPlayerParameter.avatar_leadership = 20;
			break;
		case 3: //Pvenma
			PlayerParameters._instance.myPlayerParameter.avatar_attack = 20;
			PlayerParameters._instance.myPlayerParameter.avatar_defense = 20;
			PlayerParameters._instance.myPlayerParameter.avatar_leadership = 30;
			break;
		}
			PlayerParameters._instance.myPlayerParameter.max_stamina = 20;

	}

	IEnumerator CharacterSelectApi()
	{
		AvatarStats ();
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag" ,"insertAllPlayer");
		wwwForm.AddField ("user_id" , PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id" , SystemInfo.deviceUniqueIdentifier );
		wwwForm.AddField ("avatar_no" , PlayerParameters._instance.myPlayerParameter.avatar_no );
		wwwForm.AddField ("avatar_defense" , PlayerParameters._instance.myPlayerParameter.avatar_defense );
		wwwForm.AddField ("avatar_attack" , PlayerParameters._instance.myPlayerParameter.avatar_attack );
		wwwForm.AddField ("avatar_leadership" , PlayerParameters._instance.myPlayerParameter.avatar_leadership );
		wwwForm.AddField ("max_stamina" , PlayerParameters._instance.myPlayerParameter.max_stamina );
		WWW wwwLogin = new WWW(loadingScene.Instance.baseUrl , wwwForm);
		yield return wwwLogin;
		if (wwwLogin.error == null) {
			Debug.Log(wwwLogin.text);
			IDictionary resultDict = (IDictionary)Json.Deserialize (wwwLogin.text);
			Debug.Log("TEXT = "+wwwLogin.text);
			//{"user_id":"6","avatar_no":"1","success":1,"msg":"Users avtar selected"}
			//{"success":0,"error_msg":"User does't exist!"}
			if(wwwLogin.text.Contains ("error_msg"))
			{
				popupFromServer.ShowPopup ("Network Error! Try Again");
			}
			else
			{
				StartCoroutine (GetInitialPlayerCards (5, (isSuccess, msgString) => {
					if(isSuccess)
					{
						loader.SetActive(false);
						charachterSelection.SetActive(false);
						desriptionShow.SetActive(false);

						PlayerPrefs.SetString("logout","no");
						PlayerPrefs.SetString("tap","no");
					}
					else
					{
						popupFromServer.ShowPopup ("Network Error!");
					}
				}));

//				Debug.Log("fetching cards");
//				StartCoroutine (FetchCardsOnStart (gotMyCards =>{
//					if(gotMyCards)
//					{
//						loader.SetActive(false);
//						charachterSelection.SetActive(false);
//						desriptionShow.SetActive(false);
//
//						PlayerPrefs.SetString("logout","no");
//						PlayerPrefs.SetString("tap","no");
//					}
//					else
//					{
//						popupFromServer.ShowPopup ("Network Error!");
//					}
//				}));
			}

		} else {
			popupFromServer.ShowPopup ("Network Error!");
		}
	}

	void CardProbability(int noOfCardsRequired , ref string cardrarity ,  ref string noOfCards)
	{
		List<string> rarity = new List<string> (){ "Legendary" ,"Mega" , "Super" , "Uncommon" , "Common" };
		while (rarity.Count > 0 && noOfCardsRequired > 0) {
			int noOfCardsForRarity = 0;
			string randomRarity = rarity [UnityEngine.Random.Range (0, rarity.Count)];
			cardrarity += (randomRarity+",");
			if(rarity.Count == 1)
				noOfCardsForRarity = noOfCardsRequired;
			else
				noOfCardsForRarity = UnityEngine.Random.Range (1, (noOfCardsRequired+1));
			noOfCards+=noOfCardsForRarity+",";
			noOfCardsRequired-=noOfCardsForRarity;
			rarity.Remove (randomRarity);
		}
		cardrarity = cardrarity.Remove (cardrarity.Length - 1);
		noOfCards = noOfCards.Remove (noOfCards.Length - 1);

	}


	IEnumerator GetInitialPlayerCards(int no_of_cards , Action <bool , string> callBack)
	{
		string cardrarity = "";
		string noOfCards = "";
		CardProbability (no_of_cards, ref cardrarity , ref noOfCards);
		Debug.Log ("cardrarity = "+cardrarity);
		Debug.Log ("no_of_cards = "+no_of_cards);
		WWWForm wForm = new WWWForm ();
		wForm.AddField ("tag" , "userCardsPlayercards");
		wForm.AddField ("user_id" , PlayerDataParse._instance.playersParam.userId);
		wForm.AddField ("device_id" , SystemInfo.deviceUniqueIdentifier);
		wForm.AddField ("rarity" , cardrarity);
		wForm.AddField ("no_of_cards" , noOfCards);
		WWW www = new WWW (loadingScene.Instance.baseUrl , wForm);
		yield return www;
		if (www.error == null && !string.IsNullOrEmpty(www.text)) {
			Debug.Log (www.text);
			IDictionary cardDict = (IDictionary)Json.Deserialize(www.text) as IDictionary;
			if (www.text.Contains ("error_msg")) {
				//{"success":0,"error_msg":"No cards available!"}
				callBack (false , cardDict["error_msg"].ToString());
			} else {
				Debug.Log ("Success : " + www.text);
				//{"success":1,"msg":"Cards data success","Player_Card_detail":[{"card_id":"205","cardCategory":"Ktini","cardName":"Firasil","cardCost":"6","cardRarity":"Uncommon","cardskillstrength1":"Weak",
				//"cardskillstrength2":"Weak","cardSkillsname1":"Fierce Outrage","cardSkillsname2":"Fierce Outrage","card_no_in_players_list":398,"experience":100,"cardLevel":1,"subCardId":"232","subCard_cardId":"205"
				//,"subCardtype":"Warrior","subcardLeadership":"148","subCardAttack":"213","subCardDefense":"179"}]}


				for (int i = 0; i < no_of_cards; i++) {
					IDictionary newCardData = ((Json.Deserialize (www.text) as IDictionary) ["Player_Card_detail"] as IList) [i] as IDictionary;

					CardsManager.CardParameters newcard = new CardsManager.CardParameters ();

					newcard.card_name = newCardData ["cardName"].ToString ();
					newcard.card_level = int.Parse (newCardData ["cardLevel"].ToString ());
					newcard.card_id_in_database = int.Parse (newCardData ["card_id"].ToString ());
					newcard.cardClass = newCardData ["cardCategory"].ToString ();
					newcard.rarity = newCardData ["cardRarity"].ToString ();
					newcard.type = newCardData ["subCardtype"].ToString ();
					newcard.attack = int.Parse (newCardData ["subCardAttack"].ToString ());
					newcard.defense = int.Parse (newCardData ["subCardDefense"].ToString ());
					newcard.leadership = int.Parse (newCardData ["subcardLeadership"].ToString ());
					newcard.card_soldiers = newcard.leadership;
					newcard.experience = int.Parse (newCardData ["experience"].ToString ());
					newcard.cardCost = int.Parse (newCardData ["cardCost"].ToString ());
					newcard.skill_1 = newCardData ["cardSkillsname1"].ToString ();
					newcard.skill_1_Strength = newCardData ["cardskillstrength1"].ToString ();
					newcard.skill_2 = newCardData ["cardSkillsname2"].ToString ();
					newcard.skill_2_Strength = newCardData ["cardskillstrength2"].ToString ();
					newcard.card_id_in_playerList = int.Parse (newCardData ["card_no_in_players_list"].ToString ());
					newcard.skill1_level = 1;
					newcard.skill2_level = 1;
					newcard.skill1_exp = CardsManager._instance.GetBaseExp (newcard.rarity);
					newcard.skill2_exp = CardsManager._instance.GetBaseExp (newcard.rarity);
					newcard.max_level = 25;
					newcard.cardSpriteFromResources = Resources.Load<Sprite> ("images/" + newcard.card_name);
					CardsManager._instance.AddCardForEmpire (newcard);
					CardsManager._instance.mycards.Add (newcard);
				}
				callBack (true , "");
			}
		}
		else
			callBack (false , "Error Updating Data!");

	}

	IEnumerator FetchCardsOnStart(Action <bool> callBack)
	{
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag" ,"cardCountAll");
		WWW wwwCards = new WWW(loadingScene.Instance.baseUrl , wwwForm);
		yield return wwwCards;
		if (wwwCards.error == null) {
			Debug.Log(wwwCards.text);
			IDictionary resultDict = (IDictionary)Json.Deserialize (wwwCards.text);
			//{"success":1,"msg":"Total cards count is:105","card_ids":["166","167","169","170","171","172","173","174","175","176","177","178","179","180","181","182","183","184","185","186","187","188","189","191","192","193","194","195","196","197","198","199","200","201","202","203","204","205","206","207","208","209","210","211","212","213","214","215","216","217","218","219","220","221","222","223","224","225","226","227","228","229","230","231","232","233","234","235","236","237","238","239","240","241","242","243","244","245","246","247","248","249","250","251","252","253","254","255","269","270","271","272","273","274","275","276","277","278","279","280","281","282","283","284","286"]}

			//{"success":1,"msg":"Total cards count is:105","card_detail":{"card_data":[{"card_id":"166","Card_name":"Conchobhar"},
		//{"card_id":"167","Card_name":"Zephyros"},{"card_id":"169","Card_name":"Bedivere"},{"card_id":"170","Card_name":"Quintilianus"},{"card_id":"171","Card_name":"Pericles"},{"card_id":"172","Card_name":"Cyneheard"},{"card_id":"173","Card_name":"Epaphras"},{"card_id":"174","Card_name":"Silvia"}


			if(wwwCards.text.Contains ("error_msg"))
			{
				callBack(false);
			}
			else
			{
//				IList cardIds = (IList) resultDict["card_ids"];
//				Debug.Log(cardIds.Count);
//				List <int> cardsListFromServer = new List<int>();
//				for(int i = 0 ; i < cardIds.Count ; i++)
//				{
//					cardsListFromServer.Add (int.Parse (cardIds[i].ToString ()));
//				}
//				if(PlayerParameters._instance.myPlayerParameter.card_count < 5)
//				{
//					string cardsToFetchParametersFor = ChooseRandomCardsFromList(cardsListFromServer , 5- PlayerParameters._instance.myPlayerParameter.card_count);
//					Debug.Log("cardsToFetchParametersFor = "+cardsToFetchParametersFor);
//					StartCoroutine (FetchCardParameters(cardsToFetchParametersFor  , (5- PlayerParameters._instance.myPlayerParameter.card_count), callBack));
//				}
//				else
//				{
//					callBack(true);
//				}

				IDictionary cardDetails = (IDictionary) resultDict["card_detail"];
				IList  cardIds= (IList) cardDetails["card_data"];
				Debug.Log(cardIds.Count);
				List <int> cardsListFromServer = new List<int>();
				for(int i = 0 ; i < cardIds.Count ; i++)
				{
					IDictionary idPerCard = (IDictionary) cardIds[i];
					cardsListFromServer.Add (int.Parse (idPerCard["card_id"].ToString ()));
				}
				if(PlayerParameters._instance.myPlayerParameter.card_count < 5)
				{
					string cardsToFetchParametersFor = ChooseRandomCardsFromList(cardsListFromServer , 5- PlayerParameters._instance.myPlayerParameter.card_count);
					Debug.Log("cardsToFetchParametersFor = "+cardsToFetchParametersFor);
					StartCoroutine (FetchCardParameters(cardsToFetchParametersFor  , (5- PlayerParameters._instance.myPlayerParameter.card_count), callBack));
				}
				else
				{
					callBack(true);
				}

			}

		} else {
			callBack(false);
		}
	}

	public IEnumerator FetchCardParameters(string card_ids ,int noOfCardsToChose ,Action<bool> callback)
	{
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag" ,"getCardData");
		wwwForm.AddField ("card_id" ,card_ids);
		WWW wwwCards = new WWW(loadingScene.Instance.baseUrl , wwwForm);
		yield return wwwCards;
		if (wwwCards.error == null) {
			Debug.Log(wwwCards.text);
			IDictionary resultDict = (IDictionary)Json.Deserialize (wwwCards.text);
			//{"success":1,"msg":"Card detail","Params":[["166","Andras","Conchobhar","images\/card04f_logo.jpg","149","130","Common","Weak","Weak","Fierce Outrage","Fierce Outrage","2","0","122",""],["167","Andras","Zephyros","images\/card03f_logo .jpg","127","123","Common","Weak","Weak","Fierce Outrage","Fierce Outrage","2","0","151",""]],"Subcard_details":[{"card_id":"166","type":"Perfect","leadership":"134","attack":"164","defense":"143"},{"card_id":"166","type":"Warrior","leadership":"122","attack":"164","defense":"130"},{"card_id":"166","type":"Defend","leadership":"122","attack":"149","defense":"143"},{"card_id":"166","type":"Leader","leadership":"134","attack":"149","defense":"130"},{"card_id":"166","type":"Average","leadership":"122","attack":"149","defense":"130"},{"card_id":"167","type":"Perfect","leadership":"166","attack":"140","defense":"135"},{"card_id":"167","type":"Warrior","leadership":"151","attack":"140","defense":"123"},{"card_id":"167","type":"Defend","leadership":"151","attack":"127","defense":"135"},{"card_id":"167","type":"Leader","leadership":"166","attack":"127","defense":"123"},{"card_id":"167","type":"Average","leadership":"151","attack":"127","defense":"123"}]}
			//CREATE TABLE IF NOT EXISTS `cards` (`id = 0` ,`category = 1`,,`name = 2` ,`image = 3`,`attack = 4` ,
			//`defense = 5`,`rarity = 6` ,`skillstrength1 = 7',`skillstrength2 = 8,`skillsname1=9` ,
			//'skillsname2 = 10`,`cost = 11` ,`trading = 12` ,`leadership = 13`,`desc = 14`

			//{"success":0,"error_msg":"No card found!"}
			if(wwwCards.text.Contains ("error_msg"))
			{
				callback(false);
			}
			else
			{
			//		 {"success":1,"msg":"Card detail","Params":[["166","Andras","Conchobhar","images\/card04f_logo.jpg","149","130","Common","Weak","Weak","Fierce Outrage","Fierce Outrage","2","0","122",""],["169","Andras","Bedivere","images\/card17f_logo.jpg","115","123","Common","Weak","Weak","Fierce Outrage","Fierce Outrage","2","0","165",""],["221","Andras","Zyxtris","images\/Barbarian 20 logo.jpg","167","115","Common","Weak","Weak","Fierce Outrage","Fierce Outrage","2","0","118",""]],
			//"Subcard_details":[{"card_id":"166","type":"Perfect","leadership":"134","attack":"164","defense":"143"},{"card_id":"166","type":"Warrior","leadership":"122","attack":"164","defense":"130"},{"card_id":"166","type":"Defend","leadership":"122","attack":"149","defense":"143"},{"card_id":"166","type":"Leader","leadership":"134","attack":"149","defense":"130"},{"card_id":"166","type":"Average","leadership":"122","attack":"149","defense":"130"},{"card_id":"169","type":"Perfect","leadership":"182","attack":"127","defense":"135"},{"card_id":"169","type":"Warrior","leadership":"165","attack":"127","defense":"123"},{"card_id":"169","type":"Defend","leadership":"165","attack":"115","defense":"135"},{"card_id":"169","type":"Leader","leadership":"182","attack":"115","defense":"123"},{"card_id":"169","type":"Average","leadership":"165","attack":"115","defense":"123"},{"card_id":"221","type":"Perfect","leadership":"130","attack":"184","defense":"127"},{"card_id":"221","type":"Warrior","leadership":"118","attack":"184","defense":"115"},{"card_id":"221","type":"Defend","leadership":"118","attack":"167","defense":"127"},{"card_id":"221","type":"Leader","leadership":"130","attack":"167","defense":"115"},{"card_id":"221","type":"Average","leadership":"118","attack":"167","defense":"115"}]}
				IList cardParams = (IList) resultDict["Params"];
				IList subCardDetails = (IList) resultDict["Subcard_details"];
				Debug.Log(cardParams.Count);
				for(int i = 0 ; i < cardParams.Count ; i++)
				{
					IList eachCardParams = (IList) cardParams[i];
					Debug.Log("id in database = "+eachCardParams[0].ToString ());
					Debug.Log("class = "+eachCardParams[1].ToString ());
					Debug.Log("name = "+eachCardParams[2].ToString ());
					Debug.Log("rarity = "+eachCardParams[6].ToString ());
					Debug.Log("skillstrength1 = "+eachCardParams[7].ToString ());
					Debug.Log("skillstrength2 = "+eachCardParams[8].ToString ());
					Debug.Log("skillsname1 = "+eachCardParams[9].ToString ());
					Debug.Log("skillname2 = "+eachCardParams[10].ToString ());


					int typeOfCardChosen = UnityEngine.Random.Range (i*5 , (i*5+5));
					IDictionary finalValuesOfCard = (IDictionary)subCardDetails[typeOfCardChosen];
					Debug.Log("card id in database = "+finalValuesOfCard["card_id"]);
					Debug.Log("type = "+finalValuesOfCard["type"].ToString ());
					Debug.Log("leadership = "+finalValuesOfCard["leadership"].ToString ());
					Debug.Log("attack= "+finalValuesOfCard["attack"].ToString ());
					Debug.Log("defense= "+finalValuesOfCard["defense"].ToString ());

					Dictionary<string, string> cardParameters = new Dictionary<string, string>();
					cardParameters.Add ("card_name" , eachCardParams[2].ToString ());
					cardParameters.Add ("card_id_in_database" ,eachCardParams[0].ToString ());
					cardParameters.Add ("rarity" ,eachCardParams[6].ToString ());
					cardParameters.Add ("type" ,finalValuesOfCard["type"].ToString ());
					cardParameters.Add ("class" ,eachCardParams[1].ToString ());
					cardParameters.Add ("attack" ,finalValuesOfCard["attack"].ToString ());
					cardParameters.Add ("defense" ,finalValuesOfCard["defense"].ToString ());
					cardParameters.Add ("leadership" ,finalValuesOfCard["leadership"].ToString ());
					cardParameters.Add ("card_soldiers" ,finalValuesOfCard["leadership"].ToString ());
					cardParameters.Add ("card_cost" , eachCardParams[11].ToString ());
					cardParameters.Add ("experience" , "100".ToString ());
					cardParameters.Add ("card_level" ,"1");

					cardParameters.Add ("skill_1" ,eachCardParams[9].ToString ());
					cardParameters.Add ("skill_1_Strength" ,eachCardParams[7].ToString ());
					cardParameters.Add ("skill_2" ,eachCardParams[10].ToString ());
					cardParameters.Add ("skill_2_Strength" ,eachCardParams[8].ToString ());
					cardParameters.Add ("is_captive" ,"0");
					cardParameters.Add ("fear_factor" ,"0");

					CardsManager.CardParameters cardToSave = new CardsManager.CardParameters();
					cardToSave.card_name = eachCardParams[2].ToString ();
					cardToSave.card_id_in_database = int.Parse (eachCardParams[0].ToString ());
					cardToSave.rarity = eachCardParams[6].ToString ();
					cardToSave.type = finalValuesOfCard["type"].ToString ();
					cardToSave.cardClass = eachCardParams[1].ToString ();
					cardToSave.attack = int.Parse (finalValuesOfCard["attack"].ToString ());
					cardToSave.defense = int.Parse (finalValuesOfCard["defense"].ToString ());
					cardToSave.leadership = cardToSave.leadership;
					cardToSave.card_soldiers = int.Parse (finalValuesOfCard["leadership"].ToString ());
					cardToSave.experience = CardExperience(eachCardParams[6].ToString ());
					cardToSave.skill_1 = eachCardParams[9].ToString ();
					cardToSave.skill_1_Strength = eachCardParams[7].ToString ();
					cardToSave.skill_2 = eachCardParams[10].ToString ();
					cardToSave.skill_2_Strength = eachCardParams[8].ToString ();
					cardToSave.is_captive = 0;
					cardToSave.fear_factor = 0;
					cardToSave.card_level = 1;
					cardToSave.cardCost = int.Parse (eachCardParams[11].ToString ());
					StartCoroutine (SendCardData(cardParameters , callback , cardToSave , noOfCardsToChose));
				}
			}

		} else {
			callback(false);
		}
	}


	int CardExperience(string rarity)
	{
		int cardExp = 0;
		switch (rarity) {
		case "Common":
			cardExp = 130;
			break;
		case "Uncommon":
			cardExp = 175;
			break;
		case "Super":
			cardExp = 245;
			break;
		case "Mega":
			cardExp = 370;
			break;
		case "Legendary":
			cardExp = 550;
			break;


		}
		return cardExp;
	}

	IEnumerator SendCardData(Dictionary<string, string> cardParametersFormal , Action<bool> callBack , CardsManager.CardParameters cardStructToadd , int noOfCardsToChose)
	{
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag" ,"doAddUpdatePlayerCards");
		wwwForm.AddField ("user_id" , PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id" , SystemInfo.deviceUniqueIdentifier);
//		KeyValuePair<string , string>[] parameters = new KeyValuePair<string, string>[cardParametersFormal.Count];
		for (int i = 0; i < cardParametersFormal.Count; i++) {
			wwwForm.AddField(cardParametersFormal.Keys.ElementAt(i) , cardParametersFormal.Values.ElementAt(i));
			Debug.Log (cardParametersFormal.Keys.ElementAt(i) +","+cardParametersFormal.Values.ElementAt(i));
		}
		WWW wwwCards = new WWW(loadingScene.Instance.baseUrl , wwwForm);
		yield return wwwCards;
		if (wwwCards.error == null) {

			Debug.Log(wwwCards.text);

			IDictionary cardDict = (IDictionary)Json.Deserialize(wwwCards.text);
			IDictionary cardDetail = (IDictionary)cardDict["Player_Card_detail"];
			int.TryParse ( cardDetail["card_no_in_players_list"].ToString() , out cardStructToadd.card_id_in_playerList );
			cardStructToadd.cardSpriteFromResources  = (Sprite)Resources.Load<Sprite>("images/"+cardStructToadd.card_name);

			PlayerParameters._instance.myPlayerParameter.maxDeployedSoldiers+=cardStructToadd.leadership;
			CardsManager._instance.mycards.Add (cardStructToadd);
			CardsManager._instance.cardButtonOfEmpire[PlayerParameters._instance.myPlayerParameter.card_count].name = cardStructToadd.card_id_in_playerList.ToString ();
			CardsManager._instance.cardButtonOfEmpire1[PlayerParameters._instance.myPlayerParameter.card_count].name = cardStructToadd.card_id_in_playerList.ToString ();

			loadingScene.Instance.randomCards.Add (cardStructToadd.card_id_in_playerList);
			CardsManager._instance.cardButtonOfEmpire[PlayerParameters._instance.myPlayerParameter.card_count].GetComponent<Image>().sprite = cardStructToadd.cardSpriteFromResources;
			CardsManager._instance.cardButtonOfEmpire1[PlayerParameters._instance.myPlayerParameter.card_count].GetComponent<Image>().sprite = cardStructToadd.cardSpriteFromResources;
			PlayerParameters._instance.myPlayerParameter.card_count++;
			if(PlayerParameters._instance.myPlayerParameter.card_count == noOfCardsToChose)
			{
				callBack(true);
			}
			//{"success":1,"msg":"Player card success","Player_Card_detail":{"id":"4","user_id":"62","card_name":"abd","card_id_in_database":"167","card_no_in_players_list":"0","rarity":"1","type":"Common","class":"Andreas","attack":"100","defense":"123","leadership":"1234","experience":"123","skill_1":"FierceOutrage","skill_1_Strength":"Weak","skill_2":"","skill_2_Strength":"","is_captive":"0","fear_factor":"0","created":"0000-00-00 00:00:00","modified":"0000-00-00 00:00:00"}}

		} else {
			callBack(false);
		}
	}


	public string ChooseRandomCardsFromList(List <int> cardsListFromServerFormal ,int noOfcardsToChose)
	{
		string cardsToFetch = "";
		for (int j = 0; j < noOfcardsToChose; j++) {
			int card = UnityEngine.Random.Range (0 ,cardsListFromServerFormal.Count );
			cardsToFetch+=cardsListFromServerFormal[card]+",";
			cardsListFromServerFormal.RemoveAt (card);
		}
		string trimmed = cardsToFetch.TrimEnd(',');
		return trimmed;
	}



	public void onClickSoundOn()
	{
		if(isSoundOn==true)
		{
			onSound.interactable=false;
			offSound.interactable=true;

			isSoundOn=false;

		}
		else
		{
			onSound.interactable=true;
		}
	}

//	public void onClickSoundOff()
//	{
//		if(isSoundOn==false)
//		{
//			offSound.interactable=false;
//			isSoundOn=true;
//		}
//		else
//		{
//			offSound.interactable=true;
//		}
//	}

	public void onClickSetting()
	{
		for(int i=0;i<loadingScene.Instance.bgmSliders.Length;i++)
		{
			if(loadingScene.Instance.bgmSliders[i] != null)
				loadingScene.Instance.bgmSliders[i].value=loadingScene.Instance.sliderValue;
		}
		for(int j=0;j<bottomsButtons.Length;j++)
		{

			bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
			bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
			bottomsButtons[j].GetComponent<Button>().interactable=true;

		}
		print("yes this work");
		menuScreen.SetActive(false);
		isMneuActive=false;
		setting.SetActive(true);

	}
	public void onClickSettingExit()
	{
		setting.SetActive(false);

	}
	public void chatClick()
	{
		PlayerPrefs.SetString("newMain","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			print("yes this work");
			menuScreen.SetActive(false);
			isMneuActive=false;

		}
		else
		{

		}
		loadingScene.Instance.chat();

	}
	public void community()
	{
		PlayerPrefs.SetString("newMain","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			print("yes this work");
			menuScreen.SetActive(false);
			isMneuActive=false;

		}
		else
		{

		}
		loadingScene.Instance.community ();

		//Application.LoadLevel("community");
	}

	public void clickProfile()
	{
		loadingScene.Instance.playerProfile ();
	}
	public void clickProfileExit()
	{
		profileClick.SetActive(false);
	}
	//-------------------- img-------------
	void imageMove()
	{
		l.interactable=false;
		r.interactable=false;
		m.interactable=true;
		imageRight.transform.position=imageRightPos.transform.position;
		iTween.MoveTo(imageLeft,iTween.Hash("x",imageLeftPos.transform.position.x,"time",1f));
		iTween.MoveTo(imageMid,iTween.Hash("x",imageMidPos.transform.position.x,"time",1f));
		Invoke("imageMove2",10f);
//		print("1");
	}

	void imageMove2()
	{
		l.interactable=false;
		r.interactable=true;
		m.interactable=false;
		//imageMidPos=imageLeft.transform.position;
		imageLeft.transform.position=imageRightPos.transform.position;
		iTween.MoveTo(imageRight,iTween.Hash("x",imageMidPos.transform.position.x,"time",1f));
		iTween.MoveTo(imageMid,iTween.Hash("x",imageLeftPos.transform.position.x,"time",1f));
		Invoke("imageMove3",10f);
//		print("2");

	}

	void imageMove3()
	{
		l.interactable=true;
		r.interactable=false;
		m.interactable=false;
		//imageMidPos=imageLeft.transform.position;
		imageMid.transform.position=imageRightPos.transform.position;
		iTween.MoveTo(imageRight,iTween.Hash("x",imageLeftPos.transform.position.x,"time",1f));
		iTween.MoveTo(imageLeft,iTween.Hash("x",imageMidPos.transform.position.x,"time",1f));
//		print("3");

		Invoke("imageMove",10f);
	}

	//-------------------------------------


	public void tradeScene()
	{
//		if (!Trading.BazaarContent.isLocked) {
			//manish
			DateTime tradeUnlockTime = PlayerParameters._instance.myPlayerParameter.registration_date_time.AddDays (10);
			TimeSpan difference = tradeUnlockTime - TimeManager._instance.GetCurrentServerTime ();
			int hours = difference.Days * 24 + difference.Hours;
			int minutes = difference.Minutes;
			int seconds = difference.Seconds;
		
			Debug.Log ("Hours left for trade unlocking: " + hours);
			Debug.Log ("Minutes left for trade unlocking: " + minutes);
			Debug.Log ("Seconds left for trade unlocking: " + seconds);
			Debug.Log ("Level: " + PlayerParameters._instance.myPlayerParameter.avatar_level);
		
//			if (hours <= 0 && minutes <= 0 && seconds <= 0 && PlayerParameters._instance.myPlayerParameter.avatar_level >= 15) {
		
				PlayerPrefs.SetString ("newMain", "yes");
				if (isMneuActive == true) {
			
					for (int j=0; j<bottomsButtons.Length; j++) {
						bottomsButtons [j].GetComponent<Image> ().color = new Color32 (255, 255, 255, 255);
						bottomsButtons [j].GetComponentInChildren<Text> ().color = new Color32 (255, 243, 137, 255);
						bottomsButtons [j].GetComponent<Button> ().interactable = true;
					}
					print ("yes this work");
					menuScreen.SetActive (false);
					isMneuActive = false;
			
				}
		
				loadingScene.Instance.trade ();
		
//			} else {
//				Debug.Log ("trading locked");
//			}
//		}
	}

	public void empireScen()
	{
		PlayerPrefs.SetString("newMain","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			print("yes this work");
			menuScreen.SetActive(false);
			isMneuActive=false;

		}
		else
		{

		}

		loadingScene.Instance.empire ();


		//Application.LoadLevel("empireScene");
	}
	public void cardCollections()
	{

		PlayerPrefs.SetString("newMain","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			print("yes this work");
			menuScreen.SetActive(false);
			isMneuActive=false;

		}
		else
		{

		}
		loadingScene.Instance.cardCollecton();

		//Application.LoadLevel("Battle_Layout4");
	}
	public void shopScene()
	{

		PlayerPrefs.SetString("newMain","yes");

		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			print("yes this work");
			menuScreen.SetActive(false);
			isMneuActive=false;

		}
		else
		{

		}
		loadingScene.Instance.shop ();

		//Application.LoadLevel("shopScene");
	}
	public void inventory()
	{

		PlayerPrefs.SetString("newMain","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			print("yes this work");
			menuScreen.SetActive(false);
			isMneuActive=false;

		}
		else
		{

		}
		loadingScene.Instance.inventory();


		//Application.LoadLevel("inventoryScene");
	}

	void OnApplicationQuit()
	{
		PlayerPrefs.SetString("sysString", System.DateTime.Now.ToBinary().ToString());
		print("Saving this date to prefs: " + System.DateTime.Now);

	}

	void onApplicationExit()
	{
		Application.Quit();
	}
	public void battle()
	{

		PlayerPrefs.SetString("newMain","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			print("yes this work");
			menuScreen.SetActive(false);
			isMneuActive=false;

		}
		else
		{

		}
		loadingScene.Instance.battleScene();

	}
	public void quest()
	{

		PlayerPrefs.SetString("newMain","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			print("yes this work");
			menuScreen.SetActive(false);
			isMneuActive=false;

		}
		else
		{

		}
		loadingScene.Instance.quest ();

	//	Application.LoadLevel("quest");

	}
	public void exitMenu()
	{
		menuScreen.SetActive (false);

	}
	public void menuButton()
	{
		loadingScene.Instance.objectToOpenMenu = this.gameObject;
		if(isMneuActive==false)
		{
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
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;

			}
			print("yes this work");
			menuScreen.SetActive(false);
			isMneuActive=false;

		}

		//menuScreen.SetActive (true);

	}
	public void chatButton()
	{

		if(PlayerPrefs.GetString("chat")=="off")
		{
			//chatBtn.GetComponent<Image>().raycastTarget=true;

			chatBtn.GetComponent<DragHandeler>().enabled=true;
			chatBtn.GetComponent<CanvasGroup>().blocksRaycasts=true;

			chatBtn.GetComponent<Button>().interactable=true;
			chatBtn.GetComponentInChildren<Text>().enabled=true;
			PlayerPrefs.SetString("chat","on");

			//PlayerPrefs.SetString("chat","on");

		}

	}

	//Shivam
	public void Swap_Stamina()
	{
		if (PlayerParameters._instance.myPlayerParameter.membership_no==7 && PlayerParameters._instance.myPlayerParameter.orb < PlayerParameters._instance.myPlayerParameter.maxOrb && PlayerParameters._instance.myPlayerParameter.stamina > Convert.ToInt32((PlayerParameters._instance.myPlayerParameter.stamina * 12.5) / 100)) {
			PlayerParameters._instance.myPlayerParameter.stamina -= Convert.ToInt32((PlayerParameters._instance.myPlayerParameter.stamina * 12.5) / 100);
			PlayerParameters._instance.myPlayerParameter.orb += 1;
			Dictionary<string, string> avatarParameters = new Dictionary<string, string> ();
			avatarParameters.Add ("stamina", PlayerParameters._instance.myPlayerParameter.stamina.ToString ());
			avatarParameters.Add ("orb", PlayerParameters._instance.myPlayerParameter.orb.ToString ());
			StartCoroutine (PlayerParameters._instance.SendPlayerParameters (avatarParameters, null));
			Debug.Log ("Successfully swap");
		} else if (PlayerParameters._instance.myPlayerParameter.membership_no==14 && PlayerParameters._instance.myPlayerParameter.orb < PlayerParameters._instance.myPlayerParameter.maxOrb && PlayerParameters._instance.myPlayerParameter.stamina > Convert.ToInt32((PlayerParameters._instance.myPlayerParameter.stamina * 14.3) / 100)) {
			PlayerParameters._instance.myPlayerParameter.stamina -= Convert.ToInt32((PlayerParameters._instance.myPlayerParameter.stamina * 14.3) / 100);
			PlayerParameters._instance.myPlayerParameter.orb += 1;
			Dictionary<string, string> avatarParameters = new Dictionary<string, string> ();
			avatarParameters.Add ("stamina", PlayerParameters._instance.myPlayerParameter.stamina.ToString ());
			avatarParameters.Add ("orb", PlayerParameters._instance.myPlayerParameter.orb.ToString ());
			StartCoroutine (PlayerParameters._instance.SendPlayerParameters (avatarParameters, null));
		} else if (PlayerParameters._instance.myPlayerParameter.membership_no==30 && PlayerParameters._instance.myPlayerParameter.orb < PlayerParameters._instance.myPlayerParameter.maxOrb && PlayerParameters._instance.myPlayerParameter.stamina > Convert.ToInt32((PlayerParameters._instance.myPlayerParameter.stamina * 16.6) / 100)) {
			PlayerParameters._instance.myPlayerParameter.stamina -= Convert.ToInt32((PlayerParameters._instance.myPlayerParameter.stamina * 16.6) / 100);
			PlayerParameters._instance.myPlayerParameter.orb += 1;
			Dictionary<string, string> avatarParameters = new Dictionary<string, string> ();
			avatarParameters.Add ("stamina", PlayerParameters._instance.myPlayerParameter.stamina.ToString ());
			avatarParameters.Add ("orb", PlayerParameters._instance.myPlayerParameter.orb.ToString ());
			StartCoroutine (PlayerParameters._instance.SendPlayerParameters (avatarParameters, null));
		} else {
		}
	}
	public void Swap_Orb()
	{
		if (PlayerParameters._instance.myPlayerParameter.membership_no==7 && PlayerParameters._instance.myPlayerParameter.orb > 0 && PlayerParameters._instance.myPlayerParameter.stamina < PlayerParameters._instance.myPlayerParameter.max_stamina) {
			int temp_stamina = PlayerParameters._instance.myPlayerParameter.stamina;
			temp_stamina += Convert.ToInt32((PlayerParameters._instance.myPlayerParameter.stamina * 12.5) / 100);
			if (temp_stamina > PlayerParameters._instance.myPlayerParameter.max_stamina) {
				PlayerParameters._instance.myPlayerParameter.stamina = PlayerParameters._instance.myPlayerParameter.max_stamina;
			} else {
				PlayerParameters._instance.myPlayerParameter.stamina = temp_stamina;
			}
			PlayerParameters._instance.myPlayerParameter.orb -= 1;
			Dictionary<string, string> avatarParameters = new Dictionary<string, string> ();
			avatarParameters.Add ("stamina", PlayerParameters._instance.myPlayerParameter.stamina.ToString ());
			avatarParameters.Add ("orb", PlayerParameters._instance.myPlayerParameter.orb.ToString ());
			StartCoroutine (PlayerParameters._instance.SendPlayerParameters (avatarParameters, null));
			Debug.Log ("Successfully swap");
		} else if (PlayerParameters._instance.myPlayerParameter.membership_no==14 && PlayerParameters._instance.myPlayerParameter.orb > 0 && PlayerParameters._instance.myPlayerParameter.stamina < PlayerParameters._instance.myPlayerParameter.max_stamina) {
			int temp_stamina = PlayerParameters._instance.myPlayerParameter.stamina;
			temp_stamina += Convert.ToInt32((PlayerParameters._instance.myPlayerParameter.stamina * 14.3) / 100);
			if (temp_stamina > PlayerParameters._instance.myPlayerParameter.max_stamina) {
				PlayerParameters._instance.myPlayerParameter.stamina = PlayerParameters._instance.myPlayerParameter.max_stamina;
			} else {
				PlayerParameters._instance.myPlayerParameter.stamina = temp_stamina;
			}
			PlayerParameters._instance.myPlayerParameter.orb -= 1;
			Dictionary<string, string> avatarParameters = new Dictionary<string, string> ();
			avatarParameters.Add ("stamina", PlayerParameters._instance.myPlayerParameter.stamina.ToString ());
			avatarParameters.Add ("orb", PlayerParameters._instance.myPlayerParameter.orb.ToString ());
			StartCoroutine (PlayerParameters._instance.SendPlayerParameters (avatarParameters, null));
		} else if (PlayerParameters._instance.myPlayerParameter.membership_no==30 && PlayerParameters._instance.myPlayerParameter.orb > 0 && PlayerParameters._instance.myPlayerParameter.stamina < PlayerParameters._instance.myPlayerParameter.max_stamina) {
			int temp_stamina = PlayerParameters._instance.myPlayerParameter.stamina;
			temp_stamina += Convert.ToInt32((PlayerParameters._instance.myPlayerParameter.stamina * 16.6) / 100);
			if (temp_stamina > PlayerParameters._instance.myPlayerParameter.max_stamina) {
				PlayerParameters._instance.myPlayerParameter.stamina = PlayerParameters._instance.myPlayerParameter.max_stamina;
			} else {
				PlayerParameters._instance.myPlayerParameter.stamina = temp_stamina;
			}
			PlayerParameters._instance.myPlayerParameter.orb -= 1;
			Dictionary<string, string> avatarParameters = new Dictionary<string, string> ();
			avatarParameters.Add ("stamina", PlayerParameters._instance.myPlayerParameter.stamina.ToString ());
			avatarParameters.Add ("orb", PlayerParameters._instance.myPlayerParameter.orb.ToString ());
			StartCoroutine (PlayerParameters._instance.SendPlayerParameters (avatarParameters, null));
		} else {
		}
	}
	// Shivam end

}
