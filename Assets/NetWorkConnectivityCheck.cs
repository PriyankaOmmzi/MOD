using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;

public class NetWorkConnectivityCheck : MonoBehaviour {

	bool connectionResult , connectionStatusComplete;

	public string _IP = "54.225.98.2";
	public int _port = 80;
	
	public static NetWorkConnectivityCheck _instance;
	public static bool _isConnected;
	// Use this for initialization
	void Awake () {
		_instance = this;
	}

	public IEnumerator TestConnection(Action<bool> action)
	{
		if (Application.internetReachability != NetworkReachability.NotReachable) {
			string urlForNet = "https://docs.google.com/document/d/17FKcr0zmHdl526eafHrFtlyG2YVsOM1UG_tRbR8tieo/edit?usp=sharing";
			WWW www = new WWW (urlForNet);
			yield return www;
			if (www.error == null) {
				if (www.isDone && www.bytesDownloaded > 0) {
					Debug.Log("had netwrok......");
					action (true);
				}
				if (www.isDone && www.bytesDownloaded == 0) {
					Debug.Log("no netwrok......");
					action (false);
				}
			} else {
				Debug.Log("no netwrok......"+www.error);
				Debug.Log("");
				action (false);
			}
		} else {
			Debug.Log("internet check no netwrok......");
			action (false);
		}
	}


	public bool CheckConnectionViaTCPSocket()
	{
		Socket _S = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
		try
		{
			_S.Connect( _IP, _port );
			Debug.Log( "Success connection" );
			return true;
		}
		
		catch ( System.Exception e )
		{
			Debug.Log( e.Message );
			Debug.Log( "Exception connection" );
			return false;
		}
	}

	public void CheckConnectionThread(Action<bool> callBack)
	{
		if (Application.internetReachability != NetworkReachability.NotReachable) {
			System.Threading.Thread _t = new System.Threading.Thread (() => {
				bool result = CheckConnectionViaTCPSocket ();
				connectionResult = result;
				connectionStatusComplete = true;

			});
			_t.Start ();
			StartCoroutine (CheckConnectionCallback (callBack));
		} else {
			callBack(false);
		}
	}

	IEnumerator CheckConnectionCallback(Action<bool> action)
	{
		while (!connectionStatusComplete) {
			yield return null;
		}
		action (connectionResult);
		connectionResult = false;
		connectionStatusComplete = false;
	}
}
