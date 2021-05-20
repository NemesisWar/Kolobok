using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[RequireComponent(typeof(GenerateMap))]
public class Cleaner : MonoBehaviour
{
    [SerializeField] private PlayerMover _playerMover;
    private int _viewRadius = 1;
    private List<GridObject>_mapObjects = new List<GridObject>();
    private GenerateMap _generateMap;

    public event UnityAction<Vector2Int> BecameInvisible;

    private void Awake()
    {
        _generateMap = GetComponent<GenerateMap>();
        _viewRadius += _generateMap.ViewDistance;
    }

    private void OnEnable()
    {
        _generateMap.ObjectCreated += OnObjectCreated;
        _playerMover.PositionChanged += OnPositionChanged;
    }

    private void OnDisable()
    {
        _generateMap.ObjectCreated -= OnObjectCreated;
        _playerMover.PositionChanged -= OnPositionChanged;
    }

    private void OnPositionChanged(int playerPositionX)
    {
        List<GridObject> removableObjects = new List<GridObject>();
        foreach (var removableObject in _mapObjects)
        {
            if ((playerPositionX- removableObject.transform.position.x) > _viewRadius)
            {
                BecameInvisible?.Invoke(new Vector2Int((int)removableObject.transform.position.x, (int)removableObject.transform.position.y));
                removableObjects.Add(removableObject);
            }
        }
        _mapObjects = _mapObjects.Except(removableObjects).ToList();
        foreach (var removableObject in removableObjects)
        {
            Destroy(removableObject.gameObject);
        }
    }

    private void OnObjectCreated(GridObject gridObject)
    {
        _mapObjects.Add(gridObject);
    }
}
