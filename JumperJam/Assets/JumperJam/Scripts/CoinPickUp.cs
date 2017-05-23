using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventManager;

public class CoinPickUp : SingletonMonoBehaviour<CoinPickUp> {

	[SerializeField]
	private int Point;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void OnTriggerEnter2D (Collider2D col)
	{
		if (col.CompareTag ("Player")) {
			gameObject.SetActive (false);
			// Add Point here
			GameMgr.Instance.AddPoint(Point);
			Debug.Log (GameMgr.Instance.ShowTotalPoint ());
		}
	}
		
}
