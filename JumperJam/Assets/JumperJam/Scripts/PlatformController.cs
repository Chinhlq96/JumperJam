using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventManager;
using DG.Tweening;

public class PlatformController : MonoBehaviour {

	private bool Bouncing;

	void OnEnable()
	{
		//DOTween.Init();
		Bouncing = false;
	}
	// Use this for initialization
	public void OnTriggerEnter2D(Collider2D col)
	{
		//Debug.Log ("0");
		if (col.CompareTag("Player"))
		{
			//Debug.Log ("1");
			if (col.transform.position.y > transform.position.y + 0.3f ) 
			{
				Bounce ();
			}
			//Debug.Log ("2");
		}
	}

	private void Bounce ()
	{
		if (Bouncing == false) {
			Bouncing = true;
			Debug.Log ("one");
			//Vector2 savePos = new Vector2(transform.position.x,transform.position.y);
			transform.DOLocalMoveY (transform.position.y-0.3f, 0.01f).OnComplete (() => {
				transform.DOLocalMoveY (transform.position.y + 0.3f, 0.01f).OnComplete(() => {
					Bouncing = false;
				});
			});
		}
	}

	void OnDisalbe()
	{
		
	}
}
