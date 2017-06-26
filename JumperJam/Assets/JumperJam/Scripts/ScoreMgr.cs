using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMgr : SingletonMonoBehaviour<ScoreMgr> {

	private int score;

	private int bestScore;

	[SerializeField]
	Text scoreText;
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
		}
	}

	public void SetScore (int _score) {
		if (score <= _score )
			score = _score;
	}

	public void AddScore (int _score) {
		score += _score;
	}
}
