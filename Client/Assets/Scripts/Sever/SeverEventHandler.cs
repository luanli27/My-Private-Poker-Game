using UnityEngine;

public class SeverEventHandler : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        EventManager.Instance.AddEventListener(EventName.ASK_LOGIN, AskLoginHander);
	}

    void AskLoginHander(object arg)
    {
        AskLoginEventArg msg = arg as AskLoginEventArg;
        ReqLogin reqLogin = new ReqLogin();
        reqLogin.UserName = msg.AccountName;

        Singleton<NetworkManager>.Instance.SendMsg(MessageDefine.REQ | MessageDefine.OGID_CLIENT_2_ROOM_LOGIN, reqLogin);
    }
}
