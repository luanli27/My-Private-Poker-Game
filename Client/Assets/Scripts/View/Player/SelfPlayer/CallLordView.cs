using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallLordView : MonoBehaviour
{
    public CallLordResultView CallLordResultView;
    public GameObject CallLordButtonGroup;
    public GameObject GrapLordButtonGroup;
    public CountDownView CountDownView;
    [SerializeField] private Button _callLordButton;
    [SerializeField] private Button _giveUpCallLordButton;
    [SerializeField] private Button _grapLordButton;
    [SerializeField] private Button _giveUpGrapLordButton;

    public void SetCallLordState(CallLord state, int waitTime)
    {
        gameObject.SetActive(true);
        CountDownView.gameObject.SetActive(true);
        CallLordResultView.gameObject.SetActive(false);
        switch (state)
        {
            case CallLord.CALL_LORD:
                CallLordButtonGroup.SetActive(true);
                GrapLordButtonGroup.SetActive(false);
                break;
            case CallLord.GRAP_LORD:
                CallLordButtonGroup.SetActive(false);
                GrapLordButtonGroup.SetActive(true);
                break;
        }

        CountDownView.StartCountDown(waitTime);
    }

    void Start()
    {
        _callLordButton.onClick.AddListener(() => { ReqCallLord(CallLord.CALL_LORD);});
        _giveUpCallLordButton.onClick.AddListener(() => { ReqCallLord(CallLord.GIVE_UP_CALL_LORD);});
        _grapLordButton.onClick.AddListener(() => { ReqCallLord(CallLord.GRAP_LORD); });
        _giveUpGrapLordButton.onClick.AddListener(() => { ReqCallLord(CallLord.GIVE_UP_GRAP_LORD); });
    }

    public void SetCallLordResult(CallLord result)
    {
        CallLordButtonGroup.SetActive(false);
        GrapLordButtonGroup.SetActive(false);
        CountDownView.gameObject.SetActive(false);
        CallLordResultView.gameObject.SetActive(true);
        CallLordResultView.SetCallLordResult(result);
    }

    private void ReqCallLord(CallLord result)
    {
         NetworkManager.Instance.SendMsg(MessageDefine.C2G_REQ_CALL_LORD, new ResponseCallLordMsg{Seat = DDZGameData.Instance.MySeatIndex, Result = (int)result});
    }
}
