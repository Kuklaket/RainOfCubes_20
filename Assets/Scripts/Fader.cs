using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Fader : MonoBehaviour
{
    [SerializeField] private float _minFadeTime = 2f;
    [SerializeField] private float _maxFadeTime = 5f;

    [SerializeField][Range(0f, 1f)] private float _startAlpha = 1f;
    [SerializeField][Range(0f, 1f)] private float _endAlpha = 0f;
    private Renderer _renderer;
    private Material _material;
    private bool _isInitialized = false;

    public event Action FadeCompleted;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (_isInitialized) return;

        _renderer = GetComponent<Renderer>();
        if (_renderer != null)
        {
            _material = _renderer.material;
            _isInitialized = true;
        }
    }

    public void StartFade()
    {
        if (!_isInitialized)
        {
            Initialize();
        }

        if (_material == null)
        {
            return;
        }

        float fadeTime = UnityEngine.Random.Range(_minFadeTime, _maxFadeTime);
        StartCoroutine(Fade(fadeTime));
    }

    private IEnumerator Fade(float fadeTime)
    {
        float elapsedTime = 0f;
        Color startColor = _material.color;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(_startAlpha, _endAlpha, elapsedTime / fadeTime);

            if (_material != null)
            {
                _material.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            }
            else
            {
                yield break;
            }

            yield return null;
        }

        Color finalColor = _material.color;
        finalColor.a = _endAlpha;
        _material.color = finalColor;

        FadeCompleted?.Invoke();
    }

    public void ResetFader()
    {
        if (!_isInitialized)
        {
            Initialize();
        }

        if (_material != null)
        {
            Color resetColor = _material.color;
            resetColor.a = _startAlpha;
            _material.color = resetColor;
        }
    }
}