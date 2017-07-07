using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine.UI;


public class FBUnityDeepLinkingActivity : Singleton<FBUnityDeepLinkingActivity>
{
    
    private IFacebook iFacebook;

	public GameObject ScoreEntryPanel; 
	public GameObject ScrollScoreList;
	public GameObject ScrollView;

    public static FBUnityDeepLinkingActivity GetInstance()
    {
        return Instance;
    }

    public void SetIFacebookCallback(IFacebook iFacebook)
    {
        this.iFacebook = iFacebook;
    }
    
    void Awake()
    {
        if (iFacebook == null)
        {
            iFacebook = new DummyIFacebook();
        }
        FB.Init(InitCallBack, OnHideUnity);
    }

    void InitCallBack()
    {
        if (FB.IsLoggedIn) {

        } else {

        }
        iFacebook.OnFacebookIitialized();
    }

    void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown) {
            Time.timeScale = 1;
        } else {
            Time.timeScale = 0;
        }
    }

    /// <summary>
    /// Login Facebook
    /// </summary>
    #region login
    public void FBLogin()
    {
        // open loadding


        FB.LogInWithReadPermissions(new List<string> (){ "public_profile" }, AuthCallBack);
	
    }

    void AuthCallBack(IResult result)
    {
        if (result.Error != null) {
            Debug.Log(result.Error);
            iFacebook.LoginError(result.Error);
        } else {
            if (FB.IsLoggedIn) {
                Debug.Log("login");
                // show message succcess;
                iFacebook.LoginSuccess(result);
                GetAvatarUserCallBack();
            }
            else {
                Debug.Log("login fail");
                //show message error;
                iFacebook.LoginFail();
            }
        }
        //close loadding
    }

    public void GetAvatarUserCallBack()
    {
       // avatar.SetActive(true);
        FB.API(
            "/me/picture?type=square&height=128&width=128",
            HttpMethod.GET,
            DisplayAvatarUser
        );
		FB.API(
			"/me",
			HttpMethod.GET,
			DisplayNameUser
		);
    }



    /// <summary>
    /// Displaies the infor.
    /// </summary>
    /// <param name="result">Result.</param>

    void DisplayAvatarUser(IGraphResult result)
    {
    
        if (result.Texture != null) {
            iFacebook.OnGetAvatarUser(result);
        } else {
        }
    }

	void DisplayNameUser(IGraphResult result)
	{

		if (result != null) {
			iFacebook.OnGetNameUser(result);
		} else {
		}
	}

    #endregion

    #region get score

    /// <summary>
    /// Queries the score
    /// </summary>
	private bool queried;

    public void QueryScore(int limits)
	{
        string request = "/app/scores?fields=score,user.limit(" + limits.ToString() + ")";
        FB.API(
            request,
            HttpMethod.GET,
            LeaderboardCallBack
        );

    }
        

    void LeaderboardCallBack(IResult result)
    {
		if (!queried) {
			
			ScrollView.SetActive (true);
			queried = true;
			IDictionary<string, object> data = result.ResultDictionary;
			List<object> scoreList = (List<object>)data ["data"];
			foreach (object obj in scoreList) {
				ScoreDataForLeaderBoard scoreData = new ScoreDataForLeaderBoard ();
				var entry = (Dictionary<string, object>)obj;
				var user = (Dictionary<string, object>)entry ["user"]; 

				GameObject scorePanel;
				scorePanel = Instantiate (ScoreEntryPanel) as GameObject;
				scorePanel.transform.SetParent (ScrollScoreList.transform, false);

				Transform FName = scorePanel.transform.Find ("FriendName");
				Transform Fscore = scorePanel.transform.Find ("FriendScore");
				Transform FAvatar = scorePanel.transform.Find ("FriendAvatar");

				Text Fnametext = FName.GetComponent<Text> ();
				Text Fscoretext = Fscore.GetComponent<Text> ();
				Image FUserAvatar = FAvatar.GetComponent<Image> ();

				Fnametext.text = user ["name"].ToString ();
				Fscoretext.text = entry ["score"].ToString ();

				FB.API (user ["id"].ToString () + "/picture?width=120&height=120", HttpMethod.GET, delegate(IGraphResult avatarResult) {
					if (avatarResult.Error != null) {
						Debug.Log ("Fail to load avatar user: " + avatarResult.Error);
					} else {
						FUserAvatar.sprite = Sprite.Create (avatarResult.Texture, new Rect (0, 0, 120, 120), new Vector2 (0, 0));
						iFacebook.CallBackQueryScore (scoreData);
					}
				});
			}
		} 
		else 
		{
			foreach (Transform child in ScrollScoreList.transform) 
			{
				GameObject.Destroy (child.gameObject);
			}
			ScrollView.SetActive (false);
			queried = false;
		}
    }

    #endregion

    #region set score

    /// <summary>
    /// Sets the score.
    /// </summary>
	private bool permitted;
    public void SetScore()
	{if (!permitted) {
			FB.LogInWithPublishPermissions (new List<string> (){ "publish_actions" });
			permitted = true;
		} else {
			var scoreData = new Dictionary<string, string> ();
			scoreData ["score"] = UnityEngine.Random.Range (0, 250).ToString ();

			FB.API ("/me/scores", 
				HttpMethod.POST, 
				delegate(IGraphResult result) 
				{
					{
						Debug.Log ("Submitted" + result.RawResult);
					}
				}, 
			scoreData);
		}
    }

    #endregion

    #region invite friend
        
    /// <summary>
    /// Invites the friend.
    /// </summary>

	public void InviteFriend(string urlInvite,string urlImageInvite)
    {
        
        FB.Mobile.AppInvite(
            new System.Uri (urlInvite.ToString()),
            new System.Uri (urlImageInvite.ToString()),
            iFacebook.CallBackInvite
        );
    }

    private void InviteCallback(IAppInviteResult result)
    {
        print(result.RawResult);
        if(result.Error != null){
            Debug.Log("error");
        }else if(result.Cancelled){
            Debug.Log("canceled");
        }else{
            Debug.Log("success");
        }
    }

    #endregion

    #region share facebook

    /// <summary>
    /// Share to Facebook
    /// </summary>
    public void ShareFB(string contentTitle, string urlOnShare, string description)
    {
        FB.ShareLink(
            contentTitle:contentTitle.ToString(),
            contentURL:new System.Uri(urlOnShare.ToString()),
            contentDescription:description.ToString(),
            callback:iFacebook.CallBackShare
        );
    }

    /// <summary>
    /// Method Call Back ShareFB
    /// </summary>
    private void OnShareFB(IShareResult result)
    {
        if (result.Cancelled || !string.IsNullOrEmpty(result.Error)) {
            Debug.Log("Share Link Error: " + result.Error);
        } else if(!string.IsNullOrEmpty(result.PostId)) {
            Debug.Log(result.PostId);
        } else {
            Debug.Log("Share success");
        }
    }
    #endregion
      
}
        