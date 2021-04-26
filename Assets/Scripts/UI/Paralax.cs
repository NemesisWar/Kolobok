using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class Paralax : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private PlayerMover _playerMover;
    private RawImage _image;
    private float _imagePositionX;

    private void OnEnable()
    {
        _playerMover.PlayerMoved += OnMove;
    }

    private void OnDisable()
    {
        _playerMover.PlayerMoved -= OnMove;
    }

    private void Start()
    {
        _image = GetComponent<RawImage>();
    }

    private void OnMove(bool blockMove)
    {
        if (blockMove == false)
        {
            _imagePositionX += _speed * Time.deltaTime;
            _image.uvRect = new Rect(_imagePositionX, 0, _image.uvRect.width, _image.uvRect.height);
        }
    }

    //private void Update()
    //{
    //    _imagePositionX += _speed * Time.deltaTime;
    //    _image.uvRect = new Rect(_imagePositionX, 0, _image.uvRect.width, _image.uvRect.height);
    //}
}
