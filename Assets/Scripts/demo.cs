using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class demo : MonoBehaviour 
{
	public GameObject [] avatar;
	public GameObject avatarhealth;
	public Sprite forwardedDefault;
	public Sprite forwardedSpeeded;
	public Transform enemySkillPosition;
	public Transform playerSkillPosistion;
	public GameObject [] skillPlayer1;
	public GameObject [] skillPlayer2;
	public GameObject [] skillPlayer3;
	public GameObject [] skillPlayer4;
	public GameObject [] skillEnemy1;
	public GameObject [] skillEnemy2;
	public GameObject [] skillEnemy3;
	public GameObject [] skillEnemy4;
	public GameObject [] defencePlayer;
	public GameObject [] defenceEnemy;

	//---------------------- Health Bar --------------------
	public GameObject [] helathPlayer1;
	public GameObject [] helathPlayer2;
	public GameObject [] helathPlayer3;
	public GameObject [] helathPlayer4;
	public GameObject [] helathEnemy1;
	public GameObject [] helathEnemy2;
	public GameObject [] helathEnemy3;
	public GameObject [] helathEnemy4;
	//------------------------------------------------------
	Vector3 playerScale;
	Vector3 enemyScale;
	Vector3 playerPos;
	Vector3 enemyPos;
	Vector3 enemyFisrtPos;
	Vector3 playerFirstPos;
	public GameObject[] Players;
	public GameObject[] Enemies;
	public GameObject[] Players2;
	public GameObject[] Enemies2;
	public GameObject[] Players3;
	public GameObject[] Enemies3;
	public GameObject[] Players4;
	public GameObject[] Enemies4;
	public int playerCounter=0;
	public int enemyCounter=0;
	public Transform playerUp;
	public Transform enemyUp;
	public  string isStart;
	public Transform enemiesGoPos;
	public Transform playerGoPos;
	public int rowChange=0;
	public string defenceactivation;
	public float SpeedFactor;
	bool isButtonFowarded=false;
	// Use this for initialization
	void Start () 
	{
		PlayerPrefs.SetString("isUpPlayer","no");
		PlayerPrefs.SetString("isUpEnemy","no");
		//GameObject.Find("rowChange").GetComponent<Button>().interactable=false;

		defenceactivation="1";
		isStart = "1";
		Invoke("startButton",3*SpeedFactor);
	}


	public void backButtn()
	{
		Application.LoadLevel("Battle_Layout4");
	}
	void helathBar()
	{

		Invoke("nextRow",2*SpeedFactor);

		//GameObject.Find("rowChange").GetComponent<Button>().interactable=true;
		if(rowChange==0)
		{
			for(int i=0;i<helathPlayer1.Length;i++)
			{
				if( PlayerPrefs.GetString("isUpEnemy")=="yes")
				{
				iTween.ScaleTo(helathPlayer1[i],iTween.Hash("x",0.8f/2,"time",1*SpeedFactor));
				}
				else
				{
					iTween.ScaleTo(helathPlayer1[i],iTween.Hash("x",0.8f,"time",1*SpeedFactor));

				}

			}

			for(int j=0;j<helathEnemy1.Length;j++)
			{
				if( PlayerPrefs.GetString("isUpPlayer")=="yes")
				{
					iTween.ScaleTo(helathEnemy1[j],iTween.Hash("x",0.8f/2,"time",1*SpeedFactor));
				}

				else
				{
					iTween.ScaleTo(helathEnemy1[j],iTween.Hash("x",0.8f,"time",1*SpeedFactor));

				}
	
				
			}
		}

		else if(rowChange==1)
		{
			for(int i=0;i<helathPlayer2.Length;i++)
			{
				if( PlayerPrefs.GetString("isUpEnemy")=="yes")
				{
				iTween.ScaleTo(helathPlayer2[i],iTween.Hash("x",0.6f/2,"time",1*SpeedFactor));
				}
				else
				{
					iTween.ScaleTo(helathPlayer2[i],iTween.Hash("x",0.6f,"time",1*SpeedFactor));

				}

			}
			for(int j=0;j<helathEnemy2.Length;j++)
			{
				if( PlayerPrefs.GetString("isUpPlayer")=="yes")
				{
					iTween.ScaleTo(helathEnemy2[j],iTween.Hash("x",0.6f/2,"time",1*SpeedFactor));
				}
				else
				{
					iTween.ScaleTo(helathEnemy2[j],iTween.Hash("x",0.6f,"time",1*SpeedFactor));

				}
				
			}

		}
		else if(rowChange==2)
		{
			for(int i=0;i<helathPlayer3.Length;i++)
			{
				if( PlayerPrefs.GetString("isUpEnemy")=="yes")
				{
				iTween.ScaleTo(helathPlayer3[i],iTween.Hash("x",0.4f/2,"time",1*SpeedFactor));
				}
				else
				{
					iTween.ScaleTo(helathPlayer3[i],iTween.Hash("x",0.4f,"time",1*SpeedFactor));

				}
			}
			for(int j=0;j<helathEnemy3.Length;j++)
			{
				if( PlayerPrefs.GetString("isUpPlayer")=="yes")
				{
					iTween.ScaleTo(helathEnemy3[j],iTween.Hash("x",0.4f/2,"time",1*SpeedFactor));
				}
				else
				{
					iTween.ScaleTo(helathEnemy3[j],iTween.Hash("x",0.4f,"time",1*SpeedFactor));

				}
				
			}
		}
		else if(rowChange==3)
		{
			for(int i=0;i<helathPlayer4.Length;i++)
			{
				if( PlayerPrefs.GetString("isUpEnemy")=="yes")
				{

				iTween.ScaleTo(helathPlayer4[i],iTween.Hash("x",0.2f,"time",1*SpeedFactor));
				}
				else
				{
					iTween.ScaleTo(helathPlayer4[i],iTween.Hash("x",0.2f,"time",1*SpeedFactor));

				}
			}
			for(int j=0;j<helathEnemy4.Length;j++)
			{
				if( PlayerPrefs.GetString("isUpPlayer")=="yes")
				{
					iTween.ScaleTo(helathEnemy4[j],iTween.Hash("x",0.2f,"time",1*SpeedFactor));
				}
				else
				{
					iTween.ScaleTo(helathEnemy4[j],iTween.Hash("x",0.2f,"time",1*SpeedFactor));


				}
				
			}
		}
	}
	public void fastForward()
	{
		if(isButtonFowarded==false)
		{
			SpeedFactor=SpeedFactor/2.5f;
			isButtonFowarded=true;
			GameObject.Find("Button_forward").GetComponent<Image>().sprite=forwardedSpeeded;

		}
		else
		{
			SpeedFactor=0.5f;
			isButtonFowarded=false;
			GameObject.Find("Button_forward").GetComponent<Image>().sprite=forwardedDefault;

		}

	}
	public void nextRow()
	{
		Invoke("startButton",3*SpeedFactor);
		PlayerPrefs.SetString("isUpPlayer","no");
		PlayerPrefs.SetString("isUpEnemy","no");
		//GameObject.Find("rowChange").GetComponent<Button>().interactable=false;
		//GameObject.Find("Button_start").GetComponent<Button>().interactable=true;

		if (rowChange == 0)
		{
			GameObject.Find("textPlayer").GetComponent<Text>().CrossFadeAlpha(1,0,false);
			GameObject.Find("textEnemy").GetComponent<Text>().CrossFadeAlpha(1,0,false);
			GameObject.Find("textEnemy").GetComponent<Text>().text="Enemy attacking middle Row"; 
			
			GameObject.Find("textPlayer").GetComponent<Text>().text="Player attacking middle Row"; 
			if (isStart == "3") 
			{

				playerCounter = 0;
				enemyCounter = 0;
				enemyFisrtPos = GameObject.Find ("enemies1").transform.position;
				iTween.MoveTo (GameObject.Find ("enemies1"), iTween.Hash ("x", enemiesGoPos.transform.position.x, "time", 1*SpeedFactor, "onComplete", "placeEnmey2", "onCompleteTarget", this.gameObject));
				Invoke ("playerFirstMove", 0.5f*SpeedFactor);
			}
			rowChange = 1;
		} 
		else if (rowChange == 1)
		{
			GameObject.Find("textEnemy").GetComponent<Text>().text="Enemy attacking third Row"; 
			
			GameObject.Find("textPlayer").GetComponent<Text>().text="Player attacking third Row"; 
			GameObject.Find("textPlayer").GetComponent<Text>().CrossFadeAlpha(1,0,false);
			GameObject.Find("textEnemy").GetComponent<Text>().CrossFadeAlpha(1,0,false);
			if (isStart == "3") 
			{
				playerCounter = 0;
				enemyCounter = 0;
				enemyFisrtPos = GameObject.Find ("enemies2").transform.position;
				iTween.MoveTo (GameObject.Find ("enemies2"), iTween.Hash ("x", enemiesGoPos.transform.position.x, "time", 1*SpeedFactor, "onComplete", "placeEnmey3", "onCompleteTarget", this.gameObject));
				Invoke ("playerSecondMove", 0.5f*SpeedFactor);
			}
			rowChange = 2;
		} 
		else if (rowChange == 2)
		{
			GameObject.Find("textEnemy").GetComponent<Text>().text="Enemy attacking fourth Row"; 
			
			GameObject.Find("textPlayer").GetComponent<Text>().text="Player attacking fourth Row"; 
			GameObject.Find("textPlayer").GetComponent<Text>().CrossFadeAlpha(1,0,false);
			GameObject.Find("textEnemy").GetComponent<Text>().CrossFadeAlpha(1,0,false);
			if (isStart == "3")
			{
				playerCounter = 0;
				enemyCounter = 0;
				enemyFisrtPos = GameObject.Find ("enemies3").transform.position;
				iTween.MoveTo (GameObject.Find ("enemies3"), iTween.Hash ("x", enemiesGoPos.transform.position.x, "time", 1*SpeedFactor, "onComplete", "placeEnmey4", "onCompleteTarget", this.gameObject));
				Invoke ("playerThirdMove", 0.5f*SpeedFactor);

			}
			rowChange = 3;

		}
		else if(rowChange==3)
		{
			GameObject.Find("textEnemy").GetComponent<Text>().text="Avatar attack"; 
			GameObject.Find("textEnemy").GetComponent<Text>().CrossFadeAlpha(1,0,false);

			if(isStart=="3")
			{
				//enemyCounter=3;
				GameObject.Find ("enemies4").SetActive(false);
				avatarhealth.SetActive(true);
				avatar[0].SetActive(true);
				rowChange = 4;


			}


		}

	}


	void placeEnmey2()
	{
		iTween.MoveTo(GameObject.Find("enemies2"),iTween.Hash("x",enemyFisrtPos.x,"time",1*SpeedFactor));
	
	}
	void placePlayer2()
	{
		iTween.MoveTo(GameObject.Find("players2"),iTween.Hash("x",playerFirstPos.x,"time",1*SpeedFactor));

	}
	void placeEnmey3()
	{
		iTween.MoveTo(GameObject.Find("enemies3"),iTween.Hash("x",enemyFisrtPos.x,"time",1*SpeedFactor));
		
	}
	void placePlayer3()
	{
		iTween.MoveTo(GameObject.Find("players3"),iTween.Hash("x",playerFirstPos.x,"time",1*SpeedFactor));
		
	}
	void placeEnmey4()
	{
		iTween.MoveTo(GameObject.Find("enemies4"),iTween.Hash("x",enemyFisrtPos.x,"time",1*SpeedFactor));
		
	}
	void placePlayer4()
	{
		iTween.MoveTo(GameObject.Find("players4"),iTween.Hash("x",playerFirstPos.x,"time",1*SpeedFactor));
		
	}
	void playerFirstMove()
	{
		playerFirstPos = GameObject.Find ("players1").transform.position;
		iTween.MoveTo (GameObject.Find ("players1"), iTween.Hash ("x", playerGoPos.transform.position.x, "time", 1*SpeedFactor, "onComplete", "placePlayer2", "onCompleteTarget", this.gameObject));
	}
	void playerSecondMove()
	{
		playerFirstPos = GameObject.Find ("players2").transform.position;
		iTween.MoveTo (GameObject.Find ("players2"), iTween.Hash ("x", playerGoPos.transform.position.x, "time", 1*SpeedFactor, "onComplete", "placePlayer3", "onCompleteTarget", this.gameObject));
	}
	void playerThirdMove()
	{
		playerFirstPos = GameObject.Find ("players3").transform.position;
		iTween.MoveTo (GameObject.Find ("players3"), iTween.Hash ("x", playerGoPos.transform.position.x, "time", 1*SpeedFactor, "onComplete", "placePlayer4", "onCompleteTarget", this.gameObject));
	}
	
	// Update is called once per frame
	void Update ()
	{

	
	}


	void startPlayer()
	{

		if(rowChange==0)
		{
			if(defenceactivation=="1")
			{
				playerCounter=Random.Range(0,4);
				playerScale = Players [playerCounter].transform.localScale;
				playerPos = Players [playerCounter].transform.position;
				iTween.MoveTo (Players [playerCounter], iTween.Hash ("y", playerUp.transform.position.y, "easeType", iTween.EaseType.linear, "time", 0.5*SpeedFactor, "onComplete", "playerScaleTo", "onCompleteTarget", this.gameObject));
				defenceactivation="2";
			}
			else
			{
				if(playerCounter>4)
				{

				}
				else
				{

					playerScale = Players [playerCounter].transform.localScale;
					playerPos = Players [playerCounter].transform.position;
					iTween.MoveTo (Players [playerCounter], iTween.Hash ("y", playerUp.transform.position.y, "easeType", iTween.EaseType.linear, "time", 0.5*SpeedFactor, "onComplete", "playerScaleTo", "onCompleteTarget", this.gameObject));

				}

			}
		}
		else if  (rowChange==1) 
		{
			for (int i=0; i<Players.Length; i++) 
			{
				Players [i] = Players2 [i];
			}
			for (int j=0; j<Enemies.Length; j++) 
			{
				Enemies [j] = Enemies2 [j];
			}

			if(playerCounter>4)
			{

			}
			else
			{
			playerScale = Players [playerCounter].transform.localScale;
			playerPos = Players [playerCounter].transform.position;
				iTween.MoveTo (Players [playerCounter], iTween.Hash ("y", playerUp.transform.position.y, "easeType", iTween.EaseType.linear, "time", 0.5*SpeedFactor, "onComplete", "playerScaleTo", "onCompleteTarget", this.gameObject));
			}
		} 


	  else if (rowChange == 2)
		{
			for (int i=0; i<Players.Length; i++) 
			{
				Players [i] = Players3 [i];
			}
			for (int j=0; j<Enemies.Length; j++) 
			{
				Enemies [j] = Enemies3 [j];
			}
			
			if(playerCounter>4)
			{
				
			}
			else
			{
				playerScale = Players [playerCounter].transform.localScale;
				playerPos = Players [playerCounter].transform.position;
				iTween.MoveTo (Players [playerCounter], iTween.Hash ("y", playerUp.transform.position.y, "easeType", iTween.EaseType.linear, "time", 0.5*SpeedFactor, "onComplete", "playerScaleTo", "onCompleteTarget", this.gameObject));
			}

		} 
		else if (rowChange == 3)
		{
			for (int i=0; i<Players.Length; i++) 
			{
				Players [i] = Players4 [i];
			}
			for (int j=0; j<Enemies.Length; j++) 
			{
				Enemies [j] = Enemies4 [j];
			}
			
			if(playerCounter>4)
			{
				
			}
			else
			{
				playerScale = Players [playerCounter].transform.localScale;
				playerPos = Players [playerCounter].transform.position;
				iTween.MoveTo (Players [playerCounter], iTween.Hash ("y", playerUp.transform.position.y, "easeType", iTween.EaseType.linear, "time", 0.5*SpeedFactor, "onComplete", "playerScaleTo", "onCompleteTarget", this.gameObject));
			}
		}

		else if(rowChange==4)
		{
			for (int i=0; i<Players.Length; i++) 
			{
				Players [i] = Players4 [i];
			}
			for (int j=0; j<Enemies.Length; j++) 
			{
				Enemies [0] = avatar [0];
			}
			
			if(playerCounter>4)
			{
				
			}
			else
			{
				playerScale = Players [playerCounter].transform.localScale;
				playerPos = Players [playerCounter].transform.position;
				iTween.MoveTo (Players [playerCounter], iTween.Hash ("y", playerUp.transform.position.y, "easeType", iTween.EaseType.linear, "time", 0.5*SpeedFactor, "onComplete", "playerScaleTo", "onCompleteTarget", this.gameObject));
			}

		}

		else
		{
		}

	}
	void playerScaleTo()
	{

		Players [playerCounter].GetComponent<Image> ().color = new Color (1, 1, 1, 0.5f);
		iTween.ScaleTo (Players [playerCounter], iTween.Hash ("y", 0.5f, "x", 0.5f, "time", 0.3*SpeedFactor));
		Invoke ("playerScaleReset", 0.2f*SpeedFactor);
		Invoke ("playerCardStop", 0.5f*SpeedFactor);


	}

	void playerScaleReset()
	{
		iTween.ScaleTo (Players [playerCounter], iTween.Hash ("y",playerScale.y,"x",playerScale.x,  "time", 0.5*SpeedFactor));

	}

	void playerCardStop()
	{

		iTween.Pause (Players [playerCounter]);
		Invoke("playerReset",0.5f*SpeedFactor);


	}
	void playerHide()
	{
		Players [playerCounter].GetComponent<Image> ().color = new Color (1, 1, 1, 1);
		Players [playerCounter].transform.position = playerPos;
	}

	void playerReset()
	{

		if(PlayerPrefs.GetString("isUpPlayer")=="yes")
		{
			Players [playerCounter].GetComponent<Image> ().color = new Color (1, 1, 1, 1f);
			iTween.MoveTo (Players [playerCounter], iTween.Hash ("x", playerPos.x, "y", playerPos.y, "time", 0.4*SpeedFactor, "onComplete", "playerCounterPlus", "onCompleteTarget", this.gameObject));

		}
		else
		{
		if (defenceactivation == "2")
		{
//				
				Players [playerCounter].GetComponent<Image> ().color = new Color (1, 1, 1, 0.5f);
				iTween.MoveTo (defencePlayer [playerCounter], iTween.Hash ("x", playerSkillPosistion.transform.position.x, "time", 1.5*SpeedFactor));
				Invoke("playerHide",1*SpeedFactor);
				Invoke("startEnemy",1.5f*SpeedFactor);
				Invoke("DestroyPlayerDefence",1.5f*SpeedFactor);

		} 

		
		if((rowChange==0)&&(defenceactivation=="4"))
		{

			int randomSkill=0;
			randomSkill=Random.Range(0,4);
			if(playerCounter==randomSkill)
			{

				Players [playerCounter].GetComponent<Image> ().color = new Color (1, 1, 1, 0.5f);
				iTween.MoveTo (skillPlayer1 [playerCounter], iTween.Hash ("x", playerSkillPosistion.transform.position.x, "time", 1.5*SpeedFactor,"onComplete","playerCounterPlus","onCompleteTarget",this.gameObject));
				PlayerPrefs.SetString("isUpPlayer","yes");
				Invoke("playerSkillReset",1.5f*SpeedFactor);

			}
			else
			{

				Players [playerCounter].GetComponent<Image> ().color = new Color (1, 1, 1, 1f);
				iTween.MoveTo (Players [playerCounter], iTween.Hash ("x", playerPos.x, "y", playerPos.y, "time", 0.4*SpeedFactor, "onComplete", "playerCounterPlus", "onCompleteTarget", this.gameObject));
			}
		}
		if((rowChange==1)&&(defenceactivation=="4"))
		{

			int randomSkill=0;
			randomSkill=Random.Range(0,4);
			if(playerCounter==randomSkill)
			{

				Players [playerCounter].GetComponent<Image> ().color = new Color (1, 1, 1, 0.5f);
				iTween.MoveTo (skillPlayer2 [playerCounter], iTween.Hash ("x", playerSkillPosistion.transform.position.x, "time", 1.5*SpeedFactor,"onComplete","playerCounterPlus","onCompleteTarget",this.gameObject));
				PlayerPrefs.SetString("isUpPlayer","yes");
				Invoke("playerSkillReset",1.5f*SpeedFactor);

			}
			else
			{
				Players [playerCounter].GetComponent<Image> ().color = new Color (1, 1, 1, 1f);
				iTween.MoveTo (Players [playerCounter], iTween.Hash ("x", playerPos.x, "y", playerPos.y, "time", 0.4*SpeedFactor, "onComplete", "playerCounterPlus", "onCompleteTarget", this.gameObject));
			}

		}
		if((rowChange==2)&&(defenceactivation=="4"))
		{



			int randomSkill=0;
			randomSkill=Random.Range(0,4);
			if(playerCounter==randomSkill)
			{

				Players [playerCounter].GetComponent<Image> ().color = new Color (1, 1, 1, 0.5f);
				iTween.MoveTo (skillPlayer3 [playerCounter], iTween.Hash ("x", playerSkillPosistion.transform.position.x, "time", 1.5*SpeedFactor,"onComplete","playerCounterPlus","onCompleteTarget",this.gameObject));
				PlayerPrefs.SetString("isUpPlayer","yes");
				Invoke("playerSkillReset",1.5f*SpeedFactor);
				
			}
			else
			{
				Players [playerCounter].GetComponent<Image> ().color = new Color (1, 1, 1, 1f);
				iTween.MoveTo (Players [playerCounter], iTween.Hash ("x", playerPos.x, "y", playerPos.y, "time", 0.4*SpeedFactor, "onComplete", "playerCounterPlus", "onCompleteTarget", this.gameObject));
			}
			
		}
		if((rowChange==3)&&(defenceactivation=="4"))
		{
					
			int randomSkill=0;
			randomSkill=Random.Range(0,4);
			if(playerCounter==randomSkill)	
			{

				Players [playerCounter].GetComponent<Image> ().color = new Color (1, 1, 1, 0.5f);
				iTween.MoveTo (skillPlayer4 [playerCounter], iTween.Hash ("x", playerSkillPosistion.transform.position.x, "time", 1.5*SpeedFactor,"onComplete","playerCounterPlus","onCompleteTarget",this.gameObject));
				PlayerPrefs.SetString("isUpPlayer","yes");
				Invoke("playerSkillReset",1.5f*SpeedFactor);
				
			}
			else
			{
				Players [playerCounter].GetComponent<Image> ().color = new Color (1, 1, 1, 1f);
				iTween.MoveTo (Players [playerCounter], iTween.Hash ("x", playerPos.x, "y", playerPos.y, "time", 0.4*SpeedFactor, "onComplete", "playerCounterPlus", "onCompleteTarget", this.gameObject));
			}
		}
			if((rowChange==4)&&(defenceactivation=="4"))
			{

					Players [playerCounter].GetComponent<Image> ().color = new Color (1, 1, 1, 1f);
					iTween.MoveTo (Players [playerCounter], iTween.Hash ("x", playerPos.x, "y", playerPos.y, "time", 0.4*SpeedFactor, "onComplete", "playerCounterPlus", "onCompleteTarget", this.gameObject));

			}
	}





	}
	void playerSkillReset()
	{
		Players [playerCounter].GetComponent<Image> ().color = new Color (1, 1, 1, 1f);
		iTween.MoveTo (Players [playerCounter], iTween.Hash ("x", playerPos.x, "y", playerPos.y, "time", 0.4*SpeedFactor));
	}

	void playerCounterPlus()
	{
		if(rowChange==0) 
		{
			Destroy(skillPlayer1[playerCounter]);

		}
		else if(rowChange==1)
		{
			Destroy(skillPlayer2[playerCounter]);

		}
		else if(rowChange==2)
		{
			Destroy(skillPlayer3[playerCounter]);
			
		}else if(rowChange==3)
		{
			Destroy(skillPlayer4[playerCounter]);
			
		}

		if (playerCounter <= 4)
		{

			playerCounter += 1;
			Invoke ("startPlayer", 0.7f*SpeedFactor);

		} 
	 	 if(playerCounter>4)
		{
			isStart="2";
			startEnemy();
		}

	}
	//------------------------------------------------------------------------------------------------
	void startEnemy()
	{
		if (defenceactivation == "2") 
		{

			enemyCounter=Random.Range(0,4);
			enemyScale = Enemies [enemyCounter].transform.localScale;
			enemyPos = Enemies [enemyCounter].transform.position;
			iTween.MoveTo (Enemies [enemyCounter], iTween.Hash ("y", enemyUp.transform.position.y, "easeType", iTween.EaseType.easeOutExpo, "time", 0.5*SpeedFactor, "onComplete", "enemyScaleTo", "onCompleteTarget", this.gameObject));

			defenceactivation="3";
		}


 if (isStart == "2") 
	
		{
			enemyScale=Enemies[enemyCounter].transform.localScale;
			enemyPos = Enemies [enemyCounter].transform.position;
			iTween.MoveTo (Enemies [enemyCounter], iTween.Hash ("y", enemyUp.transform.position.y, "easeType", iTween.EaseType.easeOutExpo, "time", 0.5*SpeedFactor, "onComplete", "enemyScaleTo", "onCompleteTarget", this.gameObject));
		} 

	}
	void enemyCardStop()
	{
		Enemies [enemyCounter].GetComponent<Image> ().color = new Color (1, 1, 1, 0.5f);
		iTween.Pause (Enemies [enemyCounter]);
		Invoke ("enemyReset", 0.5f*SpeedFactor);
	}
	void enemyScaleTo()
	{
			
		Enemies [enemyCounter].GetComponent<Image> ().color = new Color (1, 1, 1, 0.5f);
		iTween.ScaleTo (Enemies [enemyCounter], iTween.Hash ("y", 0.5f, "x", 0.5f, "time", 0.3*SpeedFactor));
		Invoke ("enemyScaleReset", 0.2f*SpeedFactor);
		Invoke ("enemyCardStop", 0.5f*SpeedFactor);

		
	}
	void enemyScaleReset()
	{

		iTween.ScaleTo (Enemies [enemyCounter], iTween.Hash ("y", enemyScale.y, "x", enemyScale.x, "time", 0.5*SpeedFactor));

	}

	void enemyReset()
	{
		if(	PlayerPrefs.GetString("isUpEnemy")=="yes")
		{
			Enemies [enemyCounter].GetComponent<Image> ().color = new Color (1, 1, 1, 1f);
			iTween.MoveTo (Enemies [enemyCounter], iTween.Hash ("x", enemyPos.x, "y", enemyPos.y, "time", 0.4*SpeedFactor, "onComplete", "enemyCounterPlus", "onCompleteTarget", this.gameObject));

		}
		else
		{

		if (defenceactivation == "3") 
		{

			Enemies [enemyCounter].GetComponent<Image> ().color = new Color (1, 1, 1, 0.5f);
			iTween.MoveTo (defenceEnemy [enemyCounter], iTween.Hash ("x", enemySkillPosition.transform.position.x, "time", 1.5*SpeedFactor));
			Invoke("DestroyEnemyDefence",1.5f*SpeedFactor);
			Invoke("startButton",1.5f*SpeedFactor);
			defenceactivation="4";
			Invoke("enemyHide",1*SpeedFactor);
		}

	else if ((rowChange==0) &&(defenceactivation=="4"))
		{
			int randomSkill=0;
			randomSkill=Random.Range(0,4);
			if(enemyCounter==randomSkill)
			{

				Enemies [enemyCounter].GetComponent<Image> ().color = new Color (1, 1, 1, 0.5f);
				iTween.MoveTo (skillEnemy1 [enemyCounter], iTween.Hash ("x", enemySkillPosition.transform.position.x, "time", 1.5*SpeedFactor, "onComplete","enemyCounterPlus","onCompleteTarget", this.gameObject));
				PlayerPrefs.SetString("isUpEnemy","yes");
				Invoke("enemySkillReset",1.5f*SpeedFactor);
			}
			else
			{
				Enemies [enemyCounter].GetComponent<Image> ().color = new Color (1, 1, 1, 1f);
				iTween.MoveTo (Enemies [enemyCounter], iTween.Hash ("x", enemyPos.x, "y", enemyPos.y, "time", 0.4*SpeedFactor, "onComplete", "enemyCounterPlus", "onCompleteTarget", this.gameObject));

			}
		}
		else if ((rowChange==1) &&(defenceactivation=="4"))
		{
			int randomSkill=0;
			randomSkill=Random.Range(0,4);
			if(enemyCounter==randomSkill)
			{
				
				Enemies [enemyCounter].GetComponent<Image> ().color = new Color (1, 1, 1, 0.5f);
				iTween.MoveTo (skillEnemy2 [enemyCounter], iTween.Hash ("x", enemySkillPosition.transform.position.x, "time", 1.5*SpeedFactor, "onComplete","enemyCounterPlus","onCompleteTarget", this.gameObject));
				PlayerPrefs.SetString("isUpEnemy","yes");
				Invoke("enemySkillReset",1.5f*SpeedFactor);
			}
			else
			{
				Enemies [enemyCounter].GetComponent<Image> ().color = new Color (1, 1, 1, 1f);
				iTween.MoveTo (Enemies [enemyCounter], iTween.Hash ("x", enemyPos.x, "y", enemyPos.y, "time", 0.4*SpeedFactor, "onComplete", "enemyCounterPlus", "onCompleteTarget", this.gameObject));
				
			}
		}
		else if ((rowChange==2) &&(defenceactivation=="4"))
		{
			int randomSkill=0;
			randomSkill=Random.Range(0,4);
			if(enemyCounter==randomSkill)
			{
				
				Enemies [enemyCounter].GetComponent<Image> ().color = new Color (1, 1, 1, 0.5f);
				iTween.MoveTo (skillEnemy3 [enemyCounter], iTween.Hash ("x", enemySkillPosition.transform.position.x, "time", 1.5*SpeedFactor, "onComplete","enemyCounterPlus","onCompleteTarget", this.gameObject));
				PlayerPrefs.SetString("isUpEnemy","yes");
				Invoke("enemySkillReset",1.5f*SpeedFactor);
			}
			else
			{
				Enemies [enemyCounter].GetComponent<Image> ().color = new Color (1, 1, 1, 1f);
				iTween.MoveTo (Enemies [enemyCounter], iTween.Hash ("x", enemyPos.x, "y", enemyPos.y, "time", 0.4*SpeedFactor, "onComplete", "enemyCounterPlus", "onCompleteTarget", this.gameObject));
				
			}
		}
		else if ((rowChange==3) &&(defenceactivation=="4"))
		{
			int randomSkill=0;
			randomSkill=Random.Range(0,4);
			if(enemyCounter==randomSkill)
			{
				
				Enemies [enemyCounter].GetComponent<Image> ().color = new Color (1, 1, 1, 0.5f);
				iTween.MoveTo (skillEnemy4 [enemyCounter], iTween.Hash ("x", enemySkillPosition.transform.position.x, "time", 1.5*SpeedFactor, "onComplete","enemyCounterPlus","onCompleteTarget", this.gameObject));
				PlayerPrefs.SetString("isUpEnemy","yes");
				Invoke("enemySkillReset",1.5f*SpeedFactor);
			}
			else
			{
				Enemies [enemyCounter].GetComponent<Image> ().color = new Color (1, 1, 1, 1f);
				iTween.MoveTo (Enemies [enemyCounter], iTween.Hash ("x", enemyPos.x, "y", enemyPos.y, "time", 0.4*SpeedFactor, "onComplete", "enemyCounterPlus", "onCompleteTarget", this.gameObject));
				
			}
		}
			else if ((rowChange==4) &&(defenceactivation=="4"))
			{

					Enemies [enemyCounter].GetComponent<Image> ().color = new Color (1, 1, 1, 1f);
					iTween.MoveTo (Enemies [enemyCounter], iTween.Hash ("x", enemyPos.x, "y", enemyPos.y, "time", 0.4*SpeedFactor, "onComplete", "enemyCounterPlus", "onCompleteTarget", this.gameObject));
					}
			}
		}

	void DestroyEnemyDefence()
	{
		Destroy( defenceEnemy[enemyCounter]);

	}
	void DestroyPlayerDefence()
	{
		Destroy( defencePlayer[playerCounter]);
	}
	void enemySkillReset()
	{
		Enemies [enemyCounter].GetComponent<Image> ().color = new Color (1, 1, 1, 1f);
		iTween.MoveTo (Enemies [enemyCounter], iTween.Hash ("x", enemyPos.x, "y", enemyPos.y, "time", 0.4*SpeedFactor));
	}
	void enemyHide()
	{

		Enemies [enemyCounter].GetComponent<Image> ().color = new Color (1, 1, 1, 1);
		Enemies [enemyCounter].transform.position= enemyPos;

	}
	void enemyCounterPlus()
	{

		if(rowChange==0)
		{
			Destroy(skillEnemy1[enemyCounter]);
			
		}
		else if(rowChange==1)
		{
			Destroy(skillEnemy2[enemyCounter]);

		}
		else if(rowChange==2)
		{
			Destroy(skillEnemy3[enemyCounter]);
			
		}
		else if(rowChange==3)
		{
			Destroy(skillEnemy4[enemyCounter]);
			
		}
		enemyCounter+=1;
		Invoke ("startEnemy",0.7f*SpeedFactor);
		if (enemyCounter > 4) 
		{

			Invoke("helathBar",3*SpeedFactor);
			isStart="3";
		}

	}
	void fadeText()
	{


		GameObject.Find("textPlayer").GetComponent<Text>().CrossFadeAlpha(0,2*SpeedFactor,false);
		GameObject.Find("textEnemy").GetComponent<Text>().CrossFadeAlpha(0,2*SpeedFactor,false);
	



	}
	public void startButton()
	{

		//GameObject.Find("Button_start").GetComponent<Button>().interactable=false;
		fadeText();
		playerCounter=0;
		enemyCounter=0;
		startPlayer ();

	}
	void hideDefenceEnemy()
	{
		Destroy(defenceEnemy[enemyCounter]);
	}
	void hideDefencePlayer()
	{
		Destroy(defencePlayer[playerCounter]);
	}
}
