using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventManager;
//public class MapMgr : MonoBehaviour {
	public class MapMgr		: SingletonMonoBehaviour<MapMgr>
{
	[SerializeField]
	GameObject spawnPoint;

	[SerializeField]
	GameObject spawnStartPoint;

	[SerializeField]
	GameObject despawnHold;

	private int difficultCount = 0;


//	void OnEnable()
//	{
//		this.RegisterListener(EventID.GenMap, (sender, param) => GenMap());
//		this.RegisterListener(EventID.ResetDiff, (sender, param) => resetDifficult());
//
//	}
//
//	void OnDisable()
//	{
//				this.RemoveListener(EventID.GenMap, (sender, param) => GenMap());
//		this.RemoveListener(EventID.ResetDiff, (sender, param) => resetDifficult());
//	}


	public void  resetDifficult()
	{
		difficultCount = 0;
	}

	public void GenMap()
	{	int randomValue = 0;
		if (difficultCount < 3) {
			randomValue = Random.Range (1, 3);
			ContentMgr.Instance.GetItem ("Easy" + randomValue, spawnPoint.gameObject.transform.position);
			difficultCount++;
		} else if (difficultCount < 6) {
			randomValue = Random.Range (1, 3);
			ContentMgr.Instance.GetItem ("Normal" + randomValue, spawnPoint.gameObject.transform.position);
			difficultCount++;
		} else {
			randomValue = Random.Range (1, 3);
			ContentMgr.Instance.GetItem ("Hard" + randomValue, spawnPoint.gameObject.transform.position);

		}
	}

	public void GenStart()
	{
		int randomValue = 0;
		randomValue = Random.Range (1, 2);
		ContentMgr.Instance.GetItem ("Start" + randomValue, spawnStartPoint.gameObject.transform.position);
	}



}
