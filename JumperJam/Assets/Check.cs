using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour {

	public bool stepped;
	public bool getStepped()
	{
		return stepped;
	}
	public void setStepped(bool stepped)
	{
		 this.stepped=stepped;
	}
}
