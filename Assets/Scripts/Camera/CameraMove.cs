using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform _player;
    private float _offsetX;

    private void Start()
    {
        _offsetX = transform.position.x - _player.position.x;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(_player.position.x + _offsetX, transform.position.y, transform.position.z);
    }
}
