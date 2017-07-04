using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventManager;

public class CoinPickUp : MonoBehaviour {

	private int point = 1;

	public void OnTriggerEnter2D (Collider2D col)
	{
		if (col.CompareTag ("Player")) {
			ContentMgr.Instance.Despaw (gameObject);
			// Add Point here
			ScoreMgr.Instance.AddCoin(point);
		}
	}
		
}
