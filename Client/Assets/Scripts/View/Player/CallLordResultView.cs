using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallLordResultView : MonoBehaviour
{
    //目前缺少不叫的图片资源
    public GameObject CallLordReult;
    public GameObject GiveUpCallLordResult;
    public GameObject GrapLordResult;

    public void SetCallLordResult(CallLord state)
    {
        CallLordReult.SetActive(false);
        GiveUpCallLordResult.SetActive(false);
        GrapLordResult.SetActive(false);

        switch (state)
        {
            case CallLord.CALL_LORD:
                CallLordReult.SetActive(true);
                break;
            case CallLord.GIVE_UP_CALL_LORD:
                GiveUpCallLordResult.SetActive(true);
                break;
            case CallLord.GRAP_LORD:
                GrapLordResult.SetActive(true);
                break;
            case CallLord.GIVE_UP_GRAP_LORD:
                GiveUpCallLordResult.SetActive(true);
                break;
        }
    }
}
