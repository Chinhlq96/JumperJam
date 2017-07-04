using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventManager;

public class MapMgr		: SingletonMonoBehaviour<MapMgr>
{

	// Point to spawn next Platforms pattern
	[SerializeField]
	GameObject spawnPoint;

	// Point to spawn Start Platform
	[SerializeField]
	GameObject spawnStartPoint;

	// Assign to every platform that is spawned , then add it to a list in GameMgr , this list's items will be despawn when restart
	private GameObject spawnPlatformHold;


	// Determine Platform difficult
	private int difficultCount = 0;


	// Reset to Easy
	public void  ResetDifficult()
	{
		difficultCount = 0;
	}


	public void GenMap()
	{	
		//There are 3 type of platforms patterns: Easy , Normal , Hard.
		// each of them has serveral alternatives
		// -> randomly spawn them 
		int randomPlatformValue = 0;


		//difficultCount plus 1 everytime a pattern is spawned
		//object pool : ContentMgr...."Easy"+randomPlatformValue  --> alternatives.
		if (difficultCount < 3) 
		{
			randomPlatformValue = Random.Range (1, 4);
			spawnPlatformHold = ContentMgr.Instance.GetItem ("Easy" + randomPlatformValue, spawnPoint.gameObject.transform.position);
			difficultCount++;
		}
		else if (difficultCount < 6) 
		{
			randomPlatformValue = Random.Range (1, 4);
			spawnPlatformHold = ContentMgr.Instance.GetItem ("Normal" + randomPlatformValue, spawnPoint.gameObject.transform.position);
			difficultCount++;
		}
		else
		{
			randomPlatformValue = Random.Range (1, 4);
			spawnPlatformHold = ContentMgr.Instance.GetItem ("Hard" + randomPlatformValue, spawnPoint.gameObject.transform.position);

		}

		//Add spawned pattern to List
		GameMgr.Instance.platformList.Add (spawnPlatformHold);
	}



	//Similar to above function but this is for Start Platform --> spawn only 1 time
	public void GenStart()
	{
		
		int randomPlatformValue = 0;
		randomPlatformValue = Random.Range (1, 4);
		spawnPlatformHold = ContentMgr.Instance.GetItem ("Start" + randomPlatformValue, spawnStartPoint.gameObject.transform.position);
		GameMgr.Instance.platformList.Add (spawnPlatformHold);
	}



}
