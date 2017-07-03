using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventManager;
using DG.Tweening;

public class PlayerController : SingletonMonoBehaviour<PlayerController>
{

	/// <summary>
	/// force add to jump
	/// </summary>
	[SerializeField]
	Vector2 force;

	/// <summary>
	/// sprite renderer of player
	/// </summary>
	[SerializeField]
	SpriteRenderer playerSR;

	/// <summary>
	/// RigidBody
	/// </summary>
	[SerializeField]
	Rigidbody2D RG;

	/// <summary>
	/// Transform of player
	/// </summary>
	[SerializeField]
	Transform playerTrans;

	[SerializeField]
	private Transform[] backGround;	

	/// <summary>
	/// 1 die 
	/// 2 idle
	/// 3 jump
	/// </summary>

	public Sprite[] aniSprites;


	public Transform pos;
	public Transform spawnPlayerPoint;
	//for testing on editor
	public float moveSpeed;
	private float moveX;
	private bool notTouchOne;
	//
	[SerializeField]
	private int count = 0;
	public PlayerState _playerState;

	//player can move horizontal or not
	public bool canMoveNow;

	//Check ground death at start
	private bool groundTouched;
	public bool groundDeath;



	public PlayerState playerState
	{
		get { return _playerState; }
		set
		{
			_playerState = value;
			UpdateState();
		}
	}



	public Vector3 startPos;
	public Vector3 maxPos;

	void OnEnable()
	{
		//transform.GetChild(0).GetComponent<Collider2D> ().enabled = true;
		startPos = transform.position;
		maxPos = startPos;
		DOTween.Init();
		playerState = PlayerState.Idle;
		canMoveNow = false;
		notTouchOne = true;
	}

	/// <summary>
	/// Update state while change state
	/// </summary>
	public void UpdateState()
	{
		int index = (int)playerState;
		playerSR.sprite = aniSprites[index];
		playerTrans.localRotation = Quaternion.Euler(new Vector3(0, 0, playerState == PlayerState.Die ? -30 : 0));
	}

	public void resetPosition()
	{
		this.transform.position = spawnPlayerPoint.position;
	}

	public void resetVelocity()
	{
		RG.velocity = new Vector2(0,0);
	}

	public void setGravity(int value)
	{
		RG.gravityScale = value;
	}

	public void resetOnReplay()
	{
		//playerTrans.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
		this.transform.rotation = Quaternion.Euler(new Vector3 (0, 0, 0));

		resetVelocity ();
		setGravity (0);
		resetPosition ();
		//Invoke("resetPosition",5f);
		playerState = PlayerState.Jump;

	}

	void Update () {
		//move player with phone accelerater

		if (canMoveNow == true && GameMgr.Instance.gameState!=GameState.Pause) {
			Vector2 pos = transform.position;
			pos += new Vector2 (Input.acceleration.x, 0) * moveSpeed * 0.5f;
			transform.position = pos;


			///

			moveX = Input.GetAxisRaw ("Horizontal");
			Vector2 directionX = new Vector2 (moveX, 0);
			MoveX (directionX);


		}

		//teleport player to right/left if over go

		//World position -> Screen position  ( from random world number to 0~Screen.Width number )
		Vector3 scrPos = Camera.main.WorldToScreenPoint(transform.position);
		if (scrPos.x < -10)
			TeleportToRight (scrPos);
		if (scrPos.x > Screen.width + 10)
			TeleportToLeft (scrPos);


		// Moi khi khoang cach tang len 5 thi cong 5 diem neu chua vuot qua duoc vi tri qua nhat thi khong cong diem
		if ((Mathf.RoundToInt (Mathf.Abs (transform.position.y - startPos.y)) % 5 == 0) && (transform.position.y > maxPos.y))
			ScoreMgr.Instance.AddScore (5);
		if (transform.position.y > maxPos.y) {
			maxPos = transform.position;
		}
	}


	void TeleportToRight(Vector3 scrPos)
	{
		// position we want to move to ( Screen position )
		Vector3 goalScrPos = new Vector3 (Screen.width + 10, scrPos.y, scrPos.z);
		// Convert above position to world position
		Vector3 targetWorldPos = Camera.main.ScreenToWorldPoint (goalScrPos);
		// then assign to player position 
		transform.position = targetWorldPos;
	}

	void TeleportToLeft (Vector3 scrPos)
	{
		// position we want to move to ( Screen position )
		Vector3 goalScrPos = new Vector3 (-10, scrPos.y, scrPos.z);
		// Convert above position to world position
		Vector3 targetScrPos = Camera.main.ScreenToWorldPoint (goalScrPos);
		// then assign to player position 
		transform.position = targetScrPos;

	}


	void MoveX (Vector2 directionX)
	{
		Vector2 pos = transform.position;
		pos += directionX * moveSpeed * 1.5f ;
		transform.position = pos;
	}


	public void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("Platform") || col.CompareTag ("SPlatform")) {
			if (col.CompareTag ("SPlatform")) {
				Jump (new Vector2 (0, 80f));

			} else
				Jump (force);

			//make platform bounce



			//			if (notTouchOne == true) 
			//			{
			//				Jump (force);
			//				transform.eulerAngles = new Vector3 (0,0,0);
			//				if (moveX > 0.5f || Input.acceleration.x > 0.5f) {
			//					transform.DORotate (new Vector3 (0, 0, -360), 0.5f, RotateMode.FastBeyond360);
			//				} else if (moveX < 0.5f || Input.acceleration.x < 0.5f){
			//					transform.DORotate (new Vector3 (0, 0, +360), 0.5f, RotateMode.FastBeyond360);
			//				}
			//				notTouchOne = false;
			//			}
		}


		if (playerState != PlayerState.Die) { 
			if (col.CompareTag ("Enemy") || col.CompareTag ("Bullet")) {
				if ((transform.position.y > col.transform.position.y + 0.7f) && !col.CompareTag ("Bullet")) {
					RG.velocity = new Vector2 (0, 0);
					RG.AddForce (force * 1f, ForceMode2D.Impulse);
					col.gameObject.SetActive (false);
					ScoreMgr.Instance.AddScore (col.gameObject.GetComponent<EnemyPatrol> ().point);
				} else {
					playerState = PlayerState.Die;
					count++;
					Die ();
					Shake ();
					Debug.Log (3);
				}

			}
			if (col.CompareTag ("UndeadEnemy")) {
				playerState = PlayerState.Die;
				count++;
				Die ();
				Shake ();
				Debug.Log (2);
			}
			if (col.CompareTag ("Ground")) {
				if (groundTouched) {
					playerState = PlayerState.Die;
					Die ();
					count++;
					Shake ();
					Debug.Log (1);
					groundDeath = true;
					//Khi chet ngay o doan dau thi reset luon
					count = 0;
				}
			}
		}

		if (col.CompareTag ("DeathArea")) {
			if (playerState != PlayerState.Die) 
			{
				playerState = PlayerState.Die;
				Die ();
			}
			count++;
			if (count == 2) {
				Shake ();
				Debug.Log (4);
				count = 0;
			}
		}

	}

	public void OnTriggerExit2D(Collider2D col)
	{
		if (col.CompareTag ("Ground")) {
			groundTouched = true;
		}
	}

	IEnumerator Bound(Collider2D col)
	{
		if (transform.position.y > col.transform.position.y) 
		{
			col.transform.DOLocalMoveY (col.transform.localPosition.y - 0.3f, 0.1f).OnComplete (() => {
				col.transform.DOLocalMoveY (col.transform.localPosition.y + 0.3f, 0.1f);
			});
		}
		yield return new WaitForSeconds (0.1f);
	}

//	public void OnCollisionEnter2D(Collision2D col)
//	{
//		if (col.gameObject.CompareTag("Ground") && playerState == PlayerState.Die)
//		{
//			GameMgr.Instance.GameOver();
//		}
//	}

	public void Jump(Vector2 force)
	{
		//	Debug.Log (RG.velocity.y);
		if (RG.velocity.y <= 0)
		{
			if (playerState == PlayerState.Die) return;
			RG.velocity = new Vector2(0, 0);
			RG.AddForce(force, ForceMode2D.Impulse);
			playerState = PlayerState.Jump;
		}
		notTouchOne = true;
	}

	void Die()
	{
		//transform.GetChild(0).GetComponent<Collider2D> ().enabled = false;


		RG.velocity = new Vector2(0, -5);
		canMoveNow = false;

		playerState = PlayerState.Die;
		ScoreMgr.Instance.UpdateGameOverScore ();
		MapMgr.Instance.resetDifficult ();
		GameMgr.Instance.GameOver();

//		camera.GetComponent<CameraControl> ().followOnDeath ();
    }

	void Shake() {
		backGround [0].GetComponent<CamShake> ().MinorShake (100);
		backGround [1].GetComponent<CamShake> ().MinorShake (40);
		backGround [2].GetComponent<CamShake> ().MinorShake (40);
	}
//void FixedUpdate()	
//
//    {
//		Debug.Log ("run");
//		if (canMoveNow == true && GameMgr.Instance.gameState != GameState.Pause) {
//			float moveHorizontal = Input.GetAxis ("Horizontal");
//			Vector3 movement = new Vector3 (moveHorizontal, 0, 0);
//			transform.position += movement * 20f * Time.deltaTime;
//
//
//			if (RG.velocity.y < 0 && playerState != PlayerState.Die) {
//				playerState = PlayerState.Idle;
//			}
//		}
//    }


}

public enum PlayerState
{
	Die = 0, Idle, Jump
}
