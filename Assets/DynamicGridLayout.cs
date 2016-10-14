using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DynamicGridLayout : MonoBehaviour {

//	public int heightSet = 855, widthOk = 2503;
//	public GridLayoutGroup myLayoutElement; 
	public int valueOfWidth = 2224;
	public RectTransform parentTransform;

	void OnRectTransformDimensionsChangeChild()
	{
//		Debug.Log ("DimensionChanged");
		RectTransform parent = gameObject.GetComponent<RectTransform> ();
//		myLayoutElement.cellSize = parent.rect.width*heightSet/(float)widthOk ;
//		Debug.Log(parentTransform.rect.width);
		Vector2 newSize = new Vector2(parentTransform.rect.width * valueOfWidth / 2880f , parent.rect.height);
		parent.sizeDelta = newSize ;
	}


	void OnRectTransformDimensionsChange()
	{
//		Debug.Log ("DimensionChanged");
		RectTransform me = gameObject.GetComponent<RectTransform> ();
		float changedWidth = me.rect.width * valueOfWidth / 2880f;
//		Debug.Log(me.rect.width  +" , "+changedWidth);
		Vector2 newSize = new Vector2(me.rect.width * valueOfWidth / 2880f , parentTransform.rect.height);
		parentTransform.sizeDelta = newSize ;
	}

}
