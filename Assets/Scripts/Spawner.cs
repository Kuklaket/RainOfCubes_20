using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T _prefab;
    [SerializeField] protected int _defaultCapacity = 5;
    [SerializeField] protected int _maxPoolSize = 5;
    [SerializeField] protected float _repeatRate = 1f;
    [SerializeField] protected Vector3 _spawnAreaSize = new(5f, 0f, 5f);

    protected HashSet<T> ActiveObjectsOnScene = new HashSet<T>();

    protected ObjectPool<T> _pool;

    protected virtual void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: CreateObject,
            actionOnGet: (obj) => obj.gameObject.SetActive(true),
            actionOnRelease: (obj) =>
            {
                ResetObject(obj);
                obj.gameObject.SetActive(false);
            },
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: true,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxPoolSize
        );
    }

    protected virtual void Start()
    {
        InvokeRepeating(nameof(SpawnObject), 0.0f, _repeatRate);
    }

    public int GetActiveElementsCount()
    {
        return ActiveObjectsOnScene.Count;
    }

    protected abstract T CreateObject();

    protected abstract void ResetObject(T obj);

    protected abstract void SpawnObject();

    protected Vector3 GetRandomPosition()
    {
        int halfDivider = 2;

        Vector3 randomPosition = new(
            Random.Range(-_spawnAreaSize.x / halfDivider, _spawnAreaSize.x / halfDivider),
            _spawnAreaSize.y,
            Random.Range(-_spawnAreaSize.z / halfDivider, _spawnAreaSize.z / halfDivider)
        );

        return randomPosition + transform.position;
    }
}