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
    [SerializeField] private LineRenderer _lineRenderer;
    private bool _startDrag = false;
    private static readonly int HorizonAbsSpeed = Animator.StringToHash("HorizonAbsSpeed");
    private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
    [SerializeField] private GameObject characterSprite;
    public bool showTraceHelper = false;
    public Vector2 LastPosition;
    private void Awake()
    {
        _controller = GetComponent<CharacterController2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = spriteTransform.GetComponent<Animator>();
        
    }
    private void Start()
    {
        LastPosition = transform.position;
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
            LastPosition = transform.position;
            _controller.Jump(_endPos-_startPos,jumpForce);
            
        }
        if (showTraceHelper)
        {
            TraceHelper();
        }
    }

    private void FixedUpdate()
    {
        var speed = _rigidbody.velocity;
        _animator.SetFloat(VerticalAbsSpeed,Mathf.Abs(speed.y));
        _animator.SetFloat(HorizonAbsSpeed,Mathf.Abs(speed.x));
        _animator.SetBool(IsGrounded,CharacterController2D.m_Grounded);
        // Debug.Log(speed.x);
        if (speed.x < -0.1 &&!_faceLeft)
        {
            Flip(-1);
            _faceLeft = !_faceLeft;
        }
        if (speed.x > 0.1 && _faceLeft)
        {
            Flip(1);
            _faceLeft = !_faceLeft;

        }
    }

    private void Flip(int _direction)
    {
        // Debug.Log(_direction);
        var transform1 = characterSprite.transform;
        Vector3 scale = transform1.localScale;
        scale.x *= -1;
        transform1.localScale = scale;
        
    }

    public void CheckFreeFall()
    {
        // Debug.Log("checking free fall");
        var fallLength = transform.position.y - LastPosition.y;
        // Debug.Log(fallLength);
        LastPosition = transform.position;
        if (fallLength < -8)
        {
            FailCounter.Count++;
            Debug.Log("freeFall");
        }
    }
    
    void TraceHelper()
    {
        GetComponent<TraceHelper>().ShowTrace(_startPos);
    }

    
}
