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

     private class ServerMsg
     {
         public int MsgId { set; get; }
         public object MsgBody { set; get; }
         public ServerMsg(int msgId, object msgBody)
         {
             MsgId = msgId;
             MsgBody = msgBody;
         }
     }

     //使用消息队列机制的原因是socket使用的多线程,而非主线程会有很多API调用上的问题,所以在update中重新将控制权交给unity主线程
     private Queue<ServerMsg> _serverMsgCache = new Queue<ServerMsg>();

     void Awake()
     {
         Singleton<SeverEventHandler>.Instance = this;
     }

     void Start()
     {
         Singleton<EventManager>.Instance.AddEventListener(EventName.ASK_LOGIN, AskLoginHander);
     }

     void Update()
     {
         while (_serverMsgCache.Count > 0)
         {
             ServerMsg serverMsg = _serverMsgCache.Dequeue();
             if (_receiveMsgHanlerDic.ContainsKey(serverMsg.MsgId))
                 _receiveMsgHanlerDic[serverMsg.MsgId].Invoke(serverMsg.MsgBody);
             else
                 Debug.LogError("并未在服务器响应字典中注册对应Action!");

         }
     }
    /*-------------------------------------------------------------Client Req Msg-------------------------------------------------------------------------------------*/
     void AskLoginHander(object arg)
    {
        ReqLogin reqLogin = arg as ReqLogin;
        Singleton<NetworkManager>.Instance.SendMsg(MessageDefine.OGID_CLIENT_2_ROOM_LOGIN, reqLogin);
    }

    /*-------------------------------------------------------------Receive MSG From Server-------------------------------------------------------------------------------------*/
    public void OnReceiveMsgFromServer(int msgId, byte[] msgBody)
    {
        _serverMsgCache.Enqueue(new ServerMsg(msgId, msgBody));
    }
}
