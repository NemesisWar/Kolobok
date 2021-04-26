using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GenerateMap : MonoBehaviour
{
    [SerializeField] private Transform _playerPosition;
    [SerializeField] private List<GridObject> _objects;
    [SerializeField] private int _viewRadius;
    [SerializeField] private int _cellSize;
    private HashSet<Vector2Int> _collisionMatrix = new HashSet<Vector2Int>();

    private void Update()
    {
        WatchRadius(_playerPosition.position, _viewRadius);
    }

    private void OnDeleteFromHashMap(Vector2Int deletedObject, GameObject unsubscribeObject)
    {
        _collisionMatrix.Remove(deletedObject);
        unsubscribeObject.GetComponent<AutoDeleteMap>().DeleteObject -= OnDeleteFromHashMap;
    }

    private void WatchRadius(Vector2 player, int radius)
    {
        int countCellOnAxis = radius / _cellSize;
        var fillAreaCenter = WorldToGridPosition(player);
        for (int x = -countCellOnAxis; x < countCellOnAxis; x++)
        {
            TryCreateObject(GridLayer.Ground, fillAreaCenter + new Vector2Int(x, 0));
            TryCreateObject(GridLayer.OnGround, fillAreaCenter + new Vector2Int(x, 0));
            TryCreateObject(GridLayer.Gold, fillAreaCenter + new Vector2Int(x, 0));
        }
    }

    private void TryCreateObject(GridLayer layer, Vector2Int createPosition)
    {
        createPosition.y = Random.Range(0, (int)layer);

        if (_collisionMatrix.Contains(createPosition))
        {
            return;
        }
        else
        {
            _collisionMatrix.Add(createPosition);
        }

        var template = GetRandomTemplate(layer);
        if (template == null)
        {
            return;
        }

        var position = GridToWorldPosition(createPosition);

        var CreatedTemplate = Instantiate(template, position, Quaternion.identity, transform);
        CreatedTemplate.GetComponent<AutoDeleteMap>().Init(_playerPosition);
        CreatedTemplate.GetComponent<AutoDeleteMap>().DeleteObject += OnDeleteFromHashMap;
    }

    private Vector2Int WorldToGridPosition(Vector2 worldPosition)
    {
        return new Vector2Int(
            (int)(worldPosition.x / _cellSize),
            (int)(worldPosition.y / _cellSize));
    }

    private Vector2 GridToWorldPosition(Vector2Int gridPosition)
    {
        return new Vector2(
            gridPosition.x * _cellSize,
            gridPosition.y * _cellSize);
    }

    private GridObject GetRandomTemplate(GridLayer layer)
    {
        var variants = _objects.Where(template => template.Layer == layer);
        foreach (var template in variants)
        {
            if (template.SpawnChange > Random.Range(0, 100))
            {
                return template;
            }
        }
        return null;
    }
}
