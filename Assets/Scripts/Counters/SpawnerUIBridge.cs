using UnityEngine;

public class SpawnerUIBridge : MonoBehaviour
{
    [SerializeField] private SpawnerDisplay _statistics;
    [SerializeField] private ISpawner _spawner;

    private void Awake()
    {
        _spawner = GetComponent<ISpawner>();
    }

    private void OnEnable()
    {
        if (_spawner != null)
        {
            _spawner.ObjectSpawned += _statistics.IncrementSpawnCount;
            _spawner.ObjectCreated += _statistics.IncrementCreationCount;
            _spawner.ActiveObjectCountUpdated += UpdateActiveCount;

            UpdateActiveCount();
        }
    }

    private void OnDisable()
    {
        if (_spawner != null)
        {
            _spawner.ObjectSpawned -= _statistics.IncrementSpawnCount;
            _spawner.ObjectCreated -= _statistics.IncrementCreationCount;
            _spawner.ActiveObjectCountUpdated -= UpdateActiveCount;
        }
    }

    private void UpdateActiveCount()
    {
        if (_spawner != null)
        {
            int activeCount = _spawner.GetActiveElementsCount();
            _statistics.UpdateActiveCount(activeCount);
        }
    }
}