using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEnoughSetActive : MonoBehaviour {

	public void SetActiveFail()
	{
		this.gameObject.SetActive (false);
	}
}
