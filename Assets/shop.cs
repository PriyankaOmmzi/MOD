using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class shop : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void backButton()
	{
		if(PlayerPrefs.GetString("layout3")=="yes")
		{
			Application.LoadLevel("battle_Layout3");
			PlayerPrefs.SetString("layout3","no");

		}

		if(PlayerPrefs.GetString("battleLyout")=="yes")
		{
			Application.LoadLevel("Battle_Layout");
			PlayerPrefs.SetString("battleLyout","no");

		}
		if(PlayerPrefs.GetString("Battle_Layout4")=="yes")
		{
			Application.LoadLevel("Battle_Layout4");
			PlayerPrefs.SetString("Battle_Layout4","no");

		}
		if(PlayerPrefs.GetString("newMain")=="yes")
		{
			Application.LoadLevel("menuNew");
			PlayerPrefs.SetString("newMain","no");

		}
		if(PlayerPrefs.GetString("cardCollection")=="yes") 
		{
			Application.LoadLevel("cardCollections");
			PlayerPrefs.SetString("cardCollection","no");
			
		}
		if(PlayerPrefs.GetString("lost")=="yes") 
		{
			Application.LoadLevel("lost");
			PlayerPrefs.SetString("lost","no");
			
		}
		if(PlayerPrefs.GetString("win")=="yes") 
		{
			Application.LoadLevel("win");
			PlayerPrefs.SetString("win","no");
			
		}
		if(PlayerPrefs.GetString("detail")=="yes") 
		{
			Application.LoadLevel("detail");
			PlayerPrefs.SetString("detail","no");
			
		}

	}
}
