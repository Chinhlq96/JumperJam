﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {



	void OnEnable() 
	{	
		StartCoroutine (WaitAndSpawn (0.5f));
		//Spawn ();

	}
//	void Spawn()
//	{
//		int randomValue = 0;
//		randomValue = Random.Range (1, 7);
//
//		{
//			switch (randomValue) 
//			{
//			case 1:
//				ContentMgr.Instance.GetItem ("Enemy1", transform.position);
//				break;
//			case 2:
//				ContentMgr.Instance.GetItem ("Enemy2", transform.position);
//				break;
//			case 3:
//				ContentMgr.Instance.GetItem ("Enemy3", transform.position);
//				break;
//			case 4:
//				ContentMgr.Instance.GetItem ("Enemy4", transform.position);
//				break;
//			default:
//				break;
//			}
//		}
//	}
	IEnumerator WaitAndSpawn(float waitTime)
	{
		//
		yield return new WaitForSeconds(waitTime);

		int randomValue = 0;

		//Spawn Fly Enemy type;

			if (gameObject.name == "SpawnEnemyPoint") 
			{
				if (gameObject.transform.parent.CompareTag ("PlatformEasy"))
				randomValue = Random.Range (1, 15);
				else if (gameObject.transform.parent.CompareTag ("PlatformNormal"))
				randomValue = Random.Range (1, 13);
				else 
				randomValue = Random.Range (1, 10);
				

				switch (randomValue)
				{
				case 1:
					ContentMgr.Instance.GetItem ("Enemy1", transform.position);
					break;
				case 2:
					ContentMgr.Instance.GetItem ("Enemy2", transform.position);
					break;
				case 3:
					ContentMgr.Instance.GetItem ("Enemy3", transform.position);
					break;
				case 4:
					ContentMgr.Instance.GetItem ("Enemy4", transform.position);
					break;
				case 5:
				ContentMgr.Instance.GetItem ("Coin", transform.position);
				break;

			case 6:
				ContentMgr.Instance.GetItem ("Enemy5", transform.position);
				break;
			case 7:
				ContentMgr.Instance.GetItem ("Enemy6", transform.position);
				break;
			case 8:
				ContentMgr.Instance.GetItem ("Enemy7", transform.position);
				break;
			case 9:
				ContentMgr.Instance.GetItem ("Enemy8", transform.position);
				break;
				default:
					break;
				}
			}

			//Spawn normal Enemy Type
			if (gameObject.name == "SpawnFlyEnemyPoint") 
			{


				if (gameObject.transform.parent.CompareTag ("PlatformEasy"))
				randomValue = Random.Range (1, 7);
				else if (gameObject.transform.parent.CompareTag ("PlatformNormal"))
				randomValue = Random.Range (1, 6);
				else 
				randomValue = Random.Range (1, 3);
			
				switch (randomValue) 
				{
				case 1:
					ContentMgr.Instance.GetItem ("EnemyFly1", transform.position);
					break;
				case 2:
				ContentMgr.Instance.GetItem ("Coin", transform.position);
				break;
				case 3:
					ContentMgr.Instance.GetItem ("EnemyFly2", transform.position);
					break;
				case 4:
				ContentMgr.Instance.GetItem ("EnemyFly3", transform.position);
				break;
				default:
					break;
				}

		
			}
		
	}

}