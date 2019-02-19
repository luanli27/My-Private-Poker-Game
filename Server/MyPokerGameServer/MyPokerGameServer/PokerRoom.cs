using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace MyPokerGameServer
{
    class PokerRoom
    {
        private int _roomId = -1;
        private int _maxGamerCount = 3;
        private List<string> _accountList = new List<string>();
        private Dictionary<int, Action<byte[]>> _receiveMsgHanlerDic = new Dictionary<int, Action<byte[]>>();

        public PokerRoom(int roomId)
        {
            _roomId = roomId;
            RegisterDDZEvents();
        }

        public bool CanEnter(string account)
        {
            bool result = true;
            if (_accountList.Count > _maxGamerCount)
            {
                result = false;
            }
            else
            {
                foreach (var p in _accountList)
                {
                    if (p == account)
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
            _accountList.Add(account);
            //TODO TESTAREA
//            _accountList.Add("robot1");
//            _accountList.Add("robot2");
            //TODO END NEED DELETE

            //更新账号连接信息，调整为进入房间状态
            Singleton<AccountConnectionManager>.Instance.OnEnterRoom(account, _roomId);
            //通知客户端进入游戏房间,非新玩家的广播进入新玩家消息，新玩家发送房间玩家列表信息
            foreach (var p in _accountList)
            {
                AccountConnectionInfo connectionInfo = Singleton<AccountConnectionManager>.Instance.GetAccountConnectionInfo(p);
                if (connectionInfo == null)
                {
                    continue;
                }
                Socket socket = connectionInfo.OrigSocket;

                //除进房间的新玩家外，其余玩家收到新玩家进入房间的消息
                if (p != account)
                {
                    AckNewPlayerEnterRoom msg = new AckNewPlayerEnterRoom();
                    msg.PlayerInfo = new PlayerInfo();
                    msg.PlayerInfo.Seat = _accountList.Count - 1;
                    msg.PlayerInfo.AccountName = account;
                    msg.PlayerInfo.CoinNum = 9999;
                    Singleton<NetworkManager>.Instance.SendMsg(socket, MessageDefine.G2C_NEW_PLAYER_ENTER_ROOM, msg);
                }
                //新进房间的玩家收到房间玩家列表信息
                else
                {
                    AckEnterRoomResult msg = new AckEnterRoomResult();
                    for (int i = 0; i < _accountList.Count; i++)
                    {
                        PlayerInfo pInfo = new PlayerInfo();
                        pInfo.Seat = i;
                        pInfo.AccountName = _accountList[i];
                        pInfo.CoinNum = 9999;
                        msg.PlayerInfos.Add(pInfo);
                    }

                    Singleton<NetworkManager>.Instance.SendMsg(socket, MessageDefine.G2C_ENTER_ROOM, msg);
                }

            }
            if (_accountList.Count == _maxGamerCount)
            {
                StartGame();
            }
        }

        public void StartGame()
        {
            PokerGame game = new PokerGame();
            game.InitPlayerList(_accountList);
            game.Start();
        }

        public void OnReceiveMsg()
        {

        }

        private void RegisterDDZEvents()
        {
            _receiveMsgHanlerDic.Add(MessageDefine.C2G_REQ_READY_FOR_START, OnPlayerReadyForStart);
        }

        private void OnPlayerReadyForStart(object msg)
        {
            ReqReadyForStartGame msgBody = msg as ReqReadyForStartGame;
            Console.WriteLine("收到客户端准备完毕的消息");
        }
    }
}
