using UnityEngine;
using TMPro;

[System.Serializable]
public class TrackedObjectStats<T> where T : MonoBehaviour
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
          _totalText.text = _totalSpawned.ToString();
          _createdText.text = _totalCreated.ToString();
          _activeText.text = _activeObjects.ToString();
    }
}