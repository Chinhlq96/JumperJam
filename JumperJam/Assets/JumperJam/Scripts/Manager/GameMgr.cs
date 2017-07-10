﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventManager;
using UnityEngine.SceneManagement;

public class GameMgr : SingletonMonoBehaviour<GameMgr>
{

    [SerializeField]
    GameObject[] tapObjects;

	[SerializeField]
	AudioSource bestScoreSE;

	[SerializeField]
	GameObject Camera;

	[SerializeField]
	GameObject[] backgroundParts;

	[SerializeField]
	GameObject deathBox;
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

	//Despawn Mob
	void DespawnMob()
	{
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
	}


	//Despawn platform
	void DespawnPlatform()
	{
		foreach (var item in platformList)
		{
			if (item.gameObject.activeSelf)
				ContentMgr.Instance.Despaw (item);
		}
		//Clear platform list
		platformList.Clear ();
	}

	// Reset everything to restart
	public void LoadGameScene()
	{


		DespawnMob();
		DespawnPlatform ();

		StopCoroutine ("GameOverDelay");
		//reset Camera position
		CameraControl.Instance.ResetCamera ();

		//reset player position/rotation/velocity/state
		PlayerController.Instance.ResetOnReplay ();

		//Enter GamePage
		UIManager.Instance.ShowPage ("GamePage");


		deathBox.SetActive (true);

		// random new map type
		_randomValue = Random.Range (1, 6);

		// new background
		for (int i = 0; i < backgroundParts.Length ;i++) 
		{
			backgroundParts [i].GetComponent<BackgroundScroll> ().ChangeBackground ();	
		}

		// new ground
		ChangeGround.Instance.ChangeGroundStyle ();

		//Reset score show when playing = 0
		Invoke ("ResetScore", 0.1f);

		// 'Tap to jump' scene ,  use invoke because i think  if  'MapMgr.Instance.GenStart ()'  called too soon it will be added to the PlatformList then despawned right at start.
		Invoke ("NewGame", 0.1f);
	}

	public void Exit()
	{
		//prevent double dead
		deathBox.SetActive (false);

		PlayerController.Instance.ResetOnReplay ();


		//Stop the coroutine that show Game Over Canvas when quick quit
		StopCoroutine ("GameOverDelay");

	
		UIManager.Instance.ShowPage ("StartPage");
	}

	public void ResetScore()
	{
		ScoreMgr.Instance.ResetScore ();
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
			PlayerController.Instance.Jump (new Vector2 (0, 40f));
			PlayerController.Instance.SetGravity (2);
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
		if (ScoreMgr.Instance.isBest) {
			bestScoreSE.Play ();
		}
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