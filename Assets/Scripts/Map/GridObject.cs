using UnityEngine;

public class GridObject : MonoBehaviour
{
    public GridLayer Layer => _layer;

    public int SpawnChange => _spawnChange;

    [SerializeField] private GridLayer _layer;
    [SerializeField] private int _spawnChange;

    private void OnValidate()
    {
        _spawnChange = Mathf.Clamp(_spawnChange, 1, 100);
    }
}
