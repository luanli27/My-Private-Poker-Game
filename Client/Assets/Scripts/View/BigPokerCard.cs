using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigPokerCard : Object, RecycleAbleInterface<BigPokerCard>
{
    public GameObject BigPokerCardObj;
    public void Reset()
    {

    }

    public BigPokerCard Spawn()
    {
        BigPokerCardObj = Instantiate(Resources.Load<GameObject>(Singleton<StringDefine>.Instance.BigCardResPath));
        return this;
    }
}
