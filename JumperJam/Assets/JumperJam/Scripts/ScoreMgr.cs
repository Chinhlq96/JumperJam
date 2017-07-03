using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMgr : SingletonMonoBehaviour<ScoreMgr> {

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
	// Use this for initialization
	void Start () {
		score = 0;

		int coins = PlayerPrefs.GetInt ("TotalCoin");
		coinsTextChar.text = "x" + coins;
		coinsText.text = "x" + coins;
		bestScore = PlayerPrefs.GetInt ("BestScore");
		bestScoreText.text = "" + bestScore;
	}

	public void UpdateGameOverScore() {
		if ((PlayerController.Instance.playerState == PlayerState.Die) && (bestScore < score)) {
			PlayerPrefs.SetInt ("BestScore", score);
			bestScoreImage.gameObject.SetActive (true);
		} else {
			bestScoreImage.gameObject.SetActive (false);
		}
		gameoverScoreText.text = "" + score;
	}
	public int getScore()
	{
		return score;
	}

	public void resetScore()
	{	
		score = 0;
		scoreText.text = "" + 0;
	
	}

	public void AddScore (int _score) {
		score += _score;
		scoreText.text = "" + score;
	}

	public void AddCoin (int _coin)
	{
		var coin = PlayerPrefs.GetInt ("TotalCoin");
		PlayerPrefs.SetInt ("TotalCoin", _coin + coin);
		//coinsText.text = "x" + (_coin + coin);
		printCoin();
	}

	public void SubCoin (int _coin)
	{
		var coin = PlayerPrefs.GetInt ("TotalCoin");
		PlayerPrefs.SetInt ("TotalCoin", coin - _coin);
	//	coinsTextChar.text = "x" + (coin - _coin);
		printCoin();
	}

	void printCoin()
	{
		coinsText.text = "x" + PlayerPrefs.GetInt ("TotalCoin");
		coinsTextChar.text = "x" + PlayerPrefs.GetInt ("TotalCoin");
	}
}
