using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MembershipRewardManager : MonoBehaviour {
	public static MembershipRewardManager _instance;

	public int daysLeft, FetchRewardDay;
	void Awake()

	{
		_instance = this;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
			if (PlayerParameters._instance.myPlayerParameter.membership_no==7 && FetchRewardDay!=daysLeft ) {
				if(daysLeft%2!=0)
				{
				if (Random.Range (1, 3) == 2) {
					//receive attack potion
					PlayerParameters._instance.myPlayerParameter.attack_potion += 1;
				} else {
					//receive Stamina potion
					PlayerParameters._instance.myPlayerParameter.attack_potion += 1;
				}
				}
				else{
				PlayerParameters._instance.myPlayerParameter.signal_fire += 1;
				}
				Dictionary<string, string> avatarParameters = new Dictionary<string, string> ();
				avatarParameters.Add ("attack_potion", PlayerParameters._instance.myPlayerParameter.attack_potion.ToString ());
				avatarParameters.Add ("signal_fire" ,PlayerParameters._instance.myPlayerParameter.signal_fire.ToString ());
				avatarParameters.Add ("day_of_membership_reward_fetched" ,daysLeft.ToString ());
				StartCoroutine (PlayerParameters._instance.SendPlayerParameters (avatarParameters,null ));
				FetchRewardDay = daysLeft;
			} else if (PlayerParameters._instance.myPlayerParameter.membership_no==14 && FetchRewardDay!=daysLeft) {
				if(daysLeft%2!=0)
				{
					if (Random.Range (1, 3) == 2) {
						//receive attack potion
						if(daysLeft!=14)
						PlayerParameters._instance.myPlayerParameter.attack_potion += 1;
						else
							PlayerParameters._instance.myPlayerParameter.attack_potion += 2;	
					} else {
						//receive Stamina potion
						if(daysLeft!=14)
						PlayerParameters._instance.myPlayerParameter.attack_potion += 1;
						else
							PlayerParameters._instance.myPlayerParameter.attack_potion += 2;
					}
				}
				else{
					PlayerParameters._instance.myPlayerParameter.signal_fire += 1;
				}
				Dictionary<string, string> avatarParameters = new Dictionary<string, string> ();
				avatarParameters.Add ("attack_potion", PlayerParameters._instance.myPlayerParameter.attack_potion.ToString ());
				avatarParameters.Add ("signal_fire" ,PlayerParameters._instance.myPlayerParameter.signal_fire.ToString ());
				avatarParameters.Add ("day_of_membership_reward_fetched" ,daysLeft.ToString ());
				StartCoroutine (PlayerParameters._instance.SendPlayerParameters (avatarParameters,null ));
				FetchRewardDay = daysLeft;
			} else if(PlayerParameters._instance.myPlayerParameter.membership_no==30 && FetchRewardDay!=daysLeft){
				if(daysLeft%2!=0)
				{
					if (Random.Range (1, 3) == 2) {
						//receive attack potion
						if(daysLeft!=30)
							PlayerParameters._instance.myPlayerParameter.attack_potion += 1;
						else
							PlayerParameters._instance.myPlayerParameter.attack_potion += 5;	
					} else {
						//receive Stamina potion
						if(daysLeft!=30)
							PlayerParameters._instance.myPlayerParameter.attack_potion += 1;
						else
							PlayerParameters._instance.myPlayerParameter.attack_potion += 5;
					}
				}
				else {
					PlayerParameters._instance.myPlayerParameter.signal_fire += 1;
				}
				Dictionary<string, string> avatarParameters = new Dictionary<string, string> ();
				avatarParameters.Add ("attack_potion", PlayerParameters._instance.myPlayerParameter.attack_potion.ToString ());
				avatarParameters.Add ("signal_fire" ,PlayerParameters._instance.myPlayerParameter.signal_fire.ToString ());
				avatarParameters.Add ("day_of_membership_reward_fetched" ,daysLeft.ToString ());
				StartCoroutine (PlayerParameters._instance.SendPlayerParameters (avatarParameters,null ));
				FetchRewardDay = daysLeft;
			}
			else{
			}

	}

}
