using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace MyPokerGameServer
{
    class Player
    {
        public string Name { set; get; }
        public int GoldNum { set; get; }
        public Socket Socket { set; get; }

        public Player(Socket socket, string name, int goldNum)
        {
            Name = name;
            GoldNum = goldNum;
            Socket = socket;
        }
    }
}
