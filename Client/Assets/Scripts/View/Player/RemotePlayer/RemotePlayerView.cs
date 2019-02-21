using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class RemotePlayerView : DDZPlayerBase
{
    public RemotePlayerInfoView InfoView;
    public RemotePlayerPlayView PlayView;
    public CardsLeftView CardsLeftView;
    public ReadyView ReadyView;

    private DDZPlayerData _playerData;

    public override void InitPlayerViewWithData(DDZPlayerData data)
    {
        _playerData = data;
        InfoView.SetPlayerInfo(_playerData);
        InfoView.gameObject.SetActive(true);
        PlayView.gameObject.SetActive(false);
        CardsLeftView.gameObject.SetActive(false);
        ReadyView.gameObject.SetActive(true);
        ReadyView.SetReady(true);
    }

    public override void OnGameBegin()
    {
        ReadyView.gameObject.SetActive(false);
    }

    public override void PlayCards(List<int> cards)
    {

    }

    public override void OnCallLord(int waitTime, CallLordState state)
    {

    }
}
