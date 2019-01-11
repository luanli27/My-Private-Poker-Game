using System;
using System.Collections.Generic;

namespace MyPokerGameServer
{
    class PokerRoom
    {
        private int _roomId = -1;
        private int _maxGamerCount = 3;
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

        public void EnterNewPlayer(Player newPlayer)
        {
            Console.WriteLine("玩家" + newPlayer.Name +"进入"+ _roomId + "房间！");
            _playerList.Add(newPlayer);
            //通知客户端进入游戏房间,非新玩家的广播进入新玩家消息，新玩家发送房间玩家列表信息
            foreach (var p in _playerList)
            {
                if (p.Name != newPlayer.Name)
                {
                    AckNewPlayerEnterRoom msg = new AckNewPlayerEnterRoom();
                    msg.PlayerInfo = new PlayerInfo();
                    msg.PlayerInfo.Seat = _playerList.Count - 1;
                    msg.PlayerInfo.AccountName = newPlayer.Name;
                    msg.PlayerInfo.CoinNum = newPlayer.GoldNum;
                    Singleton<NetworkManager>.Instance.SendMsg(p.Socket, MessageDefine.G2C_New_Player_Enter_Room, msg);
                }
                else
                {
                    AckEnterRoomResult msg = new AckEnterRoomResult();
                    for (int i = 0; i < _playerList.Count; i++)
                    {
                        PlayerInfo pInfo = new PlayerInfo();
                        pInfo.Seat = i;
                        pInfo.AccountName = _playerList[i].Name;
                        pInfo.CoinNum = _playerList[i].GoldNum;
                        msg.PlayerInfos.Add(pInfo);
                    }

                    Singleton<NetworkManager>.Instance.SendMsg(p.Socket, MessageDefine.G2C_Enter_Room, msg);
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
