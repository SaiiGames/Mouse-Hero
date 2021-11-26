using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    public Transform spriteTransform;
    private CharacterController2D _controller;
    private static readonly int StartJump = Animator.StringToHash("StartJump");
    private static readonly int VerticalAbsSpeed = Animator.StringToHash("VerticalAbsSpeed");
    private bool _faceLeft = true;
    private Vector2 _startPos, _endPos;
    [SerializeField] private float jumpForce = 1f;

    private bool _startDrag = false;
    private void Awake()
    {
        _controller = GetComponent<CharacterController2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = spriteTransform.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !_startDrag)
        {
            _startDrag = true;
            _startPos = Input.mousePosition;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && _startDrag)
        {
            _startDrag = false;
            _endPos = Input.mousePosition;
            
            _controller.Jump(_endPos-_startPos,jumpForce);
        }
    }

    private void FixedUpdate()
    {
        var speed = _rigidbody.velocity;
        _animator.SetFloat(VerticalAbsSpeed,Mathf.Abs(speed.y));

        // Debug.Log(speed.x);
        if (speed.x < 0 &&!_faceLeft)
        {
            Flip(-1);
            _faceLeft = !_faceLeft;
        }
        if (speed.x > 0 && _faceLeft)
        {
            Flip(1);
            _faceLeft = !_faceLeft;

        }
    }

    private void Flip(int _direction)
    {
        // Debug.Log(_direction);
        var transform1 = transform;
        Vector3 scale = transform1.localScale;
        scale.x *= -1;
        transform1.localScale = scale;
        
    }

    
}
