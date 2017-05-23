using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventManager;

public class GameMgr : SingletonMonoBehaviour<GameMgr>
{
    private GameState _gameState;
	public static int totalPoint;

    [SerializeField]
    GameObject[] tapObjects;

    public GameState gameState
    {	
        get { return _gameState; }
        set { _gameState = value; }
    }

    void OnEnable()
    {
        gameState = GameState.Start;
		totalPoint = 0;
        InputMgr.TapToScreen += TapToScreen;
    }

    void OnDisable()
    {
        InputMgr.TapToScreen -= TapToScreen;
    }


    
    /// <summary>
    /// Call from start Button
    /// </summary>
    public void NewGame()
    {
		PlayerController.Instance.playerState = PlayerState.Jump;
		PlayerController.Instance.UpdateState ();
        gameState = GameState.Wait;
        ShowTapUI();
		UIManager.Instance.ShowPage("GamePage");
    }

    public void GameOver()
    {
        //Debug.Log("gameover");
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
            PlayerController.Instance.Jump();
			PlayerController.Instance.canMoveNow = true;
        }
    }

	public void AddPoint (int Point)
	{
		totalPoint += Point;
	}

	public int ShowTotalPoint()
	{
		return totalPoint;
	}

}

public enum GameState
{
    Start,
    Wait,
    Playing,
    GameOver
}
