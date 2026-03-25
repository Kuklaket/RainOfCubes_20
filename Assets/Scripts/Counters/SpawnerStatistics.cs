using UnityEngine;

public class SpawnerStatistics : MonoBehaviour
{
    [SerializeField] private TrackedObjectStats<Cube> _cubeStatistics;
    [SerializeField] private TrackedObjectStats<Bomb> _bombStatistics;
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private BombSpawner _bombSpawner;

    private void OnEnable()
    {
        if (_cubeSpawner != null)
        {
            _cubeSpawner.CubeSpawned += _cubeStatistics.IncrementSpawnCount;
            _cubeSpawner.CubeCreated += _cubeStatistics.IncrementCreationCount;
            _cubeSpawner.ActiveCubeCountUpdated += UpdateCubeActiveCount;
        }

        if (_bombSpawner != null)
        {
            _bombSpawner.BombSpawned += _bombStatistics.IncrementSpawnCount;
            _bombSpawner.BombCreated += _bombStatistics.IncrementCreationCount;
            _bombSpawner.ActiveBombCountUpdated += UpdateBombActiveCount;
        }
    }

    private void OnDisable()
    {
        if (_cubeSpawner != null)
        {
            _cubeSpawner.CubeSpawned -= _cubeStatistics.IncrementSpawnCount;
            _cubeSpawner.CubeCreated -= _cubeStatistics.IncrementCreationCount;
            _cubeSpawner.ActiveCubeCountUpdated -= UpdateCubeActiveCount;
        }

        if (_bombSpawner != null)
        {
            _bombSpawner.BombSpawned -= _bombStatistics.IncrementSpawnCount;
            _bombSpawner.BombCreated -= _bombStatistics.IncrementCreationCount;
            _bombSpawner.ActiveBombCountUpdated -= UpdateBombActiveCount;
        }
    }

    private void UpdateCubeActiveCount()
    {
        if (_cubeSpawner != null)
        {
            int activeCount = _cubeSpawner.GetActiveElementsCount();
            _cubeStatistics.UpdateActiveCount(activeCount);
        }
    }

    private void UpdateBombActiveCount()
    {
        if (_bombSpawner != null)
        {
            int activeCount = _bombSpawner.GetActiveElementsCount();
            _bombStatistics.UpdateActiveCount(activeCount);
        }
    }
}