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
    }

    private void InitGameScene()
    {
        LeftPlayView.gameObject.SetActive(false);
        RightPlayView.gameObject.SetActive(false);
        SelfPlayerView.gameObject.SetActive(false);
        MapSeatWithPlayerView();

        foreach (var playerItem in _playerSeatDic)
        {
            playerItem.Value.gameObject.SetActive(true);
            playerItem.Value.OnPlayerEnterRoom(Singleton<DDZGameData>.Instance.PlayersData[playerItem.Key]);
        }
    }

    private void MapSeatWithPlayerView()
    {
        List<DDZPlayerData> playersData = Singleton<DDZGameData>.Instance.PlayersData;
        int playerNum = Singleton<DDZGameData>.Instance.PlayerCount;
        int mySeat = Singleton<DDZGameData>.Instance.MySeatIndex;
        _playerSeatDic[mySeat] = SelfPlayerView;
        int rightSeat = (mySeat + 1) % playerNum;
        int leftSeat = (mySeat + 2) % playerNum;

        if (rightSeat < playersData.Count)
            _playerSeatDic[rightSeat] = RightPlayView;
        if (leftSeat < playersData.Count)
            _playerSeatDic[leftSeat] = LeftPlayView;
    }
}
