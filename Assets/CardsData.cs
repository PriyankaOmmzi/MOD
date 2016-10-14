using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;  
using MiniJSON;

public class CardsData : MonoBehaviour {

	public static CardsData _instance;

	void Start()
	{
		_instance = this;
	}

	// Use this for initialization
	IEnumerator FetchCardsDataFromServer () {
		WWW www = new WWW ("http://ec2-54-174-178-121.compute-1.amazonaws.com/mod_web_active_api/index.php?tag=getRequiredCardData&user_id=xZhDES4Nphk=&device_id=8A93C9B6-D751-5F62-8E65-7347B5D2D5CA");
		yield return www;

		IDictionary jsonDic = (IDictionary)Json.Deserialize (www.text);
		IList jsonList = (IList) jsonDic["card_data"];
		for (int i = 0; i < jsonList.Count; i++) {
			IDictionary cardDic = (IDictionary)jsonList[i];
			string dataInLine = "";
			dataInLine+=cardDic["Card_id"].ToString ()+",";
			dataInLine+=cardDic["Card_name"].ToString ()+",";
			dataInLine+=cardDic["Card_rarity"].ToString ()+"\n";
			if(cardDic["Card_rarity"].ToString () == "Common")
				WriteToFile ("Assets/Resources/CardRarity1.txt" ,dataInLine);
			else if(cardDic["Card_rarity"].ToString () == "Uncommon")
				WriteToFile ("Assets/Resources/CardRarity2.txt" ,dataInLine);
			else if(cardDic["Card_rarity"].ToString () == "Super")
				WriteToFile ("Assets/Resources/CardRarity3.txt" ,dataInLine);
			else if(cardDic["Card_rarity"].ToString () == "Mega")
				WriteToFile ("Assets/Resources/CardRarity4.txt" ,dataInLine);
			else if(cardDic["Card_rarity"].ToString () == "Legendary")
				WriteToFile ("Assets/Resources/CardRarity5.txt" ,dataInLine);
		}

	}


	public List<string> LoadCardsData( string fileName , int noOfCards , ref List<string> allEnemyCards)
	{
//		List<string> cardsName = new List<string> ();
		TextAsset cardsFile = Resources.Load(fileName) as TextAsset; 
//		Debug.Log ("fileName = " + fileName);
//		Debug.Log ("cardsFile = " + noOfCards);
		string[] linesFromfile = cardsFile.text.Split("\n"[0]);
		for (int i = 0; i < noOfCards; i++) {
			int lineNo = UnityEngine.Random.Range (0,linesFromfile.Length);
			string[] entries = linesFromfile[lineNo].Split(',');
//			Debug.Log(entries[1]);
			allEnemyCards.Add (entries[1]);
		}
//		Debug.Log ("Enemy Count = "+allEnemyCards.Count);
		return allEnemyCards;
	}

	public void WriteToFile(string fileName ,  string dataToWrite)
	{
		StreamWriter theWriter = new StreamWriter(fileName , true);
		theWriter.Write(dataToWrite);
		Debug.Log ("written!!");
		theWriter.Close();
	}
}
