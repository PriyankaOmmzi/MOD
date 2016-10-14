using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using MiniJSON;

public class EmpireManager : MonoBehaviour
{

	public static EmpireManager _instance;
	[System.Serializable]
	public struct BuildingParamters
	{
		public List<int> requiredExpPerLevel;
		public List<float> timeRequiredPerLevel;
		public List<int> castleLevelRequired;
		public List<int> foodRequiredForPrimary;
		public List<int> foodRequiredForSecondary;
		public List<int> finalValue1;
		public List<int> finalValue2;
		public List<int> fearIncrease;
		public Button instantUpdateButton;
		public Button chosenCardButton;
		public Button storgeLockDown;

		public int currentLevel;  // from server
		public int currentExp; //from server
		public int primaryCardNo; //from server
		public string primaryCardName; //from server
		public int secondaryCardNo; //from server
		public string secondaryCardName; //from server
		public Image gemsBar;
		Vector3 gemsBarV;
		public System.DateTime timeOfLockOfPrimary;
		public System.DateTime timeOfLockOfSecondary;

		public Text goldText, foodText,goldText2, foodText2,goldText3, foodText3,goldText4, foodText4;
		public Text upgradeGoldCostPrimary , upgradeFoodCostPrimary;
		public Text upgradeGoldCostSecondary , upgradeFoodCostSecondary;
		public Text upgradeTimePrimary ;
		public Slider levelSlider;
	}

	public BuildingParamters castle, storage,gate,trainingGround,prison,barracks,lab,treeOfKnowledge,goldMine,barn;
//	public int totalGems;
//	public int currentFood;
	// Use this for initialization
	void Awake () {
		_instance = this;
		for (int i = 0; i < 10; i++) {
			castle.requiredExpPerLevel [i] *= 30;
			storage.requiredExpPerLevel [i] *= 30;
			gate.requiredExpPerLevel [i] *= 30;
			trainingGround.requiredExpPerLevel [i] *= 30;
			prison.requiredExpPerLevel [i] *= 30;
			barracks.requiredExpPerLevel [i] *= 30;
//			lab.requiredExpPerLevel [i] *= 30;
//			treeOfKnowledge.requiredExpPerLevel [i] *= 30;
			goldMine.requiredExpPerLevel [i] *= 30;
			barn.requiredExpPerLevel [i] *= 30;
		}
	}


	// Update is called once per frame
	void Update () {
//		if(castle.totalGems < castle.gemsRequiredForInstantUpgrade[EmpireManager._instance.castle.currentLevel])
//		{
//			castle.instantUpdateButton.interactable=false;
//		}
	}


	public void EmpireValues(IList buildingValues)
	{
		Debug.Log ("EmpireValues      was called" +buildingValues.Count + "  "+buildingValues.ToString ().Contains ("building_name"));
		if (buildingValues.Count > 2) {
			for (int i = 0; i < buildingValues.Count; i++) {
				IDictionary buildingParams = (IDictionary)buildingValues [i];
				if (buildingParams ["building_name"].ToString () == "castle") {
					int.TryParse (buildingParams ["level"].ToString (), out castle.currentLevel);
					int.TryParse (buildingParams ["building_exp"].ToString (), out castle.currentExp);
					int.TryParse (buildingParams ["primary_card_locked_no"].ToString (), out castle.primaryCardNo);
					castle.primaryCardName = buildingParams ["primary_card_locked"].ToString ();
					int.TryParse (buildingParams ["secondary_card_no"].ToString (), out castle.secondaryCardNo);
					castle.secondaryCardName = buildingParams ["secondary_card_locked"].ToString ();
					if (buildingParams ["currentt_time"].ToString () != "0000-00-00 00:00:00" && !string.IsNullOrEmpty (buildingParams ["currentt_time"].ToString ())) {
						castle.timeOfLockOfPrimary = System.Convert.ToDateTime (buildingParams ["currentt_time"].ToString ());
					}

					Debug.Log("castle.timeOfLockOfPrimary   ==  "+castle.timeOfLockOfPrimary);

					if (buildingParams ["time_of_secondary_upgrade"].ToString () != "0000-00-00 00:00:00" && !string.IsNullOrEmpty (buildingParams ["time_of_secondary_upgrade"].ToString ())) {
						castle.timeOfLockOfSecondary = System.Convert.ToDateTime (buildingParams ["time_of_secondary_upgrade"].ToString ());
					}


					StartCoroutine(loadingScene.Instance.CheckForCastleInStart());


				} else if (buildingParams ["building_name"].ToString () == "storage") {
					int.TryParse (buildingParams ["level"].ToString (), out storage.currentLevel);
					int.TryParse (buildingParams ["building_exp"].ToString (), out storage.currentExp);
					int.TryParse (buildingParams ["primary_card_locked_no"].ToString (), out storage.primaryCardNo);
					storage.primaryCardName = buildingParams ["primary_card_locked"].ToString ();
					int.TryParse (buildingParams ["secondary_card_no"].ToString (), out storage.secondaryCardNo);
					storage.secondaryCardName = buildingParams ["secondary_card_locked"].ToString ();
					if (buildingParams ["currentt_time"].ToString () != "0000-00-00 00:00:00" && !string.IsNullOrEmpty (buildingParams ["currentt_time"].ToString ())) {
						storage.timeOfLockOfPrimary = System.Convert.ToDateTime (buildingParams ["currentt_time"].ToString ());
						Debug.Log("storage.timeOfLockOfPrimary = "+storage.timeOfLockOfPrimary);
					}
					if (buildingParams ["time_of_secondary_upgrade"].ToString () != "0000-00-00 00:00:00" && !string.IsNullOrEmpty (buildingParams ["time_of_secondary_upgrade"].ToString ())) {
						storage.timeOfLockOfSecondary = System.Convert.ToDateTime (buildingParams ["time_of_secondary_upgrade"].ToString ());
						Debug.Log("storage.timeOfLockOfSecondary = "+storage.timeOfLockOfSecondary);
					}

					Debug.Log("coroutine called for storage... --------");
					StartCoroutine(loadingScene.Instance.CheckForStorageInStart());
				} else if (buildingParams ["building_name"].ToString () == "barrack") {
					int.TryParse (buildingParams ["level"].ToString (), out barracks.currentLevel);
					int.TryParse (buildingParams ["building_exp"].ToString (), out barracks.currentExp);
					int.TryParse (buildingParams ["primary_card_locked_no"].ToString (), out barracks.primaryCardNo);
					barracks.primaryCardName = buildingParams ["primary_card_locked"].ToString ();
					int.TryParse (buildingParams ["secondary_card_no"].ToString (), out barracks.secondaryCardNo);
					barracks.secondaryCardName = buildingParams ["secondary_card_locked"].ToString ();
					if (buildingParams ["currentt_time"].ToString () != "0000-00-00 00:00:00" && !string.IsNullOrEmpty (buildingParams ["currentt_time"].ToString ())) {
						barracks.timeOfLockOfPrimary = System.Convert.ToDateTime (buildingParams ["currentt_time"].ToString ());
					}
					if (buildingParams ["time_of_secondary_upgrade"].ToString () != "0000-00-00 00:00:00" && !string.IsNullOrEmpty (buildingParams ["time_of_secondary_upgrade"].ToString ())) {
						barracks.timeOfLockOfSecondary = System.Convert.ToDateTime (buildingParams ["time_of_secondary_upgrade"].ToString ());
					}
					StartCoroutine(loadingScene.Instance.CheckForBarrackInStart());


				} else if (buildingParams ["building_name"].ToString () == "goldMine") {
					int.TryParse (buildingParams ["level"].ToString (), out goldMine.currentLevel);
					int.TryParse (buildingParams ["building_exp"].ToString (), out goldMine.currentExp);
					int.TryParse (buildingParams ["primary_card_locked_no"].ToString (), out goldMine.primaryCardNo);
					goldMine.primaryCardName = buildingParams ["primary_card_locked"].ToString ();
					int.TryParse (buildingParams ["secondary_card_no"].ToString (), out goldMine.secondaryCardNo);
					goldMine.secondaryCardName = buildingParams ["secondary_card_locked"].ToString ();
					if (buildingParams ["currentt_time"].ToString () != "0000-00-00 00:00:00" && !string.IsNullOrEmpty (buildingParams ["currentt_time"].ToString ())) {
						goldMine.timeOfLockOfPrimary = System.Convert.ToDateTime (buildingParams ["currentt_time"].ToString ());
					}
					if (buildingParams ["time_of_secondary_upgrade"].ToString () != "0000-00-00 00:00:00" && !string.IsNullOrEmpty (buildingParams ["time_of_secondary_upgrade"].ToString ())) {
						goldMine.timeOfLockOfSecondary = System.Convert.ToDateTime (buildingParams ["time_of_secondary_upgrade"].ToString ());
					}
					StartCoroutine(loadingScene.Instance.CheckForGoldMineInStart());
				} else if (buildingParams ["building_name"].ToString () == "barn") {
					int.TryParse (buildingParams ["level"].ToString (), out barn.currentLevel);
					int.TryParse (buildingParams ["building_exp"].ToString (), out barn.currentExp);
					int.TryParse (buildingParams ["primary_card_locked_no"].ToString (), out barn.primaryCardNo);
					barn.primaryCardName = buildingParams ["primary_card_locked"].ToString ();
					int.TryParse (buildingParams ["secondary_card_no"].ToString (), out barn.secondaryCardNo);
					barn.secondaryCardName = buildingParams ["secondary_card_locked"].ToString ();
					if (buildingParams ["currentt_time"].ToString () != "0000-00-00 00:00:00" && !string.IsNullOrEmpty (buildingParams ["currentt_time"].ToString ())) {
						barn.timeOfLockOfPrimary = System.Convert.ToDateTime (buildingParams ["currentt_time"].ToString ());
					}
					if (buildingParams ["time_of_secondary_upgrade"].ToString () != "0000-00-00 00:00:00" && !string.IsNullOrEmpty (buildingParams ["time_of_secondary_upgrade"].ToString ())) {
						barn.timeOfLockOfSecondary = System.Convert.ToDateTime (buildingParams ["time_of_secondary_upgrade"].ToString ());
					}
					StartCoroutine(loadingScene.Instance.CheckForBarnInStart());
				} else if (buildingParams ["building_name"].ToString () == "traningGround") {
					print("====   STARTING TRAINING GROUND =====");
					int.TryParse (buildingParams ["level"].ToString (), out trainingGround.currentLevel);
					int.TryParse (buildingParams ["building_exp"].ToString (), out trainingGround.currentExp);
					int.TryParse (buildingParams ["primary_card_locked_no"].ToString (), out trainingGround.primaryCardNo);
					trainingGround.primaryCardName = buildingParams ["primary_card_locked"].ToString ();
					int.TryParse (buildingParams ["secondary_card_no"].ToString (), out trainingGround.secondaryCardNo);
					trainingGround.secondaryCardName = buildingParams ["secondary_card_locked"].ToString ();
					if (buildingParams ["currentt_time"].ToString () != "0000-00-00 00:00:00" && !string.IsNullOrEmpty (buildingParams ["currentt_time"].ToString ())) {
						trainingGround.timeOfLockOfPrimary = System.Convert.ToDateTime (buildingParams ["currentt_time"].ToString ());
					}
					if (buildingParams ["time_of_secondary_upgrade"].ToString () != "0000-00-00 00:00:00" && !string.IsNullOrEmpty (buildingParams ["time_of_secondary_upgrade"].ToString ())) {
						trainingGround.timeOfLockOfSecondary = System.Convert.ToDateTime (buildingParams ["time_of_secondary_upgrade"].ToString ());
					}
					StartCoroutine(loadingScene.Instance.CheckForTrainingGroundInStart());

				} else if (buildingParams ["building_name"].ToString () == "prison") {
					int.TryParse (buildingParams ["level"].ToString (), out prison.currentLevel);
					int.TryParse (buildingParams ["building_exp"].ToString (), out prison.currentExp);
					int.TryParse (buildingParams ["primary_card_locked_no"].ToString (), out prison.primaryCardNo);
					prison.primaryCardName = buildingParams ["primary_card_locked"].ToString ();
					int.TryParse (buildingParams ["secondary_card_no"].ToString (), out prison.secondaryCardNo);
					prison.secondaryCardName = buildingParams ["secondary_card_locked"].ToString ();
					if (buildingParams ["currentt_time"].ToString () != "0000-00-00 00:00:00" && !string.IsNullOrEmpty (buildingParams ["currentt_time"].ToString ())) {
						prison.timeOfLockOfPrimary = System.Convert.ToDateTime (buildingParams ["currentt_time"].ToString ());
					}
					if (buildingParams ["time_of_secondary_upgrade"].ToString () != "0000-00-00 00:00:00" && !string.IsNullOrEmpty (buildingParams ["time_of_secondary_upgrade"].ToString ())) {
						prison.timeOfLockOfSecondary = System.Convert.ToDateTime (buildingParams ["time_of_secondary_upgrade"].ToString ());
					}
					StartCoroutine(prisonObj.instance.CheckForPrisonInStart());
					//StartCoroutine(loadingScene.Instance.prisonSecondaryCheckStart());

				} else if (buildingParams ["building_name"].ToString () == "lab") {
					int.TryParse (buildingParams ["level"].ToString (), out lab.currentLevel);
					int.TryParse (buildingParams ["building_exp"].ToString (), out lab.currentExp);
					int.TryParse (buildingParams ["primary_card_locked_no"].ToString (), out lab.primaryCardNo);
					lab.primaryCardName = buildingParams ["primary_card_locked"].ToString ();
					int.TryParse (buildingParams ["secondary_card_no"].ToString (), out lab.secondaryCardNo);
					lab.secondaryCardName = buildingParams ["secondary_card_locked"].ToString ();
					if (buildingParams ["currentt_time"].ToString () != "0000-00-00 00:00:00" && !string.IsNullOrEmpty (buildingParams ["currentt_time"].ToString ())) {
						lab.timeOfLockOfPrimary = System.Convert.ToDateTime (buildingParams ["currentt_time"].ToString ());
					}
					if (buildingParams ["time_of_secondary_upgrade"].ToString () != "0000-00-00 00:00:00" && !string.IsNullOrEmpty (buildingParams ["time_of_secondary_upgrade"].ToString ())) {
						lab.timeOfLockOfSecondary = System.Convert.ToDateTime (buildingParams ["time_of_secondary_upgrade"].ToString ());
					}
				} else if (buildingParams ["building_name"].ToString () == "gate") {
					int.TryParse (buildingParams ["level"].ToString (), out gate.currentLevel);
					int.TryParse (buildingParams ["building_exp"].ToString (), out gate.currentExp);
					int.TryParse (buildingParams ["primary_card_locked_no"].ToString (), out gate.primaryCardNo);
					gate.primaryCardName = buildingParams ["primary_card_locked"].ToString ();
					int.TryParse (buildingParams ["secondary_card_no"].ToString (), out gate.secondaryCardNo);
					gate.secondaryCardName = buildingParams ["secondary_card_locked"].ToString ();
					if (buildingParams ["currentt_time"].ToString () != "0000-00-00 00:00:00" && !string.IsNullOrEmpty (buildingParams ["currentt_time"].ToString ())) {
						gate.timeOfLockOfPrimary = System.Convert.ToDateTime (buildingParams ["currentt_time"].ToString ());
					}
					if (buildingParams ["time_of_secondary_upgrade"].ToString () != "0000-00-00 00:00:00" && !string.IsNullOrEmpty (buildingParams ["time_of_secondary_upgrade"].ToString ())) {
						gate.timeOfLockOfSecondary = System.Convert.ToDateTime (buildingParams ["time_of_secondary_upgrade"].ToString ());
					}
					StartCoroutine(loadingScene.Instance.CheckForGateInStart());
				} else if (buildingParams ["building_name"].ToString () == "treeOfKnowledge") {
					int.TryParse (buildingParams ["level"].ToString (), out treeOfKnowledge.currentLevel);
					int.TryParse (buildingParams ["building_exp"].ToString (), out treeOfKnowledge.currentExp);
					int.TryParse (buildingParams ["primary_card_locked_no"].ToString (), out treeOfKnowledge.primaryCardNo);
					treeOfKnowledge.primaryCardName = buildingParams ["primary_card_locked"].ToString ();
					int.TryParse (buildingParams ["secondary_card_no"].ToString (), out treeOfKnowledge.secondaryCardNo);
					treeOfKnowledge.secondaryCardName = buildingParams ["secondary_card_locked"].ToString ();
					if (buildingParams ["currentt_time"].ToString () != "0000-00-00 00:00:00" && !string.IsNullOrEmpty (buildingParams ["currentt_time"].ToString ())) {
						treeOfKnowledge.timeOfLockOfPrimary = System.Convert.ToDateTime (buildingParams ["currentt_time"].ToString ());
					}
					if (buildingParams ["time_of_secondary_upgrade"].ToString () != "0000-00-00 00:00:00" && !string.IsNullOrEmpty (buildingParams ["time_of_secondary_upgrade"].ToString ())) {
						treeOfKnowledge.timeOfLockOfSecondary = System.Convert.ToDateTime (buildingParams ["time_of_secondary_upgrade"].ToString ());
					}
				}



			}
			//"building_data":[{"building_id":"0","level":"0","primary_card_locked":"4","primary_card_locked_no":"4",
			//"upgrade_time":"30","initial_value1":"","final_value1":"","currentt_time":"07\/08\/2016 04:43:29","max_deployed":"",
			//"currently_deployed":"","max_available":"","currently_available":"","secondary_card_locked":"","secondary_card_no":"0",
			//"sec_card_status":"0","soldiers_to_recruit":"","time_of_secondary_upgrade":"","building_exp":"0","user_id":"62",
			//"building_name":"castle","status":"1","upgraded_value":"0","active_time":""}]
		}
	}

	public IEnumerator GetAllBuildings()
	{
//		string url = "http://www.ommzi.com/new_app/data_all_buildings.php";
		string url = "http://ec2-54-174-178-121.compute-1.amazonaws.com/mod_web_active_api/data_all_buildings.php";
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("tag" ,"buildingdata");
		wwwForm.AddField ("user_id" , PlayerDataParse._instance.playersParam.userId);
		wwwForm.AddField ("device_id" , SystemInfo.deviceUniqueIdentifier );
		WWW wwwGetBuildings = new WWW(url , wwwForm);
		while (!TimeManager.foundTime) {
			yield return 0;
		}
		yield return wwwGetBuildings;
		if (wwwGetBuildings.error == null) {
			Debug.Log("building text = "+wwwGetBuildings.text);
			if(wwwGetBuildings.text.Contains ("success"))
			{
				IDictionary buildingsDict = (IDictionary) Json.Deserialize(wwwGetBuildings.text);
				IList buildingsList = (IList) buildingsDict["Building_detail"];
				for(int i = 0 ; i < buildingsList.Count ; i++)
				{
					IDictionary perBuildingData = (IDictionary) buildingsList[i];
				}
			}
			else if(wwwGetBuildings.text.Contains ("No Building exist"))
			{
				//Work complete
			}
			else
			{
				newMenuScene.instance.gameStopPopup.ShowPopup ("Error Fetching Data");
			}
			//{"success":0,"error_msg":"No Building exist!"}

			//{"success":1,"msg":"Building data success!","Building_detail":[{"user_id":"40","building_name":"abc",
			//"building_level":"8989898","card_name":"qwerty","card_no":"675","currentt_time":"2016-07-05 03:41:25",
			//"building_exp":"gjgjg","time_sec_upgrade":"","active_time":"","upgraded_value":"0","card_status":"0"},
		//{"user_id":"40","building_name":"castle","building_level":"0","card_name":"1",
			//"card_no":"1","currentt_time":"07\/08\/2016 00:44:26","building_exp":"26","time_sec_upgrade":"",
			//"active_time":"","upgraded_value":"0","card_status":"1"}]}
		} else {
			newMenuScene.instance.gameStopPopup.ShowPopup ("Error Fetching Data");
		}
	}
}
