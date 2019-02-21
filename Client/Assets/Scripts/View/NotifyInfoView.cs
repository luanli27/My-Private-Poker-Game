
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

class NotifyInfoView : MonoBehaviour
{
    public Text SampleText;
    public GameObject Layout;
    private List<Text> _textList;
    [SerializeField]
    private float _intervalTime = 0.3f;
    [SerializeField]
    private float _singleWordAnimTime = 0.5f;
    [SerializeField]
    private float _textOffset = 25f;

    public void Show(string text, bool show)
    {
        Layout.transform.DetachChildren();
        Layout.SetActive(show);
        SampleText.gameObject.SetActive(false);
        _textList = new List<Text>();
        if (show)
        {
            foreach (char singleChar in text)
            {
                GameObject go = new GameObject();
                GameObject child = new GameObject("childText");
                go.transform.SetParent(Layout.transform);
                go.AddComponent<RectTransform>();
                go.GetComponent<RectTransform>().sizeDelta = SampleText.rectTransform.sizeDelta;
                child.transform.SetParent(go.transform);
                Text textComponent = child.AddComponent<Text>();
                textComponent.text = singleChar.ToString();
                SetTextStyle(SampleText, textComponent);
                textComponent.transform.localPosition = new Vector3();
                _textList.Add(textComponent);
            }

            StartCoroutine(TextsAnimation());
        }
        else
            StopAllCoroutines();
    }

    private void SetTextStyle(Text sample, Text text)
    {
        text.transform.localScale = SampleText.transform.localScale;
        text.rectTransform.sizeDelta = sample.rectTransform.sizeDelta;
        text.color = SampleText.color;
        text.fontStyle = SampleText.fontStyle;
        text.font = sample.font;
        text.fontSize = sample.fontSize;
    }

    IEnumerator TextsAnimation()
    {
        for (int i = 0; i < _textList.Count; i++)
        {
            Vector3 localPos = _textList[i].transform.localPosition;
            Vector3 targetPos = new Vector3(localPos.x, localPos.y + _textOffset, localPos.z);
            _textList[i].transform.DOPunchPosition(targetPos, _singleWordAnimTime, 1);
            yield return new WaitForSeconds(_intervalTime);
            i = i == _textList.Count - 1 ? -1 : i;
        }
    }
}

