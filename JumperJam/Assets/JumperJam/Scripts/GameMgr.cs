using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventManager;
using UnityEngine.SceneManagement;

public class GameMgr : SingletonMonoBehaviour<GameMgr>
{



   	private GameState _gameState;

    [SerializeField]
    GameObject[] tapObjects;

	[SerializeField]
	GameObject Camera;

	[SerializeField]
	GameObject[] backgroundParts;


	//
	public List<GameObject> spawnList = new List<GameObject>();
	public List<GameObject> platformList = new List<GameObject> ();




	public int randomValue = 0;

    public GameState gameState
    {	
        get { return _gameState; }
        set { _gameState = value; }
    }


    void OnEnable()

	{	
		randomValue = Random.Range (1, 6);
		//Debug.Log (randomValue);
        gameState = GameState.Start;
        InputMgr.TapToScreen += TapToScreen;

    }

    void OnDisable()
    {
        InputMgr.TapToScreen -= TapToScreen;
    }

    /// Call from start Button
    /// </summary>
    public void NewGame()
    {   
		System.GC.Collect ();
		PlayerController.Instance.playerState = PlayerState.Jump;
		PlayerController.Instance.UpdateState ();
        gameState = GameState.Wait;
		MapMgr.Instance.GenStart ();
        ShowTapUI();
    }




	/// <summary>
	/// Reset everything to restart
	/// </summary>
	public void LoadGameScene()
	{
		//Despawn Mob
		foreach (GameObject element in spawnList) 
		{
			if (element.gameObject.activeSelf)
				try
				{
					ContentMgr.Instance.Despaw (element.transform.parent.gameObject);
				}
				catch
				{
					ContentMgr.Instance.Despaw (element);
				}
		
		}
		//Clear Mob List
		spawnList.Clear ();


		//Despawn platform
		foreach (var item in platformList)
		{
			if (item.gameObject.activeSelf)
				ContentMgr.Instance.Despaw (item);

		}
		//Clear platform list
		platformList.Clear ();


		//reset Camera position
		CameraControl.Instance.resetCamera ();

		//reset player position/rotation/velocity/state
		PlayerController.Instance.resetOnReplay ();

		//Enter GamePage
		UIManager.Instance.ShowPage ("GamePage");

		//Force stop 'Camera Follow Player On Death' function
		CameraControl.Instance.StopCoroutine ("deathCam");

		//Cant move
		PlayerController.Instance.canMoveNow = false;

		//reset (bool)followed of camera ->  camera follow correctly after reset
		CameraControl.Instance.followed = false;

		//reset (bool)groundDeath (Camera wont follow player if it died by ground)
		PlayerController.Instance.groundDeath = false;

		//ground deactived when out of camera sight -> we have to active it again
		CameraControl.Instance.setActiveGroundToTrue ();

		// random new map type
		randomValue = Random.Range (1, 6);


		// new background
		for (int i = 0; i < backgroundParts.Length ;i++) 
		{
			backgroundParts [i].GetComponent<BackgroundScroll> ().ChangeBackground ();	
		}


		// new ground
		changeGround.Instance.ChangeGround ();


		// reset maxPos ( to count score )
		PlayerController.Instance.maxPos = PlayerController.Instance.startPos;


		//Reset score show when playing = 0
		Invoke ("ResetScore", 0.1f);

		// 'Tap to jump' scene ,  use invoke because i think  if  'MapMgr.Instance.GenStart ()'  called too soon it will be added to the PlatformList then despawned right at start.
		Invoke ("NewGame", 0.1f);


		//SceneManager.LoadScene ("GameIce");
		//UIManager.Instance.ShowPage ("GamePage");
	}

	public void Exit()
	{
		UIManager.Instance.ShowPage ("StartPage");
	}



	public void ResetScore()
	{
		ScoreMgr.Instance.resetScore ();

	}



    public void GameOver()
    {
		
		StartCoroutine ("gameOver");
    }
		

    void ShowTapUI()
    {
        foreach (var item in tapObjects)
        {
            item.SetActive(gameState == GameState.Wait);
        }
    }

    void TapToScreen()
    {
        if (gameState == GameState.Wait)
        {
            gameState = GameState.Playing;
            ShowTapUI();
		
			PlayerController.Instance.resetVelocity ();
			PlayerController.Instance.Jump (new Vector2 (0, 50f));
			PlayerController.Instance.setGravity (3);

			PlayerController.Instance.canMoveNow = true;
        }
		//PlayerController.Instance.Jump();
    }



	public void Pause() 
	{
		gameState = GameState.Pause;
		Time.timeScale = 0;
	}

	public void UnPause() 
	{
		if (gameState == GameState.Pause) 
		{
			gameState = GameState.Playing;
			Time.timeScale = 1f;
		}
	}

	IEnumerator gameOver()
	{
		yield return new WaitForSeconds (0.7f);
		UIManager.Instance.ShowPage("GameOverPage");
	}

}

public enum GameState
{
    Start=0,
    Wait,
	Pause,
    Playing,
    GameOver
}
