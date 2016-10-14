using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OpponentPrefab : MonoBehaviour {
	public Text playerName;
	public Image playerAvatar;
	public Image image2;
	public Text textAgainstImage2;
	public Text playerLevel;
	public Text playerRank;
	public Text attack;
	public Text defense;
	public Text leadership;
	public Button goToBattle;
	public int playerId;
	public int idInList;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OpponentClicked()
	{
		int noOfOrbsDeduct = BattleLogic._instance.AttackingOrbsUsed (BattleOpponentSelection._instance.listOfOpponentDetails[idInList].playerLevel);

		if (PlayerParameters._instance.myPlayerParameter.orb >= noOfOrbsDeduct) {
			BattleLogic._instance.orbsTosubtract = noOfOrbsDeduct;
			BattleOpponentSelection._instance.FetchOpponentDetails (playerId);
		} else {
			 loadingScene.Instance.popupFromServer.ShowPopup ("Not Enough Orbs!");
		}




	}
}
