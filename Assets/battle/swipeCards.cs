using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class swipeCards : MonoBehaviour {
	public GameObject canvasLeftRight;
	Vector2 startPos, endPos, diff;
	public float sensitivity = 5;
	float distance;
	bool isCallOnce = false;
	
	public enum SwipeDirection 
	{
		None,
		Left,
		Right,
		Up,
		Down
	};
	
	public SwipeDirection swipeDir;
	
	public static swipeCards instance;
	
	void Start () {
		
		instance = this;
	}
	
	void Update ()
	{
		
		if(Input.GetMouseButtonDown(0))
		{
			startPos = Input.mousePosition; // Save start position of touch
		}
		
		if(Input.GetMouseButton(0)) {
			endPos = Input.mousePosition; // Save end position of touch
			diff = endPos - startPos; // Calculate difference
			
			distance = Mathf.Sqrt(Mathf.Pow(diff.x, 2) + Mathf.Pow(diff.y, 2)); // Calculate swipe distance
			
			if(distance < Screen.width / sensitivity) { // Find whether swipe is valid or not
				return;
			}
			
			if(!isCallOnce) { // If swipe is valid find direction
				FindSwipeDirection();
			}
			
		}
		
		if(Input.GetMouseButtonUp(0)) { // Reset every var
			startPos = endPos = diff = Vector2.zero;
			isCallOnce = false;
			swipeDir = SwipeDirection.None;
		}
		
	}
	
	void FindSwipeDirection () {
		isCallOnce = true;
		float slope = diff.y / diff.x; // Find slope to calculate, horizontal or vertical swipe
		slope = Mathf.Abs (slope); // Save positive slope value
		
		if (diff.x > 1 && slope < 1) {
			
			
			//print(nameCounter + "-------------------------------------");
			//print(PlayerPrefs.GetString("PlayerCharacterType") + PlayerPrefs.GetInt("PlayerCharacter"));

			canvasLeftRight.GetComponent<battleFormation>().rightButton();

			
			swipeDir = SwipeDirection.Right;
		}
		else if (diff.x < 1 && slope < 1)
		{
			
			

			canvasLeftRight.GetComponent<battleFormation>().leftButton();


			swipeDir = SwipeDirection.Left;
		} 
		else if (diff.y > 1 && slope > 1)
		{
			swipeDir = SwipeDirection.Up;
		}
		else if (diff.y < 1 && slope > 1)
		{
//			if(cardDrawButton.GetComponent<newShop>().drawCard.activeInHierarchy==true)
//			{
//				cardDrawButton.GetComponent<newShop>().drawPlay();
//			}
//			else if(cardDrawButton.GetComponent<newShop>().drawCardMulti.activeInHierarchy==true)
//			{
//				cardDrawButton.GetComponent<newShop>().drawPlaymulti();
//			}
			swipeDir = SwipeDirection.Down;
		} else 
		{
			swipeDir = SwipeDirection.None;
		}
		
		
	}
}