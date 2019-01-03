using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Channel
{
    Normal = 100, Wechat = 101, Guest = 102, Smallchat = 111
}
public class MessageDefine
{
    private static int OGID_CLIENT_2_ROOM_BASE = 0x00001000; 
	private static int OGID_CLIENT_2_ROOM_BASE_MAX = 0x00001FFF;
    private static int ID_Game_Base = 0x00002000;
    private static int ID_Game_Max = 0x00002FFF;
    private static int OGID_GATE_2_CHAT_BASE = 0x00007000; 
	private static int OGID_GATE_2_CHAT_BASE_MAX = 0x00007FFF; 
	private static int OGID_GATE_2_CLUB_BASE = 0x00009300;
	private static int OGID_GATE_2_CLUB_BASE_MAX = 0x00009FFF;
	public static int REQ = 0x00001000; //请求消息类型 
	public static int ACK = 0x08000000; //应答消息类型

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
