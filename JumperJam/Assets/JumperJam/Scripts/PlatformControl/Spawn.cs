using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EventManager;

public class Spawn : MonoBehaviour {

	[SerializeField]
	GameObject player;

	void Update()
	{	
		this.transform.position = new Vector3 (0, player.transform.position.y, 0);
	}

	public void OnTriggerEnter2D(Collider2D other)
	{ 
		if (other.CompareTag("PlatformG") )
		{
			//prevent mass spawn when keep jumping on same platformG 
			if (!other.gameObject.GetComponent<Check> ().getStepped ())
			{	

				this.PostEvent (EventID.GenMap, this);
				other.gameObject.GetComponent<Check> ().setStepped (true);
			}
		}
	}
}

