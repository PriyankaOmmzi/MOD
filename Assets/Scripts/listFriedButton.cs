using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class listFriedButton : MonoBehaviour {
	public GameObject drager;
	public Transform endPos;
	public Transform startPos;
	public GameObject button1;
	public GameObject button2;
	public Transform startArrow;
	public Transform endArrow;
	public GameObject arrow;
	bool ismoved=false;
	// Use this for initialization
	void Start () {
//		button2.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
//		if (this.gameObject != null) {
//			if (scroll.value == 1) {
//				secondButton.SetActive (true);
//				//drager.SetActive (false);
//			} else {
//				secondButton.SetActive (false);
//				//drager.SetActive (true);
//
//			}
//		}
	
	}
	public void blacklistClick()
	{
		if (ismoved == false) {
			iTween.MoveTo (drager, iTween.Hash ("x", endPos.transform.position.x, "time", 0.3f, "onComplete", "button1Interact", "onCompleteTarget", this.gameObject));
			iTween.MoveTo (arrow, iTween.Hash ("x", endArrow.transform.position.x, "time", 0.3f, "onComplete", "button1Interact", "onCompleteTarget", this.gameObject));

		} 
		else 
		{
			iTween.MoveTo (drager, iTween.Hash ("x", startPos.transform.position.x, "time", 0.3f, "onComplete", "button1Interact2", "onCompleteTarget", this.gameObject));
			iTween.MoveTo (arrow, iTween.Hash ("x", startArrow.transform.position.x, "time", 0.3f, "onComplete", "button1Interact2", "onCompleteTarget", this.gameObject));


		}
	}
//	public void blacklistClick2()
//	{
//		iTween.MoveTo (drager, iTween.Hash ("x", startPos.transform.position.x, "time", 0.3f,"onComplete","button1Interact2","onCompleteTarget",this.gameObject));
//
//	}
	void button1Interact()
	{
		button1.transform.localScale = new Vector2 (-1, 1.749994f);
		ismoved = true;
		//button2.SetActive (true);
	}
	void button1Interact2()
	{
		button1.transform.localScale = new Vector2 (1, 1.749994f);
		ismoved = false;

	}
	public void blackList()
	{
		
		newMenuScene.instance.loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected) {
				WWWForm form_time = new WWWForm ();
				string URltime = loadingScene.Instance.baseUrl;
				form_time.AddField ("tag", "doGetFriendsBlackList");
				form_time.AddField ("user_id",   PlayerDataParse._instance.playersParam.userId.ToString());
				form_time.AddField ("friend_id", PlayerDataParse._instance.ID(this.gameObject.name.ToString()));  
				form_time.AddField ("device_id",  SystemInfo.deviceUniqueIdentifier);  
				form_time.AddField ("blacklist",  "1");  
//				form_time.AddField ("status",  "0");  
				WWW www = new WWW (URltime, form_time.data);
				StartCoroutine (empireScene.instance.userTIMEfetching2 (www, isSuccess => 
					{
						if(www.text.Contains("error_msg"))
						{
							newMenuScene.instance.popupFromServer.ShowPopup ("Error Blacklisting friend !");
						}
						else
						{
							Debug.Log("text = "+(www.text));
							//						community.instance.opnedRequestDialog();
							//this.gameObject.transform.GetChild(3).GetComponent<Button>().interactable=false;
							newMenuScene.instance.popupFromServer.ShowPopup ("Friend blacklisted !");
							for (int j = community.instance.listPlayer.Count-1; j >= 0; j--) {
								if (community.instance.listPlayer [j] != null)
								{
									Destroy (community.instance.listPlayer [j]);
									community.instance.listPlayer.RemoveAt (j);

								}
							}
							Debug.Log("id name"+this.gameObject.name);
							Destroy(this.gameObject);
						}

					}));
			} 

			else 
			{
				empireScene.instance.popupFromServer.ShowPopup ("Network Error!");
			}

		});



	

	}
	public void deleteFriend()
	{

		newMenuScene.instance.loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected) {
				WWWForm form_time = new WWWForm ();
				string URltime = loadingScene.Instance.baseUrl;
				form_time.AddField ("tag", "doDeleteFriendsList");
				form_time.AddField ("user_id",   PlayerDataParse._instance.playersParam.userId.ToString());
				form_time.AddField ("friend_id", PlayerDataParse._instance.ID(this.gameObject.name.ToString()));  
				form_time.AddField ("device_id",  SystemInfo.deviceUniqueIdentifier);  
				form_time.AddField ("friend_status",  "0");  
				//				form_time.AddField ("status",  "0");  
				WWW www = new WWW (URltime, form_time.data);
				StartCoroutine (empireScene.instance.userTIMEfetching2 (www, isSuccess => 
					{
						if(www.text.Contains("error_msg"))
						{
							newMenuScene.instance.popupFromServer.ShowPopup ("Error Deleting friend !");
						}
						else
						{
							PlayerParameters._instance.myPlayerParameter.FriendList--;
							community.instance.totalFriends.text=PlayerParameters._instance.myPlayerParameter.FriendList+"/"+community.instance.GetMaxFriends();
							Debug.Log("text = "+(www.text));
							for (int j = community.instance.listPlayer.Count-1; j >= 0; j--) {
								if (community.instance.listPlayer [j] != null)
								{
									Destroy (community.instance.listPlayer [j]);
									community.instance.listPlayer.RemoveAt (j);
									//				Destroy (searchedPlayerIdList [i]);

								}
							}
							//						community.instance.opnedRequestDialog();
							//this.gameObject.transform.GetChild(3).GetComponent<Button>().interactable=false;
							newMenuScene.instance.popupFromServer.ShowPopup ("Friend deleted !");
							Debug.Log("id name"+this.gameObject.name);
							Destroy(this.gameObject);
						}

					}));
			} 

			else 
			{
				empireScene.instance.popupFromServer.ShowPopup ("Network Error!");
			}

		});





	}
	public void sendGift()
	{

		newMenuScene.instance.loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected) {
				WWWForm form_time = new WWWForm ();
				string URltime = loadingScene.Instance.baseUrl;
				form_time.AddField ("tag", "doAddUserGift");
				form_time.AddField ("user_id",   PlayerDataParse._instance.playersParam.userId.ToString());
				form_time.AddField ("friend_id", PlayerDataParse._instance.ID(this.gameObject.name.ToString()));  
				form_time.AddField ("device_id",  SystemInfo.deviceUniqueIdentifier);  
//				form_time.AddField ("friend_status",  "0");  
				//				form_time.AddField ("status",  "0");  
				WWW www = new WWW (URltime, form_time.data);
				StartCoroutine (empireScene.instance.userTIMEfetching2 (www, isSuccess => 
					{
						if(www.text.Contains("error_msg"))
						{
							newMenuScene.instance.popupFromServer.ShowPopup ("Error sending gift !");
						}
						else
						{
							Debug.Log("text = "+(www.text));
							//						community.instance.opnedRequestDialog();
							this.gameObject.transform.GetChild(3).GetComponent<Button>().interactable=false;
							newMenuScene.instance.popupFromServer.ShowPopup ("Gift sent !");
							Debug.Log("id name"+this.gameObject.name);
							//Destroy(this.gameObject);
						}

					}));
			} 

			else 
			{
				empireScene.instance.popupFromServer.ShowPopup ("Network Error!");
			}

		});





	}
}
