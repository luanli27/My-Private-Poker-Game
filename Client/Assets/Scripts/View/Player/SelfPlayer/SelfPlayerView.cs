using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfPlayerView : MonoBehaviour
{
    public RemotePlayerInfoView InfoView;
    public RemotePlayerPlayView PlayView;
    public CallScoreView CallScoreView;
    public CardsLeftView CardsLeftView;
    public ReadyView ReadyView;

    private RemotePlayerInfoData _playerData;

    public void OnPlayerEnterRoom(RemotePlayerInfoData data)
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
}
