using UnityEngine;
using System.Collections;
using MiniJSON;
using UnityEngine.UI;
public class Settings : MonoBehaviour {
	public GameObject loader;
	public Popup popupFromServer;
	public Slider bgmSlider;

	public GameObject OnButton;
	public GameObject OffButton;
	
	public GameObject OnNotification;
	public GameObject OffNotificaation;

	public bool sound=true;
	public bool notificationSound=true;


	// Use this for initialization
	void OnEnable () {
		bgmSlider.value = loadingScene.Instance.sliderValue;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//	public void notificationOnOff()
//	{
//		loadingScene.Instance.notificationOnOff();
//	}
//	
//	
//	public void soundOnOff()
//	{
//		
//		loadingScene.Instance.soundOnOff ();
//
//	}

	public void notificationOnOff()
	{
		if(notificationSound==false)
		{
			
			
		
				if(OnNotification != null)
				{
					OnNotification.GetComponent<Button>().interactable=false;
					OnNotification.gameObject.transform.GetChild(1).GetComponent<Text>().color=new Color32(255,255,255,255);
				}
		
				if(OffNotificaation != null)
				{
					OffNotificaation.GetComponent<Button>().interactable=true;
					OffNotificaation.gameObject.transform.GetChild(1).GetComponent<Text>().color=new Color32(0,0,0,255);
				}
				

			
			notificationSound=true;
			
		}
		else
		{
			
			

				if(OnNotification != null)
				{
					OnNotification.GetComponent<Button>().interactable=true;
					OnNotification.gameObject.transform.GetChild(1).GetComponent<Text>().color=new Color32(0,0,0,255);
				}

				if(OffNotificaation != null)
				{
					OffNotificaation.GetComponent<Button>().interactable=false;
					OffNotificaation.gameObject.transform.GetChild(1).GetComponent<Text>().color=new Color32(255,255,255,255);
				}
				

			notificationSound=false;
		}
		
	}
	
	
	
	public void soundOnOff()
	{
		print("=========== SOUND =========="+sound);
		
		if(sound==false)
		{
			AudioListener.pause=false;
			print("=======Q==== SOUND =========="+sound);

				if(OnButton != null)
				{
					OnButton.GetComponent<Button>().interactable=false;
					OnButton.gameObject.transform.GetChild(1).GetComponent<Text>().color=new Color32(255,255,255,255);
				}
				

				if(OffButton != null)
				{
					OffButton.GetComponent<Button>().interactable=true;
					OffButton.gameObject.transform.GetChild(1).GetComponent<Text>().color=new Color32(0,0,0,255);
				}
				

			
			
			sound=true;
			
		}
		else
		{
			AudioListener.pause=true;
			
			
			print("==========WEEWWEE= SOUND =========="+sound);
			
		
				if(OnButton != null)
				{
					OnButton.GetComponent<Button>().interactable=true;
					OnButton.gameObject.transform.GetChild(1).GetComponent<Text>().color=new Color32(0,0,0,255);
				}

				if(OffButton != null)
				{
					OffButton.GetComponent<Button>().interactable=false;
					OffButton.gameObject.transform.GetChild(1).GetComponent<Text>().color=new Color32(255,255,255,255);
				}

			sound=false;
		}
		
	}



	public void logOut()
	{
		Debug.Log ("Logout...!!");
		loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected)
				StartCoroutine (LogoutApi ());
			else
			{
				popupFromServer.ShowPopup ("Network Error!");
				#if UNITY_EDITOR
				PlayerPrefs.DeleteKey ("PlayerEmail");
				PlayerPrefs.DeleteKey ("PlayerPassword");
				PlayerPrefs.SetString("logout","yes");
				FacebookController._instance.Logout ();
				PlayerDataParse._instance.playersParam.userIdNo =  0;
				PlayerDataParse._instance.playersParam.userName =  "";
				PlayerDataParse._instance.playersParam.fblogin =  0;
				PlayerDataParse._instance.playersParam.referralCode = "";
				PlayerDataParse._instance.SaveData ();

//				PlayerDataParse._instance.modifyXml ("fbLogin" , "0");
//				PlayerDataParse._instance.modifyXml ("userId" , "0");
//				PlayerDataParse._instance.modifyXml ("userName" , "0");
//				PlayerDataParse._instance.modifyXml ("referralCode" , "0");
				onClickSettingExit();
				newMenuScene.instance.LoginScene.SetActive(true);
//				newMenuScene.instance.mainSound.time = 1;
				loadingScene.Instance.main();
				#endif
			}
		});
		
		
		
	}

	
	IEnumerator LogoutApi()
	{
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag" ,"doUserLogout");
		Debug.Log ("PlayerParameters._instance.myPlayerParameter.user_id  = "+PlayerDataParse._instance.playersParam.userId);
		Debug.Log ("SystemInfo.deviceUniqueIdentifier   = "+SystemInfo.deviceUniqueIdentifier );
		wwwForm.AddField ("user_id" , PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id" , SystemInfo.deviceUniqueIdentifier );
		WWW wwwLogout = new WWW(loadingScene.Instance.baseUrl , wwwForm);
		yield return wwwLogout;
		if (wwwLogout.error == null) {
			Debug.Log(wwwLogout.text);
			IDictionary resultDict = (IDictionary)Json.Deserialize (wwwLogout.text);
			//{"success":0,"error_msg":"User does't exist!"}
			//{"success":0,"error_msg":"Invalid login details!"}
			
			if(wwwLogout.text.Contains ("error_msg"))
			{
				popupFromServer.ShowPopup (resultDict["error_msg"].ToString ());
			}
			else
			{
				loader.SetActive (false);
				//{"success":1,"msg":"User logout successfully"}
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
				newMenuScene.instance.LoginScene.SetActive(true);
//				newMenuScene.instance.mainSound.time = 1;
				loadingScene.Instance.main();
				if(Facebook.Unity.FB.IsLoggedIn)
					FacebookController._instance.Logout ();
				PlayerParameters._instance.myPlayerParameter.avatar_no = 0;
				loadingScene.Instance.myBattleFormation.RemoveAllCardsFromDecks ();
				loadingScene.Instance.myQuestFormation.RemoveAllCardsFromDecks ();
				loadingScene.Instance.eventQuestFormation.RemoveAllCardsFromDecks ();
				if (chestScript._instance != null)
					chestScript._instance.ResetValues ();
				CardsManager._instance.mycards.Clear ();
				CardsManager._instance.myCaptives.Clear ();
				loadingScene.Instance.randomCards.Clear ();
				PlayerPrefs.DeleteKey ("tempButtonStorage");
				PlayerPrefs.DeleteKey ("tempButton");
				PlayerPrefs.DeleteKey ("chosenCardCastle");
				PlayerPrefs.DeleteKey ("updating");
				PlayerPrefs.DeleteKey ("chosenCardStorage");
				PlayerPrefs.DeleteKey ("updatingStorage");
				PlayerPrefs.DeleteKey ("tempButtonStorageSecondary");
				PlayerPrefs.DeleteKey ("updatingStorageSecondary");
				//--------------- Barn --------------
				PlayerPrefs.DeleteKey ("updatingBarnSecondary");
				PlayerPrefs.DeleteKey ("updatingBarn");
				PlayerPrefs.DeleteKey ("chosenCardBarn");
				PlayerPrefs.DeleteKey ("tempButtonBarn");

				PlayerPrefs.DeleteKey ("tempButtonGoldMiine");
				PlayerPrefs.DeleteKey ("updatingGoldMine");
				PlayerPrefs.DeleteKey ("chosenCardGoldMine");

				loadingScene.Instance.isCastlePrimary = false;
				loadingScene.Instance.isStoragePrimary = false;
				loadingScene.Instance.isStorageSecondary = false;
				loadingScene.Instance.isBarnPrimary = false;
				loadingScene.Instance.isBarnSecondary = false;
				loadingScene.Instance.isgoldMinePrimary = false;
				loadingScene.Instance.isgoldMineSecondary = false;
				loadingScene.Instance.castlePrimaryClockText = 0;
				loadingScene.Instance.storagePrimaryClockText = 0;
				loadingScene.Instance.storageSecondaryText = 0;
				loadingScene.Instance.barnPrimaryClockText = 0;
				loadingScene.Instance.barnSecondaryText = 0;
				loadingScene.Instance.goldMinePrimaryClockText = 0;
				loadingScene.Instance.goldMineSecondaryText = 0;
				loadingScene.Instance.readyTogo = false;

				EmpireManager._instance.castle.currentLevel = 0;
				EmpireManager._instance.castle.primaryCardNo = -1;
				EmpireManager._instance.castle.secondaryCardNo = -1;

				EmpireManager._instance.storage.currentLevel = 0;
				EmpireManager._instance.storage.primaryCardNo = -1;
				EmpireManager._instance.storage.secondaryCardNo = -1;

				EmpireManager._instance.barn.currentLevel = 0;
				EmpireManager._instance.barn.primaryCardNo = -1;
				EmpireManager._instance.barn.secondaryCardNo = -1;

				EmpireManager._instance.goldMine.currentLevel = 0;
				EmpireManager._instance.goldMine.primaryCardNo = -1;
				EmpireManager._instance.goldMine.secondaryCardNo = -1;
			}
			
		} else {
			popupFromServer.ShowPopup ("Network Error!");
		}
		
	}

//	public void onClickSetting()
//	{
//		for(int i=0;i<loadingScene.Instance.bgmSliders.Length;i++)
//		{
//			loadingScene.Instance.bgmSliders[i].value=loadingScene.Instance.sliderValue;
//		}
//		gameObject.SetActive(true);
//		
//	}

	public void onClickSettingExit()
	{
		gameObject.SetActive(false);
		
	}


	public void bgmSliderChange(Slider sliderValue)
	{
		loadingScene.Instance.sliderValue = sliderValue.value;
		for(int i=0;i<loadingScene.Instance.allSounds.Length;i++)
		{
			if(loadingScene.Instance.allSounds[i] != null)
			{
				loadingScene.Instance.allSounds[i].volume = sliderValue.value;
			}
			//loadingScene.Instance.allSounds[i].volume=loadingScene.Instance.bgmSliders[i].value;
		}
	}

	public void sfxSliderChange(Slider sliderValue)
	{
		loadingScene.Instance.sliderValueSfx = sliderValue.value;
//		for(int i=0;i<loadingScene.Instance.allSounds.Length;i++)
//		{
//			if(loadingScene.Instance.allSounds[i] != null)
//			{
//				loadingScene.Instance.allSounds[i].volume = sliderValue.value;
//			}
//		}
	}
}
