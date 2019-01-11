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
        private static int OGID_CLIENT_2_ROOM_BASE = 1000;
        private static int OGID_CLIENT_2_ROOM_BASE_MAX = 1999;
        private static int ID_Game_Base = 8002000;
        private static int ID_Game_Max = 8002999;
        private static int OGID_GATE_2_CHAT_BASE = 7000;
        private static int OGID_GATE_2_CHAT_BASE_MAX = 7999;
        private static int OGID_GATE_2_CLUB_BASE = 9300;
        private static int OGID_GATE_2_CLUB_BASE_MAX = 9999;

        //------------------------------C2G-------------------------------------------//
        //------------------------------HALL-------------------------------------------//
        //登陆
        public static int OGID_CLIENT_2_ROOM_LOGIN = OGID_CLIENT_2_ROOM_BASE + 1;
        //------------------------------GAME-------------------------------------------//

        //------------------------------G2C-------------------------------------------//
        //------------------------------HALL-------------------------------------------//
        //------------------------------GAME-------------------------------------------//
        public static int G2C_Enter_Room = ID_Game_Base + 1;
        public static int G2C_New_Player_Enter_Room = ID_Game_Base + 2;
    }
}
