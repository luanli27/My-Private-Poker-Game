
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class NotifyInfoView : MonoBehaviour
{
    public Text SampleText;
    public HorizontalLayoutGroup Layout;
    private List<Text> _textList;
    [SerializeField]
    private float _intervalTime = 0.3f;
    [SerializeField]
    private float _singleWordAnimTime = 0.5f;

    void Start()
    {
        Show("测试用文字,看看到底好不好用", true);
    }

    public void Show(string text, bool show)
    {
        Layout.transform.DetachChildren();
        Layout.enabled = show;
        _textList = new List<Text>();
        if (show)
        {
            foreach (char singleChar in text)
            {
                GameObject go = new GameObject();
                Text textComponent = go.AddComponent<Text>();
                textComponent.text = singleChar.ToString();
                go.transform.SetParent(Layout.transform);
                _textList.Add(textComponent);
            }

            StartCoroutine(TextsAnimation());
        }
        else
            StopAllCoroutines();
    }

    IEnumerator TextsAnimation()
    {
        for (int i = 0; i < _textList.Count; i++)
        {
            Mathf.Lerp(0, 2, _intervalTime);
            yield return new WaitForSeconds(_intervalTime);
        }
    }

//    IEnumerator Spring(float durationTime, Text text)
//    {
//        float halfTime = durationTime / 2;
//        Vector3 localPos = text.transform.localPosition;
//        Vector3 templePos = text.transform.localPosition;
//        while (durationTime > 0)
//        {
//            templePos.y = localPos.y + (halfTime - durationTime) * 20;
//            text.transform.localPosition = 
//            //durationTime -= Time.deltaTime;
//            yield return 0;
//        }
//
//        text.transform.localPosition.Set(localPos.x, localPos.y, localPos.z);
//    }
}

