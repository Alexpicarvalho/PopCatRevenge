using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _airSpeedLoss = 0f;
    public Vector2 _movementDirection;
    Rigidbody2D _rb;
    Animator _anim;

    [Header("Jump")]
    [SerializeField] float _jumpForce = 2f;
    [SerializeField] bool _grounded;
    [SerializeField] float _inputBufferDuration;
    [SerializeField] float _coyoteTime;
    private float _coyoteTimer;
    private float _inputTimer;

    [Header("Checks")]
    [SerializeField] private Transform _headCheck;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private float _headCheckDistance;
    [SerializeField] Vector2 _rbVelocity;

    [Header("Miscelaneous")]
    [SerializeField] bool _activateGizmos;
    [SerializeField] Color _gizmosColor = Color.red;

    [Header("Rotation")]
    [SerializeField] bool _facingRight = true;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _coyoteTimer = _coyoteTime;
        _inputTimer = _inputBufferDuration;
    }

    // Update is called once per frame
    void Update()
    {

        float xMov = Input.GetAxis("Horizontal");

        GroundCheck();
        JumpInput();
        _movementDirection = new Vector3(xMov, 0).normalized;

        if (Mathf.Abs(xMov) >= 0.1f) _anim.SetBool("isRunning", true);
        else _anim.SetBool("isRunning", false);



        if (_coyoteTimer > 0f && _inputTimer > 0)
        {
            _anim.SetBool("isJumping", true);
            _rb.velocity = new Vector2(_rbVelocity.x, _jumpForce);
        }

        if (Input.GetKeyUp(KeyCode.W) && _rb.velocity.y > 0f)
        {
            _rb.velocity = new Vector2(_rbVelocity.x, _rbVelocity.y * 0.5f);
            _coyoteTimer = 0f;
        }



    }

    private void JumpInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _inputTimer = _inputBufferDuration;
        }
        else _inputTimer -= Time.deltaTime;
    }

    private void Flip()
    {
        if (_facingRight && _movementDirection.x < 0f || !_facingRight && _movementDirection.x > 0f)
        {
            _facingRight = !_facingRight;
            //Vector3 localScale = transform.localScale;
            //localScale.x *= -1f;
            //transform.localScale = localScale;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void FixedUpdate()
    {
        if (_grounded) _rb.velocity = new Vector3(_movementDirection.x * _speed, _rb.velocity.y);
        else _rb.velocity = new Vector3(_movementDirection.x * (_speed * (1 - _airSpeedLoss)), _rb.velocity.y);

        Flip();
    }

    void GroundCheck()
    {
        if (Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _whatIsGround))
        {
            _coyoteTimer = _coyoteTime;
            _grounded = true;
            _anim.SetBool("isJumping", false);
        }
        else
        {
            _coyoteTimer -= Time.deltaTime;
            _grounded = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (!_activateGizmos) return;

        Gizmos.color = _gizmosColor;
        Gizmos.DrawSphere(_groundCheck.position, _groundCheckRadius);
    }

}
