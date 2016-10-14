using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class BattleCardEntities
{
	public GameObject []cardEntity;
}

[System.Serializable]
public class HealthEntities
{
	public Slider []cardEntity;
}

public class testBattleNew : MonoBehaviour
{
	public static testBattleNew instance;
	public GameObject combo1,combo2;
	bool isCombo=false;
	//======== SMASH ANIMATION  ===========
	public GameObject [] attackEffectsDown;

	public GameObject smash;
	GameObject smashPrefab;
	float minVlue=0f;
	public float y=-5f;
	public float z=-5f;
	float temScaled=0;
	Vector3 position;
	Vector3 position2;
	Vector3 position3;

	public BattleCardEntities []playerCards;
	public BattleCardEntities []enemyCards;
	public HealthEntities []playerHealth;
	public HealthEntities []enemyHealth;
	//=============================
	public GameObject [] attackEffects;

	public GameObject smash2;
	GameObject smashPrefab2;

	float minVlue2=0f;
	public float y2=5f;
	public float z2=5f;
	float temScaledUp=0;
	Vector3 positionUp;
	Vector3 positionUp2;
	Vector3 positionUp3;
	Vector3  scaleTimeUp;

	//===============================
	Vector3  scaleTime;

	public Sprite defaultForwarded; 
	public Sprite changeForwarded; 
	public Sprite changeForwarded2; 
	public Sprite changeForwarded3;  

	public GameObject [] defencePlayer;
	public GameObject [] defenceEnemy;

	public Transform avatarPos;

	public GameObject avatarCard;
	public GameObject avatarHealth;

	public GameObject playerAvatarCard;
	public GameObject playerAvatarHealth;

	public Transform enemySkillPosition;
	public Transform playerSkillPosistion;
	//public Transform avatarPos;

	Vector3 playerScale;
	Vector3 enemyScale;
	Vector3 playerPos;
	Vector3 enemyPos;
	Vector3 enemyFisrtPos;
	Vector3 playerFirstPos;
	public Transform playerUp;
	public Transform enemyUp;
	public Transform enemiesGoPos;
	public Transform playerGoPos;
	public int rowCounter=0;
	public string isDefenceAcite;
	public string skill;
	int playerCounter=0;
	int enemyCounter=0;
	public float speedFactor;
	//bool isPressed=false;
	public string isPressed;
	public string enemySkillCount;
	public int newVariableEne=0;
	public int newVariable=0;
	int randomPos=3;
	int randomPosUp=3;
	int randomeffects=0;
	int randomEffectsDown=0;
	GameObject[] emeptyArray;
	GameObject[] battleEffects;

	public bool fromQuest;

	public DeckCalculations playerDeck;
	public DeckCalculations enemyDeck;
	// Use this for initialization

	int attackOrderNoCarriedOut;
	int attackNoOfPlayer;
	int attackNoOfEnemy;

	public Text playerAttackText, enemyAttackText;
	public Sprite []avatarSprites;
	public Image gateImage;
	public void Awake()
	{
		instance=this;
		BattleLogic._instance.ResetStats ();

	}

	public void Start ()
	{
//		playerAttackText = GameObject.Find ("textPlayer").GetComponent<Text> ();
//		enemyAttackText = GameObject.Find ("textEnemy").GetComponent<Text> ();
		enemyScale = GameObject.Find ("enemyActualPos").transform.localScale;
		enemyPos = GameObject.Find ("enemyActualPos").transform.position;

		playerScale = GameObject.Find ("playerActualPos").transform.localScale;
		playerPos = GameObject.Find ("playerActualPos").transform.position;

		combo1.SetActive(false);
		combo2.SetActive(false);
		PlayerPrefs.SetString("avatar","no");
		isPressed ="1";
		enemySkillCount="1";
		skill="1";
		isDefenceAcite="1";
		randomPos=Random.Range(1,3);

		BattleLogic._instance.CalculateRowStatsFirstTime ();
		BattleLogic._instance.GetNoOfWaves ();
		BattleLogic._instance.Battle ();

		Invoke("StartButton",1*speedFactor);
	}

	void GateSet()
	{
		if (loadingScene.Instance.battleInstance.enemyDeck.gateLevel < 3) //1,2,3
			gateImage.sprite = (Sprite)Resources.Load<Sprite> ("Gate/Gate0");
		else if (loadingScene.Instance.battleInstance.enemyDeck.gateLevel < 6) //4,5,6
			gateImage.sprite = (Sprite)Resources.Load<Sprite> ("Gate/Gate1");
		else if (loadingScene.Instance.battleInstance.enemyDeck.gateLevel < 9) //7,8,9
			gateImage.sprite = (Sprite)Resources.Load<Sprite> ("Gate/Gate2");
		else
			gateImage.sprite = (Sprite)Resources.Load<Sprite> ("Gate/Gate3");
			
			
	}

	public void destroyAddedUp()
	{
		combo2.SetActive(false);
		z2=z2-0.5f;
		if(GameObject.Find(z2.ToString()))
			Destroy(GameObject.Find(z2.ToString()));

	}

	public void clickAddedUp()
	{
		y2=y2-0.5f;
		randomPosUp=Random.Range(1,4);
		combo2.SetActive(true);

		Vector3 positionUp = Vector3.zero;
		if(randomPosUp==1)
		{
			positionUp = new Vector3(Random.Range(-1.8f,0.6f),y2,0);
			
		}
		else if(randomPosUp==2)
		{

			print("====  TWO  ===");
			positionUp = new Vector3(Random.Range(1f,1.81f),y2,0);
		}
		else if(randomPosUp==3)
		{

			positionUp = new Vector3(Random.Range(-1.8f,-0.6f),y2,0);
		}
		else if(randomPosUp==4)
		{
			positionUp = new Vector3(Random.Range(1f,1.81f),y2,0);

		}
		randomeffects = Random.Range (0,attackEffects.Length);
		smashPrefab2 = (GameObject) Instantiate (attackEffects[randomeffects] ,positionUp,Quaternion.identity);
		scaleSmashUp();

		temScaledUp=Random.Range(0.1f,0.3f);
		smash2.transform.localScale= new  Vector2(temScaledUp,temScaledUp);
		smashPrefab2.name = y2.ToString();
		iTween.ScaleTo(combo2,iTween.Hash("x",0.17,"y",0.17,"time",0.1,"onComplete","scomboScaleDownUp","onCompleteTarget",this.gameObject));

	}
	void scomboScaleDownUp()
	{
		iTween.ScaleTo(combo2,iTween.Hash("x",0.17,"y",0.17,"time",0.1));

	}
	void scaleSmashUp()
	{
		//		
		scaleTimeUp=smashPrefab2.transform.localScale;
		iTween.ScaleTo(smashPrefab2, iTween.Hash("x",smashPrefab2.transform.localScale.x+0.07f,"y",smashPrefab2.transform.localScale.y+0.07f,"delay", .1,"onComplete","scaleSmashBackUp","onCompleteTarget",this.gameObject));
		
	}
	void scaleSmashBackUp()
	{
		iTween.ScaleTo(smashPrefab2, iTween.Hash("x",scaleTimeUp.x,"y",scaleTimeUp.y,"time",0.8f));
		
	}

	//---

	//------------  smash animation ----------
	public void destroyAdded()
	{
		combo1.SetActive(false);
		z=z+0.5f;
		if(GameObject.Find(z.ToString()))
			Destroy(GameObject.Find(z.ToString()));
		
	
	}
	public void comboActivate()
	{
		combo1.SetActive(true);
		combo2.SetActive(true);
		Invoke("comboDeactivate",0.7f);
	}

	public void comboDeactivate()
	{
		combo1.SetActive(false);
		combo2.SetActive(false);
	}

	public void clickAdded()
	{
		combo1.SetActive(true);

		y=y+0.5f;
		randomPos=Random.Range(1,4);

		Vector3 positionVal = Vector3.zero;
		if(randomPos==1)
		{
			positionVal = new Vector3(Random.Range(-1.8f,-0.6f),y,0);
		}
		else if(randomPos==2)
		{
			print("====  TWO  ===");
			positionVal = new Vector3(Random.Range(1f,1.81f),y,0);
		}
		else if(randomPos==3)
		{
			positionVal = new Vector3(Random.Range(-1.8f,-0.6f),y,0);
		}
		else if(randomPos==4)
		{
			positionVal = new Vector3(Random.Range(1f,1.81f),y,0);
		}
		randomEffectsDown = Random.Range (0,attackEffects.Length);
		smashPrefab = (GameObject) Instantiate (attackEffectsDown[randomEffectsDown] ,positionVal,Quaternion.identity);
		scaleSmash();
		iTween.ScaleTo(combo1,iTween.Hash("x",0.17,"y",0.17,"time",0.1f,"onComplete","scomboScaleDown","onCompleteTarget",this.gameObject));
		temScaled=Random.Range(0.1f,0.3f);
		smash.transform.localScale= new  Vector2(temScaled,temScaled);
		smashPrefab.name = y.ToString();

	}
	void scomboScaleDown()
	{
		iTween.ScaleTo(combo1,iTween.Hash("x",0.17,"y",0.17,"time",0.1f));

	}
	void scaleSmash()
	{
		scaleTime=smashPrefab.transform.localScale;
		iTween.ScaleTo(smashPrefab, iTween.Hash("x",smashPrefab.transform.localScale.x+0.07f,"y",smashPrefab.transform.localScale.y+0.07f,"delay", .1,"onComplete","scaleSmashBack","onCompleteTarget",this.gameObject));
	
	}


	void scaleSmashBack()
	{
		iTween.ScaleTo(smashPrefab, iTween.Hash("x",scaleTime.x,"y",scaleTime.y,"time",0.8f));
	
	}


	//==========================



	void avatarActive()
	{
		y=-5f;
		z=-5f;
		iTween.ShakePosition(GameObject.Find("players4"),iTween.Hash("x",0.1f,"y",0.1f,"time",2.0f));
		//comboActivate();
		Invoke("destroyAdded",0f);
		Invoke("destroyAdded",0.3f);
		Invoke("destroyAdded",0.5f);
		Invoke("destroyAdded",0.7f);
		avatarHealth.SetActive(true);
		avatarCard.SetActive(true);
		PlayerPrefs.SetString("avatar","yes");
		avatarHealth.gameObject.GetComponent<Image>().CrossFadeAlpha(1,0.6f*speedFactor,false);
		avatarCard.gameObject.GetComponent<Image>().CrossFadeAlpha(1,0.6f*speedFactor,false);
		Invoke("StartButton",1);
	}

	int rowBroughtIn;
	void healthBarPlayer()
	{
		Debug.Log ("rowBroughtIn player bar= "+rowBroughtIn);

		for(int i=0;i<playerHealth[playerRow].cardEntity.Length;i++)
		{
			if(i < BattleLogic._instance.playerSoldiers.cardRowCalculations[rowBroughtIn].cardsSoldiersPercentage.Count)
				playerHealth[playerRow].cardEntity[i].value = BattleLogic._instance.playerSoldiers.cardRowCalculations[rowBroughtIn].cardsSoldiersPercentage[i];
		}
		rowBroughtIn++;
//		for(int i=0;i<playerHealth[rowCounter].cardEntity.Length;i++)
//		{
//			iTween.ScaleTo(playerHealth[rowCounter].cardEntity[i].gameObject,iTween.Hash("x",0.8f,"time",1*speedFactor));
//		}
		
	}

	void healthBarEnemy()
	{
		Debug.Log ("rowBroughtIn enemy bar= "+rowBroughtIn);
		if (rowBroughtIn > 0) {
			for (int i = 0; i < enemyHealth [enemyRow].cardEntity.Length; i++) {
				if (i < BattleLogic._instance.enemySoldiers.cardRowCalculations [rowBroughtIn].cardsSoldiersPercentage.Count)
					enemyHealth [enemyRow].cardEntity [i].value = BattleLogic._instance.enemySoldiers.cardRowCalculations [rowBroughtIn].cardsSoldiersPercentage [i];
			}
		}
//		for(int i=0;i<enemyHealth[rowCounter].cardEntity.Length;i++)
//		{
//			iTween.ScaleTo(enemyHealth[rowCounter].cardEntity[i].gameObject,iTween.Hash("x",0.8f,"time",1*speedFactor));
//		}
	}



	public void forwarded()
	{
		if(isPressed=="1")
		{
			GameObject.Find("Button_forward").GetComponent<Image>().sprite=changeForwarded;
			speedFactor=speedFactor/2;
			isPressed="2";
		}
		else if(isPressed=="2")
		{
			GameObject.Find("Button_forward").GetComponent<Image>().sprite=changeForwarded2;
			speedFactor=speedFactor/4;
			isPressed="3";
		}
		else if(isPressed=="3")
		{
			GameObject.Find("Button_forward").GetComponent<Image>().sprite=changeForwarded3;
			speedFactor=speedFactor/8;
			isPressed="4";
		}
		else if(isPressed=="4")
		{
			GameObject.Find("Button_forward").GetComponent<Image>().sprite=defaultForwarded;
			speedFactor=1;
			isPressed="1";
		}
	}

	void fadeText()
	{
		playerAttackText.CrossFadeAlpha(0,2*speedFactor,false);
		enemyAttackText.CrossFadeAlpha(0,2*speedFactor,false);
	}


	void lostPopUp()
	{
		PlayerPrefs.SetString("win","no");
//		if (!fromQuest)
//			loadingScene.Instance.lost ();
//		else {
			loadingScene.Instance.resultFromQuest ();
			loadingScene.Instance.battleResultInstance.battleLost.SetActive (true);
			loadingScene.Instance.battleResultInstance.battleWin.SetActive (false);
//		}

		if (fromQuest){
			for (int i = 0; i < loadingScene.Instance.scenes.Count; i++) {
				if (loadingScene.Instance.scenes [i].name == "main") {
					loadingScene.Instance.scenes [i].SetActive (false);
				} else if (loadingScene.Instance.scenes [i].name == "quest") {
					loadingScene.Instance.scenes [i].SetActive (false);
				} else if (loadingScene.Instance.scenes [i].name == "battleResult") {
					loadingScene.Instance.scenes [i].SetActive (true);
				} else {
					loadingScene.Instance.scenes [i].SetActive (false);
					loadingScene.Instance.scenes.RemoveAt (i);
				}
			}

		}
		battleEffects= GameObject.FindGameObjectsWithTag("battleEffects");
		{
			for (int j=0;j<battleEffects.Length;j++)
			{
				Destroy(battleEffects[j].gameObject);
			}
		}
	}
	void winPopUp()
	{
		PlayerPrefs.SetString("win","yes");
//		if (!fromQuest)
//			loadingScene.Instance.win ();
//		else {
			loadingScene.Instance.resultFromQuest ();
			loadingScene.Instance.battleResultInstance.battleLost.SetActive (false);
			loadingScene.Instance.battleResultInstance.battleWin.SetActive (true);
//		}

		if (fromQuest){
			for (int i = 0; i < loadingScene.Instance.scenes.Count; i++) {
				if (loadingScene.Instance.scenes [i].name == "main") {
					loadingScene.Instance.scenes [i].SetActive (false);
				} else if (loadingScene.Instance.scenes [i].name == "win") {
					loadingScene.Instance.scenes [i].SetActive (true);
				} else if (loadingScene.Instance.scenes [i].name == "quest") {
					loadingScene.Instance.scenes [i].SetActive (false);
				} else if (loadingScene.Instance.scenes [i].name == "battleResult") {
					loadingScene.Instance.scenes [i].SetActive (true);
				} else {
					loadingScene.Instance.scenes [i].SetActive (false);
					loadingScene.Instance.scenes.RemoveAt (i);
				}
			}
			battleEffects= GameObject.FindGameObjectsWithTag("battleEffects");
			{
				for (int j=0;j<battleEffects.Length;j++)
				{
					Destroy(battleEffects[j].gameObject);
				}
			}
		}

	}

	public void rowChange()
	{
		Invoke("destroyAdded",0f);
		Invoke("destroyAdded",0.3f);
		Invoke("destroyAdded",0.5f);
		Invoke("destroyAdded",0.7f);

	}

	void desTroyPlayer()
	{
		y2=+5f;
		z2=+5f;
		
		Invoke("destroyAddedUp",0f);
		Invoke("destroyAddedUp",0.3f);
		Invoke("destroyAddedUp",0.5f);
		Invoke("destroyAddedUp",0.7f);
	}
	void rowChngePlayer1()
	{

		if(rowCounter==0)
		{

//			playerPos=GameObject.Find("players1").transform.position;
			iTween.MoveTo(GameObject.Find("players1"),iTween.Hash("x",enemiesGoPos.transform.position.x,"time",0.5*speedFactor,"onComplete","rowChngePlayer2","onCompleteTarget",this.gameObject));
		}
		else if(rowCounter==1)
		{

//			playerPos=GameObject.Find("players2").transform.position;
			iTween.MoveTo(GameObject.Find("players2"),iTween.Hash("x",enemiesGoPos.transform.position.x,"time",0.5*speedFactor,"onComplete","rowChngePlayer3","onCompleteTarget",this.gameObject));

		}
		else if(rowCounter==2)
		{
			hideEnemy();
		}
		else if(rowCounter==3)
		{

			hideEnemy();
		}

	}
	void hideAfterHide()
	{
		for(int i=0;i<enemyCards[3].cardEntity.Length;i++)
		{
			enemyCards[3].cardEntity[i].SetActive(false);
		}
	}
	void hideEnemy()
	{

		print("hideEnemy");
		for(int i=0;i<enemyCards[3].cardEntity.Length;i++)
		{
			enemyCards[3].cardEntity[i].GetComponent<Image>().CrossFadeAlpha(0,2*speedFactor,false);
			
		}
		emeptyArray= GameObject.FindGameObjectsWithTag("barHealth");
		{
			for (int j=0;j<emeptyArray.Length;j++)
			{
				emeptyArray[j].GetComponent<Image>().CrossFadeAlpha(0,2*speedFactor,false);
			}
		}
		for(int k=0;k<enemyHealth[3].cardEntity.Length;k++)
		{
			enemyHealth[3].cardEntity[k].GetComponent<Image>().CrossFadeAlpha(0,2*speedFactor,false);
		}
		Invoke("hideAfterHide",2*speedFactor);
		Invoke("avatarActive",2*speedFactor);


	}


	void ShowAvatarFight()
	{
		enemyAttackText.gameObject.SetActive (false);
		playerAttackText.gameObject.SetActive (false);
		avatarHealth.SetActive(true);
		avatarCard.SetActive(true);
		playerAvatarCard.GetComponent<Image>().sprite = avatarSprites[loadingScene.Instance.battleInstance.enemyDeck.avatarNo];
		avatarCard.gameObject.GetComponent<Image>().CrossFadeAlpha(1,0.6f*speedFactor,false);
		playerAvatarCard.SetActive(true);
		playerAvatarCard.GetComponent<Image>().sprite = avatarSprites[loadingScene.Instance.battleInstance.playerDeck.avatarNo];
		playerAvatarHealth.SetActive(true);
		playerAvatarCard.gameObject.GetComponent<Image>().CrossFadeAlpha(1,0.6f*speedFactor,false);
		StartCoroutine (DisplayFinalResult());
	}


	void ChangeHealths()
	{
		if (enemyRow > -1)
			healthBarEnemy();
		healthBarPlayer();
	}


	void SlidersOfPlayers()
	{
		for (int h = 0; h <  BattleLogic._instance.initialEnemySoldiers.cardRowCalculations.Count; h++) {
			for (int i = 0; i < BattleLogic._instance.initialEnemySoldiers.cardRowCalculations[h].cardsSoldiersPercentage.Count; i++) {
//				Debug.Log ("player h = "+h+" , i = "+i);
				enemyHealth [h].cardEntity [i].value = BattleLogic._instance.initialEnemySoldiers.cardRowCalculations [h].cardsSoldiersPercentage [i];
			}
		}
		for (int h = 0; h <  BattleLogic._instance.initialPlayerSoldiers.cardRowCalculations.Count; h++) {
			for (int i = 0; i < BattleLogic._instance.initialPlayerSoldiers.cardRowCalculations[h].cardsSoldiersPercentage.Count; i++) {
//				Debug.Log ("player h = "+h+" , i = "+i);
				playerHealth [h].cardEntity [i].value = BattleLogic._instance.initialPlayerSoldiers.cardRowCalculations [h].cardsSoldiersPercentage [i];
			}
		}
	}
	public void StartButton()
	{
		if (enemyDeckVisible != null) {
			
			iTween.MoveTo (GameObject.Find ("enemies" + (enemyRow + 1)), iTween.Hash ("x", enemiesGoPos.transform.position.x, "time", 0.5 * speedFactor, "onComplete", "BringInRows", "onCompleteTarget", this.gameObject));
			iTween.MoveTo (GameObject.Find ("players" + (playerRow + 1)), iTween.Hash ("x", enemiesGoPos.transform.position.x, "time", 0.5 * speedFactor));
		} else {
			SlidersOfPlayers ();
			if (attackOrderNoCarriedOut < BattleLogic._instance.orderOfAttacks.Count )
				BringInRows ();
			else
			{
				ShowAvatarFight();
			}
		}
	}

	IEnumerator DisplayFinalResult()
	{
		yield return new WaitForSeconds (1.0f);
		BattleResult.requireSendingResult = true;

		if(BattleLogic.isBattleWin)
		{
			winPopUp ();
		}
		else
		{
			lostPopUp ();
		}
	}



	void BringInRows()
	{
		if (attackOrderNoCarriedOut < BattleLogic._instance.orderOfAttacks.Count) {
			if (BattleLogic._instance.orderOfAttacks [attackOrderNoCarriedOut] == 1) { // player is attacking
				playerRow = BattleLogic._instance.orderOfPlayerAttack [attackNoOfPlayer];
				enemyRow = BattleLogic._instance.orderOfPlayerAttackOnEnemyRows [attackNoOfPlayer];

				playerAttackText.gameObject.SetActive (true);
				enemyAttackText.gameObject.SetActive (false);
				playerAttackText.CrossFadeAlpha (1, 0, false);
				if (enemyRow == -1)
				{
					GateSet();
					playerAttackText.text = "Row " + (playerRow + 1) + " of Player attacking Gate of Enemy";
				}
				else
					playerAttackText.text = "Row " + (playerRow + 1) + " of Player attacking Row " + (enemyRow + 1) + " of Enemy"; 
				
			} else {
				playerRow = BattleLogic._instance.orderOfEnemyAttackOnPlayerRows [attackNoOfEnemy];
				enemyRow = BattleLogic._instance.orderOfEnemyAttack [attackNoOfEnemy];
				playerAttackText.gameObject.SetActive (false);
				enemyAttackText.gameObject.SetActive (true);
				enemyAttackText.CrossFadeAlpha (1, 0, false);
				enemyAttackText.text = "Row " + (enemyRow + 1) + " of Enemy attacking Row " + (playerRow + 1) + " of Player"; 
			}
			enemyDeckVisible = GameObject.Find ("enemies" + (enemyRow + 1));
			playerDeckVisible = GameObject.Find ("players" + (playerRow + 1));
			iTween.MoveTo (enemyDeckVisible, iTween.Hash ("x", enemyPos.x, "time", 0.5 * speedFactor, "onComplete", "StartAttack", "onCompleteTarget", this.gameObject));
			iTween.MoveTo (playerDeckVisible, iTween.Hash ("x", playerPos.x, "time", 0.5 * speedFactor));
		}
		else
		{
			ShowAvatarFight();
		}
	}

	int playerRow;
	int enemyRow;
	bool skillWasActivated;
	public GameObject enemyDeckVisible;
	public GameObject playerDeckVisible;
	string skillName;
	void StartAttack()
	{
		if (attackOrderNoCarriedOut < BattleLogic._instance.orderOfAttacks.Count) {
			if (BattleLogic._instance.orderOfAttacks [attackOrderNoCarriedOut] == 1) { // player is attacking
				playerRow = BattleLogic._instance.orderOfPlayerAttack [attackNoOfPlayer];
				enemyRow = BattleLogic._instance.orderOfPlayerAttackOnEnemyRows [attackNoOfPlayer];
				playerCounter = BattleLogic._instance.orderOfSkillActivation [attackOrderNoCarriedOut];
				if (playerCounter > -1) {
					skillName = BattleLogic._instance.activatedSkillName [attackOrderNoCarriedOut];
					Debug.Log ("skillName = "+skillName);
					//display skill
					ShowSkillEffects();
					skillWasActivated = true;
					BattleLogic._instance.orderOfSkillActivation [attackOrderNoCarriedOut] = -1;
					iTween.MoveTo (playerCards [playerRow].cardEntity [playerCounter], iTween.Hash ("y", playerUp.transform.position.y, "easeType", iTween.EaseType.linear, "time", 0.1 * speedFactor, "onComplete", "playerScaleTo", "onCompleteTarget", this.gameObject));
			
				} else {
					for (int i=0; i<playerCards[playerRow].cardEntity.Length; i++) {
						if (playerCards [playerRow].cardEntity [i] != null) {
							playerFirstPos = playerCards [playerRow].cardEntity [i].transform.position;
						
							if (i == 0) {
								iTween.MoveTo (playerCards [playerRow].cardEntity [i], iTween.Hash ("y", playerUp.transform.position.y, "time", 0.2 * speedFactor, "onComplete", "ScalePlayer", "onCompleteTarget", this.gameObject));
							} else {
								iTween.MoveTo (playerCards [playerRow].cardEntity [i], iTween.Hash ("y", playerUp.transform.position.y, "time", 0.2 * speedFactor));
							}
						}
					}
					attackOrderNoCarriedOut++;
					attackNoOfPlayer++;
				}
			} else {
				//enemy is attacking

				playerRow = BattleLogic._instance.orderOfEnemyAttackOnPlayerRows [attackNoOfEnemy];
				enemyRow = BattleLogic._instance.orderOfEnemyAttack [attackNoOfEnemy];
				enemyCounter = BattleLogic._instance.orderOfSkillActivation [attackOrderNoCarriedOut];
				if (enemyCounter > -1) {
					//display skill
					skillName = BattleLogic._instance.activatedSkillName [attackOrderNoCarriedOut];
					skillWasActivated = true;
					ShowEnemySkillEffects ();
					BattleLogic._instance.orderOfSkillActivation [attackOrderNoCarriedOut] = -1;
					iTween.MoveTo (enemyCards [enemyRow].cardEntity [enemyCounter], iTween.Hash ("y", enemyUp.transform.position.y, "easeType", iTween.EaseType.linear, "time", 0.1 * speedFactor, "onComplete", "enemyScaleTo", "onCompleteTarget", this.gameObject));

				} else {
					for (int i=0; i<enemyCards[enemyRow].cardEntity.Length; i++) {
						if (enemyCards [enemyRow].cardEntity [i] != null) {
							enemyFisrtPos = enemyCards [enemyRow].cardEntity [i].transform.position;
							if (i == 0) {
								iTween.MoveTo (enemyCards [enemyRow].cardEntity [i], iTween.Hash ("y", enemyUp.transform.position.y, "time", 0.2 * speedFactor, "onComplete", "ScaleEnemy", "onCompleteTarget", this.gameObject));
							
							} else {
								iTween.MoveTo (enemyCards [enemyRow].cardEntity [i], iTween.Hash ("y", enemyUp.transform.position.y, "time", 0.2 * speedFactor));
							}
						}
					}
					attackOrderNoCarriedOut++;
					attackNoOfEnemy++;
				}
			}
		} else {
			//AVATaR BATTLES
		}

	}

	void ShowSkillEffects()
	{
		Invoke("clickAddedUp",0f);
		Invoke("clickAddedUp",0.3f);
		Invoke("clickAddedUp",0.5f);
		Invoke("clickAddedUp",0.7f);
	}
	

	void ScalePlayer()
	{
		if(enemyRow > -1)
			iTween.ShakePosition(GameObject.Find("enemies"+(enemyRow+1)),iTween.Hash("x",0.1f,"y",0.1f,"time",2.0f));
		for (int i=0; i<playerCards[playerRow].cardEntity.Length; i++) {
			if(playerCards [playerRow].cardEntity [i] != null)
			{
				if (i == 0) {
					iTween.ScaleTo (playerCards [playerRow].cardEntity [i], iTween.Hash ("x", playerScale.x + 0.01, "y", playerScale.y + 0.01, "time", 0.3 * speedFactor, "onComplete", "ScalePlayerReset", "onCompleteTarget", this.gameObject));
				} else {
					iTween.ScaleTo (playerCards [playerRow].cardEntity [i], iTween.Hash ("x", playerScale.x + 0.01, "y", playerScale.y + 0.01, "time", 0.3 * speedFactor));

				}
				playerCards [playerRow].cardEntity [i].GetComponent<Image> ().color = new Color (1, 1, 1, 0.5f);
			}
		}
	}

	void ScalePlayerReset()
	{
		for(int i=0;i<playerCards[playerRow].cardEntity.Length;i++)
		{
			if(playerCards [playerRow].cardEntity [i] != null)
			{
				iTween.ScaleTo(playerCards[playerRow].cardEntity[i],iTween.Hash("x",playerScale.x,"y",playerScale.y,"time",0.3*speedFactor,"onComplete","playerReset","onCompleteTarget",this.gameObject));
				playerCards[playerRow].cardEntity[i].GetComponent<Image>().color=new Color(1,1,1,1);
			}
		}
		ChangeHealths ();
		skillWasActivated = false;
	}
	void playerReset()
	{
		for(int i=0;i<playerCards[playerRow].cardEntity.Length;i++)
		{
			if(playerCards[playerRow].cardEntity[i] != null)
			{
				playerCards[playerRow].cardEntity[i].transform.localScale=playerScale;
				if(i == 0)
					iTween.MoveTo(playerCards[playerRow].cardEntity[i],iTween.Hash("y",playerFirstPos.y,"time",1*speedFactor,"onComplete","StartButton","onCompleteTarget",this.gameObject));
				else
					iTween.MoveTo(playerCards[playerRow].cardEntity[i],iTween.Hash("y",playerFirstPos.y,"time",1*speedFactor));
					
			}
		}


		//		healthBarEnemy();
		// CHANGE HEALTH BAR!
	}

#region Player Skill Display
	void playerScaleTo ()
	{
		iTween.ScaleTo(playerCards[playerRow].cardEntity[playerCounter],iTween.Hash("x",playerScale.x+0.01,"y",playerScale.y+0.01,"time",1*speedFactor,"onComplete","playerSkill","onCompleteTarget",this.gameObject));
		playerCards[playerRow].cardEntity[playerCounter].GetComponent<Image>().color=new Color(1,1,1,0.5f);
		
	}
	int noOfPlayerSkilsActivated;
	int noOfEnemySkilsActivated;
	void playerSkill() // Skill of player shows
	{
		defencePlayer [noOfPlayerSkilsActivated].GetComponent<Image> ().sprite = playerCards [playerRow].cardEntity [playerCounter].GetComponent<Image> ().sprite;
		defencePlayer [noOfPlayerSkilsActivated].GetComponentInChildren<Text> ().text = skillName;
		iTween.MoveTo(defencePlayer[noOfPlayerSkilsActivated],iTween.Hash("x",playerSkillPosistion.transform.position.x,"time",0.3*speedFactor,"onComplete","playerSkillScaleReset","onCompleteTarget",this.gameObject));
	}

	void playerSkillScaleReset()
	{
		iTween.ScaleTo(playerCards[playerRow].cardEntity[playerCounter],iTween.Hash("x",playerScale.x,"y",playerScale.y,"time",1*speedFactor,"onComplete","destroySkill","onCompleteTarget",this.gameObject));
		playerCards[playerRow].cardEntity[playerCounter].GetComponent<Image>().color=new Color(1,1,1,1);
		Invoke("playerSkillReset",0.3f*speedFactor);
	}
	void playerSkillReset()
	{
		iTween.MoveTo(playerCards[playerRow].cardEntity[playerCounter],iTween.Hash("y",playerPos.y,"time",0.5f*speedFactor));
//		isDefenceAcite="2";
	}
	void destroySkill()
	{
		Destroy(defencePlayer[noOfPlayerSkilsActivated]);
		noOfPlayerSkilsActivated++;
		StartAttack();
	}
#endregion
	//-----------------------
	void enemyScaleTo ()
	{
//		if(PlayerPrefs.GetString("avatar")=="yes")
//		{
//			print("reset");
//			iTween.ScaleTo(enemyCards[enemyRow].cardEntity[enemyCounter],iTween.Hash("x",enemyScale.x+0.01,"y",enemyScale.y+0.01,"time",1*speedFactor,"onComplete","enemyReset","onCompleteTarget",this.gameObject));
//			enemyCards[enemyRow].cardEntity[enemyCounter].GetComponent<Image>().color=new Color(1,1,1,0.5f);
//		}
//		else
//		{
			iTween.ScaleTo(enemyCards[enemyRow].cardEntity[enemyCounter],iTween.Hash("x",enemyScale.x+0.01,"y",enemyScale.y+0.01,"time",1*speedFactor,"onComplete","enemySkill","onCompleteTarget",this.gameObject));
			enemyCards[enemyRow].cardEntity[enemyCounter].GetComponent<Image>().color=new Color(1,1,1,0.5f);
//		}
	}
	void enemySkill()
	{
		defenceEnemy [noOfEnemySkilsActivated].GetComponent<Image> ().sprite = enemyCards [enemyRow].cardEntity [enemyCounter].GetComponent<Image> ().sprite;
		defenceEnemy [noOfEnemySkilsActivated].GetComponentInChildren<Text> ().text = skillName;
		iTween.MoveTo(defenceEnemy[noOfEnemySkilsActivated],iTween.Hash("x",enemySkillPosition.transform.position.x,"time",0.3*speedFactor,"onComplete","enemySkillScaleReset","onCompleteTarget",this.gameObject));
	}
	void enemySkillScaleReset()
	{
		iTween.ScaleTo(enemyCards[enemyRow].cardEntity[enemyCounter],iTween.Hash("x",enemyScale.x,"y",enemyScale.y,"time",1*speedFactor,"onComplete","destroySkillEnemy","onCompleteTarget",this.gameObject));
		enemyCards[enemyRow].cardEntity[enemyCounter].GetComponent<Image>().color=new Color(1,1,1,1);
		Invoke("enemySkillReset",0.3f*speedFactor);
		
		
	}
	void destroySkillEnemy()
	{
		Destroy(defenceEnemy[noOfEnemySkilsActivated]);
		noOfEnemySkilsActivated++;
		StartAttack();
	}
	
	void enemySkillReset()
	{
		iTween.MoveTo(enemyCards[enemyRow].cardEntity[enemyCounter],iTween.Hash("y",enemyPos.y,"time",0.5f*speedFactor));
	}


	//---------------


	void destroySmash()
	{
		Destroy(smashPrefab.gameObject);
	}




	void startPlayer()
	{
		if(PlayerPrefs.GetString("avatar")=="yes")
		{
			for(int i=0;i<playerCards[0].cardEntity.Length;i++)
			{
				if(playerCards[0].cardEntity [i] != null)
				{
					iTween.MoveTo(playerCards[0].cardEntity[i],iTween.Hash("y",playerUp.transform.position.y,"time",0.2*speedFactor,"onComplete","ScalePlayer","onCompleteTarget",this.gameObject));
				}
			}
		}
		else
		{
			for(int i=0;i<playerCards[0].cardEntity.Length;i++)
			{
				if(playerCards[0].cardEntity [i] != null)
				{
					playerFirstPos = playerCards[0].cardEntity[i].transform.position;

					if(i==playerCards[0].cardEntity.Length-1)
					{
						iTween.MoveTo(playerCards[0].cardEntity[i],iTween.Hash("y",playerUp.transform.position.y,"time",0.2*speedFactor,"onComplete","ScalePlayer","onCompleteTarget",this.gameObject));

					}
					else
					{
						iTween.MoveTo(playerCards[0].cardEntity[i],iTween.Hash("y",playerUp.transform.position.y,"time",0.2*speedFactor));

					}
				}
			}
			if(rowCounter >= 0 && rowCounter <= 3)
			{
				fadeText();
			}
		}
	}

	void ShowEnemySkillEffects()
	{
		Invoke("clickAdded",0f);
		Invoke("clickAdded",0.3f);
		Invoke("clickAdded",0.5f);
		Invoke("clickAdded",0.7f);
	}

	//-----------------------------------------------------------------------------------------------------
	
	void enemyReset()
	{
//		if(PlayerPrefs.GetString("avatar")=="yes")
//		{
//			iTween.MoveTo(enemyCards[0].cardEntity[0],iTween.Hash("y",avatarPos.transform.position.y,"time",1*speedFactor));
//
//		}
//		else
//		{
		for(int i=0;i<enemyCards[enemyRow].cardEntity.Length;i++)
		{
			if(enemyCards[enemyRow].cardEntity[i] != null)
			{

				if(i == 0)
					iTween.MoveTo(enemyCards[enemyRow].cardEntity[i],iTween.Hash("y",enemyFisrtPos.y,"time",1*speedFactor,"onComplete","StartButton","onCompleteTarget",this.gameObject));
				else
					iTween.MoveTo(enemyCards[enemyRow].cardEntity[i],iTween.Hash("y",enemyFisrtPos.y,"time",1*speedFactor));
			}
		
		}

		Invoke("rowChange",1*speedFactor);
//		}
	}


	void ScaleEnemy()
	{
		Debug.Log ("enemyRow = "+enemyRow);
		for (int i=0; i<enemyCards[enemyRow].cardEntity.Length; i++) {
			if(enemyCards[enemyRow].cardEntity[i].gameObject != null)
			{
				if(i == 0)
					iTween.ScaleTo (enemyCards [enemyRow].cardEntity [i], iTween.Hash ("x", enemyScale.x + 0.01, "y", enemyScale.y + 0.01, "time", 0.3 * speedFactor, "onComplete", "ScaleEnemyReset", "onCompleteTarget", this.gameObject));
				else
					iTween.ScaleTo (enemyCards [enemyRow].cardEntity [i], iTween.Hash ("x", enemyScale.x + 0.01, "y", enemyScale.y + 0.01, "time", 0.3 * speedFactor));
				enemyCards [enemyRow].cardEntity [i].GetComponent<Image> ().color = new Color (1, 1, 1, 0.5f);
			}
		}
	}

	void ScaleEnemyReset()
	{
		for(int i=0;i<enemyCards[enemyRow].cardEntity.Length;i++)
		{
			if(enemyCards[enemyRow].cardEntity[i] != null)
			{
				if(i == 0)
					iTween.ScaleTo(enemyCards[enemyRow].cardEntity[i],iTween.Hash("x",enemyScale.x,"y",enemyScale.y,"time",0.3*speedFactor,"onComplete","enemyReset","onCompleteTarget",this.gameObject));
				else
					iTween.ScaleTo(enemyCards[enemyRow].cardEntity[i],iTween.Hash("x",enemyScale.x,"y",enemyScale.y,"time",0.3*speedFactor,"onComplete","enemyReset","onCompleteTarget",this.gameObject));
				enemyCards[enemyRow].cardEntity[i].GetComponent<Image>().color=new Color(1,1,1,1);
			}

		}
		ChangeHealths ();
	}

}

