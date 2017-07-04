using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Despawn : MonoBehaviour 
{
	/// <summary>
	/// A Despawn area followed player to despawn Platform/Enemy when they out of sign
	/// </summary>
	[SerializeField]
	GameObject player;

	void Update()
	{	
		this.transform.position = new Vector3 (0, player.transform.position.y-35, 0);
	}

	public void OnTriggerEnter2D(Collider2D other)
	{ 
		if (other.CompareTag("PlatformG"))
		{
			//Despawn Platform	
			ContentMgr.Instance.Despaw (other.gameObject.transform.parent.gameObject);
		}
		if (other.CompareTag("Enemy") || other.CompareTag("UndeadEnemy"))
		{
			//Neu enemy o trong mot object khac ( object Enemy chua enemy va patrol point ) thi despawn ca parent
			try
				{
				ContentMgr.Instance.Despaw (other.gameObject.transform.parent.gameObject);
				}
			//khong thi despawn chinh object do
			catch
				{
				ContentMgr.Instance.Despaw (other.gameObject);
				}
		}
		//Despawn Coins
		if(other.CompareTag("Coin") )
		{
			ContentMgr.Instance.Despaw (other.gameObject);
		}
	}
}
