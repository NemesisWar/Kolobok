using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cleaner : MonoBehaviour
{
    private int _viewRadius = 1;
    private Transform _player;
    public event UnityAction<Vector2Int, GameObject> ObjectNotVisible;

    public void Init(Transform player, int vievRadius)
    {
        _player = player;
        _viewRadius += vievRadius;
    }

    private void FixedUpdate()
    {
        if ((_player.position.x - transform.position.x) > _viewRadius)
        {
            ObjectNotVisible?.Invoke(new Vector2Int((int)transform.position.x, (int)transform.position.y), gameObject);
            Destroy(gameObject);
        }
    }
}
