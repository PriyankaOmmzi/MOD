using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Popup : MonoBehaviour {
	public GameObject myPanel;
	public Text popupText;
	public GameObject loader;

	public GameObject signupPanel; 
	public GameObject loginPanel; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ExitPopup()
	{
		myPanel.SetActive (false);
	}

	public void ExitSignupPopup()
	{
		myPanel.SetActive (false);
		loginPanel.SetActive (true);
		signupPanel.SetActive (false);
	}

	public void ShowPopup(string textToShow)
	{
		myPanel.SetActive (true);
		popupText.text = textToShow;
		loader.SetActive (false);
		if(newMenuScene.instance != null)
			newMenuScene.instance.loader.SetActive (false);
	}
}
