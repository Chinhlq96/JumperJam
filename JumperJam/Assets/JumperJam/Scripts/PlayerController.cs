using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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


    /// <summary>
    /// 1 die 
    /// 2 idle
    /// 3 jump
    /// </summary>
    [SerializeField]
    Sprite[] aniSprites;

	//for testing on editor
	public float moveSpeed;
	private float moveX;
	private bool notTouchOne;
	//

    private PlayerState _playerState;

	//player can move horizontal or not
	public bool canMoveNow;

    public PlayerState playerState
    {
        get { return _playerState; }
        set
        {
            _playerState = value;
            UpdateState();
        }
    }



    void OnEnable()
    {
		DOTween.Init();
        playerState = PlayerState.Idle;
		canMoveNow = false;
		notTouchOne = true;
    }

    /// <summary>
    /// Update state while change state
    /// </summary>
    void UpdateState()
    {
        int index = (int)playerState;
        playerSR.sprite = aniSprites[index];
        playerTrans.localRotation = Quaternion.Euler(new Vector3(0, 0, playerState == PlayerState.Die ? -30 : 0));
    }

	void Update ()
	{
		//move player with phone accelerater
		if (canMoveNow == true ) {
			Vector2 pos = transform.position;
			pos += new Vector2 (Input.acceleration.x, 0) * moveSpeed * 0.1f;
			transform.position = pos;

			///

			moveX = Input.GetAxisRaw ("Horizontal");
			Vector2 directionX = new Vector2 (moveX, 0);
			MoveX (directionX);
		}

	}

	void MoveX (Vector2 directionX)
	{
		Vector2 pos = transform.position;
		pos += directionX * moveSpeed ;
		transform.position = pos;
	}

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Platform"))
        {
			Jump ();
			//make platform bounce
			if (transform.position.y > col.transform.position.y + 0.2f) 
			{
				col.transform.DOLocalMoveY (col.transform.position.y - 0.3f, 0.1f).OnComplete (() => {
					col.transform.DOLocalMoveY (col.transform.position.y + 0.3f, 0.1f);
				});
			}
			if (notTouchOne == true) 
			{
				if (moveX > 0 || Input.acceleration.x > 0) {
					transform.DORotate (new Vector3 (0, 0, -360), 0.65f, RotateMode.FastBeyond360);
				} else if (moveX < 0 || Input.acceleration.x < 0){
					transform.DORotate (new Vector3 (0, 0, +360), 0.65f, RotateMode.FastBeyond360);
				}
				notTouchOne = false;
			}
        }
        if (col.CompareTag("Enemy"))
        {
            playerState = PlayerState.Die;
            Die();
        }
       
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") && playerState == PlayerState.Die)
        {
            GameMgr.Instance.GameOver();
        }
    }

    public void Jump()
    {
        if (RG.velocity.y <= 0)
        {
			notTouchOne = true;
            if (playerState == PlayerState.Die) return;
            RG.velocity = new Vector2(0, 0);
            RG.AddForce(force, ForceMode2D.Impulse);
            playerState = PlayerState.Jump;
        }
    }

    void Die()
    {
        RG.velocity = new Vector2(0, -5);
    }

    void FixedUpdate()
    {
        if (RG.velocity.y < 0 && playerState != PlayerState.Die)
        {
            playerState = PlayerState.Idle;
        }

    }

}

public enum PlayerState
{
    Die = 0, Idle, Jump
}
