using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour 
{
	[SerializeField]
	private int _point;

	public int point {
		get { return _point;}
		set { _point = point;}
	}

	[SerializeField]
	private Transform[] path;
	[SerializeField]
	private Transform firePos;
	[SerializeField]
	private float moveSpeed;
	[SerializeField]
	private bool canFire;
	[SerializeField]
	private float shootSpeed;
	private float shootDelayCounter;
	[SerializeField]
	private float delayShoot;
	private Transform target;
	private float speed;
	private int targetSelect = 1;
	[SerializeField]
	private bool isFlower;
	[SerializeField]
	private float height;
	//private Rigidbody2D enemyRg;
	void Start () 
	{
		shootDelayCounter = delayShoot;
		speed = moveSpeed;
		if (path.Length != 0)
			target = path [targetSelect];
		//enemyRg = GetComponent<Rigidbody2D> ();
	}
	
	void Update () 
	{
		if (path.Length != 0) {
			//enemyRg.velocity = new Vector2 (speed, enemyRg.velocity.y);
			//Physics2D.Linecast (transform.position, path.GetChild (nextTarget).position);
			transform.position = Vector3.MoveTowards (transform.position, target.position, Time.deltaTime * speed);
			// Neu den target thi tang them
			if (transform.position == target.position) {
				targetSelect++;
				// Neu het path roi thi quay lai
				if (targetSelect == path.Length) {
					targetSelect = 0;
				}
				target = path [targetSelect];
				// Quay lai
				transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
				if (canFire) {
					{
						StartCoroutine ("FlyFire");
					}
				}
			}
		} else {
			if (canFire) {
				// Delay shoot
				shootDelayCounter -= Time.deltaTime;
				if (shootDelayCounter <= 0) 
				{
					Fire();
					shootDelayCounter = delayShoot;
				}
			}
		}
	}
	IEnumerator FlyFire()
	{
		// Dung lai va ban
		speed = 0;
		yield return new WaitForSeconds (.5f);
		if (firePos != null) 
		{
			var fireball = ContentMgr.Instance.GetItem<FireballController> ("EnemyFireBall", firePos.position);
			fireball.Shoot (shootSpeed * transform.localScale.x);
			yield return new WaitForSeconds (.5f);
			if (fireball != null)
				fireball.Destroy ();
		}
		speed = moveSpeed;
	}

	void Fire()
	{
		if (firePos != null) 
		{
			string typeBall;
			if (isFlower)
				typeBall = "EnemyFireBall";
			else
				typeBall = "EnemySnowBall";
			var fireball = ContentMgr.Instance.GetItem<FireballController> (typeBall, firePos.position);
			if (!isFlower)
				fireball.Shoot (-shootSpeed * transform.localScale.x);
			else {
				fireball.GetComponent<Rigidbody2D> ().gravityScale = 2f;
				fireball.Shoot (-shootSpeed * transform.localScale.x, height);
				shootSpeed = -shootSpeed;
			}
	

		}
	}
}
