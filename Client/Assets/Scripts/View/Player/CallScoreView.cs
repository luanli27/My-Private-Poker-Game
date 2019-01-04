using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallScoreView : MonoBehaviour
{
    public GameObject CallScore;
    public GameObject RefuseCallScore;

    public void SetCallScore(bool callScore)
    {
        CallScore.SetActive(callScore);
        RefuseCallScore.SetActive(!CallScore);
    }
}
