using System;
using System.Collections.Generic;
using System.Text;
using Google.Protobuf.Collections;

namespace MyPokerGameServer
{
    class PokerRoom
    {
        private int _roomId = -1;
        private int _maxGamerCount = 4;
        private List<Player> _playerList = new List<Player>();

        public PokerRoom(int roomId)
        {
            _roomId = roomId;
        }

        public bool CanEnter(Player player)
        {
            bool result = true;
            if (_playerList.Count > _maxGamerCount)
            {
                result = false;
            }
            else
            {
                foreach (var p in _playerList)
                {
                    if (p.Name == player.Name)
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        public void EnterNewPlayer(Player player)
        {
            Console.WriteLine("玩家" + player.Name +"进入"+ _roomId + "房间！");
            _playerList.Add(player);
            //通知客户端进入游戏房间,非新玩家的广播进入新玩家消息，新玩家发送房间玩家列表信息
            foreach (var p in _playerList)
            {
                if (p.Name != player.Name)
                {
                    AckNewPlayerEnterRoom msg = new AckNewPlayerEnterRoom();
                    msg.NewPlayerInfo = new PlayerInfo();
                    msg.NewPlayerInfo.UserName = player.Name;
                    msg.NewPlayerInfo.GoldNum = player.GoldNum;
                    Singleton<NetworkManager>.Instance.SendMsg(player.Socket, MessageDefine.G2C_New_Player_Enter_Room, msg);
                }
                else
                {
                    AckEnterRoom msg = new AckEnterRoom();
                    foreach (var innerP in _playerList)
                    {
                        PlayerInfo pInfo = new PlayerInfo();
                        pInfo.UserName = innerP.Name;
                        pInfo.GoldNum = innerP.GoldNum;
                        msg.PlayerInfo.Add(pInfo);

                        //TODO 测试阶段来两个机器人
                        PlayerInfo fakePlayerOne = new PlayerInfo();
                        fakePlayerOne.UserName = "nintendo";
                        fakePlayerOne.GoldNum = 999999;
                        msg.PlayerInfo.Add(fakePlayerOne);
                        PlayerInfo fakePlayerSec = new PlayerInfo();
                        fakePlayerSec.UserName = "capcom";
                        fakePlayerSec.GoldNum = 888888;
                        msg.PlayerInfo.Add(fakePlayerSec);
                        //TODO END
                    }

                    Singleton<NetworkManager>.Instance.SendMsg(player.Socket, MessageDefine.G2C_Enter_Room, msg);
                }

            }
            if (_playerList.Count == _maxGamerCount)
            {
                StartGame();
            }
        }

        public void StartGame()
        {
            PokerGame game = new PokerGame();
            game.Start();
        }
    }
}
