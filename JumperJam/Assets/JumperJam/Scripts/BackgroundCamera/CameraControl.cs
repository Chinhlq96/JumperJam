﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraControl : SingletonMonoBehaviour <CameraControl>
{
	[SerializeField]
	Transform BeginCameraPoint;

	//So giay camera follow khi chet
	public float followTime = 0f;

	//khoang cach giua vi tri cua camera va player
	private float distanceFromPlayer;

	//Check xem da follow chua
	public bool followed;

	//Check xem da lay distance chua
	bool checkedDistant;

	//Check xem camera da DOmove xuong player khi chet chua
	bool tweened;

	//Check xem da disable Ground khi chung' out of sign chua
	bool imageHidden;


	//GroundImage ---> phan` dat' player dung' luc bat dau
	//GroundCollider ---> Collider ground , player cham vao se chet ---> groundDeath
	[SerializeField]
	 GameObject groundImage;
	[SerializeField]
	 GameObject groundCollider;



	/// <summary>
	/// Resets the camera.
	/// </summary>
	public void ResetCamera()
	{
		transform.position = BeginCameraPoint.position;
		tweened = false;

		//Force stop 'Camera Follow Player On Death' function
		StopCoroutine ("DeathCam");

		//reset (bool)followed of camera ->  camera follow correctly after reset
		followed = false;

		//ground deactived when out of camera sight -> we have to active it again
		SetActiveGroundToTrue ();
	}

	void Update()
	{

		//Camera khong follow khi Player idle ( roi xuong nhung ko chet )
		if (PlayerController.Instance.transform.position.y>0)
		{
			if (PlayerController.Instance.transform.position.y > transform.position.y) 
			{
				this.transform.position = new Vector3 (0, PlayerController.Instance.transform.position.y, -100);
			}

		}

		//Camera follow khi player chet. Dieu kien : player da chet , camera chua follow , player ko bi groundDeath ( kieu chet tai luc bat dau)
		if (PlayerController.Instance.playerState == 0 && !followed && !PlayerController.Instance.GetComponent<PlayerController> ().groundDeath) 
		{
			StartCoroutine ("DeathCam");
		}


	}


	//Hide GroundImg and GroundCollider when out of sign
	public void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("GroundImg")) 
		{
			
			groundImage = col.gameObject;
			col.gameObject.SetActive (false);
			imageHidden = true;
		}

		if (col.CompareTag ("Ground")) 
		{
			
			groundCollider = col.gameObject;
			col.gameObject.SetActive(false);
			imageHidden = true;
		}
	}

	//Set GroundImage and GroundCollider to active
	public void SetActiveGroundToTrue()
	{
		if (imageHidden) 
		{

		groundImage.SetActive(true);
		groundCollider.SetActive (true);

			imageHidden = false;
		}
	}


	//camera follow player
	IEnumerator DeathCam()
	{	
		
		//Camera slide to player 
		if (!tweened&&PlayerController.Instance.areaDeath)
		{
			this.transform.DOMove (new Vector3 (transform.position.x, PlayerController.Instance.transform.position.y, transform.position.z), 0.2f);
			yield return new WaitForSeconds (0.2f);
			tweened = true;
		}

		//camera position = player position --> follow
		this.transform.position = new Vector3 (transform.position.x, PlayerController.Instance.transform.position.y, transform.position.z);

		yield return new WaitForSeconds(followTime);
		followed = true;
	}

}