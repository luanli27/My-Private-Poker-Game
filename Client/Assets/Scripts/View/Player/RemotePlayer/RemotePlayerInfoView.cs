using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemotePlayerInfoView : MonoBehaviour
{
    public Image HeadIcon;
    public Text PlayerName;
    public Text CoinNum;
    public Image LordFlag;


    public void SetPlayerInfo(DDZPlayerData data)
    {
        //HeadIcon.material.mainTexture = data.HeadIcon;
        PlayerName.text = data.Name;
        CoinNum.text = data.TotalCoins.ToString();
        LordFlag.enabled = false;
    }

    public void SetLord(bool isLord)
    {
        LordFlag.enabled = isLord;
    }
}
