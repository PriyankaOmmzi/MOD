using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class splash : MonoBehaviour {
	
	void Start () 
	{
		
		Invoke("playVideo",0.3f);
		//PlayerPrefs.SetString("tap","yes");

	}
	void playVideo()
	{
		Invoke ("loadNew", 0.3f);
		Handheld.PlayFullScreenMovie ("loading.mp4", Color.black, FullScreenMovieControlMode.Hidden);
	}
	
	
	public void loadNew()
	{
		loadingScene.Instance.main();
		PlayerPrefs.SetString("tap","yes");
		PlayerPrefs.SetString("logout","yes");

		//Instantiate(Resources.Load("main")as GameObject);

		//Application.LoadLevel ("menuNew");
	}

	// Update is called once per frame
	void Update () {
		
	}
}
