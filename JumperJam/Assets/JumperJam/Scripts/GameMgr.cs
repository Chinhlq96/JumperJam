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
