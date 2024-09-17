using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GameObjectPool<T>
    where T : MonoBehaviour
{
    private T _prefab;
    private ObjectPool<T> _pool;

    public GameObjectPool(T prefab)
    {
        _prefab = prefab;
        _pool = new ObjectPool<T>(OnPoolCreate, OnPoolTake, OnPoolReturn, OnPoolDestroy, true, 30, 100);
        WarmUpPool();
    }
    public T GetItem()
    {
        return _pool.Get();
    }
    public void ReturnItem(T item)
    {
        _pool.Release(item);
    }

    private void WarmUpPool()
    {
        List<T> items = new(10);

        for (int i = 0; i < items.Count; i++)
        {
            items.Add(_pool.Get());
        }

        for (int i = 0; i < items.Count; i++)
        {
            _pool.Release(items[i]);
        }
    }
    private T OnPoolCreate()
    {
        T item = GameObject.Instantiate(_prefab);
        item.gameObject.SetActive(false);
        return item;
    }
    private void OnPoolTake(T item)
    {
        
    }
    private void OnPoolReturn(T item)
    {
        item.gameObject.SetActive(false);
    }
    private void OnPoolDestroy(T item)
    {
        GameObject.Destroy(item);
    }
    
}
