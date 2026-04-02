using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour, ISpawner where T : MonoBehaviour, IPoolable
{
    [SerializeField] protected T Prefab;
    [SerializeField] protected int DefaultCapacity = 5;
    [SerializeField] protected int MaxPoolSize = 5;
    [SerializeField] protected float RepeatRate = 1f;
    [SerializeField] protected Vector3 SpawnAreaSize = new(5f, 0f, 5f);

    public event Action ObjectSpawned;
    public event Action ObjectCreated;
    public event Action ActiveObjectCountUpdated;

    protected HashSet<T> ActiveObjectsOnScene = new HashSet<T>();
    protected ObjectPool<T> _pool;

    protected virtual void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: CreateObject,
            actionOnGet: GetObject,
            actionOnRelease: ReleaseObject,
            actionOnDestroy: (item) => Destroy(item.gameObject),
            collectionCheck: true,
            defaultCapacity: DefaultCapacity,
            maxSize: MaxPoolSize
        );
    }

    private void OnDisable()
    {
        foreach (var item in ActiveObjectsOnScene)
        {
            UnsubscribeFromReturnEvent(item);
        }
    }

    public int GetActiveElementsCount()
    {
        return ActiveObjectsOnScene.Count;
    }

    protected virtual T CreateObject()
    {
        T item = Instantiate(Prefab);
        SubscribeToReturnEvent(item);
        ObjectCreated?.Invoke();
        return item;
    }

    protected virtual void GetObject(T item)
    {
        item.gameObject.SetActive(true);
    }

    protected virtual void ReleaseObject(T item)
    {
        item.ResetState();
        item.gameObject.SetActive(false);
        ActiveObjectsOnScene.Remove(item);
        ActiveObjectCountUpdated?.Invoke();
    }

    protected virtual T SpawnObject()
    {
        T item = _pool.Get();
        ActiveObjectsOnScene.Add(item);
        ActiveObjectCountUpdated?.Invoke();
        ObjectSpawned?.Invoke();
        return item;
    }
    protected Vector3 GetRandomPosition()
    {
        int halfDivider = 2;

        Vector3 randomPosition = new(
            UnityEngine.Random.Range(-SpawnAreaSize.x / halfDivider, SpawnAreaSize.x / halfDivider),
            SpawnAreaSize.y,
            UnityEngine.Random.Range(-SpawnAreaSize.z / halfDivider, SpawnAreaSize.z / halfDivider)
        );

        return randomPosition + transform.position;
    }

    private void SubscribeToReturnEvent(T item)
    {
        item.ReturnToPoolRequested += HandleReturnToPool;
    }

    private void UnsubscribeFromReturnEvent(T item)
    {
        item.ReturnToPoolRequested -= HandleReturnToPool;
    }

    private void HandleReturnToPool(IPoolable poolable)
    {
        T item = poolable as T;

        if (item != null)
        {
            _pool.Release(item);
        }
    }
}