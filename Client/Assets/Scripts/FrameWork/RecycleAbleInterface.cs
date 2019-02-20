using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface RecycleAbleInterface<T>
{
    void Reset();
    T Spawn();
}
