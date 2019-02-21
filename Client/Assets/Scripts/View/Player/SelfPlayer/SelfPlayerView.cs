using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SelfPlayerView : DDZPlayerBase
{
    public RemotePlayerInfoView InfoView;
    public RemotePlayerPlayView PlayView;
    public ReadyView ReadyView;
    public HandCardsView HandCardsView;
    public OperationCardView OperationCardView;
    public CallLordView CallLordView;

    private DDZPlayerData _playerData;

    public override void InitPlayerViewWithData(DDZPlayerData data)
    {
        _playerData = data;
        InfoView.SetPlayerInfo(_playerData);
        InfoView.gameObject.SetActive(true);
        PlayView.gameObject.SetActive(false);
        ReadyView.gameObject.SetActive(true);
        ReadyView.SetReady(true);
        HandCardsView.gameObject.SetActive(false);
        OperationCardView.gameObject.SetActive(false);
        CallLordView.gameObject.SetActive(false);
    }

    public override void OnGameBegin()
    {
        ReadyView.gameObject.SetActive(false);
        HandCardsView.gameObject.SetActive(true);
    }

    public override void PlayCards(List<int> cards)
    {

    }
    public override void OnCallLord(int waitTime, CallLordState state)
    {
        CallLordView.gameObject.SetActive(true);
        CallLordView.SetCallLordState(state, waitTime);
    }
}
