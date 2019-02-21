using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownView : MonoBehaviour
{
    public GameObject LeftNumSingle;
    public GameObject LeftNumMultiple;
    public Image LeftNumSingleImg;
    public Image LeftNumMultipleLeftImg;
    public Image LeftNumMultipleRightImg;
    public Action CallbackWhenCountDownOver;

    public void StartCountDown(int seconds)
    {
        StartCoroutine(CountDown(seconds));
    }

    IEnumerator CountDown(int seconds)
    {
        while (seconds > 0)
        {
            SetLeftTime(seconds);
            yield return new WaitForSeconds(1);
            seconds--;
        }

        SetLeftTime(0);
        CallbackWhenCountDownOver?.Invoke();
    }

    private void SetLeftTime(int num)
    {
        if (num > 99 || num < 0)
            Debug.LogError("错误的剩余牌张数量！ 数量为:" + num);
        else if (num > 9)
        {
            LeftNumSingle.SetActive(false);
            LeftNumMultiple.SetActive(true);

            int leftNum = num / 10;
            int rightNum = num % 10;

            LoadNumberImage(leftNum, LeftNumMultipleLeftImg);
            LoadNumberImage(rightNum, LeftNumMultipleRightImg);
        }
        else
        {
            LeftNumMultiple.SetActive(false);
            LeftNumSingle.SetActive(true);
            LoadNumberImage(num, LeftNumSingleImg);
        }

    }

    private void LoadNumberImage(int num, Image image)
    {
        string resPath = "";
        if (num > 9 || num < 0)
        {
            Debug.LogError("错误的剩余牌张数量！ 数量为:" + num);
        }
        else
        {
            resPath = StringDefine.Instance.CountDownNumberRes + num;
            Sprite numTexture = Resources.Load<Sprite>(resPath);
            if (numTexture == null)
            {
                Debug.LogError("并未在指定路径找到资源,路径为:" + resPath);
            }
            else
            {
                image.sprite = numTexture;
            }
        }
    }
}
