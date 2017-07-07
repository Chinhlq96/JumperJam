using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine;

public interface IFacebook {

    void LoginSuccess(IResult result);

    void LoginError(string error);

    void LoginFail();

    void CallBackQueryScore(ScoreDataForLeaderBoard scoreData);

    void CallBackInvite(IAppInviteResult result);

    void CallBackShare(IShareResult result);

    void OnFacebookIitialized();

    void OnGetAvatarUser(IGraphResult result);

	void OnGetNameUser(IGraphResult result);
}
