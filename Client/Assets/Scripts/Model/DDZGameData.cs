using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDZGameData : Singleton<DDZGameData>
{
    private Dictionary<int, DDZPlayerData> _playersSeatDataDic;
    public Dictionary<int, DDZPlayerData> PlayersSeatDataDic
    {
        set
        {
            _playersSeatDataDic = value;
            SetMySeat();
        }
        get => _playersSeatDataDic;
    }

    public int MySeatIndex = -1;
    public int PlayerCount = 3;

    private void SetMySeat()
    {
        foreach (var playerData in _playersSeatDataDic)
        {
            if (playerData.Value.Name == Singleton<AccountData>.Instance.AccountName)
            {
                MySeatIndex = playerData.Key;
                break;
            }
        }
    }
}
