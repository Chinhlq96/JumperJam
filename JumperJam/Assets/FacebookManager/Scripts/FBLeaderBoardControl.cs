using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;

public class FBLeaderBoardControl : MonoBehaviour, IFacebook
{

    [Tooltip("Leaderboard panel")]
    [SerializeField]
    GameObject leaderBoard;

    [Tooltip("Ask login panel")]
    [SerializeField]
    GameObject askLoginPanel;

    [Tooltip("Avatar User")]
    [SerializeField]
    GameObject avatar;
    //avartar user

    [Tooltip("Scroll Prefab")]
    [SerializeField]
    GameObject scrollObject;
    // scroll object

    [Tooltip("Score Entry Prefab")]
    [SerializeField]
    GameObject scoreEntryObject;
    // score prefab

    [Tooltip("Number limits of request leaderboard")]
    [SerializeField]
    int numberRequest;

    [Tooltip("Content title on Share FB")]
    [SerializeField]
    string contentTitle;

    [Tooltip("Description on Share FB")]
    [SerializeField]
    string description;

    [Tooltip("URL on Share FB")]
    [SerializeField]
    string urlOnShare;

    [Tooltip("URL invite friend")]
    [SerializeField]
    string urlInvite;

    [Tooltip("URL image invite")]
    [SerializeField]
    string urlImageInvite;

    [Tooltip("Button login above button Invite")]
    [SerializeField]
    Button btnLogin;

    private List<ScoreDataForLeaderBoard> listScoreData;
  
    void Awake()
    {
        Debug.Log("Awake leader control");
        FBUnityDeepLinkingActivity.Instance.SetIFacebookCallback(this);
        listScoreData = new List<ScoreDataForLeaderBoard>();
    }

    void Start()
    {
        // start loading panel wait to settup FB
        SetupLeaderBoard();

    }

    IEnumerator DelaySetting(float seconds)
    {

        yield return new WaitForSeconds (seconds);
        //end quay
        // end loading panel
    }

    private void SetupLeaderBoard()
    {
        if (FB.IsInitialized && FB.IsLoggedIn) {
            leaderBoard.SetActive(true);
            askLoginPanel.SetActive(false);
            FBUnityDeepLinkingActivity.Instance.QueryScore(numberRequest);
            FBUnityDeepLinkingActivity.Instance.GetAvatarUserCallBack();
            btnLogin.gameObject.SetActive(false);
        } else {
            leaderBoard.SetActive(false);
            askLoginPanel.SetActive(true);
        }
    }


    public void OnFacebookIitialized()
    {
        // end loading panel
        StartCoroutine(DelaySetting(1f));
        SetupLeaderBoard();
    }

    public void Login()
    {
        FBUnityDeepLinkingActivity.Instance.FBLogin();
    }

    public void LoginSuccess(IResult result)
    {
        SetupLeaderBoard();
    }

    public void OnGetAvatarUser(IGraphResult result)
    {
        Debug.Log(result.ToString());
        Image profile = avatar.GetComponent<Image>();
        profile.sprite = Sprite.Create(result.Texture, new Rect (0, 0, 128, 128), new Vector2 ());
    }

	public void OnGetNameUser(IGraphResult result)
	{
		
	}

    public void LoginError(string error)
    {

    }

    public void LoginFail()
    {

    }

    public void CallBackQueryScore(ScoreDataForLeaderBoard result)
    {
        listScoreData.Add(result);
        Debug.Log("score data " + result.userNAme);
        Debug.Log("call back " + listScoreData.Count);
		GameObject scoreItem = Instantiate(scoreEntryObject) as GameObject;
        scoreItem.transform.SetParent(scrollObject.transform,false);

        scoreItem.transform.localScale = new Vector3 (1f, 1f, 1f);

        scoreItem.GetComponent<ScoreItem>().SetData(result);
    }


    public void CallBackInvite(IAppInviteResult result)
    {

    }

    public void CallBackShare(IShareResult result)
    {

    }
}
