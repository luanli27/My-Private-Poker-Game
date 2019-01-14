using System.Collections;
using System.Collections.Generic;

enum Channel
{
    Normal = 100, Wechat = 101, Guest = 102, Smallchat = 111
}

namespace MyPokerGameServer
{
    public class MessageDefine
    {
        private static int C2G_MSG_BASE = 1000;
        private static int C2G_MSG_MAX = 1999;
        private static int G2C_MSG_BASE = 8002000;
        private static int G2C_MSG_MAX = 8002999;

        //------------------------------C2G-------------------------------------------//
        //登陆
        public static int C2G_REQ_LOGIN = C2G_MSG_BASE + 1;
        public static int C2G_REQ_READY_FOR_START = C2G_MSG_BASE + 2;
        public static int C2G_REQ_PLAY_CARDS = C2G_MSG_BASE + 3;

        //------------------------------G2C-------------------------------------------//
        public static int G2C_ENTER_ROOM = G2C_MSG_BASE + 1;
        public static int G2C_NEW_PLAYER_ENTER_ROOM = G2C_MSG_BASE + 2;
        public static int G2C_DEAL_CARDS = G2C_MSG_BASE + 3;
        public static int G2C_PLAY_CARDS = G2C_MSG_BASE + 4;
    }
}
