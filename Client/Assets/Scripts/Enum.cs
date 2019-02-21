using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PokerCardSuit
{
    SPADE = 0,//黑桃
    HEART = 1,// 红心
    DIAMEND = 2,// 方块
    CLUB = 3, //草花
    JOKER = 4, //王
}

public enum PokerScaleType
{
    TINY,
    MIDDLE,
    BIG,
}

public enum CallLordState
{
    CALL_LORD,
    GRAP_LORD,
}

public enum CallLordResultState
{
    CALL_LORD,
    GIVE_UP_CALL_LORD,
    GRAP_LORD,
}
