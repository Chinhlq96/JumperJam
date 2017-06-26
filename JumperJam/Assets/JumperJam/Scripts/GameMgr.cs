using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventManager;
using UnityEngine.SceneManagement;

public class GameMgr : SingletonMonoBehaviour<GameMgr>
{
    private GameState _gameState;
	public static int totalCoin;

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
	{	randomValue = Random.Range (1, 3);
		Debug.Log (randomValue);
        gameState = GameState.Start;
		totalCoin = 0;
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

//	public void NewGame()
//	{
//		PlayerController.Instance.playerState = PlayerState.Jump;
//		PlayerController.Instance.gameObject.transform.localRotation = Quaternion.Euler (0, 0, 0);
//		PlayerController.Instance.setGravity (1);
//		PlayerController.Instance.resetPosition ();
//		PlayerController.Instance.UpdateState ();
//		PlayerController.Instance.resetVelocity ();
//		//Camera.transform.GetComponent<CamareControler> ().resetDistant ();
//		Camera.transform.GetComponent<CameraControl>().resetCamera();
//		Camera.transform.position=new Vector3(0,0,-100);
//		gameState = GameState.Wait;
//		MapMgr.Instance.GenStart ();
//		ShowTapUI();
//		UIManager.Instance.ShowPage("GamePage");
//	}
//	IEnumerator Camareee()
//	{
//		yield return new WaitForSeconds (0.1f);
//		Camera.transform.position=new Vector3(0,0,-100);
//	}




	public void LoadGameScene()
	{
			SceneManager.LoadScene ("GameIce");

			UIManager.Instance.ShowPage ("GamePage");

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

	public void AddPoint (int point)
	{
		totalCoin += point;
		var coin = PlayerPrefs.GetInt ("TotalCoin");

		PlayerPrefs.SetInt ("TotalCoin", point + coin);
	}

	public int ShowTotalPoint()
	{
		totalCoin = PlayerPrefs.GetInt ("TotalCoin");
		return totalCoin;
	}

	public void Pause() {
		gameState = GameState.Wait;
		Time.timeScale = 0;
	}

	public void UnPause() {
		if (gameState == GameState.Wait) {
			gameState = GameState.Playing;
			Time.timeScale = 1f;
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
