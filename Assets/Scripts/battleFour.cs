using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class battleFour : MonoBehaviour
{
	public GameObject setting;
	public GameObject playerProfile;
	bool isMneuActive=false;
	public Button[] bottomsButtons;
	public GameObject chatBtn;
	public GameObject menuScreen;
	public GameObject battlePop;
	public GameObject deck1;
	public GameObject deck2;
	public GameObject deck3;
	public Transform leftPosition;
	public Transform rightPosition;
	public Transform midPos;
	public int leftCount=0;
	public GameObject battleProgress;
	// Use this for initialization
	void Start () 
	{
		leftCount=0;

		battleProgress.SetActive(false);
		battlePop.SetActive (false);
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

	public void logOut()
	{
		Start();

		onClickSettingExit();
		PlayerPrefs.SetString("logout","yes");
		loadingScene.Instance.main();
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

	public void community()
	{
		Start();

		PlayerPrefs.SetString("Battle_Layout4","yes");
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
	public void onClickProfile()
	{
		playerProfile.SetActive(true);
	}
	public void onClickProfileExit()
	{
		playerProfile.SetActive(false);
	}
	public void cardCollections()
	{
		Start();

		PlayerPrefs.SetString("Battle_Layout4","yes");
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
	public void shopScene()
	{
		Start();

		PlayerPrefs.SetString("Battle_Layout4","yes");
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
	public void empire()
	{
		Start();

		PlayerPrefs.SetString("Battle_Layout4","yes");
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
	public void invecntory()
	{
		Start();

		PlayerPrefs.SetString("Battle_Layout4","yes");
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

	public void questButton()
	{
		Start();

		PlayerPrefs.SetString("battle_Layout4","yes");
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

	public void battle()
	{
		Start();

		//battleProgress.SetActive(true);
		battlePop.SetActive(false);	

		loadingScene.Instance.startBattle ();

		//StartCoroutine(loadAsync());
		//Application.LoadLevel("BattleSceneTest");
	}
	private IEnumerator loadAsync()
	{


		AsyncOperation operation=Application.LoadLevelAsync("test");
		while(!operation.isDone) 
		{

			yield return operation.isDone;
			print("-------------------"+operation.progress);
			int percontlOAD = (int)((operation.progress+0.1f)*100);
			string presconString=percontlOAD.ToString()+" %";
			GameObject.Find("progress").GetComponent<Text>().text="LOADING "+presconString;
		}
	}
	public void battlePopHide()
	{
		battleProgress.SetActive(false);
		battlePop.SetActive(false);
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
	public void chatClick()
	{
		Start();

		PlayerPrefs.SetString("Battle_Layout4","yes");
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
		loadingScene.Instance.chat ();

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
				//scenes.Clear();

			}
			else
			{
				loadingScene.Instance.scenes[loadingScene.Instance.scenes.Count-2].SetActive(true);

				//loadingScene.Instance.scenes[i].SetActive(true);
				
				
			}
			
		}
	}
	
	// Update is called once per frame
	void Update () 
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

	public void trade()
	{
		Start();

		PlayerPrefs.SetString("Battle_Layout4","yes");
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
		//Application.LoadLevel("trade");
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
	public void confirmButton()
	{

		if(PlayerPrefs.GetString("fromBattle")=="yes")
		 {
			battlePop.SetActive(true);	

		}
		else if(PlayerPrefs.GetString("fromBattle")=="no")
		{
			loadingScene.Instance.quest();
			PlayerPrefs.SetString("fromQuest","yes");

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
	public void battleButton()
	{
		Start();

		PlayerPrefs.SetString("Battle_Layout4","yes");

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
		loadingScene.Instance.battleScene ();

	//	Application.LoadLevel("Battle_Layout");
		
	}

	public void editFormation()
	{
		Start();

		PlayerPrefs.SetString("Battle_Layout4","yes");
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

	//	Application.LoadLevel("cardCollections");
	}


}
