using System;
using UnityEngine;

[RequireComponent(typeof(Fader), typeof(Exploder))]
public class Bomb : MonoBehaviour
{
    private Fader _fader;
    private Exploder _exploder;

    public event Action<Bomb> ExplosionCompleted;

    private void Awake()
    {
        _fader = GetComponent<Fader>();
        _exploder = GetComponent<Exploder>();
    }

    private void OnEnable()
    {
        _fader.FadeCompleted += HandleFadeComplete;
        _fader.StartFade();

        _exploder.Exploded += NotifyCompletion;
    }

    private void OnDisable()
    {
        _fader.FadeCompleted -= HandleFadeComplete;
        _exploder.Exploded -= NotifyCompletion;
    }

    private void HandleFadeComplete()
    {
        _exploder.Explode(gameObject);
    }

    private void NotifyCompletion(GameObject owner)
    {
        ExplosionCompleted?.Invoke(this);
    }

    public void ResetState()
    {
        _fader.ResetFader();
    }
}