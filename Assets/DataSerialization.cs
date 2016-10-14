using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataSerialization : MonoBehaviour {

	public static DataSerialization _instance;
	public int userId;
	public string userName;
	public int isFbLoggedIn;
	public string referralCode;

	// Use this for initialization
	void Start () {
		_instance = this;
		Debug.Log (Application.persistentDataPath);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SaveData()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");
		PlayerData playerData = new PlayerData ();
		playerData.userId = userId;
		playerData.userName = userName;
		playerData.isFbLoggedIn = isFbLoggedIn;
		playerData.referralCode = referralCode;
		Debug.Log ("save data  ===  "+playerData.userId + " , "+playerData.userName);
		bf.Serialize (file,playerData);
		file.Close ();
	}

	public void LoadData()
	{
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat" , FileMode.Open);
			PlayerData playerData = (PlayerData) bf.Deserialize (file);
			userId = playerData.userId;
			userName = playerData.userName;
			isFbLoggedIn = playerData.isFbLoggedIn;
			referralCode = playerData.referralCode;
			Debug.Log ("load data  ===  "+playerData.userId + " , "+playerData.userName);
			file.Close ();
		}
	}

	void OnGUI()
	{
		if(GUI.Button (new Rect(10,0,100,100), "SaveData"))
		{
			SaveData();
		}
		if(GUI.Button (new Rect(10,200,100,100), "LoadData"))
		{
			LoadData();
		}
		if(GUI.Button (new Rect(10,300,100,100), "Modify"))
		{
			userId++;
		}
	}
}

[Serializable]
class PlayerData
{
	public int userId;
	public string userName;
	public int isFbLoggedIn;
	public string referralCode;

}
