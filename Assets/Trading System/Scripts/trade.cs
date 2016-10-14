using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class trade : MonoBehaviour 
{
	public GameObject playerProfile;
	public GameObject setting;
	Button tradeListButtonName;
	public GameObject createPost;
	public GameObject tradeSystem;
	public GameObject searchSystem;
	public Button [] buttonTabs;
	bool isMneuActive=false;
	public Button[] bottomsButtons;
	public GameObject chatBtn;
	public GameObject menuScreen;
	bool isConfirm=false;
	bool isRowMoved=false;
	Vector2 tempRow;
	bool buttonPress=false;
	bool cardLibItem=true;

	bool postToItemCard=true;
	bool postToItemCardDeck=true;



	public 	GameObject[] forumObj;

	int cardCounter;

	public List <GameObject> scenes = new List<GameObject>();

	// Use this for initialization
	void Start () 
	{
		resetPrevious();
		print("starsg");
		//PlayerPrefs.DeleteAll();
		//newMenuScene.instance.mainSound.Play();
//		cardselectorCounter=0;
		deactivateButtons(0);
		playerProfile.SetActive(false);

		searchSystem.SetActive(false);
		createPost.SetActive(false);
		setting.SetActive(false);



	}
	void resetPrevious()
	{

		if(isConfirm==true)
		{
			isConfirm=false;
		}
		if(isRowMoved==true)
		{
			isRowMoved=false;
		}

		if(buttonPress==true)
		{
			buttonPress=false;
		}

		if(cardLibItem==false)
		{
			cardLibItem=true;
		}

		if(postToItemCard==false)
		{
			postToItemCard=true;
		}
		if(postToItemCardDeck==false)
		{
			postToItemCardDeck=true;
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
			//loadingScene.Instance.allSounds[i].volume=loadingScene.Instance.bgmSliders[i].value;
		}
	}






//	public void cardSelection(Button cardName)
//	{
//
//
//		//if(cardUnlock)
//		//{
//
//			if(cardName.GetComponent<Image>().color==new Color32(49,47,41,255))
//			{
//
//			//cardUnlock=false;
//			cardName.GetComponent<Image>().color=new Color32(159,151,115,255);
//			cardselectorCounter-=1;
//
//			}
//			else
//			{
//			if(cardselectorCounter<5)
//			{
//			//	cardUnlock=true;
//			cardName.GetComponent<Image>().color=new Color32(49,47,41,255);
//			cardselectorCounter+=1;
//
//
//			}
//		}
//
//		print("========== COUNTER========"+cardselectorCounter);
//		//}
//
//
//
//
//	}
//	public void logOut()
//	{
//		Start();
//		onClickSettingExit();
//		PlayerPrefs.SetString("logout","yes");
//		newMenuScene.instance.logOut ();
//
//		//LoginScene.SetActive(true);
//	}
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
		playerProfile.SetActive(true);
	}
	public void onClickProfileExit()
		
	{
		playerProfile.SetActive(false);
	}

	public void leftMoveBuilding()
	{

	}


	public void deleteButtonmyOffer(Button clicked)
	{
		if(PlayerPrefs.GetString(tradeListButtonName.name)=="isSwiped")
		{
			Destroy (clicked.gameObject);

			print("destroy"+tradeListButtonName.name);
			Destroy(tradeListButtonName.gameObject);
			PlayerPrefs.SetString(tradeListButtonName.name,"notSwiped");
			isRowMoved=false;
		}


	}

	public void deleteButtonmyTrade(Button clicked)
	{
		if(PlayerPrefs.GetString(tradeListButtonName.name)=="isSwiped")
		{
			Destroy (clicked.gameObject);
			
			print("destroy"+tradeListButtonName.name);
			Destroy(tradeListButtonName.gameObject);
			PlayerPrefs.SetString(tradeListButtonName.name,"notSwiped");
			isRowMoved=false;
		}
		
		
	}

	public void clickRow(Button obj)
	{

		print("button nAME"+obj.name);
		tradeListButtonName=obj;

		PlayerPrefs.SetString("btnName","name"+tradeListButtonName);

		if(PlayerPrefs.GetString(obj.name)=="isSwiped")
		{
			//Move Left
			if(isRowMoved==true)
			{

				obj.interactable=false;
				iTween.MoveTo(obj.gameObject,iTween.Hash("x",tempRow.x,"time",0.7f,"onComplete","isMovedRowBack","onCompleteTarget",this.gameObject));

			}
		

		}

		else
		{
			if(isRowMoved==false)
			{

				//buttonPress=true;
				tempRow=obj.transform.position;
				obj.interactable=false;
				
			}
			//Move Right

		}



	}
	





	//-----------------------
	void isMovedRowGo()
	{
		tradeListButtonName.interactable=true;
		isRowMoved=true;

		PlayerPrefs.SetString(tradeListButtonName.name,"isSwiped");

	}
	void isMovedRowBack()
	{

		tradeListButtonName.interactable=true;
		isRowMoved=false;

		PlayerPrefs.SetString(tradeListButtonName.name,"notSwiped");


	}


	public void createPostButton()
	{
		for(int i=0; i<forumObj.Length;i++)
		{
			forumObj[i].SetActive(false);		
		}
	}

	public void clickEventForum()
	{


		for(int i=0; i<forumObj.Length;i++)
		{

			forumObj[i].SetActive(true);
			print("yesEnable");
		}
		createPost.SetActive(true);
//		disableCreatePost.interactable=false;

	}

	public void cardCollections()
	{
		Start();

		PlayerPrefs.SetString("trade","yes");
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
		menuScreen.SetActive(false);
		isMneuActive=false;

		//Application.LoadLevel("Battle_Layout4");
	}

	public void onClickSearch()
	{
		//tradeSystem.SetActive(false);
		searchSystem.SetActive(true);
	}
	public void backFromSearch()
	{
		tradeSystem.SetActive(true);
		searchSystem.SetActive(false);
	}



	public void exitMenu()
	{
		menuScreen.SetActive (false);
		
	}


	public void inventory()
	{
		Start();

		PlayerPrefs.SetString("trade","yes");
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
	public void chatButton()
	{
		Start();

		if(PlayerPrefs.GetString("chat")=="off")
		{
			chatBtn.GetComponent<DragHandeler>().enabled=true;
			chatBtn.GetComponent<CanvasGroup>().blocksRaycasts=true;
			//chatBtn.GetComponent<Button>().enabled=true;
			chatBtn.GetComponent<Button>().interactable=true;
			
			chatBtn.GetComponentInChildren<Text>().enabled=true;
			
			PlayerPrefs.SetString("chat","on");
			
			
		}
		
	}
	public void questButton()
	{
		Start();

		PlayerPrefs.SetString("quest","yes");
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
		loadingScene.Instance.quest();


		//Application.LoadLevel("quest");

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
		//menuScreen.SetActive (true);
		
	}
	public void chatClick()
	{
		Start();

		PlayerPrefs.SetString("trade","yes");
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

		//Application.LoadLevel("chatScene");
	}
	
	public void backButon()
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

				//loadingScene.Instance.scenes[i].SetActive(true);


			}

		}

		//loadingScene.Instance.scenes.Count-1
		//loads[i].SetActive(false);


		//Destroy(GameObject.Find("main(Clone)"));
//		if(PlayerPrefs.GetString("chatScene")=="yes")
//		{
//			loadingScene.Instance.chat();
//
//			//Application.LoadLevel("chatScene");
//			PlayerPrefs.SetString("chatScene","no");
//			
//			
//		}
//		if(PlayerPrefs.GetString("layout3")=="yes")
//		{
//			loadingScene.Instance.battle3();
//
//			//Application.LoadLevel("battle_Layout3");
//			PlayerPrefs.SetString("layout3","no");
//			
//		}
//		
//		if(PlayerPrefs.GetString("battleLyout")=="yes")
//		{
//			loadingScene.Instance.battle1();
//
//			//Application.LoadLevel("Battle_Layout");
//			PlayerPrefs.SetString("battleLyout","no");
//			
//		}
//		if(PlayerPrefs.GetString("Battle_Layout4")=="yes")
//		{
//			loadingScene.Instance.battle4();
//
//		//	Application.LoadLevel("Battle_Layout4");
//			PlayerPrefs.SetString("Battle_Layout4","no");
//			
//		}
//		if(PlayerPrefs.GetString("newMain")=="yes")
//		{
//
//			loadingScene.Instance.main ();
//			//Application.LoadLevel("menuNew");
//			PlayerPrefs.SetString("newMain","no");
//			
//		}
//		if(PlayerPrefs.GetString("inventoryScene")=="yes")
//		{
//			loadingScene.Instance.inventory();
//
//			//Application.LoadLevel("inventoryScene");
//			PlayerPrefs.SetString("inventoryScene","no");
//			
//		}
//		if(PlayerPrefs.GetString("cardCollection")=="yes") 
//		{
//			loadingScene.Instance.cardCollecton();
//
//			//Application.LoadLevel("cardCollections");
//			PlayerPrefs.SetString("cardCollection","no");
//			
//		}
//		if(PlayerPrefs.GetString("lost")=="yes") 
//		{
//			loadingScene.Instance.lost();
//
//			//Application.LoadLevel("lost");
//			PlayerPrefs.SetString("lost","no");
//			
//		}
//		if(PlayerPrefs.GetString("win")=="yes") 
//		{
//			loadingScene.Instance.win();
//
//			//Application.LoadLevel("win");
//			PlayerPrefs.SetString("win","no");
//			
//		}
//		if(PlayerPrefs.GetString("detail")=="yes") 
//		{
//			loadingScene.Instance.detail();
//
//			//Application.LoadLevel("detail");
//			PlayerPrefs.SetString("detail","no");
//			
//		}
//		if(PlayerPrefs.GetString("quest")=="yes") 
//		{
//			loadingScene.Instance.quest ();
//			//Application.LoadLevel("quest");
//			PlayerPrefs.SetString("quest","no");
//			
//		}
//		if(PlayerPrefs.GetString("empireScene")=="yes") 
//		{
//			loadingScene.Instance.empire ();
//			//Application.LoadLevel("empireScene");
//			PlayerPrefs.SetString("empireScene","no");
//			
//		}
//
//		if(PlayerPrefs.GetString("shopScene")=="yes") 
//		{
//			loadingScene.Instance.shop();
//			//Application.LoadLevel("shopScene");
//			PlayerPrefs.SetString("shopScene","no");
//			
//		}
//		else
//		{
//			loadingScene.Instance.main();
//
//			//Application.LoadLevel("menuNew");
//		}
	}
//	public void nextScene()
//	{
//		Application.LoadLevel("Battle_Layout4");
//	}

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

	public void RootMenuButton()
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
			menuScreen.SetActive(false);
			isMneuActive=false;
			
		}
		else
		{
			
		}
		loadingScene.Instance.objectToOpenMenu = this.gameObject;
		loadingScene.Instance.main();

		//Application.LoadLevel("menuNew");
	}
	public void battle()
	{
		Start();

		PlayerPrefs.SetString("trade","yes");
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


		//Application.LoadLevel("Battle_Layout");
		
	}
	public void empire()
	{
		Start();

		PlayerPrefs.SetString("trade","yes");
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
	public void shopScene()
	{
		Start();

		PlayerPrefs.SetString("trade","yes");
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
		loadingScene.Instance.shop();


		//Application.LoadLevel("shopScene");
	}


	public void clickOnTabs(Button buttonName)
	{
		
		if(buttonName.name =="Forum")
		{
			createPost.SetActive(false);
			searchSystem.SetActive(false);
			deactivateButtons(0);
			
		}
		else if(buttonName.name=="Bazaar")
		{
			createPost.SetActive(false);
			searchSystem.SetActive(false);


			//bazaar.SetActive(true);
			deactivateButtons(1);
			
		}
		else if(buttonName.name=="MyOffers")
		{

			createPost.SetActive(false);
			searchSystem.SetActive(false);


			deactivateButtons(2);
			
			
		}
		else if(buttonName.name=="MyTrades")
		{

			createPost.SetActive(false);
			searchSystem.SetActive(false);


			deactivateButtons(3);
		}
	}
	void deactivateButtons(int index)
	{
		
		for(int i=0;i<buttonTabs.Length;i++)
		{
			if(i==index)
			{
				buttonTabs[i].GetComponent<Button>().interactable=false;
			}
			else
			{
				buttonTabs[i].GetComponent<Button>().interactable=true;
			}
		}

	}



}
