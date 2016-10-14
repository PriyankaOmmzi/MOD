using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DynamicGridSize : MonoBehaviour {

	public int cellSizeHeight = 855, cellSizeWidth = 2503;
	public int actualHeightOfCanvas , actualWidthOfCanvas;
	public GridLayoutGroup myLayoutElement; 
//	public int valueOfWidth = 2224;
	public RectTransform parentTransform;

	void OnRectTransformDimensionsChange()
	{
		Debug.Log ("DimensionChanged "+parentTransform.rect.width);
		Debug.Log ("DimensionChanged "+parentTransform.rect.height);
		myLayoutElement.cellSize = new Vector2((parentTransform.rect.width*cellSizeWidth/(float)actualWidthOfCanvas) , (parentTransform.rect.height*cellSizeHeight/(float)actualHeightOfCanvas)) ;
	}




}
