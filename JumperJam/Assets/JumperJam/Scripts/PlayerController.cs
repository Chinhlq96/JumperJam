using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventManager;
using DG.Tweening;

public class PlayerController : SingletonMonoBehaviour<PlayerController>
{
	// force add to jump
	[SerializeField]
	Vector2 force;


	// sprite renderer of player
	[SerializeField]
	SpriteRenderer playerSR;


	// RigidBody
	[SerializeField]
	Rigidbody2D RG;


	// Transform of player
	[SerializeField]
	Transform playerTrans;


	[SerializeField]
	private Transform[] backGround;	

	[SerializeField]
	private Transform spawnPlayerPoint;

	[SerializeField]
	private float moveSpeed;

	[SerializeField]
	GameObject dashTrail;

	private float moveX;

	//private bool notTouchOne;


	//control when to shake
	private int count = 0;

	//player can move horizontal or not
	private bool _canMoveNow;
	public bool canMoveNow
	{
		get { return _canMoveNow; }
		set { _canMoveNow = value; }
	}

	//Check ground death at start ( player khong chet khi cham ground luc dau )
	private bool groundTouched;


	private bool _groundDeath;
	public bool groundDeath
	{
		get { return _groundDeath; }
		set { _groundDeath = value; }
	}


	// 1 die 2 idle 3 jump
	private PlayerState _playerState;
	public PlayerState playerState
	{
		get { return _playerState; }
		set
		{
			_playerState = value;
			UpdateState();
		}
	}


	// Vi tri Player de tinh diem
	private Vector3 startPos;
	private Vector3 maxPos;


	// 3 loai sprite : death idle jump
	public Sprite[] aniSprites;



	void OnEnable()
	{
		//init startPos, maxPos for scoreMgr
		startPos = transform.position;
		maxPos = startPos;

		playerState = PlayerState.Idle;
		canMoveNow = false;
	}

	/// Update state while change state
	public void UpdateState()
	{
		int index = (int)playerState;
		playerSR.sprite = aniSprites [index];
		playerTrans.localRotation = Quaternion.Euler (new Vector3 (0, 0, playerState == PlayerState.Die ? -30 : 0));
	}

	public void ResetPosition()
	{
		this.transform.position = spawnPlayerPoint.position;
	}

	public void ResetVelocity()
	{
		RG.velocity = new Vector2 (0, 0);
	}

	public void SetGravity(int value)
	{
		RG.gravityScale = value;
	}


	public void ResetOnReplay()
	{
		
		this.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
		ResetVelocity ();
		SetGravity (0);
		ResetPosition ();
		playerState = PlayerState.Jump;
	}


	void Update ()
	{
		//move player with phone accelerater
		if (canMoveNow == true && GameMgr.Instance.gameState != GameState.Pause) 
		{
			Vector2 pos = transform.position;
			pos += new Vector2 (Input.acceleration.x, 0) * moveSpeed * 0.5f;
			transform.position = pos;
			//Move in editor
			moveX = Input.GetAxisRaw ("Horizontal");
			Vector2 directionX = new Vector2 (moveX, 0);
			MoveX (directionX);
		}



		//teleport player to right/left if over go
		//World position -> Screen position  ( from random world number to 0~Screen.Width number )
		Vector3 scrPos = Camera.main.WorldToScreenPoint (transform.position);
		if (scrPos.x < -10)
			TeleportToRight (scrPos);
		if (scrPos.x > Screen.width + 10)
			TeleportToLeft (scrPos);



		// Neu chua vuot qua duoc vi tri qua nhat thi khong cong diem
		if ((transform.position.y - maxPos.y > .5f) && (playerState != PlayerState.Die)) 
		{
			ScoreMgr.Instance.AddScore (Mathf.RoundToInt (Mathf.Abs (transform.position.y - maxPos.y)));
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


	//Move player horizontaly
	void MoveX (Vector2 directionX)
	{
		Vector2 pos = transform.position;
		pos += directionX * moveSpeed * 1.5f ;
		transform.position = pos;
	}



	public void OnTriggerEnter2D(Collider2D col)
	{
		//2 loai Platform voi force khac nhau
		if ((transform.position.y > col.transform.position.y + 0.3f)&&(col.CompareTag ("Platform") || col.CompareTag ("SPlatform"))) 
		{
			if (col.CompareTag ("SPlatform")) 
			{
				Jump (new Vector2 (0, 80f));
				StartCoroutine ("Wait");
			} 

			else

			Jump (force);


		}


		// Neu chet thi k xet va cham nua
		if (playerState != PlayerState.Die) { 
			if (col.CompareTag ("Enemy") || col.CompareTag ("Bullet")) 
			{
				// Neu player tren enemy thi giet va tang diem
				if ((transform.position.y > col.transform.position.y + 0.7f) && !col.CompareTag ("Bullet")) 
				{
					RG.velocity = new Vector2 (0, 0);
					RG.AddForce (force * 1f, ForceMode2D.Impulse);
					try 
					{
						ContentMgr.Instance.Despaw (col.gameObject.transform.parent.gameObject);
					} catch 
					{
						ContentMgr.Instance.Despaw (col.gameObject);
					}
					ScoreMgr.Instance.AddScore (col.gameObject.GetComponent<EnemyPatrol> ().point);
					ContentMgr.Instance.GetItem ("DeathParticle", col.transform.position);
				} else 
				{
					count++;
					Die ();
					Shake ();
				}
			}
			if (col.CompareTag ("UndeadEnemy")) 
			{
				count++;
				Die ();
				Shake ();
			}
			if (col.CompareTag ("Ground")) 
			{
				if (groundTouched) 
				{
					Die ();
					count++;
					Shake ();
					groundDeath = true;
					//Khi chet ngay o doan dau thi reset luon
					count = 0;
				}
			}
		}

		if (col.CompareTag ("DeathArea")) 
		{
			if (playerState != PlayerState.Die) 
			{
				Die ();
			}
			count++;
			if (count == 2) {
				Shake ();
				count = 0;
			}
		}
	}


	IEnumerator Wait() 
	{
		yield return new WaitForSeconds (.9f);
		dashTrail.GetComponent<DashTrail>().mbEnabled = false;
	}


	// Luc dau cham ground ko bi chet ---> sau do roi xuong cham ground moi chet
	public void OnTriggerExit2D(Collider2D col)
	{
		if (col.CompareTag ("Ground")) 
			groundTouched = true;
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



	public void Jump(Vector2 force)
	{
		if (RG.velocity.y <= 0)
		{
			if (playerState == PlayerState.Die) return;
			RG.velocity = new Vector2(0, 0);
			if (force.y > 50)
				dashTrail.GetComponent<DashTrail> ().mbEnabled = true;
			RG.AddForce(force, ForceMode2D.Impulse);
			playerState = PlayerState.Jump;
		}
		//notTouchOne = true;
	}



	void Die()
	{
		RG.velocity = new Vector2(0, -5);
		canMoveNow = false;
		playerState = PlayerState.Die;
		ScoreMgr.Instance.UpdateGameOverScore ();
		MapMgr.Instance.ResetDifficult ();
		GameMgr.Instance.GameOver();
    }


	void Shake() 
	{
		backGround [0].GetComponent<CamShake> ().MinorShake (100);
		backGround [1].GetComponent<CamShake> ().MinorShake (40);
		backGround [2].GetComponent<CamShake> ().MinorShake (40);
	}


	public void ResetPos() 
	{
		maxPos = startPos;
	}
}

public enum PlayerState
{
	Die = 0, Idle, Jump
}
