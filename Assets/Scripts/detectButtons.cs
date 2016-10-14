using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class detectButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void trainingButton(UnityEngine.EventSystems.TouchInputModule button)
	{

		button.GetComponent<Image>().color=new Color32(0,0,0,255);
		//		for(int i=0;i<lockCards.Length;i++)
		//		{
		//			lockCards[i].GetComponent<Image>().color=new Color32(0,0,0,255);
		//		}
		Debug.Log("The button is being pressed!: " + button.ToString());
		
		//onclick.
//		skillPage.SetActive(true);
//		trainingCards.SetActive(false);
//		for(int i=0; i<forTrainingtime.Length;i++)
//		{
//			forTrainingtime[i].SetActive(false);
//		}
		
	}
}
