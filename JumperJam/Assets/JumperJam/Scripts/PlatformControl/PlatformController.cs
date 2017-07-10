using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventManager;
using DG.Tweening;

public class PlatformController : MonoBehaviour {

	private bool Bouncing;
	public Sprite junglePlatformStyle;
	public Sprite icePlatformStyle;
	public Sprite beanPlatformStyle;
	public Sprite treePlatformStyle;
	public Sprite poisonPlatformStyle;

	private bool changed;

	//Change platform style 
	// not bounce at start
	void OnEnable()
	{

		Bouncing = false;


		if(GameMgr.Instance.randomValue==1)
			GetComponent<SpriteRenderer> ().sprite =junglePlatformStyle ;
		if(GameMgr.Instance.randomValue==2)
			GetComponent<SpriteRenderer> ().sprite =icePlatformStyle ;
		if(GameMgr.Instance.randomValue==3)
			GetComponent<SpriteRenderer> ().sprite =beanPlatformStyle ;
		if(GameMgr.Instance.randomValue==4)
			GetComponent<SpriteRenderer> ().sprite =poisonPlatformStyle ;
		if(GameMgr.Instance.randomValue==5)
			GetComponent<SpriteRenderer> ().sprite =treePlatformStyle ;
	}

	public void OnTriggerEnter2D(Collider2D col)
	{
		
		if (col.CompareTag("Player"))
		{

			if (col.transform.position.y > transform.position.y + 0.3f ) 
			{
				Bounce ();
			}

		}
	}

	private void Bounce ()
	{
		if (Bouncing == false) 
		{
			Bouncing = true;
			transform.DOMoveY (transform.position.y - 0.3f, 0.1f).OnComplete (() => {
			transform.DOMoveY (transform.position.y + 0.3f, 0.1f).OnComplete(() => {

			Bouncing = false;
				});
			});
		}
	}

		
}
