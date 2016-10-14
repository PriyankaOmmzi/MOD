using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MiniJSON;
using System.Collections.Generic;

public class collectStash : MonoBehaviour {
	public static collectStash instance;
	public int _myCount;
	// Use this for initialization
	public void awake()
	{
		instance = this;
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void collectStashItem()
	{
		newMenuScene.instance.loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			
			if (isConnected) {
				WWWForm form_time = new WWWForm ();
				string URltime = loadingScene.Instance.baseUrl;
				form_time.AddField ("tag", "doDeleteUserStash");
				form_time.AddField ("user_id",   PlayerDataParse._instance.playersParam.userId.ToString()); 
				form_time.AddField ("device_id", SystemInfo.deviceUniqueIdentifier);
				form_time.AddField ("stash", this.gameObject.name);
				WWW www = new WWW (URltime, form_time.data);

				StartCoroutine (empireScene.instance.userTIMEfetching2 (www, isSuccess => 
					{
						
						Debug.Log("===== name ==== "+this.transform.GetChild(2).GetComponent<Text>().text);
						Dictionary<string, string> avatarParameters = new Dictionary<string, string> ();
						if(this.gameObject.transform.GetChild(1).GetComponent<Text>().text == "Attack Potion")
						{
							PlayerParameters._instance.myPlayerParameter.attack_potion+=_myCount;
							avatarParameters.Add ("attack_potion",PlayerParameters._instance.myPlayerParameter.attack_potion.ToString());
						}
						else if(this.gameObject.transform.GetChild(1).GetComponent<Text>().text == "Stamina Potion")
						{
							PlayerParameters._instance.myPlayerParameter.stamina_potion+= _myCount;
							avatarParameters.Add ("stamina_potion",PlayerParameters._instance.myPlayerParameter.stamina_potion.ToString());
						}
						else if(this.gameObject.transform.GetChild(1).GetComponent<Text>().text == "Dragon Coins")
						{
							PlayerParameters._instance.myPlayerParameter.dragon_coins+= _myCount;
							avatarParameters.Add ("dragon_coins",PlayerParameters._instance.myPlayerParameter.dragon_coins.ToString());
						}
						else if(this.gameObject.transform.GetChild(1).GetComponent<Text>().text == "Peace Treaties")
						{
							PlayerParameters._instance.myPlayerParameter.no_of_peace_treaties+= _myCount;
							avatarParameters.Add ("no_of_peace_treaties",PlayerParameters._instance.myPlayerParameter.no_of_peace_treaties.ToString());
						}
						else if(this.gameObject.transform.GetChild(1).GetComponent<Text>().text == "Signal Fire")
						{
							PlayerParameters._instance.myPlayerParameter.signal_fire+= _myCount;
							avatarParameters.Add ("no_of_peace_treaties",PlayerParameters._instance.myPlayerParameter.signal_fire.ToString());
						}

						StartCoroutine (PlayerParameters._instance.SendPlayerParameters(avatarParameters, callback => 
							{
								if(callback)
								{
									inventory.instance.itemList.Remove(this.gameObject);
									newMenuScene.instance.popupFromServer.ShowPopup ("Collected Successfully !");
									Destroy(this.gameObject);
								}
								else
								{
									if(this.gameObject.transform.GetChild(1).GetComponent<Text>().text == "Attack Potion")
									{
										PlayerParameters._instance.myPlayerParameter.attack_potion-=_myCount;
									}
									else if(this.gameObject.transform.GetChild(1).GetComponent<Text>().text == "Stamina Potion")
									{
										PlayerParameters._instance.myPlayerParameter.stamina_potion-= _myCount;
									}
									else if(this.gameObject.transform.GetChild(1).GetComponent<Text>().text == "Dragon Coins")
									{
										PlayerParameters._instance.myPlayerParameter.dragon_coins-= _myCount;
									}
									else if(this.gameObject.transform.GetChild(1).GetComponent<Text>().text == "Peace Treaties")
									{
										PlayerParameters._instance.myPlayerParameter.no_of_peace_treaties-= _myCount;
									}
									else if(this.gameObject.transform.GetChild(1).GetComponent<Text>().text == "Signal Fire")
									{
										PlayerParameters._instance.myPlayerParameter.signal_fire-= _myCount;
									}
									this.transform.GetChild(2).GetComponent<Text>().text= "Total x"+_myCount.ToString();
									newMenuScene.instance.popupFromServer.ShowPopup ("Could proceed at this at this time!");
								}
							}));
						print("===== item collected  =====");
					}));
				} 

			else 
			{
				empireScene.instance.popupFromServer.ShowPopup ("Network Error!");
			}

		});



	}
}
