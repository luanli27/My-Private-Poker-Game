using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class DDZPlayerBase : MonoBehaviour
{
    public abstract void PlayCards(List<int> cards);
    public abstract void InitPlayerViewWithData(DDZPlayerData data);
    public abstract void OnGameBegin();
} 
