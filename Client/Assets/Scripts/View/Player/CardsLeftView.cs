using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsLeftView : MonoBehaviour
{
    public GameObject LeftNumSingle;
    public GameObject LeftNumMultiple;
    public Image LeftNumSingleImg;
    public Image LeftNumMultipleLeftImg;
    public Image LeftNumMultipleRightImg;

    private readonly string _numResPath = "Ui\\artres\\功能\\斗地主\\jsq";

    public void SetLeftCardNum(int num)
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
            return;
        }
        else
        {
            resPath = _numResPath + num;
            Sprite numTexture = Resources.Load<Sprite>(resPath);
            if (numTexture == null)
            {
                Debug.LogError("并未在指定路径找到资源,路径为:" + resPath);
                return;
            }
            else
            {
                image.sprite = numTexture;
            }
        }
    }
}
