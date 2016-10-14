using UnityEngine;
using System.Collections;

public class CoolDownTime : MonoBehaviour {

	public static CoolDownTime instance;
	public System.TimeSpan coolDownTime;

	void Awake() {
		instance = this;
	}

	public bool IsOver() {
		Debug.Log (PlayerParameters._instance.myPlayerParameter.guildQuitTime.ToString ());
		if (PlayerParameters._instance.myPlayerParameter.guildQuitTime.ToString() != "01/01/0001 00:00:00") {
			coolDownTime = PlayerParameters._instance.myPlayerParameter.guildQuitTime.AddDays (1) - TimeManager._instance.currentServerTime;
			Debug.Log (coolDownTime);
			if (coolDownTime.Hours < 0 && coolDownTime.Minutes < 0 && coolDownTime.Days < 0) {
				return true;
			} else {
				return false;
			}
		} else {
			Debug.Log ("no quit time");
			return true;
		}
	}

}
