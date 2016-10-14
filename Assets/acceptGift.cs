using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class acceptGift : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{

	}

	// Update is called once per frame
	void Update () 
	{

	}

	public void AcceptFrinedGift( )
	{
		newMenuScene.instance.loader.SetActive (true);
		NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
			if (isConnected) {
				WWWForm form_time = new WWWForm ();
				string URltime = loadingScene.Instance.baseUrl;
				form_time.AddField ("tag", "doGetDeleteGift");

				form_time.AddField ("gift_id",this.gameObject.name.ToString());   
				form_time.AddField ("user_id", PlayerDataParse._instance.playersParam.userId.ToString());
				WWW www = new WWW (URltime, form_time.data);
				StartCoroutine (empireScene.instance.userTIMEfetching2 (www, isSuccess => 
					{
						newMenuScene.instance.popupFromServer.ShowPopup ("Gift Request Accepted");
						inventoryDuplicate.instance.giftList.Remove(this.gameObject);
						Destroy(this.gameObject);
						print("===== GIFT REQUEST ACCEPTED =====");
					}));
			} 

			else 
			{
				empireScene.instance.popupFromServer.ShowPopup ("Network Error!");
			}

		});



	}

}
