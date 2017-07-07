using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;
using System.Collections;

public class FBScript : MonoBehaviour ,IFacebook {

	[Tooltip("Leaderboard panel")]
	[SerializeField]
	GameObject leaderBoard;

	[Tooltip("Ask login panel")]
	[SerializeField]
	GameObject askLoginPanel;

	[Tooltip("Avatar User")]
	[SerializeField]
	GameObject avatar;

	[SerializeField]
	GameObject userName;
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

	void Awake()
	{
		avatar.SetActive (false);
		userName.SetActive (false);
		FBUnityDeepLinkingActivity.Instance.SetIFacebookCallback(this);
	}



	/// <summary>
	/// FBs the login.
	/// </summary>
	#region Login
	public void FBLogin()
	{
		FBUnityDeepLinkingActivity.Instance.FBLogin ();
	}

	public void OnGetAvatarUser(IGraphResult result)
	{	
		avatar.SetActive (true);
		Debug.Log(result.ToString());
		Image profile = avatar.GetComponent<Image>();
		profile.sprite = Sprite.Create(result.Texture, new Rect (0, 0, 128, 128), new Vector2 ());


	}

	public void OnGetNameUser(IGraphResult result)
	{	
		userName.SetActive (true);
		Text profileName = userName.GetComponent<Text> ();
		profileName.text = string.Format("Hi, {0}",result.ResultDictionary["name"]);
	}

	public void LoginSuccess(IResult result)
	{
		
	}

	public void LoginError(string error)
	{

	}

	public void LoginFail()
	{

	}

	#endregion



	/// <summary>
	/// FBs the share.
	/// </summary>
	#region Share
	public void FBShare()
	{
		FBUnityDeepLinkingActivity.Instance.ShareFB (contentTitle,urlOnShare,description);
	}

	public void CallBackShare(IShareResult result)
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




	/// <summary>
	/// Invites the friend.
	/// </summary>
	#region Invite
	public void InviteFriend()
	{
		FBUnityDeepLinkingActivity.Instance.InviteFriend (urlInvite, urlImageInvite);
	}

	public void CallBackInvite(IAppInviteResult result)
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

	public void QueryScore()
	{
		FBUnityDeepLinkingActivity.Instance.QueryScore (10);
	}
	public void CallBackQueryScore(ScoreDataForLeaderBoard result)
	{

	}




	public void OnFacebookIitialized()
	{}

}
