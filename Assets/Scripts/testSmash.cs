using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class testSmash : MonoBehaviour 
{
	public GameObject smash;
	GameObject smashPrefab;
	float minVlue=0f;
	float y=-5f;
	float z=-5f;
	public List<GameObject>smashAdded;


	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	public void destroyAdded()
	{
		z=z+0.1f;

//		for(int i=0;i<smashAdded.Count;i++)
//		{
//			smashAdded.Remove(smashAdded.Count-1);
////			
//			Destroy(smashAdded.Count-1);
////
//		}


//		GameObject.Find(y.ToString());
//		print("==========="+z.ToString ());
		Destroy(GameObject.Find(z.ToString()));


//		smashAdded.Remove(smashPrefab.gameObject);
//		Destroy(smashPrefab.gameObject);

	}

	public void click()
	{
		y=y+0.1f;
		Vector3 position = new Vector3(Random.Range(-3f,3f),y,0);

		smashPrefab = (GameObject) Instantiate (smash ,position,Quaternion.identity);
		smashPrefab.name = y.ToString();
		smashAdded.Add(smashPrefab.gameObject);

		//Instantiate(smash.gameObject,new Vector3((Random.Range(0,10),0,0),Quaternion.identity);
	}
}
