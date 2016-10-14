using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class removeFromBlacklist: MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void RemoveFromblackList()
	{

		newMenuScene.instance.loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected) {
				WWWForm form_time = new WWWForm ();
				string URltime = loadingScene.Instance.baseUrl;
				form_time.AddField ("tag", "doGetFriendsBlackList");
				form_time.AddField ("user_id",   PlayerDataParse._instance.playersParam.userId.ToString());
				Debug.Log("frind id = "+this.gameObject.name);
				form_time.AddField ("friend_id", PlayerDataParse._instance.ID(this.gameObject.name.ToString()));  
				form_time.AddField ("blacklist",  "0");  
				form_time.AddField ("device_id",  SystemInfo.deviceUniqueIdentifier);  
				//				form_time.AddField ("status",  "1");  
				WWW www = new WWW (URltime, form_time.data);
				StartCoroutine (empireScene.instance.userTIMEfetching2 (www, isSuccess => 
					{
						Debug.Log("text = "+(www.text));
						if(www.text.Contains("error_msg"))
						{
							newMenuScene.instance.popupFromServer.ShowPopup ("Cannot Remove blacklisted friend!");
						}
						else
						{
							newMenuScene.instance.popupFromServer.ShowPopup ("Friend removed from blacklist!");
							Debug.Log("id name"+this.gameObject.name);
							//community.instance.blackListPlayer.RemoveAt(this.gameObject);
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
}
