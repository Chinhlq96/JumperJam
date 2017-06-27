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

		bestScore = PlayerPrefs.GetInt ("BestScore");
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = "" + score;
		if ((PlayerController.Instance.playerState == PlayerState.Die) && (bestScore < score)) {
			PlayerPrefs.SetInt ("BestScore", score);
			Debug.Log (PlayerPrefs.GetInt ("BestScore"));
			bestScoreImage.gameObject.SetActive (true);
		} else {
			bestScoreImage.gameObject.SetActive (false);
		}
		gameoverScoreText.text = "" + score;
		int best = PlayerPrefs.GetInt ("BestScore");
		bestScoreText.text = "" + best;
		int coins = PlayerPrefs.GetInt ("TotalCoin");
		coinsText.text = "x" + coins;
		coinsTextChar.text = "x" + coins;
	}

	public int getScore()
	{
		return score;
	}

	public void AddScore (int _score) {
		score += _score;
	}

	public void AddCoin (int point)
	{
		var coin = PlayerPrefs.GetInt ("TotalCoin");
		PlayerPrefs.SetInt ("TotalCoin", point + coin);
	}

	public void SubCoin (int point)
	{
		var coin = PlayerPrefs.GetInt ("TotalCoin");
		PlayerPrefs.SetInt ("TotalCoin", -point + coin);
	}
}
