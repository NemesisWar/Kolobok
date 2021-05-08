using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    [SerializeField] private List<GridObject> _gridObjects;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private int _viewDistance;
    private HashSet<Vector2Int> _spawnedPositions = new HashSet<Vector2Int>();

    private void OnEnable()
    {
        _playerMover.PositionChanged += OnPositionChanged;
    }

    private void OnDisable()
    {
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
        foreach (var grid in _gridObjects)
        {
            if (CanSpawn(grid) == true)
            {
                Spawn(grid, spawnPoint);
            }
        }
    }

    private bool CanSpawn(GridObject gridObject)
    {
        return gridObject.SpawnChange > Random.Range(0, 100);
    }

    private void Spawn(GridObject spawnObject, Vector2Int spawnPoint)
    {
        bool spawnPointIsFree = false;
        while (spawnPointIsFree == false)
        {
            spawnPoint.y = Random.Range(0, spawnObject.MaxLayer);
            if (_spawnedPositions.Contains(spawnPoint))
            {
                return;
            }
            else
            {
                spawnPointIsFree = true;
                _spawnedPositions.Add(spawnPoint);
                var createdObject = Instantiate(spawnObject, (Vector2)spawnPoint, Quaternion.identity, transform);
                createdObject.GetComponent<Cleaner>().Init(_playerMover.transform, _viewDistance);
                createdObject.GetComponent<Cleaner>().ObjectNotVisible += OnObjectNotVisible;
            }
        }
    }

    private void OnObjectNotVisible(Vector2Int deletedObject, GameObject unsubscribeObject)
    {
        _spawnedPositions.Remove(deletedObject);
        unsubscribeObject.GetComponent<Cleaner>().ObjectNotVisible -= OnObjectNotVisible;
    }
}
