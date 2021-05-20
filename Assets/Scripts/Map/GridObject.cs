using UnityEngine;

public class GridObject : MonoBehaviour
{
    public int MaxLayer => _maxLayer;
    public int SpawnChange => _spawnChange;

    [SerializeField] private int _maxLayer;
    [SerializeField] private int _spawnChange;

    private void OnValidate()
    {
        _spawnChange = Mathf.Clamp(_spawnChange, 1, 100);
    }
}
