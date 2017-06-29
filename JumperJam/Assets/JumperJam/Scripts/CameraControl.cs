﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraControl : MonoBehaviour 
{
	[SerializeField]
	GameObject player;

	[SerializeField]
	Transform BeginCameraPoint;

	//Thoi gian camera follow khi chet
	public float followTime = 0.3f;

	//khoang cach giua vi tri cua camera va player
	private float distanceFromPlayer;

	//Check xem da follow chua
	bool followed;
	//Check xem da lay distance chua
	bool checkedDistant;

	bool tweened;

	public void resetDistant()
	{
		distanceFromPlayer = 0f;
	}

	public void resetCamera()
	{
		transform.position = BeginCameraPoint.position;
	}
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
		if (PlayerController.Instance.playerState == 0 &&!followed &&! player.GetComponent<PlayerController>().groundDeath ) 
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

	public void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("Ground")) {
			Destroy (col.gameObject);
			DestroyObject (col.gameObject);
		}
	}


//	//if (PlayerController.Instance.playerState == 0 &&!followed &&! player.GetComponent<PlayerController>().groundDeath ) 
//	public void followOnDeath()
//	{
//		if (!followed && !player.GetComponent<PlayerController> ().groundDeath) 
//		{
//			//Lay khoang cach giua player va camera
//			if (!checkedDistant) {
//				distanceFromPlayer = transform.position.y - player.transform.position.y;
//			}
//			checkedDistant = true;
//
//			//Follow player trong thoi gian followTime
//			StartCoroutine ("deathCam");
//		}
//
//
//
//	}


	IEnumerator deathCam()
	{	
		
		//this.transform.position = new Vector3 (transform.position.x, player.transform.position.y - distanceFromPlayer, transform.position.z);
		if (!tweened)
		{
			this.transform.DOMove (new Vector3 (transform.position.x, player.transform.position.y, transform.position.z), 0.2f);
			yield return new WaitForSeconds (0.2f);
			tweened = true;
		}

		this.transform.position = new Vector3 (transform.position.x, player.transform.position.y, transform.position.z);

		yield return new WaitForSeconds(followTime);
		followed = true;
	}

//	IEnumerator stopFalling()
//	{
//		yield return new WaitForSeconds (0.3f);
//		PlayerController.Instance.setGravity (0);
//		PlayerController.Instance.resetVelocity ();
//		
//	}

}
