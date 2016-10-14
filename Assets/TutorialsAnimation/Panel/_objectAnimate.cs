using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class _objectAnimate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		iTween.MoveTo (this.gameObject, iTween.Hash ( "y",this.gameObject.transform.position.y+4f, "time", 0.3f, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.pingPong));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
