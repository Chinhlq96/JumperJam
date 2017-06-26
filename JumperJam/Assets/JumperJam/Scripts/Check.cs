using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour {

	public bool stepped;
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
