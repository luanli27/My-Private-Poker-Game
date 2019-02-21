using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using Google.Protobuf;
using Google.Protobuf.Collections;

namespace MyPokerGameServer
{
    class PokerGame
    {
        private List<AccountInfo> _playerList = new List<AccountInfo>();
        private DDZCardDealer _dealer = new DDZCardDealer();
        private DDZGameData _ddzGameData = new DDZGameData();
        private int _callLordWaitSeconds = 20;

        public void InitPlayerList(List<string> accountList)
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

        public void Start()
        {
            Console.WriteLine("扑克桌满开始游戏！！");
            SendGameStartMsg();
            DealCards();
            SendCallLordMsg();
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

        private void SendCallLordMsg()
        {
            CallLordMsg msg = new CallLordMsg();
            msg.CurrentCallSeat = 0;
            msg.CurrentCallState = (int)CallLordState.CALL_LORD;
            msg.WaitTime = _callLordWaitSeconds;
            foreach (var player in _playerList)
            {
                SendMsgToClient(player, MessageDefine.G2C_CALL_LORD, msg);
                Console.WriteLine("开始叫地主！！");
            }
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
    }
}
