using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokerCardView : MonoBehaviour, RecycleAbleInterface
{
    public Image PokerNumImg;
    public Image PokerSuitImg;

    public void SetCard(int cardId, PokerScaleType scaleType)
    {
        PokerNumImg.sprite = Resources.Load<Sprite>(PokerGameTool.Instance.GetPokerKeyRes(cardId, scaleType));
        //joker card don't need show suit image
        string suitRes = PokerGameTool.Instance.GetPokerSuitRes(cardId);
        PokerSuitImg.enabled = suitRes != "";
        PokerSuitImg.sprite = Resources.Load<Sprite>(suitRes);
    }

    public void Reset()
    {

    }
}
