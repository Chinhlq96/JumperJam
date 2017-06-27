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


	void OnEnable()
	{
		//DOTween.Init();
		Bouncing = false;
		Debug.Log (GameMgr.Instance.randomValue);
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

	//bulshitttttttttttttttttttttttttttttttttttttttttttttttttttt
//	void Update()
//	{
//		if (ScoreMgr.Instance.getScore () >= 300) {
//			if(GameMgr.Instance.randomValue==2)
//			GetComponent<SpriteRenderer> ().sprite =junglePlatformStyle ;
//			if(GameMgr.Instance.randomValue==1)
//				GetComponent<SpriteRenderer> ().sprite =icePlatformStyle ;
//			
//		}
//	}
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
			//Vector2 savePos = new Vector2(transform.position.x,transform.position.y);

			transform.DOMoveY (transform.position.y - 0.3f, 0.1f).OnComplete (() => {
				transform.DOMoveY (transform.position.y + 0.3f, 0.1f).OnComplete(() => {

					Bouncing = false;
				});
			});
		}
	}

	void OnDisalbe()
	{
		
	}
}
