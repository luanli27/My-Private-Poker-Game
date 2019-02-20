using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : Singleton<ObjectPool<T>> where T : RecycleAbleInterface<T>, new()
{
    private Queue<T> _pool = new Queue<T>();
    public T SpawnObject()
    {
        T result = _pool.Count > 0 ? _pool.Dequeue() : new T().Spawn();
        return result;
    }

    public void BackToPool(T uselessObj)
    {
        uselessObj.Reset();
        _pool.Enqueue(uselessObj);
    }
}
