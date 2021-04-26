using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform _player;
    private Camera _camera;
    private Vector3 _cameraPosition;
    private float _distanseBetweenPlayerAndCameraX;

    private void Start()
    {
        _camera = GetComponent <Camera>();
        _cameraPosition = _camera.transform.position;
        _distanseBetweenPlayerAndCameraX = _camera.transform.position.x - _player.position.x;
    }

    private void LateUpdate()
    {
        _cameraPosition = new Vector3(_player.position.x + _distanseBetweenPlayerAndCameraX, _cameraPosition.y, -10f);
        _camera.transform.position = _cameraPosition;
    }
}
