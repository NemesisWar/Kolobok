using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    [SerializeField] private Transform _playerPosition;
    [SerializeField] private List<GridObject> _objects;
    [SerializeField] private int _viewRangeFront;
    [SerializeField] private int _viewRangeBack;
    private HashSet<Vector2Int> _collisionMatrix = new HashSet<Vector2Int>();

    private void Start()
    {
        WatchRange(ConvertToVector2Int(_playerPosition.position) - new Vector2Int(_viewRangeBack, 0), _viewRangeBack);
    }

    private void Update()
    {
        WatchRange(ConvertToVector2Int(_playerPosition.position), _viewRangeFront);
    }

    private void WatchRange(Vector2Int player, int distance)
    {
        for (int x = 0; x < distance; x++)
        {
            foreach (var gridObject in _objects)
            {
                TryCreateObject(gridObject, player + new Vector2Int(x, 0), distance);
            }
        }
    }

    private void TryCreateObject(GridObject createdObject, Vector2Int createPosition, int distance)
    {
        createPosition.y = Random.Range(0, createdObject.MaxLayer);
        if (_collisionMatrix.Contains(createPosition))
        {
            return;
        }
        else
        {
            _collisionMatrix.Add(createPosition);
        }

        if (CanCreate(createdObject.SpawnChange) == true)
        {
            var CreatedTemplate = Instantiate(createdObject, (Vector2)createPosition, Quaternion.identity, transform);
            CreatedTemplate.GetComponent<Cleaner>().Init(_playerPosition, distance);
            CreatedTemplate.GetComponent<Cleaner>().ExcessDistance += OnExcessDistance;
        }
    }

    private Vector2Int ConvertToVector2Int(Vector2 playerPosition)
    {
        return new Vector2Int(
            (int)(playerPosition.x),
            (int)(playerPosition.y));
    }

    private bool CanCreate(int maxChange)
    {
        return maxChange > Random.Range(0, 100);
    }

    private void OnExcessDistance(Vector2Int deletedObject, GameObject unsubscribeObject)
    {
        _collisionMatrix.Remove(deletedObject);
        unsubscribeObject.GetComponent<Cleaner>().ExcessDistance -= OnExcessDistance;
    }
}
