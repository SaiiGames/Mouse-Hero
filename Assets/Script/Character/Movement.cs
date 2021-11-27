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
        var transform1 = characterSprite.transform;
        Vector3 scale = transform1.localScale;
        scale.x *= -1;
        transform1.localScale = scale;
        
    }

    void TraceHelper()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _lineRenderer.gameObject.SetActive(false);
        }
        if (CharacterController2D.m_Grounded)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _lineRenderer.gameObject.SetActive(true);
                _lineRenderer.SetPosition(1,Vector3.zero);
            }
            else if(Input.GetKey(KeyCode.Mouse0))
            {
                
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var worldStartPos = Camera.main.ScreenToWorldPoint(_startPos);
                Vector2 distance = mousePos - new Vector2(worldStartPos.x,worldStartPos.y);
                // Debug.Log(distance);
                // var position = transform.position;
                // Vector2 targetPos = new Vector2(position.x,position.y) + distance;
                _lineRenderer.SetPosition(1,distance/3);

            }
            
        }

    }

    
}
