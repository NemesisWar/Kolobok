using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    public UnityAction<bool> PlayerMoved;
    [SerializeField] private float _raydistance;
    [SerializeField] private float _powerJump;
    [SerializeField] private float _speed;
    private bool _onGround;
    private bool _blockMove;
    private PlayerInput _playerInput;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _playerInput.PlayerAction.Jump.performed += ctx => OnJump();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnJump()
    {
        if (_onGround == true)
        {
            _rigidbody2D.AddForce(new Vector2(0, _powerJump), ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        _onGround = CheckGround(Vector2.down);
        _blockMove = CheckGround(Vector2.right);
        if (_blockMove == false)
        {
            PlayerMoved?.Invoke(_blockMove);
            Vector2 move = new Vector2(transform.position.x + (Vector2.right.x * _speed * Time.deltaTime), transform.position.y);
            _rigidbody2D.position = move;
        }
    }

    private bool CheckGround(Vector2 rayVector)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayVector,_raydistance,LayerMask.NameToLayer("Player"));
        if (hit.collider == null)
        {
            return false;
        }
        return _raydistance > hit.distance;
    }
}
