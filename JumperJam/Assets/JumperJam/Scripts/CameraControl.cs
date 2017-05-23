using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour 
{
	[SerializeField]
	GameObject player;

	//Thoi gian camera follow khi chet
	public float followTime = 0.3f;

	//khoang cach giua vi tri cua camera va player
	private float distanceFromPlayer;

	//Check xem da follow chua
	bool followed;
	//Check xem da lay distance chua
	bool checkedDistant;

	void Update()
	{

		//Camera khong follow khi Player idle ( roi xuong nhung ko chet )
		if ( player.transform.position.y>0)
		{
			if (player.transform.position.y > transform.position.y) 
			{
				this.transform.position = new Vector3 (0, player.transform.position.y, -100);
			}

		}

		//Camera follow khi player chet
		if (PlayerController.Instance.playerState == 0 &&!followed ) 
		{
			//Lay khoang cach giua player va camera
			if (!checkedDistant) 
			{
				distanceFromPlayer = transform.position.y - player.transform.position.y;
			}
			checkedDistant = true;

			//Follow player trong thoi gian followTime
			StartCoroutine ("deathCam");

		}

	}


	IEnumerator deathCam()
	{
		this.transform.position = new Vector3 (transform.position.x, player.transform.position.y - distanceFromPlayer, transform.position.z);
		yield return new WaitForSeconds(followTime);
		followed = true;
	}

}
