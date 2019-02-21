
using System.Collections.Generic;
using UnityEditor;

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

    public List<int> SortCards(List<int> cards, bool reverse = false)
    {
        List<int> result = QuickSort(cards, 0, cards.Count - 1);
        if (reverse)
            result.Reverse();

        return result;
    }

    private List<int> QuickSort(List<int> cards, int left, int right)
    {
        //order redJoker,blackJoker,2-3
        int finalIndex = PartSort(cards, left, right);
        if (finalIndex > left)
            QuickSort(cards, left, finalIndex - 1);

        if (finalIndex < right)
            QuickSort(cards, finalIndex + 1, right);

        return cards;
    }

    private int PartSort(List<int> cards, int left, int right)
    {
        int baseIndex = left;
        float baseOrderValue = GetCardOrderValue(cards[baseIndex]);
        while (left < right)
        {
            float rightOrderValue = GetCardOrderValue(cards[right]);
            while (rightOrderValue > baseOrderValue && right > left)
            {
                right--;
                rightOrderValue = GetCardOrderValue(cards[right]);
            }

            float leftOrderValue = GetCardOrderValue(cards[left]);
            while (leftOrderValue <= baseOrderValue && left < right)
            {
                left++;
                leftOrderValue = GetCardOrderValue(cards[left]);
            }

            int temp = cards[left];
            cards[left] = cards[right];
            cards[right] = temp;
        }

        int temple = cards[baseIndex];
        cards[baseIndex] = cards[right];
        cards[right] = temple;
        return right;
    }

    /*only use this value to compare two cards order priority
      redJoker must be the biggest, 3 is smallest
      ->RedJoker,BlackJoker,2,A,K-2<-
      same key should be ordered as club, diamend, heart,spade
     */

    private float GetCardOrderValue(int cardId)
    {
        /*
         * redJoker-3 Refers 16-2;
         */
        float result;
        int originalKey = GetCardKey(cardId);
        int key = originalKey <= 1 ? originalKey + 13 : originalKey;
        PokerCardSuit suit = GetCardSuit(cardId);
        if (suit == PokerCardSuit.JOKER)
            result = 15 + key + (int)suit*0.1f;
        else
            result = key + (int) suit * 0.1f;

        return result;
    }
}
