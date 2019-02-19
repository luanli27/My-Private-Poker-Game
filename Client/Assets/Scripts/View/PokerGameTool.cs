using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokerGameTool : Singleton<PokerGameTool>
{
    public PokerCardSuit GetCardSuit(int cardId)
    {
        return (PokerCardSuit)(cardId/13);
    }

    public int GetCardKey(int cardId)
    {
        return cardId%13;
    }

    public string GetPokerKeyRes(int cardId, PokerScaleType scaleType)
    {
        string result = StringDefine.Instance.PokerResDir;
        int cardKey = GetCardKey(cardId);
        PokerCardSuit cardSuit = GetCardSuit(cardId);

        if (cardSuit == PokerCardSuit.JOKER)
        {
            string jokerStr = cardKey == 0 ? "black_joker_" : "red_joker_";
            string scaleStr = "";
            switch (scaleType)
            {
                case PokerScaleType.TINY:
                    scaleStr = "tiny";
                    break;
                case PokerScaleType.MIDDLE:
                    scaleStr = "middle";
                    break;
                case PokerScaleType.BIG:
                    scaleStr = "big";
                    break;
            }

            result += jokerStr + scaleStr;
        }
        //spade and club show black number image, heart and diamend show red image
        else
        {
            //A-K refers 0-12 in logic, but refers 1-13 in image res
            if (cardSuit == PokerCardSuit.HEART || cardSuit == PokerCardSuit.DIAMEND)
                result += "r" + (cardKey + 1);
            else
                result += cardKey + 1;
        }

        return result;
    }

    public string GetPokerSuitRes(int cardId)
    {
        string result = StringDefine.Instance.PokerResDir;
        PokerCardSuit cardSuit = GetCardSuit(cardId);
        string suitStr = "";
        switch (cardSuit)
        {
            case PokerCardSuit.SPADE:
                suitStr = "spade";
                break;
            case PokerCardSuit.HEART:
                suitStr = "heart";
                break;
            case PokerCardSuit.DIAMEND:
                suitStr = "diamend";
                break;
            case PokerCardSuit.CLUB:
                suitStr = "club";
                break;
            //Notice that joker card do not need suit image
            case PokerCardSuit.JOKER:
                result = "";
                break;
        }

        result += suitStr;
        return result;
    }
}
