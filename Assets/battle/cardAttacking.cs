using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class cardAttacking : MonoBehaviour 
{
	public GameObject [] skillActivation;				// SKILL CARDS ARRAY
	public GameObject [] skillActivationenemy;			
	public GameObject [] defenceActivationenemy;			
	public GameObject [] skillActivationLast;			
	public Transform targetImage;
	public Transform targetImageEnmey;
	public GameObject avatarCard;
	public GameObject avtarHelath;
	public GameObject [] defenceActivation;			// DEFENCE CARDS ARRAY
	public GameObject [] lifeBar;
	public GameObject [] lifeBar1;
	public GameObject [] lifeBar2;
	public GameObject [] cards;						// 1st ROW OF ENEMY/PLAYER
	public GameObject [] cards2;						// 2nd ROW OF ENEMY/PLAYER
	public GameObject [] playersLife;
	public GameObject []cards3;						// 3rd ROW OF ENEMY/PLAYER
	public int counter=0;							// attack counter 1-10 (player/enemy)
	Vector3 tempPos;								// player cards positions
	Vector3 tempScale;								// player cards Scaling
	public float positions=160;						
	Color mycolor;
	int hitComplete = 0;													
	public float cardup=0;							// cards Up to attacking position time
	public float alphachnge=0;						// cards alpha changing time during attack 
	public float reset=0;							// cards back to main position time
	bool forwarded=true;							// Bool conditions for row speed up button							
	Vector3 temposRowEnemy;							
	Vector3 temposRowPlayer;
	public int rowNext=0;							// Next row counter
	public int skillCounter=0;
	public int skillCounterEnmey=0;
	public string count;							// First skill cards second defence card Counter 
	public string isbattle;							// defence and skill cards checking on start button
	public string enemyCount;
	public GameObject fastFoward;
	public GameObject fastFoward2;
	string enemyActiveskil;
	public Transform playerUp;
	public Transform enemyUp;
	// Use this for initialization
	void Start () 
	{
		enemyActiveskil="1";
		isbattle="1";
		avatarCard.SetActive (false);
		avtarHelath.SetActive(false);
		count="1";
		GameObject.Find ("Text_Enemy").GetComponent<TextMesh> ().text = "";
		GameObject.Find ("Text_Player").GetComponent<TextMesh> ().text = "";
		//		PlayerPrefs.DeleteAll ();
		PlayerPrefs.SetString("avatar","no");
		PlayerPrefs.SetString("reset","no");
		PlayerPrefs.SetString("row1move","no");
		PlayerPrefs.SetString("array","no");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (rowNext == 0)
		{
			temposRowEnemy = GameObject.Find ("enenmyCards2").gameObject.transform.position;
			temposRowPlayer = GameObject.Find ("playerCards2").gameObject.transform.position;
			GameObject.Find ("Text_Enemy").GetComponent<TextMesh> ().text = "Enemy attacking front row";
			GameObject.Find ("Text_Player").GetComponent<TextMesh> ().text = "Player attacking front row";
			
		} 
		else if (rowNext == 1) 
		{
			GameObject.Find ("Text_Enemy").GetComponent<TextMesh> ().text = "Enemy attacking middle row";
			GameObject.Find ("Text_Player").GetComponent<TextMesh> ().text = "Player attacking middle row";
		}
		else if (rowNext == 2) 
		{
			GameObject.Find ("Text_Enemy").GetComponent<TextMesh> ().text = "Enemy attacking last row";
			GameObject.Find ("Text_Player").GetComponent<TextMesh> ().text = "Player attacking last row";
		}
		
		
		
		if (counter > 9) 
		{
			GameObject.Find ("rowChange").GetComponent<Button> ().interactable = true;
			GameObject.Find("TextNextRow").GetComponent<Text>().text="NEXT ROW";
			
		} 
		else 
		{
			GameObject.Find ("rowChange").GetComponent<Button> ().interactable = false;
			
		}
		
		
		
	}
	//********************** CARDS UP ***************************
	
	public void cardsupDown()
	{	
		if (PlayerPrefs.GetString ("row1move") == "yes")
		{
		} 
		else 
		{
			tempPos = cards [counter].transform.position;
			tempScale = cards [counter].transform.localScale;
			iTween.MoveTo (cards [counter].gameObject, iTween.Hash ("y", 160, "time", cardup, "easeType", iTween.EaseType.easeOutExpo, "onComplete", "changeAlpha", "onCompleteTarget", this.gameObject));
		}
		if (PlayerPrefs.GetString ("array") == "yes") 
		{
			tempPos = cards [counter].transform.position;
			tempScale = cards [counter].transform.localScale;
			iTween.MoveTo (cards [counter].gameObject, iTween.Hash ("y", 160, "time", cardup, "easeType", iTween.EaseType.easeOutExpo, "onComplete", "changeAlpha", "onCompleteTarget", this.gameObject));
			PlayerPrefs.SetString("reset","yes");
			
		}
		if (PlayerPrefs.GetString("avatar")=="yes")
		{
			for(int i=0;i<cards.Length;i++)
			{
				cards[cards.Length-5] =avatarCard;
			}
			tempPos = cards [counter].transform.position;
			tempScale = cards [counter].transform.localScale;
			iTween.MoveTo (cards [counter].gameObject, iTween.Hash ("y", positions, "time", cardup, "easeType", iTween.EaseType.easeOutExpo, "onComplete", "changeAlpha", "onCompleteTarget", this.gameObject));
			PlayerPrefs.SetString("reset","yes");
		}


	}
	
	
	//*************************** BATTLE CARDS SELECTION ******************************
	
	public void battleCard ()
	{
		counter = Random.Range (1,3);
		tempPos = cards [counter].transform.position;
		tempScale = cards [counter].transform.localScale;
		iTween.MoveTo (cards [counter].gameObject, iTween.Hash ("y", positions, "time", cardup, "easeType", iTween.EaseType.easeOutExpo, "onComplete", "changeAlpha", "onCompleteTarget", this.gameObject));
	}
	
	
	//**************************** CARDS ALPHA CHANGES ON THE UP TIME ********************************
	
	void changeAlpha()
	{
		Color temp=new Color(1,1,1,0.6f);
		cards [counter].gameObject.GetComponent<Button> ().image.color = temp;
		iTween.ScaleTo(cards[counter], iTween.Hash("x", 0.37f,"y",0.37f,"easeType", iTween.EaseType.linear, "loopType", "pingPong","time", alphachnge,"onComplete","resetCard1","onCompleteTarget",this.gameObject));
		
		
		if (count == "1")
		{
			//Invoke ("resumeSkill", 4);
			iTween.ScaleTo(cards[counter], iTween.Hash("x", 0.37f,"y",0.37f,"easeType", iTween.EaseType.linear, "loopType", "pingPong","time", alphachnge,"onComplete","resetCard2","onCompleteTarget",this.gameObject));
			iTween.MoveTo (defenceActivation [counter], iTween.Hash ("x",targetImage.transform.position.x, "y", targetImage.transform.position.y, "time", 1.5f,"onComplete","des", "onCompleteTarget", this.gameObject, "easeType", iTween.EaseType.easeInOutSine));
			Invoke ("alphachangeinActivation", 0.8f);
			//Invoke("enemyDefence",3);
			iTween.Pause (cards [counter]);
			print ("------------ DEFENCE INCREASE-------------");
			
		}
		
		else if (count == "2")
		{
			if(rowNext==2)
			{
				iTween.ScaleTo(cards[counter], iTween.Hash("x", 0.37f,"y",0.37f,"easeType", iTween.EaseType.linear, "loopType", "pingPong","time", alphachnge,"onComplete","resetCard2","onCompleteTarget",this.gameObject));
				//iTween.MoveTo (skillActivation [counter], iTween.Hash ("x",targetImage.transform.position.x, "y",targetImage.transform.position.y, "time", 1.5f, "onCompleteTarget", this.gameObject, "easeType", iTween.EaseType.easeInOutSine));
				Invoke ("alphachangeinActivation", 0.8f);
				//iTween.Pause (cards [counter]);
			}
			else
			{
			skillCounter=Random.Range(1,2);
			if(counter==skillCounter)
			{
				print("random value"+skillCounter);
				//Invoke ("resumeSkill", 4);
				iTween.ScaleTo(cards[counter], iTween.Hash("x", 0.37f,"y",0.37f,"easeType", iTween.EaseType.linear, "loopType", "pingPong","time", alphachnge,"onComplete","resetCard2","onCompleteTarget",this.gameObject));
				iTween.MoveTo (skillActivation [counter], iTween.Hash ("x",targetImage.transform.position.x, "y",targetImage.transform.position.y, "time", 1.5f, "onCompleteTarget", this.gameObject, "easeType", iTween.EaseType.easeInOutSine));
				Invoke ("alphachangeinActivation", 0.8f);
				iTween.Pause (cards [counter]);

				print("------------ SKILL INCREASE-------------");
			}
			}
		}






		else if(enemyActiveskil=="2")
		{

			if(counter==7)
			{
			iTween.ScaleTo(cards[counter], iTween.Hash("x", 0.37f,"y",0.37f,"easeType", iTween.EaseType.linear, "loopType", "pingPong","time", alphachnge,"onComplete","resetCard2","onCompleteTarget",this.gameObject));
			iTween.MoveTo (skillActivation [counter], iTween.Hash ("x",targetImageEnmey.transform.position.x, "y",targetImageEnmey.transform.position.y, "time", 1.5f, "onCompleteTarget", this.gameObject, "easeType", iTween.EaseType.easeInOutSine));
			Invoke ("playEnemy", 0.8f);
			iTween.Pause (cards [counter]);
			
			print("------------ SKILL INCREASE-------------");
			}
		}
		else if(enemyActiveskil=="3")
		{

			if(counter==3)

			{
				iTween.ScaleTo(cards[counter], iTween.Hash("x", 0.37f,"y",0.37f,"easeType", iTween.EaseType.linear, "loopType", "pingPong","time", alphachnge,"onComplete","resetCard2","onCompleteTarget",this.gameObject));
				iTween.MoveTo ( skillActivationLast[counter], iTween.Hash ("x",targetImage.transform.position.x, "y",targetImage.transform.position.y, "time", 1.5f, "onCompleteTarget", this.gameObject, "easeType", iTween.EaseType.easeInOutSine));
				Invoke ("playEnemy", 0.8f);
				iTween.Pause (cards [counter]);


			}
		}



	}
	
	void enemyDefence()
	{
		
		//Invoke ("alphachangeinActivation", 0.8f);
		tempPos = cards [counter+4].transform.position;
		tempScale = cards [counter+4].transform.localScale;
		iTween.MoveTo (cards [counter+4].gameObject, iTween.Hash ("y", 350, "time", cardup, "easeType", iTween.EaseType.easeOutExpo,  "onCompleteTarget", this.gameObject));
		iTween.MoveTo (defenceActivationenemy [counter], iTween.Hash ("x",targetImageEnmey.transform.position.x, "y", targetImageEnmey.transform.position.y, "time", 1.5f,"onComplete","des2", "onCompleteTarget", this.gameObject, "easeType", iTween.EaseType.easeInOutSine));
		Invoke ("alphachangeinActivationEnemy", 0.8f);
		Invoke ("destroyEnmey", 2.5f);
		
		
	}
	//************************** changing Alpha of Attacking Card  *********************************
	
	void alphachangeinActivation()
	{
		if(count=="1")
		{

		Color temp=new Color(1,1,1,0.0f);
		cards [counter].gameObject.GetComponent<Button> ().image.color = temp;
		
		Invoke("hideActiveCard",2f);
		}
		else
		{
			Color temp=new Color(1,1,1,0.0f);
			cards [counter].gameObject.GetComponent<Button> ().image.color = temp;
			Invoke("hideActiveCard",2f);
			enemyActiveskil="2";



		}
	

	}

	void playEnemy()
	{
		Color temp=new Color(1,1,1,0.0f);
		cards [counter].gameObject.GetComponent<Button> ().image.color = temp;
		Invoke("hideActiveCard",2f);

	}
	void alphachangeinActivationEnemy()
	{
		Color temp=new Color(1,1,1,0.6f);
		cards [counter+4].gameObject.GetComponent<Button> ().image.color = temp;
		Invoke ("changeAlpha2", 0.3f);
	}
	void changeAlpha2()
	{
		Color temp=new Color(1,1,1,0.0f);
		cards [counter+4].gameObject.GetComponent<Button> ().image.color = temp;
		
		
	}
	
	
	
	void hideActiveCard()
	{

		if(rowNext==2)
		{


		}
		else
			{
			if (count == "1") 
			{
				Destroy (defenceActivation [counter]);
			//resumeSkill();
				enemyDefence();
			
			
			} 
			else if(count=="2") 
			{
				Destroy (skillActivation [counter]);
				resumeSkill();
			
			
			}
			else if(enemyActiveskil=="2")
			{
				Destroy (skillActivation [counter]);
				resumeSkill();
			}
			if (enemyActiveskil=="3")
			{
				Destroy (skillActivationLast [counter]);
				resumeSkill();

			}
			}

	}
	
	
	
	
	//******************************** RESUME ITWEEN AFTER SKILL/ BATTLE DEFENCE ********************************
	
	public void resumeSkill()
	{
		iTween.Resume(cards[counter]);
		count="3";

	}
	public void counterReset()
	{
		count="3";
		if (count == "3") 
		{
			//counter = 0;
			
			iTween.MoveTo (cards [0].gameObject, iTween.Hash ("y", 160, "time", cardup, "easeType", iTween.EaseType.easeOutExpo,  "onCompleteTarget", this.gameObject));
			tempPos = cards [0].transform.position;
			iTween.Resume(cards[counter]);
			counter = 0;
			
			
			
		}
		//iTween.Resume (cards [counter]);
		
	}
	
	
	
	
	void changeTextAplha()
	{
		
		iTween.FadeTo (GameObject.Find ("Text_Enemy"), iTween.Hash ("a", 0, "time", 0.7f));
		iTween.FadeTo (GameObject.Find ("Text_Player"), iTween.Hash ("a", 0, "time", 0.7f));
		
	}
	
	
	void destroyEnmey()
	{
		
		for(int i=0;i<defenceActivationenemy.Length;i++)
		{
			
			Destroy( defenceActivationenemy[i]);
			
		}
		
		Invoke ("counterReset",0.1f);
		
		
	}
	void des()
	{
		
		Color temp=new Color(1,1,1,1);
		cards [counter].gameObject.GetComponent<Button> ().image.color = temp;
		iTween.MoveTo (cards [counter].gameObject, iTween.Hash ("y", 74, "time", cardup, "easeType", iTween.EaseType.easeOutExpo,  "onCompleteTarget", this.gameObject));
		
	}
	void des2()
	{
		Color temp=new Color(1,1,1,1);
		cards [counter+4].gameObject.GetComponent<Button> ().image.color = temp;
		iTween.MoveTo (cards [counter+4].gameObject, iTween.Hash ("y", 446, "time", cardup, "easeType", iTween.EaseType.easeOutExpo,  "onCompleteTarget", this.gameObject));
		
	}
	
	//********************** SPEED UP ATTACKING ************************
	
	public void fastForwarded()
	{
		if (forwarded == true) 
		{
			cardup = cardup / 2;
			alphachnge = alphachnge / 2;
			reset = reset / 2;
			fastFoward2.SetActive(true);
			fastFoward.SetActive(false);
			forwarded = false;
		} 
		else 
		{
			cardup = cardup * 2;
			alphachnge = alphachnge * 2;
			reset = reset *2;
			fastFoward2.SetActive(false);
			fastFoward.SetActive(true);
			forwarded = true;
			
		}
	}
	
	//********************** START BUTTON/ START ATTACK FUNCTION ******************
	
	public void buttonStart()
	{
		if(PlayerPrefs.GetString("avatar")=="yes")
		{
			Invoke ("cardsupDown", 1f);
			changeTextAplha ();
			counter=0;
			//enemyActiveskil="3";
		}
		else
		{
			if (isbattle == "1")
			{
				print("firstStart");
				Invoke ("battleCard", 1f);
				changeTextAplha ();
				isbattle = "2";
				
			}
			else if(isbattle == "2")
			{
				print("secondStart");
				Invoke ("cardsupDown", 1f);
				changeTextAplha ();
				isbattle = "3";
				
			}
			if(rowNext == 2)
			{
				Invoke ("cardsupDown", 1f);


				changeTextAplha ();
			}
		

		}
	}
	
	//***************************** CARDS DOWN AFTER UP *******************************
	
	public void resetCard1()
	{
		//print ("ss");
		hitComplete += 1;
		if (hitComplete % 2 == 0) 
		{
			iTween.Stop ();
			hitComplete = 0;
			Color temp=new Color(1,1,1,1);
			cards [counter].gameObject.GetComponent<Button> ().image.color = temp;
			iTween.MoveTo (cards [counter].gameObject, iTween.Hash ("x", tempPos.x, "y", tempPos.y, "time",reset,"onComplete","cardsupDown","onCompleteTarget",this.gameObject));
			counter+=1;
			if (counter > 4) 
			{
				positions=350;
			}
			if (counter > 9)
			{	
				Invoke("Healthbar",1f);
				positions=160;
				PlayerPrefs.SetString ("row1move","yes");
				PlayerPrefs.SetString("array","no");
				PlayerPrefs.SetString("array2","no");
				
			}
		}
	}
	
	
	//************************** COUNTER RESET ON SKILL TIME  DOWN CARDS ***************************
	
	public void resetCard2()
	{
		//print ("ss");
		hitComplete += 1;
		if (hitComplete % 2 == 0) 
		{
			iTween.Stop ();
			hitComplete = 0;
			Color temp=new Color(1,1,1,0);
			Invoke("resetColor",0.35f);
			cards [counter].gameObject.GetComponent<Button> ().image.color = temp;
			iTween.MoveTo (cards [counter].gameObject, iTween.Hash ("x", tempPos.x, "y", tempPos.y, "time",reset,"onComplete","cardsupDown","onCompleteTarget",this.gameObject));
			counter+=1;
			if (counter > 4) 
			{
				positions=350;
			}
			if (counter > 9)
			{
				Invoke("Healthbar",1f);
				positions=160;
				PlayerPrefs.SetString ("row1move","yes");
				PlayerPrefs.SetString("array","no");
				PlayerPrefs.SetString("array2","no");
			}
		}
	}
	
	void resetColor()
	{
		for (int i=0; i<cards.Length; i++) 
		{
			Color temp = new Color (1, 1, 1, 1);
			cards [i].gameObject.GetComponent<Button> ().image.color = temp;
			
		}
	}
	
	//************************** CLICK ON NEXT ROW FUNCTION ******************************
	
	public void rowChange()
	{
		if (PlayerPrefs.GetString ("avatar") == "yes") 
		{
			enemyActiveskil="3";
			GameObject.Find("enenmyCards3").SetActive(false);
			Invoke("aciveAvatar",0.5f);
		}
		else 
		{
			iTween.FadeTo (GameObject.Find ("Text_Enemy"), iTween.Hash ("a", 1, "time", 0.1f));
			iTween.FadeTo (GameObject.Find ("Text_Player"), iTween.Hash ("a", 1, "time", 0.1f));
			rowNext += 1;
			enemyRow1 ();
			count="2";
			Invoke ("arrahide", 1);
		}
	}
	
	//**************************** AVATAR ACIVE FUNCTION *******************************
	
	void aciveAvatar()
	{
		avatarCard.SetActive (true);
		avtarHelath.SetActive(true);
		
	}
	
	//*************************** FIRST ROW OF ENEMY AND PLAYER SWAP AFTER CLICKING ON ROW CHANGE **********************
	
	public void playerRow1()
	{
		if (PlayerPrefs.GetString ("reset") == "yes") 
		{
			temposRowPlayer = GameObject.Find ("playerCards2").gameObject.transform.position;
			iTween.MoveTo (GameObject.Find ("playerCards2").gameObject, iTween.Hash ("x", 600, "time", 2.5, "onComplete", "enemyRow1" ));
			Invoke ("playerRow2", 0.4f);
			PlayerPrefs.SetString ("row1move", "yes");
		} 
		else 
		{
			temposRowPlayer = GameObject.Find ("playerCards").gameObject.transform.position;
			iTween.MoveTo (GameObject.Find ("playerCards").gameObject, iTween.Hash ("x", 600, "time", 2.5, "onComplete", "enemyRow1"));
			Invoke ("playerRow2", 0.4f);
			PlayerPrefs.SetString ("row1move", "yes");
		}
	}
	
	
	public void enemyRow1()
	{
		if (PlayerPrefs.GetString ("reset") == "yes") 
		{
			temposRowEnemy = GameObject.Find ("enenmyCards2").gameObject.transform.position;
			iTween.MoveTo (GameObject.Find ("enenmyCards2").gameObject, iTween.Hash ("x", -590, "time", 2.5));
			Invoke ("playerRow1", 0.8f);
			Invoke ("enemyRow2", 0.4f);
			PlayerPrefs.SetString ("row1move", "yes");
		}
		
		else
		{
			temposRowEnemy = GameObject.Find ("enenmyCards").gameObject.transform.position;
			iTween.MoveTo(GameObject.Find("enenmyCards").gameObject,iTween.Hash("x",-590,"time",2.5));
			Invoke ("playerRow1", 0.8f);
			Invoke ("enemyRow2",0.4f);
			PlayerPrefs.SetString("row1move","yes");
		}
	}
	
	//******************* SECOND ROW OF ENEMY AND PLAYER SWAP AFTER CLICKING ON ROW CHANGE ************************
	
	public void playerRow2()
	{
		if (PlayerPrefs.GetString ("reset") == "yes") 
		{
			iTween.MoveTo (GameObject.Find ("playerCards3").gameObject, iTween.Hash ("x", temposRowPlayer.x, "y", temposRowPlayer.y, "time", 1.5f));
			
		}
		else 
		{
			iTween.MoveTo (GameObject.Find ("playerCards2").gameObject, iTween.Hash ("x", temposRowPlayer.x, "y", temposRowPlayer.y, "time", 1.5f));
		}
	}
	
	public void enemyRow2()
	{
		if (PlayerPrefs.GetString ("reset") == "yes")
		{
			iTween.MoveTo (GameObject.Find ("enenmyCards3").gameObject, iTween.Hash ("x", temposRowEnemy.x, "y", temposRowEnemy.y, "time", 1.5f));
			
		} 
		else 
		{
			iTween.MoveTo (GameObject.Find ("enenmyCards2").gameObject, iTween.Hash ("x", temposRowEnemy.x, "y", temposRowEnemy.y, "time", 1.5f));
		}
	}
	
	//************************ HIDE OR DESTROY ROW/COLOUMN AFTER SWAP *********************
	
	public void arrahide()
	{
		if (PlayerPrefs.GetString ("reset") == "yes") 
		{
			counter = 0;
			for (int i=0; i<cards.Length; i++) 
			{
				cards [i] = cards3 [i];
				
			}
			PlayerPrefs.SetString ("array", "yes");
			GameObject.Find ("playerCards2").gameObject.SetActive (false);
			GameObject.Find ("enenmyCards2").gameObject.SetActive (false);
		} 
	
		else 
		{
			counter = 0;
			for (int i=0; i<cards.Length; i++) 
			{
				cards [i] = cards2 [i];
				
			}
			PlayerPrefs.SetString ("array", "yes");
			GameObject.Find ("playerCards").gameObject.SetActive (false);
			GameObject.Find ("enenmyCards").gameObject.SetActive (false);
		} 
	}
	
	//********************* HEALTH BAR OF THE ALL CARDS *************************



	public void Healthbar()
	{

		if(rowNext==2)
		{
			for (int k=0;k<lifeBar2.Length; k++)
			{
				lifeBar1[k]=lifeBar2[k];
				iTween.ScaleTo(lifeBar2[k],iTween.Hash("x",0.0,"time",3));
			//	PlayerPrefs.SetString ("array", "yes");
				PlayerPrefs.SetString ("avatar", "yes");

			}
		}
		else
		{	
		if (PlayerPrefs.GetString ("reset") == "yes") 
		{
			for(int i=0;i<lifeBar.Length;i++)
			{
				lifeBar[i]=lifeBar1[i];
				iTween.ScaleTo(lifeBar[i],iTween.Hash("x",0.3f,"time",3));
				print ("----- health bar second time-----");
				//PlayerPrefs.SetString("avatar","yes");
				//PlayerPrefs.SetString ("array", "yes");

			}
			for (int j=0;j<playersLife.Length; j++)
			{
				iTween.ScaleTo(playersLife[j],iTween.Hash("x",0.5,"time",3));
			}


		}
		else
			{
			for(int i=0;i<lifeBar.Length;i++)
				{
				iTween.ScaleTo(lifeBar[i].gameObject,iTween.Hash("x",0.5f,"time",3));
				print ("----- health bar first time-----");
				
				}
			}
		}
	}
}

















