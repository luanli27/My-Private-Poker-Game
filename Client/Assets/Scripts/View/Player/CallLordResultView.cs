using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallLordResultView : MonoBehaviour
{
    public GameObject CallLordReult;
    public GameObject GiveUpCallLordResult;
    public GameObject GrapLordResult;

    public void SetCallLordResult(CallLordResultState state)
    {
        CallLordReult.SetActive(false);
        GiveUpCallLordResult.SetActive(false);
        GrapLordResult.SetActive(false);

        switch (state)
        {
            case CallLordResultState.CALL_LORD:
                CallLordReult.SetActive(true);
                break;
            case CallLordResultState.GIVE_UP_CALL_LORD:
                GiveUpCallLordResult.SetActive(false);
                break;
            case CallLordResultState.GRAP_LORD:
                GrapLordResult.SetActive(true);
                break;
        }
    }
}
