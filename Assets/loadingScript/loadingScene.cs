
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using Facebook.Unity;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class loadingScene : MonoBehaviour
{
	private static bool IsUserInfoLoaded = false;

	private static bool IsAuntifivated = false;
	private static string TWITTER_CONSUMER_KEY = "6Kria8TAVlXSN1I0hK5SvdjSw";
	private static string TWITTER_CONSUMER_SECRET = "SjQ7ZWJxFgM5BmGQc55Jol0MoxxqmNbBMMX3SsFHrjyXyumhgt";


	public Sprite[] playerSprite;
	public AudioSource[] allSounds;
	public AudioClip[] allClips; //0 - main , 1- quest , 2 -  battle
	public AudioSource audioSource;
	public Slider[] bgmSliders;
	public static loadingScene Instance;
	public bool sound=true;
	public bool notificationSound=true;
	public GameObject [] loads;
	public GameObject [] OnButton;
	public GameObject [] OffButton;

	public GameObject [] OnNotification;
	public GameObject [] OffNotificaation;

	public GameObject audio;
	public List <GameObject> scenes = new List<GameObject>();
	public float sliderValue=1;
	public float sliderValueSfx=1;

	// ------- PRIMARY/ SECONDAY CASTLE --------
	public Image castlePrimaryImage;
	public string buttonName;
	public float castlePrimaryClockText;
	public Text castlePrimaryClockString;
	public Button castleUpgradeButton;
	public bool isCastlePrimary=false;
	public Sprite preloadImage;
	public DateTime currentDate;
	public DateTime oldDate;
	public Button confirmButton;
	public GameObject castle;
//	public Image updateCastle;
	public Text castleUpdateImageText;
//	Vector3 castleUpdateImageV;
	public Text castleLevel;
	public Text castleNow, castleNext;

	public GameObject  disableClick;
	//-------- PRIMARY STORAGE --------

//	Vector3 storageUpdateImageV;
	public Image storagePrimaryImage;
	public float storagePrimaryClockText;
	public Text storagePrimaryClockString;
//	public Image updateStorage;
	public Text storageUpdateImageText;
	public Text storageLevel;
	public Text storageNow, storageNext;
	public Button storageUpgradeButton;
	public bool isStoragePrimary=false;
	public Sprite StoragepreloadImage;
	public Image storageSecondaryImage;
	public float storageSecondaryText;
	public Text storageSecondaryString;
	public Button storageLockDownButton;
	public bool isStorageSecondary=false;
	public Sprite StoragepreloadImageSecondary;
	public GameObject storage;

	//-------------  GATE------
	public Image gatePrimaryImage;
	public float gatePrimaryClockText;
	public Text gatePrimaryClockString;
	//	public Image updateStorage;
	public Text gateUpdateImageText;
	public Text gateLevel;
	public Text gateNow, gateNext;
	public Button gateUpgradeButton;
	public bool isGatePrimary=false;
	public Sprite gatepreloadImage;
	public Image gateSecondaryImage;
	public float gateSecondaryText;
	public Text gateSecondaryString;
	public Button gateLockDownButton;
	public bool isGateSecondary=false;
	public Sprite gatepreloadImageSecondary;
	public GameObject gate;


	//------------- BARN --------------
//	Vector3 barnUpdateImageV;
	public Image barnPrimaryImage;
	public float barnPrimaryClockText;
	public Text barnPrimaryClockString;
//	public Image updateBarn;
	public Text barnUpdateImageText;
	public Text barnLevel;
	public Text barnNow, barnNext;
	public Button barnUpgradeButton;
	public bool isBarnPrimary=false;
	public Sprite BarnloadImage;
	public Image barnSecondaryImage;
	public float barnSecondaryText;
	public Text barnSecondaryString;
	public Button barnHarvest;
	public bool isBarnSecondary=false;
	public Sprite BarnloadImageSecondary;
	public GameObject barn;

 	//--------- BARRACKS -----------------
	public Image barrackPrimaryImage;
	public float barrackPrimaryClockText;
	public Text barrackPrimaryClockString;
	//	public Image updateBarn;
	public Text barrackUpdateImageText;
	public Text barrackLevel;
	public Text barrackIncreaseLimitNow, barrackIncreaseLimitNext;
	public Button barrackUpgradeButton;
	public bool isBarrackPrimary=false;
	public Sprite BarrackloadImage;
	public Image barrackSecondaryImage;
	public float barrackSecondaryText;
	public Text barrackSecondaryString;
	public Button soldierRecruit;
	public bool isBarrackSecondary=false;
	public Sprite BarrackloadImageSecondary;
	public GameObject Barrack;

	//------------- GOLD MINE --------------
	public Image goldMinePrimaryImage;
	public float goldMinePrimaryClockText;
	public Text goldMinePrimaryClockString;
//	public Image updategoldMine;
	public Text goldMineUpdateImageText;
	public Text goldMineLevel;
	public Text goldMineNow, goldMineNext;
	public Button goldMineUpgradeButton;
	public bool isgoldMinePrimary=false;
	public Sprite goldMineloadImage;
	public Image goldMineSecondaryImage;
	public float goldMineSecondaryText;
	public Text goldMineSecondaryString;
	public Button goldMineHarvest;
	public bool isgoldMineSecondary=false;
	public Sprite goldMineloadImageSecondary;
	public GameObject goldMine;

	//----------- TRAINING GROUND ----------
	public Image trainingGroundPrimaryImage;
	public float trainingGroundPrimaryClockText;
	public Text trainingGroundPrimaryClockString;
	//	public Image updategoldMine;
	public Text trainingGroundUpdateImageText;
	public Text trainingGroundLevel;
	public Text GroundNow, GroundNew;
	public Text GroundNowSkill, GroundNewSkill;

	public Button trainingGroundUpgradeButton;
	public bool istrainingGroundPrimary=false;
	public Sprite trainingGroundloadImage;
	public Image trainingGroundSecondaryImage;
	public float trainingGroundSecondaryText;
	public Text trainingGroundSecondaryString;
	public Button trainingGroundTraining;
	public bool istrainingGroundSecondary=false;
	public Sprite trainingGroundloadImageSecondary;
	public GameObject trainingGround;

	GameObject[] updateCards;
	public  List<int> randomCards;

	public GameObject settingsPanel;
	public GameObject playerProfilePanel;
	public GameObject menuPanel;
	public GameObject objectToOpenMenu;

	public string baseUrl;

	public Text instantGemsTextForPopup;
	public Text DeployedSoldiers, AvailableSoldiers;

	public GameObject loader;
	public Popup gameStopPopup;
	public Popup popupFromServer;

	public testBattleNew battleInstance;
	public BattleResult battleResultInstance;

	public TimeSpan tempMembership_time;

	public battleFormation myBattleFormation , myQuestFormation , eventQuestFormation;

	public Text ActivateMembershipShow_text,timeTillExpiry,timeLeft,DayText,VIPText,DiscountText;
	public GameObject ReferralUI,ReferralPanel,RewardsPanel, RulePanel , Days_7,Days_14,Days_30,VIP_UI;
	public Sprite BrownSprite, YellowSprite;
	public Button Share, Rewards, Rule,VIPButton;
	TimeSpan Difference,VipDiff;

	// Use this for initialization
	public void Awake()
	{
//		if (!FB.IsInitialized) {
//			// Initialize the Facebook SDK
//			FB.Init(InitCallback, OnHideUnity);
//		} else {
//			// Already initialized, signal an app activation App Event
//			FB.ActivateApp();
//		}

		SPTwitter.Instance.OnTwitterInitedAction += OnInit;
		SPTwitter.Instance.OnAuthCompleteAction += OnAuth;

		SPTwitter.Instance.OnPostingCompleteAction += OnPost;

		SPTwitter.Instance.OnUserDataRequestCompleteAction += OnUserDataLoaded;
		//You can use:
		//SPTwitter.instance.Init();
		//if TWITTER_CONSUMER_KEY and TWITTER_CONSUMER_SECRET was alredy set in 
		//Window -> Mobile Social Plugin -> Edit Settings menu.


		SPTwitter.Instance.Init(TWITTER_CONSUMER_KEY, TWITTER_CONSUMER_SECRET);

		PlayerPrefs.DeleteKey ("tempButtonStorage");
		PlayerPrefs.DeleteKey ("tempButton");
		PlayerPrefs.DeleteKey ("tempButtonGround");

		PlayerPrefs.DeleteKey ("chosenCardCastle");
		PlayerPrefs.DeleteKey ("updating");
		PlayerPrefs.DeleteKey ("updatingGround");

		PlayerPrefs.DeleteKey ("chosenCardGround");

		PlayerPrefs.DeleteKey ("chosenCardStorage");
		PlayerPrefs.DeleteKey ("updatingStorage");
		PlayerPrefs.DeleteKey ("tempButtonStorageSecondary");
		PlayerPrefs.DeleteKey ("updatingStorageSecondary");

		PlayerPrefs.DeleteKey ("chosenCardGate");
		PlayerPrefs.DeleteKey ("updatingGate");
		PlayerPrefs.DeleteKey ("tempButtonGateSecondary");
		PlayerPrefs.DeleteKey ("updatingGateSecondary");
		//--------------- Barn --------------
		PlayerPrefs.DeleteKey ("updatingBarnSecondary");
		PlayerPrefs.DeleteKey ("updatingBarn");
		PlayerPrefs.DeleteKey ("chosenCardBarn");
		PlayerPrefs.DeleteKey ("tempButtonBarn");

		PlayerPrefs.DeleteKey ("tempButtonGoldMiine");
		PlayerPrefs.DeleteKey ("updatingGoldMine");
		PlayerPrefs.DeleteKey ("chosenCardGoldMine");

		PlayerPrefs.DeleteKey ("tempButtonBarrack");
		PlayerPrefs.DeleteKey ("updatingBarrack");
		PlayerPrefs.DeleteKey ("chosenCardBarrack");
		PlayerPrefs.DeleteKey ("updatingBarrackSecondary");


		PlayerPrefs.DeleteKey ("tempButtonGround");
		PlayerPrefs.DeleteKey ("updatingGround");
		PlayerPrefs.DeleteKey ("chosenCardGround");
		PlayerPrefs.DeleteKey ("updatingGroundSecondary");

		PlayerPrefs.DeleteKey ("updatingPrison");
		PlayerPrefs.DeleteKey ("tempButtonPrison");
		PlayerPrefs.DeleteKey ("chosenCardPrison");
		PlayerPrefs.DeleteKey ("updatingPrisonSecondary");

		Instance=this;
	}

	private void OnPost(TWResult res) {
		if(res.IsSucceeded) {
			Debug.Log("=    == = = = =  yes message sent = = = = =");
		} else {
			Debug.Log("=    == = = = =  no message sent = = = = =");
		}
	}

	private void OnUserDataLoaded(TWResult res) {
		if(res.IsSucceeded) {
			IsUserInfoLoaded = true;
			SPTwitter.Instance.userInfo.LoadProfileImage();
			SPTwitter.Instance.userInfo.LoadBackgroundImage();
		} else {
			Debug.Log("Opps, user data load failed, something was wrong");
		}
	}

	private void OnInit(TWResult res) {
		if(SPTwitter.instance.IsAuthed) {
			OnAuth(null);
		}
	}


	private void OnAuth(TWResult res) {
		IsAuntifivated = true;
	}


	public void cardLocked()
	{
		GameObject[] deleteCards = GameObject.FindGameObjectsWithTag ("updateCards");
		for (int i = 0; i < deleteCards.Length; i ++) {

			deleteCards [i].GetComponent<Button> ().interactable = false;



		}
	}
	public void cardUnlocked()
	{
		GameObject[] deleteCards = GameObject.FindGameObjectsWithTag ("updateCards");
		for (int i = 0; i < deleteCards.Length; i ++)
		{
			deleteCards [i].GetComponent<Button> ().interactable = true;
		}
	}

	public void emailPost()
	{
		//		MailMessage mail = new MailMessage();
		//
		//		mail.From = new MailAddress("luckypawar24@gmail.com");
		//		mail.To.Add("ommziunity02@gmail.com");
		//		mail.Subject = "Test Mail";
		//		mail.Body = "This is for testing SMTP mail from GMAIL";
		//
		//		SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
		//		smtpServer.Port = 587;
		//		smtpServer.Credentials = new System.Net.NetworkCredential ("luckypawar24@gmail.com","" ) as ICredentialsByHost;
		//		smtpServer.EnableSsl = true;
		//		ServicePointManager.ServerCertificateValidationCallback = 
		//			delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) 
		//		{ return true; };
		//		smtpServer.Send(mail);
		//		Debug.Log("success");

	}


	public void shareTwitter()
	{
		SPTwitter.instance.PostWithAuthCheck ("Myriad Of Dragons : Come and Play this wonderful Game" +TimeManager._instance.GetCurrentServerTime().ToString());

		//		if(!IsAuntifivated) {
		//			SPTwitter.instance.AuthenticateUser();
		//			//PostMessage ();
		//
		//		} else {
		//			//PostMessage ();
		//			LogOut();
		//		}
	}

	public void Sharebutton()
	{
		FB.FeedShare(
			string.Empty,
			//null,
//			new Uri("https://play.google.com/store/apps/details?id=com.ommzi.sardaarg&hl=en"),
			null,
			"MyRiad of Dragons",
			"Referral",
			"Come and Play this wonderful Game",
			//null,
			null,
			string.Empty,
			ShareCallBack


			//			string toId = "",
			//			Uri link = null,
			//			string linkName = "",
			//			string linkCaption = "",
			//			string linkDescription = "",
			//			Uri picture = null,
			//			string mediaSource = "",


		);
	}

	void ShareCallBack(IResult result)
	{
		if(result.Cancelled)
		{
			Debug.Log("Share Cancelled");
		}
		else if(!string.IsNullOrEmpty(result.Error))
		{
			Debug.Log("Error on share!");
		}
		else if(!string.IsNullOrEmpty(result.RawResult))
		{
			Debug.Log("Success on share");
		}
	}

	//	public void faceBookShare()
	//	{
	//		FacebookController._instance.FbShare ((success) =>{
	//			if(success)
	//			{
	//				popupFromServer.ShowPopup("Successfully Share");
	//			}
	//			else
	//			{
	//				popupFromServer.ShowPopup("Could not Login");
	//			}
	//		});
	//		//FacebookController._instance.FbShare ();
	//		
	//	}

	public void Start ()
	{
		VIP_UI.SetActive(false);
		//secondaryBarrackShow ();
		castlePrimaryClockText=EmpireManager._instance.castle.timeRequiredPerLevel[EmpireManager._instance.castle.currentLevel]*3600f;
		castleNow.text=EmpireManager._instance.castle.finalValue1[EmpireManager._instance.castle.currentLevel].ToString();
		castleNext.text=EmpireManager._instance.castle.finalValue1[EmpireManager._instance.castle.currentLevel+1].ToString();


		//======= storage ====
		storagePrimaryClockText=EmpireManager._instance.storage.timeRequiredPerLevel[EmpireManager._instance.storage.currentLevel]*3600f;
		storageNow.text=EmpireManager._instance.storage.finalValue1[EmpireManager._instance.storage.currentLevel].ToString();
		storageNext.text=EmpireManager._instance.storage.finalValue1[EmpireManager._instance.storage.currentLevel+1].ToString();

		EmpireManager._instance.storage.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString();
		EmpireManager._instance.castle.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString();


		EmpireManager._instance.storage.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString();
		EmpireManager._instance.castle.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString();

		//--------- BARN ------------
		barnPrimaryClockText=EmpireManager._instance.barn.timeRequiredPerLevel[EmpireManager._instance.barn.currentLevel]*3600f;
		barnNow.text=EmpireManager._instance.barn.finalValue1[EmpireManager._instance.barn.currentLevel].ToString();
		barnNext.text=EmpireManager._instance.barn.finalValue1[EmpireManager._instance.barn.currentLevel+1].ToString();

		EmpireManager._instance.barn.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString();
		EmpireManager._instance.barn.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString();
		//---------------------------

		//---------- GOLD MINE -------------
		goldMinePrimaryClockText=EmpireManager._instance.goldMine.timeRequiredPerLevel[EmpireManager._instance.goldMine.currentLevel]*3600f;
		goldMineNow.text=EmpireManager._instance.goldMine.finalValue1[EmpireManager._instance.goldMine.currentLevel].ToString();
		goldMineNext.text=EmpireManager._instance.goldMine.finalValue1[EmpireManager._instance.goldMine.currentLevel+1].ToString();
		EmpireManager._instance.goldMine.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString();
		EmpireManager._instance.goldMine.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString();


		//-------- BARRACK --------------
		barrackPrimaryClockText=EmpireManager._instance.barracks.timeRequiredPerLevel[EmpireManager._instance.barracks.currentLevel]*3600f;
		barrackIncreaseLimitNow.text=EmpireManager._instance.barracks.finalValue1[EmpireManager._instance.barracks.currentLevel].ToString();
		barrackIncreaseLimitNext.text=EmpireManager._instance.barracks.finalValue1[EmpireManager._instance.barracks.currentLevel+1].ToString();
		EmpireManager._instance.barracks.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString();
		EmpireManager._instance.barracks.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
		//Debug.Log(EmpireManager._instance.castle.requiredExpPerLevel[EmpireManager._instance.castle.currentLevel]);


		//-------  TRAINING GROUND ----------
		trainingGroundPrimaryClockText=EmpireManager._instance.trainingGround.timeRequiredPerLevel[EmpireManager._instance.trainingGround.currentLevel]*3600f;
		GroundNow.text=EmpireManager._instance.trainingGround.finalValue1[EmpireManager._instance.trainingGround.currentLevel].ToString();
		GroundNew.text=EmpireManager._instance.trainingGround.finalValue1[EmpireManager._instance.trainingGround.currentLevel+1].ToString();
		GroundNowSkill.text=EmpireManager._instance.trainingGround.finalValue1[EmpireManager._instance.trainingGround.currentLevel].ToString();
		GroundNewSkill.text=EmpireManager._instance.trainingGround.finalValue1[EmpireManager._instance.trainingGround.currentLevel+1].ToString();
		EmpireManager._instance.trainingGround.foodText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString();
		EmpireManager._instance.trainingGround.goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
		EmpireManager._instance.trainingGround.foodText2.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString();
		EmpireManager._instance.trainingGround.goldText2.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
		EmpireManager._instance.trainingGround.foodText3.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString();
		EmpireManager._instance.trainingGround.goldText3.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
		EmpireManager._instance.trainingGround.foodText4.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString();
		EmpireManager._instance.trainingGround.goldText4.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();



	}

	public bool readyTogo;
	public IEnumerator CheckForCastleInStart()
	{
		while (!readyTogo) {
			yield return 0;
		}
		float percentageVal = (EmpireManager._instance.castle.currentExp/(float)EmpireManager._instance.castle.requiredExpPerLevel[EmpireManager._instance.castle.currentLevel]);
		castleUpdateImageText.text = Mathf.FloorToInt (percentageVal*100)+"%";
//		float finalScale = 1+(7/100f*percentageVal);
//		float finalScale = (8/100f*percentageVal);
		EmpireManager._instance.castle.levelSlider.value = percentageVal;

//		castleUpdateImageV = updateCastle.transform.localScale;
//		iTween.ScaleTo(updateCastle.gameObject,iTween.Hash("x",finalScale,"time",1.5f));
		if (EmpireManager._instance.castle.primaryCardNo > 0) {
			Debug.Log (" primary card no was bigger.......");
			if (EmpireManager._instance.castle.timeOfLockOfPrimary != null) {
				Debug.Log (" i have timelock primary.......");
				System.TimeSpan diff = TimeManager._instance.GetCurrentServerTime () - EmpireManager._instance.castle.timeOfLockOfPrimary;
				float diffSeconds = (float)diff.TotalSeconds;
				if ((float)EmpireManager._instance.castle.timeRequiredPerLevel [EmpireManager._instance.castle.currentLevel] * 3600 > diffSeconds && diffSeconds > 0) {
					Debug.Log (" differnce is greater...........");

					int spriteToFetch = 0;
					for (int i = 0; i < CardsManager._instance.mycards.Count; i++) {
						if (EmpireManager._instance.castle.primaryCardNo == CardsManager._instance.mycards [i].card_id_in_playerList) {
							spriteToFetch = i;
							break;
						}
					}
					loadingScene.Instance.randomCards.Remove (EmpireManager._instance.castle.primaryCardNo);
					PlayerPrefs.SetString ("tempButton", EmpireManager._instance.castle.primaryCardNo.ToString ());
					EmpireManager._instance.castle.instantUpdateButton.interactable = true;
					isCastlePrimary = true;
					castlePrimaryClockText = EmpireManager._instance.castle.timeRequiredPerLevel [EmpireManager._instance.castle.currentLevel] * 3600 - diffSeconds;
					castlePrimaryImage.sprite = CardsManager._instance.mycards [spriteToFetch].cardSpriteFromResources;
					PlayerPrefs.SetString ("updating", "yes");
					// add card to the primary image
				} else {
					isCastlePrimary = false;
					PlayerPrefs.SetString ("updating", "no");
					PlayerPrefs.SetString ("tempButton", EmpireManager._instance.castle.primaryCardNo.ToString ());
					empireScene.instance.buttonName = "castle";

					loadingScene.Instance.randomCards.Remove (EmpireManager._instance.castle.primaryCardNo);
					instantUpdateCastle ();
					// it will be already updated...
					// Change castle experience , level
				}
			}
		} else {
			PlayerPrefs.SetString ("updating", "no");
			isCastlePrimary = false;
		}
	}

	public IEnumerator CheckForStorageInStart()
	{
		while (!readyTogo) {
			yield return 0;
		}
		float percentageVal = (EmpireManager._instance.storage.currentExp/(float)EmpireManager._instance.storage.requiredExpPerLevel[EmpireManager._instance.storage.currentLevel]);
		storageUpdateImageText.text = Mathf.FloorToInt (percentageVal*100)+"%";
		EmpireManager._instance.storage.levelSlider.value = percentageVal;

//		float finalScale = 1+(7/100f*percentageVal);
//		float finalScale = (8/100f*percentageVal);
//		storageUpdateImageV = updateStorage.transform.localScale;
//		iTween.ScaleTo(updateStorage.gameObject,iTween.Hash("x",finalScale,"time",1.5f));
		if (EmpireManager._instance.storage.primaryCardNo > 0) {
			if (EmpireManager._instance.storage.timeOfLockOfPrimary != null) {
				System.TimeSpan diff = TimeManager._instance.GetCurrentServerTime () - EmpireManager._instance.storage.timeOfLockOfPrimary;
				float diffSeconds = Mathf.Abs ((float)diff.TotalSeconds);
				Debug.Log ("diffSeconds  = " + diffSeconds);
				Debug.Log ("timeReqd = " + EmpireManager._instance.storage.timeRequiredPerLevel [EmpireManager._instance.storage.currentLevel] * 3600);
				if (EmpireManager._instance.storage.timeRequiredPerLevel [EmpireManager._instance.storage.currentLevel] * 3600 > diffSeconds && diffSeconds > 0) {
					Debug.Log ("time done???");
					isStoragePrimary = true;
					PlayerPrefs.SetString ("updatingStorage", "yes");
					PlayerPrefs.SetString ("tempButtonStorage", EmpireManager._instance.storage.primaryCardNo.ToString ());

					loadingScene.Instance.randomCards.Remove (EmpireManager._instance.storage.primaryCardNo);
					int spriteToFetch = 0;
					for (int i = 0; i < CardsManager._instance.mycards.Count; i++) {
						if (EmpireManager._instance.storage.primaryCardNo == CardsManager._instance.mycards [i].card_id_in_playerList) {
							spriteToFetch = i;
							break;
						}
					}
					EmpireManager._instance.storage.instantUpdateButton.interactable = true;
					storagePrimaryClockText = EmpireManager._instance.storage.timeRequiredPerLevel [EmpireManager._instance.storage.currentLevel] * 3600 - diffSeconds;
					storagePrimaryImage.sprite = CardsManager._instance.mycards [spriteToFetch].cardSpriteFromResources;
					// add card to the primary image
				} else {
					PlayerPrefs.SetString ("tempButtonStorage", EmpireManager._instance.storage.primaryCardNo.ToString ());
					empireScene.instance.buttonName = "storage";

					loadingScene.Instance.randomCards.Remove (EmpireManager._instance.storage.primaryCardNo);
					instantStorage ();
					EmpireManager._instance.storage.primaryCardNo = -1;
					isStoragePrimary = false;
					PlayerPrefs.SetString ("updatingStorage", "no");
				}
			}
			else {
				isStoragePrimary = false;
				PlayerPrefs.SetString ("updatingStorage", "no");
			}
		} else {
			isStoragePrimary = false;
			PlayerPrefs.SetString ("updatingStorage", "no");
		}
		if(EmpireManager._instance.storage.secondaryCardNo > 0)
		{
			if(EmpireManager._instance.storage.timeOfLockOfSecondary != null)
			{
				System.TimeSpan diff = TimeManager._instance.GetCurrentServerTime () - EmpireManager._instance.storage.timeOfLockOfSecondary;
				float diffSeconds = Mathf.Abs((float)diff.TotalSeconds);
				float timeForActive = 3600;
				if(timeForActive > diffSeconds && diffSeconds > 0)
				{
					isStorageSecondary = true;
					int spriteToFetch = 0;
					for(int i = 0 ; i < CardsManager._instance.mycards.Count ; i++)
					{
						if(EmpireManager._instance.storage.secondaryCardNo == CardsManager._instance.mycards[i].card_id_in_playerList)
						{
							spriteToFetch = i;
							break;
						}
					}

					loadingScene.Instance.randomCards.Remove (EmpireManager._instance.storage.secondaryCardNo);
					storageSecondaryText = 3600 - diffSeconds;
					storageSecondaryImage.sprite = CardsManager._instance.mycards[spriteToFetch].cardSpriteFromResources;
				}
				else
				{
					storageSecondaryText = 0;
					isStorageSecondary = true;

					loadingScene.Instance.randomCards.Remove (EmpireManager._instance.storage.secondaryCardNo);
					updateStorageSecondary();
					isStorageSecondary = false;
					EmpireManager._instance.storage.secondaryCardNo = -1;
				}
			}
			else {
				isStorageSecondary = false;
			}
		}
		else {
			isStorageSecondary = false;
		}
	}


	public IEnumerator CheckForGateInStart()
	{
		while (!readyTogo) {
			yield return 0;
		}
		float percentageVal = (EmpireManager._instance.gate.currentExp/(float)EmpireManager._instance.gate.requiredExpPerLevel[EmpireManager._instance.gate.currentLevel]);
		gateUpdateImageText.text = Mathf.FloorToInt (percentageVal*100)+"%";
		EmpireManager._instance.gate.levelSlider.value = percentageVal;

		//		float finalScale = 1+(7/100f*percentageVal);
		//		float finalScale = (8/100f*percentageVal);
		//		storageUpdateImageV = updateStorage.transform.localScale;
		//		iTween.ScaleTo(updateStorage.gameObject,iTween.Hash("x",finalScale,"time",1.5f));
		if (EmpireManager._instance.gate.primaryCardNo > 0) {
			if (EmpireManager._instance.gate.timeOfLockOfPrimary != null) {
				System.TimeSpan diff = TimeManager._instance.GetCurrentServerTime () - EmpireManager._instance.gate.timeOfLockOfPrimary;
				float diffSeconds = Mathf.Abs ((float)diff.TotalSeconds);
				Debug.Log ("diffSeconds  = " + diffSeconds);
				Debug.Log ("timeReqd = " + EmpireManager._instance.gate.timeRequiredPerLevel [EmpireManager._instance.gate.currentLevel] * 3600);
				if (EmpireManager._instance.gate.timeRequiredPerLevel [EmpireManager._instance.gate.currentLevel] * 3600 > diffSeconds && diffSeconds > 0) {
					Debug.Log ("time done???");
					isGatePrimary = true;
					PlayerPrefs.SetString ("updatingGate", "yes");
					PlayerPrefs.SetString ("tempButtonGate", EmpireManager._instance.gate.primaryCardNo.ToString ());

					loadingScene.Instance.randomCards.Remove (EmpireManager._instance.gate.primaryCardNo);
					int spriteToFetch = 0;
					for (int i = 0; i < CardsManager._instance.mycards.Count; i++) {
						if (EmpireManager._instance.gate.primaryCardNo == CardsManager._instance.mycards [i].card_id_in_playerList) {
							spriteToFetch = i;
							break;
						}
					}
					EmpireManager._instance.gate.instantUpdateButton.interactable = true;
					gatePrimaryClockText = EmpireManager._instance.gate.timeRequiredPerLevel [EmpireManager._instance.gate.currentLevel] * 3600 - diffSeconds;
					gatePrimaryImage.sprite = CardsManager._instance.mycards [spriteToFetch].cardSpriteFromResources;
					// add card to the primary image
				} else {
					PlayerPrefs.SetString ("tempButtonGate", EmpireManager._instance.gate.primaryCardNo.ToString ());
					empireScene.instance.buttonName = "gate";

					loadingScene.Instance.randomCards.Remove (EmpireManager._instance.gate.primaryCardNo);
					instantGate ();
					EmpireManager._instance.gate.primaryCardNo = -1;
					isGatePrimary = false;
					PlayerPrefs.SetString ("updatingGate", "no");
				}
			}
			else {
				isGatePrimary = false;
				PlayerPrefs.SetString ("updatingGate", "no");
			}
		} else {
			isGatePrimary = false;
			PlayerPrefs.SetString ("updatingGate", "no");
		}
		if(EmpireManager._instance.gate.secondaryCardNo > 0)
		{
			if(EmpireManager._instance.gate.timeOfLockOfSecondary != null)
			{
				System.TimeSpan diff = TimeManager._instance.GetCurrentServerTime () - EmpireManager._instance.gate.timeOfLockOfSecondary;
				float diffSeconds = Mathf.Abs((float)diff.TotalSeconds);
				float timeForActive = 3600;
				if(timeForActive > diffSeconds && diffSeconds > 0)
				{
					isGateSecondary = true;
					int spriteToFetch = 0;
					for(int i = 0 ; i < CardsManager._instance.mycards.Count ; i++)
					{
						if(EmpireManager._instance.gate.secondaryCardNo == CardsManager._instance.mycards[i].card_id_in_playerList)
						{
							spriteToFetch = i;
							break;
						}
					}

					loadingScene.Instance.randomCards.Remove (EmpireManager._instance.gate.secondaryCardNo);
					gateSecondaryText = 3600 - diffSeconds;
					gateSecondaryImage.sprite = CardsManager._instance.mycards[spriteToFetch].cardSpriteFromResources;
				}
				else
				{
					gateSecondaryText = 0;
					isGateSecondary = true;

					loadingScene.Instance.randomCards.Remove (EmpireManager._instance.gate.secondaryCardNo);
					updateGateSecondary();
					isGateSecondary = false;
					EmpireManager._instance.gate.secondaryCardNo = -1;
				}
			}
			else {
				isGateSecondary = false;
			}
		}
		else {
			isGateSecondary = false;
		}
	}


	public IEnumerator CheckForBarrackInStart()
	{
		while (!readyTogo) {
			yield return 0;
		}
		float percentageVal = (EmpireManager._instance.barracks.currentExp/(float)EmpireManager._instance.barracks.requiredExpPerLevel[EmpireManager._instance.barracks.currentLevel]);
		barrackUpdateImageText.text = Mathf.FloorToInt (percentageVal*100)+"%";
		EmpireManager._instance.barracks.levelSlider.value = percentageVal;

		//		float finalScale = 1+(7/100f*percentageVal);
		//		float finalScale = (8/100f*percentageVal);
		//		barnUpdateImageV = updateBarn.transform.localScale;
		//		iTween.ScaleTo(updateBarn.gameObject,iTween.Hash("x",finalScale,"time",1.5f));
		if (EmpireManager._instance.barracks.primaryCardNo > 0) {
			if (EmpireManager._instance.barracks.timeOfLockOfPrimary != null) {
				System.TimeSpan diff = TimeManager._instance.GetCurrentServerTime () - EmpireManager._instance.barracks.timeOfLockOfPrimary;
				float diffSeconds = Mathf.Abs ((float)diff.TotalSeconds);
//				Debug.Log ("diffSeconds barn = " + diffSeconds);
//				Debug.Log ("timeReqd barn= " + EmpireManager._instance.barn.timeRequiredPerLevel [EmpireManager._instance.barn.currentLevel] * 3600);
				if (EmpireManager._instance.barracks.timeRequiredPerLevel [EmpireManager._instance.barracks.currentLevel] * 3600 > diffSeconds && diffSeconds > 0) {
					Debug.Log ("time done?  barrack??");
					isBarrackPrimary = true;
					PlayerPrefs.SetString ("tempButtonBarrack", EmpireManager._instance.barracks.primaryCardNo.ToString ());
					PlayerPrefs.SetString ("updatingBarrack", "yes");
					int spriteToFetch = 0;
					for (int i = 0; i < CardsManager._instance.mycards.Count; i++) {
						if (EmpireManager._instance.barracks.primaryCardNo == CardsManager._instance.mycards [i].card_id_in_playerList) {
							spriteToFetch = i;
							break;
						}
					}

					loadingScene.Instance.randomCards.Remove (EmpireManager._instance.barracks.primaryCardNo);
					EmpireManager._instance.barracks.instantUpdateButton.interactable = true;
					barrackPrimaryClockText = EmpireManager._instance.barracks.timeRequiredPerLevel [EmpireManager._instance.barracks.currentLevel] * 3600 - diffSeconds;
					barrackPrimaryImage.sprite = CardsManager._instance.mycards [spriteToFetch].cardSpriteFromResources;
					// add card to the primary image
				} else {
					PlayerPrefs.SetString ("tempButtonBarrack", EmpireManager._instance.barracks.primaryCardNo.ToString ());
					empireScene.instance.buttonName = "barrack";

					loadingScene.Instance.randomCards.Remove (EmpireManager._instance.barracks.primaryCardNo);
					instantBarrack ();
					EmpireManager._instance.barracks.primaryCardNo = -1;
					isBarrackPrimary = false;
					PlayerPrefs.SetString ("updatingBarrack", "no");
				}
			}
			else {
				isBarrackPrimary = false;
				PlayerPrefs.SetString ("updatingBarrack", "no");
			}
		} else {
			isBarrackPrimary = false;
			PlayerPrefs.SetString ("updatingBarrack", "no");
		}
		if(EmpireManager._instance.barracks.secondaryCardNo > 0)
		{
			if(EmpireManager._instance.barracks.timeOfLockOfSecondary != null)
			{
				System.TimeSpan diff = TimeManager._instance.GetCurrentServerTime () - EmpireManager._instance.barracks.timeOfLockOfSecondary;
				float diffSeconds = Mathf.Abs((float)diff.TotalSeconds);
				float timeForActive = 180;
				if(timeForActive > diffSeconds && diffSeconds > 0)
				{
					isBarrackSecondary = true;
					int spriteToFetch = 0;
					for(int i = 0 ; i < CardsManager._instance.mycards.Count ; i++)
					{
						if(EmpireManager._instance.barracks.secondaryCardNo == CardsManager._instance.mycards[i].card_id_in_playerList)
						{
							spriteToFetch = i;
							break;
						}
					}

					loadingScene.Instance.randomCards.Remove (EmpireManager._instance.barracks.secondaryCardNo);
					barrackSecondaryText = 180 - diffSeconds;
					barrackSecondaryImage.sprite = CardsManager._instance.mycards[spriteToFetch].cardSpriteFromResources;
				}
				else
				{
					barrackSecondaryText = 0;
					empireScene.instance.buttonName = "barrack";

					loadingScene.Instance.randomCards.Remove (EmpireManager._instance.barracks.secondaryCardNo);
					isBarrackSecondary = false;
					updateBarrackSecondary();
					isBarrackSecondary = false;
					EmpireManager._instance.barracks.secondaryCardNo = -1;
				}
			}
			else {
				isBarrackSecondary = false;
			}
		}
		else {
			isBarrackSecondary = false;
		}
	}




	public IEnumerator CheckForBarnInStart()
	{
		while (!readyTogo) {
			yield return 0;
		}
		float percentageVal = (EmpireManager._instance.barn.currentExp/(float)EmpireManager._instance.barn.requiredExpPerLevel[EmpireManager._instance.barn.currentLevel]);
		barnUpdateImageText.text = Mathf.FloorToInt (percentageVal*100)+"%";
		EmpireManager._instance.barn.levelSlider.value = percentageVal;
		//		float finalScale = 1+(7/100f*percentageVal);
//		float finalScale = (8/100f*percentageVal);
//		barnUpdateImageV = updateBarn.transform.localScale;
//		iTween.ScaleTo(updateBarn.gameObject,iTween.Hash("x",finalScale,"time",1.5f));
		if (EmpireManager._instance.barn.primaryCardNo > 0) {
			if (EmpireManager._instance.barn.timeOfLockOfPrimary != null) {
				System.TimeSpan diff = TimeManager._instance.GetCurrentServerTime () - EmpireManager._instance.barn.timeOfLockOfPrimary;
				float diffSeconds = Mathf.Abs ((float)diff.TotalSeconds);
				Debug.Log ("diffSeconds barn = " + diffSeconds);
				Debug.Log ("timeReqd barn= " + EmpireManager._instance.barn.timeRequiredPerLevel [EmpireManager._instance.barn.currentLevel] * 3600);
				if (EmpireManager._instance.barn.timeRequiredPerLevel [EmpireManager._instance.barn.currentLevel] * 3600 > diffSeconds && diffSeconds > 0) {
					Debug.Log ("time done?  barn??");
					isBarnPrimary = true;
					PlayerPrefs.SetString ("tempButtonBarn", EmpireManager._instance.barn.primaryCardNo.ToString ());
					PlayerPrefs.SetString ("updatingBarn", "yes");
					int spriteToFetch = 0;
					for (int i = 0; i < CardsManager._instance.mycards.Count; i++) {
						if (EmpireManager._instance.barn.primaryCardNo == CardsManager._instance.mycards [i].card_id_in_playerList) {
							spriteToFetch = i;
							break;
						}
					}

					loadingScene.Instance.randomCards.Remove (EmpireManager._instance.barn.primaryCardNo);
					EmpireManager._instance.barn.instantUpdateButton.interactable = true;
					barnPrimaryClockText = EmpireManager._instance.barn.timeRequiredPerLevel [EmpireManager._instance.barn.currentLevel] * 3600 - diffSeconds;
					barnPrimaryImage.sprite = CardsManager._instance.mycards [spriteToFetch].cardSpriteFromResources;
					// add card to the primary image
				} else {
					PlayerPrefs.SetString ("tempButtonBarn", EmpireManager._instance.barn.primaryCardNo.ToString ());
					empireScene.instance.buttonName = "barn";

					loadingScene.Instance.randomCards.Remove (EmpireManager._instance.barn.primaryCardNo);
					instantBarn ();
					EmpireManager._instance.barn.primaryCardNo = -1;
					isBarnPrimary = false;
					PlayerPrefs.SetString ("updatingBarn", "no");
				}
			}
			else {
				isBarnPrimary = false;
				PlayerPrefs.SetString ("updatingBarn", "no");
			}
		} else {
			isBarnPrimary = false;
			PlayerPrefs.SetString ("updatingBarn", "no");
		}
		if(EmpireManager._instance.barn.secondaryCardNo > 0)
		{
			if(EmpireManager._instance.barn.timeOfLockOfSecondary != null)
			{
				System.TimeSpan diff = TimeManager._instance.GetCurrentServerTime () - EmpireManager._instance.barn.timeOfLockOfSecondary;
				float diffSeconds = Mathf.Abs((float)diff.TotalSeconds);
				float timeForActive = 3600;
				if(timeForActive > diffSeconds && diffSeconds > 0)
				{
					isBarnSecondary = true;
					int spriteToFetch = 0;
					for(int i = 0 ; i < CardsManager._instance.mycards.Count ; i++)
					{
						if(EmpireManager._instance.barn.secondaryCardNo == CardsManager._instance.mycards[i].card_id_in_playerList)
						{
							spriteToFetch = i;
							break;
						}
					}

					loadingScene.Instance.randomCards.Remove (EmpireManager._instance.barn.secondaryCardNo);
					barnSecondaryText = 3600 - diffSeconds;
					barnSecondaryImage.sprite = CardsManager._instance.mycards[spriteToFetch].cardSpriteFromResources;
				}
				else
				{
					barnSecondaryText = 0;
					empireScene.instance.buttonName = "barn";

					loadingScene.Instance.randomCards.Remove (EmpireManager._instance.barn.secondaryCardNo);
					isBarnSecondary = false;
					updateBarnSecondary();
					isBarnSecondary = false;
					EmpireManager._instance.barn.secondaryCardNo = -1;
				}
			}
			else {
				isBarnSecondary = false;
			}
		}
		else {
			isBarnSecondary = false;
		}
	}

	public IEnumerator CheckForGoldMineInStart()
	{
		while (!readyTogo) {
			yield return 0;
		}
		float percentageVal = (EmpireManager._instance.goldMine.currentExp/(float)EmpireManager._instance.goldMine.requiredExpPerLevel[EmpireManager._instance.goldMine.currentLevel]);
		goldMineUpdateImageText.text = Mathf.FloorToInt (percentageVal*100)+"%";
		EmpireManager._instance.goldMine.levelSlider.value = percentageVal;
		//		float finalScale = 1+(7/100f*percentageVal);
//		float finalScale = (8/100f*percentageVal);
//		goldMineUpdateImageV = updategoldMine.transform.localScale;
//		iTween.ScaleTo(updategoldMine.gameObject,iTween.Hash("x",finalScale,"time",1.5f));
		if (EmpireManager._instance.goldMine.primaryCardNo > 0) {
			if (EmpireManager._instance.goldMine.timeOfLockOfPrimary != null) {
				System.TimeSpan diff = TimeManager._instance.GetCurrentServerTime () - EmpireManager._instance.goldMine.timeOfLockOfPrimary;
				float diffSeconds = Mathf.Abs ((float)diff.TotalSeconds);
				Debug.Log ("diffSeconds goldMine = " + diffSeconds);
				Debug.Log ("timeReqd goldMine= " + EmpireManager._instance.goldMine.timeRequiredPerLevel [EmpireManager._instance.goldMine.currentLevel] * 3600);
				if (EmpireManager._instance.goldMine.timeRequiredPerLevel [EmpireManager._instance.goldMine.currentLevel] * 3600 > diffSeconds && diffSeconds > 0) {
					Debug.Log ("time done?  goldMine??");
					isgoldMinePrimary = true;
					PlayerPrefs.SetString ("tempButtonGoldMine", EmpireManager._instance.goldMine.primaryCardNo.ToString ());
					PlayerPrefs.SetString ("updatingGoldMine", "yes");
					int spriteToFetch = 0;
					for (int i = 0; i < CardsManager._instance.mycards.Count; i++) {
						if (EmpireManager._instance.goldMine.primaryCardNo == CardsManager._instance.mycards [i].card_id_in_playerList) {
							spriteToFetch = i;
							break;
						}
					}
					Debug.Log ("id removed here------------");
					loadingScene.Instance.randomCards.Remove (EmpireManager._instance.goldMine.primaryCardNo);
					EmpireManager._instance.goldMine.instantUpdateButton.interactable = true;
					goldMinePrimaryClockText = EmpireManager._instance.goldMine.timeRequiredPerLevel [EmpireManager._instance.goldMine.currentLevel] * 3600 - diffSeconds;
					goldMinePrimaryImage.sprite = CardsManager._instance.mycards [spriteToFetch].cardSpriteFromResources;
					// add card to the primary image
				} else {
					PlayerPrefs.SetString ("tempButtonGoldMine", EmpireManager._instance.goldMine.primaryCardNo.ToString ());
					empireScene.instance.buttonName = "goldMine";
					Debug.Log ("id removed here------------");
					loadingScene.Instance.randomCards.Remove (EmpireManager._instance.goldMine.primaryCardNo);
					instantGoldMine ();
					EmpireManager._instance.goldMine.primaryCardNo = -1;
					isgoldMinePrimary = false;
					PlayerPrefs.SetString ("updatingGoldMine", "no");
					// it will be already updated...
					// Change castle experience , level
				}
			}
			else {
				isgoldMinePrimary = false;
				PlayerPrefs.SetString ("updatingGoldMine", "no");
			}
		} else {
			isgoldMinePrimary = false;
			PlayerPrefs.SetString ("updatingGoldMine", "no");
		}
		if(EmpireManager._instance.goldMine.secondaryCardNo > 0)
		{
			if(EmpireManager._instance.goldMine.timeOfLockOfSecondary != null)
			{
				System.TimeSpan diff = TimeManager._instance.GetCurrentServerTime () - EmpireManager._instance.goldMine.timeOfLockOfSecondary;
				float diffSeconds = Mathf.Abs((float)diff.TotalSeconds);
				float timeForActive = 3600;
				if(timeForActive > diffSeconds && diffSeconds > 0)
				{
					isgoldMineSecondary = true;
					int spriteToFetch = 0;
					for(int i = 0 ; i < CardsManager._instance.mycards.Count ; i++)
					{
						if(EmpireManager._instance.goldMine.secondaryCardNo == CardsManager._instance.mycards[i].card_id_in_playerList)
						{
							spriteToFetch = i;
							break;
						}
					}
					Debug.Log ("id removed here------------");
					loadingScene.Instance.randomCards.Remove (EmpireManager._instance.goldMine.secondaryCardNo);
					goldMineSecondaryText = 3600 - diffSeconds;
					goldMineSecondaryImage.sprite = CardsManager._instance.mycards[spriteToFetch].cardSpriteFromResources;
				}
				else
				{
					goldMineSecondaryText = 0;
					empireScene.instance.buttonName = "goldMine";
					Debug.Log ("id removed here------------");
					loadingScene.Instance.randomCards.Remove (EmpireManager._instance.goldMine.secondaryCardNo);
					isgoldMineSecondary = false;
					EmpireManager._instance.goldMine.secondaryCardNo = -1;
				}
			}
			else {
				isgoldMineSecondary = false;
			}
		}
		else {
			isgoldMineSecondary = false;
		}
	}


	public IEnumerator CheckForTrainingGroundInStart()
	{
		while (!readyTogo) {
			yield return 0;
		}

		float percentageVal = (EmpireManager._instance.trainingGround.currentExp/(float)EmpireManager._instance.trainingGround.requiredExpPerLevel[EmpireManager._instance.trainingGround.currentLevel]);
		trainingGroundUpdateImageText.text = Mathf.FloorToInt (percentageVal*100)+"%";
		EmpireManager._instance.trainingGround.levelSlider.value = percentageVal;
		//		float finalScale = 1+(7/100f*percentageVal);
		//		float finalScale = (8/100f*percentageVal);
		//		goldMineUpdateImageV = updategoldMine.transform.localScale;
		//		iTween.ScaleTo(updategoldMine.gameObject,iTween.Hash("x",finalScale,"time",1.5f));
		if (EmpireManager._instance.trainingGround.primaryCardNo > 0) {
			if (EmpireManager._instance.trainingGround.timeOfLockOfPrimary != null) {
				System.TimeSpan diff = TimeManager._instance.GetCurrentServerTime () - EmpireManager._instance.trainingGround.timeOfLockOfPrimary;
				float diffSeconds = Mathf.Abs ((float)diff.TotalSeconds);
				Debug.Log ("diffSeconds trainingGround = " + diffSeconds);
				Debug.Log ("timeReqd trainingGround= " + EmpireManager._instance.trainingGround.timeRequiredPerLevel [EmpireManager._instance.trainingGround.currentLevel] * 3600);
				if (EmpireManager._instance.trainingGround.timeRequiredPerLevel [EmpireManager._instance.trainingGround.currentLevel] * 3600 > diffSeconds && diffSeconds > 0) {
					Debug.Log ("time done?  trainingGround??");
					istrainingGroundPrimary = true;
					PlayerPrefs.SetString ("tempButtonGround", EmpireManager._instance.trainingGround.primaryCardNo.ToString ());
					PlayerPrefs.SetString ("updatingGround", "yes");
					int spriteToFetch = 0;
					for (int i = 0; i < CardsManager._instance.mycards.Count; i++) {
						if (EmpireManager._instance.trainingGround.primaryCardNo == CardsManager._instance.mycards [i].card_id_in_playerList) {
							spriteToFetch = i;
							break;
						}
					}
					Debug.Log ("id removed here------------");
					loadingScene.Instance.randomCards.Remove (EmpireManager._instance.trainingGround.primaryCardNo);
					EmpireManager._instance.trainingGround.instantUpdateButton.interactable = true;
					trainingGroundPrimaryClockText = EmpireManager._instance.trainingGround.timeRequiredPerLevel [EmpireManager._instance.trainingGround.currentLevel] * 3600 - diffSeconds;
					trainingGroundPrimaryImage.sprite = CardsManager._instance.mycards [spriteToFetch].cardSpriteFromResources;
					// add card to the primary image
				} else {
					PlayerPrefs.SetString ("tempButtonGround", EmpireManager._instance.trainingGround.primaryCardNo.ToString ());
					empireScene.instance.buttonName = "traningGround";
					Debug.Log ("id removed here------------");
					loadingScene.Instance.randomCards.Remove (EmpireManager._instance.trainingGround.primaryCardNo);
					instantUpdateTrainingGround ();
					EmpireManager._instance.trainingGround.primaryCardNo = -1;
					istrainingGroundPrimary = false;
					PlayerPrefs.SetString ("updatingGround", "no");
					// it will be already updated...
					// Change castle experience , level
				}
			}
			else {
				istrainingGroundPrimary = false;
				PlayerPrefs.SetString ("updatingGround", "no");
			}
		} else {
			istrainingGroundPrimary = false;
			PlayerPrefs.SetString ("updatingGround", "no");
		}
	}

	public void EditDescription(string descriptionOfPlayer)
	{
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected)
			{
				Dictionary<string, string> avatarParameters = new Dictionary<string, string> ();
				avatarParameters.Add ("description", descriptionOfPlayer);
				StartCoroutine (PlayerParameters._instance.SendPlayerParameters (avatarParameters, isSuccess => 
				{
					if(!isSuccess)
					{
						popupFromServer.ShowPopup ("Cannot update your data!");
						//TODO: RESET DESCRIPTION
					}
				}));
			}
			else
			{
				//TODO: RESET DESCRIPTION
				popupFromServer.ShowPopup ("Network Error!");
			}
		});
	}


	public int gemsToDeductOnInstantUpgrade;

	int maxValOfExpNeeded = 600;

	void CheckVIPDays()
	{
		VipDiff = PlayerParameters._instance.myPlayerParameter.time_of_membership_no.AddDays (PlayerParameters._instance.myPlayerParameter.membership_no) - TimeManager._instance.GetCurrentServerTime ();
//		Debug.Log ("Condition "+VipDiff.Days);
		if (VipDiff.Days > PlayerParameters._instance.myPlayerParameter.membership_no) {
			PlayerParameters._instance.myPlayerParameter.membership_no = 0;
			Dictionary<string, string> avatarParameters = new Dictionary<string, string> ();
			avatarParameters.Add ("membership_no" ,PlayerParameters._instance.myPlayerParameter.membership_no.ToString ());
			StartCoroutine (PlayerParameters._instance.SendPlayerParameters (avatarParameters,null ));

		}
	}

	void Update()
	{
		if (!readyTogo)
			return;

		if (PlayerParameters._instance.myPlayerParameter.orb < PlayerParameters._instance.myPlayerParameter.maxOrb) {
			newMenuScene.instance.orbsText.text = Mathf.FloorToInt (newMenuScene.instance.timerDecreaseOrbs / 60).ToString ("00") + ":" + Mathf.Abs (newMenuScene.instance.timerDecreaseOrbs % 60).ToString ("00");
			newMenuScene.instance.timerDecreaseOrbs -= Time.deltaTime;
			if (newMenuScene.instance.timerDecreaseOrbs <= 0f) {
				newMenuScene.instance.orbsText.text = "00" + ":" + Mathf.Abs (newMenuScene.instance.timerDecreaseOrbs % 60).ToString ("00");
				PlayerParameters._instance.myPlayerParameter.orb += 1;
				newMenuScene.instance.timerDecreaseOrbs = 10 * 60;
				for (int i = 0; i < newMenuScene.instance.attackingOrbs.Count; i++) {
					if (i < PlayerParameters._instance.myPlayerParameter.orb) {
						newMenuScene.instance.attackingOrbs [i].sprite = newMenuScene.instance.activatedOrb;
					} else {
						newMenuScene.instance.attackingOrbs [i].sprite = newMenuScene.instance.deactivedOrb;
					}
				}


			}
		} 
		if (PlayerParameters._instance.myPlayerParameter.orb == PlayerParameters._instance.myPlayerParameter.maxOrb){
			if (newMenuScene.instance.orbsText != null) {
				newMenuScene.instance.orbsText.text = "00" + ":" + "00";
			}
		}


		//Shivam
		if (PlayerParameters._instance.myPlayerParameter.membership_no == 7) {
			CheckVIPDays ();
			ActivateMembershipShow_text.text = "Weeklong Membership Active";
			DiscountText.text = "Discount - 2%";
			VIPText.text = "Weeklong Membership";
			VIPButton.interactable = true;

		} else if (PlayerParameters._instance.myPlayerParameter.membership_no == 14) {
			CheckVIPDays ();
			ActivateMembershipShow_text.text = "Fortnight Membership Active";
			DiscountText.text = "Discount - 3.5%";
			VIPButton.interactable = true;
			VIPText.text = "Fortnight Membership";

		} else if (PlayerParameters._instance.myPlayerParameter.membership_no == 30) {
			CheckVIPDays ();
			ActivateMembershipShow_text.text = "Month Membership Active";
			DiscountText.text = "Discount - 5%";
			VIPButton.interactable = true;

			VIPText.text = "Month Membership";
		} 
		else {
			ActivateMembershipShow_text.text = "None of the Membership Active";
			VIPButton.interactable = false;
		}
		//Shivam end
		if(PlayerPrefs.GetString("redirect")=="yes")
					{
						//bar123.transform.localScale=new Vector3(0.5f,1,1);
						//imageLow2.x+=Time.deltaTime/ 60/1;
						//bar123.transform.localScale=imageLow2;
					}
					else
					{
						if (PlayerParameters._instance.myPlayerParameter.stamina < PlayerParameters._instance.myPlayerParameter.max_stamina) 
						{
							newMenuScene.instance.timerDecrease -= Time.deltaTime;
							if (newMenuScene.instance.timerDecrease > 0) {
								newMenuScene.instance.timerText.text = "00" + ":" + Mathf.Abs (newMenuScene.instance.timerDecrease % 60).ToString ("00");

							} else {

					//Shivam
					if (PlayerParameters._instance.myPlayerParameter.membership_no==7) {
						newMenuScene.instance.timerDecrease = 55;
					} else if (PlayerParameters._instance.myPlayerParameter.membership_no==14) {
						newMenuScene.instance.timerDecrease = 50;
					} else if (PlayerParameters._instance.myPlayerParameter.membership_no==30) {
						newMenuScene.instance.timerDecrease = 45;
					} else {
						newMenuScene.instance.timerDecrease = 60;
					}
					// Shivam end

//								newMenuScene.instance.timerDecrease = 60;
								newMenuScene.instance.currentStamina = +1;
								PlayerParameters._instance.myPlayerParameter.stamina +=1;
								newMenuScene.instance.textFeching[6].GetComponent<Text>().text=PlayerParameters._instance.myPlayerParameter.stamina.ToString() +"/"+PlayerParameters._instance.myPlayerParameter.max_stamina.ToString();
			
							}

						}
						else 
						{
							newMenuScene.instance.timerDecrease = 00;
							newMenuScene.instance.timerText.text = "00" + ":" +"00";
			
			
						}
						
					}
	//=========  CASTLE ================== V
		if(isCastlePrimary==true)
		{


			castlePrimaryClockText-=Time.deltaTime;
			if(castlePrimaryClockText>0)
			{



				EmpireManager._instance.castle.chosenCardButton.interactable=false;
				EmpireManager._instance.castle.instantUpdateButton.interactable=true;
				castleUpgradeButton.interactable=false;


					int minutesText = Mathf.FloorToInt(castlePrimaryClockText/60);
					int secondsText = Mathf.FloorToInt(castlePrimaryClockText % 60);
					int hours = (int)(Mathf.FloorToInt(castlePrimaryClockText/3600)%24);
					int minutesText2 = minutesText - (hours*60);


					if(minutesText<=59)
					{
					castlePrimaryClockString.text =   "00:" +minutesText.ToString("00")+":"+secondsText.ToString("00");

					}
					else
					{
					castlePrimaryClockString.text =   hours.ToString("00")+":" +minutesText2.ToString("00")+":"+secondsText.ToString("00");
					}

					if(empireScene.instance != null && instantGemsTextForPopup.gameObject.activeInHierarchy && empireScene.instance.buttonName == "castle")
					{
						if(secondsText != 0)
							minutesText++;
						gemsToDeductOnInstantUpgrade = minutesText;
						instantGemsTextForPopup.text = gemsToDeductOnInstantUpgrade.ToString ();
					}





			}
			else
			{
				print("========  CASTLE PRIMARY UPDATED =======");
				EmpireManager._instance.castle.chosenCardButton.interactable=true;
				EmpireManager._instance.castle.instantUpdateButton.interactable=false;
				instantUpdateCastle();
			}


		}


		//------------------ BARN -------------------
		if(isBarnPrimary==true)
		{


			barnPrimaryClockText-=Time.deltaTime;
			if(barnPrimaryClockText>0)
			{



				EmpireManager._instance.barn.chosenCardButton.interactable=false;
				EmpireManager._instance.barn.instantUpdateButton.interactable=true;
				barnUpgradeButton.interactable=false;
				int minutesText = Mathf.FloorToInt(barnPrimaryClockText/60);
				int secondsText = Mathf.FloorToInt(barnPrimaryClockText % 60);
				int hours = (int)(Mathf.FloorToInt(barnPrimaryClockText/3600)%24);
				int minutesText2 = minutesText - (hours*60);


				if(minutesText<=59)
				{
					barnPrimaryClockString.text =   "00:" +minutesText.ToString("00")+":"+secondsText.ToString("00");

				}
				else
				{
					barnPrimaryClockString.text =   hours.ToString("00")+":" +minutesText2.ToString("00")+":"+secondsText.ToString("00");
				}
				if(empireScene.instance != null && instantGemsTextForPopup.gameObject.activeInHierarchy && empireScene.instance.buttonName == "barn")
				{
					if(secondsText != 0)
						minutesText++;
					gemsToDeductOnInstantUpgrade = minutesText;
					instantGemsTextForPopup.text = gemsToDeductOnInstantUpgrade.ToString ();
				}


			}
			else
			{
				EmpireManager._instance.barn.chosenCardButton.interactable=true;
				EmpireManager._instance.barn.instantUpdateButton.interactable=false;
				print("========  BARN PRIMARY UPDATED =======");
				instantBarn();
			}


		}
		updateBarnSecondary();


		//-------------------------------------------


		//------------------ GOLD MINE -------------------
		if(isgoldMinePrimary==true)
		{


			goldMinePrimaryClockText-=Time.deltaTime;
			if(goldMinePrimaryClockText>0)
			{
				EmpireManager._instance.goldMine.chosenCardButton.interactable=false;
				EmpireManager._instance.goldMine.instantUpdateButton.interactable=true;
				goldMineUpgradeButton.interactable=false;
				int minutesText = Mathf.FloorToInt(goldMinePrimaryClockText/60);
				int secondsText = Mathf.FloorToInt(goldMinePrimaryClockText % 60);
				int hours = (int)(Mathf.FloorToInt(goldMinePrimaryClockText/3600)%24);
				int minutesText2 = minutesText - (hours*60);


				if(minutesText<=59)
				{
					goldMinePrimaryClockString.text =   "00:" +minutesText.ToString("00")+":"+secondsText.ToString("00");

				}
				else
				{
					goldMinePrimaryClockString.text =   hours.ToString("00")+":" +minutesText2.ToString("00")+":"+secondsText.ToString("00");
				}
				if(empireScene.instance != null && instantGemsTextForPopup.gameObject.activeInHierarchy && empireScene.instance.buttonName == "goldMine")
				{
					if(secondsText != 0)
						minutesText++;
					gemsToDeductOnInstantUpgrade = minutesText;
					instantGemsTextForPopup.text = gemsToDeductOnInstantUpgrade.ToString ();
				}


			}
			else
			{
				EmpireManager._instance.goldMine.chosenCardButton.interactable=true;
				EmpireManager._instance.goldMine.instantUpdateButton.interactable=false;
				print("========  goldMine PRIMARY UPDATED =======");
				instantGoldMine();
			}


		}
		updateGoldMineSecondary();
		//==================== STORAGE =================
		if(isStoragePrimary==true)
		{


			storagePrimaryClockText-=Time.deltaTime;
			if(storagePrimaryClockText>0)
			{

				//disableClick.SetActive(true);
				//cardLocked();

				EmpireManager._instance.storage.chosenCardButton.interactable=false;
				EmpireManager._instance.storage.instantUpdateButton.interactable=true;
				storageUpgradeButton.interactable=false;


				int minutesText = Mathf.FloorToInt(storagePrimaryClockText/60);
				int secondsText = Mathf.FloorToInt(storagePrimaryClockText % 60);
				int hours = (int)(Mathf.FloorToInt(storagePrimaryClockText/3600)%24);
				int minutesText2 = minutesText - (hours*60);


				if(minutesText<=59)
				{
					storagePrimaryClockString.text =   "00:" +minutesText.ToString("00")+":"+secondsText.ToString("00");

				}
				else
				{
					storagePrimaryClockString.text =   hours.ToString("00")+":" +minutesText2.ToString("00")+":"+secondsText.ToString("00");
				}
				if(empireScene.instance != null && instantGemsTextForPopup.gameObject.activeInHierarchy && empireScene.instance.buttonName == "storage")
				{
					if(secondsText != 0)
						minutesText++;

					gemsToDeductOnInstantUpgrade = minutesText;
					instantGemsTextForPopup.text = gemsToDeductOnInstantUpgrade.ToString ();
				}
			}
			else
			{
				EmpireManager._instance.storage.chosenCardButton.interactable=true;
				EmpireManager._instance.storage.instantUpdateButton.interactable=false;
				print("========  STORAGE PRIMARY UPDATED =======");
				instantStorage();

			}
		}
		updateStorageSecondary();


		if(isGatePrimary==true)
		{


			gatePrimaryClockText-=Time.deltaTime;
			if(gatePrimaryClockText>0)
			{

				//disableClick.SetActive(true);
				//cardLocked();

				EmpireManager._instance.gate.chosenCardButton.interactable=false;
				EmpireManager._instance.gate.instantUpdateButton.interactable=true;
				gateUpgradeButton.interactable=false;


				int minutesText = Mathf.FloorToInt(gatePrimaryClockText/60);
				int secondsText = Mathf.FloorToInt(gatePrimaryClockText % 60);
				int hours = (int)(Mathf.FloorToInt(gatePrimaryClockText/3600)%24);
				int minutesText2 = minutesText - (hours*60);


				if(minutesText<=59)
				{
					gatePrimaryClockString.text =   "00:" +minutesText.ToString("00")+":"+secondsText.ToString("00");

				}
				else
				{
					gatePrimaryClockString.text =   hours.ToString("00")+":" +minutesText2.ToString("00")+":"+secondsText.ToString("00");
				}
				if(empireScene.instance != null && instantGemsTextForPopup.gameObject.activeInHierarchy && empireScene.instance.buttonName == "gate")
				{
					if(secondsText != 0)
						minutesText++;

					gemsToDeductOnInstantUpgrade = minutesText;
					instantGemsTextForPopup.text = gemsToDeductOnInstantUpgrade.ToString ();
				}
			}
			else
			{
				EmpireManager._instance.gate.chosenCardButton.interactable=true;
				EmpireManager._instance.gate.instantUpdateButton.interactable=false;
				print("========  Gate PRIMARY UPDATED =======");
				instantGate();

			}
		}
		updateGateSecondary();




		//==================== BARRACK =================
		if(isBarrackPrimary==true)
		{


			barrackPrimaryClockText-=Time.deltaTime;
			if(barrackPrimaryClockText>0)
			{

				//disableClick.SetActive(true);
				//cardLocked();

				EmpireManager._instance.barracks.chosenCardButton.interactable=false;
				EmpireManager._instance.barracks.instantUpdateButton.interactable=true;
				barrackUpgradeButton.interactable=false;


				int minutesText = Mathf.FloorToInt(barrackPrimaryClockText/60);
				int secondsText = Mathf.FloorToInt(barrackPrimaryClockText % 60);
				int hours = (int)(Mathf.FloorToInt(barrackPrimaryClockText/3600)%24);
				int minutesText2 = minutesText - (hours*60);


				if(minutesText<=59)
				{
					barrackPrimaryClockString.text =   "00:" +minutesText.ToString("00")+":"+secondsText.ToString("00");

				}
				else
				{
					barrackPrimaryClockString.text =   hours.ToString("00")+":" +minutesText2.ToString("00")+":"+secondsText.ToString("00");
				}
				if(empireScene.instance != null && instantGemsTextForPopup.gameObject.activeInHierarchy && empireScene.instance.buttonName == "barrack")
				{
					if(secondsText != 0)
						minutesText++;

					gemsToDeductOnInstantUpgrade = minutesText;
					instantGemsTextForPopup.text = gemsToDeductOnInstantUpgrade.ToString ();
				}
			}
			else
			{
				EmpireManager._instance.barracks.chosenCardButton.interactable=true;
				EmpireManager._instance.barracks.instantUpdateButton.interactable=false;
				print("========  BARRACK PRIMARY UPDATED =======");
				instantBarrack();

			}
		}
		updateBarrackSecondary();


		//------------ TRAINNING GROUND -------------
		if(istrainingGroundPrimary==true)
		{


			trainingGroundPrimaryClockText-=Time.deltaTime;
			if(trainingGroundPrimaryClockText>0)
			{
				EmpireManager._instance.trainingGround.chosenCardButton.interactable=false;
				EmpireManager._instance.trainingGround.instantUpdateButton.interactable=true;
				trainingGroundUpgradeButton.interactable=false;
				int minutesText = Mathf.FloorToInt(trainingGroundPrimaryClockText/60);
				int secondsText = Mathf.FloorToInt(trainingGroundPrimaryClockText % 60);
				int hours = (int)(Mathf.FloorToInt(trainingGroundPrimaryClockText/3600)%24);
				int minutesText2 = minutesText - (hours*60);


				if(minutesText<=59)
				{
					trainingGroundPrimaryClockString.text =   "00:" +minutesText.ToString("00")+":"+secondsText.ToString("00");

				}
				else
				{
					trainingGroundPrimaryClockString.text =   hours.ToString("00")+":" +minutesText2.ToString("00")+":"+secondsText.ToString("00");
				}
				if(empireScene.instance != null && instantGemsTextForPopup.gameObject.activeInHierarchy && empireScene.instance.buttonName == "traningGround")
				{
					if(secondsText != 0)
						minutesText++;
					gemsToDeductOnInstantUpgrade = minutesText;
					instantGemsTextForPopup.text = gemsToDeductOnInstantUpgrade.ToString ();
				}


			}
			else
			{
				EmpireManager._instance.trainingGround.chosenCardButton.interactable=true;
				EmpireManager._instance.trainingGround.instantUpdateButton.interactable=false;
				print("========  trainingGround PRIMARY UPDATED =======");
				//instanttrainingGround();
				instantUpdateTrainingGround();
			}


		}

	}
	public void instantUpdateCastle(bool wasInstant = false , int gems = 0 , Action<bool> callBack = null)
	{
		if (!wasInstant) {
			UpdateCastleFcn ();
			updateBuilding (empireScene.instance.buttonName, EmpireManager._instance.castle.currentLevel, -1, EmpireManager._instance.castle.primaryCardNo.ToString (), EmpireManager._instance.castle.currentExp, (EmpireManager._instance.castle.timeRequiredPerLevel [EmpireManager._instance.castle.currentLevel] * 60).ToString ());

		} else {
			newMenuScene.instance.loader.SetActive (true);
			NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
				if (isConnected)
				{
					int cardLocked = CardsManager._instance.PositionOfCardInList (int.Parse (PlayerPrefs.GetString("tempButton")));
					int expToSend = empireScene.instance.currentExperienceOfBuilding+CardsManager._instance.mycards[cardLocked].leadership;
					int levelToSend = EmpireManager._instance.castle.currentLevel;
					if(expToSend >= empireScene.instance.finalExperienceOfBuilding)
					{
						expToSend = - empireScene.instance.finalExperienceOfBuilding + expToSend;
						levelToSend++;
					}
					updateBuildingInstant (empireScene.instance.buttonName, levelToSend, -1, EmpireManager._instance.castle.primaryCardNo.ToString (), expToSend, (EmpireManager._instance.castle.timeRequiredPerLevel [EmpireManager._instance.castle.currentLevel] * 60).ToString (), gems , isSuccess =>{
						if(isSuccess)
						{
							UpdateCastleFcn ();
							callBack(true);
						}
						else
						{
							newMenuScene.instance.loader.SetActive (false);
							newMenuScene.instance.popupFromServer.ShowPopup ("Cannot Update at this time");
						}
						newMenuScene.instance.loader.SetActive (false);
					});

				}
				else
				{
					newMenuScene.instance.loader.SetActive (false);
					newMenuScene.instance.popupFromServer.ShowPopup ("Cannot Update at this time");
				}
			});
		}

	}

	//Shivam
	public void ShowAciveMemberShip()
	{
		VIP_UI.SetActive (true);

		if (PlayerParameters._instance.myPlayerParameter.membership_no == 7) {
			Days_7.SetActive (true);
			Days_14.SetActive (false);
			Days_30.SetActive (false);
		}else if(PlayerParameters._instance.myPlayerParameter.membership_no==14)
		{
			Days_7.SetActive (false);
			Days_14.SetActive (true);
			Days_30.SetActive (false);

		}
		else{

			Days_7.SetActive (false);
			Days_14.SetActive (false);
			Days_30.SetActive (true);


		}

		VIP_Timer();
	}
	public void VIP_UI_Back()
	{
		CancelInvoke ("VIP_Timer");
		Days_7.SetActive (false);
		Days_14.SetActive (false);
		Days_30.SetActive (false);
		VIP_UI.SetActive (false);

	}
	void VIP_Timer()
	{

//		PlayerParameters._instance.myPlayerParameter.membership_no = 0;
//		Dictionary<string, string> avatarParameters = new Dictionary<string, string> ();
//		avatarParameters.Add ("membership_no", PlayerParameters._instance.myPlayerParameter.membership_no.ToString ());
//		StartCoroutine (PlayerParameters._instance.SendPlayerParameters (avatarParameters, null));
//		VIP_UI_Back ();

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
		else 
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

	void UpdateCastleFcn()
	{
		print("========  CASTLE PRIMARY UPDATED =======");

		PlayerPrefs.SetString("chosenCardCastle","no");
		randomCards.Add (EmpireManager._instance.castle.primaryCardNo);
		EmpireManager._instance.castle.primaryCardNo = -1;
		castlePrimaryImage.sprite=preloadImage;
		isCastlePrimary=false;
		castleUpgradeButton.interactable=true;
		confirmButton.interactable=false;
		Debug.Log ("temp button = "+PlayerPrefs.GetString("tempButton"));
		int cardLocked = CardsManager._instance.PositionOfCardInList (int.Parse (PlayerPrefs.GetString("tempButton")));
		//		empireScene.instance.currentExperienceOfBuilding+=1000;
		empireScene.instance.currentExperienceOfBuilding+=CardsManager._instance.mycards[cardLocked].leadership;
		EmpireManager._instance.castle.currentExp = empireScene.instance.currentExperienceOfBuilding;
		if(empireScene.instance.currentExperienceOfBuilding >= empireScene.instance.finalExperienceOfBuilding)
		{
//			updateCastle.transform.localScale = Vector3.one;
			empireScene.instance.currentExperienceOfBuilding = - empireScene.instance.finalExperienceOfBuilding + empireScene.instance.currentExperienceOfBuilding;
			EmpireManager._instance.castle.currentExp = empireScene.instance.currentExperienceOfBuilding;
			EmpireManager._instance.castle.currentLevel++;
			int currentVal = EmpireManager._instance.castle.finalValue1[EmpireManager._instance.castle.currentLevel];
			int finalVal = EmpireManager._instance.castle.finalValue1[EmpireManager._instance.castle.currentLevel+1];
			castleLevel.text="Lvl "+(EmpireManager._instance.castle.currentLevel+1).ToString();
			castleNow.text=currentVal.ToString();
			castleNext.text=finalVal.ToString();

			empireScene.instance.finalExperienceOfBuilding = EmpireManager._instance.castle.requiredExpPerLevel[EmpireManager._instance.castle.currentLevel];
		}

			float percentageVal = (empireScene.instance.currentExperienceOfBuilding / (float)empireScene.instance.finalExperienceOfBuilding);

			castleUpdateImageText.text = Mathf.FloorToInt (percentageVal * 100) + "%";
			EmpireManager._instance.castle.levelSlider.value = percentageVal;

		//		float finalScale = 1+(7/100f*percentageVal);
//		float finalScale = (8/100f*percentageVal);
		//				finalScale = finalScale - updateCastle.transform.localScale.x;
		//				updateCastle.gameObject.transform.localScale = new Vector3(finalScale,1,1);
//		castleUpdateImageV = updateCastle.transform.localScale;
//		iTween.ScaleTo(updateCastle.gameObject,iTween.Hash("x",finalScale,"time",1.5f));
		castlePrimaryClockString.text="00:00";
		PlayerPrefs.SetString("updating","no");
		PlayerPrefs.SetString("tempButton","no");
		if(empireScene.instance.buildingUpgradeLayout.activeInHierarchy==true)
		{
			empireScene.instance.ShowAllCards();
		}
	}



	//================== Storage =======

	public void instantStorage(bool wasInstant = false , int gems = 0  , Action<bool> callBack = null)
	{
		if (!wasInstant) {
			UpdateStorageFcn ();
			updateBuilding(empireScene.instance.buttonName , EmpireManager._instance.storage.currentLevel ,  -1 , EmpireManager._instance.storage.primaryCardNo.ToString() ,EmpireManager._instance.storage.currentExp  ,(EmpireManager._instance.storage.timeRequiredPerLevel[EmpireManager._instance.storage.currentLevel]*60).ToString());

		} else {
			newMenuScene.instance.loader.SetActive (true);
			NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
				if (isConnected)
				{

					int cardLocked = CardsManager._instance.PositionOfCardInList (int.Parse (PlayerPrefs.GetString("tempButtonStorage")));
					int expToSend = empireScene.instance.currentExperienceOfBuilding+CardsManager._instance.mycards[cardLocked].leadership;
					int levelToSend = EmpireManager._instance.storage.currentLevel;
					if(expToSend >= empireScene.instance.finalExperienceOfBuilding)
					{
						expToSend = - empireScene.instance.finalExperienceOfBuilding + expToSend;
						levelToSend++;
					}


					updateBuildingInstant(empireScene.instance.buttonName , levelToSend ,  -1 , EmpireManager._instance.storage.primaryCardNo.ToString() , expToSend ,(EmpireManager._instance.storage.timeRequiredPerLevel[EmpireManager._instance.storage.currentLevel]*60).ToString() , gems , isSuccess =>{
						if(isSuccess)
						{
							UpdateStorageFcn();
							callBack(true);
						}
						else
						{
							newMenuScene.instance.loader.SetActive (false);
							newMenuScene.instance.popupFromServer.ShowPopup ("Cannot Update at this time");
						}
						newMenuScene.instance.loader.SetActive (false);
					});

				}
				else
				{
					newMenuScene.instance.loader.SetActive (false);
					newMenuScene.instance.popupFromServer.ShowPopup ("Cannot Update at this time");
				}
			});
		}


	}


	public void instantGate(bool wasInstant = false , int gems = 0  , Action<bool> callBack = null)
	{
		if (!wasInstant) {
			UpdateGateFcn ();
			updateBuilding(empireScene.instance.buttonName , EmpireManager._instance.gate.currentLevel ,  -1 , EmpireManager._instance.gate.primaryCardNo.ToString() ,EmpireManager._instance.gate.currentExp  ,(EmpireManager._instance.gate.timeRequiredPerLevel[EmpireManager._instance.gate.currentLevel]*60).ToString());

		} else {
			newMenuScene.instance.loader.SetActive (true);
			NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
				if (isConnected)
				{

					int cardLocked = CardsManager._instance.PositionOfCardInList (int.Parse (PlayerPrefs.GetString("tempButtonGate")));
					int expToSend = empireScene.instance.currentExperienceOfBuilding+CardsManager._instance.mycards[cardLocked].leadership;
					int levelToSend = EmpireManager._instance.gate.currentLevel;
					if(expToSend >= empireScene.instance.finalExperienceOfBuilding)
					{
						expToSend = - empireScene.instance.finalExperienceOfBuilding + expToSend;
						levelToSend++;
					}


					updateBuildingInstant(empireScene.instance.buttonName , levelToSend ,  -1 , EmpireManager._instance.gate.primaryCardNo.ToString() , expToSend ,(EmpireManager._instance.gate.timeRequiredPerLevel[EmpireManager._instance.gate.currentLevel]*60).ToString() , gems , isSuccess =>{
						if(isSuccess)
						{
							UpdateGateFcn();
							callBack(true);
						}
						else
						{
							newMenuScene.instance.loader.SetActive (false);
							newMenuScene.instance.popupFromServer.ShowPopup ("Cannot Update at this time");
						}
						newMenuScene.instance.loader.SetActive (false);
					});

				}
				else
				{
					newMenuScene.instance.loader.SetActive (false);
					newMenuScene.instance.popupFromServer.ShowPopup ("Cannot Update at this time");
				}
			});
		}


	}

	void UpdateStorageFcn()
	{
		print("========  STORAGE PRIMARY UPDATED =======");
		PlayerPrefs.SetString("chosenCardStorage","no");
		randomCards.Add (EmpireManager._instance.storage.primaryCardNo);
		EmpireManager._instance.storage.primaryCardNo = -1;
		storagePrimaryImage.sprite=preloadImage;
		isStoragePrimary=false;
		storageUpgradeButton.interactable=true;
		//		empireScene.instance.currentExperienceOfBuilding+=1000;

		int cardLocked = CardsManager._instance.PositionOfCardInList (int.Parse (PlayerPrefs.GetString("tempButtonStorage")));
		empireScene.instance.currentExperienceOfBuilding+=CardsManager._instance.mycards[cardLocked].leadership;

		EmpireManager._instance.storage.currentExp = empireScene.instance.currentExperienceOfBuilding;
		if(empireScene.instance.currentExperienceOfBuilding >= empireScene.instance.finalExperienceOfBuilding)
		{
//			updateStorage.transform.localScale = Vector3.one;
			empireScene.instance.currentExperienceOfBuilding = - empireScene.instance.finalExperienceOfBuilding + empireScene.instance.currentExperienceOfBuilding;
			EmpireManager._instance.storage.currentExp = empireScene.instance.currentExperienceOfBuilding;
			EmpireManager._instance.storage.currentLevel++;
			int currentVal = EmpireManager._instance.storage.finalValue1[EmpireManager._instance.storage.currentLevel];
			int finalVal = EmpireManager._instance.storage.finalValue1[EmpireManager._instance.storage.currentLevel+1];
			storageNow.text=currentVal.ToString();
			storageNext.text=finalVal.ToString();
			storageLevel.text="Lvl "+(EmpireManager._instance.storage.currentLevel+1).ToString();
			empireScene.instance.finalExperienceOfBuilding = EmpireManager._instance.storage.requiredExpPerLevel[EmpireManager._instance.storage.currentLevel];
			Debug.Log ("storage level castle reqd "+EmpireManager._instance.storage.castleLevelRequired[EmpireManager._instance.storage.currentLevel]);
			Debug.Log ("casle level now "+EmpireManager._instance.castle.currentLevel);
			Debug.Log ("current storage level "+EmpireManager._instance.storage.currentLevel);

			if(EmpireManager._instance.storage.castleLevelRequired[EmpireManager._instance.storage.currentLevel] > EmpireManager._instance.castle.currentLevel)
			{
				EmpireManager._instance.storage.currentExp = 0;
				empireScene.instance.currentExperienceOfBuilding = 0;
			}
		}

		float percentageVal = (empireScene.instance.currentExperienceOfBuilding/(float)empireScene.instance.finalExperienceOfBuilding);
		storageUpdateImageText.text = Mathf.FloorToInt (percentageVal*100)+"%";
		EmpireManager._instance.storage.levelSlider.value = percentageVal;
//		if (EmpireManager._instance.storage.currentLevel >EmpireManager._instance.storage.currentLevel)
//		{
//			percentageVal=0f;
//
//		}
		//		float finalScale = 1+(7/100f*percentageVal);
//		float finalScale = (8/100f*percentageVal);
//		storageUpdateImageV = updateStorage.transform.localScale;
//		iTween.ScaleTo(updateStorage.gameObject,iTween.Hash("x",finalScale,"time",1.5f));
		print ("====== CURRENT LEVEL storage???  ====" + EmpireManager._instance.storage.currentLevel);
		storagePrimaryClockString.text="00:00";
		PlayerPrefs.SetString("updatingStorage","no");
		PlayerPrefs.SetString("tempButtonStorage","no");
		if(empireScene.instance.buildingUpgradeLayout.activeInHierarchy==true)
		{
			empireScene.instance.ShowAllCards();
		}
		//EmpireManager._instance.currentFood -=EmpireManager._instance.storage.foodRequiredForPrimary [EmpireManager._instance.storage.currentLevel];

		EmpireManager._instance.storage.upgradeFoodCostPrimary.text=EmpireManager._instance.storage.foodRequiredForPrimary [EmpireManager._instance.storage.currentLevel].ToString();
		EmpireManager._instance.storage.upgradeGoldCostPrimary.text=EmpireManager._instance.storage.foodRequiredForPrimary [EmpireManager._instance.storage.currentLevel].ToString();
		float tempTimer=EmpireManager._instance.storage.timeRequiredPerLevel [EmpireManager._instance.storage.currentLevel]*3600f/60;
		EmpireManager._instance.storage.upgradeTimePrimary.text = tempTimer.ToString ();

	}

	void UpdateGateFcn()
	{
		print("========  GATE PRIMARY UPDATED =======");
		PlayerPrefs.SetString("chosenCardGate","no");
		randomCards.Add (EmpireManager._instance.gate.primaryCardNo);
		EmpireManager._instance.gate.primaryCardNo = -1;
		gatePrimaryImage.sprite=preloadImage;
		isGatePrimary=false;
		gateUpgradeButton.interactable=true;
		//		empireScene.instance.currentExperienceOfBuilding+=1000;

		int cardLocked = CardsManager._instance.PositionOfCardInList (int.Parse (PlayerPrefs.GetString("tempButtonGate")));
		empireScene.instance.currentExperienceOfBuilding+=CardsManager._instance.mycards[cardLocked].leadership;

		EmpireManager._instance.gate.currentExp = empireScene.instance.currentExperienceOfBuilding;
		if(empireScene.instance.currentExperienceOfBuilding >= empireScene.instance.finalExperienceOfBuilding)
		{
			//			updateStorage.transform.localScale = Vector3.one;
			empireScene.instance.currentExperienceOfBuilding = - empireScene.instance.finalExperienceOfBuilding + empireScene.instance.currentExperienceOfBuilding;
			EmpireManager._instance.gate.currentExp = empireScene.instance.currentExperienceOfBuilding;
			EmpireManager._instance.gate.currentLevel++;
			int currentVal = EmpireManager._instance.gate.finalValue1[EmpireManager._instance.gate.currentLevel];
			int finalVal = EmpireManager._instance.gate.finalValue1[EmpireManager._instance.gate.currentLevel+1];
			gateNow.text=currentVal.ToString();
			gateNext.text=finalVal.ToString();
			gateLevel.text="Lvl "+(EmpireManager._instance.gate.currentLevel+1).ToString();
			empireScene.instance.finalExperienceOfBuilding = EmpireManager._instance.gate.requiredExpPerLevel[EmpireManager._instance.gate.currentLevel];

			if(EmpireManager._instance.gate.castleLevelRequired[EmpireManager._instance.gate.currentLevel] > EmpireManager._instance.castle.currentLevel)
			{
				EmpireManager._instance.gate.currentExp = 0;
				empireScene.instance.currentExperienceOfBuilding = 0;
			}
		}

		float percentageVal = (empireScene.instance.currentExperienceOfBuilding/(float)empireScene.instance.finalExperienceOfBuilding);
		gateUpdateImageText.text = Mathf.FloorToInt (percentageVal*100)+"%";
		EmpireManager._instance.gate.levelSlider.value = percentageVal;
		//		if (EmpireManager._instance.storage.currentLevel >EmpireManager._instance.storage.currentLevel)
		//		{
		//			percentageVal=0f;
		//
		//		}
		//		float finalScale = 1+(7/100f*percentageVal);
		//		float finalScale = (8/100f*percentageVal);
		//		storageUpdateImageV = updateStorage.transform.localScale;
		//		iTween.ScaleTo(updateStorage.gameObject,iTween.Hash("x",finalScale,"time",1.5f));
			
		gatePrimaryClockString.text="00:00";
		PlayerPrefs.SetString("updatingGate","no");
		PlayerPrefs.SetString("tempButtonGate","no");
		if(empireScene.instance.buildingUpgradeLayout.activeInHierarchy==true)
		{
			empireScene.instance.ShowAllCards();
		}
		//EmpireManager._instance.currentFood -=EmpireManager._instance.storage.foodRequiredForPrimary [EmpireManager._instance.storage.currentLevel];

		EmpireManager._instance.gate.upgradeFoodCostPrimary.text=EmpireManager._instance.gate.foodRequiredForPrimary [EmpireManager._instance.gate.currentLevel].ToString();
		EmpireManager._instance.gate.upgradeGoldCostPrimary.text=EmpireManager._instance.gate.foodRequiredForPrimary [EmpireManager._instance.gate.currentLevel].ToString();
		float tempTimer=EmpireManager._instance.gate.timeRequiredPerLevel [EmpireManager._instance.gate.currentLevel]*3600f/60;
		EmpireManager._instance.gate.upgradeTimePrimary.text = tempTimer.ToString ();

	}


	public void updateStorageSecondary()
	{
		if (isStorageSecondary == true)
		{
//			print("isStorageSecondary"+isStorageSecondary);

			storageSecondaryText -= Time.deltaTime;
			if (storageSecondaryText > 0)
			{


				EmpireManager._instance.storage.storgeLockDown.interactable = false;
				storageSecondaryString.text = "" + Mathf.Floor (storageSecondaryText / 60).ToString ("00") + ":" + (storageSecondaryText % 60).ToString ("00");


			} else
			{

				print ("========  STORAGE SECONDARY UPDATED =======");
				EmpireManager._instance.storage.upgradeGoldCostSecondary.text=EmpireManager._instance.storage.foodRequiredForSecondary [EmpireManager._instance.storage.currentLevel].ToString();
				EmpireManager._instance.storage.upgradeFoodCostSecondary.text=EmpireManager._instance.storage.foodRequiredForSecondary [EmpireManager._instance.storage.currentLevel].ToString();

				randomCards.Add (EmpireManager._instance.storage.secondaryCardNo);
				EmpireManager._instance.storage.secondaryCardNo = -1;
				storageSecondaryImage.sprite = StoragepreloadImageSecondary;
				isStorageSecondary = false;

				EmpireManager._instance.storage.storgeLockDown.interactable = true;

				PlayerPrefs.SetString ("updatingStorageSecondary", "no");
				if(empireScene.instance.buildingUpgradeLayout.activeInHierarchy==true)
				{
					empireScene.instance.ShowAllCards();
				}

			}
		}

	}


	public void updateGateSecondary()
	{
		if (isGateSecondary == true)
		{
			//			print("isStorageSecondary"+isStorageSecondary);

			gateSecondaryText -= Time.deltaTime;
			if (gateSecondaryText > 0)
			{


				EmpireManager._instance.gate.storgeLockDown.interactable = false;
				gateSecondaryString.text = "" + Mathf.Floor (gateSecondaryText / 60).ToString ("00") + ":" + (gateSecondaryText % 60).ToString ("00");


			} else
			{

				print ("========  GATE SECONDARY UPDATED =======");
				EmpireManager._instance.gate.upgradeGoldCostSecondary.text=EmpireManager._instance.gate.foodRequiredForSecondary [EmpireManager._instance.gate.currentLevel].ToString();
				EmpireManager._instance.gate.upgradeFoodCostSecondary.text=EmpireManager._instance.gate.foodRequiredForSecondary [EmpireManager._instance.gate.currentLevel].ToString();

				randomCards.Add (EmpireManager._instance.gate.secondaryCardNo);
				EmpireManager._instance.gate.secondaryCardNo = -1;
				gateSecondaryImage.sprite = gatepreloadImageSecondary;
				isGateSecondary = false;

				EmpireManager._instance.gate.storgeLockDown.interactable = true;

				PlayerPrefs.SetString ("updatingGateSecondary", "no");
				if(empireScene.instance.buildingUpgradeLayout.activeInHierarchy==true)
				{
					empireScene.instance.ShowAllCards();
				}

			}
		}

	}



	//---- Barrack Instant -------


	public void instantBarrack(bool wasInstant = false , int gems = 0  , Action<bool> callBack = null)
	{
		if (!wasInstant) {
			UpdateBarrackFcn ();
			updateBuilding(empireScene.instance.buttonName , EmpireManager._instance.barracks.currentLevel ,  -1 , EmpireManager._instance.barracks.primaryCardNo.ToString() ,EmpireManager._instance.barracks.currentExp  ,(EmpireManager._instance.barracks.timeRequiredPerLevel[EmpireManager._instance.barracks.currentLevel]*60).ToString());

		} else {
			newMenuScene.instance.loader.SetActive (true);
			NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
				if (isConnected)
				{
					int cardLocked = CardsManager._instance.PositionOfCardInList (int.Parse (PlayerPrefs.GetString("tempButtonBarrack")));
					int expToSend = empireScene.instance.currentExperienceOfBuilding+CardsManager._instance.mycards[cardLocked].leadership;
					int levelToSend = EmpireManager._instance.barracks.currentLevel;
					if(expToSend >= empireScene.instance.finalExperienceOfBuilding)
					{
						expToSend = - empireScene.instance.finalExperienceOfBuilding + expToSend;
						levelToSend++;
					}

					updateBuildingInstant(empireScene.instance.buttonName , levelToSend ,  -1 , EmpireManager._instance.barracks.primaryCardNo.ToString() ,expToSend  ,(EmpireManager._instance.barracks.timeRequiredPerLevel[EmpireManager._instance.barracks.currentLevel]*60).ToString() , gems , isSuccess =>{
						if(isSuccess)
						{
							UpdateBarrackFcn();
							callBack(true);
						}
						else
						{
							newMenuScene.instance.loader.SetActive (false);
							newMenuScene.instance.popupFromServer.ShowPopup ("Cannot Update at this time");
						}
						newMenuScene.instance.loader.SetActive (false);
					});

				}
				else
				{
					newMenuScene.instance.loader.SetActive (false);
					newMenuScene.instance.popupFromServer.ShowPopup ("Cannot Update at this time");
				}
			});
		}


	}


	//----- Barn Instant -------

	public void instantBarn(bool wasInstant = false , int gems = 0  , Action<bool> callBack = null)
	{
		if (!wasInstant) {
			UpdateBarnFcn ();
			updateBuilding(empireScene.instance.buttonName , EmpireManager._instance.barn.currentLevel ,  -1 , EmpireManager._instance.barn.primaryCardNo.ToString() ,EmpireManager._instance.barn.currentExp  ,(EmpireManager._instance.barn.timeRequiredPerLevel[EmpireManager._instance.barn.currentLevel]*60).ToString());

		} else {
			newMenuScene.instance.loader.SetActive (true);
			NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
				if (isConnected)
				{
					int cardLocked = CardsManager._instance.PositionOfCardInList (int.Parse (PlayerPrefs.GetString("tempButtonBarn")));
					int expToSend = empireScene.instance.currentExperienceOfBuilding+CardsManager._instance.mycards[cardLocked].leadership;
					int levelToSend = EmpireManager._instance.barn.currentLevel;
					if(expToSend >= empireScene.instance.finalExperienceOfBuilding)
					{
						expToSend = - empireScene.instance.finalExperienceOfBuilding + expToSend;
						levelToSend++;
					}

						updateBuildingInstant(empireScene.instance.buttonName , levelToSend ,  -1 , EmpireManager._instance.barn.primaryCardNo.ToString() ,expToSend  ,(EmpireManager._instance.barn.timeRequiredPerLevel[EmpireManager._instance.barn.currentLevel]*60).ToString() , gems , isSuccess =>{
						if(isSuccess)
						{
							UpdateBarnFcn();
							callBack(true);
						}
						else
						{
							newMenuScene.instance.loader.SetActive (false);
							newMenuScene.instance.popupFromServer.ShowPopup ("Cannot Update at this time");
						}
						newMenuScene.instance.loader.SetActive (false);
					});

				}
				else
				{
					newMenuScene.instance.loader.SetActive (false);
					newMenuScene.instance.popupFromServer.ShowPopup ("Cannot Update at this time");
				}
			});
		}


	}
	public void UpdateBarnFcn()
	{


		print("========  BARN PRIMARY UPDATED =======");
		PlayerPrefs.SetString("chosenCardBarn","no");
		randomCards.Add (EmpireManager._instance.barn.primaryCardNo);
		EmpireManager._instance.barn.primaryCardNo = -1;
		barnPrimaryImage.sprite=preloadImage;
		isBarnPrimary=false;
		barnUpgradeButton.interactable=true;
		//		empireScene.instance.currentExperienceOfBuilding+=1000;

		Debug.Log ("barn card = " + PlayerPrefs.GetString ("tempButtonBarn"));
		int cardLocked = CardsManager._instance.PositionOfCardInList (int.Parse (PlayerPrefs.GetString("tempButtonBarn")));
		empireScene.instance.currentExperienceOfBuilding+=CardsManager._instance.mycards[cardLocked].leadership;

		EmpireManager._instance.barn.currentExp = empireScene.instance.currentExperienceOfBuilding;
		if(empireScene.instance.currentExperienceOfBuilding >= empireScene.instance.finalExperienceOfBuilding)
		{
//			updateBarn.transform.localScale = Vector3.one;
			empireScene.instance.currentExperienceOfBuilding = - empireScene.instance.finalExperienceOfBuilding + empireScene.instance.currentExperienceOfBuilding;
			EmpireManager._instance.barn.currentExp = empireScene.instance.currentExperienceOfBuilding;
			EmpireManager._instance.barn.currentLevel++;
			int currentVal = EmpireManager._instance.barn.finalValue1[EmpireManager._instance.barn.currentLevel];
			int finalVal = EmpireManager._instance.barn.finalValue1[EmpireManager._instance.barn.currentLevel+1];
			barnNow.text=currentVal.ToString();
			barnNext.text=finalVal.ToString();
			barnLevel.text="Lvl "+(EmpireManager._instance.barn.currentLevel+1).ToString();
			empireScene.instance.finalExperienceOfBuilding = EmpireManager._instance.barn.requiredExpPerLevel[EmpireManager._instance.barn.currentLevel];
			if(EmpireManager._instance.barn.castleLevelRequired[EmpireManager._instance.barn.currentLevel] > EmpireManager._instance.castle.currentLevel)
			{
				EmpireManager._instance.barn.currentExp = 0;
				empireScene.instance.currentExperienceOfBuilding = 0;
			}
		}

		float percentageVal = (empireScene.instance.currentExperienceOfBuilding/(float)empireScene.instance.finalExperienceOfBuilding);
		barnUpdateImageText.text = Mathf.FloorToInt (percentageVal*100)+"%";
		EmpireManager._instance.barn.levelSlider.value = percentageVal;
		if (EmpireManager._instance.barn.currentLevel >EmpireManager._instance.barn.currentLevel)
		{
			percentageVal=0f;

		}
		//		float finalScale = 1+(7/100f*percentageVal);
//		float finalScale = (8/100f*percentageVal);
//		barnUpdateImageV = updateBarn.transform.localScale;
//		iTween.ScaleTo(updateBarn.gameObject,iTween.Hash("x",finalScale,"time",1.5f));
//		print ("====== CURRENT LEVEL   ====" + EmpireManager._instance.barn.currentLevel);
		barnPrimaryClockString.text="00:00";
		barnPrimaryClockText = 0;
		PlayerPrefs.SetString("updatingBarn","no");
		PlayerPrefs.SetString("tempButtonBarn","no");
		if(empireScene.instance.buildingUpgradeLayout.activeInHierarchy==true)
		{
			empireScene.instance.ShowAllCards();
		}

		EmpireManager._instance.barn.upgradeFoodCostPrimary.text=EmpireManager._instance.barn.foodRequiredForPrimary [EmpireManager._instance.barn.currentLevel].ToString();
		EmpireManager._instance.barn.upgradeGoldCostPrimary.text=EmpireManager._instance.barn.foodRequiredForPrimary [EmpireManager._instance.barn.currentLevel].ToString();
		float tempTimer=EmpireManager._instance.barn.timeRequiredPerLevel [EmpireManager._instance.barn.currentLevel]*3600f/60;
		EmpireManager._instance.barn.upgradeTimePrimary.text = tempTimer.ToString ();

	}


	public void UpdateBarrackFcn()
	{


		print("========  BARRACK PRIMARY UPDATED =======");
		PlayerPrefs.SetString("chosenCardBarrack","no");
		randomCards.Add (EmpireManager._instance.barracks.primaryCardNo);
		EmpireManager._instance.barracks.primaryCardNo = -1;
		barrackPrimaryImage.sprite=preloadImage;
		isBarrackPrimary=false;
		barrackUpgradeButton.interactable=true;
		//		empireScene.instance.currentExperienceOfBuilding+=1000;

		Debug.Log ("barrack card = " + PlayerPrefs.GetString ("tempButtonBarrack"));
		int cardLocked = CardsManager._instance.PositionOfCardInList (int.Parse (PlayerPrefs.GetString("tempButtonBarrack")));
		empireScene.instance.currentExperienceOfBuilding+=CardsManager._instance.mycards[cardLocked].leadership;

		EmpireManager._instance.barracks.currentExp = empireScene.instance.currentExperienceOfBuilding;
		if(empireScene.instance.currentExperienceOfBuilding >= empireScene.instance.finalExperienceOfBuilding)
		{
			//			updateBarn.transform.localScale = Vector3.one;
			empireScene.instance.currentExperienceOfBuilding = - empireScene.instance.finalExperienceOfBuilding + empireScene.instance.currentExperienceOfBuilding;
			EmpireManager._instance.barracks.currentExp = empireScene.instance.currentExperienceOfBuilding;
			EmpireManager._instance.barracks.currentLevel++;
			int currentVal = EmpireManager._instance.barracks.finalValue1[EmpireManager._instance.barracks.currentLevel];
			int finalVal = EmpireManager._instance.barracks.finalValue1[EmpireManager._instance.barracks.currentLevel+1];
			barrackIncreaseLimitNow.text=currentVal.ToString();
			barrackIncreaseLimitNext.text=finalVal.ToString();
			barrackLevel.text="Lvl "+(EmpireManager._instance.barracks.currentLevel+1).ToString();
			empireScene.instance.finalExperienceOfBuilding = EmpireManager._instance.barracks.requiredExpPerLevel[EmpireManager._instance.barracks.currentLevel];
			if(EmpireManager._instance.barracks.castleLevelRequired[EmpireManager._instance.barracks.currentLevel] > EmpireManager._instance.castle.currentLevel)
			{
				EmpireManager._instance.barracks.currentExp = 0;
				empireScene.instance.currentExperienceOfBuilding = 0;
			}
		}

		float percentageVal = (empireScene.instance.currentExperienceOfBuilding/(float)empireScene.instance.finalExperienceOfBuilding);
		barrackUpdateImageText.text = Mathf.FloorToInt (percentageVal*100)+"%";
		EmpireManager._instance.barracks.levelSlider.value = percentageVal;

		//		float finalScale = 1+(7/100f*percentageVal);
		//		float finalScale = (8/100f*percentageVal);
		//		barnUpdateImageV = updateBarn.transform.localScale;
		//		iTween.ScaleTo(updateBarn.gameObject,iTween.Hash("x",finalScale,"time",1.5f));
		//		print ("====== CURRENT LEVEL   ====" + EmpireManager._instance.barn.currentLevel);
		barrackPrimaryClockString.text="00:00";
		barrackPrimaryClockText = 0;
		PlayerPrefs.SetString("updatingBarrack","no");
		PlayerPrefs.SetString("tempButtonBarrack","no");
		if(empireScene.instance.buildingUpgradeLayout.activeInHierarchy==true)
		{
			empireScene.instance.ShowAllCards();
		}

		EmpireManager._instance.barracks.upgradeFoodCostPrimary.text=EmpireManager._instance.barracks.foodRequiredForPrimary [EmpireManager._instance.barracks.currentLevel].ToString();
		EmpireManager._instance.barracks.upgradeGoldCostPrimary.text=EmpireManager._instance.barracks.foodRequiredForPrimary [EmpireManager._instance.barracks.currentLevel].ToString();
		float tempTimer=EmpireManager._instance.barracks.timeRequiredPerLevel [EmpireManager._instance.barracks.currentLevel]*3600f/60;
		EmpireManager._instance.barracks.upgradeTimePrimary.text = tempTimer.ToString ();

	}


	public void updateBarnSecondary()
	{
		if (isBarnSecondary == true)
		{

			barnSecondaryText -= Time.deltaTime;
			if (barnSecondaryText > 0)
			{


				EmpireManager._instance.barn.storgeLockDown.interactable = false;
				barnSecondaryString.text = "" + Mathf.Floor (barnSecondaryText / 60).ToString ("00") + ":" + (barnSecondaryText % 60).ToString ("00");


			}
			else
			{

				print ("========  BARN SECONDARY UPDATED =======");
				EmpireManager._instance.barn.upgradeGoldCostSecondary.text=EmpireManager._instance.barn.foodRequiredForSecondary [EmpireManager._instance.barn.currentLevel].ToString();
				EmpireManager._instance.barn.upgradeFoodCostSecondary.text=EmpireManager._instance.barn.foodRequiredForSecondary [EmpireManager._instance.barn.currentLevel].ToString();

				randomCards.Add (EmpireManager._instance.barn.secondaryCardNo);
				EmpireManager._instance.barn.secondaryCardNo = -1;
				barnSecondaryImage.sprite = BarnloadImageSecondary;
				isBarnSecondary = false;

				EmpireManager._instance.barn.storgeLockDown.interactable = true;

				PlayerPrefs.SetString ("updatingSecondaryBarn", "no");
				if(empireScene.instance.buildingUpgradeLayout.activeInHierarchy==true)
				{
					empireScene.instance.ShowAllCards();
				}

			}
		}

	}

	//--------------------------
	public void instantGoldMine (bool wasInstant = false , int gems = 0  , Action<bool> callBack = null)
	{

		if (!wasInstant) {
			updateGoldMineFcn ();
		updateBuilding(empireScene.instance.buttonName , EmpireManager._instance.goldMine.currentLevel ,  -1 , EmpireManager._instance.goldMine.primaryCardNo.ToString() ,EmpireManager._instance.goldMine.currentExp  ,(EmpireManager._instance.goldMine.timeRequiredPerLevel[EmpireManager._instance.goldMine.currentLevel]*60).ToString());

		} else {
			newMenuScene.instance.loader.SetActive (true);
			NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
				if (isConnected)
				{
					int cardLocked = CardsManager._instance.PositionOfCardInList (int.Parse (PlayerPrefs.GetString("tempButtonGoldMine")));
					int expToSend = empireScene.instance.currentExperienceOfBuilding+CardsManager._instance.mycards[cardLocked].leadership;
					int levelToSend = EmpireManager._instance.goldMine.currentLevel;
					if(expToSend >= empireScene.instance.finalExperienceOfBuilding)
					{
						expToSend = - empireScene.instance.finalExperienceOfBuilding + expToSend;
						levelToSend++;
					}

					updateBuildingInstant(empireScene.instance.buttonName , levelToSend ,  -1 , EmpireManager._instance.goldMine.primaryCardNo.ToString() ,expToSend  ,(EmpireManager._instance.goldMine.timeRequiredPerLevel[EmpireManager._instance.goldMine.currentLevel]*60).ToString() , gems , isSuccess =>{
						if(isSuccess)
						{
							updateGoldMineFcn();
							callBack(true);
						}
						else
						{
							newMenuScene.instance.loader.SetActive (false);
							newMenuScene.instance.popupFromServer.ShowPopup ("Cannot Update at this time");
						}
						newMenuScene.instance.loader.SetActive (false);
					});

				}
				else
				{
					newMenuScene.instance.loader.SetActive (false);
					newMenuScene.instance.popupFromServer.ShowPopup ("Cannot Update at this time");
				}
			});
		}

	}




	public void updateBarrackSecondary()
	{
		if (isBarrackSecondary == true)
		{

			barrackSecondaryText -= Time.deltaTime;
			if (barrackSecondaryText > 0)
			{


				EmpireManager._instance.barracks.storgeLockDown.interactable = false;
				barrackSecondaryString.text = "" + Mathf.Floor (barrackSecondaryText / 60).ToString ("00") + ":" + (barrackSecondaryText % 60).ToString ("00");


			}
			else
			{

				print ("========  BARRACK SECONDARY UPDATED =======");
				EmpireManager._instance.barracks.upgradeGoldCostSecondary.text=EmpireManager._instance.barracks.foodRequiredForSecondary [EmpireManager._instance.barracks.currentLevel].ToString();
				EmpireManager._instance.barracks.upgradeFoodCostSecondary.text=EmpireManager._instance.barracks.foodRequiredForSecondary [EmpireManager._instance.barracks.currentLevel].ToString();
				secondaryBarrackShow();
				randomCards.Add (EmpireManager._instance.barracks.secondaryCardNo);

				EmpireManager._instance.barracks.secondaryCardNo = -1;
				barrackSecondaryImage.sprite = BarrackloadImageSecondary;
				isBarrackSecondary = false;

				EmpireManager._instance.barracks.storgeLockDown.interactable = true;

				PlayerPrefs.SetString ("updatingSecondaryBarrack", "no");
				if(empireScene.instance.buildingUpgradeLayout.activeInHierarchy==true)
				{
					empireScene.instance.ShowAllCards();
				}

			}
		}

	}
	public void secondaryBarrackShow()
	{
		int cardIdLocked = CardsManager._instance.PositionOfCardInList (EmpireManager._instance.barracks.secondaryCardNo);
		int soldiersToadd = CardsManager._instance.mycards[cardIdLocked].leadership;
		string cardIds = "";
		string initialCardSoldiers = "";
		string cardSoldiers = "";
		int initialDeployedSoldiers = PlayerParameters._instance.myPlayerParameter.currentlyDeployedSoldiers;
		int initialAvailableSoldiers = PlayerParameters._instance.myPlayerParameter.currentlyAvailableSoldiers;

		if( PlayerParameters._instance.myPlayerParameter.currentlyDeployedSoldiers < PlayerParameters._instance.myPlayerParameter.maxDeployedSoldiers)
		{
			PlayerParameters._instance.myPlayerParameter.currentlyDeployedSoldiers+=soldiersToadd;
			if(PlayerParameters._instance.myPlayerParameter.currentlyDeployedSoldiers > PlayerParameters._instance.myPlayerParameter.maxDeployedSoldiers)
			{
				Debug.Log("i ha deployed greater..!");
				soldiersToadd = PlayerParameters._instance.myPlayerParameter.currentlyDeployedSoldiers - PlayerParameters._instance.myPlayerParameter.maxDeployedSoldiers;
				PlayerParameters._instance.myPlayerParameter.currentlyDeployedSoldiers = PlayerParameters._instance.myPlayerParameter.maxDeployedSoldiers;
				Debug.Log ("soldiersToadd = "+soldiersToadd);
//				if( PlayerParameters._instance.myPlayerParameter.currentlyDeployedSoldiers < PlayerParameters._instance.myPlayerParameter.maxDeployedSoldiers)
//				{
//				PlayerParameters._instance.myPlayerParameter.currentlyAvailableSoldiers=   PlayerParameters._instance.myPlayerParameter.maxDeployedSoldiers-PlayerParameters._instance.myPlayerParameter.currentlyDeployedSoldiers;
//				}
			}
			else
			{
				soldiersToadd = 0;
			}

		}
		Debug.Log ("soldiersToadd = "+soldiersToadd);
		if(soldiersToadd > 0 && PlayerParameters._instance.myPlayerParameter.currentlyAvailableSoldiers < EmpireManager._instance.barracks.finalValue1[EmpireManager._instance.barracks.currentLevel])
		{
			PlayerParameters._instance.myPlayerParameter.currentlyAvailableSoldiers+=soldiersToadd;
			if(PlayerParameters._instance.myPlayerParameter.currentlyAvailableSoldiers > EmpireManager._instance.barracks.finalValue1[EmpireManager._instance.barracks.currentLevel])
			{
				PlayerParameters._instance.myPlayerParameter.currentlyAvailableSoldiers = EmpireManager._instance.barracks.finalValue1[EmpireManager._instance.barracks.currentLevel];
			}
		}
		CardsManager._instance.DistributeDeloyedSoldiersToCards(ref cardIds , ref initialCardSoldiers , ref cardSoldiers);
		Debug.Log ("cardIds = "+cardIds);
		Dictionary<string, string> avatarParameters = new Dictionary<string, string> ();
		avatarParameters.Add ("currently_deployed_soldiers", PlayerParameters._instance.myPlayerParameter.currentlyDeployedSoldiers.ToString ());
		avatarParameters.Add ("max_deployed_soldiers" , PlayerParameters._instance.myPlayerParameter.maxDeployedSoldiers.ToString ());
		avatarParameters.Add ("currently_available_soldiers" ,PlayerParameters._instance.myPlayerParameter.currentlyAvailableSoldiers.ToString ());
		int currentFinal = -PlayerParameters._instance.myPlayerParameter.currentlyDeployedSoldiers + PlayerParameters._instance.myPlayerParameter.maxDeployedSoldiers;
		if (!string.IsNullOrEmpty (cardIds)) {
			StartCoroutine (CardsManager._instance.SendCardSoldiers (cardIds,cardSoldiers,avatarParameters , isSuccess =>{
				if(!isSuccess)
				{
				}
			}));
		} 
		else {
			StartCoroutine (PlayerParameters._instance.SendPlayerParameters (avatarParameters, null));
		}
		DeployedSoldiers.text = PlayerParameters._instance.myPlayerParameter.currentlyDeployedSoldiers.ToString () + "/" + PlayerParameters._instance.myPlayerParameter.maxDeployedSoldiers.ToString ();
		AvailableSoldiers.text = PlayerParameters._instance.myPlayerParameter.currentlyAvailableSoldiers.ToString () + "/" + EmpireManager._instance.barracks.finalValue1 [EmpireManager._instance.barracks.currentLevel].ToString ();

		empireScene.instance.secondaryCard ("barrack", 0, "0", 0, "0", PlayerParameters._instance.myPlayerParameter.wheat, PlayerParameters._instance.myPlayerParameter.gold, isSuccess => {

		});


	}

	void updateGoldMineFcn()
	{
		print("========  GOLD MINE PRIMARY UPDATED =======");
		PlayerPrefs.SetString("chosenCardGoldMine","no");
		randomCards.Add (EmpireManager._instance.goldMine.primaryCardNo);
		EmpireManager._instance.goldMine.primaryCardNo = -1;
		goldMinePrimaryImage.sprite=preloadImage;
		isgoldMinePrimary=false;
		goldMineUpgradeButton.interactable=true;
		//		empireScene.instance.currentExperienceOfBuilding+=1000;

		Debug.Log ("gold mine card = " + PlayerPrefs.GetString ("tempButtonGoldMine"));
		int cardLocked = CardsManager._instance.PositionOfCardInList (int.Parse (PlayerPrefs.GetString("tempButtonGoldMine")));
		empireScene.instance.currentExperienceOfBuilding+=CardsManager._instance.mycards[cardLocked].leadership;

		EmpireManager._instance.goldMine.currentExp = empireScene.instance.currentExperienceOfBuilding;
		if(empireScene.instance.currentExperienceOfBuilding >= empireScene.instance.finalExperienceOfBuilding)
		{
//			updategoldMine.transform.localScale = Vector3.one;
			empireScene.instance.currentExperienceOfBuilding = - empireScene.instance.finalExperienceOfBuilding + empireScene.instance.currentExperienceOfBuilding;
			EmpireManager._instance.goldMine.currentExp = empireScene.instance.currentExperienceOfBuilding;
			EmpireManager._instance.goldMine.currentLevel++;
			int currentVal = EmpireManager._instance.goldMine.finalValue1[EmpireManager._instance.goldMine.currentLevel];
			int finalVal = EmpireManager._instance.goldMine.finalValue1[EmpireManager._instance.goldMine.currentLevel+1];
			goldMineNow.text=currentVal.ToString();
			goldMineNext.text=finalVal.ToString();
			goldMineLevel.text="Lvl "+(EmpireManager._instance.goldMine.currentLevel+1).ToString();
			empireScene.instance.finalExperienceOfBuilding = EmpireManager._instance.goldMine.requiredExpPerLevel[EmpireManager._instance.goldMine.currentLevel];
			if(EmpireManager._instance.goldMine.castleLevelRequired[EmpireManager._instance.goldMine.currentLevel] > EmpireManager._instance.castle.currentLevel)
			{
				EmpireManager._instance.goldMine.currentExp = 0;
				empireScene.instance.currentExperienceOfBuilding = 0;
			}
		}

		float percentageVal = (empireScene.instance.currentExperienceOfBuilding/(float)empireScene.instance.finalExperienceOfBuilding);
//		if (EmpireManager._instance.goldMine.currentLevel =) {
//			percentageVal=0f;
//
//		}

		goldMineUpdateImageText.text = Mathf.FloorToInt (percentageVal*100)+"%";

		EmpireManager._instance.goldMine.levelSlider.value = percentageVal;
		print ("====== CURRENT LEVEL   ====" + EmpireManager._instance.goldMine.currentLevel);

		goldMinePrimaryClockString.text="00:00";
		goldMinePrimaryClockText = 0;
		PlayerPrefs.SetString("updatingGoldMine","no");
		PlayerPrefs.SetString("tempButtonGoldMine","no");
		if(empireScene.instance.buildingUpgradeLayout.activeInHierarchy==true)
		{
			empireScene.instance.ShowAllCards();
		}

		EmpireManager._instance.goldMine.upgradeFoodCostPrimary.text=EmpireManager._instance.goldMine.foodRequiredForPrimary [EmpireManager._instance.goldMine.currentLevel].ToString();
		EmpireManager._instance.goldMine.upgradeGoldCostPrimary.text=EmpireManager._instance.goldMine.foodRequiredForPrimary [EmpireManager._instance.goldMine.currentLevel].ToString();
		float tempTimer=EmpireManager._instance.goldMine.timeRequiredPerLevel [EmpireManager._instance.goldMine.currentLevel]*3600f/60;
		EmpireManager._instance.goldMine.upgradeTimePrimary.text = tempTimer.ToString ();
	}

	public void updateGoldMineSecondary()
	{
		if (isgoldMineSecondary == true)
		{
			goldMineSecondaryText -= Time.deltaTime;
			if (goldMineSecondaryText > 0)
			{


				EmpireManager._instance.goldMine.storgeLockDown.interactable = false;
				goldMineSecondaryString.text = "" + Mathf.Floor (goldMineSecondaryText / 60).ToString ("00") + ":" + (goldMineSecondaryText % 60).ToString ("00");


			}
			else
			{

				print ("========  GOLD MINE SECONDARY UPDATED =======");
				EmpireManager._instance.goldMine.upgradeGoldCostSecondary.text=EmpireManager._instance.goldMine.foodRequiredForSecondary [EmpireManager._instance.goldMine.currentLevel].ToString();
				EmpireManager._instance.goldMine.upgradeFoodCostSecondary.text=EmpireManager._instance.goldMine.foodRequiredForSecondary [EmpireManager._instance.goldMine.currentLevel].ToString();

				randomCards.Add (EmpireManager._instance.goldMine.secondaryCardNo);
				EmpireManager._instance.goldMine.secondaryCardNo = -1;
				goldMineSecondaryImage.sprite = goldMineloadImageSecondary;
				isgoldMineSecondary = false;

				EmpireManager._instance.goldMine.storgeLockDown.interactable = true;

				PlayerPrefs.SetString ("updatingSecondaryGoldMine", "no");
				if(empireScene.instance.buildingUpgradeLayout.activeInHierarchy==true)
				{
					empireScene.instance.ShowAllCards();
				}

			}
		}

	}

	public void notificationOnOff()
	{
		if(notificationSound==false)
		{


			for(int i=0;i<OnButton.Length;i++)
			{
				if(OnNotification[i] != null)
				{
					OnNotification[i].GetComponent<Button>().interactable=false;
					OnNotification[i].gameObject.transform.GetChild(1).GetComponent<Text>().color=new Color32(255,255,255,255);
				}
			}
			for(int j=0;j<OffButton.Length;j++)
			{
				if(OffNotificaation[j] != null)
				{
					OffNotificaation[j].GetComponent<Button>().interactable=true;
					OffNotificaation[j].gameObject.transform.GetChild(1).GetComponent<Text>().color=new Color32(0,0,0,255);
				}

			}

			notificationSound=true;

		}
		else
		{


			for(int i=0;i<OnButton.Length;i++)
			{
				if(OnNotification[i] != null)
				{
					OnNotification[i].GetComponent<Button>().interactable=true;
					OnNotification[i].gameObject.transform.GetChild(1).GetComponent<Text>().color=new Color32(0,0,0,255);
				}

			}
			for(int j=0;j<OffButton.Length;j++)
			{
				if(OffNotificaation[j] != null)
				{
					OffNotificaation[j].GetComponent<Button>().interactable=false;
					OffNotificaation[j].gameObject.transform.GetChild(1).GetComponent<Text>().color=new Color32(255,255,255,255);
				}

			}

			notificationSound=false;
		}

	}



	public void soundOnOff()
	{
		print("=========== SOUND =========="+sound);

		if(sound==false)
		{
			AudioListener.pause=false;
			print("=======Q==== SOUND =========="+sound);
			for(int i=0;i<OnButton.Length;i++)
			{
				if(OnButton[i] != null)
				{
					OnButton[i].GetComponent<Button>().interactable=false;
					OnButton[i].gameObject.transform.GetChild(1).GetComponent<Text>().color=new Color32(255,255,255,255);
				}

			}
			for(int j=0;j<OffButton.Length;j++)
			{
				if(OffButton[j] != null)
				{
					OffButton[j].GetComponent<Button>().interactable=true;
					OffButton[j].gameObject.transform.GetChild(1).GetComponent<Text>().color=new Color32(0,0,0,255);
				}

			}



			sound=true;

		}
		else
		{
			AudioListener.pause=true;


			print("==========WEEWWEE= SOUND =========="+sound);

			for(int i=0;i<OnButton.Length;i++)
			{
				if(OnButton[i] != null)
				{
					OnButton[i].GetComponent<Button>().interactable=true;
					OnButton[i].gameObject.transform.GetChild(1).GetComponent<Text>().color=new Color32(0,0,0,255);
				}
			}
			for(int j=0;j<OffButton.Length;j++)
			{
				if(OffButton[j] != null)
				{
					OffButton[j].GetComponent<Button>().interactable=false;
					OffButton[j].gameObject.transform.GetChild(1).GetComponent<Text>().color=new Color32(255,255,255,255);
				}

			}


			sound=false;
		}

	}



	public void chat()
	{
		deactivateItems(11);
		SoundPlay (0);
	}

	public void battleScene()
	{
		BattleLogic._instance.battleType = BattleLogic.BattleType.BATTLE;
		battleFormation.isQuest = false;
//		deactivateItems(7);
		deactivateItems(9); // will go to battle formation
		SoundPlay (2);
	}

	public void battleItemSelectionScene()
	{
		BattleLogic._instance.battleType = BattleLogic.BattleType.BATTLE;
		battleFormation.isQuest = false;
		deactivateItems(7);
		SoundPlay (2);
	}

	public void BattleFormation()
	{
		battleFormation.isQuest = false;
		deactivateItems(9);
		SoundPlay (2);

	}

	public void SoundPlay(int clipNo)
	{
		if (audioSource.clip != allClips [clipNo]) {
			audioSource.clip = allClips [clipNo];
			audioSource.Play ();
		}
	}

	public void startBattle()
	{
		BattleLogic._instance.battleType = BattleLogic.BattleType.BATTLE;
		deactivateItems(15);
		SoundPlay (2);
	}

	public void BattleOpponentSelection()
	{
		BattleLogic._instance.battleType = BattleLogic.BattleType.BATTLE;
		battleFormation.isQuest = false;
		deactivateItems(8);
		SoundPlay (2);
	}

	public void cardCollecton()
	{
		deactivateItems(10);
		SoundPlay (2);

	}

	public void inventory()
	{
		deactivateItems(5);
		SoundPlay (0);

	}


	public void community()
	{
		deactivateItems(6);
		SoundPlay (0);

	}

//	public void battle()
//	{
//		deactivateItems(7);
//
//
//	}
	public void questFormation()
	{
//		deactivateItems(16);
		deactivateItems(9);
		SoundPlay (1);
	}

	public void EventQuestFormation()
	{
//		deactivateItems(21);
		deactivateItems(9);
		SoundPlay (1);
	}

	public void win()
	{
		deactivateItems(14);
		Destroy(loads[15]);

		for(int i=0;i<scenes.Count;i++)
		{
			if(scenes.Count-2 >= 0)
			{
				if(scenes[scenes.Count-2] != null)
					scenes.RemoveAt(scenes.Count-2);
			}

		}
		loads[15] = Instantiate (Resources.Load("battle")as GameObject);

		battleInstance = loads[15].GetComponentInChildren<testBattleNew>();
		loads [15].gameObject.SetActive (false);
		SoundPlay (0);
	}



	public void lost()
	{
		deactivateItems(13);
		SoundPlay (0);
		if (objectToOpenMenu != null) {
			objectToOpenMenu.SendMessage ("ResetValues", SendMessageOptions.DontRequireReceiver);
			Debug.Log ("who reset value??  = "+objectToOpenMenu.name);
		}
		menuPanel.SetActive (false);
		Destroy(loads[15]);

		for(int i=0;i<scenes.Count;i++)
		{
			if((scenes.Count-2)>= 0)
			{
				if(scenes[scenes.Count-2] != null)
					scenes.RemoveAt(scenes.Count-2);

			}

		}
		loads[15]=Instantiate (Resources.Load("battle")as GameObject);
		battleInstance = loads[15].GetComponentInChildren<testBattleNew>();
		loads [15].gameObject.SetActive (false);
	}

	public void resultFromQuest()
	{
		SoundPlay (1);
		Debug.Log ("-----result fom quest called");
		if (objectToOpenMenu != null) {
			objectToOpenMenu.SendMessage ("ResetValues", SendMessageOptions.DontRequireReceiver);
			Debug.Log ("who reset value??  = "+objectToOpenMenu.name);
		}
		menuPanel.SetActive (false);
		loads[17].SetActive(true);
		scenes.Remove (loads[15]);
		Destroy(loads[15]);
		loads[15] = Instantiate (Resources.Load("battle")as GameObject);
		battleInstance = loads[15].GetComponentInChildren<testBattleNew>();
		loads [15].gameObject.SetActive (false);
		scenes.Add(loads[17].gameObject);
	}

	public void inventoryDupli()
	{
		deactivateItems(22);
		SoundPlay (0);

	}

//	public void StartBattle()
//	{
//		SoundPlay (2);
//		scenes.Add(loads[15].gameObject);
//		loads [4].gameObject.SetActive (false);
//		loads [15].gameObject.SetActive (true);
//		menuPanel.SetActive (false);
//	}

	public void StartBattleQuest()
	{
		SoundPlay (2);
		scenes.Add(loads[15].gameObject);
		Debug.Log ("BattleLogic._instance.battleType = "+BattleLogic._instance.battleType);
		if(BattleLogic._instance.battleType == BattleLogic.BattleType.CHEST_EVENT || BattleLogic._instance.battleType == BattleLogic.BattleType.CHEST_BOSS )
			loads [20].gameObject.SetActive (false);
		else
			loads [4].gameObject.SetActive (false);
		loads [15].gameObject.SetActive (true);
		menuPanel.SetActive (false);
	}

	public void winFromQuest()
	{
		SoundPlay (1);
		Debug.Log ("-----win fom quest called");
		if (objectToOpenMenu != null) {
			objectToOpenMenu.SendMessage ("ResetValues", SendMessageOptions.DontRequireReceiver);
			Debug.Log ("who reset value??  = "+objectToOpenMenu.name);
		}
		menuPanel.SetActive (false);
		loads[14].SetActive(true);
		scenes.Remove (loads[15]);
		Destroy(loads[15]);
		loads[15] = Instantiate (Resources.Load("battle")as GameObject);
		battleInstance = loads[15].GetComponentInChildren<testBattleNew>();
		loads [15].gameObject.SetActive (false);
		scenes.Add(loads[14].gameObject);
	}

	public void lostFromQuest()
	{
		SoundPlay (1);
		if (objectToOpenMenu != null) {
			objectToOpenMenu.SendMessage ("ResetValues", SendMessageOptions.DontRequireReceiver);
			Debug.Log ("who reset value??  = "+objectToOpenMenu.name);
		}
		menuPanel.SetActive (false);
		loads[13].SetActive(true);
		scenes.Remove (loads[15]);
		Destroy(loads[15]);
		loads[15]=Instantiate (Resources.Load("battle")as GameObject);
		battleInstance = loads[15].GetComponentInChildren<testBattleNew>();
		loads [15].gameObject.SetActive (false);
		scenes.Add(loads[13].gameObject);
	}


	public void trade()
	{
		SoundPlay (0);
		deactivateItems(2);

	}

	public void playerProfile()
	{
		playerProfilePanel.SetActive (true);
		playerProfilePanel.transform.Find("Profile Panel/NAME").GetComponent<Text>().text=PlayerDataParse._instance.playersParam.userName;
		playerProfilePanel.transform.FindChild("Profile Panel/Description/ID").GetComponent<Text>().text=PlayerDataParse._instance.playersParam.userIdNo.ToString();
		playerProfilePanel.transform.FindChild("Profile Panel/Description/CARDS").GetComponent<Text>().text= CardsManager._instance.mycards.Count.ToString();
		playerProfilePanel.transform.FindChild ("Profile Panel/Description/LEVEL").GetComponent<Text> ().text = ""+(PlayerParameters._instance.myPlayerParameter.avatar_level+1);
		playerProfilePanel.transform.FindChild ("Profile Panel/Description/GUILD").GetComponent<Text> ().text = PlayerParameters._instance.myPlayerParameter.guildName;

		TimeSpan diffLogin = TimeManager._instance.GetCurrentServerTime() - PlayerParameters._instance.myPlayerParameter.loginTime;
		playerProfilePanel.transform.FindChild ("Profile Panel/Description/Last Login").GetComponent<Text> ().text = diffLogin.Hours+"h "+diffLogin.Minutes+"m "+diffLogin.Seconds+"s";


		if (PlayerParameters._instance.myPlayerParameter.avatar_no == 1) {
			playerProfilePanel.transform.FindChild ("Profile Panel/Description/CLASS").GetComponent<Text> ().text = "Attack";


		}
		else if (PlayerParameters._instance.myPlayerParameter.avatar_no == 2) {
			playerProfilePanel.transform.FindChild ("Profile Panel/Description/CLASS").GetComponent<Text> ().text = "Defense";

		}
		else if (PlayerParameters._instance.myPlayerParameter.avatar_no == 3) {
			playerProfilePanel.transform.FindChild ("Profile Panel/Description/CLASS").GetComponent<Text> ().text = "Leadership";

		}

		playerProfilePanel.transform.FindChild ("Profile Panel/PROFILEDP").GetComponent<Image> ().sprite=playerSprite[PlayerParameters._instance.myPlayerParameter.avatar_no -1];
		playerProfilePanel.transform.FindChild ("Profile Panel/punchDes/attackText").GetComponent<Text> ().text = PlayerParameters._instance.myPlayerParameter.avatar_attack.ToString ();
		playerProfilePanel.transform.FindChild ("Profile Panel/redFlagDes/leaderText").GetComponent<Text> ().text = PlayerParameters._instance.myPlayerParameter.avatar_leadership.ToString ();
		playerProfilePanel.transform.FindChild ("Profile Panel/flagDes/deffenceText").GetComponent<Text> ().text = PlayerParameters._instance.myPlayerParameter.avatar_defense.ToString ();
		playerProfilePanel.transform.FindChild ("Profile Panel/bootDes/staminaText").GetComponent<Text> ().text = PlayerParameters._instance.myPlayerParameter.stamina.ToString ();
		playerProfilePanel.transform.FindChild ("Profile Panel/Statistics pool/statsPool").GetComponent<Text> ().text = PlayerParameters._instance.myPlayerParameter.avatar_stats_pool.ToString ();

	}


	//Shivam

	public void Referral()
	{
		ReferralUI.SetActive (true);
		ReferralPanel.SetActive (true);
		RewardsPanel.SetActive (false);
		RulePanel.SetActive (false);

//		Share.GetComponent<Image>().sprite=BrownSprite;
//		Rewards.GetComponent<Image>().sprite=YellowSprite;
//		Rule.GetComponent<Image>().sprite=YellowSprite;


		Share.GetComponent<Image>().color = new Color32(131,106,106,255);
		Rewards.GetComponent<Image>().color = new Color32(255,255,255,255);
		Rule.GetComponent<Image>().color = new Color32(255,255,255,255);

	}
	public void RewardsClick()
	{
		RewardsPanel.SetActive (true);
		ReferralPanel.SetActive (false);
		RulePanel.SetActive (false);

//		Share.GetComponent<Image>().sprite=YellowSprite;
//		Rewards.GetComponent<Image>().sprite=BrownSprite;
//		Rule.GetComponent<Image>().sprite=YellowSprite;

		Share.GetComponent<Image>().color = new Color32(255,255,255,255);
		Rewards.GetComponent<Image>().color = new Color32(131,106,106,255);
		Rule.GetComponent<Image>().color = new Color32(255,255,255,255);

	}
	public void ShareClick()
	{
		RewardsPanel.SetActive (false);
		ReferralPanel.SetActive (true);
		RulePanel.SetActive (false);

//		new Color32(131,106,106,255);
//		new Color32(255,255,255,255);
//		Share.GetComponent<Image>().sprite=BrownSprite;
//		Rewards.GetComponent<Image>().sprite=YellowSprite;
//		Rule.GetComponent<Image>().sprite=YellowSprite;

		Share.GetComponent<Image>().color = new Color32(131,106,106,255);
		Rewards.GetComponent<Image>().color = new Color32(255,255,255,255);
		Rule.GetComponent<Image>().color = new Color32(255,255,255,255);

	}
	public void ruleClick()
	{
		RewardsPanel.SetActive (false);
		ReferralPanel.SetActive (false);
		RulePanel.SetActive (true);
//		Share.GetComponent<Image>().sprite=YellowSprite;
//		Rewards.GetComponent<Image>().sprite=YellowSprite;
//		Rule.GetComponent<Image>().sprite=BrownSprite;

		Share.GetComponent<Image>().color = new Color32(255,255,255,255);
		Rewards.GetComponent<Image>().color = new Color32(255,255,255,255);
		Rule.GetComponent<Image>().color = new Color32(131,106,106,255);

	}
	public void ReferralBack()
	{
		ReferralUI.SetActive (false);
	}
	//Shivam end

	public void settings()
	{
		menuPanel.SetActive (false);
		settingsPanel.SetActive (true);
		if(objectToOpenMenu != null)
			objectToOpenMenu.SendMessage ("ResetValues", SendMessageOptions.DontRequireReceiver);

	}

	void loadTrade()
	{
		Application.LoadLevel("trade");
	}
	public  void empire()
	{
		SoundPlay (0);
		deactivateItems(3);


	}

	void CallRootMenuButtonForActivePanels(int panelCalling)
	{
		for (int i = 0; i < loads.Length; i++) {
			if(loads[i].activeInHierarchy && panelCalling != i)
			{
				loads[i].SendMessage ("RootMenuButton", SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public  void main()
	{
		SoundPlay (0);
		CallRootMenuButtonForActivePanels (0);
		scenes.Clear();
		loads[0].SetActive(true);
		scenes.Add(loads[0].gameObject);
		for(int i=1;i<loads.Length;i++)
		{
			loads[i].SetActive(false);
		}

		menuPanel.SetActive (false);
		//deactivateItems(0);


	}
	public void quest()
	{
		SoundPlay (1);
		battleFormation.isQuest = true;
		deactivateItems(4);

	}

	public void EventQuest()
	{
		BattleLogic._instance.battleType = BattleLogic.BattleType.CHEST_EVENT;
		SoundPlay (1);
		battleFormation.isQuest = true;
		deactivateItems(20);

	}


	public void ChestEvent()
	{
		SoundPlay (0);
		if (loads [19].activeSelf) {
			chestScript._instance.menuPopUp ();
		} else {
			deactivateItems (19);
		}

	}

	public void Guild()
	{
		if (loads [18].activeSelf) {
			Debug.Log ("was already active");
			GuildUIManager.instance.Menu ();
		} else {
			deactivateItems (18);
		}
		SoundPlay (0);

	}

	public void detail()
	{
		SoundPlay (0);
		deactivateItems(12);


	}
	public void shop()
	{
		SoundPlay (0);
		deactivateItems(1);

	}

	public void sotrageSecondary()
	{

	}


	void deactivateItems(int index)
	{
		if (loads [index].activeSelf) {
			if (objectToOpenMenu != null) {
				objectToOpenMenu.SendMessage ("ResetValues", SendMessageOptions.DontRequireReceiver);
				Debug.Log ("who reset value??  = " + objectToOpenMenu.name);
			}
			menuPanel.SetActive (false);
		} else {
			if (objectToOpenMenu != null) {
				objectToOpenMenu.SendMessage ("ResetValues", SendMessageOptions.DontRequireReceiver);
				Debug.Log ("who reset value??  = "+objectToOpenMenu.name);
			}
			for (int i = 0; i < loads.Length; i++) {
				if (loads [i].gameObject != null) {
					if (i == index) {

						loads [i].SetActive (true);
						if (scenes.Contains (loads [i].gameObject)) {
							scenes.RemoveAt (scenes.Count - 1);

						} else {
							scenes.Add (loads [i].gameObject);
						}


					} else {
						if (loads [i] != null)
							loads [i].SetActive (false);
					}

				}
			}
			menuPanel.SetActive (false);
		}


	}

	//---------  API -------------
	public void updateBuilding(string buildingName , int curLevel , int primaryCardNo , string primaryCardName , int buildingExp , string updateTime )
	{

		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected)
			{
				WWWForm form_time= new WWWForm ();
				string URltime = "http://ec2-54-174-178-121.compute-1.amazonaws.com/mod_web_active_api/building_upgrade.php";
				form_time.AddField ("tag","buildingupgrade");
				form_time.AddField ("user_id",PlayerDataParse._instance.playersParam.userId);
				form_time.AddField ("device_id",SystemInfo.deviceUniqueIdentifier);
				form_time.AddField ("building_name",buildingName);
				form_time.AddField ("building_level", curLevel.ToString ());
				form_time.AddField ("card_no_unlocked",primaryCardNo.ToString());
				form_time.AddField ("card_name_unlocked",primaryCardName);
				form_time.AddField ("building_exp",buildingExp.ToString());
				form_time.AddField ("upgrade_time",updateTime.ToString());
				form_time.AddField ("currentt_time",TimeManager._instance.GetCurrentServerTime ().ToString ());
				WWW www = new WWW(URltime,form_time.data);
				StartCoroutine(userTIMEfetching3(www));
			}});
	}

	public void updateBuildingInstant(string buildingName , int curLevel , int primaryCardNo , string primaryCardName , int buildingExp , string updateTime , int gems, Action<bool> callBack)
	{

		WWWForm form_time= new WWWForm ();

		string URltime = "http://ec2-54-174-178-121.compute-1.amazonaws.com/mod_web_active_api/building_upgrade.php";
		form_time.AddField ("tag","buildingupgrade");
		form_time.AddField ("user_id",PlayerDataParse._instance.playersParam.userId);
		form_time.AddField ("device_id",SystemInfo.deviceUniqueIdentifier);
		form_time.AddField ("building_name",buildingName);
		form_time.AddField ("building_level", curLevel.ToString ());
		form_time.AddField ("card_no_unlocked",primaryCardNo.ToString());
		form_time.AddField ("card_name_unlocked",primaryCardName);
		form_time.AddField ("building_exp",buildingExp.ToString());
		form_time.AddField ("upgrade_time",updateTime.ToString());
		form_time.AddField ("currentt_time",TimeManager._instance.GetCurrentServerTime ().ToString ());
		form_time.AddField ("gems",gems.ToString ());
		WWW www = new WWW(URltime,form_time.data);
		StartCoroutine(userTIMEfetching3(www , callBack));

	}



	IEnumerator userTIMEfetching3(WWW www , Action <bool> callBack = null)
	{
		yield return www;

		if (www.error == null)
		{
			Debug.Log (www.text);
			if(callBack != null)
				callBack(true);
		}
		else
		{
			Debug.Log ("ERROR MESSAGE"+www.error);
			if(callBack != null)
				callBack(false);

		}
	}
	public void instantUpdateTrainingGround(bool wasInstant = false , int gems = 0 , Action<bool> callBack = null)
	{
		if (!wasInstant) {
			//UpdateCastleFcn ();
			updateBuilding (empireScene.instance.buttonName, EmpireManager._instance.trainingGround.currentLevel, -1, EmpireManager._instance.trainingGround.primaryCardNo.ToString (), EmpireManager._instance.trainingGround.currentExp, (EmpireManager._instance.trainingGround.timeRequiredPerLevel [EmpireManager._instance.trainingGround.currentLevel] * 60).ToString ());

		} else {
			newMenuScene.instance.loader.SetActive (true);
			NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
				if (isConnected)
				{
					int cardLocked = CardsManager._instance.PositionOfCardInList (int.Parse (PlayerPrefs.GetString("tempButtonGround")));
					int expToSend = empireScene.instance.currentExperienceOfBuilding+CardsManager._instance.mycards[cardLocked].leadership;
					int levelToSend = EmpireManager._instance.trainingGround.currentLevel;
					if(expToSend >= empireScene.instance.finalExperienceOfBuilding)
					{
						expToSend = - empireScene.instance.finalExperienceOfBuilding + expToSend;
						levelToSend++;
					}
					updateBuildingInstant (empireScene.instance.buttonName, levelToSend, -1, EmpireManager._instance.trainingGround.primaryCardNo.ToString (), expToSend, (EmpireManager._instance.trainingGround.timeRequiredPerLevel [EmpireManager._instance.trainingGround.currentLevel] * 60).ToString (), gems , isSuccess =>{
						if(isSuccess)
						{
							UpdateTrainingGroundFcn();
							//UpdateCastleFcn ();
							callBack(true);
						}
						else
						{
							newMenuScene.instance.loader.SetActive (false);
							newMenuScene.instance.popupFromServer.ShowPopup ("Cannot Update at this time");
						}
						newMenuScene.instance.loader.SetActive (false);
					});

				}
				else
				{
					newMenuScene.instance.loader.SetActive (false);
					newMenuScene.instance.popupFromServer.ShowPopup ("Cannot Update at this time");
				}
			});
		}

	}

	void UpdateTrainingGroundFcn()
	{
		print("========  TRAINING  PRIMARY UPDATED =======");
		PlayerPrefs.SetString("chosenCardGround","no");
		randomCards.Add (EmpireManager._instance.trainingGround.primaryCardNo);
		EmpireManager._instance.trainingGround.primaryCardNo = -1;
		trainingGroundPrimaryImage.sprite=preloadImage;
		istrainingGroundPrimary=false;
		trainingGroundUpgradeButton.interactable=true;
		//		empireScene.instance.currentExperienceOfBuilding+=1000;

		//Debug.Log ("gold mine card = " + PlayerPrefs.GetString ("tempButtonGoldMine"));
		int cardLocked = CardsManager._instance.PositionOfCardInList (int.Parse (PlayerPrefs.GetString("tempButtonGround")));
		empireScene.instance.currentExperienceOfBuilding+=CardsManager._instance.mycards[cardLocked].leadership;

		EmpireManager._instance.trainingGround.currentExp = empireScene.instance.currentExperienceOfBuilding;
		if(empireScene.instance.currentExperienceOfBuilding >= empireScene.instance.finalExperienceOfBuilding)
		{
			//			updategoldMine.transform.localScale = Vector3.one;
			empireScene.instance.currentExperienceOfBuilding = - empireScene.instance.finalExperienceOfBuilding + empireScene.instance.currentExperienceOfBuilding;
			EmpireManager._instance.trainingGround.currentExp = empireScene.instance.currentExperienceOfBuilding;
			EmpireManager._instance.trainingGround.currentLevel++;
			int currentVal = EmpireManager._instance.trainingGround.finalValue1[EmpireManager._instance.trainingGround.currentLevel];
			int finalVal = EmpireManager._instance.trainingGround.finalValue1[EmpireManager._instance.goldMine.currentLevel+1];
			GroundNow.text=currentVal.ToString();
			GroundNew.text=finalVal.ToString();

			GroundNowSkill.text=currentVal.ToString();
			GroundNewSkill.text=finalVal.ToString();
			trainingGroundLevel.text="Lvl "+(EmpireManager._instance.trainingGround.currentLevel+1).ToString();
			empireScene.instance.finalExperienceOfBuilding = EmpireManager._instance.trainingGround.requiredExpPerLevel[EmpireManager._instance.trainingGround.currentLevel];
			if(EmpireManager._instance.trainingGround.castleLevelRequired[EmpireManager._instance.trainingGround.currentLevel] > EmpireManager._instance.castle.currentLevel)
			{
				EmpireManager._instance.trainingGround.currentExp = 0;
				empireScene.instance.currentExperienceOfBuilding = 0;
			}
		}

		float percentageVal = (empireScene.instance.currentExperienceOfBuilding/(float)empireScene.instance.finalExperienceOfBuilding);
		//		if (EmpireManager._instance.goldMine.currentLevel =) {
		//			percentageVal=0f;
		//
		//		}

		trainingGroundUpdateImageText.text = Mathf.FloorToInt (percentageVal*100)+"%";

		EmpireManager._instance.trainingGround.levelSlider.value = percentageVal;
		print ("====== CURRENT LEVEL   ====" + EmpireManager._instance.trainingGround.currentLevel);

		trainingGroundPrimaryClockString.text="00:00";
		trainingGroundPrimaryClockText = 0;
		PlayerPrefs.SetString("updatingGround","no");
		PlayerPrefs.SetString("tempButtonGround","no");
		if(empireScene.instance.buildingUpgradeLayout.activeInHierarchy==true)
		{
			empireScene.instance.ShowAllCards();
		}

		EmpireManager._instance.trainingGround.upgradeFoodCostPrimary.text=EmpireManager._instance.trainingGround.foodRequiredForPrimary [EmpireManager._instance.trainingGround.currentLevel].ToString();
		EmpireManager._instance.trainingGround.upgradeGoldCostPrimary.text=EmpireManager._instance.trainingGround.foodRequiredForPrimary [EmpireManager._instance.trainingGround.currentLevel].ToString();
		float tempTimer=EmpireManager._instance.trainingGround.timeRequiredPerLevel [EmpireManager._instance.trainingGround.currentLevel]*3600f/60;
		EmpireManager._instance.trainingGround.upgradeTimePrimary.text = tempTimer.ToString ();



	//----------------------------


		}

		public IEnumerator prisonSecondaryCheckStart()
		{
			while (!readyTogo) {
				yield return 0;
			}
			if(EmpireManager._instance.prison.secondaryCardNo > 0)
			{
				if(EmpireManager._instance.prison.timeOfLockOfSecondary != null)
				{
					System.TimeSpan diff = TimeManager._instance.GetCurrentServerTime () - EmpireManager._instance.prison.timeOfLockOfSecondary;
					float diffSeconds = Mathf.Abs((float)diff.TotalSeconds);
					float timeForActive = 3600;
					if(timeForActive > diffSeconds && diffSeconds > 0)
					{
						prisonObj.instance.isFilled = true;
						int spriteToFetch = 0;
						for(int i = 0 ; i < CardsManager._instance.mycards.Count ; i++)
						{
							if(EmpireManager._instance.prison.secondaryCardNo == CardsManager._instance.mycards[i].card_id_in_playerList)
							{
								spriteToFetch = i;
								break;
							}
						}
						Debug.Log ("id removed here------------");
						loadingScene.Instance.randomCards.Remove (EmpireManager._instance.goldMine.secondaryCardNo);
						prisonObj.instance.prisonSecondaryText = 3600 - diffSeconds;
						prisonObj.instance.prisonSecondaryImage.sprite = CardsManager._instance.mycards[spriteToFetch].cardSpriteFromResources;
					}
					else
					{
						prisonObj.instance.prisonSecondaryText = 0;
						empireScene.instance.buttonName = "prison";
						Debug.Log ("id removed here------------");
						loadingScene.Instance.randomCards.Remove (EmpireManager._instance.prison.secondaryCardNo);
						prisonObj.instance.isFilled = false;
						EmpireManager._instance.prison.secondaryCardNo = -1;
					}
				}
				else {
					prisonObj.instance.isFilled = false;
				}
			}
			else {
				prisonObj.instance.isFilled = false;
			}
		}

	private void RetrieveTimeLine() {
		TW_UserTimeLineRequest r =  TW_UserTimeLineRequest.Create();
		r.ActionComplete += OnTimeLineRequestComplete;
		r.AddParam("screen_name", "unity3d");
		r.AddParam("count", "1");
		r.Send();
	}


	private void UserLookUpRequest() {
		TW_UsersLookUpRequest r =  TW_UsersLookUpRequest.Create();
		r.ActionComplete += OnLookUpRequestComplete;
		r.AddParam("screen_name", "unity3d");
		r.Send();
	}


	private void FriedsidsRequest() {
		TW_FriendsIdsRequest r =  TW_FriendsIdsRequest.Create();
		r.ActionComplete += OnIdsLoaded;
		r.AddParam("screen_name", "unity3d");
		r.Send();
	}

	private void FollowersidsRequest() {
		TW_FollowersIdsRequest r =  TW_FollowersIdsRequest.Create();
		r.ActionComplete += OnIdsLoaded;
		r.AddParam("screen_name", "unity3d");
		r.Send();
	}

	private void TweetSearch() {
		TW_SearchTweetsRequest r =  TW_SearchTweetsRequest.Create();
		r.ActionComplete += OnSearchRequestComplete;
		r.AddParam("q", "@noradio");
		r.AddParam("count", "1");
		r.Send();
	}




	// --------------------------------------
	// Events
	// --------------------------------------

	private void OnIdsLoaded(TW_APIRequstResult result) {

		if(result.IsSucceeded) {
			Debug.Log( "Totals ids loaded: " + result.ids.Count);
		} else {
			Debug.Log(result.responce);
		}
	}


	private void OnLookUpRequestComplete(TW_APIRequstResult result) {

		if(result.IsSucceeded) {
			string msg = "User Id: ";
			msg+= result.users[0].id;
			msg+= "\n";
			msg+= "User Name:" + result.users[0].name;


			Debug.Log(msg);
		} else {
			Debug.Log(result.responce);

		}
	}


	private void OnSearchRequestComplete(TW_APIRequstResult result) {

		if(result.IsSucceeded) {
			string msg = "Tweet text:" + "\n";
			msg+= result.tweets[0].text;


			Debug.Log(msg);
		} else {
			Debug.Log(result.responce);
		}

	}


	private void OnTimeLineRequestComplete(TW_APIRequstResult result) {


		if(result.IsSucceeded) {

			string msg ;
			if(result.tweets.Count > 0) {
				msg = "Last Tweet text:" + "\n";
				msg+= result.tweets[0].text;
			} else {
				msg = "NO tweens found";
			}



			Debug.Log(msg);
		} else {
			Debug.Log(result.responce);
		}

	}




	// --------------------------------------
	// PRIVATE METHODS
	// --------------------------------------

	private IEnumerator PostScreenshot() {


		yield return new WaitForEndOfFrame();
		// Create a texture the size of the screen, RGB24 format
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D( width, height, TextureFormat.RGB24, false );
		// Read screen contents into the texture
		tex.ReadPixels( new Rect(0, 0, width, height), 0, 0 );
		tex.Apply();

		SPTwitter.instance.Post("My app ScreehShot", tex);

		Destroy(tex);

	}

	private void LogOut() {
		IsUserInfoLoaded = false;

		IsAuntifivated = false;

		SPTwitter.instance.LogOut();
	}
		/*void OnGUI()
		{
			if(GUI.Button(new Rect(100,100,50,50) , "ORB"))
			{
				PlayerParameters._instance.myPlayerParameter.orb = PlayerParameters._instance.myPlayerParameter.maxOrb;
			}
		}*/
	}
