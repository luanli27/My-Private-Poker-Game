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
            UserName = Account.text
        };
        Singleton<AccountData>.Instance.AccountName = Account.text;
        Singleton<EventManager>.Instance.DispatchEvent(EventName.ASK_LOGIN, msg);
    }

    void OnEnterRoom(object msg)
    {
        byte[] msgArray = msg as byte[];
        AckEnterRoom enterRoomMsg = AckEnterRoom.Parser.ParseFrom(msgArray);
        List<DDZPlayerData> pDataList = new List<DDZPlayerData>();
        foreach (var pInfo in enterRoomMsg.PlayerInfo)
        {
            Debug.LogError("当前牌室内的玩家为:"+pInfo.UserName);
            DDZPlayerData playerData = new DDZPlayerData();
            playerData.Name = pInfo.UserName;
            playerData.TotalCoins = pInfo.GoldNum;
            pDataList.Add(playerData);
        }

        Singleton<DDZGameData>.Instance.PlayersData = pDataList;
        AsyncOperation ao = SceneManager.LoadSceneAsync("DDZGameScene");
        ao.completed += (operation) => { Debug.Log("<color=#00EEEE>" + "斗地主场景加载完毕" + "</color>");};
    }
}
