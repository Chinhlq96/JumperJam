using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventManager;

public class CoinPickUp : MonoBehaviour {

	private int point = 1;
	[SerializeField]
	private Vector2 force = new Vector2(0,80);

	public void OnTriggerEnter2D (Collider2D col)
	{
		if (col.CompareTag ("Player")) 
		{
			if (this.CompareTag ("Coin")) 
			{
				ContentMgr.Instance.Despaw (gameObject);
				// Add Point here
				ScoreMgr.Instance.AddCoin (point);
			}
			if(this.CompareTag("Boost")&&PlayerController.Instance.playerState!=PlayerState.Die)
				{
				//PlayerController.Instance.ResetVelocity ();

				col.gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
				//Invoke ("boosted", 0.1f);
				PlayerController.Instance.GetComponent<Rigidbody2D> ().AddForce (new Vector2(0,80), ForceMode2D.Impulse);
				PlayerController.Instance.DashEnabled ();
				PlayerController.Instance.DashWaitedDisable ();
				PlayerController.Instance.InvuState ();
				ContentMgr.Instance.Despaw (gameObject);
				}
		}

	}

	void boosted()
	{
		
	}
		
}
