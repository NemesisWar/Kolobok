using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Cleaner))]
public class GenerateMap : MonoBehaviour
{
    public int ViewDistance => _viewDistance;
    [SerializeField] private List<GridObject> _gridObjects;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private int _viewDistance;
    [SerializeField] private Cleaner _cleaner;
    private HashSet<Vector2Int> _spawnedPositions = new HashSet<Vector2Int>();

    public event UnityAction<GridObject> ObjectCreated;

    private void OnEnable()
    {
        _cleaner = GetComponent<Cleaner>();
        _cleaner.BecameInvisible += OnBecameInvisibles;
        _playerMover.PositionChanged += OnPositionChanged;
    }

    private void OnDisable()
    {
        _cleaner.BecameInvisible -= OnBecameInvisibles;
        _playerMover.PositionChanged -= OnPositionChanged;
    }

    private void Start()
    {
        for (int i = -_viewDistance; i < _viewDistance + 1; i++)
        {
            TrySpawn(new Vector2Int((int)_playerMover.transform.position.x + i, 0));
        }
    }

    private void OnPositionChanged(int playerPositionX)
    {
        TrySpawn(new Vector2Int(playerPositionX + _viewDistance, 0));
    }

    private void TrySpawn(Vector2Int spawnPoint)
    {
        foreach (var mapObject in _gridObjects)
        {
            if (CanSpawn(mapObject) == true)
            {
                Spawn(mapObject, spawnPoint);
            }
        }
    }

    private bool CanSpawn(GridObject gridObject)
    {
        return gridObject.SpawnChange > Random.Range(0, 100);
    }

    private void Spawn(GridObject spawnObject, Vector2Int spawnPoint)
    {
        spawnPoint.y = Random.Range(0, spawnObject.MaxLayer);
        if (_spawnedPositions.Contains(spawnPoint))
        {
            return;
        }
        else
        {
            _spawnedPositions.Add(spawnPoint);
            var createdObject = Instantiate(spawnObject, (Vector2)spawnPoint, Quaternion.identity, transform);
            ObjectCreated?.Invoke(createdObject);
        }
    }

    private void OnBecameInvisibles(Vector2Int objectPosition)
    {
        _spawnedPositions.Remove(objectPosition);
    }
}
