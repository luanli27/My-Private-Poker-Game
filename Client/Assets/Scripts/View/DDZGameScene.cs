using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class DDZGameScene : MonoBehaviour
{
    public RemotePlayerView LeftPlayView;
    public RemotePlayerView RightPlayView;
    public SelfPlayerView LocalPlayerView;
    public DealCardsAnimator DealCardsAnimator;
    public NotifyInfoView NotifyInfoView;

    private Dictionary<int, DDZPlayerBase> _playerSeatDic = new Dictionary<int, DDZPlayerBase>(); 

    void Start()
    {
        InitGameScene();
        RegisterEvents();
        ReadyForStartGame();
    }

    private void InitGameScene()
    {
        LeftPlayView.gameObject.SetActive(false);
        RightPlayView.gameObject.SetActive(false);
        LocalPlayerView.gameObject.SetActive(false);
        MapSeatWithPlayerView();
        NotifyInfoView.Show("等待其他玩家进入牌桌中...", true);

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
        EventManager.Instance.AddEventListener(EventName.ACK_NEW_PLAYER_ENTER_ROOM, OnNewPlayerEnterRoom);
        EventManager.Instance.AddEventListener(EventName.ACK_POKER_GAME_START, GameStartHandler);
        EventManager.Instance.AddEventListener(EventName.ACK_DEAL_CARDS, DealCardsHandler);
        EventManager.Instance.AddEventListener(EventName.ACK_CALL_LORD, CallLordHandler);
    }
    
    //进入场景自动发送准备好开始游戏的消息

    private void ReadyForStartGame()
    {
        NetworkManager.Instance.SendMsg(MessageDefine.C2G_REQ_READY_FOR_START, new ReqReadyForStartGame{Ready = true});
    }

    private void MapSeatWithPlayerView()
    {
        int playerNum = DDZGameData.Instance.PlayerCount;
        int mySeat = DDZGameData.Instance.MySeatIndex;
        _playerSeatDic[mySeat] = LocalPlayerView;
        int rightSeat = (mySeat + 1) % playerNum;
        int leftSeat = (mySeat + 2) % playerNum;
        _playerSeatDic[rightSeat] = RightPlayView;
        _playerSeatDic[leftSeat] = LeftPlayView;
    }

    private void OnNewPlayerEnterRoom(object msg)
    {
        Debug.LogError("有新玩家进入牌桌");
        AckNewPlayerEnterRoom enterRoomMsg = msg as AckNewPlayerEnterRoom;
        DDZPlayerData newPlayerData = new DDZPlayerData
        {
            Name = enterRoomMsg.PlayerInfo.AccountName,
            TotalCoins = enterRoomMsg.PlayerInfo.CoinNum
        };
        int seat = enterRoomMsg.PlayerInfo.Seat;
        DDZGameData.Instance.PlayersSeatDataDic[seat] = newPlayerData;
        _playerSeatDic[enterRoomMsg.PlayerInfo.Seat].gameObject.SetActive(true);
        _playerSeatDic[enterRoomMsg.PlayerInfo.Seat].InitPlayerViewWithData(Singleton<DDZGameData>.Instance.PlayersSeatDataDic[seat]);
    }

    private void GameStartHandler(object msg)
    {
        NotifyInfoView.Show("", false);
    }

    private void DealCardsHandler(object msg)
    {
        if (msg is DealCard dealCardMsg)
        {
            foreach (var kv in _playerSeatDic)
            {
                DDZPlayerBase player = kv.Value;
                player.OnGameBegin();
            }

            List<int> localPlayerHandCards = dealCardMsg.HandCards.ToList();
            DealCardsAnimator.DealCard(localPlayerHandCards);
            DDZGameData.Instance.PlayersSeatDataDic[DDZGameData.Instance.MySeatIndex].HandCards = localPlayerHandCards;
            string cardInfo = "";
            foreach (int cardId in localPlayerHandCards)
            {
                cardInfo += cardId + ",";
            }
            Debug.LogError("收到服务器发送的发牌信息, 本地玩家的手牌数据为:" + cardInfo);
        }
    }

    private void CallLordHandler(object msg)
    {
        if (msg is CallLordMsg callLordMsg)
        {
            _playerSeatDic[callLordMsg.CurrentCallSeat].OnCallLord(callLordMsg.WaitTime, (CallLord)callLordMsg.CurrentCallState);
            for (int i = 0; i < callLordMsg.CallLordResultSeats.Count(); i++)
            {
                int seat = callLordMsg.CallLordResultSeats.ToList()[i];
                int result = callLordMsg.CallLordResults.ToList()[i];
                _playerSeatDic[seat].OnCallLordOver((CallLord)result);
            }

            Debug.LogError("收到服务器发送的叫地主信息");
        }
    }
}
