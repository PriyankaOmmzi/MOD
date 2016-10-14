using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;  

public class QuestDataFetchHotspotCount : MonoBehaviour {

	public List <int> hotspotClearance = new List<int>(){15,5,10,2,6,7,7,3,3,3,20};

	// Use this for initialization
	void Start () {
		Load ("Assets/Resources/Chapter5.txt");
	}
	
	// Update is called once per frame
	void Update () {
	

	}



	void GenerateData(string clearancePointFormal)
	{
		WriteToFile ("Assets/Resources/ChapterHotspotCount5.txt" ,clearancePointFormal);
	}


	public bool Load(string fileName)
	{
		string lineToUpdate = "";
		// Handle any problems that might arise when reading the text
		try
		{
			string line;
			// Create a new StreamReader, tell it which file to read and what encoding the file
			// was saved as
			StreamReader theReader = new StreamReader(fileName, Encoding.Default);
			// Immediately clean up the reader after this block of code is done.
			// You generally use the "using" statement for potentially memory-intensive objects
			// instead of relying on garbage collection.
			// (Do not confuse this with the using directive for namespace at the 
			// beginning of a class!)
			using (theReader)
			{
				// While there's lines left in the text file, do this:
				do
				{
					line = theReader.ReadLine();
					
					if (line != null)
					{
						// Do whatever you need to do with the text line, it's a string now
						// In this example, I split it into arguments based on comma
						// deliniators, then send that array to DoStuff()
						string[] entries = line.Split(',');
						if (entries.Length > 0)
						{
							lineToUpdate+=entries.Length+"\n";
//							for(int i = 0; i < entries.Length ; i++)
//							{
//								Debug.Log(entries[i]);
//							}
//							DoStuff(entries);
						}
					}
				}
				while (line != null);
				GenerateData (lineToUpdate);
				// Done reading, close the reader and return true to broadcast success    
				theReader.Close();
				return true;
			}
		}
		// If anything broke in the try block, we throw an exception with information
		// on what didn't work
		catch (Exception e)
		{
			Console.WriteLine("{0}\n", e.Message);
			return false;
		}
	}

	public void WriteToFile(string fileName ,  string dataToWrite)
	{
		StreamWriter theWriter = new StreamWriter(fileName , true);
		theWriter.Write(dataToWrite);
		Debug.Log ("written!!");
		theWriter.Close();
	}


}
