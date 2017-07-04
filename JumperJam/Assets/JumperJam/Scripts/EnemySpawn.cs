using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

	/// <summary>
	/// Spawn Enemy
	/// </summary>

	// Platform pattern list
	List<GameObject> spawnList = new List<GameObject>();

	// Assign to spawned platform pattern, then add to spawnList, spawnList's element will be despawn on replay
	GameObject spawnHolder;


	//Has to use coroutine, if not it will fuck up with pool
	void OnEnable() 
	{	
		StartCoroutine (WaitAndSpawn (0.5f));
	}


	IEnumerator WaitAndSpawn(float waitTime)
	{
		
		yield return new WaitForSeconds(waitTime);

		int randomMobValue = 0;



		//Spawn Normal Enemy type;

			if (gameObject.name == "SpawnEnemyPoint") 
			{

			//Ti le spawn enemy phu thuoc vao do kho cua platform pattern
				if (gameObject.transform.parent.CompareTag ("PlatformEasy"))
				randomMobValue = Random.Range (1, 22);
				else if (gameObject.transform.parent.CompareTag ("PlatformNormal"))
				randomMobValue = Random.Range (1, 16);
				else 
				randomMobValue = Random.Range (1, 13);
				

			// pool spawn enemy
				switch (randomMobValue)
				{
				case 1:
				spawnHolder = ContentMgr.Instance.GetItem ("Enemy1", transform.position);
					break;
				case 2:
				spawnHolder = ContentMgr.Instance.GetItem ("Enemy2", transform.position);
					break;
				case 3:
				spawnHolder =	ContentMgr.Instance.GetItem ("Enemy3", transform.position);
					break;
				case 4:
				spawnHolder =	ContentMgr.Instance.GetItem ("Enemy4", transform.position);
					break;
				case 5:
				spawnHolder =	ContentMgr.Instance.GetItem ("Coin", transform.position);
				break;

			case 6:
				spawnHolder =	ContentMgr.Instance.GetItem ("Enemy5", transform.position);
				break;
			case 7:
				spawnHolder =	ContentMgr.Instance.GetItem ("Enemy6", transform.position);
				break;
			case 8:
				spawnHolder =	ContentMgr.Instance.GetItem ("Enemy7", transform.position);
				break;
			case 9:
				spawnHolder =   ContentMgr.Instance.GetItem ("Enemy8", transform.position);
				break;
			case 10:
				spawnHolder = ContentMgr.Instance.GetItem ("Boost", transform.position);
				break;

			default:
					break;
				}
			}



			//Spawn Fly Enemy Type
			if (gameObject.name == "SpawnFlyEnemyPoint") 
			{

			// ti le sinh enemy phu thuoc do kho platform pattern
				if (gameObject.transform.parent.CompareTag ("PlatformEasy"))
				randomMobValue = Random.Range (1, 9);
				else if (gameObject.transform.parent.CompareTag ("PlatformNormal"))
				randomMobValue = Random.Range (1, 8);
				else 
				randomMobValue = Random.Range (1, 7);

			//pool spawn quai
				switch (randomMobValue) 
				{
				case 1:
				spawnHolder =	ContentMgr.Instance.GetItem ("EnemyFly1", transform.position);
					break;
				case 2:
				spawnHolder =	ContentMgr.Instance.GetItem ("Coin", transform.position);
				break;
				case 3:
				spawnHolder =	ContentMgr.Instance.GetItem ("EnemyFly2", transform.position);
					break;
				case 4:
				spawnHolder =  ContentMgr.Instance.GetItem ("EnemyFly3", transform.position);
				break;
				default:
					break;
				}

		
			}

		//add vao list enemy
		if(spawnHolder!=null)
		GameMgr.Instance.spawnList.Add (spawnHolder);

	}

}