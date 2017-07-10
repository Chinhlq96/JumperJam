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

	[SerializeField]
	AudioSource sfx;
	[SerializeField]
	AudioClip deathSE;
	[SerializeField]
	AudioClip jumpSE;
	[SerializeField]
	AudioClip killSE;
	[SerializeField]
	AudioClip killedSE;
	[SerializeField]
	AudioClip boostSE;


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
	Vector2 addForce;

	[SerializeField]
    private Transform[] background;	

	[SerializeField]
	private Transform spawnPlayerPoint;

	[SerializeField]
	private float moveSpeed;
    [SerializeField]
    private float editorMoveSpeed;

	[SerializeField]
	GameObject dashTrail;

	private float moveX;

	//private bool notTouchOne;


	//control when to shake
	private int _count = 0;
	public int count
	{
		get { return  _count; }
		set {  _count = value; }
	}


	//player can move horizontal or not
	private bool _canMoveNow;
	public bool canMoveNow
	{
		get { return _canMoveNow; }
		set { _canMoveNow = value; }
	}

	//Check ground death at start ( player khong chet khi cham ground luc dau )
    [HideInInspector]
	public bool groundTouched;


	private bool _groundDeath;
	public bool groundDeath
	{
		get { return _groundDeath; }
		set { _groundDeath = value; }
	}

	private bool _areaDeath;
	public bool areaDeath
	{
		get { return _areaDeath; }
		set { _areaDeath = value; }
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
	private ParticleSystem particle;
	private bool isDespawed;
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

		//invu ko co sprites dac biet
		if(index<=2)
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
	

		//prevent player spinning when restart
		RG.angularVelocity = 0f;
	}

	public void SetGravity(int value)
	{
		RG.gravityScale = value;
	}


	//Reset
	public void ResetOnReplay()
	{
		//at start if groundTouched is still true --> player instanly death as groundDeath
		groundTouched = false;
		ResetVelocity ();
		this.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
		SetGravity (0);
		ResetPosition ();
		playerState = PlayerState.Jump;

		canMoveNow = false;

		//Count to shake reset
		count = 0;

		//reset (bool)groundDeath (Camera wont follow player if it died by ground)
		// reset areaDeath
		groundDeath = false;
		areaDeath = false;

		// reset maxPos ( to count score )
		ResetMaxPos();
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
        pos += directionX * editorMoveSpeed * 1.5f ;
		transform.position = pos;
	}




	/// <summary>
	/// Xu li death va kill enemy
	/// </summary>

	public void OnTriggerEnter2D(Collider2D col)
	{
		//2 loai Platform voi force khac nhau
		if ((transform.position.y > col.transform.position.y + 0.3f)&&(col.CompareTag ("Platform") || col.CompareTag ("SPlatform"))) 
		{
			if (col.CompareTag ("SPlatform")) 
			{
				Jump (addForce);
				StartCoroutine ("Wait");
			} 

			else

			Jump (force);


		}


		// Neu chet thi k xet va cham nua
		// Neu invu ko xet
		if (playerState != PlayerState.Die )
		if(playerState!= PlayerState.Invu)
		{ 
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

					PlaySfx (killSE);


					isDespawed = false;
					particle = ContentMgr.Instance.GetItem<ParticleSystem> ("DeathParticle", col.transform.position);
					if (!isDespawed)
						StartCoroutine ("DespawAfter", particle.duration);
					
				} else 
				{
					count++;
					Die ();
					PlaySfx (killedSE);

					Shake ();
				}
			}
			if (col.CompareTag ("UndeadEnemy")) 
			{
				count++;
				Die ();
				PlaySfx (killedSE);

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

					PlaySfx (deathSE);

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
				areaDeath = true;
			}
			count++;
			if (count == 2) {
				Shake ();
				count = 0;
				PlaySfx (deathSE);
		
			}
		}
	}

	void PlaySfx(AudioClip clip) {
		sfx.clip = clip;
		sfx.Play ();
	}
    //Wait for disable dash effect and invu state
	IEnumerator Wait() 
	{
		yield return new WaitForSeconds (.9f);
		dashTrail.GetComponent<DashTrail>().mbEnabled = false;

		//invu -> jump state
		if(playerState== PlayerState.Invu)
		playerState = PlayerState.Jump;
	}

	IEnumerator DespawAfter(float duration) 
	{
		yield return new WaitForSeconds (duration);
		ContentMgr.Instance.Despaw (particle.gameObject);
		isDespawed = true;
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
			DashEnabled ();
			
			PlaySfx (jumpSE);
				

			RG.AddForce(force, ForceMode2D.Impulse);
			playerState = PlayerState.Jump;
		}
		//notTouchOne = true;
	}

	public void DashEnabled()
	{
		dashTrail.GetComponent<DashTrail> ().mbEnabled = true;
	}

	public void DashWaitedDisable()
	{
		StartCoroutine ("Wait");
	}

	public void InvuState()
	{
		PlaySfx (boostSE);

		playerState = PlayerState.Invu;
	}


	void Die()
	{
		RG.velocity = new Vector2(0, -5);
		canMoveNow = false;
		playerState = PlayerState.Die;
		MapMgr.Instance.ResetDifficult ();
		GameMgr.Instance.GameOver();
		ScoreMgr.Instance.UpdateGameOverScore ();

    }

	void Shake() 
	{
		background [0].GetComponent<CamShake> ().MinorShake (100);
		background [1].GetComponent<CamShake> ().MinorShake (40);
		background [2].GetComponent<CamShake> ().MinorShake (40);
	}


	public void ResetMaxPos() 
	{
		maxPos = startPos;
	}
}

public enum PlayerState
{
	Die = 0, Idle, Jump, Invu
}
