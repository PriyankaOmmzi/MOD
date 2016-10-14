using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MiniJSON;
using System.Collections.Generic;

public class battleLayout : MonoBehaviour
{
	public static int itemToSend;
	bool isMneuActive=false;
	Button clickActButton;

	public GameObject setting;

	public Button[] bottomsButtons;
	public GameObject chatBtn;
	public Image itemSelectedImage;
	public Text itemSelectedName;
	public Text itemSelectedDescription;
	public Text itemsOwned;
	public GameObject menuScreen;
	public GameObject[] actItems;
	public GameObject selection;
	public Button[] clickAct;

	public Text timerBar;
	public List<Image> attackingOrbs = new List<Image>();
	public Sprite activatedOrb , deactivedOrb;

	public BattleOpponentSelection battleOpponentSelection;

	// Use this for initialization
	void Start () 
	{
		deactivateButtons(0);
		menuScreen.SetActive (false);
		selection.SetActive(false);
		actItems[0].SetActive(true);
		loadingScene.Instance.playerProfilePanel.SetActive(false);
		setting.SetActive(false);
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

	public void logOut()
	{
		Start();
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
	public void chatButton()
	{
		Start();
		if(PlayerPrefs.GetString("chat")=="off")
		{
			chatBtn.GetComponent<DragHandeler>().enabled=true;
			chatBtn.GetComponent<CanvasGroup>().blocksRaycasts=true;
			
			chatBtn.GetComponent<Button>().interactable=true;
			chatBtn.GetComponentInChildren<Text>().enabled=true;
			PlayerPrefs.SetString("chat","on");


		}
		
	}
	public void cardCollections()
	{
		Start();

		PlayerPrefs.SetString("battleLyout","yes");
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
		loadingScene.Instance.cardCollecton ();
	}

	public void chatClick()
	{
		Start();

		PlayerPrefs.SetString("battleLyout","yes");
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

	public void shopScene()
	{
		Start();
		PlayerPrefs.SetString("battleLyout","yes");
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
	}
	public void inventory()
	{
		Start();

		PlayerPrefs.SetString("battleLyout","yes");
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
		loadingScene.Instance.inventory ();
	}
	public void questButton()
	{
		Start();

		PlayerPrefs.SetString("battleLyout","yes");
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

	}

	public void empire()
	{
		Start();

		PlayerPrefs.SetString("battleLyout","yes");
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

	public void backButton()
	{
		Start();
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

	// Update is called once per frame
	void Update ()
	{
		timerBar.text = newMenuScene.instance.orbsText.text;
	}

	public void community()
	{
		Start();

		PlayerPrefs.SetString("battleLyout","yes");
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
		
	}
	public void trade()
	{
		Start();

		PlayerPrefs.SetString("battleLyout","yes");
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
		loadingScene.Instance.trade ();
	}

	public void clickItem(Button clickObjects)
	{
		int.TryParse(clickObjects.name , out itemToSend);
		selection.SetActive(true);
		itemSelectedImage.sprite=clickObjects.transform.GetChild(0).GetComponent<Image>().sprite;
		itemSelectedName.text = Artefacts._instance.gameArtifacts [itemToSend - 1].name;
		itemSelectedDescription.text = Artefacts._instance.gameArtifacts [itemToSend - 1].descriptiion;
		itemsOwned.text = "Owned : "+PlayerParameters._instance.myPlayerParameter.artefacts[itemToSend-1];
	}

	public void exit()
	{
		selection.SetActive(false);
	}

	public void doneButton(bool itemSelected )
	{
		if (!itemSelected)
			itemToSend = 0;
		newMenuScene.instance.loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected) {
				StartCoroutine (FetchPlayers (isSuccess => {
					if (isSuccess) {
						newMenuScene.instance.loader.SetActive (false);
						loadingScene.Instance.BattleOpponentSelection ();
						selection.SetActive(false);
						menuScreen.SetActive(false);
						isMneuActive=false;
					} else
					{
						loadingScene.Instance.popupFromServer.ShowPopup ("No Opponent Matches your Search!");
					}
				}));
			} else {
				loadingScene.Instance.popupFromServer.ShowPopup ("Network Error!");
			}
		});


	}


	IEnumerator FetchPlayers(System.Action<bool> callback)
	{
		WWWForm playerForm = new WWWForm ();
		playerForm.AddField ("tag" ,"getTenItemSetArray");
		playerForm.AddField ("user_id" , PlayerDataParse._instance.playersParam.userId);
		playerForm.AddField ("device_id" , SystemInfo.deviceUniqueIdentifier );
		if (itemToSend > 0) {
			Debug.Log ("item to send = " + itemToSend);
			playerForm.AddField ("item_set", itemToSend);
		} else {
			playerForm.AddField ("item_set", "");
		}
		WWW wwwFetchOpponents = new WWW(loadingScene.Instance.baseUrl , playerForm);
		yield return wwwFetchOpponents;
		//{"success":0,"error_msg":"Data does not exist!"}
		if (wwwFetchOpponents.error == null) {
			Debug.Log (wwwFetchOpponents.text);
			IDictionary resultDict = (IDictionary)Json.Deserialize (wwwFetchOpponents.text);
			if (wwwFetchOpponents.text.Contains ("error_msg")) {
				callback (false);
			} else {
				battleOpponentSelection.listOfOpponentDetails.Clear ();
				IList opponentList = (IList)resultDict["Players"];
				for (int i = 0; i < opponentList.Count; i++) {
					IDictionary oppDict = (IDictionary)opponentList[i];
					OpponentPrefabDetails oppDetails = new OpponentPrefabDetails ();
//					Debug.Log (oppDict ["username"].ToString ());
					if(!string.IsNullOrEmpty(oppDict ["username"].ToString ()))
						oppDetails.playerName = oppDict ["username"].ToString ();

					int.TryParse(oppDict ["avatar_no"].ToString () , out oppDetails.avatarNo);
					int.TryParse(oppDict ["avatar_level"].ToString () , out oppDetails.playerLevel);
					oppDetails.playerRank = oppDict ["rank"].ToString ();
					int.TryParse(oppDict ["avatar_attack"].ToString () , out oppDetails.attack);
					int.TryParse(oppDict ["avatar_defense"].ToString () , out oppDetails.defense);
					int.TryParse(oppDict ["avatar_leadership"].ToString () , out oppDetails.leadership);
					int.TryParse(oppDict ["user_id"].ToString () , out oppDetails.playerId);

					battleOpponentSelection.listOfOpponentDetails.Add (oppDetails);
				}
				//{"success":1,"Players":[{"item_set":"3,5,7","avatar_level":"0","rank":"",
				//"avatar_defense":"20","avatar_attack":"20","avatar_leadership":"30","avatar_no":"3"
				//,"user_id":"90","username":null},{"item_set":"3,4,5,0","avatar_level":"0","rank":"",
				//"avatar_defense":"","avatar_attack":"","avatar_leadership":"","avatar_no":"","user_id":"79","username":null},
				//{"item_set":"3,4,5,0","avatar_level":"0","rank":"","avatar_defense":"","avatar_attack":"","avatar_leadership":"","avatar_no":"","user_id":"80","username":null}]}

				callback (true);
			}
		}
	}


	public void clickACT(Button buttonObject)
	{
		clickActButton=buttonObject;
		if(buttonObject.name =="ACT1")
		{
			deactivateButtons(0);
		}
		else if(buttonObject.name=="ACT2")
		{
			deactivateButtons(1);
		}
		else if(buttonObject.name=="ACT3")
		{
			deactivateButtons(2);
		}
		else if(buttonObject.name=="ACT4")
		{
			deactivateButtons(3);
		}
		else if(buttonObject.name=="ACT5")
		{
			deactivateButtons(4);
		}
	}
	void deactivateButtons(int index)
	{
		for(int i=0;i<actItems.Length;i++)
		{
			if(i==index)
			{
				clickAct[i].interactable=false;
				actItems[i].SetActive(true);
			}
			else
			{
				clickAct[i].interactable=true;
				actItems[i].SetActive(false);
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

	void OnEnable()
	{
		AttackingOrbDisplay ();
		ResetMenu ();
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
		Start();
		ResetMenu ();
		loadingScene.Instance.objectToOpenMenu = this.gameObject;
		loadingScene.Instance.main ();
	}

}
