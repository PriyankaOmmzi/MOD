using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class chat1 : MonoBehaviour
{
	public static chat1 instance;
	public GameObject menuObj;
	public GameObject menu;
	bool isMneuActive=false;
	public Button[] bottomsButtons;
	public GameObject setting;
	public GameObject chatBtn;

	public GameObject [] content;
	public Button [] contentButton;

	public GameObject newMessage;
	void Awake()
	{
		instance = this;
	}
	// Use this for initialization
	void Start () {
		menuObj.SetActive(false);
		menu.SetActive(false);
		//chatObj.SetActive(false);
		setting.SetActive(false);
		loadingScene.Instance.playerProfilePanel.SetActive(false);
		deactivateContent(0);
		newMessage.SetActive(false);

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
			//loadingScene.Instance.allSounds[i].volume=loadingScene.Instance.bgmSliders[i].value;
		}
	}

	public void NewMessage()
	{
		newMessage.SetActive(true);
	}

	public void ExitNewMessage()
	{
		newMessage.SetActive(false);
	}
	void deactivateContent(int index)
	{
		
		for(int i=0;i<content.Length;i++)
		{
			if(i==index)
			{
				//				buttonTabs[i].GetComponent<Image>().color=new Color32(255,255,255,255);
				//				buttonTabs[i].GetComponent<Image>().color=new Color32(255,255,255,255);
				contentButton[i].interactable=false;
				content[i].SetActive(true);
				
				//buttonTabs[i].GetComponent<Outline>().effectColor=new Color32(241,255,0,255);
			}
			else
			{
				//				buttonTabs[i].GetComponent<Image>().color=new Color32(135,106,106,255);
				contentButton[i].interactable=true;

				content[i].SetActive(false);

				//buttonTabs[i].GetComponent<Outline>().effectColor=new Color32(0,0,0,255);
			}
		}
	}

	public void buttonPress (Button name)
	{
		if(name.name=="MessageButton")
		{
			deactivateContent(0);
			newMessage.SetActive(false);

		}
		else if(name.name=="FriendsButton")		
		{
			deactivateContent(1);
			newMessage.SetActive(false);


		}
		else if(name.name=="GuildButton")		
		{
			deactivateContent(2);
			newMessage.SetActive(false);


		}
		else if(name.name=="AllButton")		
		{
			deactivateContent(3);
			newMessage.SetActive(false);


		}

	}


	public void logOut()
	{
		Start();

		onClickSettingExit();
		PlayerPrefs.SetString("logout","yes");
		loadingScene.Instance.main();
		//LoginScene.SetActive(true);
	}

	public void notificationOnOff()
	{
		loadingScene.Instance.notificationOnOff();
	}
	public void soundOnOff()
	{
		
		loadingScene.Instance.soundOnOff ();
	}
	public void chat()
	{
		menuObj.SetActive(true);
		//chatObj.SetActive(true);
		//Application.LoadLevel("chat2");
	}


	public void backFromChat()
	{
		menuObj.SetActive(false);
		//chatObj.SetActive(false);
		
	}
	// Update is called once per frame
	void Update () {
	
	}

	public void quest()
	{
		Start();

		PlayerPrefs.SetString("chatScene","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			print("yes this work");
			menu.SetActive(false);
			isMneuActive=false;
			
		}
		else
		{
			
		}
		loadingScene.Instance.quest ();
		//Application.LoadLevel("quest");
		
	}
	public void cardCollections()
	{
		Start();

		PlayerPrefs.SetString("chatScene","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			print("yes this work");
			menu.SetActive(false);
			isMneuActive=false;
			
		}
		else
		{
			
		}
		loadingScene.Instance.BattleFormation();

		//Application.LoadLevel("Battle_Layout4");
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
		menu.SetActive(false);
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
			menu.SetActive(false);
			isMneuActive=false;
		}
	}

	public void RootMenuButton()
	{
		Start();

		ResetMenu ();
		loadingScene.Instance.main ();
		//Application.LoadLevel("menuNew");
	}
	public void invecntory()
	{
		Start();

		PlayerPrefs.SetString("chatScene","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			print("yes this work");
			menu.SetActive(false);
			isMneuActive=false;
			
		}
		else
		{
			
		}
		loadingScene.Instance.inventory();

		//Application.LoadLevel("inventoryScene");		
	}
	public void shop()
	{
		Start();

		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			print("yes this work");
			menu.SetActive(false);
			isMneuActive=false;
			
		}
		else
		{
			
		}
		loadingScene.Instance.shop ();
		PlayerPrefs.SetString("chatScene","yes");
		//Application.LoadLevel("shopScene");
	}
	
	public void empire()
	{
		Start();

		PlayerPrefs.SetString("chatScene","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			print("yes this work");
			menu.SetActive(false);
			isMneuActive=false;
			
		}
		else
		{
			
		}
		loadingScene.Instance.empire ();
		//Application.LoadLevel("empireScene");
	}
	public void menuPopUp()
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
			menu.SetActive(true);
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
			menu.SetActive(false);
			isMneuActive=false;
			
		}

		
	}
	public void community()
	{
		Start();

		PlayerPrefs.SetString("chatScene","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			print("yes this work");
			menu.SetActive(false);
			isMneuActive=false;
			
		}
		else
		{
			
		}
		loadingScene.Instance.community();

	//	Application.LoadLevel("community");
		
	}
	public void exitMenu()
	{
		menu.SetActive(false);
	}
	public void backButton()
	{
		CancelInvoke ("FetchChat");
//			if(PlayerPrefs.GetString("layout3")=="yes")
//			{
//
//				//Application.LoadLevel("battle_Layout3");
//				loadingScene.Instance.BattleOpponentSelection();
//				PlayerPrefs.SetString("layout3","no");
//				
//			}
//			if(PlayerPrefs.GetString("inventoryScene")=="yes")
//			{
//				//Application.LoadLevel("inventoryScene");
//				loadingScene.Instance.inventory();
//
//				PlayerPrefs.SetString("inventoryScene","no");
//			}
//			if(	PlayerPrefs.GetString("battle_Layout3")=="yes")
//			{
//				//Application.LoadLevel("battle_Layout3");
//				loadingScene.Instance.BattleOpponentSelection();
//
//				PlayerPrefs.SetString("battle_Layout3","no");
//			}
//			
//			if(PlayerPrefs.GetString("battleLyout")=="yes")
//			{
//				//Application.LoadLevel("Battle_Layout");
//				loadingScene.Instance.battleScene();
//
//				PlayerPrefs.SetString("battleLyout","no");
//				
//			}
//			
//			if(PlayerPrefs.GetString("battle_Layout4")=="yes")
//			{
//			//	Application.LoadLevel("Battle_Layout4");
//				loadingScene.Instance.BattleFormation();
//				PlayerPrefs.SetString("battle_Layout4","no");
//				
//			}
//			if(PlayerPrefs.GetString("newMain")=="yes")
//			{
//			loadingScene.Instance.main ();
//				//Application.LoadLevel("menuNew");
//				PlayerPrefs.SetString("newMain","no");
//				
//			}
//			if(PlayerPrefs.GetString("cardCollection")=="yes")
//			{
//				//Application.LoadLevel("cardCollections");
//			  	loadingScene.Instance.cardCollecton();
//				PlayerPrefs.SetString("cardCollection","no");
//				
//			}
//			if(PlayerPrefs.GetString("lost")=="yes") 
//			{
//				//Application.LoadLevel("lost");
//				loadingScene.Instance.lost();
//				PlayerPrefs.SetString("lost","no");
//				
//			}
//			if(PlayerPrefs.GetString("win")=="yes") 
//			{
//				//Application.LoadLevel("win");
//				loadingScene.Instance.win();
//				PlayerPrefs.SetString("win","no");
//				
//			}
//			if(PlayerPrefs.GetString("detail")=="yes") 
//			{
//				//Application.LoadLevel("detail");
//				loadingScene.Instance.detail();
//				PlayerPrefs.SetString("detail","no");
//				
//			}
//			if(PlayerPrefs.GetString("shopScene")=="yes") 
//			{
//			loadingScene.Instance.shop ();
//				//Application.LoadLevel("shopScene");
//				PlayerPrefs.SetString("shopScene","no");
//				
//			}
//			if( PlayerPrefs.GetString("trade")=="yes")
//			{
//				//Application.LoadLevel("trade");
//				loadingScene.Instance.trade();
//				PlayerPrefs.SetString("trade","no");
//				
//			}
//			if(PlayerPrefs.GetString("empireScene")=="yes") 
//			{
//			loadingScene.Instance.empire ();
//				//Application.LoadLevel("empireScene");
//				PlayerPrefs.SetString("empireScene","no");
//				
//			}
//			if(PlayerPrefs.GetString("community")=="yes")
//			{
//				//Application.LoadLevel("community");
//				loadingScene.Instance.community();
//				PlayerPrefs.SetString("community","no");
//				
//				
//			}
//
//			if(PlayerPrefs.GetString("newMain")=="yes")
//			{
//			loadingScene.Instance.main ();
//		//	Application.LoadLevel("menuNew");
//			PlayerPrefs.SetString("newMain","no");
//
//			}
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

				//loadingScene.Instance.scenes[i].SetActive(true);
				
				
			}
			
		}
		}
	public void trade()
	{
		Start();

		PlayerPrefs.SetString("chatScene","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			print("yes this work");
			menu.SetActive(false);
			isMneuActive=false;
			
		}
		else
		{
			
		}
		loadingScene.Instance.trade();
		//Application.LoadLevel("trade");
	}
	public void battle()
	{
		Start();

		PlayerPrefs.SetString("chatScene","yes");
		if(isMneuActive==true)
		{
			for(int j=0;j<bottomsButtons.Length;j++)
			{
				bottomsButtons[j].GetComponent<Image>().color=new Color32(255,255,255,255);
				bottomsButtons[j].GetComponentInChildren<Text>().color=new Color32(255,243,137,255);
				bottomsButtons[j].GetComponent<Button>().interactable=true;
			}
			print("yes this work");
			menu.SetActive(false);
			isMneuActive=false;
			
		}
		else
		{
			
		}
		loadingScene.Instance.battleScene();
		//Application.LoadLevel("Battle_Layout");
		
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
			
			//PlayerPrefs.SetString("chat","on");
			
		}
		
	}
}
