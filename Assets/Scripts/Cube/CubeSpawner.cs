using System;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private BombSpawner _bombSpawner;

    public event Action CubeSpawned;
    public event Action CubeCreated;
    public event Action ActiveCubeCountUpdated;

    protected override Cube CreateObject()
    {
        Cube cube = Instantiate(_prefab);
        cube.Released += CompleteCube;
        CubeCreated.Invoke();
        return cube;
    }

    protected override void ResetObject(Cube cube)
    {
        ActiveObjectsOnScene.Remove(cube);
        ActiveCubeCountUpdated.Invoke();
        cube.ResetState();
    }

    protected override void SpawnObject()
    {
        Cube cube = _pool.Get();
        ActiveObjectsOnScene.Add(cube);
        ActiveCubeCountUpdated.Invoke();
        CubeSpawned.Invoke();
        cube.transform.position = GetRandomPosition();
    }

    private void CompleteCube(Cube cube)
    {
        _bombSpawner.SpawnBombAtPosition(cube.transform.position);

        _pool.Release(cube);
    }

    private void OnDestroy()
    {
        foreach (var cube in ActiveObjectsOnScene)
        {
            cube.Released -= CompleteCube;
        }
    }
}