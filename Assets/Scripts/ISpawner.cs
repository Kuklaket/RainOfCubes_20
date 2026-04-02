using System;

public interface ISpawner
{
    event Action ObjectSpawned;
    event Action ObjectCreated;
    event Action ActiveObjectCountUpdated;

    int GetActiveElementsCount();
}