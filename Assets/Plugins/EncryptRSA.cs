using UnityEngine;
using System.Collections;    
using System.Security.Cryptography;
using System.Text;
using System;
using System.IO;
using System.Globalization;

public class EncryptRSA : MonoBehaviour {

	public static EncryptRSA _instance;

	void Awake()
	{
		_instance = this;
	}
	public string dataEncrytpyionKey;

	public static string encryptedString;


	public string EncryptViaRSA(string toEncrpyt ) {
		// For encrypting number in player pref.
		CspParameters cspParams = new CspParameters();
		
		cspParams.KeyContainerName = dataEncrytpyionKey;  // This is the key used to encrypt and decrypt can be anything.
		var provider = new RSACryptoServiceProvider(cspParams);

		// Get the value stored in RegString and decrypt it using the key.

		

			byte[] encryptedBytes = provider.Encrypt(
				System.Text.Encoding.UTF8.GetBytes(toEncrpyt), true);
			// convert to base64string first for storage as a string in the registry.
		encryptedString =  Convert.ToBase64String(encryptedBytes);
		return encryptedString;
		
	}


	public string Decrypt(string toDecrypt)
	{
		CspParameters cspParams = new CspParameters();
		
		cspParams.KeyContainerName = dataEncrytpyionKey;  // This is the key used to encrypt and decrypt can be anything.
		var provider = new RSACryptoServiceProvider(cspParams);
		string decrypted = System.Text.Encoding.UTF8.GetString(
			provider.Decrypt   (Convert.FromBase64String(toDecrypt) , true));

		return decrypted;
	}
	
}