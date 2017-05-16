using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour 
{
	[SerializeField]
	GameObject player;

	bool followed;
	void Update()
	{
		if ( player.transform.position.y>0)
		{
			if (player.transform.position.y > transform.position.y) 
			{
				this.transform.position = new Vector3 (0, player.transform.position.y, -100);
			}
	
	 	}

		if (PlayerController.Instance.playerState == 0 && !followed) 
		{
			
			float time = 0;
			float followTime = 3f;
			if(time < followTime && !followed)
			{
				time += Time.deltaTime;
				this.transform.position = new Vector3 (0, player.transform.position.y, -100);
				//if(time >= 3f)
					//followed = true;
			}
			//StartCoroutine ("deathCam");
			followed = true;

		}
	}

	IEnumerator deathCam()
	{
		Debug.Log ("ayy");
		float time = 0;
		float followTime = 5f;
		while (time < followTime) 
		{
			Debug.Log ("ayy");
			time += Time.deltaTime;
			this.transform.position = new Vector3 (0, player.transform.position.y, -100);

		}
		yield return null;
	}


}
