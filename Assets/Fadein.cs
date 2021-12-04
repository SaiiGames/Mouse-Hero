using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Fadein : MonoBehaviour
{
	
	private void Start()
	{
		var image = GetComponent<Image>();
		var color = image.color;
		image.color = new Color(color.r, color.g, color.b, 1f);
		image.DOFade(0f, 2f);
		
	}
}
