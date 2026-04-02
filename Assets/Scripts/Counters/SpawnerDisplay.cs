using UnityEngine;
using TMPro;

[System.Serializable]
public class SpawnerDisplay
{
    [SerializeField] private TMP_Text _totalText;
    [SerializeField] private TMP_Text _createdText;
    [SerializeField] private TMP_Text _activeText;

    private int _totalSpawned = 0;
    private int _totalCreated = 0;
    private int _activeObjects = 0;

    public void IncrementSpawnCount()
    {
        _totalSpawned++;
        UpdateDisplay();
    }

    public void IncrementCreationCount()
    {
        _totalCreated++;
        UpdateDisplay();
    }

    public void UpdateActiveCount(int count)
    {
        _activeObjects = count;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (_totalText != null) _totalText.text = _totalSpawned.ToString();
        if (_createdText != null) _createdText.text = _totalCreated.ToString();
        if (_activeText != null) _activeText.text = _activeObjects.ToString();
    }
}