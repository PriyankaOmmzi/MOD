using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ChestMain : MonoBehaviour {

	/// <summary>
	/// TOP SCREEN
	/// </summary>
	public Text staminaTimer;
	public Text orbTimer;
	public Text wheatText;
	public Text goldText;
	public Text staminaText;
	public Text avatarLevelText;
	public Text avatarPercentageText;
	public Slider avatarPercentage;
	public Image avatarImage;
	public List<Image> attackingOrbs = new List<Image>();
	public Sprite activatedOrb , deactivedOrb;

	public Text toNextMilestone;
	public Text rank;
	public Text guildRank;
	public Text normalKeysText;
	public Text royalKeysText;
	public Text pointsText;
	public Text eventBonusText;

	public static ChestMain _instance;
	// Use this for initialization
	void Start () {
		DisplayFinalText ();
		_instance = this;
	}

	void OnEnable()
	{
		if (!chestScript._instance.fetchedWoodenChests) {
			loadingScene.Instance.loader.SetActive (true);
			NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
				if (isConnected) {
					StartCoroutine (chestScript._instance.FetchChests (0, (isSuccess, msg) => {
						if (isSuccess || msg != "Network Error!") {
							chestScript._instance.fetchedWoodenChests = true;
							loadingScene.Instance.loader.SetActive (false);
							if(!isSuccess)
								loadingScene.Instance.popupFromServer.ShowPopup (msg);
						} 
						else
							loadingScene.Instance.popupFromServer.ShowPopup (msg);
					}));
				} else {
					loadingScene.Instance.popupFromServer.ShowPopup ("Network Error!");
				}
			});

		}
		DisplayFinalText ();
	}
	// Update is called once per frame
	void Update () {

		orbTimer.text = newMenuScene.instance.orbsText.text;
		staminaTimer.text = newMenuScene.instance.timerText.text;
	}

	public void DisplayFinalText()
	{
		if (newMenuScene.instance.timerDecrease > 0)
			staminaTimer.text = "00" + ":" + Mathf.Abs (newMenuScene.instance.timerDecrease % 60).ToString ("00");
		else
			staminaTimer.text = "00:00";

		if (newMenuScene.instance.timerDecreaseOrbs > 0)
			orbTimer.text = "00" + ":" + Mathf.Abs (newMenuScene.instance.timerDecreaseOrbs % 60).ToString ("00");
		else
			orbTimer.text = "00:00";
		wheatText.text = PlayerParameters._instance.myPlayerParameter.wheat.ToString ();
		goldText.text = PlayerParameters._instance.myPlayerParameter.gold.ToString ();
		staminaText.text = PlayerParameters._instance.myPlayerParameter.stamina.ToString () +"/"+ PlayerParameters._instance.myPlayerParameter.max_stamina;
		avatarLevelText.text = "Lvl" + (PlayerParameters._instance.myPlayerParameter.avatar_level+1);

		System.Int64 reqdAvtarExpForLevelUp = 0;
		if (PlayerParameters._instance.myPlayerParameter.avatar_level < PlayerParameters._instance.avatarReqdExpForLevelUp.Length) {
			reqdAvtarExpForLevelUp = PlayerParameters._instance.avatarReqdExpForLevelUp [PlayerParameters._instance.myPlayerParameter.avatar_level];
		} else {
			reqdAvtarExpForLevelUp = PlayerParameters._instance.avatarReqdExpForLevelUp [PlayerParameters._instance.avatarReqdExpForLevelUp.Length-1];
		}
		double percentValForAvatar = PlayerParameters._instance.myPlayerParameter.avatar_exp / (double)reqdAvtarExpForLevelUp;
		avatarPercentage.value = (float)percentValForAvatar;
		avatarPercentageText.text = Mathf.FloorToInt(avatarPercentage.value*100)+"%";

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
		avatarImage.sprite = loadingScene.Instance.playerSprite[PlayerParameters._instance.myPlayerParameter.avatar_no - 1];

		//TODO:
//		toNextMilestone.text = ;
//		eventBonusText.text
		rank.text = chestScript._instance.playerRank.ToString();
		guildRank.text = chestScript._instance.guildRank.ToString();
		normalKeysText.text = ChestData._instance.chestData.peasantKeys.ToString();
		royalKeysText.text = ChestData._instance.chestData.royalKeys.ToString();
		pointsText.text = ChestData._instance.chestData.eventPoints.ToString();

	}


}
