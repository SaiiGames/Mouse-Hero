using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour
{
	private Camera _cameraInstance;
	private SpriteRenderer _image;
	private Tweener _tweener,_tweener2;
	private LineRenderer _lineRenderer;
	private bool isDrag = false;
	public Sprite okImage, forbidImage,dragImage;
	
	
	private void Awake()
	{
		Cursor.visible = false;
		// Cursor.lockState = CursorLockMode.Locked;
		_cameraInstance = Camera.main;
		_image = GetComponent<SpriteRenderer>();
		_lineRenderer = GetComponent<LineRenderer>();
	}

	private void Start()
	{
		_image.color = new Color(255, 255, 255, 0);
	}

	private void LateUpdate()
	{
		Vector2 cursorPos = _cameraInstance.ScreenToWorldPoint(Input.mousePosition);
		transform.position = cursorPos;

		ChangeCursorSprite();
		
		if (Input.GetKeyDown(KeyCode.Mouse0) && CharacterController2D.m_Grounded)
		{
			// _lineRenderer.enabled = true;
			isDrag = true;			
			// _lineRenderer.endColor = new Color(255, 255, 255, 255);
			// _lineRenderer.SetPosition(0,transform.position);
		}
		else if (Input.GetKey(KeyCode.Mouse0))
		{
			// _lineRenderer.SetPosition(1,transform.position);
			
		}
		else if (!Input.GetKey(KeyCode.Mouse0))
		{
			// _lineRenderer.enabled = false;
			//
			// _lineRenderer.SetPosition(0,Vector2.zero);
			// _lineRenderer.SetPosition(1,Vector2.zero);

			isDrag = false;
			DOTween.Kill(_tweener);
			DOTween.Kill(_tweener2);
			_image.transform.DOScale(Vector3.one, 0.1f);
			// _lineRenderer.endColor = new Color(255, 255, 255, 0);
		}
		
	}

	void ChangeCursorSprite()
	{
		if(!isDrag && CharacterController2D.m_Grounded)
		{
			_image.color = new Color(255, 255, 255, 1f);
			_tweener = _image.DOFade(0.2f, 0.2f);
			_image.sprite = okImage;
		}
		else if (isDrag && CharacterController2D.m_Grounded)
		{
			_image.sprite = dragImage;
			_tweener2 = _image.transform.DOScale(new Vector3(0.6f, 0.6f, 0.6f), 0.1f);
			_tweener = _image.DOFade(1f, 0.2f);

		}
		else if (!CharacterController2D.m_Grounded)
		{
			_image.color = new Color(255, 255, 255, 0.2f);
			_image.sprite = forbidImage;
		}
		
	}

	
}
