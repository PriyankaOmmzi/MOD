﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class acceptRequest : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{

	}

	// Update is called once per frame
	void Update () 
	{

	}

	public void AcceptFrinedRequest( )
	{
		newMenuScene.instance.loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected) {
				WWWForm form_time = new WWWForm ();
				string URltime = loadingScene.Instance.baseUrl;
				form_time.AddField ("tag", "doGetSendFriendsRequest");
				form_time.AddField ("friend_id",   PlayerDataParse._instance.ID(this.gameObject.name.ToString()));  
				form_time.AddField ("user_id", PlayerDataParse._instance.playersParam.userId.ToString().Split (',') [0]);
				form_time.AddField ("friend_type",  "friend");  
				form_time.AddField ("status",  "1");  
				WWW www = new WWW (URltime, form_time.data);
				StartCoroutine (empireScene.instance.userTIMEfetching2 (www, isSuccess => 
					{
						PlayerParameters._instance.myPlayerParameter.FriendList++;
						community.instance.totalFriends.text=PlayerParameters._instance.myPlayerParameter.FriendList+"/"+community.instance.GetMaxFriends();
						//						community.instance.opnedRequestDialog();
						//this.gameObject.transform.GetChild(3).GetComponent<Button>().interactable=false;
						newMenuScene.instance.popupFromServer.ShowPopup ("Friend Request Accepted");
						Destroy(this.gameObject);
						print("===== FRIEND REQUEST ACCEPTED =====");
					}));
			} 

			else 
			{
				empireScene.instance.popupFromServer.ShowPopup ("Network Error!");
			}

		});



	}

}