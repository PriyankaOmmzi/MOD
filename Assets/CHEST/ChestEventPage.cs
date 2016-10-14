using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChestEventPage : MonoBehaviour {
	//Blessing
	public Slider blessingSlider;
	public Text blessingHeading;

	//Cycle
	public Image cycleRadial;
	public Text cycleHeading;
	public GameObject cycleNeedle;
	public Text cycleTimer;

	public Text eventTimer;
	public List<ChestEventMainPageScrollElement> scrollElements;

	public Toggle alertToggle;

	// Use this for initialization
	void Start () {
		
	}


	public void ToChest()
	{
		chestScript._instance.deactivateItems (1);

	}

	public void ToQuest()
	{
		loadingScene.Instance.EventQuest ();
	}

	public void Rules()
	{
		chestScript._instance.deactivateItems (2);
	}

	public void Rewards()
	{
		chestScript._instance.deactivateItems (3);
	}

	public void AlertToggle()
	{
		alertToggle.isOn = !alertToggle.isOn;
	}

	public void EditFormation()
	{
		if (PlayerParameters._instance.myPlayerParameter.questFormationDeck == 0)
			loadingScene.Instance.EventQuestFormation ();
		else
			loadingScene.Instance.popupFromServer.ShowPopup ("Your Deck has already been locked for quest!");	
	}
}
