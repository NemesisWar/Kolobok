using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cleaner : MonoBehaviour
{
    public event UnityAction<Vector2Int, GameObject> ExcessDistance;
    private int _deleteRadius = 1;
    private Transform _player;

    public void Init(Transform player, int vievRadius)
    {
        _player = player;
        _deleteRadius += vievRadius;
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, _player.position) > _deleteRadius)
        {
            ExcessDistance?.Invoke(new Vector2Int((int)transform.position.x, (int)transform.position.y), gameObject);
            Destroy(gameObject);
        }
    }
}
