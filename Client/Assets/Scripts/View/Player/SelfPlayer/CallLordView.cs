using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallLordView : MonoBehaviour
{
    public CallLordResultView CallLordResultView;
    public GameObject CallLordButtonGroup;
    public GameObject GrapLordButtonGroup;
    public CountDownView CountDownView;

    public void SetCallLordState(CallLordState state, int waitTime)
    {
        gameObject.SetActive(true);
        CountDownView.gameObject.SetActive(true);
        CallLordResultView.gameObject.SetActive(false);
        switch (state)
        {
            case global::CallLordState.CALL_LORD:
                CallLordButtonGroup.SetActive(true);
                GrapLordButtonGroup.SetActive(false);
                break;
            case global::CallLordState.GRAP_LORD:
                CallLordButtonGroup.SetActive(false);
                GrapLordButtonGroup.SetActive(!true);
                break;
        }

        CountDownView.StartCountDown(waitTime);
    }

    private void SetCallLordResult(CallLordResultState result)
    {
        CallLordButtonGroup.SetActive(false);
        GrapLordButtonGroup.SetActive(false);
        CountDownView.gameObject.SetActive(false);
        CallLordResultView.gameObject.SetActive(true);
        CallLordResultView.SetCallLordResult(result);
    }
}
