using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDZGameData : Singleton<DDZGameData>
{
    private List<DDZPlayerData> _playersData;
    public List<DDZPlayerData> PlayersData
    {
        set
        {
            _playersData = value;
            SetMySeat();
        }
        get => _playersData;
    }

    public int MySeatIndex = -1;
    public int PlayerCount = 3;

    private void SetMySeat()
    {
        for (int playerIndex = 0; playerIndex < _playersData.Count; playerIndex++)
        {
            DDZPlayerData playerData = PlayersData[playerIndex];
            if (playerData.Name == Singleton<AccountData>.Instance.AccountName)
            {
                MySeatIndex = playerIndex;
                break;
            }
        }
    }
}
