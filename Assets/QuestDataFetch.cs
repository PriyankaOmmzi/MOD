using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;  

[System.Serializable]
//public class Hotspot
//{
//	public int typeOfHotspot;
//	public string nameOfHotspot;
//	public int staminaReqd;
//	public int clearancePointsAwarded;
//	public int avatarExpFetched;
//	public string descriptionOfHotspot;
//}

public class QuestDataFetch : MonoBehaviour {

	public List <int> hotspotClearance = new List<int>(){15,5,10,2,6,7,7,3,3,3,20};
	public List <Hotspot> hotspotTypes;
	string clearancePoint = "0";
	public int noOfTimes2ShouldAppear;
	public int noOfTimes3ShouldAppear;
	public int noOfTimes5ShouldAppear;
	public int noOfTimes6ShouldAppear;
	public int noOfTimes7ShouldAppear;
	public int noOfTimes2Appeared;
	public int noOfTimes3Appeared;
	public int noOfTimes5Appeared;
	public int noOfTimes6Appeared;
	public int noOfTimes7Appeared;

	public static QuestDataFetch _instance;
	// Use this for initialization
	void Start () {
		_instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	

	}

//	void OnGUI()
//	{
//		clearancePoint = GUI.TextField (new Rect (0, 150, 100, 100), clearancePoint , 26);
//		if (GUI.Button (new Rect (0, 0, 100, 100) , "Append")) {
//			noOfTimes2Appeared = 0;
//			noOfTimes3Appeared = 0;
//			noOfTimes5Appeared = 0;
//			noOfTimes6Appeared = 0;
//			noOfTimes7Appeared = 0;
//			hotspotClearance.Clear ();
//			hotspotClearance = new List<int>(){15,5,10,2,6,7,7,3,3,3,20};
//			GenerateData(clearancePoint);
//		}
//	}


	void GenerateData(string clearancePointFormal)
	{
		string dataInLine = "";
		int clearanceP = int.Parse (clearancePointFormal);
		int clearancePointOfLine = 0;
		while (clearancePointOfLine < clearanceP) {
			int randomVal = UnityEngine.Random.Range(0,hotspotClearance.Count);
			clearancePointOfLine+=hotspotClearance[randomVal];
			switch(hotspotClearance[randomVal])
			{
			case 2:
				dataInLine+="3,";
				break;
			case 3:
				int []hospotsFor3 = new int[]{7,8,9};
				int hotSpotUsed = UnityEngine.Random.Range (0,hospotsFor3.Length);
				dataInLine+=hospotsFor3[hotSpotUsed]+",";
				break;
			case 5:
				dataInLine+="1,";
				break;
			case 6:
				dataInLine+="4,";
				break;
			case 7:
				int []hospotsFor7 = new int[]{5,6};
				int hotSpotUsed7 = UnityEngine.Random.Range (0,hospotsFor7.Length);
				dataInLine+=hospotsFor7[hotSpotUsed7]+",";
				break;
			case 10:
				dataInLine+="2,";
				break;
			case 15:
				dataInLine+="0,";
				break;
			case 20:
				dataInLine+="10,";
				break;
			}

			if(hotspotClearance[randomVal] == 2 && noOfTimes2Appeared < noOfTimes2ShouldAppear)
			{
				noOfTimes2Appeared++;
				if(noOfTimes2Appeared >= noOfTimes2ShouldAppear )
				{
					hotspotClearance.Remove (2);
				}	
			}
			else if(hotspotClearance[randomVal] == 3 && noOfTimes3Appeared < noOfTimes3ShouldAppear)
			{
				noOfTimes3Appeared++;
				if(noOfTimes3Appeared >= noOfTimes3ShouldAppear)
				{
					hotspotClearance.Remove (3);
					hotspotClearance.Remove (3);
					hotspotClearance.Remove (3);
				}	
			}
			else if(hotspotClearance[randomVal] == 5 && noOfTimes5Appeared < noOfTimes5ShouldAppear)
			{
				noOfTimes5Appeared++;
				if(noOfTimes5Appeared >= noOfTimes5ShouldAppear)
				{
					hotspotClearance.Remove (5);
				}	
			}
			else if(hotspotClearance[randomVal] == 6 && noOfTimes6Appeared < noOfTimes6ShouldAppear)
			{
				noOfTimes6Appeared++;
				if(noOfTimes6Appeared >= noOfTimes6ShouldAppear)
				{
					hotspotClearance.Remove (6);
				}	
			}
			else if(hotspotClearance[randomVal] == 7 && noOfTimes7Appeared < noOfTimes7ShouldAppear)
			{
				noOfTimes7Appeared++;
				if(noOfTimes7Appeared >= noOfTimes7ShouldAppear)
				{
					hotspotClearance.Remove (7);
					hotspotClearance.Remove (7);
				}	
			}


		}
		dataInLine = dataInLine.TrimEnd(dataInLine[dataInLine.Length - 1]);
		dataInLine += "\n";
		WriteToFile ("Assets/Resources/Chapter5.txt" ,dataInLine);
	}

	
	public bool Load(string fileName , int chapterNo ,  List<int> lineNoForAreas  , QuestManager myQuestManager)
	{

		TextAsset chapterFile = Resources.Load(fileName) as TextAsset; 
		
		int lineEntered = 0;
		string[] linesFromfile = chapterFile.text.Split("\n"[0]);
		for(int j = 0 ; j < linesFromfile.Length ; j++)
		{
			if(j == lineNoForAreas[lineEntered])
			{
				string[] entries = linesFromfile[j].Split(',');
//				Debug.Log("Length of entries"+entries.Length);
				if (entries.Length > 0)
				{
					for(int i = 0; i < entries.Length ; i++)
					{
//						Debug.Log(entries[i]);
						myQuestManager.chapters[chapterNo].area[lineEntered].hotSpotNoUsed.Add (int.Parse (entries[i]));
						myQuestManager.chapters[chapterNo].area[lineEntered].isHotSpotCleared.Add (false);
					}
				}
				lineEntered++;
				if(lineEntered >= lineNoForAreas.Count)
					break;
			}

		}
		return true;

	}

	public bool LoadHotspotsPerArea(string fileName , int chapterNo , int areaNo,  int lineNoForArea , QuestManager myQuestManager)
	{
		TextAsset chapterFile = Resources.Load(fileName) as TextAsset; 
		
		int lineEntered = 0;
		string[] linesFromfile = chapterFile.text.Split("\n"[0]);
//		for(int j = 0 ; j < linesFromfile.Length ; j++)
//		{
//			if(j == lineNoForArea)
//			{
		string[] entries = linesFromfile[lineNoForArea].Split(',');
		Debug.Log("Length of entries"+entries.Length);
		if (entries.Length > 0)
		{
			for(int i = 0; i < entries.Length ; i++)
			{
				myQuestManager.chapters[chapterNo].area[areaNo].hotSpotNoUsed.Add (int.Parse (entries[i]));
				myQuestManager.chapters[chapterNo].area[areaNo].isHotSpotCleared.Add (false);
			}
		}
//				break;
//			}
//		}
		return true;
	}


	public bool Load(string fileName)
	{

		TextAsset chapterFile = Resources.Load(fileName) as TextAsset; 
		
		int lineEntered = 0;
		string[] linesFromfile = chapterFile.text.Split("\n"[0]);
		for(int j = 0 ; j < linesFromfile.Length ; j++)
		{
			string[] entries = linesFromfile[j].Split(',');
			if (entries.Length > 0)
			{
				for(int i = 0; i < entries.Length ; i++)
				{
					Debug.Log(entries[i]);
				}
			}
		}

		return true;

	}

	public void WriteToFile(string fileName ,  string dataToWrite)
	{
		StreamWriter theWriter = new StreamWriter(fileName , true);
		theWriter.Write(dataToWrite);
		Debug.Log ("written!!");
		theWriter.Close();
	}


}
