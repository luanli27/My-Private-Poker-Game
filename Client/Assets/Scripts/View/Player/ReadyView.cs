using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyView : MonoBehaviour
{
    public Image ReadyIcon;

    public void SetReady(bool isReady)
    {
        ReadyIcon.enabled = isReady;
    }
}
