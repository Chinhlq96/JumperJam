using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventManager;
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

	public Transform pos;
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


    void OnEnable()
    {
        playerState = PlayerState.Idle;
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


    public void OnTriggerEnter2D(Collider2D col)
    {
		

        if (col.CompareTag("Platform"))
        {
            Jump();
        }
        if (col.CompareTag("Enemy"))
        {
            playerState = PlayerState.Die;
            Die();
        }
		//PlatformG => Platform generator
		if(col.CompareTag("PlatformG"))
		{
			
			if (RG.velocity.y <= 0 && (!col.gameObject.GetComponent<Check> ().getStepped ())) 
			{	
				
				this.PostEvent (EventID.GenMap, this);
				Jump ();
				col.gameObject.GetComponent<Check> ().setStepped (true);

			} else
				Jump ();



		}
		if (col.CompareTag ("DeathArea")) 
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
		float moveHorizontal = Input.GetAxis ("Horizontal");
		Vector3 movement = new Vector3 (moveHorizontal,0,0);
		transform.position += movement * 20f * Time.deltaTime;

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
