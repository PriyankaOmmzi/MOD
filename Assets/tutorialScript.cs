using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class tutorialScript : MonoBehaviour 
{
	public static tutorialScript instance;
	public List<string> MenuElementsText;
	public Text defaultTextMenu;
	public GameObject defaultMenuObject;
	public int menuClickCounter;
	public GameObject panel; 
	public Transform defaultPos;
	public Transform defaultPosDown;
	public GameObject pointerObj, fingerObj;

	public Transform profilePos, closeProfilePos, settingPos, levelPos;


	// Use this for initialization
	public void Awake()
	{
		instance = this;
	}
	void Start () 
	{
		//pointerObj.SetActive (false);
		//fingerObj.SetActive (false);



	}

	public void callMenuTutorial()
	{
		defaultMenuObject.SetActive (true);
		deactivateMenuElements (0);
		panel.transform.position = defaultPos.transform.position;;

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void clickMenuButton(Button buttonClicked)
	{
		menuClickCounter ++ ;
		if (menuClickCounter == 1) {
			//fingerObj.SetActive (true);
			//fingerObj.transform.position = profilePos.transform.position;
			panel.transform.position = defaultPos.transform.position;
			deactivateMenuElements (1);
		} else if (menuClickCounter == 2) {
			newMenuScene.instance.clickProfile ();
//			fingerObj.SetActive (false);
//			pointerObj.SetActive (true);
//			pointerObj.transform.position = closeProfilePos.transform.position;

		
			panel.transform.position = defaultPosDown.transform.position;

			deactivateMenuElements (2);
		} else if (menuClickCounter == 3) {
			panel.transform.position = defaultPos.transform.position;
//			fingerObj.SetActive (true);
//			pointerObj.SetActive (false);
//			fingerObj.transform.position = settingPos.transform.position;
			newMenuScene.instance.clickProfileExit ();

			deactivateMenuElements (3);

		} else if (menuClickCounter == 4) {
			panel.transform.position = defaultPosDown.transform.position;
			deactivateMenuElements (4);
			//fingerObj.SetActive (false);
			newMenuScene.instance.onClickSetting ();

		} else if (menuClickCounter == 5)
			
		{
			panel.transform.position = defaultPosDown.transform.position;

			deactivateMenuElements (5);

		}
		else if (menuClickCounter == 6) 
		{
			
			panel.transform.position = defaultPosDown.transform.position;

			deactivateMenuElements (6);

		}
		else if (menuClickCounter == 7) 
		{
			panel.transform.position = defaultPosDown.transform.position;
			//fingerObj.SetActive (true);
			//fingerObj.transform.position = closeProfilePos.transform.position;

			deactivateMenuElements (7);


		}
		else if (menuClickCounter == 8) 
		{
			
			newMenuScene.instance.onClickSettingExit();

			deactivateMenuElements (8);
			panel.transform.position = defaultPos.transform.position;
		}
		else if (menuClickCounter == 9) 
		{
			
			defaultMenuObject.SetActive (false);

			deactivateMenuElements (9);

		}




	}
	public void deactivateMenuElements(int index)
	{
		for(int i=0;i<MenuElementsText.Count;i++)
		{
			if(i==index) 
			{

				defaultTextMenu.text = MenuElementsText [i];
			}	

		}
	}
}
