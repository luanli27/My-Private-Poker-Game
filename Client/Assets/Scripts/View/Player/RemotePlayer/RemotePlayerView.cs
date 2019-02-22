using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class RemotePlayerView : DDZPlayerBase
{
    public RemotePlayerInfoView InfoView;
    public RemotePlayerPlayView PlayView;
    public CardsLeftView CardsLeftView;
    public ReadyView ReadyView;
    public CallLordResultView CallLordResultView;
    public CountDownView CountDownView;

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
        CallLordResultView.gameObject.SetActive(false);
        CountDownView.gameObject.SetActive(false);
    }

    public override void OnGameBegin()
    {
        ReadyView.gameObject.SetActive(false);
    }

    public override void PlayCards(List<int> cards)
    {

    }

    public override void OnCallLord(int waitTime, CallLord state)
    {
        CountDownView.gameObject.SetActive(true);
        CountDownView.StartCountDown(waitTime);
    }

    public override void OnCallLordOver(CallLord state)
    {
        CallLordResultView.gameObject.SetActive(true);
        CallLordResultView.SetCallLordResult(state);
        CountDownView.gameObject.SetActive(false);
    }
}
