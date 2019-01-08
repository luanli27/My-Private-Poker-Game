using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SelfPlayerView : DDZPlayerBase
{
    public RemotePlayerInfoView InfoView;
    public RemotePlayerPlayView PlayView;
    public CallScoreView CallScoreView;
    public ReadyView ReadyView;
    public HandCardsView HandCardsView;
    public OptionView OptionView;

    private DDZPlayerData _playerData;

    public override void OnPlayerEnterRoom(DDZPlayerData data)
    {
        _playerData = data;
        InfoView.SetPlayerInfo(_playerData);
        InfoView.gameObject.SetActive(true);
        PlayView.gameObject.SetActive(false);
        CallScoreView.gameObject.SetActive(false);
        ReadyView.gameObject.SetActive(true);
        ReadyView.SetReady(true);
        HandCardsView.gameObject.SetActive(false);
        OptionView.gameObject.SetActive(false);
    }

    public override void PlayCards(List<int> cards)
    {

    }
}
