using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class formaion : MonoBehaviour 
{
//	public string levelName;
	//private AsyncOperation async;

	// Use this for initialization
	void Start () 
	{
	
	}
	public void StartLoading() 
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	public void battle()
	{
		StartCoroutine(loadAsync());
		//Application.LoadLevel("BattleSceneTest");
	}
	public void backButton()
	{
		Application.LoadLevel("Battle_Layout4");
	}
	private IEnumerator loadAsync()
	{
		AsyncOperation operation = Application.LoadLevelAsync("test");
		while(!operation.isDone) 
		{
			yield return operation.isDone;
			print("-------------------"+operation.progress);
			int percontlOAD = (int)((operation.progress+0.1f)*100);
			string presconString=percontlOAD.ToString()+" %";
			GameObject.Find("progress").GetComponent<Text>().text="LOADING "+presconString;
		}
	}

}
