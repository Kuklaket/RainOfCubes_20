using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private BombSpawner _bombSpawner;

    private bool _isSpawning = true;

    private void Start()
    {
        _isSpawning = true;
        StartCoroutine(SpawnRoutine());
    }

    protected override Cube SpawnObject()
    {
        Cube cube = base.SpawnObject();
        cube.transform.position = GetRandomPosition();
        return cube;
    }

    protected override void ReleaseObject(Cube cube)
    {
        _bombSpawner.SpawnBombAtPosition(cube.transform.position);

        base.ReleaseObject(cube);
    }

    private IEnumerator SpawnRoutine()
    {
        while (_isSpawning)
        {
            SpawnObject();
            yield return new WaitForSeconds(RepeatRate);
        }
    }
}
