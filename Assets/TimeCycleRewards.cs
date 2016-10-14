using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class CycleRewards
{
	public int energyDrinks;
	public int dragonCoins;
	public int eventPoints;
}

public class TimeCycleRewards : MonoBehaviour {
	public GameObject cycleRewards;
	public CycleRank []cycleRanks;
	public TimeCycleRewardElement []timeCycleRewardElement;
	int currentCycle;

	public CycleRewards []normalCycleReward;
	public CycleRewards []goldenCycleReward;
	public CycleRewards []lastGoldenCycleReward;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnEnable()
	{
		loadingScene.Instance.loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected) {
				StartCoroutine (GetCurrentCycleRank ( (isSuccess, msg) => {
					if (isSuccess) {
						loadingScene.Instance.loader.SetActive (false);
					} else
						loadingScene.Instance.popupFromServer.ShowPopup (msg);
				}));
			} else {
				loadingScene.Instance.popupFromServer.ShowPopup ("Network Error!");
			}
		});
	}

	public IEnumerator GetPreviousCycleRank(System.Action<bool , string> callBack)
	{
		yield return 0;
		callBack (true,"");
	}


	public IEnumerator GetCurrentCycleRank(System.Action<bool , string> callBack)
	{
		yield return 0;
		callBack (true,"");
	}

	public void GetNormalCycleRank()
	{
		loadingScene.Instance.loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected) {
				if (chestScript._instance.currentRunningCycle == chestScript.CycleTYpes.GOLDEN_CYCLE) {
			
					StartCoroutine (GetPreviousCycleRank ((isSuccess, msg) => {
						if (isSuccess) {
							loadingScene.Instance.loader.SetActive (false);
						} else
							loadingScene.Instance.popupFromServer.ShowPopup (msg);
					}));
				
				} else {
					StartCoroutine (GetCurrentCycleRank ((isSuccess, msg) => {
						if (isSuccess) {
							loadingScene.Instance.loader.SetActive (false);
						} else
							loadingScene.Instance.popupFromServer.ShowPopup (msg);
					}));
		
				}
			} else {
				loadingScene.Instance.popupFromServer.ShowPopup ("Network Error!");
			}
		});
	}

	public void GetGoldenCycleRank()
	{
		loadingScene.Instance.loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected) {
				if (chestScript._instance.currentRunningCycle == chestScript.CycleTYpes.NORMAL_CYCLE) {

					StartCoroutine (GetPreviousCycleRank ((isSuccess, msg) => {
						if (isSuccess) {
							loadingScene.Instance.loader.SetActive (false);
						} else
							loadingScene.Instance.popupFromServer.ShowPopup (msg);
					}));

				} else {
					StartCoroutine (GetCurrentCycleRank ((isSuccess, msg) => {
						if (isSuccess) {
							loadingScene.Instance.loader.SetActive (false);
						} else
							loadingScene.Instance.popupFromServer.ShowPopup (msg);
					}));

				}
			} else {
				loadingScene.Instance.popupFromServer.ShowPopup ("Network Error!");
			}
		});
	}

	public void CycleRewards()
	{
		cycleRewards.gameObject.SetActive (true);
		if (chestScript._instance.currentRunningCycle == chestScript.CycleTYpes.GOLDEN_CYCLE) {

			System.TimeSpan cycleTimeDiff = chestScript._instance.cycleEndTime - TimeManager._instance.GetCurrentServerTime ();
			if (cycleTimeDiff.TotalDays < 2f) {
				for (int i = 0; i < timeCycleRewardElement.Length; i++) {
					timeCycleRewardElement [i].cycleRewardDC.text = "x"+lastGoldenCycleReward [i].dragonCoins.ToString();
					timeCycleRewardElement [i].cycleRewardED.text = "x"+lastGoldenCycleReward [i].energyDrinks.ToString();
					timeCycleRewardElement [i].cycleRewardEventPoints.text = lastGoldenCycleReward [i].eventPoints.ToString();
				}
			} else {
				for (int i = 0; i < timeCycleRewardElement.Length; i++) {
					timeCycleRewardElement [i].cycleRewardDC.text = "x"+goldenCycleReward [i].dragonCoins.ToString();
					timeCycleRewardElement [i].cycleRewardED.text = "x"+goldenCycleReward [i].energyDrinks.ToString();
					timeCycleRewardElement [i].cycleRewardEventPoints.text = goldenCycleReward [i].eventPoints.ToString();
				}
			}
		
		} else {
			for (int i = 0; i < timeCycleRewardElement.Length; i++) {
				timeCycleRewardElement [i].cycleRewardDC.text = "x"+normalCycleReward [i].dragonCoins.ToString();
				timeCycleRewardElement [i].cycleRewardED.text = "x"+normalCycleReward [i].energyDrinks.ToString();
				timeCycleRewardElement [i].cycleRewardEventPoints.text = normalCycleReward [i].eventPoints.ToString();
			}
		}
	}


}
