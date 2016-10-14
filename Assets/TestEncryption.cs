using UnityEngine;
using System.Collections;
using Banty.Security;

public class TestEncryption : MonoBehaviour {

	public string stringToDecrypt;

	public string stringToEncrypt;
	public string encryptionKey;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		stringToEncrypt = GUI.TextField (new Rect(0,0,100,50),stringToEncrypt);
		encryptionKey = GUI.TextField (new Rect(0,50,100,50),encryptionKey);

		if(GUI.Button (new Rect(150,100,100,100) , "Encrypt"))
		{
			stringToDecrypt = MD5Crypt.Encrypt (stringToEncrypt,encryptionKey,true);
			Debug.Log("Encrypted value md5 = "+stringToDecrypt);
			stringToDecrypt = EncryptRSA._instance.EncryptViaRSA (stringToDecrypt);
			Debug.Log("Encrypted value rsa = "+stringToDecrypt);
		}

		if(GUI.Button (new Rect(150,200,100,100) , "Decrypt"))
		{
			stringToDecrypt = EncryptRSA._instance.Decrypt (stringToDecrypt);
			Debug.Log("Decrypted value after RSA = "+stringToDecrypt);

			Debug.Log("Decrypted value = "+MD5Crypt.Decrypt (stringToDecrypt,encryptionKey,true));
		}

	}

}
