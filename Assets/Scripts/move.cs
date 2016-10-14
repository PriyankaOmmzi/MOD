using UnityEngine;
using System.Collections;

public class move : MonoBehaviour 
{
	public float speed;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			this.gameObject.transform.Translate(Vector3.left*Time.deltaTime);

		}
		if(Input.GetKey(KeyCode.RightArrow))
		{
			this.gameObject.transform.Translate(Vector3.right*Time.deltaTime);
			if(this.gameObject.transform.position.x>1.3f)
			{
				this.gameObject.transform.Translate(Vector3.right*speed);			
			}
			else if(this.gameObject.transform.position.x>2.5f)
			{
				speed=0;
			}

		}
		if(Input.GetKey(KeyCode.UpArrow))
		{
			this.gameObject.transform.Translate(Vector3.up*Time.deltaTime);

			
		}
		if(Input.GetKey(KeyCode.DownArrow))
		{
			this.gameObject.transform.Translate(Vector3.down*Time.deltaTime);

		}



	}
}
