using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    private Vector3 _position;
    private bool _hasPendingSpawn = false;

    public void SpawnBombAtPosition(Vector3 position)
    {
        _position = position;
        _hasPendingSpawn = true;
        SpawnObject();
    }

    protected override Bomb SpawnObject()
    {
        Bomb bomb = base.SpawnObject();

        if (_hasPendingSpawn)
        {
            bomb.transform.position = _position;
            _hasPendingSpawn = false;
        }

        return bomb;
    }
}