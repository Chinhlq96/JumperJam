using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreItem : MonoBehaviour {

    [SerializeField]
    Image avatar;

    [SerializeField]
    Text userName;

    [SerializeField]
    Text score;


    public void SetData(ScoreDataForLeaderBoard scoreData)
    {
        Image profile = avatar.GetComponent<Image>();
        profile.sprite = Sprite.Create(scoreData.avatar, new Rect (0, 0, 120, 120), new Vector2 ());
        userName.text = scoreData.userNAme;
        score.text = scoreData.score;
    }

}
