using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 其实此处的事件带的参数应该是通用事件类型，但是为了方便，有时只有服务器事件模块监听某一事件时，事件参数为通信事件结构，省略普通事件类型定义
 */
public enum EventName
{
    ASK_LOGIN,
    ACK_ENTER_ROOM,
    ACK_NEW_PLAYER_ENTER_ROOM
}
