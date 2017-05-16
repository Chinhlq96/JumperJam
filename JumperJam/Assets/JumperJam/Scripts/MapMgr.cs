﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventManager;
public class MapMgr : MonoBehaviour {

	[SerializeField]
	GameObject pos;

	[SerializeField]
	GameObject despawnHold;

	int Count=-1;
	void OnEnable()
	{
		this.RegisterListener(EventID.GenMap, (sender, param) => GenMap());

	}

	void OnDisable()
	{
				this.RemoveListener(EventID.GenMap, (sender, param) => GenMap());
	}

	void GenMap()
	{	int randomValue = 0;
		GameObject obj;
		randomValue = Random.Range (1,3);

		obj = ContentMgr.Instance.GetItem ("Easy" + randomValue, pos.gameObject.transform.position);
	

	}
}