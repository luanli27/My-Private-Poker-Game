using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginUi : MonoBehaviour {

    public Button LoginButton;
    public InputField Account;
    public InputField Password;
	// Use this for initialization
	void Start ()
	{
        LoginButton.onClick.AddListener(OnRequeseLogin);
        Singleton<EventManager>.Instance.AddEventListener(EventName.ACK_ENTER_ROOM, OnEnterRoom);
	}

    void OnRequeseLogin()
    {
        ReqLogin msg = new ReqLogin
        {
            UserName = Account.text
        };
        Singleton<EventManager>.Instance.DispatchEvent(EventName.ASK_LOGIN, msg);
    }

    void OnEnterRoom(object msg)
    {
        byte[] msgArray = msg as byte[];
        AckEnterRoom enterRoomMsg = AckEnterRoom.Parser.ParseFrom(msgArray);
        foreach (var pInfo in enterRoomMsg.PlayerInfo)
        {
            Debug.LogError("当前牌室内的玩家为:"+pInfo.UserName);
        }

        SceneManager.LoadScene("DDZGameScene");
    }
}
