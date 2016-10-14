using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeCycleRewardElement : MonoBehaviour {
	public Text cycleRewardDC;
	public Text cycleRewardED;
	public Text cycleRewardEventPoints;
	public Text rank;

	public void CycleRewardsEDDetails(Text detailsText)
	{
		string countOfReward = detailsText.text.Substring (1);
		loadingScene.Instance.popupFromServer.ShowPopup ("You will receive "+countOfReward+" Energy Drinks!");
	}

	public void CycleRewardsDCDetails(Text detailsText)
	{
		string countOfReward = detailsText.text.Substring (1);
		loadingScene.Instance.popupFromServer.ShowPopup ("You will receive "+countOfReward+" Dragon Coins!");
	}
}
