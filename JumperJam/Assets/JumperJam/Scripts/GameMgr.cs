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
		PlayerController.Instance.playerState = PlayerState.Jump;
		PlayerController.Instance.UpdateState ();
        gameState = GameState.Wait;
		MapMgr.Instance.GenStart ();
        ShowTapUI();
    }


	public void LoadGameScene()
	{
		SceneManager.LoadScene ("GameIce");
		UIManager.Instance.ShowPage ("GamePage");
	}

    public void GameOver()
    {

		UIManager.Instance.ShowPage("GameOverPage");
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
          	PlayerController.Instance.Jump();

			PlayerController.Instance.canMoveNow = true;
        }
		//PlayerController.Instance.Jump();
    }



	public void Pause() {
		gameState = GameState.Pause;
		Time.timeScale = 0;
	}

	public void UnPause() {
		if (gameState == GameState.Pause) {
			gameState = GameState.Playing;
			Time.timeScale = 1f;
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
