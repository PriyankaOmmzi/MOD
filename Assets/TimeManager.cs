using UnityEngine;
using System.Collections;
using MiniJSON;
using System;

public class TimeManager : MonoBehaviour {
	public static TimeManager _instance;
	public DateTime serverTimeOnStart;
	public DateTime currentServerTime;
	public DateTime timeLapseSinceStart;
	public TimeSpan differenceInTheServerAndSystemTime;
	public DateTime systemTimeOnStart;
	public Popup gameStopPopup;
	public GameObject timeLoader;
	public static bool foundTime;
	bool firstTimeDone;
	System.DateTime previousServerTime;

	void Awake() {
		_instance = this;
	}

	// Use this for initialization
	void Start () {
			NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
				if (isConnected)
					StartCoroutine (ServerTime(false));
			});
	}


//	//{"success":0,"error_msg":"Invalid details!"}
//	void OnApplicationPause(bool isPause)
//	{
//		Debug.Log ("isPause = "+isPause);
//		if (!isPause && firstTimeDone) {
//			System.DateTime previousServerTime = currentServerTime;
//			systemTimeOnStart = DateTime.Now;
//			differenceInTheServerAndSystemTime = serverTimeOnStart - systemTimeOnStart;
//			currentServerTime = systemTimeOnStart.Add (differenceInTheServerAndSystemTime);
//			TimeSpan differnceInTheTwoTimesSinceGamePaused = currentServerTime - previousServerTime;
//			float differenceInTimesInSec = (float)differnceInTheTwoTimesSinceGamePaused.TotalSeconds;
//			Debug.Log("  return from pause  ==   "+currentServerTime +" , diff = "+differenceInTimesInSec);
//			Debug.Log(" differnceInTheTwoTimesSinceGamePaused = "+differnceInTheTwoTimesSinceGamePaused);
//			if(differenceInTimesInSec < 0)
//				differenceInTimesInSec = Mathf.Abs (differenceInTimesInSec);
//			if (loadingScene.Instance != null) {
//				if(loadingScene.Instance.castlePrimaryClockText > 0)
//					loadingScene.Instance.castlePrimaryClockText-=differenceInTimesInSec;
//
//				if(loadingScene.Instance.storagePrimaryClockText > 0)
//					loadingScene.Instance.storagePrimaryClockText-=differenceInTimesInSec;
//
//				if(loadingScene.Instance.storageSecondaryText > 0)
//					loadingScene.Instance.storageSecondaryText-=differenceInTimesInSec;
//
//				if(loadingScene.Instance.barnPrimaryClockText > 0)
//					loadingScene.Instance.barnPrimaryClockText-=differenceInTimesInSec;
//				
//				if(loadingScene.Instance.barnSecondaryText > 0)
//					loadingScene.Instance.barnSecondaryText-=differenceInTimesInSec;
//
//				if(loadingScene.Instance.goldMinePrimaryClockText > 0)
//					loadingScene.Instance.goldMinePrimaryClockText-=differenceInTimesInSec;
//				
//				if(loadingScene.Instance.goldMineSecondaryText > 0)
//					loadingScene.Instance.goldMineSecondaryText-=differenceInTimesInSec;
//			}
//		} else if(isPause){
//			GetCurrentServerTime();
//			Debug.Log("  going into pause  ==   "+currentServerTime);
//		}
//	}


	//{"success":0,"error_msg":"Invalid details!"}
	void OnApplicationPause(bool isPause)
	{
		Debug.Log ("isPause = "+isPause);
		if (!isPause && firstTimeDone) {
			previousServerTime = currentServerTime;
			NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
				if (isConnected)
					StartCoroutine (ServerTime(true));
				else
					gameStopPopup.ShowPopup ("Network Error!");
			});


		} else if(isPause){
			GetCurrentServerTime();
			Debug.Log("  going into pause  ==   "+currentServerTime);
		}
	}






	IEnumerator ServerTime(bool fromApplicationPause)
	{
		if (fromApplicationPause)
			timeLoader.SetActive (true);
//		WWW www = new WWW ("http://ommzi.com/new_app/index.php?tag=currentDateTime");
		WWW www = new WWW ("http://ec2-54-174-178-121.compute-1.amazonaws.com/mod_web_active_api/index.php?tag=currentDateTime");
		yield return www;
		if (www.error == null && www.text.Contains("dateTime")) {
			//{"tag":"currentDateTime","success":1,"error":0,"dateTime":"2016-07-01 01:19:22"}
			Debug.Log ("form server = " + www.text);
			IDictionary timeDict = (IDictionary)Json.Deserialize (www.text);
//			Debug.Log (timeDict ["dateTime"].ToString ());
			serverTimeOnStart = (Convert.ToDateTime (timeDict ["dateTime"].ToString ()));
			Debug.Log ("foundTime = " + serverTimeOnStart);
			systemTimeOnStart = DateTime.Now;
			differenceInTheServerAndSystemTime = serverTimeOnStart - systemTimeOnStart;
			currentServerTime = systemTimeOnStart.Add (differenceInTheServerAndSystemTime);
			foundTime = true;
			if(fromApplicationPause)
			{
				TimeSpan differnceInTheTwoTimesSinceGamePaused = currentServerTime - previousServerTime;
				float differenceInTimesInSec = (float)differnceInTheTwoTimesSinceGamePaused.TotalSeconds;
				Debug.Log("  return from pause  ==   "+currentServerTime +" , diff = "+differenceInTimesInSec);
				Debug.Log(" differnceInTheTwoTimesSinceGamePaused = "+differnceInTheTwoTimesSinceGamePaused);
				if(differenceInTimesInSec < 0)
					differenceInTimesInSec = Mathf.Abs (differenceInTimesInSec);
				if (loadingScene.Instance != null) {
					if(loadingScene.Instance.castlePrimaryClockText > 0)
						loadingScene.Instance.castlePrimaryClockText-=differenceInTimesInSec;
					
					if(loadingScene.Instance.storagePrimaryClockText > 0)
						loadingScene.Instance.storagePrimaryClockText-=differenceInTimesInSec;
					
					if(loadingScene.Instance.storageSecondaryText > 0)
						loadingScene.Instance.storageSecondaryText-=differenceInTimesInSec;
					
					if(loadingScene.Instance.barnPrimaryClockText > 0)
						loadingScene.Instance.barnPrimaryClockText-=differenceInTimesInSec;
					
					if(loadingScene.Instance.barnSecondaryText > 0)
						loadingScene.Instance.barnSecondaryText-=differenceInTimesInSec;
					
					if(loadingScene.Instance.goldMinePrimaryClockText > 0)
						loadingScene.Instance.goldMinePrimaryClockText-=differenceInTimesInSec;
					
					if(loadingScene.Instance.goldMineSecondaryText > 0)
						loadingScene.Instance.goldMineSecondaryText-=differenceInTimesInSec;
					}
			}
		} else {
			foundTime = false;
			gameStopPopup.ShowPopup ("Network Error!");
			Debug.Log ("issue in fetching time"+www.error);
		}
		timeLoader.SetActive (false);
		firstTimeDone = true;

	}

	public DateTime GetCurrentServerTime()
	{
		currentServerTime = DateTime.Now.Add (differenceInTheServerAndSystemTime);
		return currentServerTime;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
