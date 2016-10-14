using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class buttonAdd : MonoBehaviour 
{
	

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	
	}
	public void updateBuildingButtonOngoingClick(Button objClick)
	{
		empireScene.instance.updateBuildingButton (objClick);
	}

}
