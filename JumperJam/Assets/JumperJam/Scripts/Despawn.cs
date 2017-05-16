using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Despawn : MonoBehaviour {

	[SerializeField]
	GameObject player;

	void Update()
	{	
		this.transform.position = new Vector3 (0, player.transform.position.y-35, 0);
	}

	public void OnTriggerEnter2D(Collider2D other)
	{ 
		if (other.CompareTag("PlatformG") )
					{
					//sua lai ve false truoc khi despawn cho vao pool
					other.gameObject.transform.GetComponent<Check> ().setStepped (false);
					ContentMgr.Instance.Despaw (other.gameObject.transform.parent.gameObject);
					}
	}
}
