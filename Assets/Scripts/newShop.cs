
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using MiniJSON;

public class newShop : MonoBehaviour
{
	
	public GameObject setting;
	bool isMneuActive=false;
	public Button[] bottomsButtons;
	//public Button shopButton;
	public GameObject multiPleDetail;
	public GameObject singleDetail;
	public GameObject membershipTab;
	public GameObject warningSingle;
	public GameObject warningMultiple;
	public GameObject itemForLoyality;
	public GameObject[] drawSystem;
	public GameObject lift;
	//public GameObject lift2;
	public GameObject glowImage;
	public Transform downPos;
	public Transform upPos;
	public GameObject drawCard;

	//----------------------------

	public GameObject multiCard2;
	public GameObject multiCard3;

	public GameObject liftMulti;
	//public GameObject lift2Multi;
	public GameObject glowImageMulti;
	public Transform downPosMulti;
	public Transform upPosMulti;
	public GameObject drawCardMulti;

	Vector2 scaleGlow;
	Vector3 tempPos;

	public GameObject menu;
	public GameObject chatBtn;
	public GameObject detailTab;
	public GameObject [] itemSelection;
	public GameObject [] buttonClicks;

	public GameObject [] slectionUnderLoaylity;
	public GameObject [] buttonUnderLoylity;
	public GameObject purchaseSystem;
	public GameObject purchaseGems;
	public GameObject drawPurchase;
	bool isDrawActive=false;
	public GameObject finishedPurchased;
	public GameObject finishedGemsPurchased;


	//Shivam
	public GameObject PurchaseFailedPanel,PurchaseSuccessful,MembershipPanel,Days7Panel,Days14Panel,Days30Panel;
	int no_of_cards,NCD_cards,HCD_cards,temp_CardCounter,Vip_Membership_Days,MaxCardLimit,vipCardCounter;
	public Text costText_Draw,skillText1,skillText2,GemCostText,timeTillExpiry,timeLeft,PFText,
				GemsItemText,Gems_Text,DragonCoin_Text,DragonEgg_Text,VipStateText,VipText,
				CardItemText,MembershipItemText,MembershipCostText,RePurchase_CostText,DayText,
				RePurchase_ItemText,FailedPanel_Text,BalanceGems,CardOwnd,PS_Cost,PS_Item;
	public Button Next,Previous,MP_Button7,MP_Button14,MP_Button30,VIP_PurchaseDetail,
					MP_DC_Button7,MP_DC_Button14,MP_DC_Button30,DrawAgain;
	public Image CardImage;
	public bool Vip_Membership_7,Vip_Membership_14,Vip_Membership_30,retry;
	List<string> cardskill1=new List<string>();
	List<string> cardskill2=new List<string>();
	List<Sprite> cardSprite = new List<Sprite> ();
	string cardrarity;
	public static newShop _instance;
	bool BuyUsingGems,CardClick,VipClick,authorizePurchase;
	public int No_Of_Gems;
	public Text[] APGtext= new Text[6];
	public Text[] SPGtext= new Text[6];
	public Text[] APDtext= new Text[6];
	public Text[] SPDtext= new Text[6];
	public Text CostGem_Package1,CostGem_Package2,CostGem_Package3,CostGem_Package4,
				CostDC_Package1,CostDC_Package2,CostDC_Package3,CostDC_Package4;
	public GameObject DisGem_Obj1, DisGem_Obj2, DisGem_Obj3, DisGem_Obj4, DisDC_Obj1, DisDC_Obj2, DisDC_Obj3, DisDC_Obj4;
	public Text ItemDes_Gem1,ItemDes_Gem2,ItemDes_Gem3,ItemDes_Gem4,ItemDes_Gem5,DiscountText,
	ItemDes_Gem6,ItemDes_Gem7,ItemDes_Gem8,ItemDes_Gem9,ItemDes_Gem10,ItemDes_Gem11,ItemDes_Gem12;
	public Text ItemDes_DC1,ItemDes_DC2,ItemDes_DC3,ItemDes_DC4,ItemDes_DC5,
	ItemDes_DC6,ItemDes_DC7,ItemDes_DC8,ItemDes_DC9,ItemDes_DC10,ItemDes_DC11,ItemDes_DC12;
	TimeSpan Difference;
	void Awake()
	{
		_instance = this;

	}
	//Shivam end
	// Use this for initialization
	void Start () 
	{	
		retry = false;
		VipClick = false;
		PurchaseFailedPanel.SetActive (false);
		PurchaseSuccessful.SetActive (false);
		CheckCardLimit_Castle ();
		UpdatePotion ();

		if(isDrawActive==true)
		{
			isDrawActive=false;
		}

		deactivateItems(0);
		VIP_Button_Interactable();
		for(int i=0;i<buttonUnderLoylity.Length;i++)
		{
			buttonUnderLoylity[i].SetActive(false);
		}
		if(PlayerPrefs.GetString("newMain")=="yes")
		{
			print("======== GADBAD========");
		} 
//		for(int i=0;i<bottomsButtons.Length;i++)
//		{
//			//buttonClicks[0].GetComponent<Image>().color=new Color32(210,22,22,255);
//			
//			bottomsButtons[i].GetComponent<Image>().color=new Color32(131,106,106,255);
//			bottomsButtons[i].GetComponentInChildren<Text>().color=new Color32(131,106,106,255);
//			
//			//bottomsButtons[i].GetComponentInChildren(Text).GetComponent<Color32>()
//		}
		loadingScene.Instance.playerProfilePanel.SetActive(false);

		//---------lAKHBIR-----
		membershipTab.SetActive(false);
		//-------------
//		PlayerPrefs.DeleteAll();
		//---------lAKHBIR-----

//		warningSingle.SetActive(false);
//		warningMultiple.SetActive(false);
//		multiPleDetail.SetActive(false);
//		singleDetail.SetActive(false);
		//---------lAKHBIR-----

		multiCard2.GetComponent<Canvas>().sortingOrder=1;
		multiCard2.GetComponent<Canvas>().sortingOrder=2;
		drawCardMulti.GetComponent<Canvas>().sortingOrder=3;
		multiCard2.SetActive(false);
		multiCard3.SetActive(false);
		drawPurchase.SetActive(false);
		for(int i=0;i<drawSystem.Length;i++)
		{
		drawSystem[i].SetActive(false);
		}
		glowImage.SetActive(false);
		//lift2.SetActive(false);
		setting.SetActive(false);
		glowImageMulti.SetActive(false);
		//lift2Multi.SetActive(false);
		finishedGemsPurchased.SetActive(false);
		finishedPurchased.SetActive(false);
		purchaseGems.SetActive(false);
		purchaseSystem.SetActive(false);
		detailTab.SetActive(false);
		menu.SetActive(false);
		buttonClicks[0].GetComponent<Button>().interactable=false;
		//buttonClicks[0].GetComponent<Image>().color=new Color32(210,22,22,255);
//		PlayerPrefs.SetString("drawToggle","no");
//		PlayerPrefs.SetString("toggleMember","no");
//		PlayerPrefs.SetString("toggleItem","no");
//		PlayerPrefs.SetString("toggleLoyal","no");

		ClearList ();
		temp_CardCounter -= 1;
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
	public void lockedUnlock(Button name)
	{
		if(name.GetComponentInChildren<Text>().text=="Unlocked")
		{
			name.GetComponentInChildren<Text>().text="Locked";
		}
		else if(name.GetComponentInChildren<Text>().text=="Locked")
		{
			name.GetComponentInChildren<Text>().text="Unlocked";

		}

	}
	public void notificationOnOff()
	{
		loadingScene.Instance.notificationOnOff();
	}
	public void logOut()
	{
		Start();
		onClickSettingExit();
		PlayerPrefs.SetString("logout","yes");
		loadingScene.Instance.main();
		//LoginScene.SetActive(true);
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

	public void waringSingleActive()
	{
		warningSingle.SetActive(true);
	}
	public void warningMultiActive()
	{
		warningMultiple.SetActive(true);
		if (CardClick) {
			DrawAgain.interactable = true;
			RePurchase_CostText.text = "" + costText_Draw.text;
			RePurchase_ItemText.text = "" + CardItemText.text;
		} 
	}
	public void empire()
	{
		PlayerPrefs.SetString("shopScene","yes");
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
		loadingScene.Instance.empire();
		//Application.LoadLevel("empireScene");
	}
	public void chatClick()
	{
		Start();

		PlayerPrefs.SetString("shopScene","yes");
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
	public void waringSingleDeactive()
	{
		warningSingle.SetActive(false);
	}
	public void warningMultiDeactive()
	{
		warningMultiple.SetActive(false);
	}
	public void reTrySingle()
	{
		exitDetailMultiPle ();
		singleDetail.SetActive(false);
		warningSingle.SetActive(false);
		offDraw();
		Invoke("reactive1",0.3f);
	}
	public void reTryMultiple()
	{
//		ClearList ();
//		Debug.Log ("Amma" +cardSprite.Count);
//		temp_CardCounter = 0;
//		multiPleDetail.SetActive(false);
		warningMultiple.SetActive(false);
		exitDetailMultiPle();
		retry = true;
		if (no_of_cards == 1 ) {
			purchasePackage1ClickDraw (BuyUsingGems);
		} else if (no_of_cards == 5) {
			purchasePackage2ClickDraw (BuyUsingGems);
		} else if (no_of_cards == 9) {
			purchasePackage3ClickDraw (BuyUsingGems);
		} else if (no_of_cards == 20) {
			purchasePackage4ClickDraw (BuyUsingGems);
		} else {
		}
		//offDraw();

		//Invoke("reactive1",0.3f);

	}
	void reactive1()
	{
		
		int dra = UnityEngine.Random.Range(0,drawSystem.Length);
		for(int i=0;i<drawSystem.Length;i++)
		{
			if(i==dra)
			{
				
				drawSystem[i].SetActive(true);
			}
			else
			{
				drawSystem[i].SetActive(false);
				
			}
		}

		//drawSystem[1].SetActive(true);


	}
	void reactive2()
	{
		drawSystem[0].SetActive(true);
		
		
	}
	public void exitDetailMultiPle()
	{
		DrawAgain.interactable = true;
		ClearList ();
		temp_CardCounter = -1;
		multiPleDetail.SetActive(false);
		offDraw();

	}

	public void exitDetailSingle()
	{
		singleDetail.SetActive(false);
		offDraw();

	}

	public void drawPlay()
	{
		print("draPlay");
		tempPos=drawCard.transform.position;
		iTween.MoveTo(drawCard,iTween.Hash("y",downPos.transform.position.y,"time",0.6,"onComplete","upLoction","onCompleteTarget",this.gameObject,"easeType",iTween.EaseType.linear));

	}
	void upLoction()
	{
		lift.SetActive(false);
		//lift2.SetActive(true);

		iTween.MoveTo(drawCard,iTween.Hash("y",upPos.transform.position.y,"time",1,"easeType",iTween.EaseType.linear,"onComplete","glowTrue","onCompleteTarget",this.gameObject));
	}

	void glowTrue()
	{
		glowImage.SetActive(true);
		glowBig();
	}
	void glowBig()
	{
		scaleGlow=glowImage.transform.localScale;
		iTween.ScaleTo(glowImage,iTween.Hash("x",5,"y",5,"time",1,"easeType",iTween.EaseType.linear,"onComplete","actvieSsingleDetail","onCompleteTarget",this.gameObject));
		//Invoke("offDraw",0.2f);
	}
	public void QuestButton()
	{
		Start();

		//PlayerPrefs.SetString("newMain","no");

		PlayerPrefs.SetString("shopScene","yes");
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
		//Application.LoadLevel("quest");
	}
	void actvieSsingleDetail()
	{
		temp_CardCounter = -1;
		for(int i=0;i<drawSystem.Length;i++)
		{
			drawSystem[i].SetActive(false);
		}
		//singleDetail.SetActive(true);
		multiPleDetail.SetActive(true);
		CheckCardLimit_Castle ();
		BalanceGems.text = "" + PlayerParameters._instance.myPlayerParameter.gems;
		CardOwnd.text =  CardsManager._instance.mycards.Count.ToString() + "/"+MaxCardLimit;
		Debug.Log ("sfsfdsfsfsdfds"+ VipClick);
		if (VipClick) {
			
			DrawAgain.interactable = false;
		}
		Debug.Log ("tempCounter" + temp_CardCounter);
		NextCard();
		swipeika.instance.reGenrateArrow();
	}
	void offDraw()
	{
		for(int i=0;i<drawSystem.Length;i++)
		{
		drawSystem[i].SetActive(false);
		}
		multiCard2.transform.eulerAngles = new Vector3(0,0,0);
		multiCard3.transform.eulerAngles = new Vector3(0,0,0);
		drawCardMulti.transform.eulerAngles = new Vector3(0,0,0);
		glowImage.transform.localScale=scaleGlow;
		glowImage.SetActive(false);
		glowImageMulti.transform.localScale=scaleGlow;
		drawCard.transform.position=tempPos;
		drawCardMulti.transform.position=tempPos;
		glowImageMulti.SetActive(false);
		multiCard2.SetActive(false);
		multiCard3.SetActive(false);
		liftMulti.SetActive(true);
		lift.SetActive(true);

	}


	//------------

	public void drawPlaymulti()
	{
		print("draPlay");
		tempPos=drawCardMulti.transform.position;
		iTween.MoveTo(drawCardMulti,iTween.Hash("y",downPosMulti.transform.position.y,"time",0.6,"onComplete","upLoctionmulti","onCompleteTarget",this.gameObject,"easeType",iTween.EaseType.linear));
		
	}
	void upLoctionmulti()
	{
		liftMulti.SetActive(false);
		//lift2Multi.SetActive(true);
		
		iTween.MoveTo(drawCardMulti,iTween.Hash("y",upPosMulti.transform.position.y,"time",1,"easeType",iTween.EaseType.linear,"onComplete","cardRotate","onCompleteTarget",this.gameObject));
	}

	void cardRotate()
	{
		iTween.RotateTo(drawCardMulti,iTween.Hash("z",10,"time",0.5));
		activeCacrds();
		Invoke("rotate1",0.0f);
		Invoke("rotate2",0.0f);
	}
	void rotate1()
	{

		iTween.RotateTo(multiCard2,iTween.Hash("z",0,"time",1));

	}
	void rotate2()
	{

		iTween.RotateTo(multiCard3,iTween.Hash("z",-10,"time",1,"onComplete","glowTruemulti","onCompleteTarget",this.gameObject));
		
	}

	void activeCacrds()
	{
		multiCard2.GetComponent<Canvas>().sortingOrder=1;
		multiCard2.GetComponent<Canvas>().sortingOrder=2;
		multiCard2.SetActive(true);
		multiCard3.SetActive(true);
	}
	void glowTruemulti()
	{
		glowImageMulti.SetActive(true);
		glowBigmulti();
	}
	void glowBigmulti()
	{
		iTween.ScaleTo(glowImageMulti,iTween.Hash("x",5,"y",5,"time",1,"easeType",iTween.EaseType.linear,"onComplete","activeMultiPleDetail","onCompleteTarget",this.gameObject));
		//Invoke("offDraw",0.2f);

	//	Invoke("activeMultiPleDetail",1.5f);
	}

	void activeMultiPleDetail()
	{
		Debug.Log ("MultiDetails call");
		for(int i=0;i<drawSystem.Length;i++)
		{
			drawSystem[i].SetActive(false);
		}
		if (VipClick) {
			DrawAgain.interactable = false;
		}
		multiPleDetail.SetActive(true);
		BalanceGems.text = "" + PlayerParameters._instance.myPlayerParameter.gems;
		CardOwnd.text =  CardsManager._instance.mycards.Count.ToString() + "/"+MaxCardLimit;
		temp_CardCounter = -1;
		NextCard ();
		swipeika.instance.reGenrateArrow();

	}

	//Shivam

IEnumerator WaitForRequest(WWW www)
{
		
		yield return www;
	// check for errors
		if (www.error == null) {
			//IDictionary cardDict = Json.Deserialize(www.text) as IDictionary;
			if (www.text.Contains ("error_msg")) {
				//{"success":0,"error_msg":"No cards available!"}
				if (www.text.Contains ("No cards available!")) {
					//CallBack(true);
				} else {
					//	CallBack(false);
					newMenuScene.instance.popupFromServer.ShowPopup ("Network Error!");
				}
			} else {
				Debug.Log ("Success : " + www.text);
				//{"success":1,"msg":"Cards data success","Player_Card_detail":[{"card_id":"205","cardCategory":"Ktini","cardName":"Firasil","cardCost":"6","cardRarity":"Uncommon","cardskillstrength1":"Weak",
				//"cardskillstrength2":"Weak","cardSkillsname1":"Fierce Outrage","cardSkillsname2":"Fierce Outrage","card_no_in_players_list":398,"experience":100,"cardLevel":1,"subCardId":"232","subCard_cardId":"205"
				//,"subCardtype":"Warrior","subcardLeadership":"148","subCardAttack":"213","subCardDefense":"179"}]}


				//{"success":1,"msg":"Cards data success","Player_Card_detail":[{"card_id":"275","cardCategory":
				//"Pnevma","cardName":"Cova","cardCost":"6","cardRarity":"Uncommon","cardskillstrength1":"Weak",
				//"cardskillstrength2":"Weak","cardSkillsname1":"Fierce Outrage","cardSkillsname2":"Fierce Outrage",
				//"card_no_in_players_list":286,"experience":100,"cardLevel":1,"subCardId":"581","subCard_cardId":"275",
				//"subCardtype":"Perfect","subcardLeadership":"185","subCardAttack":"188","subCardDefense":"211"}]}
			}
			if (www.text.Contains ("\"success\":1")) {
				//Debug.Log((Json.Deserialize (www.text) as IDictionary) ["Player_Card_detail"] as IList).length));

				for (int i = 0; i < no_of_cards; i++) {
					IDictionary newCardData = ((Json.Deserialize (www.text) as IDictionary) ["Player_Card_detail"] as IList) [i] as IDictionary;

					
//				foreach (IList temp in (Json.Deserialize (www.text) as IDictionary)["Player_Card_detail"] as IList)
////				{
////					newCardData.Add(temp);
////				}
					CardsManager.CardParameters newcard = new CardsManager.CardParameters ();
					foreach (string key in newCardData.Keys) {
						Debug.Log (key);
					}

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
					if (i < cardsToSaveInMyCards) {
						newcard.is_captive = 0;
						CardsManager._instance.AddCardForEmpire (newcard);
						CardsManager._instance.mycards.Add (newcard);
					} else {
						newcard.is_captive = 2;
						CardsManager._instance.myStashCards.Add (newcard);
					}
					cardskill1.Add (newCardData ["cardSkillsname1"].ToString ());
					cardskill2.Add (newCardData ["cardSkillsname2"].ToString ());
					cardSprite.Add(Resources.Load<Sprite> ("images/" + newcard.card_name));
					Debug.Log (newcard.card_id_in_database);
					Debug.Log ("Success");
				}
			}
		}
//	Shivam end	


}




	public void NextCard()
	{
		Debug.Log ("Next card method call"+ temp_CardCounter);
		if ((temp_CardCounter+1) < no_of_cards) {
			temp_CardCounter += 1;
			skillText1.text = temp_CardCounter + cardskill1 [temp_CardCounter] ;
			skillText2.text = temp_CardCounter + cardskill2 [temp_CardCounter] ;
			CardImage.sprite = cardSprite [temp_CardCounter];
		} 
		if (temp_CardCounter+1 == no_of_cards) {
			Next.interactable = false;
		} else {
			Next.interactable = true;
		}
		if (no_of_cards==1) {
			Next.interactable = false;
			Previous.interactable = false;
		} 
		if (temp_CardCounter > 0) {
			Previous.interactable = true;
		} else {
			Previous.interactable = false;
		}
	}

	public void PreviousCard()
	{
		Debug.Log ("Previous card method call" +temp_CardCounter);
		if (temp_CardCounter > 0) {
			temp_CardCounter -= 1;
			skillText1.text = temp_CardCounter + cardskill1 [temp_CardCounter];
			skillText2.text = temp_CardCounter + cardskill2 [temp_CardCounter];
			CardImage.sprite = cardSprite [temp_CardCounter];
			Previous.interactable = true;
		} 
		if(temp_CardCounter==0){
			Previous.interactable = false;
		}
		if (temp_CardCounter < no_of_cards) {
			Next.interactable = true;
		} else{
			Next.interactable = false;
		}
	}
	public void VIP_UI_Back()
	{
		CancelInvoke ("VIP_Timer");
		Days7Panel.SetActive (false);
		Days14Panel.SetActive (false);
		Days30Panel.SetActive (false);
		MembershipPanel.SetActive (false);

	}
	public void OnVIP_Details(int days)
	{
		MembershipPanel.SetActive (true);
		if (days == 7) {
			VipText.text = "Weeklong Membership";
			DiscountText.text = "Discount - 2%";
			Days7Panel.SetActive (true);
			if (PlayerParameters._instance.myPlayerParameter.membership_no == days) {
				VIP_Timer ();

			} else {
				VipStateText.text = "Activate Now";
				timeTillExpiry.text = "00:00";
				timeLeft.text = "00:00";
			}
		} else if (days == 14) 
		{
			VipText.text = "Fortnight Membership";
			DiscountText.text = "Discount - 3.5%";
			Days14Panel.SetActive (true);
			if (PlayerParameters._instance.myPlayerParameter.membership_no == days) {
				VIP_Timer ();
			} else {
				VipStateText.text = "Activate Now";
				timeTillExpiry.text = "00:00";
				timeLeft.text = "00:00";
			}
		} else if (days == 30) {
			VipText.text = "Month Membership";
			DiscountText.text = "Discount - 5%";
			Days30Panel.SetActive (true);
			if (PlayerParameters._instance.myPlayerParameter.membership_no == days) {
				VIP_Timer();
			} else {
				VipStateText.text = "Activate Now";
				timeTillExpiry.text = "00:00";
				timeLeft.text = "00:00";
			}
		} else {
		}


	}
	void VIP_Timer()
	{
		Difference = PlayerParameters._instance.myPlayerParameter.time_of_membership_no.AddDays (PlayerParameters._instance.myPlayerParameter.membership_no) - TimeManager._instance.GetCurrentServerTime ();
		if (Difference.Days <= 1 && Difference.Hours <= 1) {
			timeTillExpiry.text = Difference.Days + "Day : " + Difference.Hours + "Hour";
		} else if (Difference.Days <= 1 && Difference.Hours >= 1) {
			timeTillExpiry.text = Difference.Days + "Day : " + Difference.Hours + "Hours";
		} else if (Difference.Days >= 1 && Difference.Hours <= 1) {
			timeTillExpiry.text = Difference.Days + "Days : " + Difference.Hours + "Hour";
		} else {
			timeTillExpiry.text = Difference.Days + "Days: " + Difference.Hours + "Hours";
		}
		timeLeft.text =	Difference.Hours + ":" + Difference.Minutes + ":" + Difference.Seconds;
		DayText.text = "DAY " + AmountInWords(PlayerParameters._instance.myPlayerParameter.membership_no - Difference.Days).ToUpper();
		if (Difference.Days == 0 && Difference.Hours == 0 && Difference.Minutes == 0 && Difference.Seconds <= 1) {
			CancelInvoke ("VIP_Timer");
			PlayerParameters._instance.myPlayerParameter.membership_no = 0;
			Dictionary<string, string> avatarParameters = new Dictionary<string, string> ();
			avatarParameters.Add ("membership_no", PlayerParameters._instance.myPlayerParameter.membership_no.ToString ());
			StartCoroutine (PlayerParameters._instance.SendPlayerParameters (avatarParameters, null));
			VIP_UI_Back ();
		}	
		Invoke ("VIP_Timer", 1f);

	}
	public static string AmountInWords(int n)
	{

		if (n == 0)
			return "Zero";
		else if (n > 0 && n <= 19) {
			string[] arr = new string[] {
				"One",
				"Two",
				"Three",
				"Four",
				"Five",
				"Six",
				"Seven",
				"Eight",
				"Nine",
				"Ten",
				"Eleven",
				"Twelve",
				"Thirteen",
				"Fourteen",
				"Fifteen",
				"Sixteen",
				"Seventeen",
				"Eighteen",
				"Nineteen"
			};
			return arr [n - 1] + " ";
		} else if (n >= 20 && n <= 99) {
			string[] arr = new string[] { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
			return arr [n / 10 - 2] + " " + AmountInWords (n % 10);
		} else {
			return "";
		}
	}
	public void yesButton_MemberShip()
	{
		//PFText.text = MembershipItemText.text;
		if (BuyUsingGems) {
			purchaseSystem.SetActive (false);
			finishedPurchased.SetActive (true);
			purchaseGems.SetActive (false);

			VipClick = true;
			//Shivam
			if (Vip_Membership_Days == 7) {
				PS_Item.text = "Weeklong Membership";
				PlayerParameters._instance.myPlayerParameter.gems -= 800;
				PS_Cost.text = "Cost: 800 Gems";
				//PS_Left.text = "Left: " + PlayerParameters._instance.myPlayerParameter.gems + " Gems";
				Vip_Membership_7 = true;
				PlayerParameters._instance.myPlayerParameter.membership_no = 7;
				PlayerParameters._instance.myPlayerParameter.time_of_membership_no = TimeManager._instance.GetCurrentServerTime ();
				Dictionary<string, string> avatarParameters = new Dictionary<string, string> ();
				avatarParameters.Add ("time_of_membership_no", PlayerParameters._instance.myPlayerParameter.time_of_membership_no.ToString ());
				avatarParameters.Add ("membership_no", PlayerParameters._instance.myPlayerParameter.membership_no.ToString ());
				avatarParameters.Add ("gems", PlayerParameters._instance.myPlayerParameter.gems.ToString ());
				StartCoroutine (PlayerParameters._instance.SendPlayerParameters (avatarParameters, null));
				purchaseCard_VIPmembership ();
				VIP_Button_Interactable();
				CheckCardLimit_Castle ();
				no_of_cards = 1;
				//vipCardCounter = 1;
				CardLimitCheck (no_of_cards);
//				if(authorizePurchase)
					yesButtonDraw_VIP  ();

			} else if (Vip_Membership_Days == 14) {
				PS_Item.text = "Fortnight Membership";
				PlayerParameters._instance.myPlayerParameter.gems -= 1500;
				PS_Cost.text = "Cost: 1500 Gems";
				//PS_Left.text = "Left: " + PlayerParameters._instance.myPlayerParameter.gems + " Gems";
				Vip_Membership_14 = true;
				PlayerParameters._instance.myPlayerParameter.membership_no = 14;
				PlayerParameters._instance.myPlayerParameter.time_of_membership_no = TimeManager._instance.GetCurrentServerTime ();
				Dictionary<string, string> avatarParameters = new Dictionary<string, string> ();
				avatarParameters.Add ("time_of_membership_no", PlayerParameters._instance.myPlayerParameter.time_of_membership_no.ToString ());
				avatarParameters.Add ("membership_no", PlayerParameters._instance.myPlayerParameter.membership_no.ToString ());
				avatarParameters.Add ("gems", PlayerParameters._instance.myPlayerParameter.gems.ToString ());
				StartCoroutine (PlayerParameters._instance.SendPlayerParameters (avatarParameters, null));
				VIP_Button_Interactable();
				CheckCardLimit_Castle ();
				no_of_cards = 2;
				//vipCardCounter = 2;
				CardLimitCheck (no_of_cards);
//				if (authorizePurchase) {
					//for (int i = 1; i <= 2; i++) {
						purchaseCard_VIPmembership ();
						yesButtonDraw_VIP ();
					//}
//				}
			} else {
				PS_Item.text = "Month Membership";
				PlayerParameters._instance.myPlayerParameter.gems -= 3000;
				PS_Cost.text = "Cost: 3000 Gems";
				//PS_Left.text = "Left: " + PlayerParameters._instance.myPlayerParameter.gems + " Gems";
				Vip_Membership_30 = true;
				PlayerParameters._instance.myPlayerParameter.membership_no = 30;
				PlayerParameters._instance.myPlayerParameter.time_of_membership_no = TimeManager._instance.GetCurrentServerTime ();
				Dictionary<string, string> avatarParameters = new Dictionary<string, string> ();
				avatarParameters.Add ("time_of_membership_no", PlayerParameters._instance.myPlayerParameter.time_of_membership_no.ToString ());
				avatarParameters.Add ("membership_no", PlayerParameters._instance.myPlayerParameter.membership_no.ToString ());
				avatarParameters.Add ("gems", PlayerParameters._instance.myPlayerParameter.gems.ToString ());
				StartCoroutine (PlayerParameters._instance.SendPlayerParameters (avatarParameters, null));
				VIP_Button_Interactable();
				CheckCardLimit_Castle ();
				no_of_cards = 5;
				//vipCardCounter = 5;
				CardLimitCheck (no_of_cards);
//				if (authorizePurchase) {
					//for (int i = 1; i <= 5; i++) {
						purchaseCard_VIPmembership ();
						yesButtonDraw_VIP();
					//}
//				}
			}
		} else {
			purchaseSystem.SetActive (false);
			finishedPurchased.SetActive (true);
			purchaseGems.SetActive (false);

			//Shivam
			if (Vip_Membership_Days == 7) {
				PS_Item.text = "Weeklong Membership";
				PlayerParameters._instance.myPlayerParameter.dragon_coins -= 500;
				PS_Cost.text = "Cost: 500 Loyalty Points";
				//PS_Left.text = "Left: " + PlayerParameters._instance.myPlayerParameter.dragon_coins + " Loyalty Points";
				Vip_Membership_7 = true;
				PlayerParameters._instance.myPlayerParameter.membership_no = 7;
				PlayerParameters._instance.myPlayerParameter.time_of_membership_no = TimeManager._instance.GetCurrentServerTime ();
				Dictionary<string, string> avatarParameters = new Dictionary<string, string> ();
				avatarParameters.Add ("time_of_membership_no", PlayerParameters._instance.myPlayerParameter.time_of_membership_no.ToString ());
				avatarParameters.Add ("membership_no", PlayerParameters._instance.myPlayerParameter.membership_no.ToString ());
				avatarParameters.Add ("dragon_coins", PlayerParameters._instance.myPlayerParameter.dragon_coins.ToString ());
				StartCoroutine (PlayerParameters._instance.SendPlayerParameters (avatarParameters, null));
				purchaseCard_VIPmembership ();
				VIP_Button_Interactable();
				CheckCardLimit_Castle ();
				no_of_cards = 1;
				//vipCardCounter = 1;
				CardLimitCheck (no_of_cards);
				//if(authorizePurchase)
					yesButtonDraw_VIP ();
				


			} else if (Vip_Membership_Days == 14) {
				PS_Item.text = "Fortnight Membership";
				PlayerParameters._instance.myPlayerParameter.dragon_coins -= 950;
				PS_Cost.text = "Cost: 950 Loyalty Points";
				//PS_Left.text = "Left: " + PlayerParameters._instance.myPlayerParameter.dragon_coins + " Loyalty Points";
				Vip_Membership_14 = true;
				PlayerParameters._instance.myPlayerParameter.membership_no = 14;
				PlayerParameters._instance.myPlayerParameter.time_of_membership_no = TimeManager._instance.GetCurrentServerTime ();
				Dictionary<string, string> avatarParameters = new Dictionary<string, string> ();
				avatarParameters.Add ("time_of_membership_no", PlayerParameters._instance.myPlayerParameter.time_of_membership_no.ToString ());
				avatarParameters.Add ("membership_no", PlayerParameters._instance.myPlayerParameter.membership_no.ToString ());
				avatarParameters.Add ("dragon_coins", PlayerParameters._instance.myPlayerParameter.dragon_coins.ToString ());
				StartCoroutine (PlayerParameters._instance.SendPlayerParameters (avatarParameters, null));
				VIP_Button_Interactable();
				CheckCardLimit_Castle ();
				no_of_cards = 2;

				CardLimitCheck (no_of_cards);
//				if (authorizePurchase) {
					//for (int i = 1; i <= 2; i++) {
						purchaseCard_VIPmembership ();

						yesButtonDraw_VIP ();
					//}
//				}
			} else {
				PS_Item.text = "Month Membership";
				PlayerParameters._instance.myPlayerParameter.dragon_coins -= 1800;
				PS_Cost.text = "Cost: 1800 Loyalty Points";
				//PS_Left.text = "Left: " + PlayerParameters._instance.myPlayerParameter.dragon_coins + " Loyalty Points";
				Vip_Membership_30 = true;
				PlayerParameters._instance.myPlayerParameter.membership_no = 30;
				PlayerParameters._instance.myPlayerParameter.time_of_membership_no = TimeManager._instance.GetCurrentServerTime ();
				Dictionary<string, string> avatarParameters = new Dictionary<string, string> ();
				avatarParameters.Add ("time_of_membership_no", PlayerParameters._instance.myPlayerParameter.time_of_membership_no.ToString ());
				avatarParameters.Add ("membership_no", PlayerParameters._instance.myPlayerParameter.membership_no.ToString ());
				avatarParameters.Add ("dragon_coins", PlayerParameters._instance.myPlayerParameter.dragon_coins.ToString ());
				StartCoroutine (PlayerParameters._instance.SendPlayerParameters (avatarParameters, null));
				VIP_Button_Interactable();
				CheckCardLimit_Castle ();
				no_of_cards = 5;
				CardLimitCheck (no_of_cards);
//				if (authorizePurchase) {
					//for (int i = 1; i <= 5; i++) {
						purchaseCard_VIPmembership ();
						yesButtonDraw_VIP ();
					//}
//				}
			}
		}
	}
	public int Discount(int Amount)
	{
		int AfterDiscount;
		if (PlayerParameters._instance.myPlayerParameter.membership_no == 7) {
			AfterDiscount =Amount - Convert.ToInt32((Amount * 2) / 100);
		} else if (PlayerParameters._instance.myPlayerParameter.membership_no == 14) {
			AfterDiscount = Amount - Convert.ToInt32((Amount * 3.5f) / 100);
		} else if (PlayerParameters._instance.myPlayerParameter.membership_no == 30) {
			AfterDiscount = Amount - Convert.ToInt32((Amount * 5) / 100);
		} else {
			return Amount;
		}
		return AfterDiscount;
	}
	public void yesButtonDraw_VIP()
	{
		//Shivam
		cardrarity = "";            //flush cardRarityString Object
		gettingcards_prob ();		//Check probability
		Debug.Log (cardrarity);
		isDrawActive=false;
		WWWForm wwwFowm = new WWWForm ();
		wwwFowm.AddField ("tag" , "userCardsPlayercards");
		wwwFowm.AddField ("user_id" , PlayerDataParse._instance.playersParam.userId);
		wwwFowm.AddField ("device_id" , SystemInfo.deviceUniqueIdentifier);
		wwwFowm.AddField ("rarity" , cardrarity);
		wwwFowm.AddField ("no_of_cards" , no_of_cards);
		string isCaptiveToSend = "";
		for(int i = 0 ; i < cardsToSaveInMyCards ; i++)
		{
			isCaptiveToSend+="0,";
		}
		for(int i = 0 ; i < cardsToSaveInStash ; i++)
		{
			isCaptiveToSend+="2,";
		}
		isCaptiveToSend = isCaptiveToSend.Remove (isCaptiveToSend.Length-1);
		wwwFowm.AddField ("is_captive" , isCaptiveToSend);
		Debug.Log ("isCaptiveToSend = "+isCaptiveToSend);
		WWW www = new WWW (loadingScene.Instance.baseUrl ,wwwFowm );

		StartCoroutine(WaitForRequest(www ));

		//Shivam end
		//IDictionary jsonDic = (IDictionary)Json.Deserialize (www.text);
		//http://ec2-54-174-178-121.compute-1.amazonaws.com/mod_web_active_api/index.php?tag=userCardsPlayercards&user_id=dF0qq5onkfk=&device_id=05cfb193ff36e84197b2778caed1d47f&rarity=Mega&no_of_cards=2
		int dra = UnityEngine.Random.Range(0,drawSystem.Length);
		for(int i=0;i<drawSystem.Length;i++)
		{
			if(i==dra)
			{

				drawSystem[i].SetActive(true);
			}
			else
			{
				drawSystem[i].SetActive(false);

			}
		}

		PurchaseExitDraw();


	}

	public void yesButtonDraw()
	{
		//Shivam
		cardrarity = "";            //flush cardRarityString Object
		gettingcards_prob ();		//Check probability
		Debug.Log (cardrarity);
		isDrawActive=false;
		if (BuyUsingGems) {
			if (no_of_cards == 1) {
				PlayerParameters._instance.myPlayerParameter.gems -= Discount(300);
				PlayerParameters._instance.myPlayerParameter.dragon_coins += 10;
				UpdateOnServer ();
			} else if (no_of_cards == 5) {
				PlayerParameters._instance.myPlayerParameter.gems -= Discount(1400);
				PlayerParameters._instance.myPlayerParameter.dragon_coins += 30;
				UpdateOnServer ();
			} else if (no_of_cards == 9) {
				PlayerParameters._instance.myPlayerParameter.gems -= Discount(2500);
				PlayerParameters._instance.myPlayerParameter.dragon_coins += 70;
				UpdateOnServer ();
			} else if (no_of_cards == 20) {
				PlayerParameters._instance.myPlayerParameter.gems -= Discount(5000);
				PlayerParameters._instance.myPlayerParameter.dragon_coins += 150;
				UpdateOnServer ();
			} else {
			}
		} else {
			if (no_of_cards == 1) {
				PlayerParameters._instance.myPlayerParameter.dragon_coins -= Discount(100);
				UpdateOnServer ();
			} else if (no_of_cards == 5) {
				
				PlayerParameters._instance.myPlayerParameter.dragon_coins -= Discount(300);
				UpdateOnServer ();
			} else if (no_of_cards == 9) {
				
				PlayerParameters._instance.myPlayerParameter.dragon_coins -= Discount(700);
				UpdateOnServer ();
			} else if (no_of_cards == 20) {
				
				PlayerParameters._instance.myPlayerParameter.dragon_coins -= Discount(1500);
				UpdateOnServer ();
			} else {
			}
		}
		WWWForm wwwFowm = new WWWForm ();
		wwwFowm.AddField ("tag" , "userCardsPlayercards");
		wwwFowm.AddField ("user_id" , PlayerDataParse._instance.playersParam.userId);
		wwwFowm.AddField ("device_id" , SystemInfo.deviceUniqueIdentifier);
		wwwFowm.AddField ("rarity" , cardrarity);
		wwwFowm.AddField ("no_of_cards" , no_of_cards);
		string isCaptiveToSend = "";
		for(int i = 0 ; i < cardsToSaveInMyCards ; i++)
		{
			isCaptiveToSend+="0,";
		}
		for(int i = 0 ; i < cardsToSaveInStash ; i++)
		{
			isCaptiveToSend+="2,";
		}
		isCaptiveToSend = isCaptiveToSend.Remove (isCaptiveToSend.Length-1);
		wwwFowm.AddField ("is_captive" , isCaptiveToSend);
		Debug.Log ("isCaptiveToSend = "+isCaptiveToSend);
		WWW www = new WWW (loadingScene.Instance.baseUrl ,wwwFowm );

		StartCoroutine(WaitForRequest(www ));

		//Shivam end
		//IDictionary jsonDic = (IDictionary)Json.Deserialize (www.text);
		//http://ec2-54-174-178-121.compute-1.amazonaws.com/mod_web_active_api/index.php?tag=userCardsPlayercards&user_id=dF0qq5onkfk=&device_id=05cfb193ff36e84197b2778caed1d47f&rarity=Mega&no_of_cards=2
		int dra = UnityEngine.Random.Range(0,drawSystem.Length);
		for(int i=0;i<drawSystem.Length;i++)
		{
			if(i==dra)
			{

			drawSystem[i].SetActive(true);
			}
			else
			{
			drawSystem[i].SetActive(false);

			}
		}

		PurchaseExitDraw();
		
		
	}
	//Shivam
	void gettingcards_prob()
	{
		for (int temp = 1; temp <= no_of_cards; temp++) {
			int prob = UnityEngine.Random.Range (0, 100);
			if (prob > 98) {
				//Rank 5*
				if (temp == 1) {
					cardrarity = cardrarity + "Legendary";
				} else {
					cardrarity = cardrarity + ",Legendary";
				}
			} else if (prob > 93 && prob < 98) {
				//Rank 4*
				if (temp == 1) {
					cardrarity = cardrarity + "Mega";
				} else {
					cardrarity = cardrarity + ",Mega";
				}
			} else if (prob > 78 && prob < 94) {
				//Rank 3*
				if (temp == 1) {
					cardrarity = cardrarity + "Super";
				} else {
					cardrarity = cardrarity + ",Super";
				}
			} else if (prob > 45 && prob < 79) {
				//Rank 2*
				if (temp == 1) {
					cardrarity = cardrarity + "Uncommon";
				} else {
					cardrarity = cardrarity + ",Uncommon";
				}
			} else {
				//Rank 1*
				if (temp == 1) {
					cardrarity = cardrarity + "Common";
				} else {
					cardrarity = cardrarity + ",Common";
				}
			}
		}

	}


	//Shivam end
	public void exitFinished()
	{

		finishedPurchased.SetActive(false);
		PurchaseFailedPanel.SetActive (false);
		PurchaseSuccessful.SetActive (false);
	}

	public void yesButtonGems()
	{
		purchaseGems.SetActive(false);
		finishedGemsPurchased.SetActive(true);
	}
	public void exitGemsFinish()
	{

		finishedGemsPurchased.SetActive(false);
	}



	public void purchaseClickGems(int no_of_gems)
	{
		purchaseGems.SetActive(true);

		No_Of_Gems = no_of_gems;
		if (No_Of_Gems == 100) {
			GemsItemText.text = "100 Gems";
			GemCostText.text = "$ 0.99";
			//SmartIAP.Instance ().Purchase ("com.myriadofdragons100gems");
		} else if (No_Of_Gems == 280) {
			GemsItemText.text = "280 Gems";
			GemCostText.text = "$ 2.99";
			//SmartIAP.Instance ().Purchase ("com.myriadofdragons280gems");
		} else if (No_Of_Gems == 570) {
			GemsItemText.text = "570 Gems";
			GemCostText.text = "$ 5.99";
			//SmartIAP.Instance ().Purchase ("com.myriadofdragons570gems");
		} else if (No_Of_Gems == 1250) {
			GemsItemText.text = "1250 Gems";
			GemCostText.text = "$ 12.99";
			//SmartIAP.Instance ().Purchase ("com.myriadofdragons1250gems");
		} else if (No_Of_Gems == 2450) {
			GemsItemText.text = "2450 Gems";
			GemCostText.text = "$ 24.99";
			//SmartIAP.Instance ().Purchase ("com.myriadofdragons2450gems");
		} else if (No_Of_Gems == 5500) {
			GemsItemText.text = "5500 Gems";
			GemCostText.text = "$ 49.99";
			//SmartIAP.Instance ().Purchase ("com.myriadofdragons5500gems");
		} else if (No_Of_Gems == 12000) {
			GemsItemText.text = "12000 Gems";
			GemCostText.text = "$ 99.99";
			//SmartIAP.Instance ().Purchase ("com.myriadofdragons12000gems");
		} else {
		}
	}
	public void purchase_yesClickGems()
	{
		if (No_Of_Gems == 100) {
			if (Debug.isDebugBuild) {
				SmartIAP.Instance ().Purchase ("android.test.purchased");
			}else{
				SmartIAP.Instance ().Purchase ("com.myriadofdragons100gems");
			}
		} else if (No_Of_Gems == 280) {
			if (Debug.isDebugBuild) {
				SmartIAP.Instance ().Purchase ("android.test.purchased");
			} else {
				SmartIAP.Instance ().Purchase ("com.myriadofdragons280gems");
			}
		} else if (No_Of_Gems == 570) {
			if (Debug.isDebugBuild) {
				SmartIAP.Instance ().Purchase ("android.test.purchased");
			} else {
				SmartIAP.Instance ().Purchase ("com.myriadofdragons570gems");
			}
		} else if (No_Of_Gems == 1250) {
			if (Debug.isDebugBuild) {
				SmartIAP.Instance ().Purchase ("android.test.purchased");
			} else {
				SmartIAP.Instance ().Purchase ("com.myriadofdragons1250gems");
			}
		} else if (No_Of_Gems == 2450) {
			if (Debug.isDebugBuild) {
				SmartIAP.Instance ().Purchase ("android.test.purchased");
			} else {
				SmartIAP.Instance ().Purchase ("com.myriadofdragons2450gems");
			}
		} else if (No_Of_Gems == 5500) {
			if (Debug.isDebugBuild) {
				SmartIAP.Instance ().Purchase ("android.test.purchased");
			} else {
				SmartIAP.Instance ().Purchase ("com.myriadofdragons5500gems");
			}
		} else if (No_Of_Gems == 12000) {
			if (Debug.isDebugBuild) {
				SmartIAP.Instance ().Purchase ("android.test.purchased");
			} else {
				SmartIAP.Instance ().Purchase ("com.myriadofdragons12000gems");
			}
		} else {
		}
	}
	//Shivam
	public void CheckCardLimit_Castle()
	{
		MaxCardLimit = EmpireManager._instance.castle.finalValue1[EmpireManager._instance.castle.currentLevel];
	}

	public void purchaseCard_VIPmembership()
	{
		CheckCardLimit_Castle ();
		CardLimitCheck (no_of_cards);
//		if(authorizePurchase)
			drawPurchase.SetActive (true);
	}

	int cardsToSaveInMyCards;
	int cardsToSaveInStash;
	public void CardLimitCheck(int cardCount)
	{
		if ((CardsManager._instance.mycards.Count + cardCount) <= MaxCardLimit) {
			cardsToSaveInMyCards = cardCount;
			cardsToSaveInStash = 0;
			authorizePurchase = true;
		} else {
			cardsToSaveInMyCards = MaxCardLimit - CardsManager._instance.mycards.Count;
			cardsToSaveInStash = cardCount - cardsToSaveInMyCards;
			authorizePurchase = false;
		}




	}
	public void purchasePackage1ClickDraw(bool usingGems)
	{
//		isDrawActive=true;
		CardItemText.text="1 Card Package";
		BuyUsingGems=usingGems;
		CheckCardLimit_Castle ();
		if (usingGems) {
			if (PlayerParameters._instance.myPlayerParameter.gems >= Discount(300)) {
				no_of_cards = 1;
				CardLimitCheck (no_of_cards);
//				if (authorizePurchase) {
					if (retry == false)
						drawPurchase.SetActive (true);
					else {
						retry = false;
						yesButtonDraw ();
					}
					costText_Draw.text = "Cost: "+Discount(300)+" Gems";
//				} else {
//					PurchaseFailedPanel.SetActive (true);
//					FailedPanel_Text.text = "Exceed Card Limit !! First upgrade your castle or try to purchase another package";
//				}
			}
				 else {
					PurchaseFailedPanel.SetActive (true);
					FailedPanel_Text.text = "Sorry you don't have enough gems to make this purchase";
				}
			
		}else {
			if (PlayerParameters._instance.myPlayerParameter.dragon_coins >= Discount(1000)) {//		isDrawActive=true;
				no_of_cards = 1;
				CardLimitCheck (no_of_cards);
//				if (authorizePurchase) {
					if (retry == false)
						drawPurchase.SetActive (true);
					else {
						retry = false;
						yesButtonDraw ();
					}
					costText_Draw.text = "Cost: "+Discount(1000)+" Loyalty points";
//				} else {
//					PurchaseFailedPanel.SetActive (true);
//					FailedPanel_Text.text = "Exceed Card Limit !! First upgrade your castle or try to purchase another package";
//				}
			}
			else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough loyalty points to make this purchase";
			}
		}

	}
	public void purchasePackage2ClickDraw(bool usingGems)
	{
		BuyUsingGems=usingGems;
		CardItemText.text="5 Cards Package";
		CheckCardLimit_Castle ();
		if (usingGems) {
			if (PlayerParameters._instance.myPlayerParameter.gems >= Discount(1400)) {//		isDrawActive=true;
				no_of_cards = 5;
				CardLimitCheck (no_of_cards);
//				if (authorizePurchase) {
					if (retry == false)
						drawPurchase.SetActive (true);
					else {
						retry = false;
						yesButtonDraw ();
					}
					costText_Draw.text = "Cost: "+Discount(1400)+" Gems";
//				} else {
//					PurchaseFailedPanel.SetActive (true);
//					FailedPanel_Text.text = "Exceed Card Limit !! First upgrade your castle or try to purchase another package";
//				}
			}
			else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough gems to make this purchase";
			}
		} else {
			if (PlayerParameters._instance.myPlayerParameter.dragon_coins >= Discount(3000)) {//		isDrawActive=true;
				no_of_cards = 5;
				CardLimitCheck (no_of_cards);
//				if (authorizePurchase) {
					if (retry == false)
						drawPurchase.SetActive (true);
					else {
						retry = false;
						yesButtonDraw ();
					}
					costText_Draw.text = "Cost: "+Discount(3000)+" Loyality points";
//				} else {
//					PurchaseFailedPanel.SetActive (true);
//					FailedPanel_Text.text = "Exceed Card Limit !! First upgrade your castle or try to purchase another package";
//				}
			}
			else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough loyalty points to make this purchase";
			}

		}

	}
	public void purchasePackage3ClickDraw(bool usingGems)
	{
		CardItemText.text="9 Cards Package";
		BuyUsingGems=usingGems;
		CheckCardLimit_Castle ();
		//		isDrawActive=true;
		if (usingGems) {
			if (PlayerParameters._instance.myPlayerParameter.gems >= Discount(2500)) {
				no_of_cards = 9;
				CardLimitCheck (no_of_cards);
//				if (authorizePurchase) {
					if (retry == false)
						drawPurchase.SetActive (true);
					else {
						retry = false;
						yesButtonDraw ();
					}
					costText_Draw.text = "Cost: "+Discount(2500)+" Gems";
//				} else {
//					PurchaseFailedPanel.SetActive (true);
//					FailedPanel_Text.text = "Exceed Card Limit !! First upgrade your castle or try to purchase another package";
//				}
			}
			else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough gems to make this purchase";
			}
		} else {
			if (PlayerParameters._instance.myPlayerParameter.dragon_coins >= Discount(7000)) {//		isDrawActive=true;
				no_of_cards = 9;
				CardLimitCheck (no_of_cards);
//				if (authorizePurchase) {
					if (retry == false)
						drawPurchase.SetActive (true);
					else {
						retry = false;
						yesButtonDraw ();
					}
					costText_Draw.text = "Cost: "+Discount(7000)+" Loyality points";
//				} else {
//					PurchaseFailedPanel.SetActive (true);
//					FailedPanel_Text.text = "Exceed Card Limit !! First upgrade your castle or try to purchase another package";
//				}
			}
			else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough loyalty points to make this purchase";
			}
		}

	}
	public void purchasePackage4ClickDraw(bool usingGems)
	{
		CardItemText.text="20 Cards Package";
		BuyUsingGems=usingGems;
		CheckCardLimit_Castle ();
		//		isDrawActive=true;
		if (usingGems) {
			if (PlayerParameters._instance.myPlayerParameter.gems >= Discount(5000)) {
				no_of_cards = 20;
				CardLimitCheck (no_of_cards);
//				if (authorizePurchase) {
					if (retry == false)
						drawPurchase.SetActive (true);
					else {
						retry = false;
						yesButtonDraw ();
					}
					costText_Draw.text = "Cost: "+Discount(5000)+" Gems";
//				} else {
//					PurchaseFailedPanel.SetActive (true);
//					FailedPanel_Text.text = "Exceed Card Limit !! First upgrade your castle or try to purchase another package";
//				}
			}
			else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough gems to make this purchase";
			}
		} else {
			if (PlayerParameters._instance.myPlayerParameter.dragon_coins >= Discount(15000)) {//		isDrawActive=true;
				no_of_cards = 20;
				CardLimitCheck (no_of_cards);
//				if (authorizePurchase) {
					if (retry == false)
						drawPurchase.SetActive (true);
					else {
						retry = false;
						yesButtonDraw ();
					}
					costText_Draw.text = "Cost: "+Discount(15000)+" Loyalty points";
//				} else {
//					PurchaseFailedPanel.SetActive (true);
//					FailedPanel_Text.text = "Exceed Card Limit !! First upgrade your castle or try to purchase another package";
//				}
			}
			else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough loyalty points to make this purchase";
			}
		}

	}
	public void VIP_7_purchaseClick(bool usingGems)
	{
		Debug.Log ("Left DCoins" + PlayerParameters._instance.myPlayerParameter.dragon_coins);
		BuyUsingGems = usingGems;
		MembershipItemText.text = "Weeklong Membership";

		if (usingGems) {
			if (PlayerParameters._instance.myPlayerParameter.gems >= 800) {
			
				purchaseSystem.SetActive (true);
				MembershipCostText.text = "Cost: 800 Gems";
				Vip_Membership_Days = 7;
			} else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough gems to make this purchase";
			}
		} else {
			if (PlayerParameters._instance.myPlayerParameter.dragon_coins >= 5000) {

				purchaseSystem.SetActive (true);
				MembershipCostText.text = "Cost: 5000 Loyalty points";
				Vip_Membership_Days = 7;
			}
			else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough loyalty points to make this purchase";
			}
		}


	}
	public void VIP_14_purchaseClick(bool usingGems)
	{
		MembershipItemText.text = "Fortnight Membership";
		Debug.Log ("Left DCoins" + PlayerParameters._instance.myPlayerParameter.dragon_coins);
		BuyUsingGems = usingGems;
		if (usingGems) {
			if (PlayerParameters._instance.myPlayerParameter.gems >= 1500) {
				purchaseSystem.SetActive (true);
				MembershipCostText.text = "Cost: 1500 Gems";
				Vip_Membership_Days = 14;
			}
			else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough gems to make this purchase";
			}
		} else {
			if (PlayerParameters._instance.myPlayerParameter.dragon_coins >= 9500) {
				purchaseSystem.SetActive (true);
				MembershipCostText.text = "Cost: 9500 Loyalty points";
				Vip_Membership_Days = 14;
			}
			else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough loyalty points to make this purchase";
			}

		}


	}
	public void VIP_30_purchaseClick(bool usingGems)
	{
		MembershipItemText.text = "Month Membership";
		Debug.Log ("Left DCoins" + PlayerParameters._instance.myPlayerParameter.dragon_coins);
		BuyUsingGems = usingGems;
		if (usingGems) {
			if (PlayerParameters._instance.myPlayerParameter.gems >= 3000) {
				purchaseSystem.SetActive (true);
				MembershipCostText.text = "Cost: 3000 Gems";
				Vip_Membership_Days = 30;
			}
			else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough gems to make this purchase";
			}
		} else {
			if (PlayerParameters._instance.myPlayerParameter.dragon_coins >= 18000) {
				purchaseSystem.SetActive (true);
				MembershipCostText.text = "Cost: 18000 Loyalty points";
				Vip_Membership_Days = 30;
			}
			else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough loyalty points to make this purchase";
			}
		}

	}

	//Shivam end
//	}
	public void purchaseExit()
	{

		purchaseSystem.SetActive(false);
	}
	public void PurchaseExitDraw()
	{
//
//
		drawPurchase.SetActive(false);
	}


	public void purchaseGemsExit()
	{

		purchaseGems.SetActive(false);
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
	public void detailTabClick()
	{
		detailTab.SetActive(true);

	}
	public void memberShipDetail()
	{
		membershipTab.SetActive(true);
	}
	public void exitMembershipTab()
	{
		membershipTab.SetActive(false);

	}
	public void detailTabExit()
	{

		detailTab.SetActive(false);
		
	}
	public void battle()
	{
		Start();

		PlayerPrefs.SetString("shopScene","yes");
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

	public void inventory()
	{
		Start();

		PlayerPrefs.SetString("shopScene","yes");
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
	public void cardCollections()
	{
		Start();

		PlayerPrefs.SetString("shopScene","yes");
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

		//Application.LoadLevel("Battle_Layout4");
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Shivam

		if (PlayerParameters._instance.myPlayerParameter.membership_no == 0) {
			VIP_Button_Interactable ();
		}
		if (MembershipRewardManager._instance.daysLeft >= 7 && PlayerParameters._instance.myPlayerParameter.membership_no==7) {
			newShop._instance.Vip_Membership_7 = false;
			PlayerParameters._instance.myPlayerParameter.membership_no = 0;
		} else if (MembershipRewardManager._instance.daysLeft >= 14 && PlayerParameters._instance.myPlayerParameter.membership_no==14) {
			newShop._instance.Vip_Membership_14 = false;
			PlayerParameters._instance.myPlayerParameter.membership_no = 0;
		} else if (MembershipRewardManager._instance.daysLeft >= 30 && PlayerParameters._instance.myPlayerParameter.membership_no==30) {
			newShop._instance.Vip_Membership_30 = false;
			PlayerParameters._instance.myPlayerParameter.membership_no = 0;
		} else {
		}

		DragonCoin_Text.text = "" + PlayerParameters._instance.myPlayerParameter.dragon_coins;
		DragonEgg_Text.text = "" + PlayerParameters._instance.myPlayerParameter.dragon_eggs;
		Gems_Text.text = "" + PlayerParameters._instance.myPlayerParameter.gems;
		// Shivam end
		//--------- lKAHBIR-------
		float one;
		Vector3 two;
		two = multiCard2.transform.position;
		one = drawCardMulti.transform.position.y;
		two.y = one;
		multiCard2.transform.position = two;

		float three;
		Vector3 four;
		four = multiCard3.transform.position;
		three = drawCardMulti.transform.position.y;
		four.y = three;
		multiCard3.transform.position = four;
		//--------- lKAHBIR-------





	}
	public void UpdatePotion()
	{
		for (int i = 0; i <= APGtext.Length - 1; i++) {
			APGtext [i].text = "Owned " + PlayerParameters._instance.myPlayerParameter.attack_potion;
			SPGtext [i].text = "Owned " + PlayerParameters._instance.myPlayerParameter.stamina_potion;
			APDtext [i].text = "Owned " + PlayerParameters._instance.myPlayerParameter.attack_potion;
			SPDtext [i].text = "Owned " + PlayerParameters._instance.myPlayerParameter.stamina_potion;
		}
	}
	public void VIP_Button_Interactable()
	{
		if (PlayerParameters._instance.myPlayerParameter.membership_no == 7 || PlayerParameters._instance.myPlayerParameter.membership_no == 14 || PlayerParameters._instance.myPlayerParameter.membership_no == 30) {
			MP_Button7.interactable = false;
			MP_Button14.interactable = false;
			MP_Button30.interactable = false;
			MP_DC_Button7.interactable = false;
			MP_DC_Button14.interactable = false;
			MP_DC_Button30.interactable = false;
		} else {
			MP_Button7.interactable = true;
			MP_Button14.interactable = true;
			MP_Button30.interactable = true;
			MP_DC_Button7.interactable = true;
			MP_DC_Button14.interactable = true;
			MP_DC_Button30.interactable = true;
		}
	}
	public void backButton()
	{
		
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
//		if(PlayerPrefs.GetString("inventoryScene")=="yes")
//		{
//			loadingScene.Instance.inventory();
//
//			//Application.LoadLevel("inventoryScene");
//			PlayerPrefs.SetString("inventoryScene","no");
//		}
//		
//		if(PlayerPrefs.GetString("battleLyout")=="yes")
//		{
//			loadingScene.Instance.battle1();
//
//		//	Application.LoadLevel("Battle_Layout");
//			PlayerPrefs.SetString("battleLyout","no");
//			
//		}
//		if(PlayerPrefs.GetString("Battle_Layout4")=="yes")
//		{
//			loadingScene.Instance.battle4();
//
//			//Application.LoadLevel("Battle_Layout4");
//			PlayerPrefs.SetString("Battle_Layout4","no");
//			
//		}
//		if(PlayerPrefs.GetString("newMain")=="yes")
//		{
//		//	Destroy(GameObject.Find("shop(Clone)"));
//			loadingScene.Instance.main();
//			//Application.LoadLevel("menuNew");
//			PlayerPrefs.SetString("newMain","no");
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
//
//			//Application.LoadLevel("trade");
//			PlayerPrefs.SetString("trade","no");
//			
//		}
//		if(PlayerPrefs.GetString("community")=="yes")
//		{
//			loadingScene.Instance.community();
//
//		//	Application.LoadLevel("community");
//			PlayerPrefs.SetString("community","no");
//			
//			
//		}
//		if(PlayerPrefs.GetString("chatScene")=="yes")
//		{
//			loadingScene.Instance.chat();
//
//			//Application.LoadLevel("chatScene");
//			PlayerPrefs.SetString("chatScene","no");
//			
//			
//		}
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

			//	loadingScene.Instance.scenes[i].SetActive(true);
				
				
			}
			
		}

		
	}
	//Shivam
	public void AttackPotionPack_GemPurchase(int total_item)
	{
		
		if (total_item == 1) {
			if (PlayerParameters._instance.myPlayerParameter.gems >= Discount(100) ) {
				PlayerParameters._instance.myPlayerParameter.gems -= Discount(100);
				PlayerParameters._instance.myPlayerParameter.dragon_coins += 5;
				PlayerParameters._instance.myPlayerParameter.attack_potion += total_item;
				UpdateOnServer ();
				PurchaseSuccessful.SetActive (true);
			}
			else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough gems to make this purchase";
			}
		} else if (total_item == 10) {
			if (PlayerParameters._instance.myPlayerParameter.gems >= Discount(900) ) {
				PlayerParameters._instance.myPlayerParameter.gems -= Discount(900);
				PlayerParameters._instance.myPlayerParameter.dragon_coins += 55;
				PlayerParameters._instance.myPlayerParameter.attack_potion += total_item;
				UpdateOnServer ();
				PurchaseSuccessful.SetActive (true);
			}else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough gems to make this purchase";
			}
		} else if (total_item == 50) {
			if (PlayerParameters._instance.myPlayerParameter.gems >= Discount(4000) ) {
				PlayerParameters._instance.myPlayerParameter.gems -= Discount(4000);
				PlayerParameters._instance.myPlayerParameter.dragon_coins += 280;
				PlayerParameters._instance.myPlayerParameter.attack_potion += total_item;
				UpdateOnServer ();
				PurchaseSuccessful.SetActive (true);
			}
			else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough gems to make this purchase";
			}
		} else if (total_item == 100) {
			if (PlayerParameters._instance.myPlayerParameter.gems >= Discount(7000) ) {
				PlayerParameters._instance.myPlayerParameter.gems -= Discount(7000);
				PlayerParameters._instance.myPlayerParameter.dragon_coins += 570;
				PlayerParameters._instance.myPlayerParameter.attack_potion += total_item;
				UpdateOnServer ();
				PurchaseSuccessful.SetActive (true);
			}
			else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough gems to make this purchase";
			}
		} else if (total_item == 300) {
			if (PlayerParameters._instance.myPlayerParameter.gems >= Discount(15000) ) {
				PlayerParameters._instance.myPlayerParameter.gems -= Discount(15000);
				PlayerParameters._instance.myPlayerParameter.dragon_coins += 1740;
				PlayerParameters._instance.myPlayerParameter.attack_potion += total_item;
				UpdateOnServer ();
				PurchaseSuccessful.SetActive (true);
			}
			else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough gems to make this purchase";
			}
		} else if (total_item == 500) {
			if (PlayerParameters._instance.myPlayerParameter.gems >= Discount(20000) ) {
				PlayerParameters._instance.myPlayerParameter.gems -= Discount(20000);
				PlayerParameters._instance.myPlayerParameter.dragon_coins += 3000;
				PlayerParameters._instance.myPlayerParameter.attack_potion += total_item;
				UpdateOnServer ();
				PurchaseSuccessful.SetActive (true);
			}
			else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough gems to make this purchase";
			}
		} else {
		}
		UpdatePotion ();
	}
	public void StaminaPotionPack_GemPurchase(int total_item)
	{
		
		//no_of_items= 
		if (total_item == 1) {
			if (PlayerParameters._instance.myPlayerParameter.gems >= Discount(100) ) {
				PlayerParameters._instance.myPlayerParameter.gems -= Discount(100);
				PlayerParameters._instance.myPlayerParameter.dragon_coins += 5;
				PlayerParameters._instance.myPlayerParameter.stamina_potion += total_item;
				UpdateOnServer ();
				PurchaseSuccessful.SetActive (true);
			}

			else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough gems to make this purchase";
			}
		} else if (total_item == 10) {
			if (PlayerParameters._instance.myPlayerParameter.gems >= Discount(900) ) {
				PlayerParameters._instance.myPlayerParameter.gems -= Discount(900);
				PlayerParameters._instance.myPlayerParameter.dragon_coins += 55;
				PlayerParameters._instance.myPlayerParameter.stamina_potion += total_item;
				UpdateOnServer ();
				PurchaseSuccessful.SetActive (true);
			}else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough gems to make this purchase";
			}
		} else if (total_item == 50) {
			if (PlayerParameters._instance.myPlayerParameter.gems >= Discount(4000) ) {
				PlayerParameters._instance.myPlayerParameter.gems -= Discount(4000);
				PlayerParameters._instance.myPlayerParameter.dragon_coins += 280;
				PlayerParameters._instance.myPlayerParameter.stamina_potion += total_item;
				UpdateOnServer ();
				PurchaseSuccessful.SetActive (true);
			}
			else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough gems to make this purchase";
			}
		} else if (total_item == 100) {
			if (PlayerParameters._instance.myPlayerParameter.gems >= Discount(7000) ) {
				PlayerParameters._instance.myPlayerParameter.gems -= Discount(7000);
				PlayerParameters._instance.myPlayerParameter.dragon_coins += 570;
				PlayerParameters._instance.myPlayerParameter.stamina_potion += total_item;
				UpdateOnServer ();
				PurchaseSuccessful.SetActive (true);
			}
			else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough gems to make this purchase";
			}
		} else if (total_item == 300) {
			if (PlayerParameters._instance.myPlayerParameter.gems >= Discount(15000) ) {
				PlayerParameters._instance.myPlayerParameter.gems -= Discount(15000);
				PlayerParameters._instance.myPlayerParameter.dragon_coins += 1740;
				PlayerParameters._instance.myPlayerParameter.stamina_potion += total_item;
				UpdateOnServer ();
				PurchaseSuccessful.SetActive (true);
			}
			else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough gems to make this purchase";
			}
		} else if (total_item == 500) {
			if (PlayerParameters._instance.myPlayerParameter.gems >= Discount(20000) ) {
				PlayerParameters._instance.myPlayerParameter.gems -= Discount(20000);
				PlayerParameters._instance.myPlayerParameter.dragon_coins += 3000;
				PlayerParameters._instance.myPlayerParameter.stamina_potion += total_item;
				UpdateOnServer ();
				PurchaseSuccessful.SetActive (true);
			}
			else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough gems to make this purchase";
			}
		} else {
		}
		UpdatePotion ();
	}


	public void AttackPotionPack_DCPurchase(int total_item)
	{
		
		if (total_item == 1) {
			if(PlayerParameters._instance.myPlayerParameter.dragon_coins>=Discount(50*5))
			{
				PlayerParameters._instance.myPlayerParameter.dragon_coins -= Discount(50*5);
				PlayerParameters._instance.myPlayerParameter.attack_potion += total_item;
				UpdateOnServer ();
				PurchaseSuccessful.SetActive (true);
			}
			else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough loyalty points to make this purchase";
			}
		} else if (total_item == 10) {
			if (PlayerParameters._instance.myPlayerParameter.dragon_coins >= Discount(450*5)) {
				PlayerParameters._instance.myPlayerParameter.dragon_coins -= Discount(450*5);
				PlayerParameters._instance.myPlayerParameter.attack_potion += total_item;
				UpdateOnServer ();
				PurchaseSuccessful.SetActive (true);
			} else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough loyalty points to make this purchase";
			}
		} else if (total_item == 50) {
			if (PlayerParameters._instance.myPlayerParameter.dragon_coins >= Discount(2200*5)) {
				PlayerParameters._instance.myPlayerParameter.dragon_coins -= Discount(2200*5);
				PlayerParameters._instance.myPlayerParameter.attack_potion += total_item;
				UpdateOnServer ();
				PurchaseSuccessful.SetActive (true);
			} else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough loyalty points to make this purchase";
			}
		} else if (total_item == 100) {
			if (PlayerParameters._instance.myPlayerParameter.dragon_coins >= Discount(4300*5)) {
				PlayerParameters._instance.myPlayerParameter.dragon_coins -= Discount(4300*5);
				PlayerParameters._instance.myPlayerParameter.attack_potion += total_item;
				UpdateOnServer ();
				PurchaseSuccessful.SetActive (true);
			} else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough loyalty points to make this purchase";
			}
		} else if (total_item == 300) {
			if (PlayerParameters._instance.myPlayerParameter.dragon_coins >= Discount(12900*5)) {
				PlayerParameters._instance.myPlayerParameter.dragon_coins -= Discount(12900*5);
				PlayerParameters._instance.myPlayerParameter.attack_potion += total_item;
				UpdateOnServer ();
				PurchaseSuccessful.SetActive (true);
			} else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough loyalty points to make this purchase";
			}
		} else if (total_item == 500) {
			if (PlayerParameters._instance.myPlayerParameter.dragon_coins >= Discount(20000*5)) {
				PlayerParameters._instance.myPlayerParameter.dragon_coins -= Discount(20000*5);
				PlayerParameters._instance.myPlayerParameter.attack_potion += total_item;
				UpdateOnServer ();
				PurchaseSuccessful.SetActive (true);
			} else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough loyalty points to make this purchase";
			}
		} else {
		}
		UpdatePotion ();
	}
	public void StaminaPotionPack_DCPurchase(int total_item)
	{
		
		//no_of_items= 
		if (total_item == 1) {
			if(PlayerParameters._instance.myPlayerParameter.dragon_coins>=Discount(50*5))
			{
				PlayerParameters._instance.myPlayerParameter.dragon_coins -= Discount(50*5);
				PlayerParameters._instance.myPlayerParameter.stamina_potion += total_item;
				UpdateOnServer ();
				PurchaseSuccessful.SetActive (true);
			}
			else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough loyalty points to make this purchase";
			}
		} else if (total_item == 10) {
			if (PlayerParameters._instance.myPlayerParameter.dragon_coins >= Discount(450*5)) {
				PlayerParameters._instance.myPlayerParameter.dragon_coins -= Discount(450*5);
				PlayerParameters._instance.myPlayerParameter.stamina_potion += total_item;
				UpdateOnServer ();
				PurchaseSuccessful.SetActive (true);
			} else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough loyalty points to make this purchase";
			}
		} else if (total_item == 50) {
			if (PlayerParameters._instance.myPlayerParameter.dragon_coins >= Discount(2200*5)) {
				PlayerParameters._instance.myPlayerParameter.dragon_coins -= Discount(2200*5);
				PlayerParameters._instance.myPlayerParameter.stamina_potion += total_item;
				UpdateOnServer ();
				PurchaseSuccessful.SetActive (true);
			} else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough loyalty points to make this purchase";
			}
		} else if (total_item == 100) {
			if (PlayerParameters._instance.myPlayerParameter.dragon_coins >=Discount(4300*5)) {
				PlayerParameters._instance.myPlayerParameter.dragon_coins -= Discount(4300*5);
				PlayerParameters._instance.myPlayerParameter.stamina_potion += total_item;
				UpdateOnServer ();
				PurchaseSuccessful.SetActive (true);
			} else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough loyalty points to make this purchase";
			}
		} else if (total_item == 300) {
			if (PlayerParameters._instance.myPlayerParameter.dragon_coins >= Discount(12900*5)) {
				PlayerParameters._instance.myPlayerParameter.dragon_coins -= Discount(12900*5);
				PlayerParameters._instance.myPlayerParameter.stamina_potion += total_item;
				UpdateOnServer ();
				PurchaseSuccessful.SetActive (true);
			} else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough loyalty points to make this purchase";
			}
		} else if (total_item == 500) {
			if (PlayerParameters._instance.myPlayerParameter.dragon_coins >= Discount(20000*5)) {
				PlayerParameters._instance.myPlayerParameter.dragon_coins -= Discount(20000*5);
				PlayerParameters._instance.myPlayerParameter.stamina_potion += total_item;
				UpdateOnServer ();
				PurchaseSuccessful.SetActive (true);
			} else {
				PurchaseFailedPanel.SetActive (true);
				FailedPanel_Text.text="Sorry you don't have enough loyalty points to make this purchase";
			}
		} else {
		}
		UpdatePotion ();
	}


	void UpdateOnServer()
	{
		Dictionary<string, string> avatarParameters = new Dictionary<string, string> ();
		avatarParameters.Add ("gems" ,PlayerParameters._instance.myPlayerParameter.gems.ToString ());
		avatarParameters.Add ("attack_potion" ,PlayerParameters._instance.myPlayerParameter.attack_potion.ToString ());
		avatarParameters.Add ("stamina_potion" ,PlayerParameters._instance.myPlayerParameter.stamina_potion.ToString ());
		avatarParameters.Add ("dragon_coins" ,PlayerParameters._instance.myPlayerParameter.dragon_coins.ToString ());
		StartCoroutine (PlayerParameters._instance.SendPlayerParameters (avatarParameters,null ));
	}
	//shivam end
	public void community()
	{
		Start();

		PlayerPrefs.SetString("shopScene","yes");
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
		//Application.LoadLevel("community");
		loadingScene.Instance.community();


	}
	public void trade()
	{
		Start();

		PlayerPrefs.SetString("shopScene","yes");
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
	public void meneuPop()
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

	//	menu.SetActive(true);
	}
	public void exitPop()
	{
		menu.SetActive(false);
	}

	public void onClickItems(Button buttonObject)
	{
		OnclickDiscountCheck ();
		if(buttonObject.name =="Gems")
		{
			for(int i=0;i<buttonUnderLoylity.Length;i++)
			{
			buttonUnderLoylity[i].GetComponent<Button>().enabled=false;
			}
			for(int j=0;j<slectionUnderLoaylity.Length;j++)
			{
			slectionUnderLoaylity[j].SetActive(false);
			}

			deactivateItems(0);
		}

		else if(buttonObject.name=="Cards")
		{
			CardClick = true;
			VipClick = false;
			DrawAgain.interactable = true;
			for(int i=0;i<buttonUnderLoylity.Length;i++)
			{
				buttonUnderLoylity[i].GetComponent<Button>().enabled=false;
			}
			for(int j=0;j<slectionUnderLoaylity.Length;j++)
			{
				slectionUnderLoaylity[j].SetActive(false);
			}

			deactivateItems(1);			

		}
		else if(buttonObject.name=="VIP")
		{
			CardClick = false;
			VipClick = true;
			for(int i=0;i<buttonUnderLoylity.Length;i++)
			{
				buttonUnderLoylity[i].GetComponent<Button>().enabled=false;

			}
			for(int j=0;j<slectionUnderLoaylity.Length;j++)
			{
				slectionUnderLoaylity[j].SetActive(false);
			}

			deactivateItems(2);

		}
		else if(buttonObject.name=="Items")
		{
			
			deactivateItems(3);

		}
		else if(buttonObject.name=="DragonCoins")
		{
			//buttonObject.name = "Cards";
			for(int i=0;i<buttonUnderLoylity.Length;i++)
			{
				buttonUnderLoylity[i].SetActive(true);
			}

			for(int i=0;i<buttonUnderLoylity.Length;i++)
			{
				buttonUnderLoylity[i].GetComponent<Button>().enabled=true;
			}
			for(int j=0;j<slectionUnderLoaylity.Length;j++)
			{
				slectionUnderLoaylity[j].SetActive(false);
			}

			//deactivateItems(1);			

			deactivateItems(4);
			deavtiVateLyalityContent(0);

		}
	}
	void OnclickDiscountCheck()
	{
		if (PlayerParameters._instance.myPlayerParameter.membership_no == 0) {
			DisGem_Obj1.SetActive (false);
			DisGem_Obj2.SetActive (false);
			DisGem_Obj3.SetActive (false);
			DisGem_Obj4.SetActive (false);
			DisDC_Obj1.SetActive (false);
			DisDC_Obj2.SetActive (false);
			DisDC_Obj3.SetActive (false);
			DisDC_Obj4.SetActive (false);
		} else {
			DisGem_Obj1.SetActive (true);
			DisGem_Obj2.SetActive (true);
			DisGem_Obj3.SetActive (true);
			DisGem_Obj4.SetActive (true);
			DisDC_Obj1.SetActive (true);
			DisDC_Obj2.SetActive (true);
			DisDC_Obj3.SetActive (true);
			DisDC_Obj4.SetActive (true);
		}

		CostGem_Package1.text = "" + Discount (300);
		CostGem_Package2.text = "" +  Discount (1400);
		CostGem_Package3.text = "" +  Discount (2500);
		CostGem_Package4.text = "" +  Discount (5000);
		CostDC_Package1.text = "" + Discount (1000);
		CostDC_Package2.text = "" +  Discount (3000);
		CostDC_Package3.text = "" +  Discount (7000);
		CostDC_Package4.text = "" +  Discount (15000);
		ItemDes_Gem1.text = "Buy 1 Attack Potion for " + Discount (100) + " Gems and also get 5 loyalty points";
		ItemDes_Gem2.text = "Buy 10 Attack Potion for " + Discount (900) + " Gems and also get 55 loyalty points";
		ItemDes_Gem3.text = "Buy 50 Attack Potion for " + Discount (4000) + " Gems and also get 280 loyalty points";
		ItemDes_Gem4.text = "Buy 100 Attack Potion for " + Discount (7000) + " Gems and also get 570 loyalty points";
		ItemDes_Gem5.text = "Buy 300 Attack Potion for " + Discount (15000) + " Gems and also get 1740 loyalty points";
		ItemDes_Gem6.text = "Buy 500 Attack Potion for " + Discount (20000) + " Gems and also get 3000 loyalty points";
		ItemDes_Gem7.text = "Buy 1 Stamina Potion for " + Discount (100) + " Gems and also get 5 loyalty points";
		ItemDes_Gem8.text = "Buy 10 Stamina Potion for " + Discount (900) + " Gems and also get 55 loyalty points";
		ItemDes_Gem9.text = "Buy 50 Stamina Potion for " + Discount (4000) + " Gems and also get 280 loyalty points";
		ItemDes_Gem10.text = "Buy 100 Stamina Potion for " + Discount (7000) + " Gems and also get 570 loyalty points";
		ItemDes_Gem11.text = "Buy 300 Stamina Potion for " + Discount (15000) + " Gems and also get 1740 loyalty points";
		ItemDes_Gem12.text = "Buy 500 Stamina Potion for " + Discount (20000) + " Gems and also get 3000 loyalty points";
		ItemDes_DC1.text = "Buy 1 Attack Potion for " + Discount (50*5) + " loyalty points";
		ItemDes_DC2.text = "Buy 10 Attack Potion for " + Discount (450*5) + " loyalty points";
		ItemDes_DC3.text = "Buy 50 Attack Potion for " + Discount (2200*5) + " loyalty points";
		ItemDes_DC4.text = "Buy 100 Attack Potion for " + Discount (4300*5) + " loyalty points";
		ItemDes_DC5.text = "Buy 300 Attack Potion for " + Discount (12900*5) + " loyalty points";
		ItemDes_DC6.text = "Buy 500 Attack Potion for " + Discount (20000*5) + " loyalty points";
		ItemDes_DC7.text = "Buy 1 Stamina Potion for " + Discount (50*5) + " loyalty points";
		ItemDes_DC8.text = "Buy 10 Stamina Potion for " + Discount (450*5) + " loyalty points";
		ItemDes_DC9.text = "Buy 50 Stamina Potion for " + Discount (2200*5) + " loyalty points";
		ItemDes_DC10.text = "Buy 100 Stamina Potion for " + Discount (4300*5) + " loyalty points";
		ItemDes_DC11.text = "Buy 300 Stamina Potion for " + Discount (12900*5) + " loyalty points";
		ItemDes_DC12.text = "Buy 500 Stamina Potion for " + Discount (20000*5) + " loyalty points";
	}
	void deactivateItems(int index)
	{
		
		for(int i=0;i<itemSelection.Length;i++)
		{
			for(int k=0;k<buttonClicks.Length;k++)
			{
			if(i==index) 
			{
				//buttonClicks[i].GetComponent<Image>().color=new Color32(210,22,22,255);
				buttonClicks[i].GetComponent<Button>().interactable=false;

				itemSelection[i].SetActive(true);
			}	
			else
			{
			//	buttonClicks[i].GetComponent<Image>().color=new Color32(205,181,181,255);
					buttonClicks[i].GetComponent<Button>().interactable=true;

					itemSelection[i].SetActive(false);

			}
			}
		}
	}




	public void onclickButtonUnderLoaylity(Button buttonType)
	{
		if(buttonType.name =="cardsLoyality")
		{
			
			deavtiVateLyalityContent(0);
		}

		
		else if(buttonType.name =="itemsLoyality")
		{
			print("yes");

			
			deavtiVateLyalityContent(1);			
			
		}
	 else if(buttonType.name=="membershipLoyality")
		{
			deavtiVateLyalityContent(2);
			
		}

	}
	void ClearList()
	{
		cardskill1.Clear ();
		cardskill2.Clear ();
		cardSprite.Clear ();
	}
	void deavtiVateLyalityContent(int index2)
	{
		
		for(int i=0;i<slectionUnderLoaylity.Length;i++)
		{
			for(int k=0;k<buttonUnderLoylity.Length;k++)
			{
				if(i==index2) 
				{
					//buttonClicks[i].GetComponent<Image>().color=new Color32(210,22,22,255);
					buttonUnderLoylity[i].GetComponent<Button>().interactable=false;
					
					slectionUnderLoaylity[i].SetActive(true);
				}	
				else
				{
					//	buttonClicks[i].GetComponent<Image>().color=new Color32(205,181,181,255);
					buttonUnderLoylity[i].GetComponent<Button>().interactable=true;
					
					slectionUnderLoaylity[i].SetActive(false);
					
				}
			}
		}
	}

}
