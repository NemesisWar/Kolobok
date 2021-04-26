using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AutoDeleteMap : MonoBehaviour
{
    public UnityAction<Vector2Int,GameObject> DeleteObject;
    private Transform _player;

    public void Init(Transform player)
    {
        _player = player;
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position,_player.position) > 21)
        {
            DeleteObject?.Invoke(new Vector2Int((int)transform.position.x, (int)transform.position.y),gameObject);
            Destroy(gameObject);
        }
    }
}
