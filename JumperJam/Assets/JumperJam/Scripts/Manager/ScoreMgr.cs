using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMgr : SingletonMonoBehaviour<ScoreMgr> 
{

	private int score;
	private int bestScore;

	[SerializeField]
	Text scoreText;
	[SerializeField]
	Text gameoverScoreText;
	[SerializeField]
	Text bestScoreText;
	[SerializeField]
	Image bestScoreImage;
	[SerializeField]
	Text coinsText;
	[SerializeField]
	Text coinsTextChar;

	[SerializeField]
	AudioSource coinSE;

	void OnEnable () 
	{
		score = 0;
		int coins = PlayerPrefs.GetInt ("TotalCoin");
		coinsTextChar.text = "x" + coins;
		coinsText.text = "x" + coins;
		bestScore = PlayerPrefs.GetInt ("BestScore");
		bestScoreText.text = "" + bestScore;
	}
	[HideInInspector]
	public bool isBest = false;
	//Update diem khi gameover
	public void UpdateGameOverScore() 
	{
		if ((PlayerController.Instance.playerState == PlayerState.Die) && (bestScore < score)) 
		{
			PlayerPrefs.SetInt ("BestScore", score);
			bestScore = PlayerPrefs.GetInt ("BestScore");
			bestScoreText.text = "" + bestScore;
			bestScoreImage.gameObject.SetActive (true);
			isBest = true;
		} else 
		{
			bestScoreImage.gameObject.SetActive (false);
			isBest = false;
		}
		gameoverScoreText.text = "" + score;
	}

	public void ResetScore()
	{	
		score = 0;
		scoreText.text = "" + 0;
	}

	public void AddScore (int _score) 
	{
		score += _score;
		scoreText.text = "" + score;
	}

	public void AddCoin (int _coin)
	{
		coinSE.Play ();

		var coin = PlayerPrefs.GetInt ("TotalCoin");
		PlayerPrefs.SetInt ("TotalCoin", _coin + coin);
		//coinsText.text = "x" + (_coin + coin);
		UpdateCoin();
	}

	public void SubCoin (int _coin)
	{
		var coin = PlayerPrefs.GetInt ("TotalCoin");
		PlayerPrefs.SetInt ("TotalCoin", coin - _coin);
	//	coinsTextChar.text = "x" + (coin - _coin);
		UpdateCoin();
	}

	void UpdateCoin()
	{
		coinsText.text = "x" + PlayerPrefs.GetInt ("TotalCoin");
		coinsTextChar.text = "x" + PlayerPrefs.GetInt ("TotalCoin");
	}
}
