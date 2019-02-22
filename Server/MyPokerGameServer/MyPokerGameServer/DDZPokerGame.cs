using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using Google.Protobuf;
using Google.Protobuf.Collections;

namespace MyPokerGameServer
{
    class DDZPokerGame
    {
        private List<AccountInfo> _playerList = new List<AccountInfo>();
        private DDZCardDealer _dealer = new DDZCardDealer();
        private DDZGameData _ddzGameData = new DDZGameData();
        private int _callLordWaitSeconds = 20;
        private Dictionary<int, Action<string, byte[]>> _receiveMsgHanlerDic = new Dictionary<int, Action<string, byte[]>>();

        public void Init(List<string> accountList)
        {
            InitPlayerData(accountList);
            RegisterEventHandler();
        }

        private void InitPlayerData(List<string> accountList)
        {
            List<AccountInfo> accountInfos = new List<AccountInfo>();
            foreach (var account in accountList)
            {
                //应该进行数据库查询获取玩家信息的,目前简化掉
                accountInfos.Add(new AccountInfo(account, 9999));
            }
            _playerList = accountInfos;
            _ddzGameData.InitPlayerInfoDic(accountInfos);
        }

        private void RegisterEventHandler()
        {
            _receiveMsgHanlerDic.Add(MessageDefine.C2G_REQ_CALL_LORD, OnGetPlayerCallLordResult);
        }

        public void HandlerMsgs(string account, int msgId, byte[] msgBody)
        {
            if (_receiveMsgHanlerDic.ContainsKey(msgId))
                _receiveMsgHanlerDic[msgId].Invoke(account, msgBody);
        }

        public void Start()
        {
            Console.WriteLine("扑克桌满开始游戏！！");
            SendGameStartMsg();
            DealCards();
            SendCallLordMsg(0, CallLord.CALL_LORD, new List<int>(), new List<int>());
        }

        private void SendGameStartMsg()
        {
            foreach (var player in _playerList)
                SendMsgToClient(player, MessageDefine.G2C_POKER_GAME_BEGIN, new AckGameStart());
            Console.WriteLine("斗地主游戏开始啦！！");
        }

        private void DealCards()
        {
            Dictionary<int, List<int>> result = _dealer.Deal(_playerList.Count);
            foreach (var kv in result)
            {
                _ddzGameData.PlayerInfoDic[kv.Key].Handcards = kv.Value;
            }

            DealCard msg = new DealCard();

            for (int i = 0; i < _playerList.Count; i++)
            {
                msg.StartSeat = 0;
                msg.HandCards.Clear();
                foreach (var cardId in _ddzGameData.PlayerInfoDic[i].Handcards)
                    msg.HandCards.Add(cardId);

                foreach (var playerInfo in _ddzGameData.PlayerInfoDic)
                    msg.LeftCardNum.Add(playerInfo.Value.Handcards.Count);

                msg.ThinkTime = 20;
                msg.StartSeat = 0;

                SendMsgToClient(_playerList[i], MessageDefine.G2C_DEAL_CARDS, msg);
            }
        }

        private void SendCallLordMsg(int callLordSeat, CallLord state, List<int> resultSeats, List<int> results)
        {
            CallLordMsg msg = new CallLordMsg();
            msg.CurrentCallSeat = callLordSeat;
            msg.CurrentCallState = (int)state;
            msg.WaitTime = _callLordWaitSeconds;
            foreach (int seat in resultSeats)
                msg.CallLordResultSeats.Add(seat);
            foreach (int result in results)
                msg.CallLordResults.Add(result);
            foreach (var player in _playerList)
                SendMsgToClient(player, MessageDefine.G2C_CALL_LORD, msg);
        }

        private void SendMsgToClient(AccountInfo accountInfo, int msgId, IMessage msg)
        {
            NetworkManager.Instance.SendMsg(GetPlayerSocket(accountInfo.Name), msgId, msg);
        }

        private Socket GetPlayerSocket(string account)
        {
            Socket result = null;
            AccountConnectionInfo info = Singleton<AccountConnectionManager>.Instance.GetAccountConnectionInfo(account);
            if (info != null)
            {
                result = info.OrigSocket;
            }

            return result;
        }

        private void OnGetPlayerCallLordResult(string account, byte[] msg)
        {
            ResponseCallLordMsg responseCallLordMsg = ResponseCallLordMsg.Parser.ParseFrom(msg, 0, msg.Length);
            Console.WriteLine("收到玩家:" + account + "的叫地主结果：" + responseCallLordMsg.Seat + "  result is :" + responseCallLordMsg.Result);
            if (!DDZGameData.Instance.PlayerInfoInGameDic.ContainsKey(responseCallLordMsg.Seat))
                DDZGameData.Instance.PlayerInfoInGameDic[responseCallLordMsg.Seat] = new PlayerDataInGame();
            DDZGameData.Instance.PlayerInfoInGameDic[responseCallLordMsg.Seat].CallLordResultState = (CallLord)responseCallLordMsg.Result;
            int nextCallLordSeat = ++_ddzGameData.CurrentCallLordSeat;
            List<int> callLordResultSeats = new List<int>();
            List<int> callLordResults = new List<int>();
            foreach (var kv in DDZGameData.Instance.PlayerInfoInGameDic)
            {
                callLordResultSeats.Add(kv.Key);
                callLordResults.Add((int)kv.Value.CallLordResultState);
            }
            if ((CallLord) responseCallLordMsg.Result == CallLord.CALL_LORD)
                DDZGameData.Instance.IsGrapLord = true;
            if (nextCallLordSeat < DDZGameData.Instance.PlayerCount)
                SendCallLordMsg(nextCallLordSeat, DDZGameData.Instance.IsGrapLord ? CallLord.GRAP_LORD : CallLord.CALL_LORD, callLordResultSeats, callLordResults);
            else
                SendCallLordResult();
        }

        private void SendCallLordResult()
        {
        }
    }
}
