using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginUi : MonoBehaviour {

    public Button LoginButton;
    public InputField Account;
    public InputField Password;
	// Use this for initialization
	void Start ()
	{
        LoginButton.onClick.AddListener(OnRequeseLogin);
        Singleton<EventManager>.Instance.AddEventListener(EventName.ACK_ENTER_ROOM, OnEnterRoom);
	}

    void OnRequeseLogin()
    {
        ReqLogin msg = new ReqLogin
        {
            AccountName = Account.text
        };
        Singleton<AccountData>.Instance.AccountName = Account.text;
        Singleton<EventManager>.Instance.DispatchEvent(EventName.ASK_LOGIN, msg);
    }

    void OnEnterRoom(object msg)
    {
        byte[] msgArray = msg as byte[];
        AckEnterRoomResult enterRoomMsg = AckEnterRoomResult.Parser.ParseFrom(msgArray);
        Dictionary<int, DDZPlayerData> pSeatDataDic = new Dictionary<int, DDZPlayerData>();
        foreach (var pInfo in enterRoomMsg.PlayerInfos)
        {
            Debug.LogError("当前牌室内的玩家为:"+pInfo.AccountName);
            DDZPlayerData playerData = new DDZPlayerData();
            playerData.Name = pInfo.AccountName;
            playerData.TotalCoins = pInfo.CoinNum;
            pSeatDataDic[pInfo.Seat] = playerData;
        }

        Singleton<DDZGameData>.Instance.PlayersSeatDataDic = pSeatDataDic;
        AsyncOperation ao = SceneManager.LoadSceneAsync("DDZGameScene");
        ao.completed += (operation) => { Debug.Log("<color=#00EEEE>" + "斗地主场景加载完毕" + "</color>");};
    }
}
