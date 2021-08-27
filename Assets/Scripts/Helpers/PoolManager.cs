using System;
using System.Collections;
using System.Collections.Generic;
class PoolService<T>
{
    private Queue<T> pool;

    public Action<T> OnGetObj;
    public Action<T> OnAddObj;

    public PoolService()
    {
        pool = new Queue<T>();
    }
    public void AddToPool(T obj)
    {
        pool.Enqueue(obj);
        OnAddObj?.Invoke(obj);
    }

    public T GetObj()
    {
        T obj = pool.Dequeue();
        OnGetObj?.Invoke(obj);
        return obj;
    }

}