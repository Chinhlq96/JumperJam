using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventManager;

public class GameMgr : SingletonMonoBehaviour<GameMgr>
{
    private GameState _gameState;

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
        gameState = GameState.Wait;
        ShowTapUI();
    }

    public void GameOver()
    {
        Debug.Log("gameover");
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

}

public enum GameState
{
    Start,
    Wait,
    Playing,
    GameOver
}
