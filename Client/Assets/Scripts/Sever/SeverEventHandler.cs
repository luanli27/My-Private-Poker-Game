using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

 class SeverEventHandler : MonoBehaviour {

     private Dictionary<int, Action<object>> _receiveMsgHanlerDic = new Dictionary<int, Action<object>>
     {
         { MessageDefine.G2C_Enter_Room, (msg) => Singleton<EventManager>.Instance.DispatchEvent(EventName.ACK_ENTER_ROOM, msg)},
         { MessageDefine.G2C_New_Player_Enter_Room, (msg) => Singleton<EventManager>.Instance.DispatchEvent(EventName.ACK_NEW_PLAYER_ENTER_ROOM, msg)}
     };
    /*-------------------------------------------------------------Client Req Msg-------------------------------------------------------------------------------------*/
    void Awake ()
    {
        Singleton<SeverEventHandler>.Instance = this;
    }

    void Start()
    {
        Singleton<EventManager>.Instance.AddEventListener(EventName.ASK_LOGIN, AskLoginHander);
    }

    void AskLoginHander(object arg)
    {
        ReqLogin reqLogin = arg as ReqLogin;
        Singleton<NetworkManager>.Instance.SendMsg(MessageDefine.OGID_CLIENT_2_ROOM_LOGIN, reqLogin);
    }

    /*-------------------------------------------------------------Receive MSG From Server-------------------------------------------------------------------------------------*/
    public void OnReceiveMsgFromServer(int msgId, byte[] msgBody)
    {
        if (_receiveMsgHanlerDic.ContainsKey(msgId))
            _receiveMsgHanlerDic[msgId].Invoke(msgBody);
        else
            Debug.LogError("并未在服务器响应字典中注册对应Action!");
    }
}
