using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class movingObj : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public static GameObject itemBeingDragged;
//	Vector3 startPosition;
//	Transform startParent;
//	public Transform midPos;
//	public Transform rightPos;
//	public Transform leftPos;
//	public Transform leftMid;
//	public Transform rightMid;
//	public Transform top;
//	public Transform down;
//	public GameObject xButton;
//	public Transform leftb;
//	public Transform rightb;
//	public Transform topb;
//	public Transform downb;
	//public GameObject xPos;
	bool isRight=false;
	#region IBeginDragHandler implementation
	
	public void OnBeginDrag (PointerEventData eventData)
	{
		itemBeingDragged = gameObject;
//		startPosition = transform.position;
//		startParent = transform.parent;
		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}
	
	#endregion
	
	#region IDragHandler implementation
	
	public void OnDrag (PointerEventData eventData)
	{
		//xButton.SetActive(true);
		transform.position= eventData.position;
	}
	
	#endregion
	
	#region IEndDragHandler implementation
	
	public void OnEndDrag (PointerEventData eventData)
	{
		itemBeingDragged = null;
		GetComponent<CanvasGroup>().blocksRaycasts = true;
//		if(transform.parent == startParent)
//		{
//			xButton.SetActive(false);
//			
//			iTween.MoveTo(this.gameObject,iTween.Hash("x",startPosition.x,"y",startPosition.y,"time",0.4,"easeType",iTween.EaseType.easeOutBack,"onComplete","lastPosition","onCompleteTarget",this.gameObject));
//			//transform.position = startPosition;
//		}
//		
	}
	
	#endregion
//	void Start()
//	{
//		this.gameObject.GetComponent<Image>().raycastTarget=false;
//		
//		if(PlayerPrefs.GetString("chat")=="off")
//		{
//			this.gameObject.GetComponent<DragHandeler>().enabled=false;
//			this.gameObject.GetComponent<Button>().interactable=false;
//			this.gameObject.GetComponentInChildren<Text>().enabled=false;
//			
//		}
//		else
//		{
//			
//		}
//		
//		xButton.SetActive(false);
//		//xPos.SetActive(false);
//		if(PlayerPrefs.GetString("bubbleX")== null || PlayerPrefs.GetString("bubbleX")== "")
//		{
//			PlayerPrefs.SetString("bubbleX",this.transform.position.x.ToString());
//			PlayerPrefs.SetString("bubbleY",this.transform.position.y.ToString());
//			PlayerPrefs.SetString("bubbleZ",this.transform.position.z.ToString());
//			
//		}
//		else
//		{
//			print ("========   " + float.Parse( "23.0") );
//			
//			print ("========   " + PlayerPrefs.GetString("bubbleX") );
//			Vector3 bubbleTemp;
//			bubbleTemp = new Vector3( float.Parse( PlayerPrefs.GetString("bubbleX") ), float.Parse( PlayerPrefs.GetString("bubbleY") ), float.Parse( PlayerPrefs.GetString("bubbleZ") ));
//			this.transform.position = bubbleTemp;
//		}
//		
//		
//	}
//	void lastPosition()
//	{
//		PlayerPrefs.SetString("bubbleX",this.transform.position.x.ToString());
//		PlayerPrefs.SetString("bubbleY",this.transform.position.y.ToString());
//		PlayerPrefs.SetString("bubbleZ",this.transform.position.z.ToString());
//	}
	void Update()
	{
		
//		if((this.gameObject.transform.position.x>midPos.transform.position.x) &&(isRight==false))
//		{				
//			startPosition.x=rightPos.transform.position.x;
//			isRight=true;
//			
//		}
//		else if((this.gameObject.transform.position.x<midPos.transform.position.x)&&(isRight==true))
//		{
//			
//			startPosition.x=leftPos.transform.position.x;	
//			
//			isRight=false;
//			
//		}
//		if(this.gameObject.transform.position.x<leftMid.transform.position.x)
//		{
//			if((this.gameObject.transform.position.y<top.transform.position.y) && (this.gameObject.transform.position.y>down.transform.position.y))
//			{
//				startPosition.y=this.gameObject.transform.position.y;
//				
//			}
//		}
//		else if(this.gameObject.transform.position.x>rightMid.transform.position.x)
//		{
//			if((this.gameObject.transform.position.y<top.transform.position.y) && (this.gameObject.transform.position.y>down.transform.position.y))
//			{
//				startPosition.y=this.gameObject.transform.position.y;
//				
//			}
//			
//		}
//		
//		
//		
//		if((this.gameObject.transform.position.x>leftb.transform.position.x)&&(this.gameObject.transform.position.x<rightb.transform.position.x))
//		{
//			if((this.gameObject.transform.position.y<topb.transform.position.y)&&(this.gameObject.transform.position.y>downb.transform.position.y))
//			{
//				
//				xButton.SetActive(false);
//				this.gameObject.GetComponent<DragHandeler>().enabled=false;
//				//this.gameObject.GetComponent<Button>().enabled=false;
//				this.GetComponent<Button>().interactable=false;
//				this.gameObject.GetComponentInChildren<Text>().enabled=false;
//				Invoke("chatOff",1);
//				//this.gameObject.SetActive(false);
//				
//				PlayerPrefs.SetString("chat","off");
//				
//			}
//			
//		}
//		
//		
		
		
	}
//	void chatOff()
//	{
//		iTween.MoveTo(this.gameObject,iTween.Hash("x",startPosition.x,"y",startPosition.y,"time",0.4,"easeType",iTween.EaseType.easeOutBack,"onComplete","lastPosition","onCompleteTarget",this.gameObject));
//	}
	
}
