using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DynamicGrid : MonoBehaviour {

	public int heightSet = 855, widthOk = 2503;
	public LayoutElement myLayoutElement; 


	void OnRectTransformDimensionsChange()
	{
		Debug.Log ("DimensionChanged");
		RectTransform parent = gameObject.GetComponent<RectTransform> ();
		myLayoutElement.minHeight = parent.rect.width*heightSet/(float)widthOk ;
	}

}
