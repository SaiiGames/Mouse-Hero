using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceHelper : MonoBehaviour
{
	[SerializeField] private LineRenderer _lineRenderer;

	private void Awake()
	{
	}

	public void ShowTrace(Vector2 _startPos)
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
