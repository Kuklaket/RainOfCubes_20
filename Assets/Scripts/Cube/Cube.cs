using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private float _minReleaseDelay = 2f;
    private float _maxReleaseDelay = 5f;
    private bool _hasCollided = false;
    private ColorChanger _colorChanger;

    public event Action<Cube> Released;

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_hasCollided && collision.gameObject.TryGetComponent<Platform>(out _))
        {
            _hasCollided = true;
            _colorChanger.ChangeColor(Color.red);
            StartCoroutine(ScheduleRelease(GetRandomTime()));
        }
    }

    public void ResetState()
    {
        _hasCollided = false;
        _colorChanger.ResetColor();
    }

    private float GetRandomTime()
    {
        return UnityEngine.Random.Range(_minReleaseDelay, _maxReleaseDelay);
    }

    private IEnumerator ScheduleRelease(float delay)
    {
        yield return new WaitForSeconds(delay);
        Released?.Invoke(this);
    }
}