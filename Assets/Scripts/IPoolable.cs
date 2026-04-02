using System;

public interface IPoolable
{
    event Action<IPoolable> ReturnToPoolRequested;
    void ResetState();
}
