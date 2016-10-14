using UnityEngine;
using System.Collections;

[AddComponentMenu("Game/Load Level on Click")]
public class mainScene : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	public void load()
	{
		Application.LoadLevel("Battle_Layout");
	}
}
