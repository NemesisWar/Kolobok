using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _rayDistance;
    [SerializeField] private float _powerJump;
    [SerializeField] private float _speed;
    private int _previousPositionX;
    private PlayerInput _playerInput;
    private Rigidbody2D _rigidbody2D;

    public event UnityAction<bool> PlayerMoved;
    public event UnityAction<int> PositionChanged;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _previousPositionX = (int)_rigidbody2D.position.x;
    }

    private void OnDisable()
    {
        _playerInput.PlayerAction.Jump.performed -= ctx => TryJump();
        _playerInput.Disable();
    }

    private void OnEnable()
    {
        _playerInput.PlayerAction.Jump.performed += ctx => TryJump();
        _playerInput.Enable();
    }

    private void TryJump()
    {
        if (CheckGround(Vector2.down) == true)
        {
            _rigidbody2D.AddForce(new Vector2(0, _powerJump), ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (CheckGround(Vector2.right) == false)
        {
            PlayerMoved?.Invoke(CheckGround(Vector2.right));
            var nextPosition = new Vector2(transform.position.x + (Vector2.right.x * _speed * Time.deltaTime), transform.position.y);
            if ((int)nextPosition.x > _previousPositionX)
            {
                _previousPositionX = (int)nextPosition.x;
                PositionChanged?.Invoke((int)nextPosition.x);
            }
            _rigidbody2D.position = nextPosition;
        }
    }

    private bool CheckGround(Vector2 rayVector)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayVector, _rayDistance, LayerMask.NameToLayer("Player"));
        if (hit.collider == null)
        {
            return false;
        }
        return _rayDistance > hit.distance;
    }
}
