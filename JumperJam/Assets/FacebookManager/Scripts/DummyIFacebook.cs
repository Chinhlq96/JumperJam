using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class DummyIFacebook : IFacebook {


    public void LoginSuccess(IResult result)
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
        
    }

    public void CallBackInvite(IAppInviteResult result)
    {

    }

    public void CallBackShare(IShareResult result)
    {

    }

    public void OnGetAvatarUser(IGraphResult result) 
    {

    }

	public void OnGetNameUser(IGraphResult result) 
	{

	}

    public void OnFacebookIitialized()
    {
        Debug.Log("OnFacebookIitialized - Dummy");
    }
}
