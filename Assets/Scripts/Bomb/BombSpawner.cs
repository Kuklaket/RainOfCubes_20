using System;
using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    private Vector3 _position;
    private bool _hasPendingSpawn = false;

    public event Action BombSpawned;
    public event Action BombCreated;
    public event Action ActiveBombCountUpdated;

    public void SpawnBombAtPosition(Vector3 position)
    {
        _position = position;
        _hasPendingSpawn = true;
        SpawnObject();
    }

    protected override Bomb CreateObject()
    {
        Bomb bomb = Instantiate(_prefab);
        BombCreated.Invoke();
        bomb.ExplosionCompleted += ReleaseBomb;
        return bomb;
    }

    protected override void ResetObject(Bomb bomb)
    {
        ActiveObjectsOnScene.Remove(bomb);
        bomb.ResetState();
    }

    protected override void SpawnObject()
    {
        if (!_hasPendingSpawn) return;

        Bomb bomb = _pool.Get();
        BombSpawned.Invoke();
        ActiveObjectsOnScene.Add(bomb);
        ActiveBombCountUpdated.Invoke();
        bomb.transform.position = _position;
        _hasPendingSpawn = false;
    }

    private void ReleaseBomb(Bomb bomb)
    {
        _pool.Release(bomb);
    }

    private void OnDestroy()
    {
        foreach (var bomb in ActiveObjectsOnScene)
        {
            bomb.ExplosionCompleted -= ReleaseBomb;
        }
    }
}