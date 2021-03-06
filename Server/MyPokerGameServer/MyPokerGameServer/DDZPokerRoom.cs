﻿using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace MyPokerGameServer
{
    class DDZPokerRoom
    {
        private int _roomId = -1;
        private int _maxGamerCount = 3;
        private Dictionary<string, PlayerInfoInRoom> _playerInfoDic = new Dictionary<string, PlayerInfoInRoom>();
        private Dictionary<int, Action<string, byte[]>> _receiveMsgHanlerDic = new Dictionary<int, Action<string, byte[]>>();
        private DDZPokerGame _pokerGame;

        public DDZPokerRoom(int roomId)
        {
            _roomId = roomId;
            RegisterDDZEvents();
        }

        public bool CanEnter(string account)
        {
            bool result = true;
            if (_playerInfoDic.Count > _maxGamerCount)
                result = false;
            else
            {
                foreach (var p in _playerInfoDic)
                {
                    if (p.Key == account)
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        public void EnterNewPlayer(string account)
        {
            Console.WriteLine("玩家" + account + "进入"+ _roomId + "房间！");
            _playerInfoDic.Add(account, new PlayerInfoInRoom());
            //TODO TESTAREA
//            _accountList.Add("robot1");
//            _accountList.Add("robot2");
            //TODO END NEED DELETE

            //更新账号连接信息，调整为进入房间状态
            Singleton<AccountConnectionManager>.Instance.OnEnterRoom(account, _roomId);
            //通知客户端进入游戏房间,非新玩家的广播进入新玩家消息，新玩家发送房间玩家列表信息
            foreach (var p in _playerInfoDic)
            {
                AccountConnectionInfo connectionInfo = Singleton<AccountConnectionManager>.Instance.GetAccountConnectionInfo(p.Key);
                if (connectionInfo == null)
                {
                    continue;
                }
                Socket socket = connectionInfo.OrigSocket;

                //除进房间的新玩家外，其余玩家收到新玩家进入房间的消息
                if (p.Key != account)
                {
                    AckNewPlayerEnterRoom msg = new AckNewPlayerEnterRoom();
                    msg.PlayerInfo = new PlayerInfo();
                    msg.PlayerInfo.Seat = _playerInfoDic.Count - 1;
                    msg.PlayerInfo.AccountName = account;
                    msg.PlayerInfo.CoinNum = 9999;
                    Singleton<NetworkManager>.Instance.SendMsg(socket, MessageDefine.G2C_NEW_PLAYER_ENTER_ROOM, msg);
                }
                //新进房间的玩家收到房间玩家列表信息
                else
                {
                    AckEnterRoomResult msg = new AckEnterRoomResult();
                    int seat = 0;
                    foreach (var kv in _playerInfoDic)
                    {
                        PlayerInfo pInfo = new PlayerInfo();
                        pInfo.Seat = seat;
                        pInfo.AccountName = kv.Key;
                        pInfo.CoinNum = 9999;
                        msg.PlayerInfos.Add(pInfo);
                        seat++;
                    }

                    Singleton<NetworkManager>.Instance.SendMsg(socket, MessageDefine.G2C_ENTER_ROOM, msg);
                }
            }
        }

        public void StartGame()
        {
            _pokerGame = new DDZPokerGame();
            List<string> accountList = new List<string>();
            foreach (var kv in _playerInfoDic)
                accountList.Add(kv.Key);
            _pokerGame.Init(accountList);
            _pokerGame.Start();
        }

        public void OnReceiveMsg(string account, int msgId, byte[] msgBody)
        {
            if (_receiveMsgHanlerDic.ContainsKey(msgId) && _playerInfoDic.ContainsKey(account))
                _receiveMsgHanlerDic[msgId].Invoke(account, msgBody);
            else
                _pokerGame.HandlerMsgs(account, msgId, msgBody);
        }

        private void RegisterDDZEvents()
        {
            _receiveMsgHanlerDic.Add(MessageDefine.C2G_REQ_READY_FOR_START, OnPlayerReadyForStart);
        }

        private void OnPlayerReadyForStart(string account, byte[] msg)
        {
            ReqReadyForStartGame readyForStartGame = ReqReadyForStartGame.Parser.ParseFrom(msg, 0, msg.Length);
            _playerInfoDic[account].IsReadyForStartGame = readyForStartGame.Ready;
            Console.WriteLine("收到玩家:" + account + "准备完毕的消息");
            CheckIfCanStartGame();
        }

        private void CheckIfCanStartGame()
        {
            if (_playerInfoDic.Count == _maxGamerCount)
            {
                bool canStart = true;
                foreach (var kv in _playerInfoDic)
                {
                    if (!kv.Value.IsReadyForStartGame)
                    {
                        canStart = false;
                        break;
                    }
                }

                if (canStart)
                    StartGame();
            }
        }
    }

    class PlayerInfoInRoom
    {
        public bool IsReadyForStartGame;
    }
}
