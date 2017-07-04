﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour {

	/// <summary>
	/// Add to platform with PlatformG tag
	/// if 'Spawn' gameobject that follow player 'stepped' on these platform ---> new platform pattern is spawned
	/// </summary>
	private bool stepped;

	void OnEnable()
	{
		stepped = false;

	}
	public bool getStepped()
	{
		return stepped;
	}
	public void setStepped(bool stepped)
	{
		 this.stepped=stepped;
	}
}
