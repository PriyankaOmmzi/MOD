using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MiniJSON;

public class SignUp : MonoBehaviour
{
	public static SignUp instance;
	public InputField email,confirmEmail,password,confirmPassword,userName,refferalCode;
	public InputField userNameOfLogin;
	public GameObject loginPanel;
	string passwordT;
	public GameObject emailNot;
	public GameObject passwordNot;
	public Text emailFieldWarning,confirmEmailFieldWarning,passwordFieldWarning,confirmPasswordFieldWarning,userNameWarning , referralCodeWarning;
	public Popup popupFromServer;
	public Popup signupSuccessPopupFromServer;
	// Use this for initialization

	void Awake()
	{
		instance=this;
	}
	void OnEnable () 
	{
		passwordT=password.text;
		emailNot.SetActive(false);
		passwordNot.SetActive(false);
		email.text = "";
		confirmEmail.text = "";
		password.text = "";
		confirmPassword.text = "";
		userName.text = "";
		refferalCode.text = "Optional";
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape) && !newMenuScene.instance.loader.activeInHierarchy) {
			if(popupFromServer.gameObject.activeInHierarchy)
			{
				popupFromServer.ExitPopup ();
			}
			else if(signupSuccessPopupFromServer.gameObject.activeInHierarchy)
			{
				popupFromServer.ExitSignupPopup ();
			}
			else
			{
				newMenuScene.instance.signUpExit ();
			}
		}
	}

	public void UserNameFilled(InputField input)
	{
		Debug.Log ("-------"+input.text);
		if (input.text.Length > 20) 
		{
			userNameWarning.color = new Color32(201,0,0,255);
		}
		else
		{
			userNameWarning.color = Color.black;
		}

	}

	public void PasswordFilled(InputField input)
	{
		Debug.Log ("-------"+input.text);
		if (input.text.Length < 6) 
		{
			passwordFieldWarning.color = new Color32(201,0,0,255);
		}
		else
		{
			passwordFieldWarning.color = Color.black;
		}
	}

	public void ConfirmPasswordFilled(InputField input)
	{
		Debug.Log ("-------"+input.text);
		if (password.text == confirmPassword.text) 
		{
			confirmPasswordFieldWarning.gameObject.SetActive (false);
		}
		else
		{
			confirmPasswordFieldWarning.gameObject.SetActive (true);
		}
	}

	public void EmailFilled(InputField input)
	{
		bool isEmail = System.Text.RegularExpressions.Regex.IsMatch(input.text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
		
		if (!isEmail) 
		{
			emailFieldWarning.color = new Color32(201,0,0,255);
		}
		else
		{
			emailFieldWarning.color = Color.black;
		}

		if(email.text==confirmEmail.text)
		{
			emailNot.SetActive(false);
		}
		else
			emailNot.SetActive(true);
	}

	public void ConfirmEmailFilled(InputField input)
	{
		if(email.text==confirmEmail.text)
		{
			emailNot.SetActive(false);
		}
		else
			emailNot.SetActive(true);
	}

	public void ReferralCode(InputField input)
	{
	}


	void deActivate()
	{
		loginPanel.SetActive(false);

	}
	void  sendata()
	{
//		string url  = "http://www.myriadofdragon.ommzisolutions.com/wot/api/index.php";
//
//		WWWForm form = new WWWForm ();
//	
//		form.AddField ("email", email.text);
//		form.AddField ("password", password.text);
//		//form.AddField( "avatarname",avatarname.text);
////		form.AddField( "description",description.text);
//
//		form.AddField ("apikey", "wot119323s");
//		form.AddField( "action", "newAccount");
////		form.AddField( "action", "finishAccount");
//
//		form.AddField("version", "1");
//
//		form.AddField("phonecode", SystemInfo.deviceUniqueIdentifier);
//
//		//form.AddField ("avatarname", avatarName.text);
//
//		
//		WWW www = new WWW (url, form);
//		
//		StartCoroutine (WaitForRequest (www));
//		
	}


//	public void RESET()
//	{
//		email.GetComponent<Text>().text="";
//		confirmEmail.GetComponent<Text>().text="";
//		password.GetComponent<Text>().text="";
//		confirmPassword.GetComponent<Text>().text="";
//			
//
//	}

	IEnumerator SendData()
	{
		WWWForm wform = new WWWForm ();
		wform.AddField ("tag", "userGetRegister");
		wform.AddField ("username", userName.text);
		wform.AddField ("password", password.text);
		wform.AddField ("email", email.text);
		if (refferalCode.text != null && refferalCode.text != "Optional") {
			wform.AddField ("friend_refferral_code", refferalCode.text);
		}
		WWW wwwRegister = new WWW (loadingScene.Instance.baseUrl, wform);
		yield return wwwRegister;
		if (wwwRegister.error == null) {
			IDictionary dict = Json.Deserialize(wwwRegister.text) as IDictionary; 
			if(wwwRegister.text.Contains("error_msg"))
			{
//				{"success":0,"error_msg":"Username or email already exist!"}
				popupFromServer.ShowPopup (dict["error_msg"].ToString());
			}
			else
			{
//			{"success":1,"msg":"User created successfully","user":{"user_id":"45","name":"pr","peace_treaty":"1","my_referral_code":"3d5230"}}
				userNameOfLogin.text = userName.text;
				IDictionary userResonse = (IDictionary)(dict["user"]);
				Debug.Log ("user_id"+userResonse["user_id"].ToString ());
				Debug.Log ("name"+userResonse["name"].ToString ());
				Debug.Log ("peace_treaty"+userResonse["peace_treaty"].ToString ());
				Debug.Log ("my_referral_code"+userResonse["my_referral_code"].ToString ());
				signupSuccessPopupFromServer.ShowPopup ("A validation email has been sent to your email id. Kindly confirm the email before proceeding!");
			}
		
			Debug.Log (wwwRegister.text);
		} 
		else 
		{
			popupFromServer.ShowPopup ("Network Error!");
			Debug.Log ("------" + wwwRegister.error);
		}
	}
	

	public void onSignUp()
	{
		bool isEmail = System.Text.RegularExpressions.Regex.IsMatch(email.text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

		if (email.text == "" || confirmEmail.text == "" || password.text == "" || confirmPassword.text == "" || userName.text == "") {
			popupFromServer.ShowPopup("Please fill all the compulsory fields");

		} else if (password.text.Length < 6) {
			popupFromServer.ShowPopup("Password is too short!");
		} 
		else if (password.text.Contains (" ") || password.text.Contains ("..")) {
			popupFromServer.ShowPopup("Invalid Password!");
		}else if (password.text != confirmPassword.text) {
			popupFromServer.ShowPopup("Passwords do not match!");
		} else if (email.text != confirmEmail.text) {
			popupFromServer.ShowPopup("Email id does not match!");
		} else if (!isEmail) {
			popupFromServer.ShowPopup("Invalid Email!");
		}
//		else if (referralCodeWarning.enabled == true) {
//			popupFromServer.ShowPopup("Invalid Referral Code!");
//		}
		else {
			newMenuScene.instance.loader.SetActive (true);
			
//			StartCoroutine (NetWorkConnectivityCheck._instance.TestConnection ((isConnected) => {
			NetWorkConnectivityCheck._instance.CheckConnectionThread ((isConnected) => {
				if(isConnected)
					StartCoroutine (SendData ());
				else
					popupFromServer.ShowPopup ("Network Error!");
			});
		}
	}
}

