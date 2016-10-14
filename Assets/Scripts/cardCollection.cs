using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;

public class cardCollection : MonoBehaviour 
{
	public GameObject sort;
	public GameObject setting;
	public GameObject cardSelection1;
	public GameObject editFormation;
	bool isMneuActive=false;
	public Button[] bottomsButtons;
	public GameObject chatBtn;
	public GameObject menu;
	public Transform leftPosition;
	public Transform rightPosition;
	public Transform midPos;
	public int leftCount=0;
	public GameObject deck1;
	public GameObject deck2;
	public GameObject deck3;
	bool iseditDormation=true;

	//public GameObject [] row1Deck1;
	public GameObject [] row2Deck1;
	public GameObject [] row3Deck1;
	public GameObject [] row1Deck2;
	public GameObject [] row2Deck2;
	public GameObject [] row3Deck2;
	public GameObject [] row1Deck3;
	public GameObject [] row2Deck3;
	public GameObject [] row3Deck3;
	public string deckSelectCount;
	public List<GameObject> row1Deck1;
	public List<string> selectedButton;
	public List<Button> interActivity;
	GameObject addItem;
	public int deck1Counter=0;
	public int deck2Counter=1;
	public int deck3Counter=1;
	public Button confirm;

	// Use this for initialization
	void Start () 
	{
		resetPrevious();
		deckSelectCount="0";
		editFormation.SetActive(true);
		cardSelection1.SetActive(false);
		loadingScene.Instance.playerProfilePanel.SetActive(false);
		setting.SetActive(false);
		sort.SetActive(false);
		confirm.interactable=false;
//		for(int i=0;i<bottomsButtons.Length;i++)
//		{
//			//buttonClicks[0].GetComponent<Image>().color=new Color32(210,22,22,255);
//			
//			bottomsButtons[i].GetComponent<Image>().color=new Color32(131,106,106,255);
//			bottomsButtons[i].GetComponentInChildren<Text>().color=new Color32(131,106,106,255);
//			
//			//bottomsButtons[i].GetComponentInChildren(Text).GetComponent<Color32>()
//		}
		menu.SetActive(false);
	
	}
	void resetPrevious()
	{
		leftCount=0;
		if(iseditDormation==false)
		{
			iseditDormation=true;
		}
		deck1Counter=0;
		deck2Counter=1;
		deck3Counter=1;
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
	public void onSort()
	{
		sort.SetActive(true);


	}
	public void exitSort()
	{
		sort.SetActive(false);
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

	public void clickOnDeck(Button nameOfButtons)
	{


		editFormation.SetActive(false);
		cardSelection1.SetActive(true);
		iseditDormation=false;
		if(nameOfButtons.name=="Deck1Row1")
		{
			deckSelectCount="1";
			print(nameOfButtons.name);



		}
		else if(nameOfButtons.name=="Deck1Row2")
		{

			deckSelectCount="2";
			print(nameOfButtons.name);


	
		}
		else if(nameOfButtons.name=="Deck1Row3")
		{

			deckSelectCount="3";
			print(nameOfButtons.name);


		}
		else if(nameOfButtons.name=="Deck2Row1")
		{


			deckSelectCount="4";
			print(nameOfButtons.name);

		}
		else if(nameOfButtons.name=="Deck2Row2")
		{

			deckSelectCount="5";
			print(nameOfButtons.name);

		}
		else if(nameOfButtons.name=="Deck2Row3")
		{


			deckSelectCount="6";
			print(nameOfButtons.name);


		}
		else if(nameOfButtons.name=="Deck3Row1")
		{

			deckSelectCount="7";
			print(nameOfButtons.name);


		}
		else if(nameOfButtons.name=="Deck3Row2")
		{

			deckSelectCount="8";
			print(nameOfButtons.name);


		}
		else if(nameOfButtons.name=="Deck3Row3")
		{

			deckSelectCount="9";
			print(nameOfButtons.name);


		}


//		for(int j=0;j<row1Deck1.Length;j++)
//		{
//			PlayerPrefs.SetString("arrayAdded","j"+row1Deck1[j].name);
//
//		}


	}

	public void cardSelectionButton(Button buttonName)
	{



//		if(PlayerPrefs.GetString("deck1")=="yes")
//		{


//		if(deck1Counter<=  5)
//
//		{
			

			

			if(selectedButton.Contains(buttonName.name))
			{
				if(buttonName.GetComponent<Image>().color==new Color32(200,200,200,128))
				{
					if(deck1Counter>1)
					{
					buttonName.GetComponent<Image>().color=new Color32(255,255,255,255);
					selectedButton.Remove(buttonName.name);
					deck1Counter-=1;
					}

				}


				//buttonName.interactable=true;


			}
			else
			{

					if(deck1Counter<5)
					{
					selectedButton.Add(buttonName.name);
					buttonName.GetComponent<Image>().color=new Color32(200,200,200,128);		
					deck1Counter+=1;
					}

			

		//	buttonName.interactable=false;

//			}


		}
//		}
//
//		if(PlayerPrefs.GetString("deck2")=="yes")
//		{
//		if(deck2Counter>8)
//		{
//			
//		}
//		else
//		{
//			deck2Counter+=1;
//			
//			selectedButton.Add(buttonName.name);
//			
//		}
//		}
//
//		if(PlayerPrefs.GetString("deck3")=="yes")
//		{
//
//		if(deck3Counter>8)
//		{
//			
//		}
//		else
//		{
//			deck3Counter+=1;
//			
//			selectedButton.Add(buttonName.name);
//			
//		}
//		}
//	
	}

	public void backFromCards()
	{
		
		editFormation.SetActive(true);
		cardSelection1.SetActive(false);
		iseditDormation=true;
	}
	public void confrimButton()
	{

		editFormation.SetActive(true);
		cardSelection1.SetActive(false);
		iseditDormation=true;

		if(deckSelectCount=="1")
		{
			for (int i=0; i < row1Deck1.Count; i++)
			{
				row1Deck1[i].GetComponentInChildren<Text>().text=selectedButton[i];
				//InstanceList[o].doSomething();
			}

//			for(int i=0;i<row1Deck1;i++)
//				{
//
//				row1Deck1[i].GetComponentInChildren<Text>().text=selectedButton[i];
//				}
		}

		else if(deckSelectCount=="2")
		{

			for(int i=0;i<row2Deck1.Length;i++)
			{
				row2Deck1[i].GetComponentInChildren<Text>().text=selectedButton[i];
			}
		}
		else if(deckSelectCount=="3")
		{
			
			for(int i=0;i<row3Deck1.Length;i++)
			{
				row3Deck1[i].GetComponentInChildren<Text>().text=selectedButton[i];
			}
		}
		else if(deckSelectCount=="4")
		{
			
			for(int i=0;i<row1Deck2.Length;i++)
			{
				row1Deck2[i].GetComponentInChildren<Text>().text=selectedButton[i];
			}
		}
		if(deckSelectCount=="5")
		{
			
			for(int i=0;i<row2Deck2.Length;i++)
			{
				row2Deck2[i].GetComponentInChildren<Text>().text=selectedButton[i];
			}
		}
		else if(deckSelectCount=="6")
		{
			
			for(int i=0;i<row3Deck2.Length;i++)
			{
				row3Deck2[i].GetComponentInChildren<Text>().text=selectedButton[i];
			}
		}
		else if(deckSelectCount=="7")
		{
			
			for(int i=0;i<row1Deck3.Length;i++)
			{
				row1Deck3[i].GetComponentInChildren<Text>().text=selectedButton[i];
			}
		}
		else if(deckSelectCount=="8")
		{
			
			for(int i=0;i<row2Deck3.Length;i++)
			{
				row2Deck3[i].GetComponentInChildren<Text>().text=selectedButton[i];
			}
		}
		else if(deckSelectCount=="9")
		{
			
			for(int i=0;i<row3Deck3.Length;i++)
			{

				row3Deck3[i].GetComponentInChildren<Text>().text=selectedButton[i];
			}
		}

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

	public void leftButton()
	{	
		if(leftCount==0)
		{
			leftCount+=1;
			iTween.MoveTo(deck1,iTween.Hash("x",leftPosition.transform.position.x,"time",0.5f,"onComplete","deck2Object","onCompleteTarget",this.gameObject));
			
		}
		else if(leftCount==1)
		{
			leftCount+=1;
			iTween.MoveTo(deck2,iTween.Hash("x",leftPosition.transform.position.x,"time",0.5f,"onComplete","deck3Object","onCompleteTarget",this.gameObject));
		}
		
		
	}
	public void confirmButton()
	{
		Start();

		loadingScene.Instance.BattleFormation();
		menu.SetActive(false);
		isMneuActive=false;

		//Application.LoadLevel("Battle_Layout4");
	}
	public void rightButton()
	{
		if(leftCount==1)
		{
			leftCount-=1;
			iTween.MoveTo(deck2,iTween.Hash("x",rightPosition.transform.position.x,"time",0.5f,"onComplete","deck1Object","onCompleteTarget",this.gameObject));
		}
		else if(leftCount==2)
		{
			leftCount-=1;
			iTween.MoveTo(deck3,iTween.Hash("x",rightPosition.transform.position.x,"time",0.5f,"onComplete","deck2Object","onCompleteTarget",this.gameObject));
		}
	}
	void deck2Object()
	{
		iTween.MoveTo(deck2,iTween.Hash("x",midPos.transform.position.x,"time",0.5));
	}
	void deck3Object()
	{
		iTween.MoveTo(deck3,iTween.Hash("x",midPos.transform.position.x,"time",0.5));
	}
	void deck1Object()
	{
		iTween.MoveTo(deck1,iTween.Hash("x",midPos.transform.position.x,"time",0.5));
	}


	public void questButton()
	{
		Start();

		PlayerPrefs.SetString("cardCollection","yes");
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
		loadingScene.Instance.quest();

	//	Application.LoadLevel("quest");
	}
	public void shopScene()
	{
		Start();

		PlayerPrefs.SetString("cardCollection","yes");
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
		loadingScene.Instance.shop();
	
		//Application.LoadLevel("shopScene");
	}

	public void cardColectio()
	{
		Start();

		PlayerPrefs.SetString("cardCollection","yes");
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
		loadingScene.Instance.cardCollecton();
	
		//Application.LoadLevel("cardCollections");
	}
	public void inventory()
	{
		Start();

		PlayerPrefs.SetString("cardCollection","yes");
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
	public void backButton()
	{
		Start();


//		if(PlayerPrefs.GetString("chatScene")=="yes")
//		{
//			//Application.LoadLevel("chatScene");
//			loadingScene.Instance.chat();
//			PlayerPrefs.SetString("chatScene","no");
//			
//			
//		}
//		if(PlayerPrefs.GetString("layout3")=="yes")
//		{
//			loadingScene.Instance.BattleOpponentSelection();
//			//Application.LoadLevel("battle_Layout3");
//			PlayerPrefs.SetString("layout3","no");
//			
//		}
//		if(PlayerPrefs.GetString("inventoryScene")=="yes")
//		{
//			loadingScene.Instance.inventory();
//			//Application.LoadLevel("inventoryScene");
//			PlayerPrefs.SetString("inventoryScene","no");
//		}
//		
//		if(PlayerPrefs.GetString("battleLyout")=="yes")
//		{
//			loadingScene.Instance.battleScene();
//			//Application.LoadLevel("Battle_Layout");
//			PlayerPrefs.SetString("battleLyout","no");
//			
//		}
//		if(PlayerPrefs.GetString("Battle_Layout4")=="yes")
//		{
//			loadingScene.Instance.BattleFormation();
//			//Application.LoadLevel("Battle_Layout4");
//			PlayerPrefs.SetString("Battle_Layout4","no");
//			
//		}
//		if(PlayerPrefs.GetString("newMain")=="yes")
//		{
//			loadingScene.Instance.main();
//			//Application.LoadLevel("menuNew");
//			PlayerPrefs.SetString("newMain","no");
//			
//		}
//		if(PlayerPrefs.GetString("detail")=="yes")
//		{
//			loadingScene.Instance.detail();
//			//Application.LoadLevel("detail");
//			PlayerPrefs.SetString("detail","no");
//			
//		}
//		if(PlayerPrefs.GetString("lost")=="yes")
//		{
//			loadingScene.Instance.lost();
//			//Application.LoadLevel("lost");
//			PlayerPrefs.SetString("lost","no");
//			
//		}
//		if(PlayerPrefs.GetString("win")=="yes")
//		{
//			loadingScene.Instance.win();
//			//Application.LoadLevel("win");
//			PlayerPrefs.SetString("win","no");
//			
//		}
//		if(PlayerPrefs.GetString("shopScene")=="yes")
//		{
//			loadingScene.Instance.shop();
//		//	Application.LoadLevel("shopScene");
//			PlayerPrefs.SetString("shopScene","no");
//			
//		}
//		if(PlayerPrefs.GetString("quest")=="yes") 
//		{
//			loadingScene.Instance.quest();
//			//Application.LoadLevel("quest");
//			PlayerPrefs.SetString("quest","no");
//			
//		}
//		if(PlayerPrefs.GetString("empireScene")=="yes") 
//		{
//			loadingScene.Instance.empire();
//		//	Application.LoadLevel("empireScene");
//			PlayerPrefs.SetString("empireScene","no");
//			
//		}
//		if( PlayerPrefs.GetString("trade")=="yes")
//		{
//			loadingScene.Instance.trade();
//		//	Application.LoadLevel("trade");
//			PlayerPrefs.SetString("trade","no");
//			
//		}
//		if(PlayerPrefs.GetString("community")=="yes")
//		{
//			loadingScene.Instance.community();
//			//Application.LoadLevel("community");
//			PlayerPrefs.SetString("community","no");
//			
//			
//		}

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

			//	loadingScene.Instance.scenes[i].SetActive(true);
				
				
			}
			
		}

	}
	public void chatClick()
	{
		Start();

		PlayerPrefs.SetString("cardCollections","yes");
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
		loadingScene.Instance.chat();

		//Application.LoadLevel("chatScene");
	}
	public void community()
	{
		Start();

		PlayerPrefs.SetString("cardCollections","yes");
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

		//Application.LoadLevel("community");
		
	}
	public void trade()
	{
		Start();

		PlayerPrefs.SetString("cardCollections","yes");
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

	//	Application.LoadLevel("trade");
	}
	public void empire()
	{
		Start();

		PlayerPrefs.SetString("cardCollections","yes");
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
		loadingScene.Instance.empire();

	//	Application.LoadLevel("empireScene");
	}

	
	// Update is called once per frame
	void Update () 
	{
		if(deck1Counter<=1)
		confirm.interactable=false;
		else

		{
			confirm.interactable=true;


		}


		if(iseditDormation==true)
		{
		if(leftCount>0)
		{
			GameObject.Find("rightButton").GetComponent<Button>().interactable=true;
		}
		if(leftCount>1)
		{
			GameObject.Find("leftButton").GetComponent<Button>().interactable=false;
			
		}
		if(leftCount==0)
		{
			GameObject.Find("leftButton").GetComponent<Button>().interactable=true;
			GameObject.Find("rightButton").GetComponent<Button>().interactable=false;
			
			
		}
		if(leftCount==1)
		{
			GameObject.Find("leftButton").GetComponent<Button>().interactable=true;
			GameObject.Find("rightButton").GetComponent<Button>().interactable=true;
			
			
		}
		}
		else
		{

		}
	}

	public void exitMenu()
	{
		menu.SetActive (false);
		
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
	public void battle()
	{
		Start();

		PlayerPrefs.SetString("cardCollections","yes");
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
		loadingScene.Instance.objectToOpenMenu = this.gameObject;
		loadingScene.Instance.main();

		//Application.LoadLevel("menuNew");
	}



}
