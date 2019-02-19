using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealCardsAnimator : MonoBehaviour
{
    public float InsertCardInterval;
    public HandCardsView HandCardsView;
    private float _shrinkDurationTime = 0.3f;
    private float _stretchDurationTime = 0.3f;
    private float _originalSpacing;

    public void DealCard(List<int> cards)
    {
        StartCoroutine(DealCardAnimation(cards));
    }

    private IEnumerator DealCardAnimation(List<int> cards)
    {
        yield return StartCoroutine(InsertAnimation(cards));
        yield return StartCoroutine(ShrinkHandCardsAnimation(_shrinkDurationTime));
        yield return StartCoroutine(StretchHandCardsAnimation(_stretchDurationTime));
    }

    private IEnumerator InsertAnimation(List<int> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            int cardId = cards[i];
            GameObject newCard = Instantiate(Resources.Load<GameObject>(Singleton<StringDefine>.Instance.BigCardResPath));
            newCard.transform.SetParent(HandCardsView.LayoutGroup.transform);
            PokerCardView cardView = newCard.GetComponent<PokerCardView>();
            cardView.SetCard(cardId, PokerScaleType.BIG);
            yield return new WaitForSeconds(InsertCardInterval);
        }
    }

    private IEnumerator ShrinkHandCardsAnimation(float durationTime)
    {
        _originalSpacing = HandCardsView.LayoutGroup.spacing;
        float originalDurationTime = durationTime;
        while (durationTime > 0)
        {
            HandCardsView.LayoutGroup.spacing = _originalSpacing * durationTime/originalDurationTime;
            yield return 0;
            durationTime -= Time.deltaTime;
        }

        HandCardsView.LayoutGroup.spacing = 0;
    }

    private IEnumerator StretchHandCardsAnimation(float durationTime)
    {
        float lastTime = 0;
        while (lastTime < durationTime)
        {
            HandCardsView.LayoutGroup.spacing = _originalSpacing * lastTime / durationTime;
            yield return 0;
            lastTime += Time.deltaTime;
        }

        HandCardsView.LayoutGroup.spacing = _originalSpacing;
    }

}
