using System;
using System.Collections.Generic;
using System.Text;

namespace MyPokerGameServer
{
    class PokerGame
    {
        private List<Player> _playerList = new List<Player>();

        public void InitPlayerList(List<Player> playerList)
        {
            _playerList = playerList;
        }

        public void Start()
        {
            Console.WriteLine("扑克桌满开始游戏！！");
        }

        public void DealCards()
        {

        }
    }
}
