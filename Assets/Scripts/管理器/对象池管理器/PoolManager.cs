using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : Singleton<PoolManager>
{
    private Dictionary<Component, object> poolDictionary = new();

    /// <summary>
    /// 释放对象
    /// </summary>
    /// <param name="prefab">预制体</param>
    /// <param name="position">位置</param>
    /// <param name="rotation">旋转</param>
    public T Spawn<T>(T prefab, Vector3 position, Quaternion rotation, bool autoActive = true) where T : PoolableObject<T>
    {
        if (!poolDictionary.TryGetValue(prefab, out var poolObj))
        {
            poolObj = CreatePool(prefab);
            poolDictionary.Add(prefab, poolObj);
        }

        var pool = (ObjectPool<T>)poolObj;
        T obj = pool.Get();

        obj.transform.SetPositionAndRotation(position, rotation);

        if (autoActive)
            Activate(obj);

        return obj;
    }

    /// <summary>
    /// 生成池
    /// </summary>
    private ObjectPool<T> CreatePool<T>(T prefab) where T : PoolableObject<T>
    {
        ObjectPool<T> pool = null;

        pool = new ObjectPool<T>(
                createFunc: () =>
                {
                    T obj = Instantiate(prefab);

                    if(obj is PoolableObject<T> poolable)
                    {
                        poolable.SetPool(pool);
                    }
                    obj.gameObject.SetActive(false);
                    return obj;
                },
                actionOnRelease: obj => GameOnRelease(obj),
                actionOnDestroy: obj => Destroy(obj.gameObject),
                collectionCheck: true,
                defaultCapacity: 50,
                maxSize: 200
            );
        return pool;
    }

    /// <summary>
    /// 激活时调用
    /// </summary>
    /// <param name="obj">对象</param>
    public void Activate<T>(T obj) where T : PoolableObject<T>
    {
        if(obj is PoolableObject<T> poolable)
        {
            poolable.OnSpawn();
        }
        obj.gameObject.SetActive(true);
    }

    /// <summary>
    /// 回收时调用
    /// </summary>
    /// <param name="obj">对象</param>
    public void GameOnRelease<T>(T obj) where T : PoolableObject<T>
    {
        if(obj is PoolableObject<T> poolable)
        {
            poolable.OnDespawn();
        }
        obj.gameObject.SetActive(false);
    }
}
