using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class DDZGameScene : MonoBehaviour
{
    public RemotePlayerView LeftPlayView;
    public RemotePlayerView RightPlayView;
    public SelfPlayerView SelfPlayerView;

    private Dictionary<int, DDZPlayerBase> _playerSeatDic = new Dictionary<int, DDZPlayerBase>(); 

    void Start()
    {
        InitGameScene();
        RegisterEvents();
    }

    private void InitGameScene()
    {
        LeftPlayView.gameObject.SetActive(false);
        RightPlayView.gameObject.SetActive(false);
        SelfPlayerView.gameObject.SetActive(false);
        MapSeatWithPlayerView();

        foreach (var playerItem in _playerSeatDic)
        {
            if (Singleton<DDZGameData>.Instance.PlayersSeatDataDic.ContainsKey(playerItem.Key))
            {
                playerItem.Value.gameObject.SetActive(true);
                playerItem.Value.InitPlayerViewWithData(Singleton<DDZGameData>.Instance.PlayersSeatDataDic[playerItem.Key]);
            }
        }
    }

    private void RegisterEvents()
    {
        Singleton<EventManager>.Instance.AddEventListener(EventName.ACK_NEW_PLAYER_ENTER_ROOM, OnNewPlayerEnterRoom);
    }

    private void MapSeatWithPlayerView()
    {
        int playerNum = Singleton<DDZGameData>.Instance.PlayerCount;
        int mySeat = Singleton<DDZGameData>.Instance.MySeatIndex;
        _playerSeatDic[mySeat] = SelfPlayerView;
        int rightSeat = (mySeat + 1) % playerNum;
        int leftSeat = (mySeat + 2) % playerNum;
        _playerSeatDic[rightSeat] = RightPlayView;
        _playerSeatDic[leftSeat] = LeftPlayView;
    }

    private void OnNewPlayerEnterRoom(object msg)
    {
        Debug.LogError("有新玩家进入牌桌");
        byte[] msgArray = msg as byte[];
        AckNewPlayerEnterRoom enterRoomMsg = AckNewPlayerEnterRoom.Parser.ParseFrom(msgArray);
        DDZPlayerData newPlayerData = new DDZPlayerData
        {
            Name = enterRoomMsg.PlayerInfo.AccountName,
            TotalCoins = enterRoomMsg.PlayerInfo.CoinNum
        };
        int seat = enterRoomMsg.PlayerInfo.Seat;
        Singleton<DDZGameData>.Instance.PlayersSeatDataDic[seat] = newPlayerData;
        _playerSeatDic[enterRoomMsg.PlayerInfo.Seat].gameObject.SetActive(true);
        _playerSeatDic[enterRoomMsg.PlayerInfo.Seat].InitPlayerViewWithData(Singleton<DDZGameData>.Instance.PlayersSeatDataDic[seat]);
    }
}
