using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using MiniJSON;

[System.Serializable]
public class OpponentPrefabDetails
{
	public string playerName;
	public int avatarNo;
	public int textAgainstImage2;
	public int playerLevel;
	public string playerRank;
	public int attack;
	public int defense;
	public int leadership;
	public int playerId;
}

public class BattleOpponentSelection : MonoBehaviour
{
	public GameObject setting;
	bool isMneuActive=false;
	public Button[] bottomsButtons;
	public GameObject chatBtn;
	public GameObject menuScreen;
	public List<Image> attackingOrbs = new List<Image>();
	public Sprite activatedOrb , deactivedOrb;
	public Text orbTimer;
	public GameObject containerOfOpponents;

	public List<OpponentPrefab> listOfOpponents  = new List<OpponentPrefab>();
	public List<OpponentPrefabDetails> listOfOpponentDetails = new List<OpponentPrefabDetails>();

	public static BattleOpponentSelection _instance;
	// Use this for initialization
	void Start () 
	{
		_instance = this;

	}

	// Update is called once per frame
	void Update ()
	{
		orbTimer.text = newMenuScene.instance.orbsText.text;
	}

	public void FetchOpponentDetails(int userID)
	{
		
		loadingScene.Instance.loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected)
			{
				StartCoroutine(GetOpponentDetail(userID , (isSuccess , msgString) => {
					if(isSuccess)
					{
						StartCoroutine(StartBattle());
						//GO TO BATTLE
					}
					else
					{
						loadingScene.Instance.popupFromServer.ShowPopup (msgString);
					}
				}));
			} 
			else 
			{
				loadingScene.Instance.popupFromServer.ShowPopup ("Network Error!");
			}

		});
	}

	IEnumerator StartBattle()
	{
		BattleLogic._instance.EnemyResourcesLoot (OpponentData._instance.myPlayerParameter.avatar_level , OpponentData._instance.myPlayerParameter.wheat , OpponentData._instance.myPlayerParameter.gold);
		BattleLogic._instance.PlayerResourcesLoot (OpponentData._instance.myPlayerParameter.avatar_level , PlayerParameters._instance.myPlayerParameter.wheat , PlayerParameters._instance.myPlayerParameter.gold);
		BattleLogic._instance.SetPlayerCardForBattle(loadingScene.Instance.myBattleFormation);
		yield return new WaitForSeconds(1.0f);
		loadingScene.Instance.loader.SetActive (false);
		loadingScene.Instance.startBattle();
	}

	IEnumerator GetOpponentDetail(int userID , System.Action <bool , string> callback)
	{
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag" ,"doGetRequiredPlayerPlayerCardBuildingData");
		wwwForm.AddField ("user_id" , PlayerDataParse._instance.ID(userID.ToString()));
		wwwForm.AddField ("device_id" , SystemInfo.deviceUniqueIdentifier );
		WWW wwwFetchOpponentDetails = new WWW(loadingScene.Instance.baseUrl , wwwForm);
		yield return wwwFetchOpponentDetails;
		Debug.Log ("oopennet => "+wwwFetchOpponentDetails.text);
		// {"success":0,"error_msg":"Data does not exist!"}
		if (wwwFetchOpponentDetails.error == null) {
			IDictionary resultDict = (IDictionary)Json.Deserialize (wwwFetchOpponentDetails.text);
			if(wwwFetchOpponentDetails.text.Contains ("error_msg"))
			{
				
				callback(false , resultDict["error_msg"].ToString());
			}
			else
			{
				OpponentData._instance.playerID = userID;
				OpponentData._instance.GetOpponentsData (resultDict , isSuccess =>{
					if(isSuccess)
					{
						
						callback(true , "eyeyye");
					}
					else
					{
						callback(false, "Could not fetch data!");
					}
				});
					
			}

		} else {
			callback(false, "Network Error!");
		}
	}

	void AttackingOrbDisplay()
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
	public void logOut()
	{
		onClickSettingExit();
		PlayerPrefs.SetString("logout","yes");

		loadingScene.Instance.main();
	}
	public void notificationOnOff()
	{
		loadingScene.Instance.notificationOnOff();
	}
	public void soundOnOff()
	{
		
		loadingScene.Instance.soundOnOff ();
	}
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

	public void onClickProfile()
	{
		loadingScene.Instance.playerProfilePanel.SetActive(true);
	}
	public void onClickProfileExit()
	{
		loadingScene.Instance.playerProfilePanel.SetActive(false);
	}

	public void cardCollections()
	{
		PlayerPrefs.SetString("layout3","yes");
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
		loadingScene.Instance.BattleFormation ();
	}

	public void empire()
	{
		PlayerPrefs.SetString("layout3","yes");
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
	}

	public void shopScene()
	{
		PlayerPrefs.SetString("layout3","yes");
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
		loadingScene.Instance.shop ();
	}
	public void inventory()
	{
		PlayerPrefs.SetString("layout3","yes");
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
		loadingScene.Instance.inventory ();
	}
	public void chatButton()
	{
		if(PlayerPrefs.GetString("chat")=="off")
		{
			chatBtn.GetComponent<DragHandeler>().enabled=true;
			chatBtn.GetComponent<CanvasGroup>().blocksRaycasts=true;
			chatBtn.GetComponent<Button>().interactable=true;

			chatBtn.GetComponentInChildren<Text>().enabled=true;
			PlayerPrefs.SetString("chat","on");
		}
		
	}
	public void chatClick()
	{
		PlayerPrefs.SetString("battle_Layout3","yes");
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
		loadingScene.Instance.chat ();
	}

	public void questButton()
	{
		PlayerPrefs.SetString("battle_Layout3","yes");
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
		loadingScene.Instance.quest ();
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
		
	}

	public void backButon()
	{

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
	public void trade()
	{
		PlayerPrefs.SetString("battle_Layout3","yes");
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
		loadingScene.Instance.trade ();
	}
	public void nextScene()
	{
		loadingScene.Instance.startBattle();
//		loadingScene.Instance.BattleFormation();
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
		PlayerPrefs.SetString("fromBattle","yes");
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

	public static bool wentFromBattleResult;
	void OnEnable()
	{
		ResetMenu ();
		setting.SetActive(false);
		menuScreen.SetActive (false);
		menuScreen.SetActive (false);
		loadingScene.Instance.playerProfilePanel.SetActive(false);
		if (!wentFromBattleResult) {
			for (int i = listOfOpponents.Count - 1; i >= 0; i--) {
				Destroy (listOfOpponents [i].gameObject);
				listOfOpponents.RemoveAt (i);
			}
			for (int i = 0; i < listOfOpponentDetails.Count; i++) {
				GameObject opponentObj = (GameObject)Instantiate (Resources.Load ("Opponent"));
				opponentObj.transform.SetParent (containerOfOpponents.transform);
				opponentObj.transform.localScale = Vector3.one;
				OpponentPrefab oppScript = opponentObj.GetComponent<OpponentPrefab> ();
				oppScript.playerName.text = listOfOpponentDetails [i].playerName;
				oppScript.playerLevel.text = "Lvl :" + (listOfOpponentDetails [i].playerLevel+1);
				oppScript.playerRank.text = listOfOpponentDetails [i].playerRank;
				oppScript.attack.text = listOfOpponentDetails [i].attack.ToString ();
				oppScript.leadership.text = listOfOpponentDetails [i].leadership.ToString ();
				oppScript.defense.text = listOfOpponentDetails [i].defense.ToString ();
				oppScript.playerId = listOfOpponentDetails [i].playerId;
				oppScript.idInList = i;
				if (listOfOpponentDetails [i].avatarNo > 0)
					oppScript.playerAvatar.sprite = loadingScene.Instance.playerSprite [listOfOpponentDetails [i].avatarNo - 1];
				oppScript.textAgainstImage2.text = BattleLogic._instance.AttackingOrbsUsed (listOfOpponentDetails [i].playerLevel).ToString ();
				listOfOpponents.Add (oppScript);

			}
		}
		wentFromBattleResult = false;
		AttackingOrbDisplay ();
	}
	
	void ResetMenu()
	{
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
	}

	public void RootMenuButton()
	{
		ResetMenu ();
		loadingScene.Instance.objectToOpenMenu = this.gameObject;
		loadingScene.Instance.main ();
	}
	public void community()
	{
		PlayerPrefs.SetString("battle_Layout3","yes");
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
		loadingScene.Instance.community ();

		
	}
	public void battle()
	{
		PlayerPrefs.SetString("battle_Layout3","yes");
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
		loadingScene.Instance.battleScene ();
		
	}

}
