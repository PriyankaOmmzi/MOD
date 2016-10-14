using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Banty.Security;

public class PlayerDataParse : MonoBehaviour 
{
	private string path;
	private string fileInfo;
	private XmlDocument xmlDoc;
	private WWW www;
	private TextAsset textXml;
	public PlayerParam playersParam;
	private string fileName;
	public string serverEncryptionKey;
	public string dataEncryptedKey;
	public static PlayerDataParse _instance;
	public string deviceId;
	// Structure for mainitaing the player information

	[System.Serializable]
	public struct PlayerParam
	{
		public int userIdNo;
		public string userIdSaved;
		public string userId;
		public string userName;
		public int fblogin;
		public string referralCode;
	};

	void Awake()
	{
		deviceId = SystemInfo.deviceUniqueIdentifier;
		fileName = "PlayerFile";
		_instance = this;
		LoadData ();

	}

	public string ID(string IDNo) {

		return MD5Crypt.Encrypt (IDNo, serverEncryptionKey, true);
	}

	void Start ()
	{

//		loadXMLFromAssest();
//		readXml();
	}
	// Following method load xml file from resouces folder under Assets
	private void loadXMLFromAssest()
	{
		xmlDoc = new XmlDocument();
		if(System.IO.File.Exists(getPath()))
		{
			xmlDoc.LoadXml(System.IO.File.ReadAllText(getPath()));
		}
		else
		{
			textXml = (TextAsset)Resources.Load(fileName, typeof(TextAsset));
			xmlDoc.LoadXml(textXml.text);
		}
	}
	

	// Following method reads the xml file and display its content 
	void readXml()
	{
//		foreach(XmlElement node in xmlDoc.SelectNodes("Player"))
//		{
//			playersParam.userId = int.Parse(node.SelectSingleNode("userId").InnerText);
//			playersParam.userName = node.SelectSingleNode("userName").InnerText;
//			playersParam.fblogin = int.Parse(node.SelectSingleNode("fbLogin").InnerText);
//			playersParam.referralCode = node.SelectSingleNode("referralCode").InnerText;
//			Debug.Log("userid = "+playersParam.userId +" , userName =  "+playersParam.userName + " , isFbLogin = "+playersParam.fblogin + "referral_code = "+playersParam.referralCode);
//		}

//		int.TryParse (PlayerPrefs.GetString ("userId") , out playersParam.userId);
//		playersParam.userName = PlayerPrefs.GetString ("userName");
//		int.TryParse (PlayerPrefs.GetString ("fbLogin") , out playersParam.fblogin);
//		playersParam.referralCode = PlayerPrefs.GetString ("referralCode");

	}

	// Following method will be called by tapping ModifyXml button
//	public void modifyXml(string nodeName , string nodeValue)
//	{
////		foreach(XmlElement node in xmlDoc.SelectNodes("Player"))
////		{
////			node.SelectSingleNode(nodeName).InnerText = nodeValue;
////		}
////		xmlDoc.Save(getPath()+".xml");
//
//		PlayerPrefs.SetString (nodeName ,nodeValue);
//		readXml ();
//
//
//
//	}

	// Following method is used to retrive the relative path as device platform
	private string getPath(){
		#if UNITY_EDITOR
		return Application.dataPath +"/Resources/"+fileName;
		#elif UNITY_ANDROID
		return Application.persistentDataPath+fileName;
		#elif UNITY_IPHONE
//		return GetiPhoneDocumentsPath()+"/"+fileName;
		return Application.persistentDataPath+fileName;
		#else
		return Application.dataPath +"/"+ fileName;
		#endif
	}
	private string GetiPhoneDocumentsPath()
	{
		// Strip "/Data" from path 
		string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
		// Strip application name 
		path = path.Substring(0, path.LastIndexOf('/'));
		return path + "/Documents";
	}

	public void SaveData()
	{
		BinaryFormatter bf = new BinaryFormatter ();
//		FileStream file = new FileStream (Application.persistentDataPath + "/playerInfo.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");
		PlayerParam playerData = new PlayerParam ();

//		playerData.userIdNo = playersParam.userIdNo;
		playerData.userIdSaved = EncryptData (playersParam.userIdNo.ToString ());
		playersParam.userIdSaved = playerData.userIdSaved;
		playersParam.userId = MD5Crypt.Encrypt (playersParam.userIdNo.ToString (), serverEncryptionKey,true);
//		playerData.userId = playersParam.userId;
		playerData.userName = playersParam.userName;
		playerData.fblogin = playersParam.fblogin;
		playerData.referralCode = playersParam.referralCode;
		Debug.Log ("save data  ===  "+playerData.userId + " , "+playerData.userName);
		bf.Serialize (file,playerData);
		file.Close ();
	}
	
	public void LoadData()
	{
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerParam playerData = (PlayerParam)bf.Deserialize (file);
			playersParam.userIdSaved = playerData.userIdSaved;
			Debug.Log("playerData.userIdSaved" + playerData.userIdSaved);
			int.TryParse (DecryptData (playerData.userIdSaved) , out playersParam.userIdNo);
			playersParam.userId = MD5Crypt.Encrypt (playersParam.userIdNo.ToString (), serverEncryptionKey,true);
			playersParam.userName = playerData.userName;
			playersParam.fblogin = playerData.fblogin;
			playersParam.referralCode = playerData.referralCode;
			Debug.Log ("load data  ===  " + playerData.userId + " , " + playerData.userName +" ," +playerData.userIdNo +" , "+playerData.userIdSaved);
			file.Close ();
		} else {
			SaveData();
		}
	}

	public string EncryptData(string valueToEncrypt)
	{
		if (string.IsNullOrEmpty (valueToEncrypt) || valueToEncrypt == "0")
			return "";
		string encryptedValue = MD5Crypt.Encrypt (valueToEncrypt, dataEncryptedKey,true);
//		if(EncryptRSA._instance == null)
//			encryptedValue = transform.GetComponent<EncryptRSA>().EncryptViaRSA (encryptedValue);
//		else
//			encryptedValue = EncryptRSA._instance.EncryptViaRSA (encryptedValue);
		return encryptedValue;
	}

	public string DecryptData(string valueToDecrypt)
	{
		if (string.IsNullOrEmpty (valueToDecrypt)  || valueToDecrypt == "0")
			return "";
		string decryptedValue = "";
//		if(EncryptRSA._instance == null)
//			decryptedValue = transform.GetComponent<EncryptRSA>().Decrypt (valueToDecrypt);
//		else
//			decryptedValue = EncryptRSA._instance.Decrypt (valueToDecrypt);
//		decryptedValue =	MD5Crypt.Decrypt (decryptedValue, dataEncryptedKey,true);
		decryptedValue =	MD5Crypt.Decrypt (valueToDecrypt, dataEncryptedKey,true);
		return decryptedValue;
	}

}

[System.Serializable]
class PlayerDataClass
{
	public int userId;
	public string userName;
	public int isFbLoggedIn;
	public string referralCode;
}