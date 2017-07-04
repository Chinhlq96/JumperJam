using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventManager;
using UnityEngine.SceneManagement;

public class GameMgr : SingletonMonoBehaviour<GameMgr>
{

    [SerializeField]
    GameObject[] tapObjects;

	[SerializeField]
	GameObject Camera;

	[SerializeField]
	GameObject[] backgroundParts;
	//
	public List<GameObject> spawnList = new List<GameObject>();
	public List<GameObject> platformList = new List<GameObject> ();

	private int _randomValue = 0;
	public int randomValue
	{
		get { return _randomValue; }
		set { _randomValue = value; }
	}

	private GameState _gameState;
    public GameState gameState
    {	
        get { return _gameState; }
        set { _gameState = value; }
    }

    void OnEnable()
	{	
		_randomValue = Random.Range (1, 6);
        gameState = GameState.Start;
        InputMgr.TapToScreen += TapToScreen;
    }

    void OnDisable()
    {
        InputMgr.TapToScreen -= TapToScreen;
    }

    // Call from start Button
    public void NewGame()
	{   
		System.GC.Collect ();
		PlayerController.Instance.playerState = PlayerState.Jump;
		PlayerController.Instance.UpdateState ();
		gameState = GameState.Wait;
		MapMgr.Instance.GenStart ();
		ShowTapUI ();
	}

	// Reset everything to restart
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
		CameraControl.Instance.ResetCamera ();

		//reset player position/rotation/velocity/state
		PlayerController.Instance.ResetOnReplay ();

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
		CameraControl.Instance.SetActiveGroundToTrue ();

		// random new map type
		_randomValue = Random.Range (1, 6);

		// new background
		for (int i = 0; i < backgroundParts.Length ;i++) 
		{
			backgroundParts [i].GetComponent<BackgroundScroll> ().ChangeBackground ();	
		}

		// new ground
		changeGround.Instance.ChangeGround ();

		// reset maxPos ( to count score )
		PlayerController.Instance.ResetPos();

		//Reset score show when playing = 0
		Invoke ("ResetScore", 0.1f);

		// 'Tap to jump' scene ,  use invoke because i think  if  'MapMgr.Instance.GenStart ()'  called too soon it will be added to the PlatformList then despawned right at start.
		Invoke ("NewGame", 0.1f);
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
		StartCoroutine ("GameOverDelay");
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
			ShowTapUI ();
		
			PlayerController.Instance.ResetVelocity ();
			PlayerController.Instance.Jump (new Vector2 (0, 50f));
			PlayerController.Instance.SetGravity (3);
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

	IEnumerator GameOverDelay()
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
