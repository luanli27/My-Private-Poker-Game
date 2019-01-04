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


    public void SetPlayerInfo(RemotePlayerInfoData data)
    {
        HeadIcon.material.mainTexture = data.HeadIcon;
        PlayerName.text = data.PlayerName;
        CoinNum.text = data.CoinNum.ToString();
        LordFlag.enabled = false;
    }

    public void SetLord(bool isLord)
    {
        LordFlag.enabled = isLord;
    }
}
