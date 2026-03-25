using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorChanger : MonoBehaviour
{
    private Renderer _renderer;
    private Material _originalMaterial;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _originalMaterial = new Material(_renderer.material);
    }

    public void ChangeColor(Color newColor)
    {
        _renderer.material.color = newColor;
    }

    public void ResetColor()
    {
        _renderer.material = _originalMaterial;
    }
}
