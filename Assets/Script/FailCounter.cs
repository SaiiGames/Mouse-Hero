using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FailCounter : MonoBehaviour
{
	private static List<TextMeshPro> _textMeshPros = new List<TextMeshPro>();
	private static AudioSource _audioSource;
	private static int _count;
	private void Awake()
	{
		_textMeshPros.Add(GetComponent<TextMeshPro>());
		_audioSource = GetComponent<AudioSource>();
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
		foreach (var VARIABLE in _textMeshPros)
		{
			VARIABLE.text = Count.ToString();
		}
		if(!_audioSource.isPlaying) { _audioSource.Play(); }
	}
	
	
}
