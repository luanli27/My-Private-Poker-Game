using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class RemotePlayerView : DDZPlayerBase
{
    public RemotePlayerInfoView InfoView;
    public RemotePlayerPlayView PlayView;
    public CallScoreView CallScoreView;
    public CardsLeftView CardsLeftView;
    public ReadyView ReadyView;

    private DDZPlayerData _playerData;

    public override void OnPlayerEnterRoom(DDZPlayerData data)
    {
        _playerData = data;
        InfoView.SetPlayerInfo(_playerData);
        InfoView.gameObject.SetActive(true);
        PlayView.gameObject.SetActive(false);
        CallScoreView.gameObject.SetActive(false);
        CardsLeftView.gameObject.SetActive(false);
        ReadyView.gameObject.SetActive(true);
        ReadyView.SetReady(true);
    }

    public override void PlayCards(List<int> cards)
    {

    }
}
