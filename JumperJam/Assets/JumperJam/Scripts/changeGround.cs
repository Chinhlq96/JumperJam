using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeGround : SingletonMonoBehaviour<changeGround> {
	public Sprite jungleGroundStyle;
	public Sprite iceGroundStyle;
	public Sprite poisonGroundStyle;
	public Sprite beanGroundStyle;
	public Sprite treeGroundStyle;



	void OnEnable()
	{
		//DOTween.Init();

		ChangeGround ();
	}

	public void ChangeGround()
	{
		if(GameMgr.Instance.randomValue==1)
			GetComponent<SpriteRenderer> ().sprite =jungleGroundStyle ;
		if(GameMgr.Instance.randomValue==2)
			GetComponent<SpriteRenderer> ().sprite =iceGroundStyle ;
		if(GameMgr.Instance.randomValue==3)
			GetComponent<SpriteRenderer> ().sprite =beanGroundStyle ;
		if(GameMgr.Instance.randomValue==4)
			GetComponent<SpriteRenderer> ().sprite =poisonGroundStyle ;
		if(GameMgr.Instance.randomValue==5)
			GetComponent<SpriteRenderer> ().sprite =treeGroundStyle ;
	}
}
