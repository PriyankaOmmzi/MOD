using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChestEventMainPageScrollElement : MonoBehaviour {
	public enum ChestEventScrollElementType
	{
		GUILD,
		PLAYER,
		NEXTMILESTONE
	}
	public Text heading;
	public Image profileImage;
	public Text pointHeading;
	public Text point;
	public Text rankHeading;
	public Text rank;

	public ChestEventScrollElementType myType;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
