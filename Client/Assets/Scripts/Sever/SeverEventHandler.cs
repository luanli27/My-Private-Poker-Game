using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

 class SeverEventHandler : Singleton<SeverEventHandler> {

     private Dictionary<int, Action<byte[]>> _receiveMsgHanlerDic = new Dictionary<int, Action<byte[]>>
     {
         { MessageDefine.G2C_ENTER_ROOM, msg => Singleton<EventManager>.Instance.DispatchEvent(EventName.ACK_ENTER_ROOM,  AckEnterRoomResult.Parser.ParseFrom(msg))},
         { MessageDefine.G2C_NEW_PLAYER_ENTER_ROOM, msg => 
             Singleton<EventManager>.Instance.DispatchEvent(EventName.ACK_NEW_PLAYER_ENTER_ROOM, AckNewPlayerEnterRoom.Parser.ParseFrom(msg))},
         { MessageDefine.G2C_DEAL_CARDS, msg => Singleton<EventManager>.Instance.DispatchEvent(EventName.ACK_DEAL_CARDS, DealCard.Parser.ParseFrom(msg))},
     };

     private class ServerMsg
     {
         public int MsgId { set; get; }
         public byte[] MsgBody { set; get; }
         public ServerMsg(int msgId, byte[] msgBody)
         {
             MsgId = msgId;
             MsgBody = msgBody;
         }
     }

     //使用消息队列机制的原因是socket使用的多线程,而非主线程会有很多API调用上的问题,所以在update中重新将控制权交给unity主线程
     private Queue<ServerMsg> _serverMsgCache = new Queue<ServerMsg>();

     public void Start()
     {
         Singleton<EventManager>.Instance.AddEventListener(EventName.REQ_LOGIN, AskLoginHander);
     }

     public void Update()
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
        Singleton<NetworkManager>.Instance.SendMsg(MessageDefine.C2G_REQ_LOGIN, reqLogin);
    }

    /*-------------------------------------------------------------Receive MSG From Server-------------------------------------------------------------------------------------*/
    public void OnReceiveMsgFromServer(int msgId, byte[] msgBody)
    {
        _serverMsgCache.Enqueue(new ServerMsg(msgId, msgBody));
    }
}
