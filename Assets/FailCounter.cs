using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FailCounter : MonoBehaviour
{
	private static TextMeshPro _textMeshPro;
	private static int _count;
	private void Awake()
	{
		_textMeshPro = GetComponent<TextMeshPro>();
	}
	public static int Count
	{
		get
		{
			return _count;
		}
		set
		{
			_count = value;
			ValueChange();
		}
	}

	static void ValueChange()
	{
		_textMeshPro.text = Count.ToString();
	}
	
	
}
