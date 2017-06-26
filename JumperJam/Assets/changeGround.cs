using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeGround : MonoBehaviour {
	public Sprite jungleGroundStyle;
	public Sprite iceGroundStyle;


	void OnEnable()
	{
		//DOTween.Init();


		if(GameMgr.Instance.randomValue==1)
			GetComponent<SpriteRenderer> ().sprite =jungleGroundStyle ;
		if(GameMgr.Instance.randomValue==2)
			GetComponent<SpriteRenderer> ().sprite =iceGroundStyle ;
	}
}
